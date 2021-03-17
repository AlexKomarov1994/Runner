using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour
{
    public GameObject win_panel;//Панель победы
    public Text points_onWinPanel;//Количество баллов на панеле выигрыша
    public GameObject joystick;//джостик
    public Text points_text;//Вывод баллов
    public CharacterMechanics CM;//управление персонажем   
    public EnemysReload ER;//класс методов для врагов
    public RoadSpawner RS;//Класс методов для дороги
   
   

    //параметры для контроллера игры
    private bool canReload = true;
    private int points_box;//для баллов

    /// <summary>
    /// метод при проигрыше
    /// </summary>
    private void ShowResult()//если мы столкнулись с собъектом
    {
        
       joystick.SetActive(false);
        points_onWinPanel.text = points_box.ToString();
        win_panel.SetActive(true);

    }
  

    /// <summary>
    /// Перезапуск игры
    /// </summary>
    public void StratNewGame()//вызов новой игры (при нажатии на кнопку рестарта
    {
        canReload = true;
        print("перезапуск");
        joystick.SetActive(true);
        win_panel.SetActive(false);
        ER.Reload();//перезагружаем врагов
        RS.RoadRestart();//перезагружаем дорогу
        points_box = 0;
        addPoints(0);
        

        CM.reloadGame();//перезагружаем персонажа (его местоположение)
       
      

    }
    /// <summary>
    /// метод при проигрыше
    /// </summary>
    public void loose()//метод, который вызывается откуда угодно при проигрыше
    {
        print("GameManager:loose");
        if (canReload)
        {
            canReload = false;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {

        CM.Loose();//метод при проигрыше игрока
        yield return new WaitForSeconds(2.2f);
        ShowResult();
    }
    /// <summary>
    /// добавление баллоы
    /// </summary>
    /// <param name="points">количество баллов</param>
    public void addPoints(int points)
    {
        points_box += points;
        points_text.text = points_box.ToString();
    }
}
