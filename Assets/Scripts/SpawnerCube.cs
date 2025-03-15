using System.Collections;
using TMPro;
using UnityEngine;

public class SpawnerCube : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private ObjectPool<Cube> _pool;
    private WaitForSeconds _waitForSeconds;
    private SpawnerBomb _spawnerBomb;

    private float _minRandomPosition = -15f;
    private float _maxRandomPosition = 15f;
    private int _positionY = 15;
    private int _time = 2;
    private int _capacity = 5;
    private int _spawnedCubes = 0;
    private int _activeCubeCount = 0;

    private void Awake()
    {
        _spawnerBomb = GetComponent<SpawnerBomb>();
        _pool = new ObjectPool<Cube>();

        for (int i = 0; i < _capacity; i++)
        {
            _pool.Create(_cubePrefab);
        }

        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        _textMeshPro.text = $"Кубов заспавнено: {_spawnedCubes}\n" +
                              $"Кубов создано: {_pool.CreatedObjectsCount}\n" +
                              $"Активные Кубы: {_activeCubeCount}";
    }

    public IEnumerator SpawnRoutine()
    {
        _waitForSeconds = new(_time);

        while (enabled)
        {
            if (_pool != null)
            {
                Cube cube = _pool.GetObj();

                if (cube != null)
                {
                    OnGet(cube);
                    yield return _waitForSeconds;
                }
                else
                {
                    _pool.Create(_cubePrefab);
                }
            }
        }
    }

    public void OnGet(Cube cube)
    {
        cube.OnCubeDeactivate += Release;
        cube.transform.position = new Vector3
            (Random.Range(_minRandomPosition, _maxRandomPosition), _positionY, 
            Random.Range(_minRandomPosition, _maxRandomPosition));
        cube.gameObject.SetActive(true);
        _spawnedCubes++;
        _activeCubeCount++;
    }

    public void Release(Cube cube)
    {
        cube.OnCubeDeactivate -= Release;
        cube.gameObject.SetActive(false);
        _spawnerBomb.OnGet(cube);
        _activeCubeCount--;
    }
}
