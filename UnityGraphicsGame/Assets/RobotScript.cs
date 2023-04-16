using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    private Dictionary<string, string[]> allAnimations = new Dictionary<string, string[]>() {
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

    public string robotName;
    public string[] colourSequence = new string[3];
    public string[] particleSequence = new string[3];

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
                    colourSequence[i] = index.ToString();
                    // Get category
                    particleSequence[i] = allAnimations[randomItem][1].ToString();
                    animations[i] = randomItem;
                }

            }

        }

        robotName = "Robot of " + allAnimations[animations[0]][0] + ", " + allAnimations[animations[1]][0] + ", and " + allAnimations[animations[2]][0];

        Debug.Log(colourSequence[0] + " " + colourSequence[1] + " " + colourSequence[2]);
        Debug.Log(particleSequence[0] + " " + particleSequence[1] + " " + particleSequence[2]);
        Debug.Log(CombinationIDGenerator.Instance.GetWord(particleSequence));
        Debug.Log(robotName);
    }
    public void PlayAnimation(int id)
    {
        anim.SetTrigger(animations[id]);
    }
    public void PlayAnimation(string id)
    {
        anim.SetTrigger(id);
    }
}
