using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorPanelScript : MonoBehaviour
{
    PhoneDialogue phoneDialogue;
    public List<NodeUIScript> nuis;

    private RectTransform r;
    private Transform graphWindow;
    public float test = 10;
    public int openMode = 0; // 0 = Editor, 1 = Executor, 2 = Both
    private bool moving;
    private WaitForSeconds w;
    private float targetX;
    private float r1;
    public GameObject nodePrefab;
    public Text headerText;

    public int nodeCounter;

    void Start()
    {
        phoneDialogue = TestPhoneDialogue.testPhoneDialogue;
        nuis = new List<NodeUIScript>();
        r = GetComponent<RectTransform>();
        graphWindow = transform.GetChild(1);
        w = new WaitForSeconds(1);

        LoadGraph();
    }

    void Update()
    {
        if(moving)
            r.offsetMin = new Vector2(Mathf.SmoothDamp(r.offsetMin.x, targetX, ref r1, .15f), 0);
    }

    public void ChangeEditorMode(int newOpenMode)
    {
        openMode = newOpenMode;

        if (openMode == 0) targetX = 0;
        else if (openMode == 1) targetX = 924.6f;
        else targetX = 252.75f;

        StopAllCoroutines();
        StartCoroutine(ToggleMoving());
    }

    public void LoadGraph()
    {
        int i;

        //headerText.text = phoneDialogue.agentName;

        // Create Nodes
        for (i = 0; i < phoneDialogue.dialogueNodes.Count; i++)
            CreateNode(i);

        // Update Nodes (Particularly the node transitions
        for (i = 0; i < phoneDialogue.dialogueNodes.Count; i++)
            nuis[i].UpdateNodeUI();
    }

    public void CreateNode(int nodeID)
    {
        NodeUIScript nui = Instantiate(nodePrefab, graphWindow).GetComponent<NodeUIScript>();
        nui.eps = this;
        nui.AssignNode(phoneDialogue.dialogueNodes[nodeID]);
        nuis.Add(nui);
    }

    private IEnumerator ToggleMoving()
    {
        moving = true;
        yield return w;
        moving = false;
    }
}
