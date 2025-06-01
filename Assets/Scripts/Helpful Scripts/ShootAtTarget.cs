using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtTarget : MonoBehaviour
{
    public Transform target;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float distanceWhenToShoot;
    public int bulletDamage;
    public float bulletForce;
    public float firerate;

    private Timer cooldownTimer;

    private void Awake()
    {
        cooldownTimer = new Timer(2);
    }

    private void Update()
    {
        Vector2 closestPoint = target.GetComponent<Collider2D>().ClosestPoint(transform.position);
        float distance = Vector3.Distance(closestPoint, transform.position);

        if (cooldownTimer.CheckTimer() && (distance <= distanceWhenToShoot || distanceWhenToShoot == 0f))
        {
            Shoot();
            cooldownTimer = new Timer(1/firerate);
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        bullet.GetComponent<EnemyBulletScript>().damage = bulletDamage;
        bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
