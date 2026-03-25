using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    //Pat Attack
    public Transform pat_leftHandPoint;
    public Transform pat_rightHandPoint;
    public GameObject pat_shockwavePrefab;

    //Sweep Attack
    public Transform sweep_SpawnPoint;
    public GameObject sweep_Prefab;

    // Is Lower Arm Alive
    private bool isLeftArmAlive = true;
    private bool isRightArmAlive = true;

    public void OnPatHit(int handIndex)
    {
        // handIndex: 0 = left, 1 = right

        if (handIndex == 0 && !isLeftArmAlive) return;
        if (handIndex == 1 && !isRightArmAlive) return;

        Transform spawnPoint = handIndex == 0 ? pat_leftHandPoint : pat_rightHandPoint;

        if (spawnPoint == null || pat_shockwavePrefab == null)
        {
            Debug.LogWarning("Missing reference!");
            return;
        }

        Instantiate(pat_shockwavePrefab, spawnPoint.position, Quaternion.identity);
    }
    public void OnRightArmDisappear()
    {
        isRightArmAlive = false;
    }
    public void OnLeftArmDisappear()
    {
        isLeftArmAlive = false;
    }
    public void OnSweepHit()
    {
        Instantiate(sweep_Prefab, sweep_SpawnPoint.position, sweep_SpawnPoint.rotation);
    }
}
