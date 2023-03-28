using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player playerPrefab;
    public Platform platformPrefab;
    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;
    Player m_player;
    public CamController mainCam;
    public float powerBarUp;
    int m_score;
    bool is_gameStarted;

    public bool Is_gameStarted { get => is_gameStarted;  }

    public override void Awake()
    {
        MakeSingleton(false);
    }
    public override void Start()
    {
        GameGUIManager.Ins.UpdateScoreCouting(m_score);
      //  GameGUIManager.Ins.ShowgameGui(false);
        GameGUIManager.Ins.PowerBar(0, 1);
        AudioController.Ins.PlayBackgroundMusic();

    }
    public void Playgame()
    {
        StartCoroutine(PlatformInit());
        GameGUIManager.Ins.ShowgameGui(true);
    }
    IEnumerator PlatformInit()
    {
        Platform PlatformClone = null;
        if(platformPrefab)
        {
            PlatformClone = Instantiate(platformPrefab, new Vector2(0, Random.Range(minSpawnY, maxSpawnY)),Quaternion.identity);
            PlatformClone.id = PlatformClone.gameObject.GetInstanceID();
        }
        yield return new WaitForSeconds(0.5f);
        if (playerPrefab)
        {
            m_player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            m_player.lastPlatFormID = PlatformClone.id;
        }
        if (platformPrefab)
        {
            float spawnX = m_player.transform.position.x + minSpawnX;
            float spawnY = Random.Range(minSpawnY, maxSpawnY);
           Platform PlatformClone02 = Instantiate(platformPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);
            PlatformClone02.id = PlatformClone02.gameObject.GetInstanceID();
        }
        yield return new WaitForSeconds(0.5f);
        is_gameStarted = true;
    }
    public void CreatPlatform()
    {
        float spawnX = Random.Range(m_player.transform.position.x + minSpawnX, m_player.transform.position.x + maxSpawnX);
        float spawnY = Random.Range(minSpawnY, maxSpawnY);
        if (!m_player || !platformPrefab) return;
        Platform PlatformClone = Instantiate(platformPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);
        PlatformClone.id = PlatformClone.gameObject.GetInstanceID();
    }
    public void CreatePlatformAndLerp(float playerXPos)
    {
        if (mainCam)
        
            mainCam.LerpTrigger(playerXPos + minSpawnX);

        CreatPlatform();
    }
    public void AddScore()
    {
        m_score++;
        GameGUIManager.Ins.UpdateScoreCouting(m_score);
        Prefs.bestScore = m_score;
        AudioController.Ins.PlaySound(AudioController.Ins.getScore);
    }
}
