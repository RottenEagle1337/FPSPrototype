using System.Collections;
using UnityEngine;

public class Target : Stats
{
    [SerializeField] protected float resetTime;

    private bool isResetting;

    private Animator animator;
    private AudioSource audioSource;

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();

        base.Awake();
    }

    protected override void Die()
    {
        StartCoroutine(Reset());
    }

    public override void TakeDamage(float value)
    {
        if (isResetting)
            return;

        base.TakeDamage(value);
        audioSource.Play();
    }

    protected IEnumerator Reset()
    {
        isResetting = true;
        animator.SetBool("isResetting", true);

        yield return new WaitForSeconds(resetTime - 0.25f); // animation transition time
        
        animator.SetBool("isResetting", false);

        yield return new WaitForSeconds(0.25f);

        isResetting = false;
        currentHealthPoints = maxHealthPoints;
    }
}

