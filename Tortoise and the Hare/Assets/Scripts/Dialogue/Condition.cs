using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Condition", menuName = "Dialogue/Condition")]
public class Condition : ScriptableObject
{
    public bool Value;

    [MenuItem("Dialogue/Reset Conditions")]
    public static void ClearDialogueProgression()
    {
        string[] conditionStrings = AssetDatabase.FindAssets("t:condition", new[] { "Assets/Dialogue" });
        foreach (string asset in conditionStrings)
        {
            string path = AssetDatabase.GUIDToAssetPath(asset);
            AssetDatabase.LoadAssetAtPath<Condition>(path).Value = false;
        }

        Debug.Log($"{conditionStrings.Length} Conditions had their Value reset!");
    }
}
