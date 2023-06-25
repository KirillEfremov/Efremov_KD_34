using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MatchManager : MonoBehaviour
{
    public TMP_Text[] healthPlayers;
    public GameObject FinalMathUI;
    public Text FinalText;

    void Update()
    {
        int healthPlayer1 = int.Parse(healthPlayers[0].text);
        int healthPlayer2 = int.Parse(healthPlayers[1].text);
        if (healthPlayer1 <= 0 && healthPlayer2 <= 0)
        {
            FinalMathUI.SetActive(true);
            FinalText.text = "Ничья";
        }
        else if (healthPlayer1 <= 0)
        {
            FinalMathUI.SetActive(true);
            FinalText.text = "Победитель: Игрок 2";
        }
        else if (healthPlayer2 <= 0)
        {
            FinalMathUI.SetActive(true);
            FinalText.text = "Победитель: Игрок 1";
        }
    }

    public void OnMenu()
    {
        SceneManager.LoadScene("StartGame");
    }
}
