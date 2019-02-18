using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextProcessorScript : MonoBehaviour {

    public TextAsset expansionListFile, wordPoolsFile;
    public Dictionary<string, string[]> expansionList;
    public Dictionary<string, string> wordPools;

    private char[] rowDelimiters = { '\r', '\n' };
    private char[] columnDelimiters = { ',' };
    private string[] numberClassA = {"one", "two", "three", "four", "five", "six", "seven", "eight",
                                     "nine" };
    private string[] numberClassB = {"ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen",
                                     "sixteen", "seventeen", "eighteen", "nineteen" };
    private string[] numberClassC = {"twenty", "thirty", "forty", "fifty", "sixty", "seventy",
                                     "eighty", "ninety" };
    private string[] numberClassD = { "hundred", "thousand", "million", "billion", "trillion" };

    // Number conversion graph (slightly simplified)

    //   ------------------------------------
    //   |                                  |
    //   |                                  |
    // __v_______________________________   |
    // |       +---+                    |   |
    // |       | A |------>+---------+  |   |
    // |       +-+-+       |         |  |   |
    // | +---+   ^         |         |  |   |
    // | | B |---|-------->|    D    |--|---|
    // | +---+   |         |         |  |
    // |       +-+-+       |         |  |
    // |       | C |------>+---------+  |
    // |       +---+                    |
    // |________________________________|

    // A, B, C, D, AD, BD, CD, CA, CAD

    void Start () {
        GenerateExpansionList();
        GenerateWordPools();
	}
	
	void Update () {
		
	}

    private void GenerateExpansionList()
    {
        int i, j;
        string[] splitLines;
        string[] splitEntries;
        string head;

        expansionList = new Dictionary<string, string[]>();
        splitLines = expansionListFile.text.Split(rowDelimiters, System.StringSplitOptions.RemoveEmptyEntries);

        for (i = 0; i < splitLines.Length; i++)
        {
            splitEntries = splitLines[i].Split(columnDelimiters, System.StringSplitOptions.RemoveEmptyEntries);
            head = splitEntries[0];
            for (j = 1; j < splitEntries.Length; j++)
                expansionList.Add(splitEntries[j], head.Split(' '));
        }

        //foreach (string k in expansionList.Keys)
        //    print(k + " -> " + expansionList[k][0] + ", " + expansionList[k][1]);
    }

    private void GenerateWordPools()
    {
        int i, j;
        string[] splitLines;
        string[] splitEntries;
        string head;

        wordPools = new Dictionary<string, string>();
        splitLines = wordPoolsFile.text.Split(rowDelimiters, System.StringSplitOptions.RemoveEmptyEntries);

        for (i = 0; i < splitLines.Length; i++)
        {
            splitEntries = splitLines[i].Split(columnDelimiters, System.StringSplitOptions.RemoveEmptyEntries);
            head = splitEntries[0];
            for (j = 1; j < splitEntries.Length; j++)
                wordPools.Add(splitEntries[j], head);
        }

        //foreach (string k in wordPools.Keys)
        //    print(k + " -> " + wordPools[k]);
    }

    public List<string> CleanText(string rawText, PhoneDialogue pd)
    {
        List<string> splitWords;
        char[] delimiters = { '.' , ' '};

        // Clean Text
        rawText = rawText.Replace(",", "");
        rawText = rawText.Replace("!", "");
        rawText = rawText.Replace("?", " ? ");
        rawText = rawText.ToLower();

        // Delimit Text
        splitWords = new List<string>(rawText.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries));

        // Funnel Text
        for(int i = 0; i < splitWords.Count; i++)
        {
            // Resolve number form


            // Resolve contractions
            foreach (string k in expansionList.Keys)
            {
                if (splitWords[i].Equals(k))
                {
                    print(k + " was found and will now be substituted");
                    splitWords.RemoveAt(i);
                    for(int j = 0; j < expansionList[k].Length; j++)
                        splitWords.Insert(i + j, expansionList[k][j]);
                }
            }

            // Apostrophes must be removed after we check for contractions
            rawText = rawText.Replace("'", "");

            // Pool words
            foreach (string k in wordPools.Keys)
            {
                if (splitWords[i].Equals(k))
                    splitWords[i] = wordPools[k];
            }
        }

        return splitWords;
    }
}
