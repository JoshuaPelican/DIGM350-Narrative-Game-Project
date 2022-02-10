using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "D_NewDialogue", menuName = "Dialogue/Dialogue Node")]
public class DialogueNode : Node
{
    [PropertyOrder(2)]
    [TitleGroup("Next Node")]
    [SerializeField] Node nextNode;
    public override Node NextNode()
    {
        return nextNode;
    }
}
