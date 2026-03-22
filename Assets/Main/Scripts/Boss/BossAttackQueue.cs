using System.Collections.Generic;
using UnityEngine;

public class BossAttackQueue : MonoBehaviour
{
    public enum AttackType
    {
        Pat,
        Throw,
        Sweep
    }

    private Queue<AttackType> attackQueue = new Queue<AttackType>();

    /// <summary>
    /// 对外接口：获取下一个攻击
    /// </summary>
    public AttackType GetNextAttack()
    {
        if (attackQueue.Count == 0)
        {
            GenerateNewSet();
        }

        return attackQueue.Dequeue();
    }

    /// <summary>
    /// 生成一组 [Pat, Throw, Sweep] 的随机排列
    /// </summary>
    private void GenerateNewSet()
    {
        List<AttackType> list = new List<AttackType>()
        {
            AttackType.Pat,
            AttackType.Throw,
            AttackType.Sweep
        };

        // Fisher-Yates Shuffle
        for (int i = 0; i < list.Count; i++)
        {
            int randIndex = Random.Range(i, list.Count);
            AttackType temp = list[i];
            list[i] = list[randIndex];
            list[randIndex] = temp;
        }

        foreach (var attack in list)
        {
            attackQueue.Enqueue(attack);
        }

        // 输出本次生成的顺序，便于直接从 Console 读取
        string seq = string.Join(", ", list.ConvertAll(a => a.ToString()).ToArray());
        Debug.Log($"New Attack Queue Generated: [{seq}]");
    }
}