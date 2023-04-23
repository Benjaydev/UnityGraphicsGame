using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    private static Dictionary<string, string[]> allAnimations = new Dictionary<string, string[]>() {
        { "Walk", 
            new string[2] {"Walking", "1" } },
        { "DanceRegular", 
            new string[2] {"Dancing", "2"} },
        { "Grenade", 
            new string[2] { "Throwing", "3"} },
        { "Run", 
            new string[2] { "Running", "1" } },
        { "DanceFunny",
            new string[2] { "Grooving", "2"} },
        { "Punch", 
            new string[2] { "Fighting", "3"} },
        { "Golf", 
            new string[2] { "Golfing", "1" } },
        { "HeadSpin", 
            new string[2] { "Spinning", "2"} },
        { "Backflip", 
            new string[2] { "Acrobatics", "3"} }
    };

    private string[] animations = new string[3];

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private SkinnedMeshRenderer mRenderer;

    [SerializeField]
    private bool shouldFacePlayer = true;

    public string robotName;
    public string[] idSequence = new string[3];
    public string typeSequence = "";

    [System.NonSerialized]
    public ParticleSystem[] sprayParticles;


    private void Start()
    {
        List<string> keys = Enumerable.ToList(allAnimations.Keys);
        for (int i = 0; i < animations.Length; i++)
        {
            while (animations[i] == null)
            {
                int index = Random.Range(0, keys.Count);
                string randomItem = keys[index];
                if (!animations.Contains(randomItem))
                {
                    // Get id
                    idSequence[i] = index.ToString();
                    // Get category
                    typeSequence += allAnimations[randomItem][1].ToString();
                    animations[i] = randomItem;
                }

            }

        }

        robotName = "Robot of " + allAnimations[animations[0]][0] + ", " + allAnimations[animations[1]][0] + ", and " + allAnimations[animations[2]][0];
    }

    private void Update()
    {
        if (shouldFacePlayer)
        {
            transform.LookAt(PlayerScript.instance.transform);
        }
  
    }

    public void PlayAnimation(int id)
    {
        anim.SetTrigger(animations[id]);
    }
    public void PlayAnimation(string id)
    {
        anim.SetTrigger(id);
    }

    public void Paint(string[] id, string type)
    {
        Color mainCol = Color.white;
        
        if(type != null)
        {
            // For each number in type sequence, set the rgb components
            for (int i = 0; i < 3; i++)
            {
                switch (type[i])
                {
                    case '1':
                        mainCol[i] = (1f / 3f);
                        break;
                    case '2':
                        mainCol[i] = (2f / 3f);
                        break;
                    case '3':
                        mainCol[i] = 1f;
                        break;

                }
            }
        }
        

        float[] parameters = new float[3];

        int idVal1 = 0;
        int idVal2 = 0;
        int idVal3 = 0;

        int.TryParse(id[0], out idVal1);
        int.TryParse(id[1], out idVal2);
        int.TryParse(id[2], out idVal3);


        // Smoothness
        parameters[0] = (float)idVal1 / 8;
        // Metallic
        parameters[1] = (float)idVal2 / 8;
        // Rim Power
        parameters[2] = Mathf.Max(0.5f, (8 - (float)idVal2));

        Color rimCol = new Color((float)idVal1 / 8, (float)idVal2 / 8, (float)idVal3 / 8);

        mRenderer.material.SetColor("_Color", mainCol);
        mRenderer.material.SetColor("_RimColor", rimCol);

        mRenderer.material.SetFloat("_Glossiness", parameters[0]);
        mRenderer.material.SetFloat("_Metallic", parameters[1]);
        mRenderer.material.SetFloat("_RimPower", parameters[2]);


        ParticleSystem.MainModule main1 = sprayParticles[0].main;
        ParticleSystem.MainModule main2 = sprayParticles[1].main;
        main1.startColor = mainCol;
        main2.startColor = rimCol;
        sprayParticles[0].Play();
        sprayParticles[1].Play();

    }
}
