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
    [SerializeField] Animator hpBarAnim;

    [SerializeField] TextMeshProUGUI[] waveTexts;

    [SerializeField] Animator enemyRemAnim;
    [SerializeField] TextMeshProUGUI enemiesRemaining;

    [SerializeField] LevelManager levelManager;

    public Player player;
    
    int currentTotalEnemies;

    public LosePopup losePopup;
    
    void Start()
    {
        PlayerHP.OnDamage += UpdatePlayerHP;
        PlayerHP.OnChange += UpdatePlayerHP;
        PlayerHP.OnDead += EndGame;
        levelManager.OnLevelUpStarted += OnLevelStart;
        levelManager.OnEnemiesSpawn += OnEnemiesSpawn;
        UpdateView();
    }

    private void OnEnemiesSpawn()
    {
        currentTotalEnemies = levelManager.allEnemies.Count;
        enemiesRemaining.text = currentTotalEnemies.ToString();
        foreach (var item in levelManager.allEnemies)
        {
            item.GetComponent<HP>().OnDead += OnEnemyDead;
        }
    }

    private void OnLevelStart()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        foreach (var item in waveTexts)
        {
            item.text = "Wave " + (levelManager.currentWave + 1);
        }
    }

    private void OnEnemyDead()
    {
        currentTotalEnemies--;
        enemiesRemaining.text = currentTotalEnemies.ToString();
        enemyRemAnim.SetTrigger("count");
    }

    private void UpdatePlayerHP()
    {
        hpBarAnim.SetTrigger("hit");
        hpBar.fillAmount = PlayerHP.currentHP / PlayerHP.maxHP;
    }

    private void EndGame()
    {
        PlayerPrefs.SetInt("wave", levelManager.currentWave + 1);
        player.Block(true);
        player.gameObject.SetActive(false);
        player.weaponSwitcher.currentIWeapon.enabled = false;
        losePopup.gameObject.SetActive(true);
    }
}
