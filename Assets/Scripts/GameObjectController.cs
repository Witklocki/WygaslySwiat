using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectController : MonoBehaviour
{
    public int maxEnemysOnMap;

    public GameObject enemyNormal;
    public GameObject enemyBoomer;
    public int enemyCounter;
    public int enemyCounterDestroyer;
    public List<GameObject> enemyList;

    private Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        //wyci¹ganie max enemy z mapData
        maxEnemysOnMap = (int)GameObject.FindObjectOfType<Map>().map.maxEnemy;
        enemyList = new List<GameObject>();
        playerPosition = GameObject.FindWithTag("Player").transform;
        StartCoroutine(SpawnEnemy());
        Debug.Log(maxEnemysOnMap);
    }

    private void Update()
    {
        StartCoroutine(DestroyeEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (enemyCounter < maxEnemysOnMap)
        {
            enemyList.Add(createObject());
            enemyCounter++;
        }
        yield return new WaitForSeconds(0.2f);

    }

    IEnumerator DestroyeEnemy()
    {

        for (int item = 0; item < maxEnemysOnMap; item++)
        {
            if (this.enemyList[item].GetComponent<EnemyAI>().isEnemyDead)
            {
                enemyCounter--;
                enemyList[item].GetComponent<EnemyAI>().EnemyDead();
                this.enemyList[item] = createObject();
            }

        }
        yield return new WaitForSeconds(0.2f);
    }

    GameObject createObject()
    {

        int number = Random.Range(0, 100);
        if (number < 85)
        {
            return Instantiate(enemyNormal, RandomPointGenerator.RandomPointGenerate(playerPosition.position, 100), Quaternion.identity);
        }
        else
        {
            return (Instantiate(enemyBoomer, RandomPointGenerator.RandomPointGenerate(playerPosition.position, 100), Quaternion.identity));
        }

    }
}
