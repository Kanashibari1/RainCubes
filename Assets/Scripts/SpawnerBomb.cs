using TMPro;
using UnityEngine;

public class SpawnerBomb : MonoBehaviour
{
    [SerializeField] private Bomb _prefab;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private ObjectPool<Bomb> _pool;

    private int _capacity = 5;
    private int _spawnedBombs = 0;
    private int _activeBombCount = 0;

    private void Awake()
    {
        _pool = new ObjectPool<Bomb>();
    }

    private void Start()
    {
        for (int i = 0; i < _capacity; i++)
        {
            _pool.Create(_prefab);
        }
    }

    private void Update()
    {
        _textMeshPro.text = $"Бомб заспавнено: {_spawnedBombs}\n" +
                              $"Бомб создано: {_pool.CreatedObjectsCount}\n" +
                              $"Активные бомбы: {_activeBombCount}";
    }

    public void OnGet(Cube cube)
    {
        Bomb bomb = _pool.GetObj();

        if (bomb != null)
        {
            bomb.OnBombDeactivate += Release;
            bomb.transform.position = cube.transform.position;
            bomb.gameObject.SetActive(true);
            bomb.TriggerFade();
        }
        else
        {
            _pool.Create(_prefab);
        }

        _spawnedBombs++;
        _activeBombCount++;
    }

    public void Release(Bomb bomb)
    {
        bomb.OnBombDeactivate -= Release;
        bomb.gameObject.SetActive(false);
        _activeBombCount--;
    }
}
