using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    public UnityEvent OnTakeDamage;
    public UnityEvent PlayerAttack;
    public UnityEvent<float> UpdateAttackCD;
    public Rigidbody2D rb;

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
    public float beAttackedColdDown = 3f;
    public int sfxIndex = 2;

    protected float timerAtk;
    protected float timerMove;
    protected float timerCanBeAtk;
    public bool CanBeAttacked { get; set; } = true;
    public bool CanAttack { get; set; } = true;
    public bool CanMove { get; set; } = true;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(rb == null)
        {
            rb = transform.parent.GetComponent<Rigidbody2D>();
        }
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
            if(actor != null && actor != this)
            {
                actor.TakeDamage(this);
                Vector2 force = (collider.transform.position - transform.position).normalized * AtkForce;
                actor.rb?.AddForce(force, ForceMode2D.Impulse);
            }
        }
        PlayerAttack?.Invoke();
    }

    public virtual void TakeDamage(Actor atkActor)
    {
        if (!CanBeAttacked) return;
        CanBeAttacked = false;
        CanMove = false;        
        Debug.Log($"{gameObject}Takge Damage ,by {atkActor.gameObject}");
        OnTakeDamage?.Invoke();
        AudioManager.Instance.PlaySFX(sfxIndex);
    }

    protected virtual void ColdDownAttack()
    {
        if (!CanAttack)
        {
            timerAtk += Time.deltaTime;
            if (timerAtk >= AtkColdDown)
            {
                CanAttack = true;             
            }
            UpdateAttackCD?.Invoke(timerAtk / AtkColdDown);
        }
        else
        {
            timerAtk = 0f;
        }        
    }

    protected virtual void ColdDownMove()
    {
        if (!CanMove)
        {
            timerMove += Time.deltaTime;
            if (timerMove >= StopColdDown)
            {
                CanMove = true;                
            }
        }
        else
        {
            timerMove = 0f;            
        }
    }

    protected virtual void ColdDownBeAttacked()
    {
        if (!CanBeAttacked)
        {
            timerCanBeAtk += Time.deltaTime;
            if (timerCanBeAtk >= beAttackedColdDown)
            {
                CanBeAttacked = true;
            }
        }
        else
        {
            timerCanBeAtk = 0f;
        }
    }

    protected virtual void Update()
    {
        ColdDownAttack();
        ColdDownMove();
        ColdDownBeAttacked();
    }
}
