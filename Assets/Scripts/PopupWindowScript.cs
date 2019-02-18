using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopupWindowScript : MonoBehaviour
{
    private Image image, childImage;
    public CanvasGroup cg;
    private bool open, transitioning1, transitioning2;
    private WaitForSeconds tran2Time, fullTime;
    private float r1, r2;

    public bool testButton;


    void Start()
    {
        image = GetComponent<Image>();
        childImage = transform.GetChild(2).GetComponent<Image>();
        tran2Time = new WaitForSeconds(.06f);
        fullTime = new WaitForSeconds(1.94f);
    }

    void Update()
    {
        // Test
        if (testButton)
        {
            SetOpen(!open);
            testButton = false;
        }

        if (transitioning1)
        {
            image.fillAmount = Mathf.SmoothDamp(image.fillAmount, (open ? 1 : 0), ref r1, .2f);
            cg.alpha = image.fillAmount * .8f;
        }
        if (transitioning2)
            childImage.fillAmount = Mathf.SmoothDamp(childImage.fillAmount, (open ? 1 : 0), ref r2, .2f);
    }

    public void SetOpen(bool setOpen)
    {
        StopAllCoroutines();
        open = setOpen;
        StartCoroutine(Transition());
    }

    public IEnumerator Transition()
    {
        transitioning1 = true;
        transitioning2 = false;
        yield return tran2Time;
        transitioning2 = true;
        yield return fullTime;
        transitioning1 = transitioning2 = false;
    }
}
