using UnityEngine;
using System.Collections;

public class ArmDisapper : MonoBehaviour
{
    public GameObject armMesh;


    public void Start()
    {
        PlayDisappear();
    }

    public void PlayDisappear()
    {
        StartCoroutine(FlashAndDisable());
    }

    IEnumerator FlashAndDisable()
    {
        for (int i = 0; i < 5; i++)
        {
            armMesh.SetActive(false);
            yield return new WaitForSeconds(0.1f);

            armMesh.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }

        armMesh.SetActive(false);
    }
}
