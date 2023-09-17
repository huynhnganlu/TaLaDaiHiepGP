using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Vector3 shootDir;
  
    // Update is called once per frame
    void Update()
    {
        if (shootDir != null)
        {
            transform.position += 2f * Time.deltaTime * shootDir;
        }
           
    }
}
