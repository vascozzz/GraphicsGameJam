using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance = null;

    private Camera camera;

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

    void Start()
    {
        camera = GetComponent<Camera>();
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

    public void ZoomOnTarget(Vector3 target, float speed)
    {
        StartCoroutine(ZoomCoroutine(target, speed));
    }

    IEnumerator ZoomCoroutine(Vector3 target, float speed)
    {
        target.z = transform.position.z;

        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 8f, Time.deltaTime * speed);

            yield return null;
        }
    }
}
