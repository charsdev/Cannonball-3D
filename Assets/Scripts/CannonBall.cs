using System.Collections;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float timeToDestroy;
    public float scaleMultiplier;
    public float height = 1f;
    public AnimationCurve curve;
    public float duration;
    public Vector3 targetPos;

    private void Start()
    {
        transform.localScale *= scaleMultiplier;
        StartCoroutine(CurvePath(transform.position, targetPos, duration));
        Destroy(gameObject, timeToDestroy);
    }

    public IEnumerator CurvePath(Vector3 start, Vector3 end, float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float elapsedTime = currentTime / duration;
            float currentHeight = curve.Evaluate(elapsedTime);
            transform.position = Vector3.Lerp(start, end, elapsedTime) + Vector3.up * Mathf.Lerp(0f, height, currentHeight);
            yield return null;
        }
    }

    public void Setup(float scale, Vector3 target, float newHeight, float time)
    {
        scaleMultiplier = scale;
        targetPos = target;
        height = newHeight;
        timeToDestroy = time;
    }
}

