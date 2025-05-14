using UnityEngine;
using UnityEngine.UI;

public class NoteReading : MonoBehaviour
{
    [Header("Properties")]
    public bool reading = false;

    [Header("UI")]
    public GameObject readingUI;
    [SerializeField] Text readingText;

    [Header("Scripts and components")]
    public ItemInteraction itemInt;
    private GuessingManager gm;
    private Note selectedNoteText;
 /*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    private void Start()
    {
        readingUI.gameObject.SetActive(false);
        gm = GetComponent<GuessingManager>();
    }
    private void Update()
    {
        if (!gm.guessingMode && !itemInt.inspecting)   
        {
            Reading();
        }   
    }
 /*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    private void Reading()
    {
        if (Input.GetMouseButtonDown(0) && !reading)
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition); //Raycast que apunta a donde esta el mouse
            RaycastHit clickedNote;

            if (Physics.Raycast(rayo, out clickedNote) && clickedNote.transform.gameObject.tag == "Note")
            {
                selectedNoteText = clickedNote.transform.gameObject.GetComponent<Note>();
                readingText.text = selectedNoteText.noteText;
                readingUI.gameObject.SetActive(true);
                reading = true;
            }           
        }
        if (Input.GetMouseButtonDown(1) && reading)
        {
            readingUI.gameObject.SetActive(false);
            reading = false;
        }
    }
}
