using UnityEngine;
using Cinemachine;
using StarterAssets;
using System;


public class TPSController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    
    [Header("Collision Detect")]
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    
    [Header("Projectile")]
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    [Header("Aiming UI")]
    [SerializeField] private GameObject crosshair;
    
    [Header("Audio")]
    [SerializeField] private AudioClip shootSfx;
    [SerializeField] private AudioSource audioSource;

    // references
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();

        // 尝试获取或创建 AudioSource（方便在没有手动添加时也能播放）
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
            }
        }
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        // aiming
        if (starterAssetsInputs.aim) {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);

            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            crosshair.SetActive(true);

            Vector3 worldAimTarget = mouseWorldPosition; 
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            // stop aiming
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);

            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            
            crosshair.SetActive(false);
        }

        if (starterAssetsInputs.shoot && starterAssetsInputs.aim)
        {
            //shoot bullet towards mouseWorldPosition
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir,Vector3.up));

            // 播放开枪音效（优先使用绑定的 AudioSource，否则使用 PlayClipAtPoint）
            if (shootSfx != null)
            {
                if (audioSource != null)
                    audioSource.PlayOneShot(shootSfx);
                else
                    AudioSource.PlayClipAtPoint(shootSfx, spawnBulletPosition.position);
            }

            starterAssetsInputs.shoot = false;
        }
    }
}
