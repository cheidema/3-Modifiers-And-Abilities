﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement; // TODO question wisdom of having here rather than core

// TODO consider re-wire...
using RPG.CameraUI;
using RPG.Core;
using RPG.Weapons;

namespace RPG.Characters
{
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] float maxHealthPoints = 100f;
        [SerializeField] float baseDamage = 10f;
        [SerializeField] Weapon weaponInUse = null;
        [SerializeField] AnimatorOverrideController animatorOverrideController = null; // TODO hide
       
        [SerializeField] AudioClip[] damageSounds;
        [SerializeField] AudioClip deathSound;

        // Temporarily serialized for debugging
        [SerializeField] SpecialAbility[] abilities;

        const string ATTACK_TRIGGER = "Attack";
        const string DEATH_TRIGGER = "Death";
        const string DEFAULT_ATTACK_CLIP_NAME = "DEFAULT ATTACK";

        Enemy enemy = null;
        AudioSource audioSource = null;
        Animator animator = null;
        float currentHealthPoints;
        CameraRaycaster cameraRaycaster;
        float lastHitTime = 0f;

        public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }

        void Start()
        {
            RegisterForMouseClick();
            SetCurrentMaxHealth();
            PutWeaponInHand();
            SetupRequiredComponents();
        }

        private void SetupRequiredComponents()
        {
            SetupRuntimeAnimator();
            abilities[0].AttachComponentTo(gameObject);
            audioSource = GetComponent<AudioSource>();
        }

        public void TakeDamage(float damage)
        {
            if (currentHealthPoints - damage <= 0)
            {
                ReduceHealth(damage);
                StartCoroutine(KillPlayer());
            }
            else
            {
                ReduceHealth(damage);
            }

        }

        IEnumerator KillPlayer()
        {
            audioSource.clip = deathSound;
			audioSource.Play();
            animator.SetTrigger(DEATH_TRIGGER);
            yield return new WaitForSecondsRealtime(deathSound.length);
            SceneManager.LoadScene(0);
        }

        private void ReduceHealth(float damage)
        {
			currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
			int clipIndex = UnityEngine.Random.Range(0, damageSounds.Length);
			audioSource.clip = damageSounds[clipIndex];
			audioSource.Play();
        }

        private void SetCurrentMaxHealth()
        {
            currentHealthPoints = maxHealthPoints;
        }

        private void SetupRuntimeAnimator()
        {
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;
            animatorOverrideController[DEFAULT_ATTACK_CLIP_NAME] = weaponInUse.GetAttackAnimClip(); // remove const
        }

        private void PutWeaponInHand()
        {
            var weaponPrefab = weaponInUse.GetWeaponPrefab();
            GameObject dominantHand = RequestDominantHand();
            var weapon = Instantiate(weaponPrefab, dominantHand.transform);
            weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
            weapon.transform.localRotation = weaponInUse.gripTransform.localRotation;
        }

        private GameObject RequestDominantHand()
        {
            var dominantHands = GetComponentsInChildren<DominantHand>();
            int numberOfDominantHands = dominantHands.Length;
            Assert.IsFalse(numberOfDominantHands <= 0, "No DominantHand found on Player, please add one");
            Assert.IsFalse(numberOfDominantHands > 1, "Multiple DominantHand scripts on Player, please remove one");
            return dominantHands[0].gameObject;
        }

        private void RegisterForMouseClick()
        {
            cameraRaycaster = FindObjectOfType<CameraUI.CameraRaycaster>();
            cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
        }

        void OnMouseOverEnemy(Enemy enemyMouseIsOverThisFrame)
        {
            this.enemy = enemyMouseIsOverThisFrame;
            if (Input.GetMouseButton(0) && IsTargetInRange(enemy.gameObject))
            {
                AttackCurrentEnemy();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                AttemptSpecialAbility(0);
            }
        }

        private void AttemptSpecialAbility(int abilityIndex)
        {
            var energyComponent = GetComponent<Energy>();
            var energyCost = abilities[abilityIndex].GetEnergyCost();

            if (energyComponent.IsEnergyAvailable(energyCost))
            {
                energyComponent.ConsumeEnergy(energyCost);
                var abilityParams = new AbilityUseParams(enemy, baseDamage);
                abilities[abilityIndex].Use(abilityParams);
            }
        }

        private void AttackCurrentEnemy()
        {
            if (Time.time - lastHitTime > weaponInUse.GetMinTimeBetweenHits())
            {
                animator.SetTrigger(ATTACK_TRIGGER);
                lastHitTime = Time.time;
            }
        }

        // Note this method name must match that in the animation
        public void Hit()
        {
			enemy.TakeDamage(baseDamage);
        }

        private bool IsTargetInRange(GameObject target)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= weaponInUse.GetMaxAttackRange();
        }
    }
}