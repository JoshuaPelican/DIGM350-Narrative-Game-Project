using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/Conversation")]
public class Conversation : ScriptableObject
{
    public Node FirstNode;

    public Node VisitedNode;

    public Node CancelledNode;
    public Node CancelledVisitNode;
}
