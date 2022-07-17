using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void Start()
    {
        GetComponent<HP>().OnDamage += OnDamage;
    }

    void OnDamage()
    {
        anim.SetTrigger("hit");
    }
}
