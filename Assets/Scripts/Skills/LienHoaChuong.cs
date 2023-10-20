using System.Threading.Tasks;
using UnityEngine;

public class LienHoaChuong : SkillAbstract
{

    void Update()
    {
        transform.Rotate(0f, 0f, 10f * Time.deltaTime * 20f);
    }


    public async override void ProcessSkill()
    {
        GameObject o1 = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position, Quaternion.identity);
        o1.transform.SetParent(MyCharacterController.Instance.transform);
        await Task.Delay(skillLifeTime * 1000);
        ObjectPoolController.Instance.ReturnObjectToPool(o1);
    }
}
