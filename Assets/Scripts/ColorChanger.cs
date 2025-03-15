using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChanger : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private float _hueMin = 0f;
    private float _hueMax = 1f;
    private float _saturationMin = 0.5f;
    private float _saturationMax = 1f;
    private float _valueMin = 0.5f;
    private float _valueMax = 1f;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Changer()
    {
        _meshRenderer.material.color = Random.ColorHSV(_hueMin, _hueMax, _saturationMin, _saturationMax, _valueMin, _valueMax);
    }
}
