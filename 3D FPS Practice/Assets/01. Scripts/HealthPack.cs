using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour,IItem
{
    public float health = 50;

    public void Use(GameObject target)
    {
        // tartget에 탄알을 회복하는 처리
        Debug.Log("체력을 회복했다 : " + health);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
