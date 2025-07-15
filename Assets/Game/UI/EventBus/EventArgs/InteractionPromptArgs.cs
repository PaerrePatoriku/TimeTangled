using Assets.Game.World.Interactable;
using System;

public class EnterInteractionPromptArgs : EventArgs
{
    public InteractionPrompt[] prompts { get; set; }

    public EnterInteractionPromptArgs(InteractionPrompt[] prompts)
    {
        this.prompts = prompts;
    }
}
public class ExitInteractionPromptArgs : EventArgs
{

}
