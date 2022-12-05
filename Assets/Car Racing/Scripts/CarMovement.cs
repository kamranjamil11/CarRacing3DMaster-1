using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.DevTools;
using FluffyUnderware.Curvy.Examples;
public class CarMovement : MonoBehaviour
{
    public Camera main_Camera,endPoint_Camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {

            main_Camera.gameObject.SetActive(false);
            //main_Camera.GetComponent<ChaseCam>().enabled = false;
            endPoint_Camera.gameObject.SetActive(true);
        }
    }
}
