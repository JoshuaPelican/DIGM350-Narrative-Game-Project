using System.Collections;
using UnityEngine;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] Conversation Conversation;

    Popup playerPopup;
    Popup otherPopup;

    bool listening;
    bool speaking;
    Node currentNode;

    private void Awake()
    {
        otherPopup = GetComponentInChildren<Popup>();
        playerPopup = GameObject.FindWithTag("Player Popup").GetComponent<Popup>();
    }

    private void Update()
    {
        PlayerInput();
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

    void PlayerInput()
    {
        //If Listening and the mouse is pressed
        if (Input.GetMouseButtonDown(0) && listening)
        {
            //If the dialogue is in progress, finish it
            if (speaking)
                FinishTypeDialogue(currentNode);
            else if (!currentNode)
                EnterConversation();
            //Otherwise move on to the next node
            else
                EndNode();
        }
    }

    void EnterConversation()
    {
        //If there is no conversation setup
        if (!Conversation)
        {
            Debug.LogWarning($"{transform.parent.name} has no Conversation setup in the Conversation Starter!");
            return;
        }

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
        if (!Conversation)
            return;

        listening = false;

        //If the dialogue is in progress, and the conversation has not finished, and there is cancel dialogue, then start it.
        if (!Conversation.Finished && Conversation.CancelledNode)
        {
            StartNode(Conversation.CancelledNode);
        }
        else
        {
            EndConversation();
        }
    }

    void StartNode(Node node)
    {
        StopAllCoroutines();

        //If node exists.
        if (node)
        {
            currentNode = node;
            CharacterPopup(node).DisplayPopup(node);
            StartCoroutine(TypeDialogue(node));
        }
        //Node is null, remove the popup.
        else
        {
            EndConversation();
        }
    }

    //Types out dialogue text based on speed
    IEnumerator TypeDialogue(Node node)
    {
        speaking = true;

        CharacterPopup(node).DialogueTextMesh.SetText(string.Empty);

        foreach (char c in node.Dialogue.ToCharArray())
        {
            CharacterPopup(node).DialogueTextMesh.text += c;
            yield return new WaitForSeconds(1 - node.DialogueSpeed);
        }

        speaking = false;
    }

    //Completely type out dialogue in one frame
    void FinishTypeDialogue(Node node)
    {
        speaking = false;
        StopAllCoroutines();
        CharacterPopup(node).DialogueTextMesh.SetText(node.Dialogue);
    }

    //When a node is complete, if player still listening, move to next node
    //Otherwise player is gone, end conversation
    void EndNode()
    {
        currentNode.Visit();
        CharacterPopup(currentNode).RemovePopup();
        currentNode = null;

        if (listening)
            StartNode(Conversation.NextNode());
    }

    //Stops all dialogue and removes popup
    void EndConversation()
    {
        StopAllCoroutines();
        otherPopup.RemovePopup();
        playerPopup.RemovePopup();
    }

    Popup CharacterPopup(Node node)
    {
        if(node.Speaker.name == "Hare")
            return playerPopup;
        return otherPopup;
    }
}
