using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed;
    private Vector3 previousMousePosition;
    
    [Header("Scripts and components")]
    public GuessingManager gm;
    public NoteReading nr;
    public GameObject itemInteractionPosition;
    Camera cam;

    [HideInInspector]public bool inspecting = false;
    private GameObject currentlyInspecting;
    private Vector3 ogPosition;
    private Quaternion ogRotation;

   
    /*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        if (!gm.guessingMode && !nr.reading)
        {
            ItemInspection();
        }     
        if (inspecting && !gm.guessingMode)
        {     
            ItemRotation();                           
        }     
    }
 /*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    void ItemRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePosition = Input.mousePosition - previousMousePosition;
            currentlyInspecting.transform.rotation =
                Quaternion.AngleAxis(-deltaMousePosition.x * rotationSpeed, transform.up) *
                Quaternion.AngleAxis(deltaMousePosition.y * rotationSpeed, transform.right) *
                currentlyInspecting.transform.rotation;

            previousMousePosition = Input.mousePosition;
        }
    }
 /*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    void ItemInspection()
    {
        if (Input.GetMouseButtonDown(0) && !inspecting)
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition); //Raycast que apunta a donde esta el mouse
            RaycastHit clickedObject;

            if (Physics.Raycast(rayo, out clickedObject) && clickedObject.transform.gameObject.tag == "Interactable")
            {
                currentlyInspecting = clickedObject.transform.gameObject;
                ogPosition = currentlyInspecting.transform.position;
                ogRotation = currentlyInspecting.transform.rotation;
                inspecting = true;
                clickedObject.transform.position = itemInteractionPosition.transform.position;
                clickedObject.transform.LookAt(transform.position);
                cam.fieldOfView = 60;
            }
        }
        if (Input.GetMouseButtonDown(1) && inspecting)
        {
            inspecting = false;
            currentlyInspecting.transform.position = ogPosition;
            currentlyInspecting.transform.rotation = ogRotation;
        }      
    }
}
