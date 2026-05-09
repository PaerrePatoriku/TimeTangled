using Assets.Game.Internal.PhysicsHelpers;
using Assets.Game.World.Interactable;
using UnityEngine;

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
        Debug.Log("This is from iside the handler," + @event.player);
        targetMover.MoveTowards(transform.TransformDirection(VectorAxis.GetRelativeForwardVector2D(@event.player.transform.position, transform.position)));
        Debug.DrawRay(transform.position, VectorAxis.GetRelativeForwardVector2D(@event.player.transform.position, transform.position), Color.blue, 10);
    }
}
