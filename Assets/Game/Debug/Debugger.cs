using UnityEngine;

public class Debugger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        #if UNITY_EDITOR
        Application.targetFrameRate = 60;
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
