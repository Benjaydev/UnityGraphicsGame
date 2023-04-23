using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTimedActivate : MonoBehaviour
{
    [SerializeField]
    private float cooldownMin = 10f;
    [SerializeField]
    private float cooldownMax = 30f;

    [SerializeField]
    private float durationMin = 5f;
    [SerializeField]
    private float durationMax = 15f;

    [SerializeField]
    private GameObject deactivationObject;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomActivation());
    }

    IEnumerator RandomActivation()
    {
        deactivationObject.SetActive(false);
        yield return new WaitForSeconds(Random.Range(cooldownMin, cooldownMax));
        deactivationObject.SetActive(true);
        yield return new WaitForSeconds(Random.Range(durationMin, durationMax));
        StartCoroutine(RandomActivation());
    }
}
