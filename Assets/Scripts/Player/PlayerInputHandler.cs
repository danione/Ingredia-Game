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
        EmptyCauldron();
        UseRitual();
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

        if (Input.GetKey(KeyCode.E) && hasAvailableAmmo && isNotOnCooldown)
        {
            Instantiate(projectileObject, spawnPoint.position, projectileObject.rotation);
            PlayerController.Instance.inventory.SubtractAmmo();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void UseRitual()
    {
        if(Input.GetKeyDown(KeyCode.R) && inventory.possibleRitual != null && inventory.possibleRitual.IsAvailable)
        {
            RewardEventHandler.Instance.PlayerReward(inventory.possibleRitual.Reward);
            PlayerEventHandler.Instance.EmptyCauldron();
        }
    }

    private void ShootLaser()
    {
        if (Input.GetKey(KeyCode.Q)){
            PlayerEventHandler.Instance.FireLaser();
        }
    }
}
