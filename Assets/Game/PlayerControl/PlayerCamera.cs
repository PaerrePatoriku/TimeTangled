using Unity.Mathematics.Geometry;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Camera controlledCamera;
    [SerializeField]
    private Transform characterPivot;
    [SerializeField]
    private float mouseSensitivity = 5f;
    [SerializeField]
    float maxCameraPitch = 90f;
    [SerializeField]
    float minCameraPitch = -90f;

    InputAction _cameraMove;

    float currentPitch;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _cameraMove = InputSystem.actions.FindAction("Look");
        currentPitch = transform.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 _lastMouseMovement = _cameraMove.ReadValue<Vector2>();
        if (_lastMouseMovement != Vector2.zero)
       {


            float newPitch = currentPitch + ((-_lastMouseMovement.y * Time.deltaTime) * mouseSensitivity);
            newPitch = Mathf.Clamp(newPitch, minCameraPitch, maxCameraPitch);
            currentPitch = newPitch;
            Quaternion newPitchRotation = Quaternion.AngleAxis(currentPitch, Vector3.right);

            float currentYaw = characterPivot.rotation.eulerAngles.y;
            float newYaw = currentYaw + ((_lastMouseMovement.x * Time.deltaTime) * mouseSensitivity);
            transform.eulerAngles = new Vector3(newPitchRotation.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            characterPivot.eulerAngles = new Vector3(characterPivot.rotation.eulerAngles.x, newYaw, characterPivot.eulerAngles.z);
            //Debug.Log(currentPitch);
        }
        
    }
}
