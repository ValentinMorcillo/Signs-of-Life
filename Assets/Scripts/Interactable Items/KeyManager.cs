using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    //MANAGER VARIABLES
    private List<int> keyInventory = new List<int>();

    [Header("UI")]
    public GameObject keyUI;
    private Image keyIconComp;
    [SerializeField] private List<GameObject> iconPositions = new List<GameObject>();

    //SCRIPT VARIABLES
    private GuessingManager gm;
    private NoteReading nr;
    private Keys k;
    private Locked l;
    /*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    private void Start()
    {
        gm = GetComponent<GuessingManager>();
        nr = GetComponent<NoteReading>();
        keyUI.SetActive(false);
    }
    private void Update()
    {
        if (!gm.guessingMode && !nr.reading)
        {
            PickUp();
            Unlock();
        }
    }
 /*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    void PickUp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition); //Raycast que apunta a donde esta el mouse
            RaycastHit clickedObject;

            if (Physics.Raycast(rayo, out clickedObject) && clickedObject.transform.gameObject.tag == "Key")
            {
                k = clickedObject.transform.GetComponent<Keys>();
                keyInventory.Add(k.keyID);
                ShowKeyUI();
                print("Picked up " + k.keyName);
                clickedObject.transform.gameObject.SetActive(false);
            }
        }
    }
    void Unlock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition); //Raycast que apunta a donde esta el mouse
            RaycastHit clickedObject;

            if (Physics.Raycast(rayo, out clickedObject) && clickedObject.transform.gameObject.tag == "Locked")
            {
                l = clickedObject.transform.GetComponent<Locked>();

                if (keyInventory.Contains(l.lockID) && l.unlocked == false)
                {
                    print("Unlocked with " + l.keyToUnlock.name);
                    keyInventory.Remove(l.lockID);
                    l.unlocked = true;
                    HideKeyUI();
                }
                else if (l.unlocked)
                {
                    print("Already open");
                }
                else
                {
                    print("Dont have the key");
                }
            }
        }
    }
 /*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    void ShowKeyUI()
    {
        keyUI.SetActive(true);
        for (int i = 0; i < iconPositions.Count; i++)
        {
            if (!iconPositions[i].gameObject.activeInHierarchy)
            {
                keyIconComp = iconPositions[i].gameObject.GetComponent<Image>();
                keyIconComp.sprite = k.keyIcon.sprite;
                iconPositions[i].gameObject.SetActive(true);
                iconPositions[i].gameObject.name = k.keyName;
                return;
            }
        }
    }
    void HideKeyUI()
    {
        GameObject keyIconOfThisLock = GameObject.Find(l.keyToUnlock.name);
        if (keyIconOfThisLock != null)
        {
            keyIconOfThisLock.SetActive(false);
        }
        SortKeyUI();
    }
    void SortKeyUI()
    {
        for (int i = 0; i < iconPositions.Count; i++)
        {
            if (i > 0 && iconPositions[i].gameObject.activeInHierarchy && !iconPositions[i - 1].gameObject.activeInHierarchy)
            {
                Image iconComp;
                keyIconComp = iconPositions[i - 1].gameObject.GetComponent<Image>();
                iconComp = iconPositions[i].gameObject.GetComponent<Image>();
                keyIconComp.sprite = iconComp.sprite;
                iconPositions[i - 1].gameObject.SetActive(true);
                iconPositions[i - 1].gameObject.name = iconPositions[i].gameObject.name;
                iconPositions[i].gameObject.SetActive(false);
            }
        }
    }
}

