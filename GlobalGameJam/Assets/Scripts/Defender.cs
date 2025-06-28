using UnityEngine;

public class Defender : Actor
{
    public override void TakeDamage()
    {
        base.TakeDamage();
        CanAttack = false;
    }
}
