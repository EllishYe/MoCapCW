using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class BossInputKey : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Animator 参数名（可在 Inspector 修改）
    [SerializeField] private string paramPat = "IsPat";
    [SerializeField] private string paramThrow = "IsThrow";
    [SerializeField] private string paramHurt = "IsHurt";
    [SerializeField] private string paramArmfall = "IsArmfall";
    [SerializeField] private string paramDie = "IsDie";

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError($"{name} BossInputKey: Animator 未找到，请在 Inspector 指定 Animator。");
        }
    }

    private void Update()
    {
        if (animator == null) return;

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        // 仅新输入系统
        if (Keyboard.current == null) return;
        HandleKey(Keyboard.current.digit1Key.wasPressedThisFrame, Keyboard.current.digit1Key.wasReleasedThisFrame, paramPat);
        HandleKey(Keyboard.current.digit2Key.wasPressedThisFrame, Keyboard.current.digit2Key.wasReleasedThisFrame, paramThrow);
        HandleKey(Keyboard.current.digit3Key.wasPressedThisFrame, Keyboard.current.digit3Key.wasReleasedThisFrame, paramHurt);
        HandleKey(Keyboard.current.digit4Key.wasPressedThisFrame, Keyboard.current.digit4Key.wasReleasedThisFrame, paramArmfall);
        HandleKey(Keyboard.current.digit5Key.wasPressedThisFrame, Keyboard.current.digit5Key.wasReleasedThisFrame, paramDie);

#elif ENABLE_LEGACY_INPUT_MANAGER && !ENABLE_INPUT_SYSTEM
        // 仅旧输入系统
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetParam(paramPat, true);
        if (Input.GetKeyUp(KeyCode.Alpha1)) SetParam(paramPat, false);

        if (Input.GetKeyDown(KeyCode.Alpha2)) SetParam(paramThrow, true);
        if (Input.GetKeyUp(KeyCode.Alpha2)) SetParam(paramThrow, false);

        if (Input.GetKeyDown(KeyCode.Alpha3)) SetParam(paramHurt, true);
        if (Input.GetKeyUp(KeyCode.Alpha3)) SetParam(paramHurt, false);

        if (Input.GetKeyDown(KeyCode.Alpha4)) SetParam(paramArmfall, true);
        if (Input.GetKeyUp(KeyCode.Alpha4)) SetParam(paramArmfall, false);

        if (Input.GetKeyDown(KeyCode.Alpha5)) SetParam(paramDie, true);
        if (Input.GetKeyUp(KeyCode.Alpha5)) SetParam(paramDie, false);

#else
        // Both 或未确定：优先新系统，否则回退旧系统
        if (Keyboard.current != null)
        {
            HandleKey(Keyboard.current.digit1Key.wasPressedThisFrame, Keyboard.current.digit1Key.wasReleasedThisFrame, paramPat);
            HandleKey(Keyboard.current.digit2Key.wasPressedThisFrame, Keyboard.current.digit2Key.wasReleasedThisFrame, paramThrow);
            HandleKey(Keyboard.current.digit3Key.wasPressedThisFrame, Keyboard.current.digit3Key.wasReleasedThisFrame, paramHurt);
            HandleKey(Keyboard.current.digit4Key.wasPressedThisFrame, Keyboard.current.digit4Key.wasReleasedThisFrame, paramArmfall);
            HandleKey(Keyboard.current.digit5Key.wasPressedThisFrame, Keyboard.current.digit5Key.wasReleasedThisFrame, paramDie);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SetParam(paramPat, true);
            if (Input.GetKeyUp(KeyCode.Alpha1)) SetParam(paramPat, false);

            if (Input.GetKeyDown(KeyCode.Alpha2)) SetParam(paramThrow, true);
            if (Input.GetKeyUp(KeyCode.Alpha2)) SetParam(paramThrow, false);

            if (Input.GetKeyDown(KeyCode.Alpha3)) SetParam(paramHurt, true);
            if (Input.GetKeyUp(KeyCode.Alpha3)) SetParam(paramHurt, false);

            if (Input.GetKeyDown(KeyCode.Alpha4)) SetParam(paramArmfall, true);
            if (Input.GetKeyUp(KeyCode.Alpha4)) SetParam(paramArmfall, false);

            if (Input.GetKeyDown(KeyCode.Alpha5)) SetParam(paramDie, true);
            if (Input.GetKeyUp(KeyCode.Alpha5)) SetParam(paramDie, false);
        }
#endif
    }

    private void HandleKey(bool pressed, bool released, string param)
    {
        if (pressed) SetParam(param, true);
        if (released) SetParam(param, false);
    }

    private void SetParam(string paramName, bool value)
    {
        if (animator == null) return;

        if (!animator.HasParam(paramName))
        {
            Debug.LogWarning($"{name} BossInputKey: Animator 不包含参数 '{paramName}'");
            // 仍然尝试设置以防用户拼写不同（Animator 会忽略未知参数）
        }

        animator.SetBool(paramName, value);
        Debug.Log($"{name} BossInputKey: Set '{paramName}' = {value}");
    }
}

// Animator 扩展：检查参数是否存在（避免硬错误）
public static class AnimatorExtensions
{
    public static bool HasParam(this Animator animator, string paramName)
    {
        if (animator == null || string.IsNullOrEmpty(paramName)) return false;
        foreach (var p in animator.parameters)
        {
            if (p.name == paramName) return true;
        }
        return false;
    }
}
