using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum EnemyType
{
    Grunt,
    Mini,
    Mama,
    ChampionGrunt
}

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance = null;

    [Header("Enemy Prefabs")]
    // regular enemies
    public GameObject gruntPrefab;
    public GameObject miniPrefab;
    public GameObject mamaPrefab;
    // champions
    public GameObject champGruntPrefab;

    // container for enemyTypes and prefabs
    Dictionary<EnemyType, GameObject> enemyPrefabs;

    // Enemy tracker
    [HideInInspector] public List<GameObject> enemies;

    // Traversal references
    GameObject[] spawnPoints;
    [HideInInspector] public GameObject[] wayPoints;

    void Awake()
    {
        // Manage singleton instance
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    void Start()
    {
        // Get spawn points and way points
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");

        // store all enemy prefabs in easy-access dictionary
        enemyPrefabs = new Dictionary<EnemyType, GameObject>()
        {
            {EnemyType.Grunt, gruntPrefab},
            {EnemyType.Mini, miniPrefab},
            {EnemyType.Mama, mamaPrefab},
            {EnemyType.ChampionGrunt, champGruntPrefab}
        };
    }

    public void RemoveEnemy(GameObject enemy)
    {
        /*
        Remove dead enemy.
        */
        enemies = enemies.Where(item => item != enemy).ToList();

        // If no enemies remain, start next wave.
        if (enemies.Count == 0 && !WaveManager.instance.CanStartNextWave)
            WaveManager.instance.FinishWave();
    }

    // --------------------------------------------------------------------------------
    // Spawn logic
    //
    void SpawnEnemy(EnemyType enemyType, int enemyLevel, Vector3 spawnPosition)
    {
        /*
        Base spawn enemy function -- all others call this.
        */
        GameObject enemyPrefab = enemyPrefabs[enemyType];
        var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<EnemyController>().Initialize(enemyLevel);
        enemies.Add(enemy);
    }

    public void CreateEnemy(string enemyTypeStr, int enemyLevel)
    {
        /*
        Spawn enemy at one of the level's spawn points.
        Enemy name is based on a string, which is converted to an enum.
        */
        EnemyType enemyType = EnemyStr2Enum(enemyTypeStr);
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // get random spawn point
        SpawnEnemy(enemyType, enemyLevel, spawnPoint.transform.position);
    }

    public void SpawnEnemiesAtPos(EnemyType enemyType, Vector3 pos, int level, int numEnemies)
    {
        /*
        Spawn a given number of enemies at the given position.

        Used by Mama enemies to birth babies.
        */
        for (int i=0; i<numEnemies; i++)
            SpawnEnemy(enemyType, level, pos);
    }

    EnemyType EnemyStr2Enum(string enemyTypeStr)
    {
        // convert string to EnemyType enum
        return (EnemyType) System.Enum.Parse(typeof(EnemyType), enemyTypeStr);
    }
}
