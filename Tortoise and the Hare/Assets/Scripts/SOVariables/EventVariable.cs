using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Event Variable", menuName = "Variables/Event")]
public class EventVariable : ScriptableObject
{
    public UnityAction OnInvoke;
}
