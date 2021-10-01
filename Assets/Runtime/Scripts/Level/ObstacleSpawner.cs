using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle[] obstaclePrefabOptions;
    private Obstacle currentObstacle;

    public void SpawnObstacle()
    {
        int index = Random.Range(0, obstaclePrefabOptions.Length);
        Obstacle prefab = obstaclePrefabOptions[index];
        currentObstacle = Instantiate(prefab, transform);
        currentObstacle.transform.localPosition = Vector3.zero;
        currentObstacle.transform.rotation = Quaternion.identity;
    }
}
