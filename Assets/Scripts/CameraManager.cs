using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    public void Shake(float shakeDuration, float shakeAmount)
    {
        StartCoroutine(ShakeCoroutine(shakeDuration, shakeAmount));
    }

    IEnumerator ShakeCoroutine(float shakeDuration, float shakeAmount)
    {
        Vector3 originalPos = this.transform.position;

        while (shakeDuration > 0)
        {
            Vector3 shake = Random.insideUnitSphere * shakeAmount;
            shake.z = 0;

            this.transform.position = originalPos + shake;
            shakeDuration -= Time.deltaTime;

            yield return null;
        }

        this.transform.position = originalPos;
    }
}
