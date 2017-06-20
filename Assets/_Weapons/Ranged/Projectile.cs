using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO consider re-wire
using RPG.Core;

namespace RPG.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed; // todo add range of values
		[SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0);

		const float DESTROY_DELAY = 0.01f;
		const float MAX_RAYCAST_DISTANCE = 100f;

        GameObject target;
        Vector3 originalTargetPosition = Vector3.zero;
        float damageToDeal;

        public void SetDamage(float damage)
        {
            damageToDeal = damage;
        }

        public void Launch(GameObject target, float damageToDeal)
        {
            this.target = target;
            this.damageToDeal = damageToDeal;
        }

        void Update()
        {
            Vector3 toTarget = target.transform.position - transform.position + aimOffset;
            float distanceToMoveThisFrame = projectileSpeed * Time.deltaTime;
            if (toTarget.magnitude > distanceToMoveThisFrame)
            {
                transform.position += toTarget.normalized * distanceToMoveThisFrame;                
            }
            else
            {
                target.GetComponent<IDamageable>().TakeDamage(damageToDeal);
                Destroy(gameObject);
            }
        }
    }
}