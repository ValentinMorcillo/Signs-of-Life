using UnityEngine;

public class Locked : MonoBehaviour
{
    [Header("Locked Properties")]
    public int lockID;
    public bool unlocked;
    
    [Header("Components")]
    public GameObject keyToUnlock;
}
