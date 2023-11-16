using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject Node;
    public GameObject Ground;
    [Header("References")]
    public Transform PlatformsParent;
    public List<Transform> AllWaypoints = new List<Transform>();    //includes START and END
    public List<Transform> MapCorners = new List<Transform>();
    public List<Transform> MainCorners = new List<Transform>();

    private HashSet<Vector3> grid = new HashSet<Vector3>();
    private const int squareSize = 5;

    private void Awake()
    {
        //build level
        for (int i = 0; i < AllWaypoints.Count - 1; i++)
        {
            Transform startPoint = AllWaypoints[i];
            Transform endPoint = AllWaypoints[i+1];

            float distance = Vector3.Distance(startPoint.position, endPoint.position);
            distance = distance / squareSize;

            // Spawn a platform at each interval along the line.
            for (float j = 1; j <= distance; j += 1)
            {
                Vector3 spawnPosition = startPoint.position + j * (endPoint.position - startPoint.position) / distance;
                spawnPosition.y = 0;
                grid.Add(spawnPosition);

                Instantiate(Ground, spawnPosition, Quaternion.identity, PlatformsParent);
            }
        }

        //find corners
        float[] xPositions = new float[MapCorners.Count];
        float[] zPositions = new float[MapCorners.Count];
        float[] xMainPositions = new float[MainCorners.Count];
        float[] zMainPositions = new float[MainCorners.Count];

        for (int i = 0; i < MapCorners.Count; i++)
        {
            xPositions[i] = MapCorners[i].position.x;
            zPositions[i] = MapCorners[i].position.z;
            xMainPositions[i] = MainCorners[i].position.x;
            zMainPositions[i] = MainCorners[i].position.z;
        }

        float minX = Mathf.Min(xPositions);
        float maxX = Mathf.Max(xPositions);
        float minZ = Mathf.Min(zPositions);
        float maxZ = Mathf.Max(zPositions);
        float minMainX = Mathf.Min(xMainPositions);
        float maxMainX = Mathf.Max(xMainPositions);
        float minMainZ = Mathf.Min(zMainPositions);
        float maxMainZ = Mathf.Max(zMainPositions);

        // Spawn nodes for rest of map
        for (float x = minX; x <= maxX; x += squareSize)
        {
            for (float z = minZ; z <= maxZ; z += squareSize)
            {
                Vector3 newPos = new Vector3(x, 0, z);

                if (!grid.Contains(newPos))
                {
                    GameObject newNode = Instantiate(Node, newPos, Quaternion.identity, PlatformsParent);
                    if (x < minMainX || x > maxMainX || z < minMainZ || z > maxMainZ)
                    {
                        newNode.GetComponent<Node>().enabled = false;
                    }
                }
            }
        }
    }
}
