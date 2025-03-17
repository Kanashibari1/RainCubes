using UnityEngine;

public class CubeView : SpawnerView<Cube>
{
    [SerializeField] private SpawnerCube _spawnerCube;

    private void OnEnable()
    {
        _spawnerCube.ViewUpdated += ShowInfo;
    }

    private void OnDisable()
    {
        _spawnerCube.ViewUpdated -= ShowInfo;
    }

    protected override void ShowInfo()
    {
        _text.text = $"Кубов заспавнено: {_spawnerCube.SpawnedCubes}\n" +
                         $"Кубов создано: {_spawnerCube.Pool.CreatedObjectsCount}\n" +
                         $"Активные Кубы: {_spawnerCube.ActiveCubeCount}";
    }
}
