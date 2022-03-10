using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] bool RemoveOnInteract = true;
    public UnityEvent OnInteract;

    public void Interact()
    {
        OnInteract?.Invoke();
        gameObject.SetActive(!RemoveOnInteract);
    }
}
