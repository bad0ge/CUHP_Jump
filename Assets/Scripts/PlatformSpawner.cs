using System;
using System.Collections;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private int _numPlatformsToSpawn = 5;
    [SerializeField] private GameObject _firstPlatform;
    [SerializeField] private float _maxDistanceFromCameraBeforeSpawn = 5f;
    [SerializeField] private float _minDistanceFromPreviousPlatform = 1.5f;
    [SerializeField] private float _maxDistanceFromPreviousPlatform = 3f;
    [SerializeField] private float _xSpawnRange = 3f;
    [SerializeField] private int _poolSize = 50;
    

    private GameObject _lastSpawnedPlatform;

    private void Start()
    {
        _lastSpawnedPlatform = _firstPlatform;
        SpawnPlatforms();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Camera.main.transform.position.y > _lastSpawnedPlatform.transform.position.y + _maxDistanceFromCameraBeforeSpawn) SpawnPlatforms();
    }

    private void SpawnPlatforms()
    {
        for (int i = 0; i < _numPlatformsToSpawn; i++)
        {
            float distanceToNextPlatform = UnityEngine.Random.Range(_minDistanceFromPreviousPlatform, _maxDistanceFromPreviousPlatform);
            float xPosition = UnityEngine.Random.Range(-_xSpawnRange, _xSpawnRange);

            Vector2 spawnPosition = new Vector2(xPosition, _lastSpawnedPlatform.transform.position.y + distanceToNextPlatform);

            _lastSpawnedPlatform = Instantiate(_platformPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
