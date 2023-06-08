using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> monsters;

    [SerializeField] private GameObject[] monsterPrefabs;

    [SerializeField] private int maxMonster = 10;

    [SerializeField] private float spawnRate = 1f;

    [SerializeField] private bool isSpawning = true;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject[] spawnPoints;

    [SerializeField] private float spawnRange;

    


    private Camera cam;


    void Start()
    {
        List<GameObject> monsters = new List<GameObject>();
        cam = Camera.main;
        StartCoroutine(SpawnMonster());
    }

    void Update()
    {
        
    }

    private IEnumerator SpawnMonster(){
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        
        while(true){
            yield return wait;
            if (monsters.Count<maxMonster&&isSpawning){
                int rand = Random.Range(0,monsterPrefabs.Length);
                GameObject monsterToSpawn = monsterPrefabs[rand];

                GameObject spawnedMonster = Instantiate(monsterToSpawn, getSpawnPosition(), Quaternion.identity);
                spawnedMonster.GetComponent<AIDestinationSetter>().target = player.transform;
                spawnedMonster.GetComponent<Enemy>().player = player;

                monsters.Add(spawnedMonster);
            }
            
        }
    }
    
    

    private Vector3 getSpawnPosition(){
        
        int rand = Random.Range(0,spawnPoints.Length);
        Vector3 basePosition = spawnPoints[rand].transform.position;
        return new Vector3(basePosition.x+Random.Range(0,spawnRange), basePosition.y + Random.Range(0, spawnRange), 0);
    }

    public int GetMonsterCount()
    {
        return monsters.Count;
    }
    
}
