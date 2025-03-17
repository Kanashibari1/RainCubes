using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Material))]
public class Transparency : MonoBehaviour
{
    private Material _material;
    private MeshRenderer _renderer;
    private Color _defaultColor;

    private const float _minTime = 2f;
    private const float _maxTime = 5f;
    private float _startAlpha;
    private string modeProperty = "_Mode";
    private float modeValue = 3f;
    private string srcBlend = "_SrcBlend";
    private string dstBlend = "_DstBlend";
    private string zWrite = "_ZWrite";
    private int srcBlendValue = (int)UnityEngine.Rendering.BlendMode.SrcAlpha;
    private int dstBlendValue = (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;
    private  int zWriteValue = 0;
    private string alphaTestKeyword = "_ALPHATEST_ON";
    private string alphaBlendKeyword = "_ALPHABLEND_ON";
    private string alphaPremultiplyKeyword = "_ALPHAPREMULTIPLY_ON";
    private int renderQueueValue = 3000;

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
        float fadeTime = Random.Range(_minTime, _maxTime);

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
        if (_material.HasProperty(modeProperty))
        {
            _material.SetFloat(modeProperty, modeValue);
            _material.SetInt(srcBlend, srcBlendValue);
            _material.SetInt(dstBlend, dstBlendValue);
            _material.SetInt(zWrite, zWriteValue);
            _material.DisableKeyword(alphaTestKeyword);
            _material.EnableKeyword(alphaBlendKeyword);
            _material.DisableKeyword(alphaPremultiplyKeyword);
            _material.renderQueue = renderQueueValue;
        }
    }
}
