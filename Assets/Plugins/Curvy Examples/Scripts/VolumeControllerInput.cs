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

namespace FluffyUnderware.Curvy.Examples
{
    public class VolumeControllerInput : MonoBehaviour
    {
        public float AngularVelocity = 0.2f;
        public ParticleSystem explosionEmitter;
        public VolumeController volumeController;
        public Transform rotatedTransform;
        public float maxSpeed = 40f;
        public float accelerationForward = 20f;
        public float accelerationBackward = 40f;
        public bool mGameOver;
        private bool IsPlay;
        public Text text_s;
        public Vector3 mov_Val;
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

        private void ResetController()
        {
            volumeController.Speed = 0;       
            volumeController.RelativePosition = volumeController.RelativePosition-0.02f;
             volumeController.CrossRelativePosition = 0;
        }

        private void Update()
        {
            if (!mGameOver)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    IsPlay = true;
                    mov_Val = new Vector3(0, 1, 0);
                    volumeController.Speed = 40f;
                }
                if (IsPlay)
                {
                    if (volumeController /*&& !mGameOver*/)
                    {
                        if (volumeController.PlayState != CurvyController.CurvyControllerState.Playing) volumeController.Play();
                        Vector2 input = mov_Val; //new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
                                                 // text_s.text = "Playing";
                        print("input " + input);
                        float speedRaw = volumeController.Speed + input.y * Time.deltaTime * Mathf.Lerp(accelerationBackward, accelerationForward, (input.y + 1f) / 2f);

                        volumeController.Speed = Mathf.Clamp(speedRaw, 0f, maxSpeed);
                        volumeController.CrossRelativePosition += AngularVelocity * Mathf.Clamp(volumeController.Speed / 10f, 0.2f, 1f) * input.x * Time.deltaTime;


                        if (rotatedTransform)
                        {
                            float yTarget = Mathf.Lerp(-25f, 25f, (input.x + 1f) / 2f);
                            rotatedTransform.localRotation = Quaternion.Euler(0f, yTarget, 0f);
                        }
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    IsPlay = false;
                    mov_Val = new Vector3(0, 0, 0);
                    StartCoroutine(SpeedTest());
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

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Hurdle"))
            {
                if (mGameOver == false)
                {
                    explosionEmitter.Emit(200);
                    volumeController.Pause();
                    mGameOver = true;
                    Invoke("StartOver", 1);
                }
            }
        }

        private void StartOver()
        {

            ResetController();
            mGameOver = false;
        }
    }
}
