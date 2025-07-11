using UnityEngine;

public class PlayerObjectInteraction : MonoBehaviour
{
    [SerializeField]
    float _interactionDistanceMax = 1f;
    [SerializeField]
    LayerMask _interactableLayerMask;
    InteractableObject currentInteractableObject;
    Camera _raycastCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _raycastCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = _raycastCamera.transform.position;
        Vector3 endPos = GetInteractionDirectionRay() * _interactionDistanceMax;
        Debug.DrawRay(startPos, endPos, Color.aliceBlue);
        RaycastHit hit;
        if (Physics.Raycast(startPos, endPos, out hit, _interactionDistanceMax, _interactableLayerMask))
        {
            //Debug.Log(hit.collider.gameObject.name);
            InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();
            if (interactableObject != null)
            {
                if (currentInteractableObject == null || currentInteractableObject != interactableObject )
                {
                    currentInteractableObject = interactableObject;
                    currentInteractableObject.PromptInteraction();
                }
            }
        }
        else if (currentInteractableObject != null)
        {
            currentInteractableObject = null;
        }
    }
    Vector3 GetInteractionDirectionRay()
    {
        return _raycastCamera.transform.TransformDirection(Vector3.forward);
    }
}
