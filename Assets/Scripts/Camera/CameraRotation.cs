using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Header("Rotation")]
    public float rotateSpeed;
    [Header("Scripts")]
    public GuessingManager gm;
    public ItemInteraction itInt;
    public NoteReading nr;

    void Update()
    {
        if (!gm.guessingMode && !itInt.inspecting && !nr.reading)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Quaternion.identity.x, rotateSpeed, Quaternion.identity.z);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Quaternion.identity.x, -rotateSpeed, Quaternion.identity.z);
            }
        }    
    }
}
