using UnityEngine;
using System.Collections;
using System;

public class CameraSway : MonoBehaviour
{
    public float shakeDur = 300;
    public float shakeStrenght = 1;
    private Vector3 targetPos;
    private void Start()
    {
        StartCoroutine(Shake(shakeDur, shakeStrenght));
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-5f, 5f) * magnitude;
            float y = UnityEngine.Random.Range(-5f, 5f) * magnitude;

            targetPos = transform.position + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(0.2f);
        }
        transform.position = orignalPosition;
    }
}