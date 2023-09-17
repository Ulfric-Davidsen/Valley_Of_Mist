using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.Attributes;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private float damage;
    private float knockback;

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == myCollider) { return; }

        if(alreadyCollidedWith.Contains(other)) { return; }

        alreadyCollidedWith.Add(other);

        if(other.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(damage);
        }

        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }
    }

    public void SetAttack(float damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
