using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class CuongPhongDaoSkill : SkillAbstract
{
    public async override void ProcessSkill()
    {
        if (FindClosestEnemy() != null)
        {
            GameObject go = ObjectPoolController.Instance.SpawnObject(gameObject, FindClosestEnemy().position, Quaternion.identity);
            await Task.Delay(skillLifeTime * 1000);
            ObjectPoolController.Instance.ReturnObjectToPool(go);
        }

    }
}
