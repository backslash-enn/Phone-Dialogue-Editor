using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueExecutor : MonoBehaviour
{
    private PhoneDialogue phoneDialogue;
    public PersonalMessageScript messager;
    private PhoneDialogue.DialogueNode currentNode;
    private bool transitioning;
    private WaitForSeconds w;

    private void Start()
    {

        Initialize(TestPhoneDialogue.testPhoneDialogue);
    }

    public void Initialize(PhoneDialogue inputPhoneDialogue)
    {
        phoneDialogue = inputPhoneDialogue;
        currentNode = phoneDialogue.dialogueNodes[0];
        StartCoroutine(Transition(0, false));
        w = new WaitForSeconds(2);
    }

    private IEnumerator Transition(int intent, bool changeNode)
    {
        if (transitioning) yield break;
        transitioning = true;

        if (changeNode) currentNode = currentNode.intentNodeMappings[intent];

        if(currentNode.winState || currentNode.loseState)
        {
            enabled = false;
            yield break;
        }

        yield return new WaitForSeconds(currentNode.responseDelay);

        if (currentNode.responseList.Count > 0)
            messager.SendNewMessage(currentNode.responseList[Random.Range(0, currentNode.responseList.Count)], false);

        transitioning = false;

        if (currentNode.autoTransition)
            StartCoroutine(Transition(0, true));
    }

    public void ExecuteDialogue(List<string> wordList)
    {
        int intent;

        print(currentNode.nodeName);

        // Stop if at win or lose state
        if (currentNode.winState || currentNode.loseState) return;

        intent = FindIntent(wordList);
        if (intent != -1)
            StartCoroutine(Transition(intent, true));
        else
            StartCoroutine(ExpressUnknownIntent());
    }

    private int FindIntent(List<string> wordList)
    {
        //if (currentNode.keywordlistIntentMappings == null) return -1;

        int i;
        int chosenKeywordMatchCount = 0, chosenKeywordList = -1;
        print(currentNode.nodeName);
        print(currentNode.keywordlistIntentMappings);
        List<List<string>> kll = new List<List<string>>(currentNode.keywordlistIntentMappings.Keys);
        int[] stringMatchCount = new int[kll.Count];

        print("trying to figure out what youre saying!");
        print("INPUT:");
        foreach (string  s in wordList)
            print(">" + s + "<");

        for (i = 0; i < wordList.Count; i++)
        {
            for (int j = 0; j < kll.Count; j++)
            {
                if (stringMatchCount[j] < kll[j].Count && wordList[i].Equals(kll[j][stringMatchCount[j]]))
                {
                    stringMatchCount[j]++;
                    if (stringMatchCount[j] == kll[j].Count && stringMatchCount[j] > chosenKeywordMatchCount)
                    {
                        chosenKeywordList = j;
                        chosenKeywordMatchCount = stringMatchCount[j];
                    }
                }
            }
        }

        if (chosenKeywordList == -1) print("sorry man, I dunno");
        else
        {
            print("You were saying '" + currentNode.intentNameMappings[currentNode.keywordlistIntentMappings[kll[chosenKeywordList]]] + "'! I got that from the following");
            foreach (string s in kll[chosenKeywordList])
                print(">" + s + "<");
        }

        return (chosenKeywordList != -1 ? currentNode.keywordlistIntentMappings[kll[chosenKeywordList]] : -1);
    }


    private IEnumerator ExpressUnknownIntent()
    {
        yield return w;
        messager.SendNewMessage(phoneDialogue.unknownIntentResponses[Random.Range(0, phoneDialogue.unknownIntentResponses.Count)], false);
        StartCoroutine(Transition(0, false));
    }

} 
