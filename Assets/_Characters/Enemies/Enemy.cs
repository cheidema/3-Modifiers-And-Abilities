﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO consider re-wire
using RPG.Core;
using RPG.Weapons;

namespace RPG.Characters
{
    public class Enemy : MonoBehaviour, IDamageable
    {

        [SerializeField] float maxHealthPoints = 100f;
        [SerializeField] float chaseRadius = 6f;

        [SerializeField] float attackRadius = 4f;
        [SerializeField] float damagePerShot = 9f;
        [SerializeField] float firingPeriodInS = 0.5f;
        [SerializeField] float firingPeriodVariation = 0.1f;
        [SerializeField] GameObject projectileToUse;
        [SerializeField] GameObject projectileSocket;

        bool isAttacking = false;
        float currentHealthPoints;
        AICharacterControl aiCharacterControl = null;
        Player player = null;

        public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }

        public void TakeDamage(float damage)
        {
            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
            if (currentHealthPoints <= 0) { Destroy(gameObject); }
        }

        void Start()
        {
            player = FindObjectOfType<Player>();
            aiCharacterControl = GetComponent<AICharacterControl>();
            currentHealthPoints = maxHealthPoints;
        }

        void Update()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (player.healthAsPercentage < Mathf.Epsilon)
            {
                Destroy(this); // Stop enemy behaviour on death
            }

            if (distanceToPlayer <= attackRadius && !isAttacking)
            {
                isAttacking = true;
                StartCoroutine(RepeatFire());
            }

            if (distanceToPlayer > attackRadius)
            {
                isAttacking = false;
                StopCoroutine(RepeatFire());
            }

            if (distanceToPlayer <= chaseRadius)
            {
                aiCharacterControl.SetTarget(player.transform);
            }
            else
            {
                aiCharacterControl.SetTarget(transform);
            }
        }

        IEnumerator RepeatFire()
        {
            while (isAttacking)
            {
				float randomisedDelay = firingPeriodInS * Random.Range(1f - firingPeriodVariation, 1f + firingPeriodVariation);
				yield return new WaitForSeconds(randomisedDelay);

                GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);

                Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
                projectileComponent.Launch(player.gameObject, damagePerShot);
            }
        }

        void OnDrawGizmos()
        {
            // Draw attack sphere 
            Gizmos.color = new Color(255f, 0, 0, .5f);
            Gizmos.DrawWireSphere(transform.position, attackRadius);

            // Draw chase sphere 
            Gizmos.color = new Color(0, 0, 255, .5f);
            Gizmos.DrawWireSphere(transform.position, chaseRadius);
        }
    }
}