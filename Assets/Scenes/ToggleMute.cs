using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMute : MonoBehaviour
{
   public void Print(bool value)
    {
        if (value)
        {
            Debug.Log("Suara dimatikan");
        }
        else
        {
            Debug.Log("Suara diaktifkan");
        }
    }   

}
