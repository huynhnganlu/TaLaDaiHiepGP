using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuongPhongDaoSkill : SkillAbstract
{
    private Collider2D spawnArea;

    public override void ProcessSkill()
    {
        spawnArea = GameObject.Find("SpawnEnemyArea").GetComponent<BoxCollider2D>();
        GameObject o1 = Instantiate(this, MapController.Instance.GetRandomSpawnPosition(spawnArea, 5f, 10f), Quaternion.identity).gameObject;
        Destroy(o1, skillLifeTime);
    }
}
