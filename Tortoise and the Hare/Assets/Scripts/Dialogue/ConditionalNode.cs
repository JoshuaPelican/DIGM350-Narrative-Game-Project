using UnityEngine;

[CreateAssetMenu(fileName = "CD_NewConditional", menuName = "Dialogue/Conditional Node")]
public class ConditionalNode : Node
{
    public Condition condition;

    public Node TrueNode;
    public Node FalseNode;
}
