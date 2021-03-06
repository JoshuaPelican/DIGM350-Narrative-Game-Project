using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

public class ConditionEventTrigger : MonoBehaviour
{
    [TitleGroup("Trigger Settings")]
    [ValueDropdown("GetAllConditions", FlattenTreeView = true, DropdownTitle = "Select A Condition")]
    [SerializeField] Condition[] Conditions;
    [Space]
    [ValueDropdown("GetAllEvents", FlattenTreeView = true, DropdownTitle = "Select An Event")]
    [SerializeField] EventVariable TrueEvent;
    [Space]
    [ValueDropdown("GetAllEvents", FlattenTreeView = true, DropdownTitle = "Select An Event")]
    [SerializeField] EventVariable FalseEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (Conditions.All(x => x.Value == true) || Conditions.Length == 0)
        {
            TrueEvent?.Invoke();
        }
        else
        {
            FalseEvent?.Invoke();
        }
        
        GetComponent<Collider>().enabled = false;
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
