using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Material))]
public class Transparency : MonoBehaviour
{
    private Material _material;
    private MeshRenderer _renderer;
    private float _startAlpha;
    private Color _defaultColor;

    public event System.Action FadeCompleted;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _material = _renderer.material;
        _defaultColor = _renderer.material.color;
    }

    public void StartFadeOut()
    {
        _startAlpha = _material.color.a;
        float fadeTime = Random.Range(2f, 5f);

        SetMaterialToTransparent();

        StartCoroutine(Fade(fadeTime));
    }

    private IEnumerator Fade(float fadeTime)
    {
        float time = 0f;
        Color newColor = _renderer.material.color;

        while (fadeTime > time)
        {
            float alpha = Mathf.Lerp(_startAlpha, 0f, time / fadeTime);

            newColor.a = alpha;
            _material.color = newColor;
            time += Time.deltaTime;

            yield return null;
        }

        FadeCompleted.Invoke();

        _renderer.material.color = _defaultColor;
    }

    private void SetMaterialToTransparent()
    {
        if (_material.HasProperty("_Mode"))
        {
            _material.SetFloat("_Mode", 3);
            _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha); 
            _material.SetInt("_ZWrite", 0); 
            _material.DisableKeyword("_ALPHATEST_ON"); 
            _material.EnableKeyword("_ALPHABLEND_ON"); 
            _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            _material.renderQueue = 3000;
        }
    }
}
