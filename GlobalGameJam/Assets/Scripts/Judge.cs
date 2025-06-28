using DG.Tweening;
using UnityEngine;

public class Judge : Actor
{
    public Vector2 maxPosition, minPosition;
    public float Speed = 20f;
    public float RotSpeed = 500f;
    public Vector2 attachOffset;
    private Transform attachTrans;
    private Rigidbody2D rb;
    private bool isMoving;
    private Vector2 targetPosition;
    private bool isAttached;

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

    public override void TakeDamage(Actor atkActor)
    {
        base.TakeDamage(atkActor);
        if(atkActor is Attacker)
        {
            isAttached = true;
        }
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
        if(isAttached)
        {
            if(!CanMove)
            {
                transform.position = attachTrans.position + (Vector3)attachOffset;
            }
            else
            {
                isAttached = false;
            }
            return;
        }
        
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
