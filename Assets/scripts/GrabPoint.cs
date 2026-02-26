using UnityEngine;

public class GrabPoint : MonoBehaviour
{
    [HideInInspector] public Vector2 mouseDelta;
    [SerializeField] private float SmoothTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseDelta = Vector2.Lerp(mouseDelta, new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")), Time.deltaTime * SmoothTime);
    }
}
