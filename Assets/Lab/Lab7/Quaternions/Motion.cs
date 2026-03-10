using Unity.VisualScripting;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public float speed = 3.0f;
 
    void Update()
    {
        transform.Translate(-Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
    }
}
