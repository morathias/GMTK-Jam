using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Medkit : MonoBehaviour
    {
        public float amount;
        public Action<Medkit> OnDestroy;
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HP hp))
            {
                hp.Heal(amount);
                OnDestroy?.Invoke(this);
                Destroy(gameObject);
            }
        }
    }
}