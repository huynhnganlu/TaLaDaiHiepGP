using System.Threading.Tasks;
using UnityEngine;

public class ThaiCucKiemSkill : SkillAbstract
{
    public async override void ProcessSkill()
    {
        GameObject o1 = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position + new Vector3(0f, -3f, 0f), Quaternion.Euler(0f, 0f, 98f));
        GameObject o2 = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position + new Vector3(-3f, 0f, 0f), Quaternion.Euler(0f, 0f, 8f));
        GameObject o3 = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position + new Vector3(3f, 0f, 0f), Quaternion.Euler(0f, 0f, 188f));
        GameObject o4 = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position + new Vector3(0f, 3f, 0f), Quaternion.Euler(0f, 0f, -82f));
        await Task.Delay(skillLifeTime * 1000);
        ObjectPoolController.Instance.ReturnObjectToPool(o1);
        ObjectPoolController.Instance.ReturnObjectToPool(o2);
        ObjectPoolController.Instance.ReturnObjectToPool(o3);
        ObjectPoolController.Instance.ReturnObjectToPool(o4);

    }

    private void Update()
    {
        transform.Translate(2f * Time.deltaTime * new Vector3(-1.2f, -1f, 0f));
    }

}
