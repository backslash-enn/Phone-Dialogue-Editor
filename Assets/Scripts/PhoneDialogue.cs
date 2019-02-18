using System;
using System.Collections.Generic;

[Serializable]
public class PhoneDialogue
{
    public string agentName;
    public List<DialogueNode> dialogueNodes;
    public List<string> unknownIntentResponses;

    public PhoneDialogue(string inputAgentName, List<string> inputUnknownIntentResponses)
    {
        agentName = inputAgentName;
        dialogueNodes = new List<DialogueNode>();
        unknownIntentResponses = inputUnknownIntentResponses;
    }

    public class DialogueNode
    {
        public int id;
        public string nodeName;
        public float positionX, positionY;
        public float responseDelay;
        public List<string> responseList;

        public List<string> intentNameMappings; // Implicit mapping via indices
        public Dictionary<List<string>, int> keywordlistIntentMappings; // Explict mapping via dictionary
        public List<DialogueNode> intentNodeMappings; // Implicit mapping via indices
        public List<List<Joint>> intentJoints; // Implicit mapping via indices

        public bool autoTransition, winState, loseState;

        public DialogueNode(int inputId, bool inputAutoTransition, string inputNodeName, float inputResponseDelay, List<string> inputResponseList)
        {
            id = inputId;
            nodeName = inputNodeName;
            responseDelay = inputResponseDelay;
            responseList = inputResponseList;

            autoTransition = inputAutoTransition;

            intentNameMappings = new List<string>();
            intentNodeMappings = new List<DialogueNode>();
            intentJoints = new List<List<Joint>>();

            if (autoTransition == false)
                keywordlistIntentMappings = new Dictionary<List<string>, int>();
        }

        public DialogueNode(int inputId, bool isWinState)
        {
            // Special Constructor for win/lose nodes
            id = inputId;

            winState = isWinState;
            loseState = !isWinState;
        }

        public void AddIntent(string intentName, DialogueNode intentTargetNode)
        {
            // The Order in Which You Call MakeIntent Will Determine the Index of the Intent
            intentNameMappings.Add(intentName);
            intentNodeMappings.Add(intentTargetNode);
            intentJoints.Add(new List<Joint>());
        }

        public void MapKeywordListToIntent(int intentIndex, List<string> keywordList)
        {
            keywordlistIntentMappings.Add(keywordList, intentIndex);
        }

        public void SetPosition(float posX, float posY)
        {
            positionX = posX;
            positionY = posY;
        }

        public void AddJoint(int intentIndex, Joint newJoint)
        {
            intentJoints[intentIndex].Add(newJoint);
        }
    }

    public class Joint
    {
        public float xPos, yPos;

        public Joint(float inputXPos, float inputYPos)
        {
            xPos = inputXPos;
            yPos = inputYPos;
        }
    }
}