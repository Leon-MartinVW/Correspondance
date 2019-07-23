using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    public GameObject destroyEffect;
    public Transform firePoint;
  
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);//changed up to right
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy")) {
                hitInfo.collider.GetComponent<Enemy>().TakeDamageProjectile(damage);
            }
            DestroyProjectile();
        }
        transform.Translate(firePoint.right * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        //struggling to find way to destroy the visual effects after they have been created
        //Destroy(this.destroyEffect,1f);
    }
}
