using DG.Tweening;
using UnityEngine;

public class Judge : Actor
{
    public Vector2 maxPosition, minPosition;
    public float Speed = 20f;
    public float RotSpeed = 500f;
    private bool isMoving;
    private Vector2 targetPosition;

    private Vector2 RandmoPoint()
    {
        float x = Random.Range(minPosition.x, maxPosition.x);
        float y = Random.Range(minPosition.y, maxPosition.y);
        return new Vector2(x, y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Judge collided with {collision.gameObject.name}");
        var obj = collision.gameObject.GetComponentInChildren<Actor>();        
        if (obj != null && obj is Defender)
        {
            obj.TakeDamage(this);            
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!CanMove) return;
        
        if(!isMoving)
        {
            targetPosition = RandmoPoint();
            //Debug.Log($"Judge moving to {targetPosition}");
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
