using UnityEngine;

public class Node : ScriptableObject
{
    public Character Character;

    [TextArea(3, 10)]
    public string Text;

    public bool HasVisited;
}
