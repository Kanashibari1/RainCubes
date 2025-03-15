using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Platform))]
public class Cube : MonoBehaviour
{
    private const float _minTime = 2f;
    private const float _maxTime = 5f;
    private Color _initialColor;
    private Color _defaultColor = Color.white;
    private Renderer _renderer;
    private ColorChanger _changer;
    public event Action<Cube> OnCubeDeactivate;

    private void Awake()
    {
        _changer = GetComponent<ColorChanger>();
        _renderer = GetComponent<Renderer>();
        _initialColor = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        float lifeTime = UnityEngine.Random.Range(_minTime, _maxTime);

        if (collision.gameObject.TryGetComponent<Platform>(out _))
        {
            if (_renderer.material.color == _initialColor)
            {
                _changer.Changer();
                StartCoroutine(TimeStop(lifeTime));
            }
        }
    }

    private IEnumerator TimeStop(float time)
    {
        WaitForSeconds _sleepTime = new(time);

        yield return _sleepTime;

        OnCubeDeactivate.Invoke(this);
        _renderer.material.color = _defaultColor;
    }
}
