using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimation : MonoBehaviour
{
    #region Small Flickering
    public Light light;
    public float minIntensity = 5f;
    public float maxIntensity = 8f;
    
    [Range(1, 50)]
    public int smoothing = 5;

    Queue<float> q;
    float sum = 0;

    void Awake()
    {
        q = new Queue<float>(smoothing);
        if (light == null)
        {
            light = GetComponent<Light>();
        }
    }

    void Update()
    {
        if (doingBlackout) return;

        while (q.Count >= smoothing)
        {
            sum -= q.Dequeue();
        }

        float rnd = Random.Range(minIntensity, maxIntensity);

        q.Enqueue(rnd);
        sum += rnd;

        float smoothAvg = sum / q.Count;
        light.intensity = smoothAvg;
    }
    #endregion

    #region Blackout
    public float minTimeBtwnBlackouts = 50f;
    public float maxTimeBtwnBlackouts = 200f;

    public float blackoutTime = 1f;

    bool doingBlackout = false;
    

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(minTimeBtwnBlackouts, maxTimeBtwnBlackouts));
        StartCoroutine(Blackout());
        StartCoroutine(Start());
    }

    IEnumerator Blackout()
    {
        doingBlackout = true;

        float totalTimeWaited = 0f;
        while(totalTimeWaited < blackoutTime)
        {
            float rndWait = Random.Range(0.05f, 0.2f);
            totalTimeWaited += rndWait;
            if(light.intensity < minIntensity)
            {
                light.intensity = maxIntensity;
            }
            else
            {
                light.intensity = 0.1f;
            }
            yield return new WaitForSeconds(rndWait);
        }

        doingBlackout = false;
    }

    #endregion
}
