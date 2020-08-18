using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] bool isHoming = false;
    [SerializeField] GameObject hitEffect = null;

    bool projectileFired = false;
    float damage = 0;

    Health target = null;

    private void Start()
    {
        transform.LookAt(GetAimLocation());
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        if (isHoming && !(target.getIsDead()))
        {
            transform.LookAt(GetAimLocation());
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

        if (targetCapsule == null) return target.transform.position; // If target does not have capsule collider

        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target) return;
        if (target.getIsDead()) return;

        target.TakeDamage(damage);

        GameObject persistentObject = Instantiate(hitEffect, other.transform);
        Destroy(gameObject);
    }
}
