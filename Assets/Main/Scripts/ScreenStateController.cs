using System.Collections.Generic;
using UnityEngine;

// 放在屏幕父物件或场景管理物体上，监听按键 1/2/3 并切换对应组的开/关
public class ScreenStateController : MonoBehaviour
{
    [SerializeField] private ScreenGenerator generator;

    // 三个组的当前 on/off 状态，默认全部开启
    private bool[] groupStates = new bool[] { true, true, true };

    private void Update()
    {
        if (generator == null) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleGroup(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleGroup(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ToggleGroup(2);
        }
    }

    private void ToggleGroup(int groupIndex)
    {
        groupStates[groupIndex] = !groupStates[groupIndex];
        ApplyGroupState(groupIndex);
    }

    private void ApplyGroupState(int groupIndex)
    {
        List<ScreenUnit> units = generator.GetScreenUnits();
        if (units == null) return;

        foreach (var u in units)
        {
            if (u == null) continue;
            if (u.GroupId == groupIndex)
            {
                u.SetEnabled(groupStates[groupIndex]);
            }
        }
    }
}
