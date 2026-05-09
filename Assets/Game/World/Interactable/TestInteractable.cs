using Assets.Game.World.Interactable;
using UnityEngine;

public class TestInteractable : MonoBehaviour, Interactable
{
    [SerializeField]
    InteractionPrompt[] prompts;


    public InteractionPrompt[] GetInteractionPrompts()
    {
        return prompts;
    }

    public void Interact(InteractionEvent e)
    {
        Debug.Log("INTERACTION WAS TRIGGERED"); 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
