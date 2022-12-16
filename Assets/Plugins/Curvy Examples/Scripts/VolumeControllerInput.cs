// =====================================================================
// Copyright 2013-2018 ToolBuddy
// All rights reserved
// 
// http://www.toolbuddy.net
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace FluffyUnderware.Curvy.Examples
{
    public class VolumeControllerInput : MonoBehaviour
    {
      //  public TrackPoint track_Point;
        public float AngularVelocity = 0.2f;
        public ParticleSystem explosionEmitter;
        public VolumeController volumeController;
        public Transform rotatedTransform;
        public float maxSpeed = 40f;
        public float accelerationForward = 20f;
        public float accelerationBackward = 40f;
        private float X_POS;
        private float X_rot;
        public float pixetDistToDetect;
        public bool mGameOver;
        public static bool isStop;
        private bool IsPlay;
        public Text text_s;
        public Vector3 mov_Val;
        public Vector3 start_Pos;
        public Vector3 rot;
        bool isLeftRot, isRightRot;
        public CarMovement car_Movement;
        private void Awake()
        {
            if (!volumeController)
                volumeController = GetComponent<VolumeController>();
        }

        void Start()
        {
            if (volumeController.IsReady)
                ResetController();
            else
                volumeController.OnInitialized.AddListener(arg0 => ResetController());
        }
        public void UpAndDown(bool isTrue) 
        {
            start_Pos = Input.mousePosition;
            if (isTrue)
            {
                start_Pos = Input.mousePosition;
                IsPlay = true;
                mov_Val = new Vector3(0, 1, 0);
                volumeController.Speed = 30f;
                isStop = false;
              
                //  print("Down");
            }
            else
            {
                IsPlay = false;
                mov_Val = new Vector3(0, 0, 0);
                X_POS = 0f;
                isStop = true;
                StartCoroutine(SpeedTest());
               // print("Up");
            }

        }       
        public void Drag()
        {
            if (Input.mousePosition.x >= start_Pos.x + pixetDistToDetect)
            {
               // print("right_Drag");
                 X_POS = 0.5f;
                StartCoroutine(RotationEnd(true));
            }
            else if (Input.mousePosition.x <= start_Pos.x + pixetDistToDetect)
            {
                 X_POS = -0.5f;
                StartCoroutine(RotationEnd(false));
                // print("Left_Drag");
            }
        }
       
        IEnumerator RotationEnd(bool isTrue)
        {
            if (isTrue)
            {
                if (!isRightRot)
                {
                    isRightRot = true;
                    X_rot = 0.5f;
                    //while (X_rot<0.5)
                    //{
                    //    X_rot += 0.0000001f;
                    //}
                    rot = new Vector3(X_rot, 1, 0);
                    yield return new WaitForSeconds(0.5f);
                    rot = new Vector3(0, 1, 0);
                    yield return new WaitForSeconds(0.5f);
                    isRightRot = false;
                }
            }
            else 
            {
                if (!isLeftRot)
                {
                    isLeftRot = true;
                    X_rot = -0.5f;
                    //while (X_rot > -0.5)
                    //{
                    //    X_rot -= 0.0000001f;
                    //}
                    rot = new Vector3(X_rot, 1, 0);
                    yield return new WaitForSeconds(0.5f);
                    rot = new Vector3(0, 1, 0);
                    yield return new WaitForSeconds(0.5f);
                    isLeftRot = false;
                }
            }
        }
        private void ResetController()
        {
            IsPlay = false;
            volumeController.Speed = 0;       
            volumeController.RelativePosition = volumeController.RelativePosition-0.02f;
             volumeController.CrossRelativePosition = 0;
            car_Movement.transform.position = new Vector3(car_Movement.transform.position.x, car_Movement.transform.position.y+2f, car_Movement.transform.position.z);
            car_Movement.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            car_Movement.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void Update()
        {
            if (IsPlay)
            {
                if (volumeController && !mGameOver)
                {
                    mov_Val = new Vector3(X_POS, 1, 0);
                    if (volumeController.PlayState != CurvyController.CurvyControllerState.Playing) volumeController.Play();
                    Vector2 input = mov_Val; //new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;                                                                                   
                    float speedRaw = volumeController.Speed + input.y * Time.deltaTime * Mathf.Lerp(accelerationBackward, accelerationForward, (input.y + 1f) / 2f);

                    volumeController.Speed = Mathf.Clamp(speedRaw, 0f, maxSpeed);
                    volumeController.CrossRelativePosition += AngularVelocity * Mathf.Clamp(volumeController.Speed / 10f, 0.2f, 1f) * input.x * Time.deltaTime;
                    if (rotatedTransform)
                    {
                        //  print("input " + input);
                       // float yTarget = Mathf.Lerp(-25f, 25f, (rot.x + 1f) / 2f);
                       // rotatedTransform.localRotation = Quaternion.Euler(0f, yTarget, 0f);
                    }
                }
            }
        }

        IEnumerator SpeedTest()
        {
            yield return new WaitForSeconds(0.01f);
            if (volumeController.Speed > 0&& mov_Val.y!=1)
            {
                volumeController.Speed -= 0.5f;
                StartCoroutine(SpeedTest());
            }
        }
        public void Trigger()
        {
            if (mGameOver == false)
            {
                explosionEmitter.Emit(200);
                volumeController.Pause();
                mGameOver = true;
                Invoke("StartOver", 1);
            }
        }
        //public void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.CompareTag("TrackPoint"))
        //    {
        //        print("hit");
        //        volumeController.Speed = 0;
        //        //if (mGameOver == false)
        //        //{
        //        //    explosionEmitter.Emit(200);
        //        //    volumeController.Pause();
        //        //    mGameOver = true;
        //        //    Invoke("StartOver", 1);
        //        //}
        //    }
        //}

        private void StartOver()
        {

            ResetController();
            mGameOver = false;
        }
        public void Restart()
        {

            SceneManager.LoadScene(0);
        }

        //        private void JumpOrSwipe()
        //        {
        //#if UNITY_EDITOR
        //            if (fingerDown == false && Input.GetMouseButtonDown(0))
        //            {
        //                start_Pos = Input.mousePosition;
        //                fingerDown = true;
        //            }
        //            if (fingerDown)
        //            {
        //                if (Input.mousePosition.y >= start_Pos.y + pixetDistToDetect)
        //                {
        //                    fingerDown = false;
        //                    Jump();
        //                    // Debug.Log("Swipe Up");
        //                }
        //                else if (Input.mousePosition.y <= start_Pos.y - pixetDistToDetect)
        //                {
        //                    fingerDown = false;
        //                    Slide();
        //                    // Debug.Log("Swipe Down");
        //                }
        //            }
        //            if (fingerDown && Input.GetMouseButtonUp(0))
        //            {
        //                fingerDown = false;
        //            }
        //#else
        //        if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        //        {
        //            start_Pos = Input.touches[0].position;
        //            fingerDown = true;
        //        }
        //        if (fingerDown)
        //        {
        //            if (Input.touches[0].position.y >= start_Pos.y + pixetDistToDetect)
        //            {
        //                fingerDown = false;
        //                Jump();
        //               // ui_Text.text = "Swipe Up";
        //            }
        //            else if (Input.touches[0].position.y <= start_Pos.y - pixetDistToDetect)
        //            {
        //                fingerDown = false;
        //                Slide();
        //               // ui_Text.text = "Swipe Down";
        //            }
        //        }
        //        if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        //        {
        //            fingerDown = false;
        //        }
        //#endif
        //        }
        //        public void Jump()
        //        {
        //            if (click < 2)
        //            {
        //                isJump = true;
        //                if (click == 0)
        //                {
        //                    audio_Source.enabled = false;
        //                    GameManager.instance.sound_Manager.PlaySound(0);
        //                    this.rb.AddForce(new Vector2(0, jetpackSpeed + 1.2f), ForceMode2D.Impulse);
        //                    StartCoroutine(DoubleJump());
        //                }
        //                else if (click == 1)
        //                {
        //                    if (!isDoubleJump)
        //                    {
        //                        GameManager.instance.sound_Manager.PlaySound(7);
        //                        this.rb.AddForce(new Vector2(0, jetpackSpeed), ForceMode2D.Impulse);
        //                        this.animator.SetBool("DoubleJump", true);
        //                        dbl_Jump_Effect.Play();
        //                    }
        //                }
        //                click++;
        //            }
        //        }
        //        public void Slide()
        //        {
        //            if (!isJump)
        //            {
        //                isSlide = true;
        //                StartCoroutine(SlideOff());
        //            }
        //        }
    }
}
