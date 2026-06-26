using Game.GameLogic;
using Game.Internal.PhysicsHelpers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.PlayerControl
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float speedModifier = 15f;
        [SerializeField]
        private float sprintMultiplier = 1.5f;
        [SerializeField]
        private float jumpImpulseModifier = 2f;
        [SerializeField]
        private float gravityModifier = -20f;

        Vector3 velocity;

        private CharacterController characterController;

        GroundDetection<Player> groundDetection;
        [SerializeField]
        LayerMask detectionLayerMask;

        InputAction moveAction;
        InputAction jumpAction;
        InputAction sprintAction;

        Vector3 currentMovementVector;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            characterController = GetComponent<CharacterController>();

            sprintAction = InputSystem.actions.FindAction("Sprint");
            jumpAction = InputSystem.actions.FindAction("Jump");
            moveAction = InputSystem.actions.FindAction("Move");



            groundDetection = new GroundDetection<Player>(this, Vector3.down * (characterController.bounds.extents.y), 0.15f, detectionLayerMask);
        }
        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 100, 200), "Velocity: " + currentMovementVector.ToShortString());
        }
        // Update is called once per frame
        void Update()
        {
            groundDetection.CastDetection();
            MovePlayer();
        }
        void MovePlayer()
        {
            Vector3 movementVector = new Vector3(0, 0, 0);
            Vector3 inputVector = Vector3.zero;

            inputVector = HandleMoveVector();
            if (groundDetection.isGrounded())
            {
                velocity.y = 0;
                if (jumpAction.triggered)
                {
                    velocity.y = Mathf.Sqrt(jumpImpulseModifier * -2 * gravityModifier);

                }
            }
            else
                velocity.y += gravityModifier * Time.fixedDeltaTime;



            movementVector = new Vector3(movementVector.x + inputVector.x,velocity.y, movementVector.z + inputVector.z);

            characterController.Move(movementVector * Time.deltaTime);
            currentMovementVector = movementVector * Time.deltaTime;
        }

        private Vector3 GetInputVector()
        {
        
            Vector2 movementVector = GameGlobals.instance.InGameInteractionActive ? moveAction.ReadValue<Vector2>() : Vector2.zero;
            Vector3 movementVector3D = new Vector3(movementVector.x, 0, movementVector.y);
            Vector3 directionalMovementVector = (movementVector3D).normalized;
            return new Vector3(directionalMovementVector.x, 0, directionalMovementVector.z);
        }
        private Vector3 HandleMoveVector()
        {
            float speedMultiplier = sprintAction.IsPressed() ? speedModifier * sprintMultiplier : speedModifier;
            Vector3 vel = transform.TransformDirection(GetInputVector()) * speedMultiplier;
            return vel;
        }
    }
}
