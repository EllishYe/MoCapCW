using UnityEngine;
using StarterAssets;

public class ShockwaveCylinder : MonoBehaviour
{
    private float currentRadius = 0f;
    
    [Header("VFX")]
    public ParticleSystem shockwaveParticle;

    [Header("Shockwave Visual")]
    public Transform shockwaveCylinder;   // cylinder

    [Header("Scale Animation")]
    public float expandSpeed = 8f;        // cylinder expands per second
    public float maxRadius = 6f;      // maximum diameter

    [Header("Hit Detection")]
    public float hitThickness = 0.4f;     // Circular ring for thickness determination
    public int damage = 1;

    [Header("Player")]
    public Transform player;
    public ThirdPersonController playerController;

    private bool hasHitPlayer = false;

    void Start()
    {
        currentRadius = 0f;

        if (shockwaveCylinder != null)
        {
            Vector3 s = shockwaveCylinder.localScale;
            shockwaveCylinder.localScale = new Vector3(0f, s.y, 0f);
        }

        if (shockwaveParticle != null)
        {
            shockwaveParticle.Play();

            var main = shockwaveParticle.main;
            main.startSpeed = expandSpeed;
            float lifetime = (maxRadius) / expandSpeed;
            main.startLifetime = lifetime;
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
        // Radius expands over time
        currentRadius += expandSpeed * Time.deltaTime;
        currentRadius = Mathf.Min(currentRadius, maxRadius);

        // use radius to set scale
        float diameter = currentRadius * 2f;

        Vector3 scale = shockwaveCylinder.localScale;
        shockwaveCylinder.localScale = new Vector3(diameter, scale.y, diameter);
    }

    void CheckPlayerHit()
    {
        if (hasHitPlayer || player == null) return;

        Vector3 center = shockwaveCylinder.position;
        Vector3 playerPos = player.position;

        Vector2 centerXZ = new Vector2(center.x, center.z);
        Vector2 playerXZ = new Vector2(playerPos.x, playerPos.z);

        float playerDistance = Vector2.Distance(centerXZ, playerXZ);

        // ✅ 直接用 currentRadius
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
        if (currentRadius >= maxRadius)
        {
            Destroy(gameObject);
        }
    }
}
