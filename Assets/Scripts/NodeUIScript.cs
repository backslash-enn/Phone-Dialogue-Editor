using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUIScript : MonoBehaviour
{
    public PhoneDialogue.DialogueNode node;

    public Text nodeIndexText;
    public TextMeshProUGUI nodeNameText, firstResponseText;
    public InputField responseDelayIF;
    public Toggle autoTransitionToggle;
    public List<TextMeshProUGUI> intentTexts;
    public List<TransitionArrowScript> transitionArrows;

    private RectTransform r;
    private Transform c;
    public GameObject dialogueNodeElemets, intentPanel;
    public GameObject intentUI;
    private Transform arrowParent;
    public GameObject transitionArrowPrefab;
    public ManagerScript ms;

    void Awake()
    {
        r = GetComponent<RectTransform>();
        c = transform.GetChild(transform.childCount - 1);
        arrowParent = GameObject.Find("Transition Arrows").transform;
    }

    void Update()
    {

    }

    public void AssignNode(PhoneDialogue.DialogueNode newNode)
    {
        node = newNode;
        intentTexts = new List<TextMeshProUGUI>();
    }

    public void UpdateNodeUI()
    {
        nodeIndexText.text = node.id.ToString();
        r.anchoredPosition = new Vector2(node.positionX, node.positionY);
        if (node.winState)
            nodeNameText.text = "<color=#44aa44>Win State</color>";
        else if (node.loseState)
            nodeNameText.text = "<color=#aa4444>Lose State</color>";
        else
            nodeNameText.text = node.nodeName;

        if (node.winState || node.loseState)
        {
            r.sizeDelta = new Vector2(r.sizeDelta.x, 64);
            dialogueNodeElemets.SetActive(false);
            c.localScale = new Vector3(26.5f, c.localScale.y, c.localScale.z);
            return;
        }

        // Intents and Transitions
        for (int i = 0; i < node.intentNameMappings.Count; i++)
        {
            intentTexts.Add(Instantiate(intentUI, intentPanel.transform).transform.GetChild(0).GetComponent<TextMeshProUGUI>());
            intentTexts[i].text = node.intentNameMappings[i];

            transitionArrows.Add(Instantiate(transitionArrowPrefab, arrowParent).GetComponent<TransitionArrowScript>());
            if (node.intentNodeMappings[i] != null)
            {
                transitionArrows[i].intent = intentTexts[i].transform;
                transitionArrows[i].target = ms.nuis[node.intentNodeMappings[i].id].transform;
                // Joints
                for (int j = 0; j < node.intentJoints[i].Count; j++)
                    transitionArrows[i].joints.Add(node.intentJoints[i][j]);
                transitionArrows[i].UpdateArrow();
            }
        }

        responseDelayIF.text = node.responseDelay.ToString();
        autoTransitionToggle.isOn = node.autoTransition;
        firstResponseText.text = (node.responseList.Count > 0 ? node.responseList[0] : "");

        r.sizeDelta = new Vector2(364 + Mathf.Max(0, (node.intentNameMappings.Count-4) * 83), r.sizeDelta.y);
        c.localScale = new Vector3(c.localScale.x, c.localScale.y + Mathf.Max(0, (node.intentNameMappings.Count - 4) * 30.5f), c.localScale.z);
    }
}
