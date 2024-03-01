using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquippedWeaponUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_WeaponName;
    [SerializeField] private TextMeshProUGUI m_Ammo;
    [SerializeField] private TextMeshProUGUI m_Unlimited;
    private PlayerInventory m_Inventory;
    private Weapon currentlyEquippedWeapon;

    private void Start()
    {
        m_Inventory = PlayerController.Instance.inventory;
        currentlyEquippedWeapon = m_Inventory.GetCurrentlyEquippedWeapon();
        GameEventHandler.Instance.SwappedProjectiles += OnSwapWeapon;
        PlayerEventHandler.Instance.FiredWeapon += OnChangeWeapon;
        InputEventHandler.instance.UsedPotion += OnChangeWeapon;
        GameEventHandler.Instance.UpdatedUI += OnUpdateUI;
    }

    private void OnDestroy()
    {
        try
        {
            GameEventHandler.Instance.SwappedProjectiles -= OnSwapWeapon;
            PlayerEventHandler.Instance.FiredWeapon -= OnChangeWeapon;
            InputEventHandler.instance.UsedPotion -= OnChangeWeapon;
            GameEventHandler.Instance.UpdatedUI -= OnUpdateUI;
        }
        catch { }
        
    }

    private void OnChangeWeapon()
    {
        if (currentlyEquippedWeapon == null) { return; }

        m_WeaponName.text = currentlyEquippedWeapon.weaponName;

        if (!currentlyEquippedWeapon.IsUnlimited)
        {
            m_Unlimited.gameObject.SetActive(false);
            m_Ammo.gameObject.SetActive(true);
            m_Ammo.text = currentlyEquippedWeapon.ammo.ToString();
        }
        else
        {
            m_Ammo.gameObject.SetActive(false);
            m_Unlimited.gameObject.SetActive(true);
        }
    }

    private void OnSwapWeapon()
    {
        currentlyEquippedWeapon = m_Inventory.GetCurrentlyEquippedWeapon();
        OnChangeWeapon();
    }

    private void OnUpdateUI()
    {
        OnSwapWeapon();
    }
}
