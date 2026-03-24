using UnityEngine;

// 简单的 BulletTarget 占位类
public class BulletTarget : MonoBehaviour
{
    [Header("Body Part Type")]
    public BossBodyPartType partType = BossBodyPartType.Body;

    private BossHealth bossHealth;

    void Awake()
    {
        bossHealth = GetComponentInParent<BossHealth>();
    }
    public bool TakeHit(float damage)
    {
        if (bossHealth == null)
        {
            Debug.LogWarning("No BossHealth found!");
            return false;
        }

        return bossHealth.TakeDamage(damage, partType);
    }
}