using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEntryScript : MonoBehaviour {

    public bool open;
    private RectTransform r;
    public RectTransform m;
    private float r1;
    private Text inputField, cursorField;
    public string inputText;
    private PersonalMessageScript personalMessages;
    private PhoneUIButtonScript button;
    public PhoneUIButtonScript messagePanelButton;
    int cursorPosition = 0, cursorZoneStart = 0;
    private bool shiftHeld, shiftStuck;
    public Image shiftImage;
    public Sprite[] shiftSprites;
    public Text[] letterKeyText;
    private bool cursorOn;

    void Start () {

        r = GetComponent<RectTransform>();
        inputField = transform.GetChild(0).GetComponent<Text>();
        cursorField = transform.GetChild(1).GetComponent<Text>();
        personalMessages = m.GetComponent<PersonalMessageScript>();
        button = GetComponent<PhoneUIButtonScript>();
        InvokeRepeating("BlinkCursor", 1, .5f);
    }

    void Update () {

        r.anchoredPosition = new Vector2(r.anchoredPosition.x, Mathf.SmoothDamp(r.anchoredPosition.y, (open ? -39.55f : -60f), ref r1, .15f));
        m.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, -(r.anchoredPosition.y + r.rect.yMax));
        personalMessages.AdjustScrolls();
        if (open && messagePanelButton.pressed)
            open = false;
        if (!open && button.justReleased)
            open = true;

    }

    public void InsertChar(char newChar)
    {
        inputText = inputText.Insert(cursorPosition, (shiftHeld ? char.ToUpper(newChar) : newChar).ToString());
        cursorPosition++;
        if (!shiftStuck)
        {
            shiftHeld = false;
            shiftImage.sprite = shiftSprites[0];
            CaseLetterKeys(false);
        }

        if (cursorPosition < cursorZoneStart) cursorZoneStart--;
        if (cursorPosition > cursorZoneStart + 22) cursorZoneStart++;

        inputField.text = (inputText.Length == 0 ? "Say something..." : inputText.Substring(cursorZoneStart, Mathf.Min(inputText.Length, 22)));
        inputField.color = Color.white * (inputText.Length == 0 ? .56f : .83f);

        cursorField.text = "";
        for (int i = 0; i < cursorPosition - cursorZoneStart; i++)
            cursorField.text += ' ';
        cursorField.text += "<color=#888888>|</color>";
    }

    public void MoveCursor(bool forward)
    {
        if (inputText.Length == 0) return;
        cursorPosition += (forward ? 1 : -1);
        cursorPosition = Mathf.Clamp(cursorPosition, 0, inputText.Length);
        if (cursorPosition < cursorZoneStart) cursorZoneStart--;
        if (cursorPosition > cursorZoneStart + 22) cursorZoneStart++;

        if (cursorPosition < cursorZoneStart) cursorZoneStart--;
        if (cursorPosition > cursorZoneStart + 22) cursorZoneStart++;

        inputField.text = inputText.Substring(cursorZoneStart, Mathf.Min(inputText.Length, 22));

        cursorField.text = "";
        for (int i = 0; i < cursorPosition - cursorZoneStart; i++)
            cursorField.text += ' ';
        cursorField.text += "<color=#888888>|</color>";
    }

    public void DeleteChar()
    {
        if (cursorPosition > 0)
        {
            inputText = inputText.Remove(cursorPosition - 1, 1);
            cursorPosition--;
            if (cursorZoneStart > 0) cursorZoneStart--;
        }

        if (cursorPosition < cursorZoneStart) cursorZoneStart--;
        if (cursorPosition > cursorZoneStart + 22) cursorZoneStart++;

        inputField.text = inputText.Substring(cursorZoneStart, Mathf.Min(inputText.Length, 22));
        inputField.color = Color.white * (inputText.Length == 0 ? .56f : .83f);

        cursorField.text = "";
        for (int i = 0; i < cursorPosition - cursorZoneStart; i++)
            cursorField.text += ' ';
        cursorField.text += "<color=#888888>|</color>";
    }

    public void ToggleShift()
    {
        if (shiftStuck)
        {
            shiftHeld = false;
            shiftStuck = false;
            shiftImage.sprite = shiftSprites[0];
            CaseLetterKeys(false);
        }
        else if (shiftHeld)
        {
            shiftStuck = true;
            shiftImage.sprite = shiftSprites[2];
            CaseLetterKeys(true);
        }
        else
        {
            shiftHeld = true;
            shiftImage.sprite = shiftSprites[1];
            CaseLetterKeys(true);
        }
    }

    public void SendTextMessage()
    {
        if (inputText.Length == 0) return;
        personalMessages.SendNewMessage(inputText, true);
        //TextProcessorScript.CleanText(inputText);
        inputText = cursorField.text = "";
        cursorPosition = cursorZoneStart = 0;
        inputField.text = "Say something...";
        inputField.color = Color.white * .56f;

    }

    private void CaseLetterKeys(bool upper)
    {
        for(int i = 0; i < 26; i++)
            letterKeyText[i].text = (upper ? char.ToUpper(letterKeyText[i].text[0]) : char.ToLower(letterKeyText[i].text[0])) + "";
    }

    private void BlinkCursor()
    {
        if (inputText.Length == 0) return;

        if (cursorOn)
        {
            cursorField.text = cursorField.text.Replace("<color=#888888>|</color>", "<color=#4f4f4f>|</color>");
            cursorOn = false;
        }
        else
        {
            cursorField.text = cursorField.text.Replace("<color=#4f4f4f>|</color>", "<color=#888888>|</color>");
            cursorOn = true;
        }
    }
}
