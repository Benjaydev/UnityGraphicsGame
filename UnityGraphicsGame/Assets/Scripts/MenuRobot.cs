using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRobot : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private string[] names = new string[]
    {
        "Walk",
        "DanceRegular",
        "Grenade",
        "Run",
        "DanceFunny",
        "Punch",
        "Golf",
        "HeadSpin",
        "Backflip"
    };

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Twitch());
    }

    IEnumerator Twitch()
    {
        yield return new WaitForSeconds(Random.Range(3, 10));
        animator.SetTrigger(names[Random.Range(0, names.Length)]);
        yield return new WaitForSeconds(Random.Range(0.3f, 1f));
        animator.SetTrigger("Idle");
        StartCoroutine(Twitch());
    }
}
