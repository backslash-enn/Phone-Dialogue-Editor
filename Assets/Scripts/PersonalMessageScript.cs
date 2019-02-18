using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalMessageScript : MonoBehaviour {

    List<MessageEntry> messageList;
    public static float bottomY = -2, maxScroll = 0, h;
    public static float currentScroll = 0;
    private float scrollSpeed = .8f;
    private RectTransform r;
    public GameObject humanMessagePrefab, aiMessagePrefab;
    public AudioSource aud;
    public AudioClip recieveMessageAud, sendMessageAud;
    private bool scrolling;
    private float startScroll, startTouchPosY;
    private List<float> yList;
    private float momentum;
    public float momentumDecay, momentumStrength;
    public PhoneUIButtonScript button;

    public TextProcessorScript textProcessor;
    public PhoneDialogue phoneDialogue;
    public DialogueExecutor dialogueExecutor;

    // Use this for initialization
    void Start () {
        messageList = new List<MessageEntry>();
        r = GetComponent<RectTransform>();
        h = r.rect.height;

        yList = new List<float> { 0, 0, 0, 0, 0, 0 };

        phoneDialogue = TestPhoneDialogue.testPhoneDialogue;
    }
	
	// Update is called once per frame
	void Update () {
        // Momentum
        yList.RemoveAt(0);
        yList.Add(TouchScreenScript.touchPosition.y);

        // Scrolling
        currentScroll = Mathf.Clamp(currentScroll, 0, maxScroll);
        r.anchoredPosition = new Vector2(r.anchoredPosition.x, currentScroll);
        if (button.pressed)
        {
            if (scrolling)
                currentScroll = startScroll + (TouchScreenScript.touchPosition.y - startTouchPosY) * scrollSpeed;
            else
            {
                scrolling = true;
                startTouchPosY = TouchScreenScript.touchPosition.y;
                startScroll = currentScroll;
            }
        }
        else
        {
            if (scrolling)
            {
                scrolling = false;
                momentum = CalculateMomentum() * momentumStrength;
            }
        }

       if(Mathf.Abs(momentum) > .01f)
        {
            currentScroll += momentum;
            momentum *= momentumDecay;
        }
	}

    public void SendNewMessage(string message, bool humanSender)
    {
        GameObject prefab = Instantiate((humanSender ? humanMessagePrefab : aiMessagePrefab), transform.position, transform.rotation, transform);
        prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2((humanSender ? 1.5f : 29.5f), bottomY);
        prefab.transform.GetChild(1).GetComponent<MessageBubbleScript>().message = message;
        prefab.transform.GetChild(1).GetComponent<MessageBubbleScript>().humanSender = humanSender;
        messageList.Add(new MessageEntry(message, prefab, humanSender));
        aud.clip = (humanSender ? sendMessageAud : recieveMessageAud);
        aud.Play();

        if(humanSender)
            dialogueExecutor.ExecuteDialogue( textProcessor.CleanText(message, TestPhoneDialogue.testPhoneDialogue) );
    }

    public void AdjustScrolls()
    {
        float temp;

        temp = maxScroll;
        maxScroll = Mathf.Max(-bottomY - r.rect.height, 0);
        if (currentScroll == temp)
            currentScroll = maxScroll;
    }

    private float CalculateMomentum()
    {
        float retVal = 0;

        for(int i = 0; i < 5; i++)
            retVal += yList[i + 1] - yList[i];

        return retVal;
    }
}

public class MessageEntry
{
    public string message;
    public GameObject prefab;
    public bool humanSender;

    public MessageEntry(string inputMessage, GameObject inputPrefab, bool inputHumanSender)
    {
        message = inputMessage;
        prefab = inputPrefab;
        humanSender = inputHumanSender;
    }

    
}
