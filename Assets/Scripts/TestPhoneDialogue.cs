using System.Collections.Generic;
using UnityEngine;

public class TestPhoneDialogue : MonoBehaviour
{
    public static PhoneDialogue testPhoneDialogue;

    void Awake()
    {
        // Here we have a small example of a phone dialogue that's strictly
        // hardcoded. This is terrible and hopefully this serves as an example
        // as to why a visual graph editor is so important. But in a pinch 
        // something like this can be incredibly useful

        // Unknown intent responses
        List<string> uirs = new List<string>
        {
          "What?",
          "Man I really don't even get what you're saying...",
          "Wait, wha?",
          "What are you even saying?",
          "just answer the question",
          "can we get back to the question at hand?",
          "ok...let me say it again"
        };

        // Response list for node 0
        List<string> node0res = new List<string>
        {
          "Is Die Hard a christmas movie?",
          "Be honest...is Die hard a christmas movie?",
          "Is Die Hard a christmas movie or not?"
        };

        // Response list for node 1
        List<string> node1res = new List<string>
        {
          "Thank you!! That's what i've been saying!",
          "Finally, someone with some sense",
          "EXACTLY, THAT'S WHAT I'VE BEEN TRYING TO TELL THEM",
          "THANK. YOU.",
          "IKR!?"
        };

        // Response list for node 2
        List<string> node2res = new List<string>  
        {
          "Man you seriously don't think so? Jeez, you think you know a person...",
          "Common man...really? Honestly?",
          "Wow. I'm dissapointed in you",
          "yeah just delete my number",
          "You know what, I don't need your approval"
        };

        // Response list for node 3
        List<string> node3res = new List<string>
        {
          "Nope, no get out of jail free card. Give a real answer",
          "That's a yes or no question dude. Stop being so wishy washy",
          "Bro I don't want to here that. GIVE A REAL ANSWER",
          "Stop trying to play both sides. Answer the question",
          "Stop being uncertain. PICK A SIDE"
        };

        // Response list for node 4
        List<string> node4res = new List<string>
        {
          "Oh really? Nevermind then I guess",
          "Are you serious? Do you live under a rock?",
          "Wow, I can't believe you've never seen Die Hard. Just forget it then",
          "Seriously?? Die Hard is an all-time classic. You know what, just delete my number",
          "If you haven't seen Die Hard, you NEED to watch it. Come back to me afterwards",
          "Well if you haven't seen it...GO SEE IT",
          "Fine, you're safe for now, but you better go watch the movie"
        };

        // Response list for node 5
        List<string> node5res = new List<string>
        {
          "I don't care if you don't care, answer the question",
          "Well give an answer anyways",
          "Common man, how can you not care about Die Hard!? Answer!!",
          "Well I do, so ANSWER",
          "Then act like you care. I'll ask again"
        };

        testPhoneDialogue = new PhoneDialogue("Friend", uirs);

        // Create node 0
        testPhoneDialogue.dialogueNodes.Add(new PhoneDialogue.DialogueNode(
            0,
            false,
            "Asking Question",
            2f,
            node0res
         ));

        // Create node 1
        testPhoneDialogue.dialogueNodes.Add(new PhoneDialogue.DialogueNode(
            1,
            true,
            "Answer Question Yes",
            .2f,
            node1res
         ));

        // Create node 2
        testPhoneDialogue.dialogueNodes.Add(new PhoneDialogue.DialogueNode(
            2,
            true,
            "Answer Question No",
            3f,
            node2res
         ));

        // Create node 3
        testPhoneDialogue.dialogueNodes.Add(new PhoneDialogue.DialogueNode(
            3,
            true,
            "Answer Question Maybe",
            3f,
            node3res
         ));

        // Create node 4
        testPhoneDialogue.dialogueNodes.Add(new PhoneDialogue.DialogueNode(
            4,
            true,
            "Hasnt Seen Movie",
            3.5f,
            node4res
         ));

        // Create node 5
        testPhoneDialogue.dialogueNodes.Add(new PhoneDialogue.DialogueNode(
            5,
            true,
            "Does Not Care",
            1.8f,
            node5res
         ));

        // Create win/lose nodes
        testPhoneDialogue.dialogueNodes.Add(new PhoneDialogue.DialogueNode(6, true));
        testPhoneDialogue.dialogueNodes.Add(new PhoneDialogue.DialogueNode(7, false));

        // Set Positions
        testPhoneDialogue.dialogueNodes[0].SetPosition(0, 0);
        testPhoneDialogue.dialogueNodes[1].SetPosition(-550, -400);
        testPhoneDialogue.dialogueNodes[2].SetPosition(550, -400);
        testPhoneDialogue.dialogueNodes[3].SetPosition(550, 98);
        testPhoneDialogue.dialogueNodes[4].SetPosition(0, -400);
        testPhoneDialogue.dialogueNodes[5].SetPosition(-550, 98);
        testPhoneDialogue.dialogueNodes[6].SetPosition(-350, -800);
        testPhoneDialogue.dialogueNodes[7].SetPosition(350, -800);

        // Intents and transitions
        testPhoneDialogue.dialogueNodes[0].AddIntent("apathetic", testPhoneDialogue.dialogueNodes[5]);
        testPhoneDialogue.dialogueNodes[0].AddIntent("yes", testPhoneDialogue.dialogueNodes[1]);
        testPhoneDialogue.dialogueNodes[0].AddIntent("ignorant", testPhoneDialogue.dialogueNodes[4]);
        testPhoneDialogue.dialogueNodes[0].AddIntent("no", testPhoneDialogue.dialogueNodes[2]);
        testPhoneDialogue.dialogueNodes[0].AddIntent("maybe", testPhoneDialogue.dialogueNodes[3]);

        testPhoneDialogue.dialogueNodes[1].AddIntent("", testPhoneDialogue.dialogueNodes[6]);
        testPhoneDialogue.dialogueNodes[2].AddIntent("", testPhoneDialogue.dialogueNodes[7]);
        testPhoneDialogue.dialogueNodes[3].AddIntent("", testPhoneDialogue.dialogueNodes[0]);
        testPhoneDialogue.dialogueNodes[4].AddIntent("", testPhoneDialogue.dialogueNodes[6]);
        testPhoneDialogue.dialogueNodes[5].AddIntent("", testPhoneDialogue.dialogueNodes[0]);

        // Keylist Intent Mappings
        // Intent 0
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "not", "care" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "not", "cares" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "who", "care" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "who", "cares" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "not", "concerned" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "no", "concern" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "irrelevant" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "not", "matter" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "better", "things", "about" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "better", "stuff", "about" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "bigger", "things", "about" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "bigger", "stuff", "about" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "important", "things", "about" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(0, new List<string> { "important", "stuff", "about" });

        // Intent 1
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "yes" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "yeah" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "it", "is" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "say", "so" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "much" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "course" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "absolutely" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "definetely" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "guess" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "why", "not" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "not", "see", "why", "not" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "sure" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "know", "is" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "fine" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "why", "would", "not" });

        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "yeah" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "yuh" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "yah" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "yaah" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "ya" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "ye" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(1, new List<string> { "yee" });

        // Intent 2
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(2, new List<string> { "never", "see" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(2, new List<string> { "never", "saw" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(2, new List<string> { "never", "watch" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(2, new List<string> { "never", "watched" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(2, new List<string> { "not", "see" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(2, new List<string> { "not", "saw" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(2, new List<string> { "not", "watch" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(2, new List<string> { "not", "know", "is" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(2, new List<string> { "never", "heard", "it" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(2, new List<string> { "never", "heard", "movie" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(2, new List<string> { "what", "movie", "about" });

        // Intent 3
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "no" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "it", "is", "no" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "not" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "it", "is", "not" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "much" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "nope" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "isnt" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "absolutely", "not" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "definetely", "not" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "ridiculous" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "guess", "not" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "why", "would", "be" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "why", "would", "call" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(3, new List<string> { "why", "would", "consider" });

        // Intent 4
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(4, new List<string> { "maybe" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(4, new List<string> { "possibly" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(4, new List<string> { "depends" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(4, new List<string> { "define", "christmas" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(4, new List<string> { "define", "movie" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(4, new List<string> { "perhaps" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(4, new List<string> { "do", "not", "know" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(4, new List<string> { "yes", "and", "no" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(4, new List<string> { "no", "and", "yes" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(4, new List<string> { "is", "and", "is", "not" });
        testPhoneDialogue.dialogueNodes[0].MapKeywordListToIntent(4, new List<string> { "is", "not", "and", "is" });

        // Joints
        testPhoneDialogue.dialogueNodes[0].AddJoint(0, new PhoneDialogue.Joint(-385, 0));
        testPhoneDialogue.dialogueNodes[0].AddJoint(4, new PhoneDialogue.Joint(385, 0));
    }
}

// send while down