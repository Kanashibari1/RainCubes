using TMPro;
using UnityEngine;

public abstract class SpawnerView<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _text;

    protected abstract void ShowInfo();
}