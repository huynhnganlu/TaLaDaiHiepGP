using System.Threading.Tasks;
using UnityEngine;

public class ThaiCucQuyenSkill : SkillAbstract
{
    private Vector3 dir;
    public async override void ProcessSkill()
    {
        GameObject o1 = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position + new Vector3(2f, 0f, 0f), Quaternion.identity);
        await Task.Delay(skillLifeTime * 1000);
        ObjectPoolController.Instance.ReturnObjectToPool(o1);
    }

    private void Start()
    {
        dir = Random.insideUnitCircle.normalized;
    }

    private void Update()
    {
        if (dir != null)
            transform.Translate(2f * Time.deltaTime * dir);
    }
}
