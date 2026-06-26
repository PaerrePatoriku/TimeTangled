using Game.Internal.PhysicsHelpers;
using Game.Physics;
using Game.World.Interactable;
using UnityEngine;

namespace Game.World.Puzzle
{
    public class PushableTestBlock : MonoBehaviour
    {
        [SerializeField]
        GameObject pushTarget;
        SimpleKinematicMover targetMover;



        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            targetMover = pushTarget.GetComponent<SimpleKinematicMover>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void HandlePushItem(InteractionEvent @event)
        {
            UnityEngine.Debug.Log("This is from iside the handler," + @event.player);
            targetMover.MoveTowards(transform.TransformDirection(VectorAxis.GetRelativeForwardVector2D(@event.player.transform.position, transform.position)));
            UnityEngine.Debug.DrawRay(transform.position, VectorAxis.GetRelativeForwardVector2D(@event.player.transform.position, transform.position), Color.blue, 10);
        }
    }
}
