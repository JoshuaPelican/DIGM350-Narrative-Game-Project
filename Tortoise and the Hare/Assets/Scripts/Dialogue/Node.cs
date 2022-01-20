using UnityEngine;

public class Node : ScriptableObject
{
    [Header("Speaker Settings")]
    public Character Speaker;

    [Header("Dialogue Text")]
    [TextArea(3, 10)]
    public string Text;

    [Header("Text Speed Settings")]
    [Range(0, 1)] public float DialogueSpeed = 0.95f;
    public float FinishDelay = 1.5f;

    public bool Visited;
}
