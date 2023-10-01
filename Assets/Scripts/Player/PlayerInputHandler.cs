using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(PlayerMovement))]
public class PlayerInputHandler : MonoBehaviour
{
    private float nextFireTime = 0f;
    private PlayerMovement movement;
    private bool isNotOnCooldown = true;


    [SerializeField] private Transform projectileObject;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireRate;
    [SerializeField] private float emptyCauldronCooldown = 1.0f;
    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    public void HandleInput()
    {
        movement.Move();
        Shoot();
        EmptyCauldron();
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
}
