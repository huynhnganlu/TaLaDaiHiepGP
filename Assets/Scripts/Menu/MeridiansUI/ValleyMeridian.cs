using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValleyMeridian : MeridianAbstract
{
    public override int hp { get; set; }
    public override int level { get; set; }

    private void Start()
    {
        hp = 0;
        level = 0;
    }

}
