using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FluffyUnderware.DevTools;
using FluffyUnderware.Curvy.Examples;
using DG.Tweening;
using FluffyUnderware.Curvy.Controllers;

namespace FluffyUnderware.Curvy.Examples
{
    public class CarMovement : MonoBehaviour
    {
        public Text Pos_Text;
        public Camera main_Camera, endPoint_Camera;
        public VolumeControllerInput VL_Input;      
        public GameObject rt;
        public GameObject[] pos_Board;
        public GameObject[] ai_Cars;
       // int count;
        bool isTrue;
        private void Start()
        {
            StartCoroutine(PosUpdate());
        }
        IEnumerator PosUpdate()
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < pos_Board.Length; i++)
            {
                pos_Board[i].SetActive(false);
            }
            switch (GameManager.CarPos_Counter)
            {
                case 0:
                    pos_Board[4].SetActive(true);
                    Pos_Text.text = "5th";
                    break;
                case 1:
                    pos_Board[3].SetActive(true);
                    Pos_Text.text = "4th";
                    break;
                case 2:
                    pos_Board[2].SetActive(true);
                    Pos_Text.text = "3rd";
                    break;
                case 3:
                    pos_Board[1].SetActive(true);
                    Pos_Text.text = "2nd";
                    break;
                case 4:
                    pos_Board[0].SetActive(true);
                    Pos_Text.text = "1st";
                    break;
            }
            StartCoroutine(PosUpdate());
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("MainCamera") && !isTrue)
            {
                VL_Input.car_Sound.enabled = false;
                print("Collide");
                VL_Input.mGameOver = true;
                GameManager.instance.EndEffect();
                gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                main_Camera.GetComponent<ChaseCam>().enabled = false;
                main_Camera.gameObject.transform.DOLocalMove(new Vector3(transform.position.x, transform.position.y + 30f, transform.position.z), 2f).OnComplete(delegate
                {
                    GameManager.instance.LevelComplete();
                });
                isTrue = true;
            }
            else if (other.gameObject.CompareTag("Finish"))
            {
                // print("Collide");
                //other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 3);
                //GameObject tc = other.transform.parent.parent.gameObject;
                //if (tc.transform.childCount >= 0)
                //{
                //    for (int i = 0; i < tc.transform.childCount; i++)
                //    {
                //        tc.transform.GetChild(i).GetChild(0).GetComponent<Rigidbody>().AddForce(Vector3.forward * 2f);
                //        tc.transform.GetChild(i).GetChild(0).GetComponent<Rigidbody>().AddForce(Vector3.up * 0.2f);
                //    }
                //}
            }
            else if (other.gameObject.CompareTag("Track"))
            {
                // print("Track");
                //  gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else if (other.gameObject.CompareTag("AICarCollider"))
            {
                // print("Collide");
                SplineController SC = other.gameObject.transform.parent.GetComponent<SplineController>();
                SC.Speed = SC.Speed+20f;
                // other.gameObject.GetComponent<AICarMovement>().explosionEmitter.Emit(200);
               ContactPoint cp= other.contacts[0];
                VL_Input.hit_Effect.transform.position = cp.point;
                VL_Input.hit_Effect.Play();
                SoundManager.instance.PlaySound(0);
                StartCoroutine(OppositCarSpeed(SC));
            }
            else if (other.gameObject.CompareTag("Plane"))
            {               
                ContactPoint cp = other.contacts[0];
                VL_Input.hit_Effect.transform.position = cp.point;
                VL_Input.hit_Effect.Play();
                SoundManager.instance.PlaySound(0);
                VL_Input.Trigger("Plane");
            }
            if (other.gameObject.CompareTag("Hurdle"))
            {
                // print("Collide");
                SoundManager.instance.PlaySound(0);
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                VL_Input.Trigger("Hurdle");
            }           
        }
        IEnumerator OppositCarSpeed(SplineController SC)
        {
            yield return new WaitForSeconds(2f);
            SC.Speed = SC.Speed -15f;
        }
        private void OnCollisionExit(Collision other)
        {
             if (other.gameObject.CompareTag("Track"))
            {
               // print("Track");
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }
}