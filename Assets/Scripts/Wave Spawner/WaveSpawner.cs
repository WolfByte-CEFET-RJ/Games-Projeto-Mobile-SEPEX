using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Wave
{
    public string waveName;
    public int numOfEnemies;
    public GameObject[] typeOfEnemies;
    public GameObject boss;
    public float spawnInterval;
}
public class WaveSpawner : MonoBehaviour
{
    
    public Wave[] waves;
    public Transform[] spawnPoints;
    public GameObject botao, timer;
    public Timer x;

    private Wave currentWave;
    private int currentWaveNumber;

    int t = 0;
    private bool canSpawn=true;
    private float nextSpawnTime;
    
    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length)
        {
            OnBot();

        }
        
    }
    void OnBot()
    {
        if (t == 0)
        {
            botao.SetActive(true);
            t++;
        }
        
    }
    void OffBot()
    {
        botao.SetActive(false);
    }
    void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
        
    }
    public void ClickNext()
    {
        OffBot();      
        TimerOn();

    }
    public void TimerOn()
    {
        timer.SetActive(true);
        botao.SetActive(false);
        x.currentTime = 5;
        x.enabled=true;
        x.timerText.color = Color.yellow;
    }
    public void TimerOff()
    {
        timer.SetActive(false);
        SpawnNextWave();
    }
    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime<Time.time)
        {
            t=0;
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            GameObject waveBoss = currentWave.boss;
            currentWave.numOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.numOfEnemies == 1)
            {
                Instantiate(waveBoss, randomPoint.position, Quaternion.identity);
                currentWave.numOfEnemies--;
                

            }
            if (currentWave.numOfEnemies == 0)
            {
                canSpawn = false;
            }
        }
        

    }


}