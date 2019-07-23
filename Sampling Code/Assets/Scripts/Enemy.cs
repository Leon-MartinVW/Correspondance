using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject deathEffect;
   
    private void Update()
    {
        //make the character jump back or enemy be dazed
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); 
            //can do blood effect as well as screen shake etc
            //sound effect
        }
    }
    //getting damage from the player
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage");
    }

    public void TakeDamageProjectile(int damage)
    {
        health -= damage;
        Debug.Log("Projectile hit");
    }
}
