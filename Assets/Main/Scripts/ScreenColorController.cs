using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenColorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScreenGenerator generator;

    [Header("Base Color (HSV)")]
    [Range(0f, 1f)] public float baseHue = 0.55f;        // 蓝色附近
    [Range(0f, 1f)] public float baseSaturation = 0.7f;
    [Range(0f, 1f)] public float baseValue = 0.8f;

    [Header("Variation Range")]
    [Range(0f, 0.5f)] public float hueVariation = 0f;
    [Range(0f, 0.5f)] public float saturationVariation = 0.15f;
    [Range(0f, 0.5f)] public float valueVariation = 0.2f;

    [Header("Random Seed")]
    public int randomSeed = 0;

    public int Count;

    [Header("Gradient")]
    public Gradient Gradient;
    public List<Color> ListGradient;

    private IEnumerator Start()
    {
        yield return null; // 等一帧，确保其它 Start（如 ScreenGenerator）已执行
        ApplyColorVariation();
    }

    public void ApplyColorVariation()
    {
        if (generator == null)
        {
            Debug.LogError("ScreenColorController: generator is null. Assign ScreenGenerator in the inspector.");
            return;
        }

        List<ScreenUnit> units = generator.GetScreenUnits();
        if (units == null)
        {
            Debug.LogError("ScreenColorController: generator.GetScreenUnits() returned null.");
            return;
        }

        Random.InitState(randomSeed);
        Count = units.Count;

        foreach (ScreenUnit unit in units)
        {
            if (unit == null) continue;

            Color color;

            if (Gradient != null)
            {
                // 从 Gradient 随机取值（0..1）并采样颜色
                float t = Random.Range(0f, 1f);
                color = Gradient.Evaluate(t);
            }
            else
            {
                // 回退到原有的 HSV 随机方法
                float h = baseHue + Random.Range(-hueVariation, hueVariation);
                float s = baseSaturation + Random.Range(-saturationVariation, saturationVariation);
                float v = baseValue + Random.Range(-valueVariation, valueVariation);

                h = Mathf.Repeat(h, 1f);
                s = Mathf.Clamp01(s);
                v = Mathf.Clamp01(v);

                color = Color.HSVToRGB(h, s, v);
            }

            unit.SetColor(color);
        }
    }
}
