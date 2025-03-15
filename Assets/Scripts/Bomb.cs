using System;
using UnityEngine;

[RequireComponent(typeof(Explode))]
[RequireComponent(typeof(Transparency))]
public class Bomb : MonoBehaviour
{
    private Transparency _transparency;
    private Explode _explode;

    public event Action<Bomb> OnBombDeactivate;

    private void Awake()
    {
        _explode = GetComponent<Explode>();
        _transparency = GetComponent<Transparency>();
    }

    public void TriggerFade()
    {
        _transparency.OnFadeComplete += Explosion;
        _transparency.StartFadeOut();
    }

    private void Explosion()
    {
        _transparency.OnFadeComplete -= Explosion;
        _explode.Bang();
        OnBombDeactivate.Invoke(this);
    }
}
