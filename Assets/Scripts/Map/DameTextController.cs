using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DameTextController : MonoBehaviour
{
    private int dmg;

    void Update()
    {
        if(dmg != -1)
            transform.Translate(2f * Time.deltaTime * new Vector3(0f, 2f, 0f));
    }

    public void SetDamage(int dmg)
    {
        this.dmg = dmg;
        GetComponent<TextMeshPro>().text = dmg.ToString();
    }
}
