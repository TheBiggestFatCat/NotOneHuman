using DG.Tweening;
using System.Threading;
using UnityEngine;

public class Judge : Actor
{
    public Vector2 maxPosition, minPosition;
    public float Speed = 20f;
    public float RotSpeed = 500f;
    private bool isMoving;
    private Vector2 targetPosition;
    private float changeDirectionTime = 3f;
    private float timer;
    private void OnEnable()
    {
        GameManager.Instance.OnGameOver.AddListener(OnGameOver);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver.RemoveListener(OnGameOver);
    }

    private void OnGameOver(int arg0)
    {
        Destroy(gameObject);
    }

    private Vector2 RandmoPoint()
    {
        float x = Random.Range(minPosition.x, maxPosition.x);
        float y = Random.Range(minPosition.y, maxPosition.y);
        return new Vector2(x, y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Judge collided with {collision.gameObject.name}");
        AtkDefender(collision.gameObject);
    }
    private void AtkDefender(GameObject gameObject)
    {
        var obj = gameObject.GetComponentInChildren<Defender>();
        if (obj != null)
        {
            obj.TakeDamage(this);
            if(isMoving && CanMove)
            {
                isMoving = false;
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!CanMove) return;
        
        if(!isMoving)
        {
            targetPosition = RandmoPoint();
            isMoving = true;
            timer = 0f;
        }
        else
        {
            //移动到目标点
            if(Vector2.Distance(rb.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
            else
            {
                Vector2 direction = (targetPosition - rb.position).normalized;
                rb.MovePosition(rb.position + direction * Time.deltaTime * Speed);
            }
            rb.rotation += RotSpeed * Time.deltaTime;
            
            //更换目标计时器
            timer += Time.deltaTime;
            if(timer >= changeDirectionTime)
            {
                isMoving = false;
            }
        }

    }
}
