using UnityEngine;

[CreateAssetMenu(fileName = "C_NewConvo", menuName = "Dialogue/Conversation")]
public class Conversation : ScriptableObject
{
    [Header("Conversation Speaker")]
    public Character Speaker;

    [Header("Conversation Nodes")]
    public Node StartingNode;
    public Node CancelledNode;
    public Node ReturningNode;
    public Node FinishedNode;
    [Space(10)]
    public bool Started;
    public bool Finished;
    [Space()]
    public Node CurrentNode;
}
