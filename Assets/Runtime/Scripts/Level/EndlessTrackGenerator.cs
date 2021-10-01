using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private TrackSegment[] segmentPrefabs;
    [SerializeField] private TrackSegment firstTrackPrefab;
    [SerializeField] private int initialTrackCount = 10;
    private List<TrackSegment> currentSegments = new List<TrackSegment>();

    void Start()
    {
        TrackSegment initialTrack = Instantiate(firstTrackPrefab, transform);
        currentSegments.Add(initialTrack);

        TrackSegment previousTrack = initialTrack;
        for (int i = 0; i < initialTrackCount; i++)
        {
            int index = Random.Range(0, segmentPrefabs.Length);
            var track = segmentPrefabs[index];
            TrackSegment trackInstance = Instantiate(track, transform);
            trackInstance.transform.position = previousTrack.End.position +
            (trackInstance.transform.position - trackInstance.Start.position);

            foreach (var obstacleSpawner in trackInstance.ObstacleSpawners)
            {
                obstacleSpawner.SpawnObstacle();
            }

            previousTrack = trackInstance;
        }
    }
}
