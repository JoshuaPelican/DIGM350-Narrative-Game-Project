using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent OnPickup;

    public void Pickup()
    {
        OnPickup?.Invoke();
        gameObject.SetActive(false);
    }
}
