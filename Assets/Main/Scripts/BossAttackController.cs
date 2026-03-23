using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    public Transform pat_leftHandPoint;
    public Transform pat_rightHandPoint;
    public GameObject pat_shockwavePrefab;

    public void OnPatHit(int handIndex)
    {
        Transform spawnPoint = handIndex == 0 ? pat_leftHandPoint : pat_rightHandPoint;

        if (spawnPoint == null || pat_shockwavePrefab == null)
        {
            Debug.LogWarning("Missing reference!");
            return;
        }

        Instantiate(pat_shockwavePrefab, spawnPoint.position, Quaternion.identity);
    }
}
