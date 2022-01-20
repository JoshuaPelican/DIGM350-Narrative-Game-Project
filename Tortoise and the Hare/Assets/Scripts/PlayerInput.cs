using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] KeyCode[] choiceKeys = new KeyCode[]
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4
    };

    [SerializeField] KeyCode interactKey = KeyCode.Mouse0; 

    public static UnityAction<int> OnChoiceInput;
    public static UnityAction OnInteractInput;

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            OnInteractInput?.Invoke();
        }
        else
        {
            for (int i = 0; i < choiceKeys.Length; i++)
            {
                if (Input.GetKeyDown(choiceKeys[i]))
                {
                    OnChoiceInput?.Invoke(i);
                }
            }
        }

    }
}
