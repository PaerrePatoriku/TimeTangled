using Game.Physics;
using UnityEngine;

namespace Game.World.Interactable
{
    public class DraggableTestBlock : MonoBehaviour
    {
        [SerializeField]
        float dragSpeed = 3f;
        bool dragging = false;
        InteractionEvent _lastActionPrompt;
        SimpleKinematicMover targetMover;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

            targetMover = GetComponent<SimpleKinematicMover>();
        }
        void OnEnable()
        {
            dragging = false;
        }

        Plane getTargetPointPlanePosition()
        {
            Vector3 planeLocation = transform.position;
            Plane testingPlane = new Plane(Vector3.up, planeLocation);
            return testingPlane;
        }
        // Update is called once per frame
        void Update()
        {
            if (dragging)
            {
                Plane testPlane = getTargetPointPlanePosition();
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

                if (testPlane.Raycast(ray, out float enter))
                {
                    Vector3 point = ray.GetPoint(enter);
                    targetMover.MoveTowards(point, Space.World, true);

                }

            }
        }
        public void HandleDragObject(InteractionEvent @event)
        {
            if (@event.interactionEventType == InteractionEventType.InputPressed)
            {
                dragging = true;
                _lastActionPrompt = @event;
            }
            else if (@event.interactionEventType == InteractionEventType.InputReleased ||  @event.interactionEventType == InteractionEventType.InputCanceled)
            {
                dragging = false;
                _lastActionPrompt = null;
                targetMover.StopMoving();
            }
        }


    }
}
