using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mainView;
    [SerializeField]
    private GameObject paperView;
    [SerializeField]
    private GameObject animationView;
    [SerializeField]
    private GameObject loseScreen;
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private Image[] livesImages;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
    }
    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

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

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
        mainView.SetActive(false);
        paperView.SetActive(false);
        animationView.SetActive(false);
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
    public void SetLives(int lives)
    {
        for (int i = 0; i < livesImages.Length; i++)
        {
            if (i < lives)
            {
                livesImages[i].color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                livesImages[i].color = Color.red;
            }
        }
    }
}
