using UnityEngine;

public class FPSInteract : MonoBehaviour
{
    [SerializeField] float interactRange = 100f;
    [SerializeField] LayerMask interactableLayers;

    Transform mainCam;
    RaycastHit clickHit;

    private void Start()
    {
        mainCam = Camera.main.transform;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckClickedObject();
        }
    }

    private void CheckClickedObject()
    {
        Ray clickRay = new Ray(mainCam.position, mainCam.forward);
        Physics.Raycast(clickRay, out clickHit, interactRange, interactableLayers, QueryTriggerInteraction.Ignore);

        if (!clickHit.transform)
            return;

        if (!clickHit.transform.TryGetComponent(out Interactable interactable))
            return;

        interactable.Interact();
    }
}