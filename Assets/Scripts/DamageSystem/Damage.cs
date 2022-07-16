using System;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float amount;

    public Action OnDamage;
    
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out HP hp) && !hp.IsDead)
        {
            hp.TakeDamage(amount);
            OnDamage?.Invoke();
        }
    }
}
