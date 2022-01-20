using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue/Dialogue Node")]
public class DialogueNode : Node
{
    public Node NextNode;

    public bool AutoContinue;
    public float DialogueDuration;
    public UnityAction OnDurationEnd;

    public Node Next()
    {
        return NextNode;
    }
}
