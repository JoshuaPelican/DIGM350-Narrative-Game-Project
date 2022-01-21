using UnityEngine;

[CreateAssetMenu(fileName = "D_NewDialogue", menuName = "Dialogue/Dialogue Node")]
public class DialogueNode : Node
{
    [Header("Dialogue Settings")]
    public Node NextNode;
}
