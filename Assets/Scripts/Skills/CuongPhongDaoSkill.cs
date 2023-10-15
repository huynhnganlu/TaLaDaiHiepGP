using UnityEngine;

public class CuongPhongDaoSkill : SkillAbstract
{
    public override void ProcessSkill()
    {
        GameObject o1 = Instantiate(this, MapController.Instance.GetRandomSpawnPosition(5f, 10f), Quaternion.identity).gameObject;
        Destroy(o1, skillLifeTime);
    }

}
