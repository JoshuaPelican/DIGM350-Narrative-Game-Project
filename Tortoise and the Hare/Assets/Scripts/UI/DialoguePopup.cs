using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialoguePopup : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float dialogueSpeed = 0.75f;

    [SerializeField] GameObject dialogueContainer;
    [SerializeField] GameObject choicesContainer;

    [SerializeField] TextMeshProUGUI characterTextMesh;
    [SerializeField] TextMeshProUGUI dialogueTextMesh;

    [SerializeField] ConversationStarter conversationStarter;

    [SerializeField] GameObject choiceButtonPrefab;

    Node currentNode;

    bool typing;

    private void OnEnable()
    {
        //PlayerInput.OnInteractInput +=
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        dialogueContainer.SetActive(false);
    }

    private void FixedUpdate()
    {
       //Turn towards the player always 
    }

    public void OnStartNode(Node node)
    {
        node = currentNode;

        dialogueContainer.SetActive(true);

        characterTextMesh.text = node.Character.name;
        StopAllCoroutines();
        StartCoroutine(TypeDialogue(node.Text));

        if(node is ChoiceNode)
        {
            ChoiceNode choiceNode = node as ChoiceNode;

            for (int i = 0; i < choiceNode.Choices.Count; i++)
            {
                //Setup Choices
                Instantiate(choiceButtonPrefab, choicesContainer.transform);
            }
        }
        else if(node is DialogueNode)
        {
            //Setup Continue Option
        }
    }

    public void OnEndConversation()
    {
        dialogueContainer.SetActive(false);
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        typing = true;

        dialogueTextMesh.text = "";
        foreach (char c in dialogue.ToCharArray())
        {
            dialogueTextMesh.text += c;
            yield return new WaitForSeconds(1 - dialogueSpeed);
        }

        typing = false;
    }

    void OnInteract()
    {
        if (typing)
        {
            float speed = dialogueSpeed;
            dialogueSpeed = 0;
            dialogueSpeed = speed;
        }
        else
        {
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        conversationStarter.StartNode(choicesContainer.transform.GetChild(choiceIndex).GetComponent<ChoiceButton>().choice.NextNode);
    }
}
