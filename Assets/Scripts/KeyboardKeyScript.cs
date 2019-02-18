using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardKeyScript : MonoBehaviour {

    private PhoneUIButtonScript button;
    private Image i;
    private Color startColor, pressedColor;
    public char key;
    private KeyType keyType;
    public TextEntryScript textEntryScript;
    public AudioSource aud;
    private WaitForSeconds repeatedPressDelay, repeatedPressTime;
    private bool repeating;

	void Start () {
        button = GetComponent<PhoneUIButtonScript>();
        i = GetComponent<Image>();
        startColor = i.color;
        pressedColor = i.color - new Color(.2f, .2f, .2f);
        repeatedPressDelay = new WaitForSeconds(.5f);
        repeatedPressTime = new WaitForSeconds(.1f);

        if (key == '^')
            keyType = KeyType.Shift;
        else if (key == '-')
            keyType = KeyType.Backspace;
        else if (key == '<' || key == '>')
            keyType = KeyType.Arrow;
        else if (key == '=')
            keyType = KeyType.Send;
        else
            keyType = KeyType.Character;

    }

    void Update () {

        i.color = Color.Lerp(i.color, (button.pressed ? pressedColor : startColor), 6 * Time.deltaTime);
        if (keyType != KeyType.Arrow && keyType != KeyType.Backspace)
        {
            if (button.justReleased && (textEntryScript.open || key == '='))
            {
                if (keyType == KeyType.Character)
                    textEntryScript.InsertChar(key);
                else if (keyType == KeyType.Shift)
                    textEntryScript.ToggleShift();
                else
                    textEntryScript.SendTextMessage();
                aud.Play();
            }
        }
        else
        {
            if (button.pressed && !repeating && textEntryScript.open)
                StartCoroutine(PressAndDelayedRepeat());
            if (!button.pressed)
            {
                StopAllCoroutines();
                repeating = false;
            }
        }
		
	}

    private IEnumerator PressAndDelayedRepeat()
    {
        repeating = true;

        if (keyType == KeyType.Arrow)
            textEntryScript.MoveCursor(key == '>');
        else
            textEntryScript.DeleteChar();

        yield return repeatedPressDelay;
        while (button.pressed)
        {
            yield return repeatedPressTime;
            if (button.pressed)
            {
                if (keyType == KeyType.Arrow)
                    textEntryScript.MoveCursor(key == '>');
                else
                    textEntryScript.DeleteChar();
            }
        }

        repeating = false;
    }

    public enum KeyType
    {
        Character,
        Shift,
        Backspace,
        Send,
        Arrow,
    }
}
