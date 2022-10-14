using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                Debug.Log("*");
            }
        }
    }
}
