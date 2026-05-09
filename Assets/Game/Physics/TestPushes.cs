using UnityEngine;

public class TestPushes : MonoBehaviour
{
    [SerializeField]
    GameObject pushTarget;
  
    SimpleKinematicMover targetMover;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            targetMover.MoveTowards(transform.TransformDirection(Vector3.forward));
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetMover = pushTarget.GetComponent<SimpleKinematicMover>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
