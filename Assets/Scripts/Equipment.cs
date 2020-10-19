using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public int damage;
    public float attackRange;
    public float attackRate;


    public class noWeapon
    {
        public int damage;
        public float attackRange;
        public float attackRate;

        public noWeapon(int dmg, float range, float rate)
        {
            damage = dmg;
            attackRange = range;
            attackRate = rate;
        }

    }
}
