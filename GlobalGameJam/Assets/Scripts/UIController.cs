using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public GameObject gameNameText;

    void Start()
    {
        ShowStartMenu();
    }

    public void ShowStartMenu()
    {
        gameNameText.transform.DOScale(Vector3.one, 2f);
    }
}