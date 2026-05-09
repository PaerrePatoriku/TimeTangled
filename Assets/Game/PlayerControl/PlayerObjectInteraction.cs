using Assets.Game.World.Interactable;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerObjectInteraction : MonoBehaviour
{
//    [SerializeField]
    float _interactionDistanceMax = 1f;
    [SerializeField]
    LayerMask _interactableLayerMask;
    InteractableObject currentInteractableObject;
    Camera _raycastCamera;
    GameObject _player;


    InteractionPrompt[] _currentPossibleInteractions;
    InteractionPrompt _lastActionPrompt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _raycastCamera = Camera.main;
        _player = this.transform.parent.gameObject;
        _lastActionPrompt = null;
        _interactionDistanceMax = GameGlobals.instance.MaximumInteractionDistance;
    }



    // Update is called once per frame
    void Update()
    {
        
        if (_lastActionPrompt != null )
        {
            float distanceMagnitude = 0f;
            if (_lastActionPrompt.requireMaximumDistance)
            {
                distanceMagnitude = Vector3.Distance(this.transform.position,
                    _lastActionPrompt.interactableObject.transform.position);

            }
            if (_lastActionPrompt.getPromptAction().WasReleasedThisFrame() || 
                (_lastActionPrompt.requireMaximumDistance && distanceMagnitude > _interactionDistanceMax))
            {
                _lastActionPrompt.InteractableObject.CallInteraction(new InteractionEvent(_lastActionPrompt, _player, InteractionEventType.InputCanceled));
                _lastActionPrompt = null;                
                return;
            }
        }
        //Jos ei kontrollia: ei tarvitse tarkistaa voiko minkään kanssa interactaa
        if (!GameGlobals.instance.InGameInteractionActive)
            return;
        
        Vector3 startPos = this.transform.position;
        Vector3 endPos = GetInteractionDirectionRay() ;
        Debug.DrawRay(startPos, endPos, Color.aliceBlue);
        RaycastHit hit;
        if (Physics.Raycast(startPos, endPos, out hit, _interactionDistanceMax, _interactableLayerMask))
        {
            Debug.Log($"${hit.collider.gameObject.name} ${hit.distance}, ${_interactionDistanceMax}");
            InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();
            if (interactableObject != null)
            {
                if (currentInteractableObject == null || currentInteractableObject != interactableObject )
                {
                    currentInteractableObject = interactableObject;
                    currentInteractableObject.PromptInteraction(out var prompts);
                    if (prompts != null)
                    {
                        _currentPossibleInteractions = prompts;
                        
                    }
                }
            }
        }
        else if (currentInteractableObject != null)
        {
            GameGlobals.instance.gameUIEventBus.UpdateEvent(new ExitInteractionPromptArgs());
            currentInteractableObject = null;
            _currentPossibleInteractions = null;
        }


        
        if (_currentPossibleInteractions != null && _currentPossibleInteractions.Length > 0)
        {
            foreach (var inputPrompt in _currentPossibleInteractions)
            {
                if (inputPrompt.getPromptAction().WasPressedThisFrame() || inputPrompt.getPromptAction().WasReleasedThisFrame())
                {

                    if (inputPrompt.getPromptAction().WasPressedThisFrame())
                        _lastActionPrompt = inputPrompt;
                    else //release
                        _lastActionPrompt = null;

                    currentInteractableObject.CallInteraction(new InteractionEvent(inputPrompt, _player));
                    break;
                }
            }
        }

    }

    Vector3 GetInteractionDirectionRay()
    {
        return _raycastCamera.transform.TransformDirection(Vector3.forward);
    }
}
