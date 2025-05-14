using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    [Header("Zoom")]
    public float multiplier;
    public float minDis;
    public float maxDis;
    float mouseWheelMov; //Movimiento de la camara

    [Header("Scripts and components")]
    public ItemInteraction itemInter;
    public GuessingManager gm;
    Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        if (!itemInter.inspecting && !gm.guessingMode)
        {
            Zoom();
        }        
    }

    void Zoom()
    {
        mouseWheelMov = Input.GetAxis("Mouse ScrollWheel");

        cam.fieldOfView -= mouseWheelMov * multiplier;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minDis,maxDis);
    }

}
