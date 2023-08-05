using UnityEngine;
using TMPro;

public class PlayerStats : Stats
{
    [Header("UI")]
    [SerializeField] private TMP_Text healthText;

    protected override void Awake()
    {
        base.Awake();
        healthText.text = currentHealthPoints.ToString();
    }

    public override void TakeDamage(float value)
    {
        base.TakeDamage(value);

        healthText.text = currentHealthPoints.ToString();
    }
}
