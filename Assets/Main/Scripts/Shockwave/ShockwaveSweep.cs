using UnityEngine;

public class ShockwaveSweep : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 1f;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit by Sweep!");
        }
    }
}
