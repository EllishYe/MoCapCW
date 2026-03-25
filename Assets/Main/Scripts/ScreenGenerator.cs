using System.Collections.Generic;
using UnityEngine;


public class ScreenGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform ringCenter;
    [SerializeField] private GameObject screenPrefab;

    [Header("Ring Parameters")]
    [SerializeField] private float radius = 15f;
    [SerializeField] private int screenCount = 20;
    [SerializeField] private float startAngleOffset = 0f;

    // Save for potential future use
    private List<ScreenUnit> screenUnits = new List<ScreenUnit>();

    private void Start()
    {
        GenerateRing();
    }

    private void GenerateRing()
    {
        ClearChildren();
        screenUnits.Clear();

        if (ringCenter == null)
        {
            ringCenter = transform;
        }

        float angleStep = 360f / screenCount;

        for (int i = 0; i < screenCount; i++)
        {
            float angle = startAngleOffset + angleStep * i;
            float radian = angle * Mathf.Deg2Rad;

            // align y position
            Vector3 localPos = new Vector3(
                Mathf.Cos(radian) * radius,
                ringCenter.position.y - transform.position.y,
                Mathf.Sin(radian) * radius
            );

            GameObject screenObj = Instantiate(screenPrefab, transform);
            screenObj.name = $"RingScreen_{i}";
            screenObj.transform.localPosition = localPos;

            // face the screen towards the center of the ring but keep it level (no pitch)
            Vector3 lookDirection = ringCenter.position - screenObj.transform.position;
            lookDirection.y = 0f; // remove vertical component to keep it level
            if (lookDirection.sqrMagnitude > 0.0001f)
            {
                screenObj.transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            }

            // 更稳健地查找 ScreenUnit（根或子对象）
            ScreenUnit unit = screenObj.GetComponent<ScreenUnit>() ?? screenObj.GetComponentInChildren<ScreenUnit>();
            if (unit != null)
            {
                // 随机分配组 id（0,1,2）
                unit.GroupId = Random.Range(0, 3);
                unit.SetEnabled(true); // 确保初始为开启状态
                screenUnits.Add(unit);
            }
        }
    }

    private void ClearChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public List<ScreenUnit> GetScreenUnits()
    {
        return screenUnits;
    }
}
