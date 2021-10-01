using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    public Transform Start => start;
    public Transform End => end;

    private ObstacleSpawner[] obstacleSpawners;

    public ObstacleSpawner[] ObstacleSpawners => obstacleSpawners == null ?
    obstacleSpawners = GetComponentsInChildren<ObstacleSpawner>() : obstacleSpawners;
}
