using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubmissionSidebar : MonoBehaviour
{
    [SerializeField]
    private Animator animator;


    [SerializeField]
    private TextMeshProUGUI[] idDisplays = new TextMeshProUGUI[3];

    [SerializeField]
    private string clearCharacter = "-";

    [SerializeField]
    private TMP_Dropdown dropdown;

    private void Awake()
    {
        animator.keepAnimatorControllerStateOnDisable = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (TextMeshProUGUI disp in idDisplays)
        {
            disp.text = clearCharacter;
        }

        List<string> values = Enumerable.ToList(CombinationIDGenerator.Instance.possibilities.Values);
        values.Sort();
        values.Insert(0, clearCharacter);
        dropdown.AddOptions(values);
    }

    public void SetAnimation(bool state)
    {
        if (canToggleAnimation)
        {
            animator.SetBool("Open", state);
        }
    }
    public void ToggleAnimation()
    {
        if (canToggleAnimation)
        {
            animator.SetBool("Open", !animator.GetBool("Open"));
        }
    }

    public void OnChange()
    {
        PlayerScript.instance.PaintRobot();
    }


    // Stop animations from being called for small duration
    private bool canToggleAnimation = true;
    public void SetAnimationOverride()
    {
        StartCoroutine(SetAnimationOverrideCo());
    }

    public void ToggleAnimationOverride(bool state)
    {
        canToggleAnimation = state;
    }

    IEnumerator SetAnimationOverrideCo()
    {
        canToggleAnimation = false;
        yield return new WaitForSeconds(1);
        canToggleAnimation = true;
    }

    public void IdButtonPress(int id)
    {

        foreach (TextMeshProUGUI disp in idDisplays)
        {
            // Clear
            if(id == -1)
            {
                disp.text = clearCharacter;
                continue;
            }

            // Text box is next empty
            if(disp.text == clearCharacter)
            {
                disp.text = id.ToString();
                OnChange();
                return;
            }
        }

        if(id == -1)
        {
            OnChange();
        }
    }

    public string[] GetIdSequence()
    {
        string[] sequence = new string[idDisplays.Length];
        for(int i = 0; i < idDisplays.Length; i++)
        {
            sequence[i] = idDisplays[i].text.ToString();
        }
        return sequence;
    }
    public string GetKeycode()
    {
        return dropdown.options[dropdown.value].text;
    }
}
