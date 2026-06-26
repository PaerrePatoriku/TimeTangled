using UnityEngine;

namespace Game.World.Interactable
{
    public enum InteractionEventType
    {
        InputPressed,
        InputReleased,
        InputCanceled
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

        public InteractionEvent(InteractionPrompt prompt,
                                GameObject player,
                                InteractionEventType interactionEventType)
        {
            this.prompt = prompt;
            this.player = player;
            this.interactionEventType = interactionEventType;
        }
    }
}
