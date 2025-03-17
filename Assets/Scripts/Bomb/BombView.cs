using UnityEngine;

public class BombView : SpawnerView<Bomb>
{
    [SerializeField] protected SpawnerBomb _spawner;

    private void OnEnable()
    {
        _spawner.ViewUpdated += ShowInfo;
    }

    private void OnDisable()
    {
        _spawner.ViewUpdated -= ShowInfo;
    }

    protected override void ShowInfo()
    {
        _text.text = $"Бомб заспавнено: {_spawner.SpawnedBombs}\n" +
                         $"Бомб создано: {_spawner.Pool.CreatedObjectsCount}\n" +
                         $"Активные бомбы: {_spawner.ActiveBombCount}";
    }
}
