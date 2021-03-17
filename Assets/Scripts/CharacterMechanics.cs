using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMechanics : MonoBehaviour
{
    //Основные параметры
    public float speedMove;//скорость персонажа   
    public bool canPlay = true;
    public GameObject starAnimationObject;//объект анимации vfx
    public bool ObstacleDie = true;//Умирать ли от препядствий
    

    //Параметры геймплэйя для персонажа
    private float gravityForce;
    private Vector3 moveVector;
    private Vector3 startPlayerPos;
   
    //ссылки на компоненты
    private CharacterController ch_controller;
    private Animator ch_animator;
    private MobileController mContr;
    public MyGameManager gameManager;

    private void Start()
    {
        startPlayerPos = transform.position;
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
        mContr = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileController>();
    }

    private void Update()
    {
        CharacterMove();//Вызываем метод перемещения персонажа
    }


    public void reloadGame()
    {
        canPlay = false;
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
        //перемещение по поверхности
        moveVector.x = mContr.Horizontal() * speedMove;
        moveVector.z = mContr.Vertical() * speedMove;

        //анимация движения персонажа
        if (moveVector.x != 0 || moveVector.z != 0)
        {
            ch_animator.SetBool("run", true);
            

        }
        else
        {
            ch_animator.SetBool("run", false);
        }


        //поворот персонажа в сторону направления перемещения
        if (Vector3.Angle(Vector3.forward, moveVector) > 1)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }


        ch_controller.Move(moveVector * Time.deltaTime);//метод передвижения персонажа по направлению


    }

   
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("NonEnemy"))//Если сталкивается не с врагом, то уходим
            return;
        if (!ObstacleDie)//Умирать ли от препядствий
        {
            if (hit.gameObject.CompareTag("Obstacle"))//Если сталкнется с препятствием - не умрет. (для тестов. Убрать)
                return;
        }
          
            print("Столкновение: герой");
            gameManager.loose();
        
    }

    private void OnTriggerEnter(Collider other)//при столкновении объекта с тригеером
    {
        if (!ObstacleDie)//Умирать ли от препядствий
        {
            if (other.gameObject.CompareTag("Obstacle"))//Если сталкнется с препятствием - не умрет. (для тестов. Убрать)
                return;
        }
            if (other.gameObject.CompareTag("Star"))//если это звезда
        {
            other.gameObject.SetActive(false);//Убираем звезду, которую взяли
            
            //проигрываем в этом месте vfx анимацию
            starAnimationObject.SetActive(true);
            var vfx = Instantiate(starAnimationObject);
            vfx.transform.position = transform.position;
            Destroy(vfx, 1f);
            starAnimationObject.SetActive(false);
            //добавляем балл
            gameManager.addPoints(1);
        }
        else//иначе столкнулся с препядствием
        {
           
                print("Столкновение: герой");
                gameManager.loose();
            
        }
    }
    
    
    
    /// <summary>
    /// Анимация при проигрыше
    /// </summary>
    public void Loose()//метод проигрыша
    {
        canPlay = false;
        ch_animator.SetBool("run", false);
        ch_animator.SetTrigger("death");
    }
    

   
}
