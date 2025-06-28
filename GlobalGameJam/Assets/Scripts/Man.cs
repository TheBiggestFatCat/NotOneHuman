using UnityEngine;

public class Man : MonoBehaviour
{
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public bool isTriggered;


    void Update()
    {
        if(!isTriggered)
        {
            if (transform.position.x > maxPosition.x || transform.position.x < minPosition.x ||
               transform.position.y > maxPosition.y || transform.position.y < minPosition.y)
            {
                isTriggered = true;
                GameManager.Instance.GetReady();
                Debug.Log($"{gameObject} is Ready");
            }
        }
    }
}
