using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent( typeof(PlayerInventory))]
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerInventory inventory;
    private bool isNotOnCooldown = true;


    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireRate;
    [SerializeField] private float emptyCauldronCooldown = 1.0f;
    [SerializeField] private List<ObjectsSpawner> projectileSpawners;
    public static InputPermissions permissions = new();


    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        inventory = GetComponent<PlayerInventory>();
        permissions.UnlockAll();
        GameEventHandler.Instance.SetTutorialMode += OnSetTutorialMode;
    }

    private void OnSetTutorialMode()
    {
        permissions.LockAll();
    }

    public void HandleInput()
    {
        movement.Move();

        UseOrRemoveIngredients();

        UseAPowerUp();

        OpenMenuUI();
    }

    private void UseAPowerUp()
    {
        if(permissions.canUsePotions)
            UsePotion();
        TurnIntoGhost();
        Shoot();
    }

    private void UseOrRemoveIngredients()
    {
        if(permissions.canEmptyCauldron)
            EmptyCauldron();
        
        if(permissions.canPerformRituals)
            AttemptRitual();
    }

    private void ScrollMenu()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerEventHandler.Instance.OpenScrollMenu();
        }
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

    private void OpenMenuUI()
    {
        if (permissions.canOpenMenus)
        {
            if (permissions.canOpenUpgrades)
                UpgradeMenu();
            if(permissions.canOpenScrollMenu)
                ScrollMenu();
        }

        EscapeMenu();
    }

    private void EscapeMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerEventHandler.Instance.EscapeMenuOpen();
        }
    }

    private void ShootProjectile()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerController.Instance.inventory.SwitchEquippedWeapon();
            GameEventHandler.Instance.SwappedProjectilesPressed();
        }


        if (!Input.GetKeyDown(KeyCode.E)) return;

        Weapon equippedWeapon = PlayerController.Instance.inventory.GetCurrentlyEquippedWeapon();
        bool hasAvailableAmmo = equippedWeapon.HasAvailableAmmo();

        if (hasAvailableAmmo && isNotOnCooldown)
        {
            PlayerController.Instance.inventory.SubtractCurrentWeaponAmmo(1);
            StartCoroutine(FireAProjectile(equippedWeapon.GetObjectPosition()));
            PlayerEventHandler.Instance.FiresWeapon();
        }
    }

    private void Shoot()
    {
        ShootProjectile();
        ShootLaser();
    }

    private IEnumerator FireAProjectile(int objectToShoot)
    {
        if (objectToShoot >= 0 && objectToShoot < projectileSpawners.Count)
        {
            projectileSpawners[objectToShoot]._pool.Get().transform.position = spawnPoint.position;
            yield return new WaitForSeconds(fireRate);
            isNotOnCooldown = true;
        }
    }

    private void AttemptRitual()
    {
        if (Input.GetKeyDown(KeyCode.R) && inventory.possibleRitual != null && inventory.possibleRitual.IsAvailable)
        {
            if(Constants.Instance.currentRitualRewards >= inventory.possibleRitual.RitualData.potionRewardData.Count)
            {
                Debug.Log("Current requested ritual reward does not correspond to the actual data");
                return;
            }
            inventory.AddPotion(inventory.possibleRitual.RitualData.potionRewardData[Constants.Instance.currentRitualRewards]);
            PlayerEventHandler.Instance.RitualHasBeenPerformed();
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

    private void UpgradeMenu()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerEventHandler.Instance.BringUpUpgradesMenu();
        }
    }

    public void ChangeFireRate(float newFireRate)
    {
        fireRate = newFireRate;
    }
}
