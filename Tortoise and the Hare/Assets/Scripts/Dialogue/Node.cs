using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent OnNodeVisited;

    [Space()]
    public Condition[] ConditionsToSet;

    [MenuItem("Dialogue/Clear Dialogue Progression")]
    public static void ClearDialogueProgression()
    {
        string[] nodeStrings = AssetDatabase.FindAssets("t:node", new[] { "Assets/Dialogue" });
        foreach (string asset in nodeStrings)
        {
            string path = AssetDatabase.GUIDToAssetPath(asset);
            AssetDatabase.LoadAssetAtPath<Node>(path).Visited = false;
        }

        Debug.Log($"{nodeStrings.Length} Nodes had their Progression cleared!");

        string[] convoStrings = AssetDatabase.FindAssets("t:Conversation", new[] { "Assets/Dialogue" });
        foreach (string asset in convoStrings)
        {
            string path = AssetDatabase.GUIDToAssetPath(asset);
            Conversation convo = AssetDatabase.LoadAssetAtPath<Conversation>(path);
            convo.Started = false;
            convo.Finished = false;
            convo.CurrentNode = null;
        }

        Debug.Log($"{convoStrings.Length} Conversations had their progression cleared!");
    }


    public void Visit()
    {
        Visited = true;

        foreach (Condition condition in ConditionsToSet)
        {
            condition.Value = true;
        }

        OnNodeVisited.Invoke();
    }
}
