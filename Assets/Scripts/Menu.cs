using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] Button PlayButton;
    [SerializeField] Button CreditsButton;
    [SerializeField] Button ExitButton;
    [SerializeField] GameObject CreditsView;

    void Start()
    {
        PlayButton.onClick.AddListener(StartGame);
        CreditsButton.onClick.AddListener(Credits);
        ExitButton.onClick.AddListener(Exit);
    }

    private void StartGame()
    {
        PlayButton.onClick.RemoveListener(StartGame);
        CreditsButton.onClick.RemoveListener(Credits);
        ExitButton.onClick.RemoveListener(Exit);
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        CreditsView.SetActive(!CreditsView.activeInHierarchy);
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    private void Exit()
    {
        Application.Quit();
    }




}
