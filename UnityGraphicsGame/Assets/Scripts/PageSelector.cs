using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pages;

    [SerializeField]
    private GameObject backButton;
    [SerializeField]
    private GameObject forwardButton;


    private int currentPage = 0;

    private void Start()
    {
        backButton.SetActive(false);
        forwardButton.SetActive(true);
        pages[0].SetActive(true);
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            pages[currentPage].SetActive(false);
            currentPage++;
            pages[currentPage].SetActive(true);
            backButton.SetActive(true);
        }
        if (currentPage == pages.Length - 1)
        {
            forwardButton.SetActive(false);
        }
    }
    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            pages[currentPage].SetActive(false);
            currentPage--;
            pages[currentPage].SetActive(true);
            forwardButton.SetActive(true);
        }
        if (currentPage == 0)
        {
            backButton.SetActive(false);
        }
    }
}
