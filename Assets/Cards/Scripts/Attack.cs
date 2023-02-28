using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : HealthDamage
{
    private GameObject _target;
    public int _dmg;

    void OnMouseDown()
    {
        HealthDamage TargetHealth = _target.GetComponent<HealthDamage>();
        TargetHealth.TakeDamage(_dmg);
    }
}
