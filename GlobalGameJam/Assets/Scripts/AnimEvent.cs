using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void AttackOver()
    {
        transform.parent.GetComponent<PlayerController>()?.Attackover();
    }

    public void AttackStart()
    {
        transform.parent.GetComponent<PlayerController>()?.AttackStart();
    }
}
