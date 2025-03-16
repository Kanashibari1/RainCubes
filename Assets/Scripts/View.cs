using TMPro;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textCube;
    [SerializeField] private TextMeshProUGUI _textBomb;

    [SerializeField] private SpawnerCube _spawnerCube;
    [SerializeField] private SpawnerBomb _spawnerBomb;

    private void OnEnable()
    {
        _spawnerCube.ViewUpdated += ShowInfo;
        _spawnerBomb.ViewUpdated += ShowInfo;
    }

    private void OnDisable()
    {
        _spawnerCube.ViewUpdated -= ShowInfo;
        _spawnerBomb.ViewUpdated -= ShowInfo;
    }

    private void ShowInfo()
    {
        _textCube.text = $"����� ����������: {_spawnerCube.SpawnedCubes}\n" +
                              $"����� �������: {_spawnerCube.Pool.CreatedObjectsCount}\n" +
                              $"�������� ����: {_spawnerCube.ActiveCubeCount}";

        _textBomb.text = $"���� ����������: {_spawnerBomb.SpawnedBombs}\n" +
                              $"���� �������: {_spawnerBomb.Pool.CreatedObjectsCount}\n" +
                              $"�������� �����: {_spawnerBomb.ActiveBombCount}";
    }
}
