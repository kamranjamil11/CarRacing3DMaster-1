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
            print("Camera_Change");
            main_Camera.gameObject.SetActive(false);
            main_Camera.GetComponent<ChaseCam>().enabled = false;
            endPoint_Camera.gameObject.SetActive(true);
        }
        else
            if (other.gameObject.CompareTag("Finish"))
        {         
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward*3);
            GameObject tc = other.transform.parent.parent.gameObject;
            for (int i = 0; i < tc.transform.childCount; i++)
            {
                tc.transform.GetChild(i).GetChild(0).GetComponent<Rigidbody>().AddForce(Vector3.forward *2f);
                tc.transform.GetChild(i).GetChild(0).GetComponent<Rigidbody>().AddForce(Vector3.up *0.2f);
            }
          
        }
    }
}
