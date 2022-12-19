using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy.Examples;
using FluffyUnderware.Curvy.Controllers;
public class AICarMovement : MonoBehaviour
{
    public SplineController sp_Controller; 
    Rigidbody rig_Car;
    public int car_Id;
    public VolumeControllerInput VL_Controller;
    public GameObject[] pos_Board;
    bool isTrue;
    private void Start()
    {
        rig_Car= gameObject.GetComponent<Rigidbody>();
        StartCoroutine(PosUpdate());
    }
    IEnumerator PosUpdate()
    {
        yield return new WaitForSeconds(0.5f);
        if (!isTrue)
        {
            if (VL_Controller.transform.position.z > sp_Controller.transform.position.z)
            {
                GameManager.CarPos_Counter++;
                isTrue = true;
            }
        }
        if (isTrue)
        {
            if (VL_Controller.transform.position.z < sp_Controller.transform.position.z)
            {
                GameManager.CarPos_Counter--;
                isTrue = false;
            }

        }
        for (int i = 0; i < pos_Board.Length; i++)
        {
            pos_Board[i].SetActive(false);
        }
        switch (GameManager.CarPos_Counter)
        {
            case 0:
                pos_Board[4].SetActive(true);
                break;
            case 1:
                pos_Board[3].SetActive(true);
                break;
            case 2:
                pos_Board[2].SetActive(true);
                break;
            case 3:
                pos_Board[1].SetActive(true);
                break;
            case 4:
                pos_Board[0].SetActive(true);
                break;
        }
        if (gameObject.name== "CarSpeed01")
        print("count " + GameManager.CarPos_Counter);
        StartCoroutine(PosUpdate());
    }
    public void SpeedExceed(float speed)
    {
        sp_Controller.Speed = sp_Controller.Speed+ speed;
    }
    public void ReachedPoint()
    {
        rig_Car.constraints = RigidbodyConstraints.FreezeAll;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    
        }
       
    }
}
//public enum CarNum { car1, car2, car3, car4 };
