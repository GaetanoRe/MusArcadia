using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance;
    public bool canPlayerMove;

    public void Start()
    {
        canPlayerMove = true;
    }

    public void Update()
    {
        playerSettingsSO.canPlayerMove = canPlayerMove;
    }

    // Singleton reference
    [SerializeField] private PlayerSettingsSO playerSettingsSO;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object alive between scenes
        }
        else
        {
            Destroy(gameObject); // Destroys any duplicate
        }
    }
}