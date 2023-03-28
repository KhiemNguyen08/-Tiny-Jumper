using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGUI;
    public GameObject gameGUI;
    public Text scoreCountingText;
    public Image powerBar;
    public Dialog achieventDialog;
    public Dialog helpDialog;
    public Dialog gameoverDialog;
    public override void Awake()
    {
        MakeSingleton(false);
    }
    public void ShowgameGui(bool isshow)
    {
        if (gameGUI)
        {
            gameGUI.SetActive(isshow);
        }
        if (homeGUI)
        {
            homeGUI.SetActive(!isshow);
        }
    }
    public void UpdateScoreCouting(int score)
    {
        if (scoreCountingText)
        {
            scoreCountingText.text = score.ToString();
        }
    }
    public void PowerBar(float curVal , float totalVal)
    {
        if (powerBar)
        {
            powerBar.fillAmount = curVal / totalVal;
        }
    }
    public void AchieventDialog()
    {
        if (achieventDialog)
            achieventDialog.Show(true);
    }
    public void HelpDialog()
    {
        if (helpDialog)
            helpDialog.Show(true);
    }
    public void GameoverDialog()
    {
        if (gameoverDialog)
            gameoverDialog.Show(true);
    }
}
