namespace Game.World.Interactable
{
    public interface Interactable
    {
        void Interact(InteractionEvent @event);
        InteractionPrompt[] GetInteractionPrompts();
    }
}