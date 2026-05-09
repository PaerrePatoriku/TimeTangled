
using UnityEditor;

using UnityEngine;


public class SimpleKinematicMover : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider col;

    [SerializeField]
    float baseMovementSpeed = 0.2f;
    [SerializeField]
    float stoppingDistance = 0.1f;
    [SerializeField]
    LayerMask movementBlockingLayers;

    private float maxDistance;
    bool isMoving = false;
    float usedMovementSpeed = 0;
    Vector3 targetPosition;

    SimpleKinematicMoverGizmoDrawContext drawContext;

    void Start()
    {
        drawContext = new SimpleKinematicMoverGizmoDrawContext();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; //Jos on pois p��lt�.
        col = GetComponent<BoxCollider>();
        maxDistance = GameGlobals.instance.MaximumInteractionDistance;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 movementVector = (targetPosition - transform.position);
            float distanceToTarget = movementVector.magnitude;
            Vector3 movementStep = movementVector.normalized * (usedMovementSpeed * Time.deltaTime);
            //Debug.Log("REMAINING DISTANCE" + distanceToTarget);
            if (!_canMoveTo(transform.position + movementStep, movementBlockerLayers: movementBlockingLayers))
            {
                StopMoving();
                return;
            }
 

            if (distanceToTarget <= stoppingDistance)
            {
                Debug.Log("moving to " + targetPosition.ToString());
                //rb.MovePosition(targetPosition);
                rb.position = targetPosition;
                StopMoving();
            }
            else
            {
                rb.MovePosition(transform.position + movementStep);
            }
        }
    }
    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (EditorApplication.isPlaying && drawContext.active)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(drawContext.drawPosition, drawContext.cubeDrawExtent);
        }
    }
    #endif
    //Voidaan liikutella vain yhdellä voimalla kerrallaan (esim 1:stä pelaajasta kerrallaan)
    public void MoveTowards(Vector3 position, Space movementSpace = Space.Self, bool overrideMode = false)
    {
        if (!isMoving || overrideMode)
        {
            if (overrideMode)
                StopMoving();
            targetPosition = (movementSpace == Space.World) ? position : transform.position + position;
            _startMoving();  
        }
    }
    bool _canMoveTo(Vector3 target, float colliderPadding = 0.005f, LayerMask movementBlockerLayers = default)
    {
        drawContext.active = true;
        Vector3 colliderHalfExtents = col.bounds.extents - (Vector3.one * colliderPadding);
        drawContext.cubeDrawExtent = colliderHalfExtents * 2f;
        drawContext.drawPosition = col.center + target;
        Quaternion rot = transform.rotation;
        Collider[] allHits = Physics.OverlapBox(col.center + target,
            colliderHalfExtents,
            rot,
            movementBlockerLayers, 
            QueryTriggerInteraction.Ignore);
        foreach (var collider in allHits)
        {
            if (collider.gameObject != gameObject)
            {
                Debug.Log("movement blocked by" + collider.name);
                return false;
                
            }
                
        }
        return true;


    }
    void _startMoving(float speed = 0f)
    {
        float minDragPowerMultiplier = 0.8f;
        float maxDragPowerMultiplier = 6f;
        float maxDistanceMultiplier = maxDistance;
        float distanceBetween = Vector3.Distance(transform.position, targetPosition);
        
        float normalizedDistanceBetween = distanceBetween / maxDistanceMultiplier;
        
        normalizedDistanceBetween = minDragPowerMultiplier + ((maxDragPowerMultiplier - minDragPowerMultiplier) * Mathf.Clamp(normalizedDistanceBetween, 0f, 1f));
        
        speed = (speed != 0) ? speed : baseMovementSpeed;
        speed *= normalizedDistanceBetween;
        usedMovementSpeed = speed;
        isMoving = true;
    }
    public void StopMoving()
    {
        usedMovementSpeed = baseMovementSpeed;
        isMoving = false;
        //drawContext.active = false;
    }
}
class SimpleKinematicMoverGizmoDrawContext
{
    public bool active;
    public Vector3 cubeDrawExtent;
    public Vector3 drawPosition;
}