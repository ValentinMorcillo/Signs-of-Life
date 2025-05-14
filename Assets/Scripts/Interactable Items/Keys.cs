using UnityEngine;
using UnityEngine.UI;

public class Keys : MonoBehaviour
{
    [Header("Properties")]
    public string keyName;
    public int keyID;
    public Image keyIcon;
    
    private void Start()
    {
        keyIcon = GetComponent<Image>();
    }
}
