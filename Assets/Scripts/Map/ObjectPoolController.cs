using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    public static ObjectPoolController Instance;
    public List<PooledObjectInfo> objectPools = new();
    [SerializeField]
    private GameObject parentHolder;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPos, Quaternion spawnRotation)
    {
        PooledObjectInfo pool = objectPools.Find(p => p.lookupString.Equals(objectToSpawn.name));

        if (pool == null)
        {
            pool = new PooledObjectInfo() { lookupString = objectToSpawn.name };
            objectPools.Add(pool);
        }

        //Check co cac object nao trong pool inactive khong
        GameObject spawnableObject = pool.inactiveObjects.FirstOrDefault();
        //Khong co object nao dang inactive
        if (spawnableObject == null)
        {
            spawnableObject = Instantiate(objectToSpawn, spawnPos, spawnRotation);
            if (spawnableObject != null)
            {
                spawnableObject.transform.SetParent(parentHolder.transform);
            }
        }
        //Co object dang inactive
        else if (spawnableObject != null)
        {
            spawnableObject.transform.position = spawnPos;
            spawnableObject.transform.rotation = spawnRotation;
            pool.inactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);
        }

        return spawnableObject;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        if(obj != null)
        {
            string nameCheck = obj.name.Replace("(Clone)", string.Empty);
            PooledObjectInfo pool = objectPools.Find(p => p.lookupString.Equals(nameCheck));

            if (pool != null)
            {
                obj.SetActive(false);
                pool.inactiveObjects.Add(obj);
            }
        }  
    }

}

public class PooledObjectInfo
{
    public string lookupString;
    public List<GameObject> inactiveObjects = new List<GameObject>();
}
