using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1;
    [SerializeField] private AudioClip pickupAudio;
    [SerializeField] private GameObject model;

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    public void OnPickedUp()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioUtility.PlayAudioCue(audioSource, pickupAudio);

        model.SetActive(false);
        Destroy(gameObject, pickupAudio.length);
    }
}
