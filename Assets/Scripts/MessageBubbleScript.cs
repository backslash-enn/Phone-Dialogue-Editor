using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBubbleScript : MonoBehaviour {

    private RectTransform r, c;
    public RectTransform t;
    private CanvasGroup tCG, cCG;
    public string message;
    public bool humanSender;
    private float temp;

    private bool ready;
    private float targetWidth, targetHeight;

    private float ref1, ref3, ref4;
    private Vector3 ref2;

	void Start () {

        r = GetComponent<RectTransform>();
        c = transform.GetChild(0).GetComponent<RectTransform>();
        tCG = t.gameObject.GetComponent<CanvasGroup>();
        cCG = c.gameObject.GetComponent<CanvasGroup>();
        c.GetComponent<Text>().text = FormatMessage(message);

        StartCoroutine(DelayedStart());
    }
	
	void Update () {

        if (!ready) return;

        // Spawning animation
        t.anchoredPosition = new Vector2(r.rect.xMin * r.localScale.x + 1, r.rect.yMin * r.localScale.y + .8f);
        t.localEulerAngles = Vector3.SmoothDamp(t.localEulerAngles, Vector3.zero, ref ref2, .3f);
        tCG.alpha = Mathf.SmoothDamp(tCG.alpha, 1, ref ref1, .7f);
        cCG.alpha = tCG.alpha;

        r.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.SmoothDamp(r.rect.width, targetWidth, ref ref3, .3f));
        r.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.SmoothDamp(r.rect.height, targetHeight, ref ref4, .3f));

    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForEndOfFrame();

        targetWidth = Mathf.Max(c.rect.width + 100, 240);
        targetHeight = c.rect.height + 80;
        PersonalMessageScript.bottomY = (r.anchoredPosition.y - targetHeight) * r.localScale.y + r.parent.GetComponent<RectTransform>().anchoredPosition.y - (1 * r.parent.GetComponent<RectTransform>().localScale.y);
        transform.parent.parent.GetComponent<PersonalMessageScript>().AdjustScrolls();
        ready = true;
        yield return new WaitForSeconds(3);
        ready = false;
    }

    string FormatMessage(string originalMessage)
    {
        string[] wordList = originalMessage.Split(' ');
        string finalMessage = "";
        int rowSpace = 20;

        // There are 3 scenarios:
            // 1. The string can fit on the line
            // 2. The string can't fit on the line, but it can fit on it's own line (<= 20 chars)
            // 3. The srting can't fit on the line, and it can't fit on it's own line (> 20 chars)

        for (int i = 0; i < wordList.Length; i++)
        {
            if (wordList[i].Length <= rowSpace) {
                finalMessage += wordList[i];
                rowSpace -= (wordList[i].Length + 1);
                if (i != wordList.Length - 1)
                    finalMessage += ' ';
            }
            else if (wordList[i].Length <= 20)
            {
                if (finalMessage.Length > 0 && finalMessage[finalMessage.Length - 1] == ' ')
                    finalMessage = finalMessage.Remove(finalMessage.Length - 1);
                finalMessage += '\n' + wordList[i];
                rowSpace = 20 - (wordList[i].Length + 1);
                if (i != wordList.Length - 1)
                    finalMessage += ' ';
            }
            else
            {
                while (wordList[i].Length > 20)
                {
                    if (finalMessage.Length > 0)
                        finalMessage += '\n';
                    finalMessage += wordList[i].Substring(0, 16) + '-';
                    wordList[i] = wordList[i].Substring(16, wordList[i].Length - 16);
                }
                i--;
                rowSpace = 0;
            }
        }

        return finalMessage;
    }
}
