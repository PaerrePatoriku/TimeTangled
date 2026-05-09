using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameLogic
{
    public class InteractionOrchestrator : MonoBehaviour
    {
        InputAction _escapeAction;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            EnableInteraction();
            _escapeAction = InputSystem.actions.FindAction("Escape"); 
        }

        void EnableInteraction()
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameGlobals.instance.EnableIngameInteraction();
        }

        void DisableInteraction()
        {
            Cursor.lockState = CursorLockMode.None;
            GameGlobals.instance.DisableIngameInteraction();
        }
        // Update is called once per frame
        void Update()
        {
            if (_escapeAction.WasReleasedThisFrame())
            {
                if (GameGlobals.instance.InGameInteractionActive)
                    DisableInteraction();
                else
                    EnableInteraction();
            }
            
        }
    }
}
