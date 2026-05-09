using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Game.World.Interactable
{
    public enum InteractionEventType
    {
        InputPressed,
        InputReleased
    }
    public class InteractionEvent
    {
        public InteractionPrompt prompt;
        public GameObject player;
        public InteractionEventType interactionEventType = InteractionEventType.InputPressed;
        
        public InteractionEvent(InteractionPrompt prompt, 
                                GameObject player)
        {
            this.prompt = prompt;
            this.player = player;
            
            if (this.prompt.getPromptAction() != null )
            {
                if (this.prompt.getPromptAction().WasReleasedThisFrame())
                {
                    this.interactionEventType = InteractionEventType.InputReleased;
                }
            }

        }
    }
}
