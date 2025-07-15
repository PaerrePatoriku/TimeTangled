using Assets.Game.World.Interactable;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    GameObject interactableObjectSlot;
    Interactable interactable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactable = interactableObjectSlot.GetComponent<Interactable>();
        Debug.Log(interactable.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CallInteraction()
    {
        interactable.Interact();
    }
    public void PromptInteraction(out InteractionPrompt[] prompts)
    {
        Debug.Log("ENTERING INTERACTION AREA");
        prompts = interactable.GetInteractionPrompts();
        GameGlobals.instance.gameUIEventBus.UpdateEvent(new EnterInteractionPromptArgs(prompts));
    }
}
