using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TrackSegment firstTrackPrefab;
    [SerializeField] private TrackSegment[] easyTrackPrefabs;
    [SerializeField] private TrackSegment[] hardTrackPrefabs;

    [Header("Endless generation parameter")]
    [Space]
    [SerializeField] private int initialTrackCount = 10;
    [SerializeField] private int minTracksInFrontOfPlayer = 3;
    [SerializeField] private float minDistanceToConsiderInsideTrack = 3;

    [Header("Level dificulty parameters")]
    [Space]
    [Range(0, 1)]
    [SerializeField] private float hardTrackChance = 0.2f;
    [SerializeField] private int minTracksBeforeReward = 10;
    [SerializeField] private int maxTracksBeforeReward = 20;
    [SerializeField] private int minRewardTrackCount = 1;
    [SerializeField] private int maxRewardTrackCount = 3;

    private List<TrackSegment> currentSegments = new List<TrackSegment>();

    private bool isSpawningRewardTracks = false;
    private int rewardTracksLeftToSpawn = 0;
    private int trackSpawnedAfterLastReward = 0;

    void Start()
    {
        SpawnTrackSegment(firstTrackPrefab, null);
        SpawnTracks(initialTrackCount);
    }

    void Update()
    {
        UpdateTracks();
    }

    private void UpdateTracks()
    {
        //em qual track o player esta
        int playerTrackIndex = FindTrackIndexWithPlayer();

        if (playerTrackIndex < 0)
        {
            return;
        }

        InstantiateTracksInFrontOfPlayer(playerTrackIndex);
        DestroyTrackBehindPlayer(playerTrackIndex);
    }

    private void DestroyTrackBehindPlayer(int playerTrackIndex)
    {
        //destroi track atras do player
        for (int i = 0; i < playerTrackIndex; i++)
        {
            TrackSegment track = currentSegments[i];
            Destroy(track.gameObject);
        }
        currentSegments.RemoveRange(0, playerTrackIndex);
    }

    private void InstantiateTracksInFrontOfPlayer(int playerTrackIndex)
    {
        //instanciar tracks na frente do player se necessario
        int tracksInFrontOfPlayer = currentSegments.Count - (playerTrackIndex - 1);
        if (tracksInFrontOfPlayer < minTracksInFrontOfPlayer)
        {
            SpawnTracks(minTracksInFrontOfPlayer - tracksInFrontOfPlayer);
        }
    }

    private int FindTrackIndexWithPlayer()
    {
        int playerTrackIndex = -1;
        for (int i = 0; i < currentSegments.Count; i++)
        {
            TrackSegment track = currentSegments[i];
            if (player.transform.position.z >= (track.Start.position.z + minDistanceToConsiderInsideTrack) &&
            player.transform.position.z <= track.End.position.z)
            {
                playerTrackIndex = i;
                break;
            }
        }

        return playerTrackIndex;
    }

    private void SpawnTracks(int trackCount)
    {
        TrackSegment previousTrack = currentSegments.Count > 0
        ? currentSegments[currentSegments.Count - 1]
        : null;

        for (int i = 0; i < trackCount; i++)
        {
            var track = GetRandomTrack();
            previousTrack = SpawnTrackSegment(track, previousTrack);
        }
    }

    private TrackSegment GetRandomTrack()
    {
        TrackSegment[] trackList = Random.value <= hardTrackChance ? hardTrackPrefabs : easyTrackPrefabs;
        return trackList[Random.Range(0, trackList.Length)];
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

        UpdateRewardTracking();

        return trackInstance;
    }

    private void UpdateRewardTracking()
    {
        if (isSpawningRewardTracks)
        {
            rewardTracksLeftToSpawn--;
            if (rewardTracksLeftToSpawn < -0)
            {
                isSpawningRewardTracks = false;
                trackSpawnedAfterLastReward = 0;
            }
        }
        else
        {
            rewardTracksLeftToSpawn++;
            int requiretTracksBeforeReward = Random.Range(minTracksBeforeReward, maxTracksBeforeReward + 1);
            if (trackSpawnedAfterLastReward >= requiretTracksBeforeReward)
            {
                isSpawningRewardTracks = true;
                rewardTracksLeftToSpawn = Random.Range(minRewardTrackCount, maxRewardTrackCount + 1);
            }
        }
    }
}
