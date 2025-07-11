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
    public void PromptInteraction()
    {
        Debug.Log("ENTERING INTERACTION AREA");
        var prompts = interactable.GetInteractionPrompts();
        foreach (var prompt in prompts)
        {
            Debug.Log(prompt.getActionBindingDisplayString());
        }
    }
}
