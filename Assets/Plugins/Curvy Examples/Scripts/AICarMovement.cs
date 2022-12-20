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
    public GameObject[] opposit_Cars;
    bool isTrue,isOtherCar;
    int pos_Counter;
    private void Start()
    {
        rig_Car= gameObject.GetComponent<Rigidbody>();
        StartCoroutine(PosUpdate());
        //StartCoroutine(OppositCarUpdate());
    }
    IEnumerator PosUpdate()
    {
        yield return new WaitForSeconds(0.5f);
        if (!GameManager.instance.isComplete)
        {
            if (!isTrue)
            {
                if (VL_Controller.transform.position.z > sp_Controller.transform.position.z)
                {
                    GameManager.CarPos_Counter++;
                    //  pos_Counter--;
                    isTrue = true;
                }
            }
            if (isTrue)
            {
                if (VL_Controller.transform.position.z < sp_Controller.transform.position.z)
                {
                    GameManager.CarPos_Counter--;
                    // pos_Counter++;
                    isTrue = false;
                }
            }

            // if (gameObject.name== "CarSpeed01")
            // print("count " + GameManager.CarPos_Counter);
            StartCoroutine(PosUpdate());
        }
    }
    IEnumerator OppositCarUpdate()
    {
        yield return new WaitForSeconds(0.5f);
        if (gameObject.name == "car_3")
        {
            for (int i = 0; i < opposit_Cars.Length-1; i++)
            {
                //if (1 != 3)
                //{
                    AICarMovement other = opposit_Cars[i].transform.GetChild(0).GetComponent<AICarMovement>();
                //}
                //else 
                //{
                //    CarMovement other = opposit_Cars[3].transform.GetChild(0).GetChild(0).GetComponent<CarMovement>();
                //}
                if (!other.isOtherCar)
                {
                    if (sp_Controller.transform.position.z > other.transform.position.z)
                    {
                        pos_Counter++;
                        other.isOtherCar = true;
                    }
                }
                if (other.isOtherCar)
                {
                    if (sp_Controller.transform.position.z < other.transform.position.z)
                    {
                        pos_Counter--;
                        other.isOtherCar = false;
                    }
                }
            }

            //for (int i = 0; i < opposit_Cars.Length-1; i++)
            //{
            //    AICarMovement other = opposit_Cars[i].transform.GetChild(0).GetComponent<AICarMovement>();
            //    if (other.isOtherCar)
            //    {
            //        if (sp_Controller.transform.position.z < other.transform.position.z)
            //        {
            //            pos_Counter--;
            //            other.isOtherCar = false;
            //        }
            //    }
            //}
            for (int i = 0; i < pos_Board.Length; i++)
            {
                pos_Board[i].SetActive(false);
            }

            switch (pos_Counter)
            {
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
                default:
                    pos_Board[car_Id].SetActive(true);
                    break;
            }
            if (gameObject.name == "car_3")
                print("count " + pos_Counter);
            StartCoroutine(OppositCarUpdate());
        }
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
            GameManager.instance.isComplete = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    
        }
       
    }
}
//public enum CarNum { car1, car2, car3, car4 };
