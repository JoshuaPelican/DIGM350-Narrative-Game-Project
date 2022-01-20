using UnityEngine;
using UnityEngine.Events;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] Conversation conversation;

    [SerializeField] DialoguePopup dialoguePopup;

    Node currentNode;
    Node savedNode;

    public void StartNode(Node node)
    {
        dialoguePopup.OnStartNode(node);
        currentNode = node;
    }

    public void EndNode()
    {
        currentNode.HasVisited = true;

        if (savedNode)
        {
            dialoguePopup.OnStartNode(savedNode);
            currentNode = savedNode;
            savedNode = null;
        }
        else
        {
        }
    }

    void EndConversation(Node node)
    {
        if (conversation.CancelledNode)
        {
            StartNode(conversation.CancelledNode);
        }
        else
        {
            dialoguePopup.OnEndConversation();
            currentNode = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(currentNode != null)
            {
                if(!currentNode.HasVisited)
                {
                    Node tempNode = currentNode;
                    StartNode(conversation.CancelledVisitNode);
                    savedNode = tempNode;
                }
                else
                {
                    StartNode(conversation.VisitedNode);
                }
            }
            else
            {
                StartNode(conversation.FirstNode);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (currentNode)
                EndConversation(currentNode);
        }
    }
}
