using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    private float intensityMin = 0.3f;

    [SerializeField]
    private float intensityMax = 0.5f;

    [SerializeField]
    private float intensityNormal = 1f;

    [SerializeField]
    private float cooldownMin = 3f;
    [SerializeField]
    private float cooldownMax = 5f;

    [SerializeField]
    private float durationMin = 0.3f;
    [SerializeField]
    private float durationMax = 1f;

    private Light lt;

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        yield return new WaitForSeconds(Random.Range(cooldownMin, cooldownMax));
        lt.intensity = Random.Range(intensityMin, intensityMax);
        yield return new WaitForSeconds(Random.Range(durationMin, durationMax));
        lt.intensity = intensityNormal;
        StartCoroutine(Flicker());
    }
}
