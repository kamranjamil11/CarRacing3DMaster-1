using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy.Examples;
using FluffyUnderware.Curvy.Controllers;

public class TrackPoint : MonoBehaviour
{
    public float player_Speed;
    public float[] aicars_Speed;//, car2_Speed, car3_Speed, car4_Speed;
    public VolumeController volumeController;
    public AICarMovement ai_Cars;
    private VolumeControllerInput VL_input;
    private void Start()
    {
       // VL_input = FindObjectOfType<VolumeControllerInput>();
    }
    public void OnTriggerEnter(Collider other)
    {
        //if (!VolumeControllerInput.isStop)
        //{
        //    if (other.gameObject.CompareTag("Player"))
        //    {
        //        // print("hit");
        //       // volumeController.Speed = volumeController.Speed+ player_Speed;
        //    }
        //}
        if (other.gameObject.CompareTag("AICarCollider"))
        {
           // print("Collide");
            AICarMovement car = other.GetComponent<AICarMovement>();
            car.SpeedExceed(aicars_Speed[car.car_Id]);
        }
        else if(other.gameObject.CompareTag("Player"))
        {
           other.GetComponent<CarMovement>().VL_Input.lastPoint_Pos= other.transform.position;
        }
    }
}
