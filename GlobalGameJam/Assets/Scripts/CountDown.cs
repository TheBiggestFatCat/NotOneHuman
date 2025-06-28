using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI countDownText;
    private float countDownTime;
    private float timer;
    private bool isCountingDown;
    private bool LowTimeWarning;
    private int sfxCount = 10;
    private Coroutine coroutine;
    void Start()
    {
        GameManager.Instance.OnGameReady.AddListener(StartCountDown);
        GameManager.Instance.OnGameOver.AddListener(OnGameOver);
    }

    private void OnGameOver(int arg0)
    {
        isCountingDown = false;
        StopCoroutine(coroutine);
    }

    private void StartCountDown()
    {
        countDownTime = GameManager.Instance.gameStats.GameTime;
        timer = countDownTime;
        isCountingDown = true;        
    }

    void Update()
    {
        if(isCountingDown)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                GameManager.Instance.TimeOver();
            }
            countDownText.text = Mathf.CeilToInt(timer).ToString();

            if(!LowTimeWarning && timer <= 10)
            {
                LowTimeWarning = true;
                coroutine = StartCoroutine(LowTimeWarningSFX());
            }
        }
    }

    IEnumerator LowTimeWarningSFX()
    {
        while(sfxCount > 0)
        {
            AudioManager.Instance.PlaySFX(1);
            yield return new WaitForSeconds(1);
            sfxCount--;
        }        
    }
}
