using UnityEngine;

public class FPSLook : MonoBehaviour
{
    [SerializeField] float mouseSpeed = 3;

    Transform camTrans;
    float xRotation = 0f;

    private void Start()
    {
        camTrans = Camera.main.transform;

        //Locks cursor to middle screen and hides it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Get both mouse axis
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed;

        //Calculate the camera rotation based on mouse Y axis and clamp it
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75f, 60f);

        //Apply camera rotation
        camTrans.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //Apply player rotation using mouse X axis
        transform.Rotate(Vector3.up * mouseX);
    }
}