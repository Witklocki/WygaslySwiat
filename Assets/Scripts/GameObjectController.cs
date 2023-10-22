using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectController : MonoBehaviour
{

    public GameObject enemyNormal;
    public GameObject enemyBoomer;
    public int enemyCounter;

    private Transform playerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
        StartCoroutine(SpawnEnemy()); 
    }

    IEnumerator SpawnEnemy()
    {
        while (enemyCounter < 10)
        {
            int number = Random.Range(0, 100);
            if(number < 85)
            {
                Instantiate(enemyNormal, RandomPointGenerator.RandomPointGenerate(playerPosition.position, 100), Quaternion.identity);
            }
            else
            {
                Instantiate(enemyBoomer, RandomPointGenerator.RandomPointGenerate(playerPosition.position, 100), Quaternion.identity);
            }
            yield return new WaitForSeconds(0.2f);
            enemyCounter++;
        }
    }
}
