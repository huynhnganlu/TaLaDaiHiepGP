using UnityEngine;

public class FighterController : EnemyController
{
    public float radius;
    public Transform hitRaycast;

    public void SetHitPlayer()
    {
        HitPlayer(radius, hitRaycast.position);
    }
}
