using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public SoundPlayer sp;
    public void Attack()
    {
        sp.PlaySound(0);
        var player = GameObject.Find("Player").GetComponent<Damagable>();
        var dist = Vector3.Distance(player.transform.position, transform.position);
        if(dist < 2)
        {
            player.TakeDamage();
        }
        //check player in range
        //deal damage if is
    }
    public void Die()
    {
        transform.parent.GetComponent<Enemy>().Die();
    }
}
