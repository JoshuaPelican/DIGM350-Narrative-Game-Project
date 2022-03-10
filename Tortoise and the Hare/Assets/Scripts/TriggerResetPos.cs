using UnityEngine;

public class TriggerResetPos : MonoBehaviour
{
    [SerializeField] Transform ResetPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        other.transform.position = ResetPoint.position;
    }
}
