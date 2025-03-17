using System;
using UnityEngine;

public class SpawnerBomb : MonoBehaviour
{
    [SerializeField] private Bomb _prefab;

    public event Action ViewUpdated;

    public ObjectPool<Bomb> Pool { get; private set; }

    public int SpawnedBombs { get; private set; }

    public int ActiveBombCount { get; private set; }

    private void Awake()
    {
        Pool = new ObjectPool<Bomb>(_prefab);
    }

    public void OnGet(Cube cube)
    {
        Bomb bomb = Pool.GetObj(_prefab);

        bomb.BombDeactivated += Release;
        bomb.transform.position = cube.transform.position;
        bomb.gameObject.SetActive(true);
        bomb.TriggerFade();

        SpawnedBombs++;
        ActiveBombCount++;
        ViewUpdated.Invoke();
    }

    public void Release(Bomb bomb)
    {
        bomb.BombDeactivated -= Release;
        bomb.gameObject.SetActive(false);
        Pool.Return(bomb);
        ActiveBombCount--;
        ViewUpdated.Invoke();
    }
}
