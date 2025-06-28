using UnityEngine;

public class Actor : MonoBehaviour
{
    public void InitObject(int playerIndex)
    {
        PlayerData playerData = GameManager.Instance.GetPlayerData(playerIndex);
        if(playerData != null)
        {
            var obj = Instantiate(playerData.prefab, transform);
            obj.transform.localPosition = playerData.localPositionOffset;
        }
    }    
}
