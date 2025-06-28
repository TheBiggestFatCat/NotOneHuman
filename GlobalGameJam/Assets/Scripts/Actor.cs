using System;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    public UnityEvent TackDamage;
    public UnityEvent PlayerAttack;
    public UnityEvent<float> UpdateAttackCD;

    [Header("AtkBox")]
    public float atkDistance = 0.5f;
    public Vector2 atkPoint = Vector2.zero;
    public Vector2 atkSize = Vector2.one;
    public float atkAngle = 0f;
    public LayerMask atkLayer;

    [Header("Action")]    
    public float AtkForce = 10f;
    public float AtkColdDown = 1f;
    public float StopColdDown = 2f;
    private float atkTimer;
    private float stopTimer;
    public bool CanAttack { get; set; } = true;
    public bool CanMove { get; set; } = true;

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
        this.enabled = false;
    }

    public virtual void Attack()
    {
        if (!CanAttack || !CanMove)
        {
            Debug.Log("Can not attack yet!");
            return;
        }
        CanAttack = false;

        Debug.Log("Attacker is attacking!");
        Vector2 dir = transform.parent.right * -1;
        Vector2 point = (dir * atkDistance)  + new Vector2(transform.position.x,transform.position.y) + atkPoint;
        //Instantiate(gameObject, point, Quaternion.identity);
        Collider2D collider = Physics2D.OverlapBox(point, atkSize, atkAngle, atkLayer);
        if(collider != null)
        {
            Debug.Log($"Collider:{collider.gameObject}");
            Actor actor = collider.GetComponent<Actor>();
            if(actor != null)
            {
                actor.TakeDamage(this);
            }
            Vector2 force =(collider.transform.position - transform.position).normalized * AtkForce;
            collider.GetComponent<Rigidbody2D>()?.AddForce(force, ForceMode2D.Impulse);
        }
        PlayerAttack?.Invoke();
    }

    public virtual void TakeDamage(Actor atkActor)
    {
        CanMove = false;        
        Debug.Log($"{gameObject}Takge Damage ,by {atkActor.gameObject}");
        TackDamage?.Invoke();
    }

    protected virtual void ColdDownAttack()
    {
        if (!CanAttack)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= AtkColdDown)
            {
                CanAttack = true;             
            }
            UpdateAttackCD?.Invoke(atkTimer / AtkColdDown);
        }
        else
        {
            atkTimer = 0f;
        }        
    }

    protected virtual void ColdDownMove()
    {
        if (!CanMove)
        {
            stopTimer += Time.deltaTime;
            if (stopTimer >= StopColdDown)
            {
                CanMove = true;                
            }
        }
        else
        {
            stopTimer = 0f;            
        }
    }

    protected virtual void Update()
    {
        ColdDownAttack();
        ColdDownMove();
    }
}
