using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.DevTools;
using FluffyUnderware.Curvy.Examples;
using FluffyUnderware.Curvy.Controllers;
public class CarSound : MonoBehaviour
{
    public float maxSpeed = 100.0f; // The maximum speed of the car
    public VolumeController VL;
    private AudioSource audioSource; // The audio source for the engine sound
  //  private Rigidbody rigidbody; // The car's rigidbody component
    public float max_Rot,min_Rot;
    void Start()
    {
        // Get the audio source and rigidbody components
        audioSource = GetComponent<AudioSource>();
     //   rigidbody = GetComponent<Rigidbody>();
        //
        //Debug.Log(rigidbody.gameObject.name);
    }

    void Update()
    {
        // Get the current speed of the car
        float speed = VL.Speed; //rigidbody.velocity.magnitude;
       // Debug.Log("velocity " + speed);

        // Calculate the pitch value based on the car's speed
        float minPitch = 0.5f;
        float maxPitch = 3.0f;
        float pitch = Mathf.Lerp(minPitch, maxPitch, speed / maxSpeed);
        // Mathf.Clamp(transform.localEulerAngles.z, min_Rot,max_Rot);

        // Set the pitch of the engine sound
        audioSource.pitch = pitch;
    }
}
