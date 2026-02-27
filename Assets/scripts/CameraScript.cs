using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float mouseInputX;
    private float mouseInputY;
    [HideInInspector] public bool isCameraOn = true;


    [SerializeField] private float Sensetivity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCameraOn)
        {
            mouseInputX += Input.GetAxis("Mouse X") * Sensetivity;

            mouseInputY += Input.GetAxis("Mouse Y") * -1 * Sensetivity;
            mouseInputY = Mathf.Clamp(mouseInputY, -90, 90);

            transform.localEulerAngles = new Vector3(mouseInputY, mouseInputX, 0);
        }
    }

}
