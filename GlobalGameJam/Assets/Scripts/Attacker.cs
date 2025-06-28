using UnityEngine;

public class Attacker : Actor
{
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
