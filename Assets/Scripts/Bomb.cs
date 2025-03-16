using System;
using UnityEngine;

[RequireComponent(typeof(Exploder))]
[RequireComponent(typeof(Transparency))]
public class Bomb : MonoBehaviour
{
    private Transparency _transparency;
    private Exploder _explode;

    public event Action<Bomb> OnBombDeactivate;

    private void Awake()
    {
        _explode = GetComponent<Exploder>();
        _transparency = GetComponent<Transparency>();
    }

    public void TriggerFade()
    {
        _transparency.FadeCompleted += Explosion;
        _transparency.StartFadeOut();
    }

    private void Explosion()
    {
        _transparency.FadeCompleted -= Explosion;
        _explode.Bang();
        OnBombDeactivate.Invoke(this);
    }
}
