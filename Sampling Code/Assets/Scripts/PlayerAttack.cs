using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    //circle area of attack
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;


    private void Update() {
        if (timeBtwAttack <= 0) {
            //then you can attack
            if (Input.GetKey(KeyCode.Q)) {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++) {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        } else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected() {
        //can do a cube if that would be better
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }
}
