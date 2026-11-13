using System.Collections;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private GameObject light;

    [SerializeField] private float currentSize = 2.5f;
    [SerializeField] private float targetSize = 14;
    private float lerpSize;
    [SerializeField] private float t;
    private float timePassed;

    private void Awake()
    {

    }

    public void Dim()
    {
        currentSize = light.transform.localScale.x;
        targetSize = 2.5f;
        StartCoroutine(LerpLight());
    }

    public void Flare()
    {
        currentSize = light.transform.localScale.x;
        targetSize = 14;
        StartCoroutine(LerpLight());
    }

    private IEnumerator LerpLight()
    {
        yield return new WaitForSeconds(0.5f);
        t = 0;
        timePassed = 0;
        lerpSize = currentSize;
        while (light.transform.localScale.x != targetSize)
        {
            timePassed += Time.deltaTime;
            t = timePassed / 2f;

            lerpSize = Mathf.Lerp(currentSize, targetSize, t);
            light.transform.localScale = new Vector3(lerpSize, lerpSize, lerpSize);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
        }
    }

}
