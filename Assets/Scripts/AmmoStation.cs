using UnityEngine;

public class AmmoStation : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject Icon;

    private void Awake()
    {
        Icon.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Icon.SetActive(true);

            Weapon weapon = other.GetComponentInChildren<Weapon>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                weapon.RefillAmmo();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            Icon.SetActive(false);
    }
}
