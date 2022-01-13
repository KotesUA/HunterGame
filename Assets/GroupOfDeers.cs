using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupOfDeers : MonoBehaviour
{
    public event Action<Vector3> ChangeTargetEvent;

    [SerializeField]
    private Deer _prefab;

    [SerializeField]
    private int _maxDistance;

    [SerializeField]
    private int _maxSpawnDistance;

    public void StartSpawn(int count)
    {
        SpawnDeers(count);
        SetNewDestination();
        StartCoroutine(StartCountdown());

    }

    public IEnumerator StartCountdown()
    {
        while (true)
        {
            SetNewDestination();
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void SpawnDeers(int count)
    {
        var position = transform.position;
        for (int i = 0; i < count; i++)
        {
            var myVector = new Vector3(UnityEngine.Random.Range(-_maxSpawnDistance, _maxSpawnDistance),
                UnityEngine.Random.Range(-_maxSpawnDistance, _maxSpawnDistance), 0);
            position += myVector;
            var deer = Instantiate(_prefab, position, Quaternion.identity);
            deer.SetGroup(this);
        }
    }

    private void SetNewDestination()
    {
        ChangeTargetEvent.Invoke(new Vector2(UnityEngine.Random.Range(-_maxSpawnDistance, _maxSpawnDistance),
                UnityEngine.Random.Range(-_maxSpawnDistance, _maxSpawnDistance)));
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
