using System.Collections;
using UnityEngine;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] Conversation Conversation;

    Popup popup;

    bool listening;
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
            EnterConversation();
        }
    }

    //TODO: Reformat this part of the code, make it concise using one 1 current node slot if possible.

    private void OnTriggerExit(Collider other)
    {
        //If the player leaves the trigger
        if (other.CompareTag("Player"))
        {
            ExitConversation();
        }
    }

    void EnterConversation()
    {
        listening = true;

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
            }
            else
            {
                StartNode(Conversation.NextNode());
            }
        }
        //If the conversation is complete, start the looping finished node
        else if (Conversation.Finished)
        {
            StartNode(Conversation.FinishedNode);
        }
        else
        {
            Debug.LogError("Unknown Dialogue Condition Found! Get Josh To Fix This!");
        }
    }

    void ExitConversation()
    {
        listening = false;

        //If the dialogue is in progress, and the conversation has not finished, and there is cancel dialogue, then start it.
        if (!Conversation.Finished && Conversation.CancelledNode)
        {
            StartNode(Conversation.CancelledNode);
        }
    }

    void StartNode(Node node)
    {
        StopAllCoroutines();

        //If node exists.
        if (node)
        {
            popup.DisplayPopup(node);
            StartCoroutine(TypeDialogue(node));
        }
        //Node is null, remove the popup.
        else
        {
            EndConversation();
        }
    }

    IEnumerator TypeDialogue(Node node)
    {
        speaking = true;
        popup.DialogueTextMesh.SetText(string.Empty);

        foreach (char c in node.Dialogue.ToCharArray())
        {
            popup.DialogueTextMesh.text += c;
            yield return new WaitForSeconds(1 - node.DialogueSpeed);
        }
        speaking = false;

        StartCoroutine(DialogueDelay(node));
    }

    IEnumerator DialogueDelay(Node node)
    {
        node.Visit();
        yield return new WaitForSeconds(node.FinishDelay);
        EndNode();
    }

    void EndNode()
    {
        if (listening)
            StartNode(Conversation.NextNode());
        else
            EndConversation();
    }

    void EndConversation()
    {
        StopAllCoroutines();
        popup.RemovePopup();
    }
}
