using UnityEngine;

[CreateAssetMenu(fileName = "CD_NewConditional", menuName = "Dialogue/Conditional Node")]
public class ConditionalNode : Node
{
    [Header("Condition Settings")]
    public Condition condition;
    [Space()]
    public Node TrueNode;
    public Node FalseNode;
}
