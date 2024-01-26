using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquippedWeaponUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_WeaponName;
    [SerializeField] private TextMeshProUGUI m_Ammo;
    private PlayerInventory m_Inventory;
    private Weapon currentlyEquippedWeapon;

    private void Start()
    {
        m_Inventory = PlayerController.Instance.inventory;
        currentlyEquippedWeapon = m_Inventory.GetCurrentlyEquippedWeapon();
        GameEventHandler.Instance.SwappedProjectiles += OnSwapWeapon;
        PlayerEventHandler.Instance.FiredWeapon += OnChangeWeapon;
        InputEventHandler.instance.UsedPotion += OnChangeWeapon;
    }

    private void OnChangeWeapon()
    {
        if (currentlyEquippedWeapon == null) { return; }

        m_WeaponName.text = currentlyEquippedWeapon.weaponName;
        m_Ammo.text = currentlyEquippedWeapon.ammo.ToString();
    }

    private void OnSwapWeapon()
    {
        currentlyEquippedWeapon = m_Inventory.GetCurrentlyEquippedWeapon();
        OnChangeWeapon();
    }

}
