using System.Collections;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

public class Node : ScriptableObject
{
    [PropertyOrder(0)]
    [TitleGroup("Speaker Settings")]
    public Character Speaker;

    [PropertyOrder(1)]
    [TitleGroup("Dialogue Settings")]
    [TextArea(3, 10)]
    public string Dialogue;

    [TitleGroup("Extra Settings")]
    [Space]
    [PropertyOrder(4)]
    public float TimeSpent = 2.5f;
    [Space]
    [PropertyOrder(4)]
    [Range(0, 1)] public float DialogueSpeed = 0.95f;
    [Space]
    [PropertyOrder(4)]
    public float FinishDelay = 1f;

    [Space(10)]
    [PropertyOrder(6)]
    [ValueDropdown("GetAllConditions", FlattenTreeView = true, DropdownTitle = "Select A Condition")]
    public Condition[] ConditionsToSet;

    [HideInInspector]
    public bool Visited;

    FloatVariable time;

    public void Visit()
    {
        Visited = true;

        string[] file = AssetDatabase.FindAssets("Time t:floatvariable", new[] { "Assets/Variables" });

        string path = AssetDatabase.GUIDToAssetPath(file[0]);

        time = AssetDatabase.LoadAssetAtPath<FloatVariable>(path);

        time.AddValue(TimeSpent);

        foreach (Condition condition in ConditionsToSet)
        {
            condition.Value = true;
        }
    }

    public virtual Node NextNode()
    {
        return null;
    }

    static IEnumerable GetAllConditions()
    {
        return AssetDatabase.FindAssets("t:condition", new[] { "Assets/Dialogue" })
            .Select(x => AssetDatabase.GUIDToAssetPath(x))
            .Select(x => new ValueDropdownItem(AssetDatabase.LoadAssetAtPath<Condition>(x).name, AssetDatabase.LoadAssetAtPath<Condition>(x)));
    }
}
