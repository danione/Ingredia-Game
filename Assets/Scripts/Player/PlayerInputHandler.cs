using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent( typeof(PlayerInventory))]
public class PlayerInputHandler : MonoBehaviour
{
    private float nextFireTime = 0f;
    private PlayerMovement movement;
    private PlayerInventory inventory;
    private bool isNotOnCooldown = true;
    private bool hasSpawnedABat = false;

    [SerializeField] private Transform projectileObject;
    [SerializeField] private Transform knifeObject;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireRate;
    [SerializeField] private float emptyCauldronCooldown = 1.0f;
    [SerializeField] GameObject bat;


    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        inventory = GetComponent<PlayerInventory>();
    }

    public void HandleInput()
    {
        movement.Move();
        Shoot();
        ShootLaser();
        EmptyCauldron();
        AttemptRitual();
        UsePotion();
        TurnIntoGhost();
        Cheats();
        UpgradeMenu();
    }

    private void UsePotion()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) inventory.UsePotion(0);
        else if(Input.GetKeyDown(KeyCode.Alpha2)) inventory.UsePotion(1);
        else if(Input.GetKeyDown(KeyCode.Alpha3)) inventory.UsePotion(2);
    }

    public void EmptyCauldron()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isNotOnCooldown)
        {
            isNotOnCooldown = false;
            StartCoroutine(EmptyCooldown());
            PlayerEventHandler.Instance.EmptyCauldron();
        }
    }

    IEnumerator EmptyCooldown()
    {
        yield return new WaitForSeconds(emptyCauldronCooldown);
        isNotOnCooldown = true;
    }

    private void Shoot()
    {
        bool hasAvailableAmmo = PlayerController.Instance.inventory.GetFlameBombAmmo() > 0;
        bool isNotOnCooldown = Time.time >= nextFireTime;
        bool hasAvailableKnifes = PlayerController.Instance.inventory.GetKnifeAmmo() > 0;
        Transform objectToShoot = null;

        if (Input.GetKey(KeyCode.E)) {
            if(hasAvailableKnifes && isNotOnCooldown)
            {
                objectToShoot = knifeObject;
                PlayerController.Instance.inventory.SubtractKnifeAmmo();
                nextFireTime = Time.time + fireRate;
            } else if(hasAvailableAmmo && isNotOnCooldown)
            {
                objectToShoot = projectileObject;
                PlayerController.Instance.inventory.SubtractAmmo();
                nextFireTime = Time.time + fireRate;
            }
        }

        if(objectToShoot != null)
        {
            Instantiate(objectToShoot, spawnPoint.position, projectileObject.rotation);
            
        }
    }

    private void AttemptRitual()
    {
        if (Input.GetKeyDown(KeyCode.R) && inventory.possibleRitual != null && inventory.possibleRitual.IsAvailable)
        {
            inventory.AddPotion(inventory.possibleRitual.RitualData.potionReward);
            PlayerEventHandler.Instance.EmptyCauldron();
        }
    }

    private void ShootLaser()
    {
        if (Input.GetKey(KeyCode.Q)){
            PlayerEventHandler.Instance.FireLaser(true);
        }
        else
        {
            PlayerEventHandler.Instance.FireLaser(false);
        }
    }

    private void TurnIntoGhost()
    {
        if (Input.GetKey(KeyCode.Space)) PlayerEventHandler.Instance.GhostTransform(true);
        else PlayerEventHandler.Instance.GhostTransform(false);

    }

    private void Cheats()
    {
        if (Input.GetKey(KeyCode.B) && !hasSpawnedABat)
        {
            BatEnemy enemy = bat.gameObject.GetComponent<BatEnemy>();
            Instantiate(enemy, enemy.GetRandomPosition(), Quaternion.identity);
            hasSpawnedABat = true;
            StartCoroutine(ResetSpawnedBatFlagAfterDelay());
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GetComponent<PlayerInventory>().AddKnifeAmmo(100);
            GetComponent<PlayerInventory>().AddFlameBombAmmo(100);
        }
    }

    IEnumerator ResetSpawnedBatFlagAfterDelay()
    {
        yield return new WaitForSeconds(1);
        hasSpawnedABat = false;
    }

    private void UpgradeMenu()
    {
        if (Input.GetKey(KeyCode.I))
        {
            GameEventHandler.Instance.BringUpUpgradesMenu();
        }
    }
}
