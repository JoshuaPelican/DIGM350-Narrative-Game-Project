using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

public class VariableEventTrigger : MonoBehaviour
{
    [TitleGroup("Trigger Settings")]
    [SerializeField] FloatVariable Variable;
    [SerializeField] float LessThanValue;
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

        if (Variable.Value < LessThanValue)
        {
            TrueEvent?.Invoke();
        }
        else
        {
            FalseEvent?.Invoke();
        }

        GetComponent<Collider>().enabled = false;
    }

    static IEnumerable GetAllEvents()
    {
        return AssetDatabase.FindAssets("t:eventvariable", new[] { "Assets/Variables" })
            .Select(x => AssetDatabase.GUIDToAssetPath(x))
            .Select(x => new ValueDropdownItem(AssetDatabase.LoadAssetAtPath<EventVariable>(x).name, AssetDatabase.LoadAssetAtPath<EventVariable>(x)));
    }
}
