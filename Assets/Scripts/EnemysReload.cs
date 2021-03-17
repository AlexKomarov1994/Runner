using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysReload : MonoBehaviour
{
    //массив врагов
    public EnemyMechanics[] EC;

    public void Reload()
    {
        for (int i = 0; i < EC.Length; i++)
        {
            EC[i].reloadGame();//перзагружаем врагов
        }
    }
}
