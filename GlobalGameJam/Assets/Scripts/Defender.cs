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

    private void OnDrawGizmos()
    {
        Vector2 dir = transform.parent.right * -1;
        Vector2 point = (dir * atkDistance) + new Vector2(transform.position.x, transform.position.y) + atkPoint;
        Gizmos.color = Color.red;

        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(
        point,
        Quaternion.Euler(0, 0, atkAngle),
        Vector3.one
        );
        Gizmos.DrawWireCube(Vector3.zero, atkSize);
    }
}
