using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour, IItem
{
    public int ammo = 30;

    public void Use(GameObject target)
    {
        // taret�� ź�� �߰��ϴ� ó��
        Debug.Log("ź���� �����ߴ� : ");
    }

    
}
