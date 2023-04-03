using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [SerializeField]
    private float speed = 100f;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartMoving();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            StopMoving();
        }
    }

    public void StartMoving()
    {
        meshRenderer.material.SetFloat("_MoveSpeedMult", speed);
    }
    public void StopMoving()
    {
        meshRenderer.material.SetFloat("_MoveSpeedMult", 0f);
    }
}
