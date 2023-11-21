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


    [SerializeField] private Transform projectileObject;
    [SerializeField] private Transform knifeObject;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireRate;
    [SerializeField] private float emptyCauldronCooldown = 1.0f;

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
        bool hasAvailableAmmo = PlayerController.Instance.inventory.ammo > 0;
        bool isNotOnCooldown = Time.time >= nextFireTime;
        bool hasAvailableKnifes = PlayerController.Instance.inventory.knifeAmmo > 0;
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
            inventory.AddPotion(inventory.possibleRitual.RewardPotion);
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
}
