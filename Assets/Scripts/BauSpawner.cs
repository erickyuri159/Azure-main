using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BauSpawner : MonoBehaviour
{
    public GameObject bauPrefab; // Prefab do baú
    public float intervaloSpawn = 180f; // Intervalo de tempo para o próximo spawn
    public Vector2 areaSpawnMin; // Coordenadas mínimas da área de spawn
    public Vector2 areaSpawnMax; // Coordenadas máximas da área de spawn

    void Start()
    {
        StartCoroutine(SpawnBau());
    }

    IEnumerator SpawnBau()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloSpawn);

            Vector2 spawnPosition = new Vector2(
                Random.Range(areaSpawnMin.x, areaSpawnMax.x),
                Random.Range(areaSpawnMin.y, areaSpawnMax.y)
            );

            GameObject bau = Instantiate(bauPrefab, spawnPosition, Quaternion.identity);
            bau.GetComponent<BauController>().SetPodeSerDestruido(true);
        }
    }
}
