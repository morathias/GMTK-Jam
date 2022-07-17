using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] HP PlayerHP;
    [SerializeField] Image hpBar;

    [SerializeField] TextMeshProUGUI wave;

    [SerializeField] LevelManager levelManager;

    void Start()
    {
        PlayerHP.OnDamage += UpdatePlayerHP;
        PlayerHP.OnDead += EndGame;
        levelManager.OnLevelUpStarted += UpdateWaveView;
        UpdateWaveView();
    }

    private void UpdateWaveView()
    {
        wave.text = "Wave " + (levelManager.currentWave +1);
    }

    private void UpdatePlayerHP()
    {
        hpBar.fillAmount = PlayerHP.currentHP / PlayerHP.maxHP;
    }

    private void EndGame()
    {
        PlayerPrefs.SetInt("wave", levelManager.currentWave + 1);
        SceneManager.LoadScene(0);
    }
}
