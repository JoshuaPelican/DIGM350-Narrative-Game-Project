using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Float Variable", menuName = "SO/Float Variable")]
public class FloatVariable : ScriptableObject
{
    public float Value { get; private set; }

    public UnityAction<float> OnValueChanged;

    public void AddValue(float amount)
    {
        Value += amount;
        OnValueChanged?.Invoke(Value);
    }

    public void ClearVariable()
    {
        Value = 0;
    }

    [MenuItem("SO/Clear Variables")]
    public static void ClearDialogueProgression()
    {
        string[] varStrings = AssetDatabase.FindAssets("t:floatvariable", new[] { "Assets/SO" });
        foreach (string asset in varStrings)
        {
            string path = AssetDatabase.GUIDToAssetPath(asset);
            AssetDatabase.LoadAssetAtPath<FloatVariable>(path).ClearVariable();
        }

        Debug.Log($"{varStrings.Length} Variables had their Value cleared!");
    }
}
