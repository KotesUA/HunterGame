using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private int _wolfCount;
    [SerializeField]
    private Wolf _wolf;

    [SerializeField]
    private int[] _deerCount;
    [SerializeField]
    private GroupOfDeers _groupOfDeers;

    [SerializeField]
    private int _rabbitCount;
    [SerializeField]
    private Rabbit _rabbit;

    [SerializeField]
    private int _maxDistance;

    void Start()
    {
        SpawnDeer(_deerCount);
        SpawnRabbits(_rabbitCount);
        SpawnWolfes(_wolfCount);
    }

    private void SpawnWolfes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(_wolf, new Vector3(Random.Range(-_maxDistance, _maxDistance), Random.Range(-_maxDistance, _maxDistance), 1), Quaternion.identity);
        }
    }

    private void SpawnDeer(int[] count)
    {
        for (int i = 0; i < count.Length; i++)
        {
            var group = Instantiate(_groupOfDeers, new Vector3(Random.Range(-_maxDistance, _maxDistance), Random.Range(-_maxDistance, _maxDistance), 1), Quaternion.identity);
            group.StartSpawn(count[i]);
        }
    }

    private void SpawnRabbits(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(_rabbit, new Vector3(Random.Range(-_maxDistance, _maxDistance), Random.Range(-_maxDistance, _maxDistance), 1), Quaternion.identity);
        }
    }
}
