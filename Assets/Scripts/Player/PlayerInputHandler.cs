using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMovement))]
public class PlayerInputHandler : MonoBehaviour
{
    private float nextFireTime = 0f;
    private PlayerMovement movement;


    [SerializeField] private Transform projectileObject;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireRate;
    
    

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    public void HandleInput()
    {
        movement.Move();
        Shoot();
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
