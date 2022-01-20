using UnityEngine;

[CreateAssetMenu(fileName = "New Conditional Node", menuName = "Dialogue/Conditional Node")]
public class ConditionalNode : Node
{
    public Condition condition;

    public Node TrueNode;
    public Node FalseNode;
}
