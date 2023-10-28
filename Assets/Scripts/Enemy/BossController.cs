using UnityEngine;

public class BossController : EnemyController
{

    public float radius;
    public Transform hitRaycast;

    public void SetHitPlayer()
    {
        HitPlayer(radius, hitRaycast.position);
    }

}
