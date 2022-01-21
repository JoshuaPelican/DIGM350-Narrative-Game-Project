using System.Collections;
using UnityEngine;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] Conversation Conversation;

    Popup popup;

    Node currentNode;
    Node queuedNode;

    bool speaking;

    private void Awake()
    {
        popup = GetComponentInChildren<Popup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the player enters the trigger

        if (other.CompareTag("Player"))
        {
            //Popup the conversation and play the correct line
            if (!Conversation.Started)
            {
                StartNode(Conversation.StartingNode);
                Conversation.Started = true;
            }
            else if (Conversation.Started && !Conversation.Finished && Conversation.CurrentNode)
            {
                StartNode(Conversation.ReturningNode);
                QueueNode(Conversation.CurrentNode);
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
    }

    private void OnTriggerExit(Collider other)
    {
        //If the player leaves the trigger
        if (other.CompareTag("Player"))
        {
            if (speaking)
            {
                StartNode(Conversation.CancelledNode);
                Conversation.CurrentNode = currentNode;
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
            else
            {
                Conversation.Finished = true;
                popup.RemovePopup();
            }
        }
        else if(node is ConditionalNode)
        {
            ConditionalNode cNode = (ConditionalNode)node;

            if (cNode.condition == true)
            {
                StartNode(cNode.TrueNode);
            }
            else if(cNode.condition == false)
            {
                StartNode(cNode.FalseNode);
            }
        }

        currentNode = null;
    }
}
