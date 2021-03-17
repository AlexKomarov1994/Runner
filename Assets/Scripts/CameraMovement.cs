using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;//точка за которой мы будем следить

    Vector3 startDistance, moveVec;
    
    void Start()
    {
        //разница своей позиции и позиции таргета (отступ камеры от игрока)
        startDistance = transform.position - target.position;
    }

   
    void Update()
    {
        moveVec = target.position + startDistance;
        moveVec.z = target.position.z+startDistance.z;
        moveVec.y = startDistance.y;

        transform.position = moveVec;
    }
}
