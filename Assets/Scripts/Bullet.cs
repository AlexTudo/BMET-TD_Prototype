using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject ImpactEffect;
    [Header("Attributes")]
    public int damage = 50;

    public void Seek(Enemy enemy)
    {
        target = enemy.transform;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Move();
    }

    public float speed = 70f;
    private Transform target;

    private void Move()
    {
        //step 1



        //step 2



        //...



    }

    private void Damage(Transform enemy)
    {
        enemy.GetComponent<Enemy>().TakeDamage(damage);
    }

    //private void Move()
    //{
    //    //method 1
    //    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    //    transform.LookAt(target);

    //    //method 2
    //    //Vector3 dir = target.position - transform.position;
    //    //transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);        
    //    //transform.LookAt(target);

    //    //method 3
    //    //Vector3 dir = target.position - transform.position;
    //    //float distanceThisFrame = speed * Time.deltaTime;
    //    //transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    //    //transform.LookAt(target);
    //    //if (dir.magnitude <= distanceThisFrame)


    //    if (Vector3.Distance(transform.position, target.position) <= 0.1f)
    //    {
    //        GameObject effect = Instantiate(ImpactEffect, transform.position, transform.rotation);
    //        Destroy(effect, 5);

    //        Damage(target);

    //        Destroy(gameObject);
    //    }
    //}
}
