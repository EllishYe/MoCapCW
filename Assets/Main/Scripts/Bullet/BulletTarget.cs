using Unity.VisualScripting;
using UnityEngine;

// 简单的 BulletTarget 占位类
public class BulletTarget : MonoBehaviour
{
    [Header("Body Part Type")]
    public BossBodyPartType partType = BossBodyPartType.Body;

    private BossHealth bossHealth;
    private BtnMove btnMove;

    void Awake()
    {
        bossHealth = GetComponentInParent<BossHealth>();
        btnMove = GetComponent<BtnMove>() ?? GetComponentInParent<BtnMove>() ?? GetComponentInChildren<BtnMove>();
    }
    public bool TakeHit(float damage)
    {
        // Btn return true but not take damage
        if (partType == BossBodyPartType.Btn)
        {
            //bossHealth.TakeDamage(damage, partType); 
            btnMove?.PlayBtnPressed();
            switch (btnMove.buttonType)
            {
                case BtnMove.ButtonType.A:
                    bossHealth.TriggerButtonA();
                    break;

                case BtnMove.ButtonType.B:
                    bossHealth.TriggerButtonB();
                    break;

                case BtnMove.ButtonType.C:
                    bossHealth.TriggerButtonC();
                    break;
            }
            return true;
        }

        if (bossHealth == null)
        {
            Debug.LogWarning("No BossHealth found!");
            //if (partType == BossBodyPartType.Btn)
            //{
            //    btnMove?.PlayBtnPressed();
                
            //    return true;
            //}
            return false;
        }

        return bossHealth.TakeDamage(damage, partType);
    }
}