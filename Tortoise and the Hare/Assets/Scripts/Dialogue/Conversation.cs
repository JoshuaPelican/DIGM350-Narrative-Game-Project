using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "C_NewConvo", menuName = "Dialogue/Conversation")]
public class Conversation : ScriptableObject
{
    [Header("Conversation Nodes")]
    public Node StartingNode;
    public Node CancelledNode;
    public Node ReturningNode;
    public Node FinishedNode;
    [Space(10)]
    [SerializeField]
    public bool Started;
    [SerializeField]
    public bool Finished;

    //Retrieves the next node in the conversation
    //Returns null if there is no non-visited nodes left
    public Node NextNode()
    {
        Node nextNode = Finished ? FinishedNode : StartingNode;
        int i = 0;

        while (nextNode != null && nextNode?.Visited == true && i <= 50)
            nextNode = nextNode.NextNode();

        if (nextNode == null)
            Finished = true;

        return nextNode;
    }

#if UNITY_EDITOR

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
        }

        Debug.Log($"{convoStrings.Length} Conversations had their progression cleared!");
    }

#endif

}
