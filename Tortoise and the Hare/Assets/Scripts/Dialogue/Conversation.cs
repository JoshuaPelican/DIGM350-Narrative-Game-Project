using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class Conversation : ScriptableObject
{
    public Character Speaker;

    public Node StartingNode;
    public Node CancelledNode;
    public Node ReturningNode;
    public Node FinishedNode;

    public bool Started;
    public bool Finished;

    [HideInInspector]
    public Node CurrentNode;
}
