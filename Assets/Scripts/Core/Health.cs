﻿using RPG.Saving;
using UnityEngine;

namespace RPG.Core
{ 
    public class Health: MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public bool getIsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0); // Ak klesneme pod 0, tak dostaneme 0

            if (healthPoints == 0)
            {
                Die();
            }
        }

        object ISaveable.CaptureState()
        {
            return healthPoints;
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;

            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        void ISaveable.RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints == 0)
            {
                Die();
            }
        }
    }
}