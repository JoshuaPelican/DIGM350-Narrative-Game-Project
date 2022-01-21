using System.Collections;
using UnityEngine;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] Conversation Conversation;

    Popup popup;

    Node currentNode;
    Node queuedNode;

    bool speaking;

    private void Start()
    {
        popup = GetComponentInChildren<Popup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the player enters the trigger
        if (other.CompareTag("Player"))
        {        
            //Popup the conversation and play the correct line
            CheckConversationNode();
        }
    }

    //TODO: Reformat this part of the code, make it concise using one 1 current node slot if possible.

    void CheckConversationNode()
    {
        //If the conversation has never started yet, play the starting node
        if (!Conversation.Started)
        {
            StartNode(Conversation.StartingNode);
            Conversation.Started = true;
        }
        //If the conversation has started but not finished, then return and play the last not visited node
        else if (Conversation.Started && !Conversation.Finished)
        {
            if (Conversation.ReturningNode)
            {
                StartNode(Conversation.ReturningNode);
                QueueNode(Conversation.CurrentNode);
            }
            else
            {
                StartNode(Conversation.CurrentNode);
            }
        }
        else if (Conversation.Finished)
        {
            StartNode(Conversation.FinishedNode);
        }
        else
        {
            Debug.LogError("Unknown Dialogue Condition Found! Get Josh To Fix This!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If the player leaves the trigger
        if (other.CompareTag("Player"))
        {
            Conversation.CurrentNode = currentNode;

            if (speaking)
            {
                StartNode(Conversation.CancelledNode);
            }
            else if(!speaking)
            {
                popup.RemovePopup();
            }
        }
    }

    void StartNode(Node node)
    {
        currentNode = node;
        StopAllCoroutines();

        if (node)
        {
            popup.DisplayPopup(node);

            StartCoroutine(TypeDialogue(node));
        }
        else
        {
            popup.RemovePopup();
        }
    }

    void QueueNode(Node node)
    {
        queuedNode = node;
    }

    IEnumerator TypeDialogue(Node node)
    {
        speaking = true;
        popup.DialogueTextMesh.SetText(string.Empty);

        foreach (char c in node.Text.ToCharArray())
        {
            popup.DialogueTextMesh.text += c;
            yield return new WaitForSeconds(1 - node.DialogueSpeed);
        }
        speaking = false;

        StartCoroutine(DialogueDelay(node));
    }

    IEnumerator DialogueDelay(Node node)
    {
        node.Visited = true;

        yield return new WaitForSeconds(node.FinishDelay);

        EndNode(node);
    }

    void EndNode(Node node)
    {
        if (queuedNode)
        {
            StartNode(queuedNode);
        }
        else if (node is DialogueNode)
        {
            DialogueNode dNode = (DialogueNode)node;

            if (dNode.NextNode)
            {
                StartNode(dNode.NextNode);
            }
            else if (node != Conversation.CancelledNode)
            {
                Conversation.Finished = true;
                popup.RemovePopup();
            }
            else
            {
                popup.RemovePopup();
            }
        }
        else if (node is ConditionalNode)
        {
            ConditionalNode cNode = (ConditionalNode)node;

            if (cNode.condition.Value == true)
            {
                StartNode(cNode.TrueNode);
            }
            else if (cNode.condition.Value == false)
            {
                StartNode(cNode.FalseNode);
            }
        }
    }
}
