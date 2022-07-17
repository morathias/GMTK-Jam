using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem hitParticle;

    private void Start()
    {
        GetComponent<HP>().OnDamage += OnDamage;
    }

    void OnDamage()
    {
        anim.SetTrigger("hit");
        if (hitParticle != null)
        {
            hitParticle.Play();
        }
    }
}
