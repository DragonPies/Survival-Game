using UnityEngine;

public class Camera : MonoBehaviour
{
    private float mouseInputX;
    private float mouseInputY;

    [SerializeField] private float Sensetivity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseInputX += Input.GetAxis("Mouse X") * Sensetivity;
        mouseInputY += Input.GetAxis("Mouse Y") * -1 * Sensetivity;
        transform.localEulerAngles = new Vector3(mouseInputY, mouseInputX, 0);
    }
}
