using UnityEngine;

public class Man : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject.GetComponent<Actor>();
        if(obj != null)
        {

        }
    }    
}
