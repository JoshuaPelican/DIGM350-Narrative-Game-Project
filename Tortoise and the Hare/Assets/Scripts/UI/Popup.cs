using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public TextMeshProUGUI characterTextMesh;
    public TextMeshProUGUI DialogueTextMesh;

    public GameObject popupBase;

    Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        popupBase.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.forward = cameraTransform.forward;
    }

    public void DisplayPopup(Node node)
    {
        popupBase.SetActive(true);

        characterTextMesh.SetText(node.Speaker.name);
    }

    public void RemovePopup()
    {
        popupBase.SetActive(false);
    }
}
