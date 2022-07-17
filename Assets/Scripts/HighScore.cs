using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI label;
    void Start()
    {
        int wave = PlayerPrefs.GetInt("wave", 0);
        if (wave > 0)
        {
            label.text = string.Format("Best run {0} waves", wave);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
