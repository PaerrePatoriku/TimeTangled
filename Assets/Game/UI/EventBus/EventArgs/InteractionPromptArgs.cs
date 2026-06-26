using Game.World.Interactable;

namespace Game.UI.EventBus.EventArgs
{
    public class EnterInteractionPromptArgs : System.EventArgs
    {
        public InteractionPrompt[] prompts { get; set; }

        public EnterInteractionPromptArgs(InteractionPrompt[] prompts)
        {
            this.prompts = prompts;
        }
    }
    public class ExitInteractionPromptArgs : System.EventArgs
    {

    }
}