using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class Opening : MonoBehaviour
{
    public float delayTime;
    public GameObject ball;
    public Animation anim;

    private void Start()
    {
        GameManager.Instance.OnGameReady.AddListener(OnGameReady);
        ball.SetActive(false);
    }

    private void OnGameReady()
    {
        StartCoroutine(GameStart());
        ball.SetActive(true);
        anim.Play();
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(delayTime);
        GameManager.Instance.StartGame();
        ball.SetActive(false);
    }
}
