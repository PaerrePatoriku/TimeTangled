using Game.GameLogic;
using Game.UI.EventBus.EventArgs;
using UnityEngine;

namespace Game.World.Interactable
{
    public class InteractableObject : MonoBehaviour
    {
        [SerializeField]
        GameObject interactableObjectSlot;
        Interactable interactable;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            interactable = interactableObjectSlot.GetComponent<Interactable>();
            UnityEngine.Debug.Log(interactable.ToString());
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void CallInteraction(InteractionEvent e)
        {
            interactable.Interact(e);
        }
        public void PromptInteraction(out InteractionPrompt[] prompts)
        {
            UnityEngine.Debug.Log("ENTERING INTERACTION AREA");
            prompts = interactable.GetInteractionPrompts();
            GameGlobals.instance.gameUIEventBus.UpdateEvent(new EnterInteractionPromptArgs(prompts));
        }
    }
}
