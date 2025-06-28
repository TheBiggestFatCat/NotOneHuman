using DG.Tweening;
using UnityEngine;

public class Judge : Actor
{
    public Vector2 maxPosition, minPosition;
    public float Speed = 20f;
    public float RotSpeed = 500f;
    private Rigidbody2D rb;
    private bool isMoving;
    private Vector2 targetPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 RandmoPoint()
    {
        float x = Random.Range(minPosition.x, maxPosition.x);
        float y = Random.Range(minPosition.y, maxPosition.y);
        return new Vector2(x, y);
    }

    


    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject.GetComponent<Actor>();
        if(obj != null && obj is Defender)
        {
            obj.TakeDamage(this);
            Debug.Log($"Judge collided with {obj.gameObject.name}");
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!CanMove) return;
        
        if(!isMoving)
        {
            targetPosition = RandmoPoint();
            Debug.Log($"Judge moving to {targetPosition}");
            isMoving = true;
        }
        else
        {
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
        }

    }
}
