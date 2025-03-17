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
        _text.text = $"����� ����������: {_spawnerCube.SpawnedCubes}\n" +
                         $"����� �������: {_spawnerCube.Pool.CreatedObjectsCount}\n" +
                         $"�������� ����: {_spawnerCube.ActiveCubeCount}";
    }
}
