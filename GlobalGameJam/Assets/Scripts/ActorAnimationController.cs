using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActorAnimationController : MonoBehaviour
{
    public Animator animator;
    public int blinkChance = 10; // 眨眼的概率
    public int lookChance = 10; // 左右看的概率

    void Start()
    {
        StartCoroutine(SetIdle());
        transform.parent.GetComponent<PlayerController>().PlayerMove.AddListener(SetMove);
        transform.GetComponent<Actor>().PlayerAttack.AddListener(SetAttack);
        transform.GetComponent<Actor>().OnTakeDamage.AddListener(SetTakeDamage);
    }

    // 待机动画 随机事件眨眼和左右看
    public IEnumerator SetIdle()
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
        if (move == Vector2.zero)
        {
            animator.SetBool("isMoving", false);
            return;
        }
        animator.SetBool("isMoving", true);
    }

    public void SetAttack()
    {
        animator.SetTrigger("attack");
    }

    public void SetTakeDamage()
    {
        animator.SetTrigger("beAttacked");
        StartCoroutine(CheckEndDizzy());
    }

    public IEnumerator CheckEndDizzy()
    {
        while (true) {
            if (GetComponent<Actor>().CanMove)
            {
                animator.SetTrigger("endDizzy");
                yield break; // 结束协程
            }
            else
            {
                yield return new WaitForSeconds(0.1f); // 每0.5秒检查一次
            }
        }
    }
}