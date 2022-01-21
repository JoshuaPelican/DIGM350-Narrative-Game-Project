using UnityEngine;

public class Node : ScriptableObject
{
    [Header("Speaker Settings")]
    public Character Speaker;

    [Header("Text Speed Settings")]
    [Range(0, 1)] public float DialogueSpeed = 0.95f;
    public float FinishDelay = 3f;
    [Space(10)]
    public bool Visited;

    [Header("Dialogue Settings")]
    [TextArea(3, 10)]
    public string Text;
}
