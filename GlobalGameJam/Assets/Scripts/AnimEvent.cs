using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void AttackOver()
    {
        transform.parent.GetComponent<PlayerController>()?.Attackover();
    }
}
