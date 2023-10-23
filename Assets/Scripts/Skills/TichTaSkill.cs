using System.Threading.Tasks;
using UnityEngine;

public class TichTaSkill : SkillAbstract
{
    public async override void ProcessSkill()
    {
        GameObject o1 = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position + new Vector3(2f, 0f, 0f), Quaternion.identity);
        GameObject o2 = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position + new Vector3(-2f, 0f, 0f), Quaternion.Euler(0f, 180f, 0f)).gameObject;
        await Task.Delay(skillAppearTime * 1000);
        ObjectPoolController.Instance.ReturnObjectToPool(o1);
        ObjectPoolController.Instance.ReturnObjectToPool(o2);
    }



    private void Update()
    {
        transform.Translate(3f * Time.deltaTime * new Vector3(2f, 0f, 0f));
    }
}
