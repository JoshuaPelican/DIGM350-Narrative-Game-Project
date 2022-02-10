using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CD_NewConditional", menuName = "Dialogue/Conditional Node")]
public class ConditionalNode : Node
{
    [PropertyOrder(2)]
    [TitleGroup("Next Node")]
    [ValueDropdown("GetAllConditions", FlattenTreeView = true, DropdownTitle = "Select A Condition")]
    public Condition condition;
    [Space()]
    [PropertyOrder(3)]
    public Node TrueNode;
    [PropertyOrder(3)]
    public Node FalseNode;

    public override Node NextNode()
    {
        return condition.Value ? TrueNode : FalseNode;
    }
}
