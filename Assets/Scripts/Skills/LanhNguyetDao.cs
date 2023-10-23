using System.Threading.Tasks;
using UnityEngine;

public class LanhNguyetDao : SkillAbstract
{
    public override async void ProcessSkill()
    {
        GameObject o1 = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
        await Task.Delay(skillLifeTime * 1000);
        ObjectPoolController.Instance.ReturnObjectToPool(o1);
    }


    private void Update()
    {
        transform.Rotate(0f, 0f, 30f * Time.deltaTime * 10f);
        if (!MapController.Instance.isFreezing)
            transform.Translate(0f, -0.03f, 0f, Space.World);
    }
}
