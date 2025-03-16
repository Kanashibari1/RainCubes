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
        _textCube.text = $"Кубов заспавнено: {_spawnerCube.SpawnedCubes}\n" +
                              $"Кубов создано: {_spawnerCube.Pool.CreatedObjectsCount}\n" +
                              $"Активные Кубы: {_spawnerCube.ActiveCubeCount}";

        _textBomb.text = $"Бомб заспавнено: {_spawnerBomb.SpawnedBombs}\n" +
                              $"Бомб создано: {_spawnerBomb.Pool.CreatedObjectsCount}\n" +
                              $"Активные бомбы: {_spawnerBomb.ActiveBombCount}";
    }
}
