using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    public Transform HealthCanvas;

    private Transform target;
    private int waypointIndex = 0;
    private GameFlow playerStats;
    private Enemy enemy;
    private Camera mainCamera;
    private WaveManager waveManager;

    private void Start()
    {
        target = Waypoints.points[0];
        playerStats = FindObjectOfType<GameFlow>();
        enemy = GetComponent<Enemy>();
        waveManager = FindObjectOfType<WaveManager>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (enemy.IsDead)
            return;

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.MoveSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
        transform.LookAt(target);
        HealthCanvas.LookAt(mainCamera.transform);
    }

    private void EndPath()
    {
        waveManager.DestroyEnemy(enemy);
        playerStats.LoseLife();
    }
}
