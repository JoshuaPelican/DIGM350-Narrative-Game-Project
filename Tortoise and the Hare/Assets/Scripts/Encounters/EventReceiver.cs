using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class EventReceiver : MonoBehaviour
{
    [TitleGroup("Receiver Settings")]
    [ValueDropdown("GetAllEvents", FlattenTreeView = true, DropdownTitle = "Select An Event")]
    [SerializeField] EventVariable ReceivedEvent;
    [Space]
    [SerializeField] UnityEvent OnEventReceived;

    private void OnEnable()
    {
        ReceivedEvent.OnInvoke += Receive;
    }

    void Receive()
    {
        OnEventReceived.Invoke();
    }

    static IEnumerable GetAllEvents()
    {
        return AssetDatabase.FindAssets("t:eventvariable", new[] { "Assets/Variables" })
            .Select(x => AssetDatabase.GUIDToAssetPath(x))
            .Select(x => new ValueDropdownItem(AssetDatabase.LoadAssetAtPath<EventVariable>(x).name, AssetDatabase.LoadAssetAtPath<EventVariable>(x)));
    }
}
