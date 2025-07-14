using UnityEngine;

public class GameGlobals : MonoBehaviour
{
    [SerializeField]
    public GameUIEventBus gameUIEventBus;
    public static GameGlobals instance;
    private void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
