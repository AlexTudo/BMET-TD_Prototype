using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turret : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject Bullet;
    [Header("References")]
    public Transform PartToRotate;
    public Transform FirePoint;
    [Header("Attributes")]
    public float range = 15f;
    public float turnSpeed = 10;
    public float fireRate = 1;

    private float fireCountdown = 0;
    private WaveManager waveSpawner;

    void Start()
    {
        fireRate += Random.Range(-0.1f, 0.2f);

        waveSpawner = FindObjectOfType<WaveManager>();

        enemies = waveSpawner.Enemies;
    }


    List<Enemy> enemies;
    protected Enemy target;

    private void FindTarget()
    {
        //step 1



        //step 2



        //...



    }

    void Update()
    {
        FindTarget();

        if (target == null)
        {
            return;
        }

        LockOnTarget();

        Shoot();
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    protected virtual void Shoot()
    {
        if (fireCountdown <= 0f)
        {
            GameObject newBullet = Instantiate(Bullet, FirePoint.position, Quaternion.Euler(90, 0, 0));
            newBullet.GetComponent<Bullet>().Seek(target);
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    //private void FindTarget()
    //{
    //    float shortestDistance = Mathf.Infinity;
    //    Enemy nearestEnemy = null;

    //    foreach (Enemy enemy in enemies)
    //    {
    //        if (enemy.IsDead)
    //            continue;

    //        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
    //        if (distanceToEnemy < shortestDistance)
    //        {
    //            shortestDistance = distanceToEnemy;
    //            nearestEnemy = enemy;
    //        }
    //    }

    //    if (shortestDistance <= range)
    //    {
    //        target = nearestEnemy;
    //    }
    //    else
    //    {
    //        target = null;
    //    }
    //}
}
