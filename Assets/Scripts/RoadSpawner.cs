using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] roadBlockPrefabs;//Массив префабов дороги
    public GameObject startBlock;//стартовый блок
    public GameObject startBlockPrefab;//префаб стартового блока

    float roadStart = 0;
    float blockZpos = 0;//Для хранения Х-позиции для генерирования блоков дороги
    int blocksCount = 1;//Хранится количество изначально генерируемых блоков (возможно удалить)
    float blockLength = 0;//Длина одного блока    

    public Transform playerTransform;//игрок (его позиция)
    List<GameObject> currentBlocks = new List<GameObject>();

    private void Start()
    {
        roadStart= startBlock.transform.position.z;
        currentBlocks.Add(startBlock);//добавляем стартовый блок
        blockZpos = startBlock.transform.position.z;
        blockLength = startBlock.GetComponent<BoxCollider>().bounds.size.z;//Определяем размер старотвого блока       
        for (int i = 0; i < blocksCount; i++)
        {
            SpawnBlock();
        }
    }

    private void Update()
    {
        if (playerTransform.position.z < blockZpos + blocksCount + 1 * blockLength / 1.5)//Магическая формула свевременного появления мира, которая все-таки сработала.
        {
            SpawnBlock();
            DestroyBlock();
        }
    }

   

    private void SpawnBlock()//создает новые блоки
    {
        GameObject block = Instantiate(roadBlockPrefabs[Random.Range(0, roadBlockPrefabs.Length)], transform);//Создание нового рандомного блока дороги
        blockZpos -= blockLength;
        block.transform.position = new Vector3(0, 0, blockZpos);//Переносим блок в нужно место
        currentBlocks.Add(block);//добавляем этот блок в список
        Debug.Log("блок добавлен. Всего блоков: " + currentBlocks.Count);
    }

    /// <summary>
    /// последовательное удаление блоков
    /// </summary>
    private void DestroyBlock()
    {
        Debug.Log("уничтожение блока: "+currentBlocks[0].ToString());
        Destroy(currentBlocks[0]);
        currentBlocks.RemoveAt(0);

    }
    /// <summary>
    /// удаление какого-то определенного блока
    /// </summary>
    /// <param name="numberDestrybleBlock">номер удаляемого блока</param>
    private void DestroyBlock(int numberDestrybleBlock)
    {
        Debug.Log("Уничтожение блока №" + numberDestrybleBlock);
        Destroy(currentBlocks[numberDestrybleBlock]);
        
    }

    

    public void RoadRestart()
    {
        print("Уничтожение всех блоков");
        for (int i = 0; i < currentBlocks.Count; i++)//Уничтожаем все блоки дороги
        {
            DestroyBlock(i);
        }
        currentBlocks.Clear();//После удаления всех блоков очищаем список
        GameObject block = Instantiate(startBlockPrefab, transform);//Создание стартового префаба
        currentBlocks.Add(block);//добавляем стартовый блок
        blockZpos = roadStart;//возвращаем позицию z к исходному значению        
        block.transform.position = new Vector3(0, 0, blockZpos);//Переносим блок в нужно место
        for (int i = 0; i < blocksCount; i++)
        {
            SpawnBlock();
        }
    }

}
