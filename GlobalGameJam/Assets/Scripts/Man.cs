using UnityEngine;

public class Man : MonoBehaviour
{
    public float force = 30f;
    public Vector3 maxPosition;
    public Vector3 minPosition;

    private bool isMoveing;
    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject.GetComponent<Actor>();
        if(obj != null)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                Vector2 dir = (transform.position - obj.transform.position).normalized;
                rb.AddForce(dir * force, ForceMode2D.Impulse);
                isMoveing = true;
            }
        }
    }
    private void Update()
    {
        if(isMoveing)
        {
            if (transform.position.x > maxPosition.x || transform.position.x < minPosition.x ||
               transform.position.y > maxPosition.y || transform.position.y < minPosition.y)
            {
                
            }
        }
    }
}
