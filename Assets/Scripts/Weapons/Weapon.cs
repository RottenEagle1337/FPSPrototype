using System.Collections;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 15f;
    private float nextTimeToShoot = 0f;
    [SerializeField] private float impactForce = 45f;
    [SerializeField] private LayerMask shootLayerMask;

    [Header("Ammo settings")]
    [SerializeField] private int maxAmmo = 180;
    private int currentAmmo;
    [SerializeField] private int maxAmmoPerMagazine = 30;
    private int currentAmmoInMagazine;
    [SerializeField] private float reloadTime = 1f;
    private bool isReloading;
    [SerializeField] private TMP_Text ammoText;

    [Header("Recoil settings")]
    [SerializeField] [Range(0f, 7f)] private float recoilYValue;
    [SerializeField] [Range(0f, 3f)] private float recoilXValue;
    private float currentRecoilXPosition;
    private float currentRecoilYPosition;

    [Header("GFX & VFX")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject muzzleVFX;
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private GameObject impactVFX;

    [Header("SFX")]
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;

    private Animator anim;
    private MouseLook mouseLook;
    private AudioSource audioSource;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        mouseLook = GetComponentInParent<MouseLook>();
        audioSource = GetComponentInChildren<AudioSource>();

        currentAmmo = maxAmmo;
        ammoText.text = currentAmmoInMagazine.ToString() + "/" + currentAmmo.ToString();
    }

    private void Update()
    {
        if (isReloading)
            return;

        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(HandleReload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot && currentAmmoInMagazine > 0)
        {
            nextTimeToShoot = Time.time + 1f / fireRate;

            HandleShooting();
        }

        HandleAim();
    }

    private IEnumerator HandleReload()
    {
        isReloading = true;

        anim.SetBool("isReloading", true);

        audioSource.clip = reloadSound;
        audioSource.Play();

        yield return new WaitForSeconds(reloadTime - 0.25f); //0.25 is animation transition time

        anim.SetBool("isReloading", false);

        yield return new WaitForSeconds(0.25f);

        if((currentAmmo + currentAmmoInMagazine) >= maxAmmoPerMagazine)
        {
            currentAmmo -= (maxAmmoPerMagazine - currentAmmoInMagazine);
            currentAmmoInMagazine = maxAmmoPerMagazine;
        }
        else
        {
            currentAmmoInMagazine += currentAmmo;
            currentAmmo = 0;
        }
        ammoText.text = currentAmmoInMagazine.ToString() + "/" + currentAmmo.ToString();

        isReloading = false;
    }

    private void HandleShooting()
    {
        audioSource.clip = shootSound;
        audioSource.Play();

        Instantiate(muzzleVFX, muzzleTransform);

        currentAmmoInMagazine--;
        ammoText.text = currentAmmoInMagazine.ToString() + "/" + currentAmmo.ToString();

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, range, shootLayerMask))
        {
            if(hit.collider.TryGetComponent<IDamageable>(out IDamageable target))
            {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            HandleRecoil();

            GameObject impactObject = Instantiate(impactVFX, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(impactObject, 1f);
        }
    }

    private void HandleAim()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("isAiming", true);
            crosshair.SetActive(false);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            anim.SetBool("isAiming", false);
            crosshair.SetActive(true);
        }
    }

    private void HandleRecoil()
    {
        currentRecoilXPosition = ((Random.value - 0.5f) / 2f) * recoilXValue;
        currentRecoilYPosition = ((Random.value - 0.5f) / 2f) * recoilYValue;

        mouseLook.SetWantedCameraXRotation(Mathf.Abs(currentRecoilYPosition));
        mouseLook.SetWantedYRotation(currentRecoilXPosition);
    }

    public void RefillAmmo()
    {
        currentAmmo = maxAmmo;
        ammoText.text = currentAmmoInMagazine.ToString() + "/" + currentAmmo.ToString();
    }
}
