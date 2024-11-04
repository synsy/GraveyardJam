using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public int monsterCount = 1;
    public float spawnRadius = 2.5f;

    void Start()
    {
        for (int i = 0; i < monsterCount; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            Instantiate(monsterPrefab, spawnPosition, Quaternion.identity, transform);
        }
    }

}
