using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Event", menuName = "Variables/Event")]
public class EventVariable : ScriptableObject
{
    public UnityAction OnInvoke;

    public void Invoke()
    {
        OnInvoke?.Invoke();
    }
}
