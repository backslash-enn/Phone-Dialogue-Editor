using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKeyboardScript : MonoBehaviour
{
    private TextEntryScript textEntryScript;
    private bool holdingShift;

    void Start()
    {
        textEntryScript = GetComponent<TextEntryScript>();
    }

    void Update()
    {
        // Shift
        holdingShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Chatacters
        if (Input.GetKeyDown("a"))
            textEntryScript.InsertChar(holdingShift ? 'A' : 'a');
        else if (Input.GetKeyDown("b"))
            textEntryScript.InsertChar(holdingShift ? 'B' : 'b');
        else if (Input.GetKeyDown("c"))
            textEntryScript.InsertChar(holdingShift ? 'C' : 'c');
        else if (Input.GetKeyDown("d"))
            textEntryScript.InsertChar(holdingShift ? 'D' : 'd');
        else if (Input.GetKeyDown("e"))
            textEntryScript.InsertChar(holdingShift ? 'E' : 'e');
        else if (Input.GetKeyDown("f"))
            textEntryScript.InsertChar(holdingShift ? 'F' : 'f');
        else if (Input.GetKeyDown("g"))
            textEntryScript.InsertChar(holdingShift ? 'G' : 'g');
        else if (Input.GetKeyDown("h"))
            textEntryScript.InsertChar(holdingShift ? 'H' : 'h');
        else if (Input.GetKeyDown("i"))
            textEntryScript.InsertChar(holdingShift ? 'I' : 'i');
        else if (Input.GetKeyDown("j"))
            textEntryScript.InsertChar(holdingShift ? 'J' : 'j');
        else if (Input.GetKeyDown("k"))
            textEntryScript.InsertChar(holdingShift ? 'K' : 'k');
        else if (Input.GetKeyDown("l"))
            textEntryScript.InsertChar(holdingShift ? 'L' : 'l');
        else if (Input.GetKeyDown("m"))
            textEntryScript.InsertChar(holdingShift ? 'M' : 'm');
        else if (Input.GetKeyDown("n"))
            textEntryScript.InsertChar(holdingShift ? 'N' : 'n');
        else if (Input.GetKeyDown("o"))
            textEntryScript.InsertChar(holdingShift ? 'O' : 'o');
        else if (Input.GetKeyDown("p"))
            textEntryScript.InsertChar(holdingShift ? 'P' : 'p');
        else if (Input.GetKeyDown("q"))
            textEntryScript.InsertChar(holdingShift ? 'Q' : 'q');
        else if (Input.GetKeyDown("r"))
            textEntryScript.InsertChar(holdingShift ? 'R' : 'r');
        else if (Input.GetKeyDown("s"))
            textEntryScript.InsertChar(holdingShift ? 'S' : 's');
        else if (Input.GetKeyDown("t"))
            textEntryScript.InsertChar(holdingShift ? 'T' : 't');
        else if (Input.GetKeyDown("u"))
            textEntryScript.InsertChar(holdingShift ? 'U' : 'u');
        else if (Input.GetKeyDown("v"))
            textEntryScript.InsertChar(holdingShift ? 'V' : 'v');
        else if (Input.GetKeyDown("w"))
            textEntryScript.InsertChar(holdingShift ? 'W' : 'w');
        else if (Input.GetKeyDown("x"))
            textEntryScript.InsertChar(holdingShift ? 'X' : 'x');
        else if (Input.GetKeyDown("y"))
            textEntryScript.InsertChar(holdingShift ? 'Y' : 'y');
        else if (Input.GetKeyDown("z"))
            textEntryScript.InsertChar(holdingShift ? 'Z' : 'z');

        else if (Input.GetKeyDown("1"))
            textEntryScript.InsertChar(holdingShift ? '!' : '1');
        else if (Input.GetKeyDown("2"))
            textEntryScript.InsertChar('2');
        else if (Input.GetKeyDown("3"))
            textEntryScript.InsertChar('3');
        else if (Input.GetKeyDown("4"))
            textEntryScript.InsertChar('4');
        else if (Input.GetKeyDown("5"))
            textEntryScript.InsertChar('5');
        else if (Input.GetKeyDown("6"))
            textEntryScript.InsertChar('6');
        else if (Input.GetKeyDown("7"))
            textEntryScript.InsertChar('7');
        else if (Input.GetKeyDown("8"))
            textEntryScript.InsertChar('8');
        else if (Input.GetKeyDown("9"))
            textEntryScript.InsertChar('9');
        else if (Input.GetKeyDown("0"))
            textEntryScript.InsertChar('0');

        else if (Input.GetKeyDown(KeyCode.Space))
            textEntryScript.InsertChar(' ');

        else if (Input.GetKeyDown("."))
            textEntryScript.InsertChar('.');
        else if (Input.GetKeyDown(","))
            textEntryScript.InsertChar(',');
        else if (Input.GetKeyDown("'"))
            textEntryScript.InsertChar('\'');
        else if (Input.GetKeyDown("/"))
            textEntryScript.InsertChar('?');

        // Special Input
        else if(Input.GetKeyDown(KeyCode.Return))
            textEntryScript.SendTextMessage();
        else if (Input.GetKeyDown(KeyCode.Backspace))
            textEntryScript.DeleteChar();
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            textEntryScript.MoveCursor(false);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            textEntryScript.MoveCursor(true);
    }
}

