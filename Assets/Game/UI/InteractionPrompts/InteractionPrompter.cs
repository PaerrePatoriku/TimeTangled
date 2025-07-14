using Assets.Game.UI.EventBus.Attributes;
using Assets.Game.World.Interactable;
using System;
using UnityEngine;

public class InteractionPrompter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GameObject promptParent;
    [SerializeField]
    GameObject promptPrefab;

    InteractionPrompt[] currentPrompts;


    void Start()
    {
        GameGlobals.instance.gameUIEventBus.RegisterInstance(this);
        
    }
    private void OnDestroy()
    {
        GameGlobals.instance.gameUIEventBus.UnregisterInstance(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        RenderPrompts();
    }
    void RenderPrompts()
    {
        for (int i = 0; i < promptParent.transform.childCount; i++)
        {
            var child = promptParent.transform.GetChild(i);
            GameObject.Destroy(child.gameObject);
        }
        if (currentPrompts != null && currentPrompts.Length > 0)
        {
            foreach (var prompt in currentPrompts)
            {
                var promptObject = GameObject.Instantiate(promptPrefab, promptParent.transform);
                promptObject.GetComponent<UITextPrompt>().SetText($"[{prompt.getActionBindingDisplayString()}] {prompt.promptText}");
            }
        }
    }
    [UISignal]
    void OnUpdatePrompt(EnterInteractionPromptArgs args)
    {
        promptParent.SetActive(true);
        var prompts = args.prompts;
        currentPrompts = prompts;
        RenderPrompts();
    }
    [UISignal]
    void OnExitPrompt(ExitInteractionPromptArgs args)
    {
        promptParent.SetActive(false);
        currentPrompts = null;
        RenderPrompts();
    }
}
