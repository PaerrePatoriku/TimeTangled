using Assets.Game.World.Interactable;
using UnityEngine;

public class StandardInteractable : MonoBehaviour, Interactable
{
    [SerializeField]
    InteractionPrompt[] prompts;


    public InteractionPrompt[] GetInteractionPrompts()
    {
        return prompts;
    }

    public void Interact(InteractionEvent @event)
    {
        Debug.Log("INTERACTION WAS TRIGGERED. Event onclick is " + @event.prompt.onInteractEvent + ", player is " + @event.player.name);
        if (@event.prompt.onInteractEvent != null)
        {

            @event.prompt.onInteractEvent.Invoke(@event);
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var prompt in prompts)
        {
            prompt.InteractableObject = GetComponent<InteractableObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
