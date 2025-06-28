using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public GameObject gameNameText;
    public GameObject MenuUI;
    public GameObject InGameUI;
    public GameObject Player1UI;
    public GameObject Player2UI;

    void Start()
    {
        ShowStartText();
        GameManager.Instance.OnGameReady.AddListener(HideMenuUI);
    }

    void Update()
    {
        if (GameManager.Instance.GetActor(0) != null)
        {
            ShowPlayer1UI();
        }
        else
        {
            return;
        }
        if (GameManager.Instance.GetActor(1) != null)
        {
            ShowPlayer2UI();
        }
        else
        {
            return;
        }
    }

    public void ShowStartText()
    {
        gameNameText.transform.DOScale(Vector3.one, 2f);
    }

    public void HideMenuUI()
    {
        MenuUI.transform.DOScale(Vector3.zero, 0.5f);
        Debug.Log("OnGameReady");
    }

    public void ShowInGameUI()
    {
        InGameUI.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
    }

    public void ShowPlayer1UI()
    {
        Player1UI.transform.DOScale(Vector3.one, 0.5f);
    }
    public void ShowPlayer2UI()
    {
        Player2UI.transform.DOScale(Vector3.one, 0.5f);
    }
}