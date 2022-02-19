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
    float timeSpent = 2.5f;
    [Space]
    [PropertyOrder(4)]
    [Range(0, 1)] public float DialogueSpeed = 0.95f;

    [Space(10)]
    [PropertyOrder(6)]
    [ValueDropdown("GetAllConditions", FlattenTreeView = true, DropdownTitle = "Select A Condition")]
    public Condition[] ConditionsToSet;

    [PropertyOrder(7)]
    [ValueDropdown("GetAllEvents", FlattenTreeView = true, DropdownTitle = "Select An Event")]
    public EventVariable[] EventsToTrigger;

    [HideInInspector]
    public bool Visited;

    [SerializeField] FloatVariable time;

    public void Visit()
    {
        Visited = true;

        time.AddValue(timeSpent);

        foreach (Condition condition in ConditionsToSet)
        {
            condition.SetValue(true);
        }

        foreach (EventVariable eventVar in EventsToTrigger)
        {
            eventVar.Invoke();
        }
    }

    public virtual Node NextNode()
    {
        return null;
    }

#if UNITY_EDITOR

    static IEnumerable GetAllConditions()
    {
        return AssetDatabase.FindAssets("t:condition", new[] { "Assets/Dialogue" })
            .Select(x => AssetDatabase.GUIDToAssetPath(x))
            .Select(x => new ValueDropdownItem(AssetDatabase.LoadAssetAtPath<Condition>(x).name, AssetDatabase.LoadAssetAtPath<Condition>(x)));
    }

    static IEnumerable GetAllEvents()
    {
        return AssetDatabase.FindAssets("t:eventvariable", new[] { "Assets/Variables" })
            .Select(x => AssetDatabase.GUIDToAssetPath(x))
            .Select(x => new ValueDropdownItem(AssetDatabase.LoadAssetAtPath<EventVariable>(x).name, AssetDatabase.LoadAssetAtPath<EventVariable>(x)));
    }

#endif

}
