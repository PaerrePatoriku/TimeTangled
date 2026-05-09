using UnityEngine;

public class GameGlobals : MonoBehaviour
{
    [SerializeField]
    public GameUIEventBus gameUIEventBus;
    public static GameGlobals instance;
    public bool InGameInteractionActive;
    public float MaximumInteractionDistance = 5f;
    private void Awake()
    {
        instance = this;
    }

    public void EnableIngameInteraction()
    {
        InGameInteractionActive = true;
    }

    public void DisableIngameInteraction()
    {
        InGameInteractionActive = false;
    }

    public void ToggleIngameInteraction()
    {
        InGameInteractionActive = !InGameInteractionActive;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
