using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float mouseInputX;
    private float mouseInputY;
    [HideInInspector] public bool isCameraOn = true;

    [HideInInspector] public Vector2 mouseDelta;
    [SerializeField] private float SmoothTime;

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
            mouseDelta = Vector2.Lerp(mouseDelta, new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")),Time.deltaTime * SmoothTime);
            mouseInputX += Input.GetAxis("Mouse X") * Sensetivity;

            mouseInputY += Input.GetAxis("Mouse Y") * -1 * Sensetivity;
            mouseInputY = Mathf.Clamp(mouseInputY, -90, 90);

            transform.localEulerAngles = new Vector3(mouseInputY, mouseInputX, 0);
        }
    }

}
