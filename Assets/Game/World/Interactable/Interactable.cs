using Assets.Game.World.Interactable;

public interface Interactable
{
    void Interact();
    InteractionPrompt[] GetInteractionPrompts();
}