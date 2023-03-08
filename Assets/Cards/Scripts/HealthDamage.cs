
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDamage : MonoBehaviour
{
    private int _curentHealth;
    public int _startHealth;
    void Start()
    {
        _curentHealth = _startHealth;
    }

    public void TakeDamage(int dmg)
    {
        _curentHealth -= dmg;
        if (_curentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }


}
