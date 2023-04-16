using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCameraToTimed(Transform location)
    {
        StopAllCoroutines();
        StartCoroutine(MoveCameraToTimedCo(location));
    }
    public void MoveCameraTo(Transform location)
    {
        transform.position = location.position;
        transform.rotation = location.rotation;
    }

    IEnumerator MoveCameraToTimedCo(Transform location)
    {
        Vector3 lastPosition = transform.position;
        Quaternion lastRotation = transform.rotation;

        transform.position = location.position;
        transform.rotation = location.rotation;

        yield return new WaitForSeconds(5);

        transform.position = lastPosition;
        transform.rotation = lastRotation;


    }
}
