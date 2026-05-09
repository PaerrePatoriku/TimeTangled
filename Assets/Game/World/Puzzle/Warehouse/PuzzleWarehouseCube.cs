using System.Collections;
using UnityEngine;

public class PuzzleWarehouseCube : MonoBehaviour
{
    [SerializeField]
    public string cubeID;
    [SerializeField]
    bool editorBoolean_RunAnim;
    [SerializeField]
    Vector3 localTransformOffset;
    [SerializeField]
    GameObject piston;
    Vector3 defaultPosition;
    [SerializeField]
    float pistonAnimationTime = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        defaultPosition = piston.transform.localPosition;
    }
    void PlayAnimation()
    {
        StartCoroutine(AnimationCoroutine(localTransformOffset));
    }
    IEnumerator AnimationCoroutine(Vector3 targetPosition)
    {
        piston.gameObject.SetActive(true);
        float currentTime = 0;
        Vector3 currentPosition = defaultPosition;
        while (currentTime < pistonAnimationTime)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / pistonAnimationTime;
            piston.transform.localPosition = Vector3.Lerp(currentPosition, targetPosition, t);
            yield return null;
        }
        piston.transform.localPosition = targetPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if (editorBoolean_RunAnim)
        {
            PlayAnimation();
            editorBoolean_RunAnim = false;
        }
    }
}
