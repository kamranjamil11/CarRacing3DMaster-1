using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class CrossPromo : MonoBehaviour
{
    public Button[] btns;
    public Button r_btn;
    public int num;

    private void OnEnable()
    {
        pick();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pick()
    {
        Debug.Log("i am alive");
        num = Random.Range(0,3);
        for(int i = 0; i < btns.Length; i++)
        {
            if(i == num)
            {
                btns[i].gameObject.SetActive(true);
            }
            else
            {
                btns[i].gameObject.SetActive(false);
            }
        }
    }

    public void Promo(string id)
    {
        switch (id)
        {
            case "Bicycle":
                Application.OpenURL("https://play.google.com/store/apps/details?id=com.oneup.extreme.bike.bicycle.riding.race");
                break;
            case "MonsterTruck":
                Application.OpenURL("https://play.google.com/store/apps/details?id=com.monster.truck.mega.ramp.games");
                break;
            case "VirtualMom":
                Application.OpenURL("https://play.google.com/store/apps/details?id=com.oneupgaming.virtual.mom.newborn.baby.virtual.family.games");
                break;
        }
    }
}
