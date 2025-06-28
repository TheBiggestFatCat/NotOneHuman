using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    public UnityEvent TackDamage;
    public UnityEvent PlayerAttack;

    [Header("AtkBox")]
    public float atkDistance = 0.5f;
    public Vector2 atkPoint = Vector2.zero;
    public Vector2 atkSize = Vector2.one;
    public float atkAngle = 0f;
    public LayerMask atkLayer;

    [Header("Action")]
    public float AtkColdDown = 1f;
    public float StopColdDown = 2f;
    private float atkTimer;
    private float stopTimer;
    public bool CanAttack { get; set; } = true;
    public bool CanMove { get; set; } = true;

    public virtual void Attack()
    {
        if(!CanAttack)
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
                actor.TakeDamage();
            }
        }
        PlayerAttack?.Invoke();
    }

    public virtual void TakeDamage()
    {
        CanMove = false;
        Debug.Log("Takge Damage");
        TackDamage?.Invoke();
    }

    private void Update()
    {
        if(!CanAttack)
        {
            atkTimer += Time.deltaTime;
            if(atkTimer >= AtkColdDown)
            {
                CanAttack = true;
                atkTimer = 0f;
            }
        }

        if(!CanMove)
        {
            stopTimer += Time.deltaTime;
            if(stopTimer >= StopColdDown)
            {
                CanMove = true;
                stopTimer = 0f;
            }
        }
    }
}
