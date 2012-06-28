/*
 * file: StorehouseManager.cs
 * last update: 2011/5/16  
 * version : 1.1
 * 
 * Ver1.0 -> Ver1.1����:
 * ��磌����o�覡�{���g�k�A���Ӥ�����Jtotal�A�R�W�]�����Τ@��(���R�W�٬O�n���㦳�N�q)

 * 
 * �Q�k�G
 * �z�Lvector3�}�C�A�@�}�l�x�s�Ҧ����󪺭�l��m
 * �Q��RestartObject()�禡�I�s�^�_�_�l��m(�Ω�}������)
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StorehouseManager : MonoBehaviour
{    
    public bool isRestart = false;

    private List<Vector3> OriginPosition = new List<Vector3>();
    private Transform[] trans;

    void Start()
    {
        trans = GetComponentsInChildren<Transform>();

        for (int i = 1; i < trans.Length; i++)        
            OriginPosition.Add(trans[i].position);
        
    }

    public void RestartObject()
    {
        for (int i = 1; i < trans.Length; i++)        
            trans[i].position = OriginPosition[i - 1];
        
    }

    void Update()
    {
        if (isRestart)
        {
            for (int i = 1; i < trans.Length; i++)
                trans[i].position = OriginPosition[i - 1];

            isRestart = false;
        }
    }
}
