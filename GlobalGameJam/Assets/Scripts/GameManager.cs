using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
    }

    public UnityEvent OnGameReady;
    public UnityEvent<int> OnGameOver;

    public GameStats gameStats;
    public PlayerData[] playerData;
    public GameObject judgePrefab;
    public Vector3 judgeStartPosition;    
    public int readyManCount = 12;

    private Dictionary<int, Actor> actorList = new();

    public void GetReady()
    {
        readyManCount--;
        if(readyManCount <= 0)
        {
            OnGameReady?.Invoke();
        }
        Debug.Log($"GetReady Man = {readyManCount}");
    }

    public void StartGame()
    {
        Instantiate(judgePrefab, judgeStartPosition, Quaternion.identity);
    }

    public Actor CreateActor(int playerIndex,Transform parent)
    {
        var playerData = GetPlayerData(playerIndex);
        if(playerData == null)
        {
            Debug.LogError($"No player data found for player index {playerIndex}");
            return null;
        }
        var obj = Instantiate(playerData.prefab,parent);
        obj.transform.localPosition = playerData.localPositionOffset;
        Actor actor = obj.GetComponent<Actor>();
        actorList.Add(playerIndex, actor);
        return actor;
    }

    public Actor GetActor(int playerIndex)
    {
        if(actorList.TryGetValue(playerIndex, out Actor actor))
        {
            return actor;
        }
        //Debug.LogWarning($"No actor found for player index {playerIndex}");
        return null;
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

    public void GoToNextBattle()
    {
        SceneManager.LoadScene(0);
    }
}



[Serializable]
public class GameStats
{
    public int AttackerPlayerIndex;
    public int SceneIndex;
    public float GameTime = 60f;
}

[Serializable]
public class PlayerData
{
    public GameObject prefab;
    public Vector3 StartPosition;
    public Vector3 localPositionOffset;
    public bool isAttacker;
    public int SceneIndex;
}