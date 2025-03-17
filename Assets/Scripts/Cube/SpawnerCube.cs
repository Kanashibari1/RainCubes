using System;
using System.Collections;
using UnityEngine;

public class SpawnerCube : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private SpawnerBomb _spawnerBomb;

    private WaitForSeconds _waitForSeconds;
    public event Action ViewUpdated;

    private float _minRandomPosition = -15f;
    private float _maxRandomPosition = 15f;
    private int _positionY = 15;
    private int _time = 2;

    public ObjectPool<Cube> Pool { get; private set; }

    public int ActiveCubeCount { get; private set; }

    public int SpawnedCubes { get; private set; }

    private void Awake()
    {
        Pool = new ObjectPool<Cube>(_cubePrefab);

        StartCoroutine(SpawnRoutine());
    }

    public IEnumerator SpawnRoutine()
    {
        _waitForSeconds = new(_time);

        while (enabled)
        {
            if (Pool != null)
            {
                Cube cube = Pool.GetObj(_cubePrefab);
                OnGet(cube);

                yield return _waitForSeconds;
            }
        }
    }

    public void OnGet(Cube cube)
    {
        cube.CubeDeactivated += Release;
        cube.transform.position = new Vector3
            (UnityEngine.Random.Range(_minRandomPosition, _maxRandomPosition), _positionY,
            UnityEngine.Random.Range(_minRandomPosition, _maxRandomPosition));
        cube.gameObject.SetActive(true);

        SpawnedCubes++;
        ActiveCubeCount++;
        ViewUpdated.Invoke();
    }

    public void Release(Cube cube)
    {
        cube.CubeDeactivated -= Release;
        cube.gameObject.SetActive(false);
        _spawnerBomb.OnGet(cube);
        Pool.Return(cube);
        ActiveCubeCount--;
        ViewUpdated.Invoke();
    }
}
