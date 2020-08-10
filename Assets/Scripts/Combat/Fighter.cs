using System;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;

        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;

        [SerializeField] Weapon defaultWeapon = null;

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        Weapon currentWeapon = null;

        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.getIsDead()) return;
            

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;

            Animator animator = GetComponent<Animator>();

            //if (weapon == null) return;

            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        internal bool CanAttack(object gameObject)
        {
            throw new NotImplementedException();
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform); // Look at the enemy first

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();

                //Health healthComponent = target.GetComponent<Health>();
                //healthComponent.TakeDamage(weaponDamage);
                timeSinceLastAttack = 0;
            }
        }

        internal void Attack(object gameObject)
        {
            throw new NotImplementedException();
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation Event - it is not called from a code, it is called from an animator
        void Hit()
        {
            if (target == null) return;

            target.TakeDamage(currentWeapon.getWeaponDamage());
        }

        private bool GetIsInRange()
        {
            if (currentWeapon == null) return false;
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.getWeaponRange();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.getIsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}