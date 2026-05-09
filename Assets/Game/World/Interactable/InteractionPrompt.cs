using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.Game.World.Interactable
{
    [Serializable]
    public class InteractionPrompt
    {

        public string promptInputActionName;
        public string promptText;
        public UnityEvent<InteractionEvent> onInteractEvent; 
        public InteractableObject interactableObject;
        public bool requireMaximumDistance = true;
        public InteractableObject InteractableObject
        {
            get => interactableObject;
            set => interactableObject = value;
        }

        public InteractionPrompt()
        {
            
        }

        public InputAction getPromptAction()
        {
            return InputSystem.actions.FindAction(promptInputActionName);
        }
        public string getActionBindingDisplayString()
        {
            InputAction action = getPromptAction();
            return action.GetBindingDisplayString(InputBinding.MaskByGroup("Keyboard&Mouse"));
        }
    }
}
