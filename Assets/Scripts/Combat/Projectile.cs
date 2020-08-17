using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;
    Health target = null;
    float damage = 0;

    bool guidedMissile = false;
    bool projectileFired = false;
    Vector3 savedLocation;

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        if (!projectileFired && !guidedMissile) {
            savedLocation = GetAimLocation();
            projectileFired = true;

        } else if (guidedMissile) {
            savedLocation = GetAimLocation();
        }
        
        transform.LookAt(savedLocation);

        print("Vector3.Distance(transform.position, savedLocation):" + Vector3.Distance(transform.position, savedLocation));

        if (Vector3.Distance(transform.position, savedLocation) <= 1)
        {
            Destroy(gameObject);
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

        target.TakeDamage(damage);

        Destroy(gameObject);
    }
}
