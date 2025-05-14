using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GuessingManager : MonoBehaviour
{
    [Header("Scripts")]
    private KeyManager km;
    private NoteReading nr;

    [Header("Level")]
    [SerializeField] private string signText;
    private string userGuess;
    [HideInInspector] public bool guessingMode = false;

    [Header("UI")]
    public GameObject guessingUI;
    public Text currentModeText;
    [SerializeField] GameObject currentLetterSpaceIcon;
    [SerializeField] private List<GameObject> signLetters = new List<GameObject>();

    //LOGIC
    int fullLetters = 0;
    GameObject currentLetterSpace;
    bool foundCurrentLetterSpace;
    private static List<KeyCode> kbCharacters = new List<KeyCode>() 
    { KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, 
      KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, 
      KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z};
    /*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    private void Start()
    {
        guessingUI.SetActive(false);
        km = GetComponent<KeyManager>();
        nr = GetComponent<NoteReading>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !guessingMode && !nr.reading)
        {
            EnterGuessing();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && guessingMode)
        {
            ExitGuessing();
        }
        if (guessingMode)
        {
            CurrentLetterSpace();
            TypeIn();
            Backspace();
            SubmitGuess();


        }     
    }
 /*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    private void TypeIn()
    {
        if (Input.inputString != "")
        {
            for (int i = 0; i < kbCharacters.Count; i++)
            {
                if (Input.GetKeyDown(kbCharacters[i]))
                {
                    for (int x = 0; x < signLetters.Count; x++)
                    {
                        Text txt = signLetters[x].GetComponent<Text>();

                        if (txt.text == "")
                        {
                            txt.text = Input.inputString.ToUpper();
                            fullLetters++;
                            foundCurrentLetterSpace = false;
                            return;
                        }
                    }
                }
            }           
        }
    }
    private void Backspace()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
             for (int x = signLetters.Count-1; x >= 0; x--)
             {
                Text txt = signLetters[x].GetComponent<Text>();

                if (txt.text != "")
                {
                    txt.text = "";
                    fullLetters--;
                    foundCurrentLetterSpace = false;
                    return;                       
                }
             }
        }
    }    
    private void SubmitGuess()
    {      
        if (Input.GetKeyDown(KeyCode.Return) && fullLetters == signLetters.Count)
        {
            for (int i = 0; i < signLetters.Count; i++)
            {
                Text txt = signLetters[i].GetComponent<Text>();
                userGuess += txt.text;         
            }
            /*if (fullLetters == signLetters.Count)
            {
                print("full");
            }
            else
            {
                print("COMPLETE BEFORE SUBMITTING");
            }  */
            if(userGuess == signText)
            {
                print("Success");
                userGuess = "";
            }
            else
            {
                print("Wrong guess");
                userGuess = "";            
            }
            
        }
    }
    private void CurrentLetterSpace()
    {
        if(!foundCurrentLetterSpace)
        {
            for (int x = 0; x < signLetters.Count; x++)
            {
                Text txt = signLetters[x].GetComponent<Text>();

                if (txt.text == "")
                {
                    currentLetterSpace = signLetters[x].gameObject;
                    foundCurrentLetterSpace = true;
                    currentLetterSpaceIcon.transform.position = signLetters[x].gameObject.transform.position;
                    currentLetterSpaceIcon.SetActive(true);
                    break;
                }
                else if (txt.text != "")
                {
                    currentLetterSpaceIcon.SetActive(false);
                }
            }
        }
    }
    private void EnterGuessing()
    {
        guessingMode = true;
        guessingUI.SetActive(true);
        currentModeText.text = "Guess";
        km.keyUI.SetActive(false);
        nr.readingUI.SetActive(false);
    }
    private void ExitGuessing()
    {
        guessingMode = false;
        guessingUI.SetActive(false);
        currentModeText.text = "Look";
        km.keyUI.SetActive(true);
    }
}
