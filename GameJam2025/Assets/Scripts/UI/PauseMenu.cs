using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject canvasParent;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private List<GameObject> pages = new List<GameObject>();

    private int currentIndex = 0;
    private bool isPaused = false;

    private void Start()
    {
        InputsManagers.Instance.OnEscapeClick += OnEscapeClick;
        closeButton.onClick.AddListener(CloseMenu);
        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PreviousPage);

        canvasParent.SetActive(false);
        DeactivateAllPages();
    }

    private void OnDestroy()
    {
        InputsManagers.Instance.OnEscapeClick -= OnEscapeClick;
    }

    private void OnEscapeClick()
    {
        OpenMenu();
    }

    private void OpenMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;
        canvasParent.SetActive(true);

        Cursor.lockState = CursorLockMode.None; // trava o cursor no centro
        Cursor.visible = true;

        currentIndex = 0;
        ShowPage(currentIndex);
    }

    private void CloseMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        canvasParent.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked; // trava o cursor no centro
        Cursor.visible = false;

        DeactivateAllPages();
    }

    private void NextPage()
    {
        if (pages.Count == 0) return;
        currentIndex = (currentIndex + 1) % pages.Count;
        ShowPage(currentIndex);
    }

    private void PreviousPage()
    {
        if (pages.Count == 0) return;
        currentIndex = (currentIndex - 1 + pages.Count) % pages.Count;
        ShowPage(currentIndex);
    }

    private void ShowPage(int index)
    {
        DeactivateAllPages();
        if (index >= 0 && index < pages.Count)
        {
            pages[index].SetActive(true);
        }
    }

    private void DeactivateAllPages()
    {
        foreach (var page in pages)
        {
            if (page != null) page.SetActive(false);
        }
    }
}
