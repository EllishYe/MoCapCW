using UnityEngine;
using StarterAssets;

public class ShockwaveByCylinder : MonoBehaviour
{
    [Header("Shockwave Visual")]
    public Transform shockwaveCylinder;   // cylinder

    [Header("Scale Animation")]
    public float expandSpeed = 8f;        // cylinder expands per second
    public float maxDiameter = 12f;       // maximum diameter

    [Header("Hit Detection")]
    public float hitThickness = 0.4f;     // Circular ring for thickness determination
    public int damage = 1;

    [Header("Player")]
    public Transform player;
    public ThirdPersonController playerController;

    private bool hasHitPlayer = false;

    void Start()
    {
        if (shockwaveCylinder != null)
        {
            Vector3 s = shockwaveCylinder.localScale;
            shockwaveCylinder.localScale = new Vector3(0f, s.y, 0f);
        }

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;

                if (playerController == null)
                    playerController = playerObj.GetComponent<StarterAssets.ThirdPersonController>();
            }
        }
    }

    void Update()
    {
        if (shockwaveCylinder == null) return;

        ExpandCylinder();
        CheckPlayerHit();
        CheckDestroy();
    }

    void ExpandCylinder()
    {
        Vector3 scale = shockwaveCylinder.localScale;

        float newDiameterX = scale.x + expandSpeed * Time.deltaTime;
        float newDiameterZ = scale.z + expandSpeed * Time.deltaTime;

        newDiameterX = Mathf.Min(newDiameterX, maxDiameter);
        newDiameterZ = Mathf.Min(newDiameterZ, maxDiameter);

        shockwaveCylinder.localScale = new Vector3(newDiameterX, scale.y, newDiameterZ);
    }

    void CheckPlayerHit()
    {
        if (hasHitPlayer || player == null) return;

        Vector3 center = shockwaveCylinder.position;
        Vector3 playerPos = player.position;

        // Only consider the XZ distance on the ground.
        Vector2 centerXZ = new Vector2(center.x, center.z);
        Vector2 playerXZ = new Vector2(playerPos.x, playerPos.z);

        float playerDistance = Vector2.Distance(centerXZ, playerXZ);

        // Read the current radius of the cylinder
        float currentRadius = shockwaveCylinder.localScale.x * 0.5f;

        // When the player is close to the current radius, it means the edge of the shockwave will hit the player.
        if (Mathf.Abs(playerDistance - currentRadius) <= hitThickness)
        {
            bool isGrounded = true;

            if (playerController != null)
            {
                isGrounded = playerController.IsGrounded();
            }

            if (isGrounded)
            {
                Debug.Log("Player hit by shockwave!");
            }
            else
            {
                Debug.Log("Player jumped over the shockwave!");
            }

            hasHitPlayer = true;
        }
    }

    void CheckDestroy()
    {
        if (shockwaveCylinder.localScale.x >= maxDiameter)
        {
            Destroy(gameObject);
        }
    }
}