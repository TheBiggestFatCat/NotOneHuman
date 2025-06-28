using System;
using UnityEngine;

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

    public void DefenderTakeDamage()
    {
        bool p1Attacker = gameStats.AttackerPlayerIndex == 0;
        if(p1Attacker)
        {
            ScoreManager.Instance.AddP1Score(1);
        }
        else
        {
            ScoreManager.Instance.AddP2Score(1);
        }
    }
    public void TimeOver()
    {

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