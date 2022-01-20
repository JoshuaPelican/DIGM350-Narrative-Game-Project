using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue/Dialogue Node")]
public class DialogueNode : Node
{
    [Header("Dialogue Settings")]
    public Node NextNode;
}
