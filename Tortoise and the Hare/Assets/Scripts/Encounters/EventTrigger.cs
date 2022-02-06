using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

public class EventTrigger : MonoBehaviour
{
    [ValueDropdown("GetAllConditions", FlattenTreeView = true, DropdownTitle = "Select A Condition")]
    [SerializeField] Condition[] Conditions;

    public EventVariable eventToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!Conditions.All(x => x.Value == true))
            return;

        eventToTrigger.OnEvent.Invoke();

        Destroy(gameObject);
    }

    static IEnumerable GetAllConditions()
    {
        return AssetDatabase.FindAssets("t:condition", new[] { "Assets/Dialogue" })
            .Select(x => AssetDatabase.GUIDToAssetPath(x))
            .Select(x => new ValueDropdownItem(AssetDatabase.LoadAssetAtPath<Condition>(x).name, AssetDatabase.LoadAssetAtPath<Condition>(x)));
    }
}
