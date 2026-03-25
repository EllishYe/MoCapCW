using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public float speed = 10f;
    public GameObject bombPrefab;

    private Vector3 targetPos;
    private bool isLaunched = false;

    public void Launch(Vector3 target)
    {
        targetPos = target;
        isLaunched = true;
    }

    void Update()
    {
        if (!isLaunched) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.2f)
        {
            OnHit();
        }
    }

    void OnHit()
    {
        Instantiate(bombPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
