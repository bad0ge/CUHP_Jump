using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformSpawner2 : MonoBehaviour
{
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private int _poolSize = 15;
    [SerializeField] private GameObject _firstPlatform;
    [SerializeField] private float _screenEdgePadding = 0.5f;
    [SerializeField] private float _spawnAheadDistance = 10f;
    [SerializeField] private AnimationCurve _difficultyCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float _heightForMaxDifficulty = 500f;
    [SerializeField] private float _easyMinGap = 0.5f;
    [SerializeField] private float _easyMaxGap = 1.2f;
    [SerializeField] private float _hardMinGap = 2.5f;
    [SerializeField] private float _hardMaxGap = 3.5f;
    private Queue<GameObject> _platformPool;
    private float _highestPlatformY;
    private float _dynamicSpawnRange;
    private Camera _mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera = Camera.main;
        _platformPool = new Queue<GameObject>();
        float _screenRightEdge = _mainCamera.orthographicSize * _mainCamera.aspect;
        _dynamicSpawnRange = _screenRightEdge - _screenEdgePadding;

        _highestPlatformY = _firstPlatform != null ? _firstPlatform.transform.position.y : 0f;

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject plat = Instantiate(_platformPrefab);
            PositionPlatform(plat);
            _platformPool.Enqueue(plat);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_mainCamera.transform.position.y + _spawnAheadDistance > _highestPlatformY)
        {
            GameObject oldestPlatform = _platformPool.Dequeue();
            PositionPlatform(oldestPlatform);
            _platformPool.Enqueue(oldestPlatform);
        }
    }

    private void PositionPlatform(GameObject plat)
    {
        float progress = Mathf.Clamp01(_highestPlatformY / _heightForMaxDifficulty);
        float curveMultiplier = _difficultyCurve.Evaluate(progress);

        float currentMinGap = Mathf.Lerp(_easyMinGap, _hardMinGap, curveMultiplier);
        float currentMaxGap = Mathf.Lerp(_easyMaxGap, _hardMaxGap, curveMultiplier);
        float currentGap = UnityEngine.Random.Range(currentMinGap, currentMaxGap);


        float xPos = UnityEngine.Random.Range(-_dynamicSpawnRange, _dynamicSpawnRange);
        _highestPlatformY += currentGap;

        plat.transform.position = new Vector2(xPos, _highestPlatformY);
    }
}
