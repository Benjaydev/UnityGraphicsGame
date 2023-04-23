using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;

    private UIScript ui;
    private PostProcessor postProcessor;
    [SerializeField]
    private SubmissionSidebar sidebar;

    [SerializeField]
    private Transform paperCameraLocation;

    [Header("Security Cameras")]
    [System.NonSerialized]
    public bool securityCameraAnomaly = false;
    private int securityAnomalyCount = 0;
    private bool securityCameraAnomalyHasHappened = false;
    [SerializeField]
    private TextMeshProUGUI containmentText;
    [SerializeField]
    private RobotScript securityDisplayRobot;
    [SerializeField]
    private Material securityCameraMaterial;
    [SerializeField]
    private Transform securityCameraLocation;
    [SerializeField]
    private Transform securityCameraAnomalyLocation;

    [Header("Robots")]
    [SerializeField]
    private MovingPlatform movingPlatform;

    [System.NonSerialized]
    public int lives = 3;
    [System.NonSerialized]
    public int score = 0;

    private void Awake()
    {
        instance = this;
        ui = GetComponentInChildren<UIScript>();
        postProcessor = GetComponent<PostProcessor>();
    }

    public void ActivateSecurityCamera(string animation)
    {
        if (!securityCameraAnomalyHasHappened)
        {

            if ((Random.Range(0, 5) == 0 && securityAnomalyCount > 2) || securityAnomalyCount == 8)
            {
                securityCameraAnomaly = true;
                securityCameraAnomalyHasHappened = true;

                securityDisplayRobot.PlayAnimation("Idle");
                MoveCameraTo(securityCameraAnomalyLocation);
                ui.ShowAnimationView();

                containmentText.text = "<color=red>BOO! *Scary Noise*</color>";
                containmentText.fontSize = 35;

                securityCameraMaterial.SetFloat("_PixelSize", 0.003f);
                securityCameraMaterial.SetFloat("_Movement", 4f);
                securityCameraMaterial.SetFloat("_Distortion", 10f);

                postProcessor.ApplyMaterial(securityCameraMaterial);
                return;
            }
            securityAnomalyCount++;
        }


        securityDisplayRobot.PlayAnimation(animation);
        MoveCameraTo(securityCameraLocation);
        ui.ShowAnimationView();

        containmentText.text = "Containment <color=red>~Redacted</color>";
        containmentText.fontSize = 15;

        securityCameraMaterial.SetFloat("_PixelSize", 0.0003f);
        securityCameraMaterial.SetFloat("_Movement", 1.5f);
        securityCameraMaterial.SetFloat("_Distortion", 15f);

        postProcessor.ApplyMaterial(securityCameraMaterial);
    }

    public void DeactivateSecurityCamera()
    {
        securityCameraAnomaly = false;
        MoveCameraTo(paperCameraLocation);
        ui.ShowPaperView();
        postProcessor.RemoveMaterial(securityCameraMaterial);
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

    public void PaintRobot()
    {
        movingPlatform.currentRobot.Paint(sidebar.GetIdSequence(), CombinationIDGenerator.Instance.GetId(sidebar.GetKeycode()));
    }


    public void SubmitRobot()
    {
        if (!movingPlatform.isMoving)
        {
            movingPlatform.DestroyRobot();

            StartCoroutine(SubmitRobotCo());
        }

    }
    IEnumerator SubmitRobotCo()
    {
        bool success = false;
        if (CombinationIDGenerator.Instance.GetWord(movingPlatform.currentRobot.typeSequence) == sidebar.GetKeycode())
        {
            string[] actualSequence = movingPlatform.currentRobot.idSequence;
            string[] chosenSequence = sidebar.GetIdSequence();
            if (actualSequence[0] == chosenSequence[0] && actualSequence[1] == chosenSequence[1] && actualSequence[2] == chosenSequence[2])
            {
                success = true;
            }
        }

        yield return new WaitForSeconds(8);

        if (success)
        {
            score++;
            ui.SetScore(score);
        }
        else
        {
            lives--;
            ui.SetLives(lives);
            if(lives <= 0)
            {
                Time.timeScale = 0;
                ui.ShowLoseScreen();
            }
        }
    }
}
