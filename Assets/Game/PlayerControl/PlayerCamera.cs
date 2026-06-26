using Game.GameLogic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.PlayerControl
{
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
            currentPitch = transform.eulerAngles.x;
        }

        Vector2 GetMouseInput()
        {
            Vector2 _lastMouseMovement = GameGlobals.instance.InGameInteractionActive ? _cameraMove.ReadValue<Vector2>() : Vector2.zero;
            return _lastMouseMovement;
        }
        // Update is called once per frame
        void Update()
        {
            var _lastMouseMovement = GetMouseInput();
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
}
