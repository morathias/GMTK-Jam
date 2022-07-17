using System;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float currentHP;
    public float maxHP;

    public Action OnDead;
    public Action OnDamage;
    public Action OnChange;
    
    void Start()
    {
        currentHP = maxHP;
    }

    public bool IsDead => currentHP == 0;
    
    public void TakeDamage(float damage)
    {
        if (currentHP == 0)
            return;
        
        currentHP = Mathf.Max(0,currentHP - damage);
        
        OnDamage?.Invoke();
        
        if (currentHP == 0)
            OnDead?.Invoke();
    }

    public void Heal(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        OnChange?.Invoke();
    }
}
