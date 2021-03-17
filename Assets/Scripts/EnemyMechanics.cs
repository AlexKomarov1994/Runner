using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMechanics : MonoBehaviour
{
    //Основные параметры
    public float speedMove;//скорость врага    
   

    //Параметры геймплэйя для персонажа    
    private Vector3 moveVector;
    private Vector3 startPlayerPos;
    private bool canPlay = true;

    //ссылки на компоненты
    private Animator ec_animator;
    private CharacterController ch_controller;  
    public MyGameManager gameManager;

    private void Start()
    {
        startPlayerPos = transform.position;
        ch_controller = GetComponent<CharacterController>();
        ec_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CharacterMove();//Вызываем метод перемещения персонажа
    }



    public void reloadGame()
    {
        print("reloadEnemy");
        canPlay = false;
        ec_animator.enabled = true;
        
        transform.position = startPlayerPos;
       
    }


    //метод перемещения персонажа
    private void CharacterMove()
    {
        if (!canPlay)
        {
            if (transform.position == startPlayerPos)
                canPlay = true;
            return;
        }

        moveVector = Vector3.zero;//обнуляем вектор направления движения (чтоб небыло случайных ошибок)
       
        moveVector.z = -1 * speedMove;  

        ch_controller.Move(moveVector * Time.deltaTime);//метод передвижения персонажа по направлению


    }   

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("NonEnemy") || hit.gameObject.CompareTag("Obstacle"))//Если сталкивается не с врагом, то уходим
            return;

        if (canPlay)
        {
            canPlay = false;
            print("Столкновение: враг"+ hit.gameObject);
            ec_animator.enabled = false;            
            gameManager.loose();
        }
        
    }




}
