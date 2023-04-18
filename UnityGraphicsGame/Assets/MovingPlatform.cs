using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [SerializeField]
    private float speed = 100f;

    [SerializeField]
    private Transform robotSpawnLocation;
    [SerializeField]
    private Transform robotStopLocation; 
    [SerializeField]
    private Transform robotEndLocation;

    [SerializeField]
    private GameObject robotPrefab;

    [NonSerialized]
    public RobotScript currentRobot;

    [SerializeField]
    private ParticleSystem[] sprayParticles;


    private bool isMoving = false;
    private bool destroyRobot = false;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        SpawnNewRobot();
    }

    public void PlayCurrentRobotAnimation(int id)
    {
        currentRobot.PlayAnimation(id);
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector3 diff = robotStopLocation.position - robotSpawnLocation.position;
            float mag = diff.magnitude;
            currentRobot.transform.position += (diff/ mag) * Time.deltaTime;

            // Robot moving to destroy
            if (destroyRobot)
            {
                float dist = (currentRobot.transform.position - robotEndLocation.position).sqrMagnitude;
                if (dist <= 0.1f * 0.1f)
                {
                    SpawnNewRobot();
                }
            }
            // Robot moving to center
            else
            {
                float dist = (currentRobot.transform.position - robotStopLocation.position).sqrMagnitude;
                if (dist <= 0.1f * 0.1f)
                {
                    StopMoving();
                }
            }

        }
    }

    public void SpawnNewRobot()
    {
        if (currentRobot != null)
        {
            Destroy(currentRobot.gameObject);
            currentRobot = null;
            destroyRobot = false;
        }

        currentRobot = Instantiate(robotPrefab).GetComponent<RobotScript>();
        currentRobot.sprayParticles = sprayParticles;
        currentRobot.transform.position = robotSpawnLocation.position;
        StartMoving();
    }
    public void DestroyRobot()
    {
        destroyRobot = true;
        currentRobot.PlayAnimation("Idle");
        StartMoving();
    }

    public void StartMoving()
    {
        isMoving = true;
        meshRenderer.material.SetFloat("_MoveSpeedMult", speed);
    }
    public void StopMoving()
    {
        isMoving = false;
        meshRenderer.material.SetFloat("_MoveSpeedMult", 0f);
    }
}
