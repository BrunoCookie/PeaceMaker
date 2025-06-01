using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    //public int bulletDamage;
    public float bulletForce;
    public float firerate;

    private Timer cooldownTimer;

    private void Awake()
    {
        cooldownTimer = new Timer(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && cooldownTimer.CheckTimer())
        {
            Shoot();
            cooldownTimer = new Timer(1f/firerate);
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);// shootPoint.rotation);
        //bullet.GetComponent<BulletScript>().damage = bulletDamage;
        bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * bulletForce, ForceMode2D.Impulse);
        GameMode.instance.audiomanager.Play("ShootSound");
    }
}
