using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI killCountText;
    [SerializeField] Tilemap groundTilemap; // Adicione esta linha
    static EnemySpawner instance;
    public List<GameObject> enemyList = new List<GameObject>(500);
    const float maxX = 10;
    const float maxY = 16;
    const float DecreseSpawnDelayTime = 0.8f;
    float spawnDelay;
    int stage;
    int killCount;
    public List<GameObject> listaMonstro;
    public int QuantidadeInimigos;
    GameObject newEnemy = null;
    int spawnedBosses = 0;
    private EnemySpawner() { }
    
    enum Direction
    {
        North,
        South,
        West,
        East
    }

    void Awake()
    {
        Initialize();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(listChecker());
    }

    void Initialize()
    {
        instance = this;
        spawnDelay = 0.5f;
        stage = 1;
        killCount = 0;
    }

    IEnumerator SpawnEnemy()
    {
        


            GameObject newEnemy= null;

            while (true)
            {
                switch (stage)
                {
                    default:
                    case 1:
                    if (QuantidadeInimigos < 100)
                    {
                        newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.FlyingEye);
                        listaMonstro.Add(newEnemy);
                        QuantidadeInimigos++;
                    }
                        break;
                    case 2:
                        newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.Goblin);
                        listaMonstro.Add(newEnemy);
                    QuantidadeInimigos++;
                    break;
                    case 3:
                        newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.Mushroom);
                        listaMonstro.Add(newEnemy);
                    QuantidadeInimigos++;
                    break;
                    case 4:
                    case 5:
                        newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.Skeleton);
                        listaMonstro.Add(newEnemy);
                    QuantidadeInimigos++;
                    break;
                case 6:
                    int bossesToSpawn = 8; // Quantidade de Bosses a serem gerados por iteração
                    while (spawnedBosses < 15 && bossesToSpawn > 0)
                    {
                        newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.Boss);
                        listaMonstro.Add(newEnemy);
                        QuantidadeInimigos++;
                        spawnedBosses++;
                        newEnemy.transform.position = RandomPosition();
                        newEnemy.SetActive(true);
                        enemyList.Add(newEnemy);
                        bossesToSpawn--;
                    }
                    if (QuantidadeInimigos <= 1)
                    {
                        stage = 1;
                    }
                    break;
            }

            if (stage != 6) { 
                newEnemy.transform.position = RandomPosition();
                newEnemy.SetActive(true);
                enemyList.Add(newEnemy);
            }


            if (stage == 6)
                {
                    newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.FlyingEye);
                    newEnemy.transform.position = RandomPosition();
                    newEnemy.SetActive(true);
                    enemyList.Add(newEnemy);
                }

                yield return new WaitForSeconds(spawnDelay);
            }
        

    }


    Vector3 RandomPosition()
    {
        Vector3 pos = new Vector3();
        BoundsInt bounds = groundTilemap.cellBounds;

        while (true)
        {
            int x = Random.Range(bounds.xMin, bounds.xMax);
            int y = Random.Range(bounds.yMin, bounds.yMax);
            Vector3Int cellPosition = new Vector3Int(x, y, 0);

            if (groundTilemap.HasTile(cellPosition))
            {
                pos = groundTilemap.CellToWorld(cellPosition) + groundTilemap.cellSize / 2;
                break;
            }
        }

        return pos;
    }

    public Vector2 GetNearestEnemyPosition()
    {
        float[] min = {0, int.MaxValue};

        for(int i = 0; i < enemyList.Count; i++) { 
            if(min[1] > (enemyList[i].transform.position - Player.GetInstance().GetPosition()).sqrMagnitude)
            {
                min[0] = i;
                min[1] = (enemyList[i].transform.position - Player.GetInstance().GetPosition()).sqrMagnitude;
            }
        }

        return enemyList[(int)min[0]].transform.position;
    }

    public Vector2 GetRandomEnemyPosition()
    {
        int random = Random.Range(0, enemyList.Count);

        return enemyList[random].transform.position;
    }

    IEnumerator listChecker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            for(int i=0; i<enemyList.Count; i++)
            {
                if(!enemyList[i].activeSelf)
                    enemyList.RemoveAt(i);
            }
        }
    }

    public void IncreaseStage()
    {
        ++stage;

        switch (stage)
        {
            case 3:
            case 4:
                spawnDelay *= DecreseSpawnDelayTime;
                break;
        }
    }
    
    public void IncreaseKillCount()
    {
        ++killCount;

        QuantidadeInimigos--;
        killCountText.text = killCount.ToString();
    }

    public static EnemySpawner GetInstance()
    {
        return instance;
    }

    public int GetListCount()
    {
        return enemyList.Count;
    }
}
