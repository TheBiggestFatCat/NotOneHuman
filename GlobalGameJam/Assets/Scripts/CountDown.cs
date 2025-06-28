using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI countDownText;
    private float countDownTime;
    private float timer;
    private bool isCountingDown;

    void Start()
    {
        GameManager.Instance.OnGameReady.AddListener(StartCountDown);
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
        }
    }
}
