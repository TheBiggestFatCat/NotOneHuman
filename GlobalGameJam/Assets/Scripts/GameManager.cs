using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public GameStats gameStats;
    public PlayerData[] playerData;
    public GameObject judgePrefab;
    public Vector3 judgeStartPosition;
    public UnityEvent<int> OnGameOver;
    public int readyManCount = 12;
    public UnityEvent OnGameReady;
    public float gameStartDelay = 3f;

    public void GetReady()
    {
        readyManCount--;
        if(readyManCount <= 0)
        {
            OnGameReady?.Invoke();
            StartCoroutine(StartGame());
        }
        Debug.Log($"GetReady Man = {readyManCount}");
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(gameStartDelay);
        Instantiate(judgePrefab, judgeStartPosition, Quaternion.identity);
    }

    public void DefenderTakeDamage()
    {
        bool p1Attacker = gameStats.AttackerPlayerIndex == 0;
        if(p1Attacker)
        {
            ScoreManager.Instance.AddP1Score(1);
            OnGameOver?.Invoke(0);
        }
        else
        {
            ScoreManager.Instance.AddP2Score(1);
            OnGameOver?.Invoke(1);
        }
    }
    public void TimeOver()
    {
        bool p1Attacker = gameStats.AttackerPlayerIndex == 0;
        if(p1Attacker)
        {
            ScoreManager.Instance.AddP2Score(1);
            OnGameOver?.Invoke(1);
        }
        else
        {
            ScoreManager.Instance.AddP1Score(1);
            OnGameOver?.Invoke(0);
        }
    }

    public PlayerData GetPlayerData(int playerIndex)
    {
        bool playerIsAttacker = playerIndex == gameStats.AttackerPlayerIndex;

        for (int i = 0; i < playerData.Length; i ++)
        {
            int sceneIndex = playerData[i].SceneIndex;
            bool isAttacker = playerData[i].isAttacker;
            if(playerData[i].prefab != null && sceneIndex == gameStats.SceneIndex && isAttacker == playerIsAttacker)
            {
                return playerData[i];
            }
        }

        Debug.LogWarning($"No player data found for player index {playerIndex} in scene {gameStats.SceneIndex} with isAttacker = {playerIsAttacker}");
        return null;
    }
}



[Serializable]
public class GameStats
{
    public int AttackerPlayerIndex;
    public int SceneIndex;
}

[Serializable]
public class PlayerData
{
    public GameObject prefab;
    public Vector3 localPositionOffset;
    public bool isAttacker;
    public int SceneIndex;
}