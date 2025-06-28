using UnityEngine;

public class Man : MonoBehaviour
{
    public float force = 30f;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public bool isMoveing;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        isMoveing = true;
    }
    private void Update()
    {
        if(isMoveing)
        {
            rb.rotation *= Quaternion.Euler(0, 1, 0);
            if (transform.position.x > maxPosition.x || transform.position.x < minPosition.x ||
               transform.position.y > maxPosition.y || transform.position.y < minPosition.y)
            {
                isMoveing = false;
                GameManager.Instance.GetReady();
            }
        }
    }
}
