using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject enemy;

    public float timer;

    public int enemyAmt;
    void Start()
    {
        enemyAmt = FindObjectsOfType<EnemyController>().Length;
        foreach(Transform t in spawnPoints)
        {
            Instantiate(enemy, t.position, t.rotation);
        }
    }
    void Update()
    {
        enemyAmt = FindObjectsOfType<EnemyController>().Length;
        if(enemyAmt<=0)
        {
            foreach (Transform t in spawnPoints)
            {
                Instantiate(enemy, t.position, t.rotation);
            }
        }
        timer += Time.deltaTime;
        if(timer>15.0f)
        {
            timer = 0f;
        }
    }
}
