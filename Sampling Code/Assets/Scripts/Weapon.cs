using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour{

    public float firerate = 0;
    public float damage = 10f;
    public LayerMask whatToHit;
    public int offset;
    float timeToSpawnEffect=0;
    public float effectSpawnRate = 10;
    private float timeToFire = 0;
    Transform firePoint;
    

    public GameObject projectile;
    public Transform BulletTrail;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private void Awake()
    {
        firePoint = transform.Find("Shotpoint");
        if (firePoint == null)
        {
            Debug.LogError("No fp");
        }

    }
    private void Update()
    {
        FaceMouse();

        if (firerate == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Mouse0") && Time.time > timeToFire)//this is rapid fire but it does not work at the moment
            {
                timeToFire = Time.time + 1 / firerate;
                Shoot();
            }
        }
    }

    private void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.right = direction;
    }

    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        if (Time.time > timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect=(Time.time + 1 /effectSpawnRate);
        }

        /*Debug.DrawLine(firePointPosition, (mousePosition-firePointPosition)*100,Color.cyan);

        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.log("Hit"+hit.collider.name+"did"+damage+"damage;
        }*/
    }

    private void Effect()
    {
        Instantiate(BulletTrail,firePoint.position,firePoint.rotation);
        Instantiate(projectile, firePoint.position, firePoint.rotation);
    }
    /*
public float offset;

public GameObject projectile;
public Transform shotPoint;

private float timeBtwShots;
public float startTimeBtwShots;

private void Update() {
   Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;//destination minus origin
   float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
   transform.rotation = Quaternion.Euler(0f, 0f, rotZ+offset);

   if (timeBtwShots <= 0) {
       if (Input.GetMouseButtonDown(0)) {
           Instantiate(projectile, shotPoint.position, transform.rotation);
           timeBtwShots = startTimeBtwShots;
       }
   } else {
       timeBtwShots -= Time.deltaTime;
   }
}*/
}
