using UnityEngine;
using System.Collections;

public class ArmDisapper : MonoBehaviour
{
    [Header("Meshes")]
    public GameObject leftArmMesh;
    public GameObject rightArmMesh;

    [Header("Colliders")]
    public Collider[] leftArmColliders;
    public Collider[] rightArmColliders;

    public void PlayDisappearLeft()
    {
        DisableColliders(leftArmColliders);
        StartCoroutine(FlashAndDisable(leftArmMesh));
    }

    public void PlayDisappearRight()
    {
        DisableColliders(rightArmColliders);
        StartCoroutine(FlashAndDisable(rightArmMesh));
    }

    void DisableColliders(Collider[] colliders)
    {
        foreach (Collider col in colliders)
        {
            if (col != null)
            {
                col.enabled = false;
            }
        }
    }

    IEnumerator FlashAndDisable(GameObject targetMesh)
    {
        if (targetMesh == null) yield break;

        for (int i = 0; i < 5; i++)
        {
            targetMesh.SetActive(false);
            yield return new WaitForSeconds(0.1f);

            targetMesh.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }

        targetMesh.SetActive(false);
    }
}
