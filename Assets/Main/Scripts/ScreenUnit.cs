using UnityEngine;


public class ScreenUnit : MonoBehaviour
{
    //[Header("Materials")]
    //[SerializeField] private Material onMaterial;
    //[SerializeField] private Material offMaterial;

    [Header("Renderer Reference")]
    [SerializeField] private Renderer screenRenderer;

    [Header("Visual State")]
    [SerializeField] private Color inactiveColor = Color.black; // 可在 Inspector 修改
    private Color activeColor = Color.white;

    private MaterialPropertyBlock propertyBlock;

    // 分组 id，由 ScreenGenerator 随机分配
    public int GroupId { get; set; }

    // 当前开关状态
    public bool IsOn { get; private set; } = true;

    private void Awake()
    {
        if (screenRenderer == null)
        {
            screenRenderer = GetComponentInChildren<Renderer>();
        }

        propertyBlock = new MaterialPropertyBlock();

        // 初始化 visual 为 activeColor（保证运行时有正确颜色）
        ApplyColor(IsOn ? activeColor : inactiveColor);
    }

    // 外部用于设置「开启时的颜色」，ScreenColorController 会调用此方法
    public void SetColor(Color color)
    {
        activeColor = color;
        if (IsOn)
        {
            ApplyColor(activeColor);
        }
    }

    // 切换视觉上的开/关（仅改变显示颜色）
    public void SetEnabled(bool on)
    {
        if (IsOn == on) return;
        IsOn = on;
        ApplyColor(IsOn ? activeColor : inactiveColor);

        if (IsOn == on) return;

        //IsOn = on;

        //// 🔥 切换材质
        //if (screenRenderer != null)
        //{
        //    screenRenderer.material = IsOn ? onMaterial : offMaterial;
        //}

        //// 如果开启状态，还可以同步颜色
        //if (IsOn)
        //{
        //    ApplyColor(activeColor);
        //}
    }

    // 内部：通过 MaterialPropertyBlock 写入颜色到 renderer（不实例化材质）
    private void ApplyColor(Color color)
    {
        if (screenRenderer == null) return;

        screenRenderer.GetPropertyBlock(propertyBlock);

        // 尝试常见属性名（兼容不同 shader）
        if (screenRenderer.sharedMaterial != null && screenRenderer.sharedMaterial.HasProperty("_BaseColor"))
        {
            propertyBlock.SetColor("_BaseColor", color);
        }
        else
        {
            propertyBlock.SetColor("_Color", color);
        }

        screenRenderer.SetPropertyBlock(propertyBlock);
    }
}
