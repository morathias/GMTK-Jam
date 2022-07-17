using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePopup : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

    public LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        text.text = $"YOU SURVIVED {levelManager.currentWave} WAVES";
        transform.DOScale(1, 1f).SetEase(Ease.OutElastic);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Replay()
    {
        SceneManager.LoadScene(1);
    }
}
