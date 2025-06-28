using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActorAnimationController: MonoBehaviour
{
    public Animator animator;
    public int blinkChance = 10; // 眨眼的概率
    public int lookChance = 10; // 左右看的概率

    void Start()
    {
        StartCoroutine(Idle());
        transform.parent.GetComponent<PlayerController>().PlayerMove.AddListener(SetMove);
    }
    
    // 待机动画 随机事件眨眼和左右看
    public IEnumerator Idle()
    {
        while (true)
        {
            int blinkRand = Random.Range(0, 100);
            // 随机眨眼
            if (blinkRand < blinkChance) // 10% 概率眨眼
            {
                animator.SetTrigger("blink");
            }

            int lookRand = Random.Range(0, 100);

            // 随机左右看
            if (lookRand < lookChance) // 10% 概率左右看
            {
                animator.SetTrigger("evil");
            }

            yield return new WaitForSeconds(1f); // 每隔1秒检查一次
        }
    }

    public void SetMove(Vector2 move)
    {
        Debug.Log($"ActorAnimationController SetMove: {move}");
        animator.SetBool("isMoving", true);
    }
}