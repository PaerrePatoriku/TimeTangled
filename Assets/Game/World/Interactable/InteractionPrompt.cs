using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

namespace Assets.Game.World.Interactable
{
    [Serializable]
    public class InteractionPrompt
    {

        public string promptInputActionName;
        public string promptText;

        public InputAction promptAction()
        {
            return InputSystem.actions.FindAction(promptInputActionName);
        }
        public string getActionBindingDisplayString()
        {
            InputAction action = promptAction();
            return action.GetBindingDisplayString(InputBinding.MaskByGroup("Keyboard&Mouse"));
        }
    }
}
