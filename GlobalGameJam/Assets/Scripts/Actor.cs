using UnityEngine;

public class Actor : MonoBehaviour
{
    [Header("AtkBox")]
    public float atkDistance = 0.5f;
    public Vector2 atkPoint = Vector2.zero;
    public Vector2 atkSize = Vector2.one;
    public float atkAngle = 0f;
    public LayerMask atkLayer;
    public virtual void Attack()
    {
        Debug.Log("Attacker is attacking!");
        Vector2 dir = transform.parent.right * -1;
        Vector2 point = (dir * atkDistance)  + new Vector2(transform.position.x,transform.position.y) + atkPoint;
        //Instantiate(gameObject, point, Quaternion.identity);
        Collider2D collider = Physics2D.OverlapBox(point, atkSize, atkAngle, atkLayer);
        if(collider != null)
        {
            Debug.Log($"Collider:{collider.gameObject}");
        }        
    }
}
