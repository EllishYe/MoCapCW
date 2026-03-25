using UnityEngine;
using System.Collections;

public class BtnMove : MonoBehaviour
{
    [Header("Object 1 (Down)")]
    public Transform object1;

    public Transform object1StartPoint; // Empty 1
    public Transform object1EndPoint;   // Empty 2

    [Header("Object 2 (Up)")]
    public Transform object2;

    public Transform object2StartPoint; // Empty 3
    public Transform object2EndPoint;   // Empty 4

    [Header("Object 2 Shoot Reaction")]
    public Transform object3;
    public Transform object2DownPoint; // 被射击后下降到这里
    private bool object2ShotTriggered = false;

    [Header("Settings")]
    public float moveDuration = 1.5f;

    private Coroutine moveCoroutine;

    private void Start()
    {
        PlayMove();
    }

    // ⭐ 总入口
    public void PlayMove()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        float time = 0f;

        // 缓存起点（避免过程中被改动）
        Vector3 o1Start = object1StartPoint.position;
        Vector3 o1End = object1EndPoint.position;

        Vector3 o2Start = object2StartPoint.position;
        Vector3 o2End = object2EndPoint.position;

        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float t = time / moveDuration;

            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            if (object1 != null)
                object1.position = Vector3.Lerp(o1Start, o1End, smoothT);

            if (object2 != null)
                object2.position = Vector3.Lerp(o2Start, o2End, smoothT);

            yield return null;
        }

        // 确保最终位置精确
        if (object1 != null)
            object1.position = o1End;

        if (object2 != null)
            object2.position = o2End;
    }

    public void PlayBtnPressed()
    {
        if (object2ShotTriggered) return;

        object2ShotTriggered = true;

        if (object3 != null && object2DownPoint != null)
        {
            StopAllCoroutines();
            StartCoroutine(MoveBtnDown());
        }
    }
    IEnumerator MoveBtnDown()
    {
        Vector3 start = object3.position;
        Vector3 end = object2DownPoint.position;

        float time = 0f;

        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float t = time / moveDuration;

            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            object3.position = Vector3.Lerp(start, end, smoothT);

            yield return null;
        }

        object3.position = end;
    }
}
