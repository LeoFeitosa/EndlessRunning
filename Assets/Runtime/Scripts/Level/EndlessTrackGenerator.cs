using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TrackSegment[] segmentPrefabs;
    [SerializeField] private TrackSegment firstTrackPrefab;

    [Header("Endless generation parameter")]
    [Space]
    [SerializeField] private int initialTrackCount = 10;
    [SerializeField] private int minTracksInFrontOfPlayer = 3;
    private List<TrackSegment> currentSegments = new List<TrackSegment>();

    void Start()
    {
        SpawnTrackSegment(firstTrackPrefab, null);
        SpawnTracks(initialTrackCount);
    }

    void Update()
    {
        //em qual track o player esta
        int playerTrackIndex = -1;
        for (int i = 0; i < currentSegments.Count; i++)
        {
            TrackSegment track = currentSegments[i];
            if (player.transform.position.z >= track.Start.position.z &&
            player.transform.position.z <= track.End.position.z)
            {
                playerTrackIndex = i;
                break;
            }

            if (playerTrackIndex < 0)
            {
                return;
            }
        }

        //instanciar tracks na frente do player se necessario
        int trackInFrontOfPlayer = currentSegments.Count - (playerTrackIndex - 1);
        if (trackInFrontOfPlayer < minTracksInFrontOfPlayer)
        {

        }

    }

    private TrackSegment SpawnTrackSegment(TrackSegment track, TrackSegment previousTrack)
    {
        TrackSegment trackInstance = Instantiate(track, transform);

        if (previousTrack != null)
        {
            trackInstance.transform.position = previousTrack.End.position +
            (trackInstance.transform.position - trackInstance.Start.position);
        }
        else
        {
            trackInstance.transform.localPosition = Vector3.zero;
        }

        foreach (var obstacleSpawner in trackInstance.ObstacleSpawners)
        {
            obstacleSpawner.SpawnObstacle();
        }

        currentSegments.Add(trackInstance);

        return trackInstance;
    }

    private void SpawnTracks(int trackCount)
    {
        TrackSegment previousTrack = currentSegments.Count > 0
        ? currentSegments[currentSegments.Count - 1]
        : null;

        for (int i = 0; i < trackCount; i++)
        {
            int index = Random.Range(0, segmentPrefabs.Length);
            var track = segmentPrefabs[index];
            previousTrack = SpawnTrackSegment(track, previousTrack);
        }
    }

}
