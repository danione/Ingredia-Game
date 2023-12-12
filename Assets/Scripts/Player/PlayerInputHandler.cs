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
    private bool hasSpawnedABat = false;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireRate;
    [SerializeField] private float emptyCauldronCooldown = 1.0f;
    [SerializeField] GameObject bat;
    [SerializeField] private List<ObjectsSpawner> projectileSpawners;


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
        ScrollMenu();
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

    private void Shoot()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        bool hasAvailableAmmo = PlayerController.Instance.inventory.GetFlameBombAmmo() > 0;
        bool hasAvailableKnifes = PlayerController.Instance.inventory.GetKnifeAmmo() > 0;
        int objectToShoot = -1;

        if(hasAvailableKnifes && isNotOnCooldown)
        {
            isNotOnCooldown = false;
            objectToShoot = 1;
            PlayerController.Instance.inventory.SubtractKnifeAmmo();
        } else if(hasAvailableAmmo && isNotOnCooldown)
        {
            isNotOnCooldown = false;
            objectToShoot = 0;
            PlayerController.Instance.inventory.SubtractAmmo();
        }
        

        if(objectToShoot >= 0 && objectToShoot < projectileSpawners.Count)
        {
            StartCoroutine(FireAProjectile(objectToShoot));
        }
    }

    private IEnumerator FireAProjectile(int objectToShoot)
    {
        projectileSpawners[objectToShoot]._pool.Get().transform.position = spawnPoint.position;
        yield return new WaitForSeconds(fireRate);
        isNotOnCooldown = true;
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
        if (Input.GetKeyDown(KeyCode.B) && !hasSpawnedABat)
        {
            RitualistEnemy enemy = bat.gameObject.GetComponent<RitualistEnemy>();
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameEventHandler.Instance.BringUpUpgradesMenu();
        }
    }

    public void ChangeFireRate(float newFireRate)
    {
        fireRate = newFireRate;
    }
}
