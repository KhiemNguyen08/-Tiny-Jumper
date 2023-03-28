using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverDialog : Dialog
{
    public Text bestScoreText;
    bool m_replayBtnClick;
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (bestScoreText)
            bestScoreText.text = Prefs.bestScore.ToString();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    public void Replay()
    {
        m_replayBtnClick = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void BackHome()
    {
        GameGUIManager.Ins.ShowgameGui(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnSceneLoaded(Scene scene , LoadSceneMode mode)
    {
        if (m_replayBtnClick)
        {
            GameGUIManager.Ins.ShowgameGui(true);
        GameManager.Ins.Playgame();

        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
}
