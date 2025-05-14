using UnityEngine;

public class IsometricCameraPanner : MonoBehaviour
{

    public float panSpeed;
    private Camera _camera;

    public Vector2 panLimitX;
    public Vector2 panLimitZ;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
    }


    void Update()
    {
        Vector2 panPosition = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        transform.position += Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * new Vector3(panPosition.x, 0, panPosition.y) * panSpeed * Time.deltaTime;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, panLimitX.x, panLimitX.y), transform.position.y, Mathf.Clamp(transform.position.z, panLimitZ.x, panLimitZ.y));
    }
}
