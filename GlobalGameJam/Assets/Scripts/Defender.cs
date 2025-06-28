using UnityEngine;

public class Defender : Actor
{
    public override void TakeDamage(Actor atkActor)
    {
        base.TakeDamage(this);
        GameManager.Instance.DefenderTakeDamage();
    }

    public override void Attack()
    {
        base.Attack();
        CanAttack = false;
    }
}
