using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int level = 0;
    [SerializeField] private int maxEnemySpawned = 0;
    public Transform[] spawnPoint;
    private bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        level = GameManager.Instance.level;
        maxEnemySpawned = GameManager.Instance.maxEnemySpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (level != 0 && !isSpawning && (GameManager.Instance.enemySpawned < maxEnemySpawned)) StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        isSpawning = true; // lagi ngespawn maka jangan spawn lagi

        // ambil lokasi acaknya dulu
        int randomLoc = GetRandomLoc();

        // cek level
        if (level == 1) // cuma coffin
        {
            InstantiateEnemy(GameManager.Instance.enemyPrefab[0], randomLoc);
            yield return new WaitForSeconds(1.5f);
        }
        else if (level == 2) // coffin sama cactus
        {
            // random enemynya
            int randEnemy = Random.Range(0, 2);

            // enemynya yg mn
            if (randEnemy == 0) InstantiateEnemy(GameManager.Instance.enemyPrefab[0], randomLoc);
            else if (randEnemy == 1) InstantiateEnemy(GameManager.Instance.enemyPrefab[1], randomLoc);

            yield return new WaitForSeconds(1f);
        }
        else if (level == 3) // coffin, cactus, dan coyote
        {
            // random enemynya
            int randEnemy = Random.Range(0, 3);

            // enemynya yg mn
            if (randEnemy == 0) InstantiateEnemy(GameManager.Instance.enemyPrefab[0], randomLoc);
            else if (randEnemy == 1) InstantiateEnemy(GameManager.Instance.enemyPrefab[1], randomLoc);
            else if (randEnemy == 2) InstantiateEnemy(GameManager.Instance.enemyPrefab[2], randomLoc);

            yield return new WaitForSeconds(0.5f);
        }

        GameManager.Instance.enemySpawned++; // increase the number of enemy being spawned
        isSpawning = false;
    }

    private int GetRandomLoc()
    {
        return Random.Range(0, 8); // ada 8 spawn location
    }

    private void InstantiateEnemy(GameObject enemy, int loc)
    {
        Instantiate(enemy, spawnPoint[loc].position, Quaternion.identity);
    }
}
