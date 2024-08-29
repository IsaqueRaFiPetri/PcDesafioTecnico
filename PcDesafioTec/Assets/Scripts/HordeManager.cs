using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HordeManager : MonoBehaviour
{
    public GameObject[] enemyPrefab; // Prefab dos inimigos
    public Transform[] spawnPoints; // Pontos de spawn dos inimigos
    public int initialEnemiesPerWave = 5; // N�mero inicial de inimigos por horda
    public int waveEnemies; // N�mero inicial de inimigos por horda
    public float timeBetweenWaves = 5f; // Tempo entre as hordas
    public float spawnDelay = 0.5f; // Intervalo de spawn entre cada inimigo
    public static int enemiesKilled;

    private int waveNumber = 0; // Contador de hordas

    void Start()
    {
        StartCoroutine(SpawnHorde());
    }
    private void Update()
    {
        waveEnemies = initialEnemiesPerWave * waveNumber;

        if(waveNumber == 37)
        {
            SceneManager.LoadScene(0);
        }
    }
    IEnumerator SpawnHorde()
    {
        while (true)
        {
            
            waveNumber++; // Incrementa o n�mero da horda
            int enemiesToSpawn = waveEnemies; // Calcula o n�mero de inimigos para a horda atual

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnDelay); // Aguarda um tempo antes de spawnar o pr�ximo inimigo
            }

            yield return new WaitForSeconds(timeBetweenWaves); // Aguarda antes de iniciar a pr�xima horda
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Escolhe um ponto de spawn aleat�rio
        Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], spawnPoint.position, spawnPoint.rotation); // Instancia o inimigo no ponto de spawn
    }
}
