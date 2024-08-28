using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    public GameObject slot1, slot2;

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Equip1();
        }

        if (Input.GetKeyDown("2"))
        {
            Equip2();
        }
    }

    void Equip1()
    {
        slot1.SetActive(true);
        slot2.SetActive(false);
    }

    void Equip2()
    {
        slot1.SetActive(false);
        slot2.SetActive(true);
    }
}
//https://www.youtube.com/watch?v=RyzFwix15Dg