using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mainView;
    [SerializeField]
    private GameObject paperView;
    [SerializeField]
    private GameObject animationView;

    public void ShowMainView()
    {
        mainView.SetActive(true);
        paperView.SetActive(false);
        animationView.SetActive(false);
    }

    public void ShowPaperView()
    {
        mainView.SetActive(false);
        paperView.SetActive(true);
        animationView.SetActive(false);
    }

    public void ShowAnimationView()
    {
        mainView.SetActive(false);
        paperView.SetActive(false);
        animationView.SetActive(true);
    }
}
