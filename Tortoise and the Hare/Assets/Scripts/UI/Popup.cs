using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public TextMeshProUGUI characterTextMesh;
    public TextMeshProUGUI DialogueTextMesh;

    Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.forward = cameraTransform.forward;
    }

    public void DisplayPopup(Node node)
    {
        gameObject.SetActive(true);

        characterTextMesh.SetText(node.Speaker.name);
    }

    public void RemovePopup()
    {
        gameObject.SetActive(false);
    }
}
