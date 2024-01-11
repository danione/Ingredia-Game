[System.Serializable]
public class InputPermissions
{
    public bool canMove = true;
    public bool canEmptyCauldron = true;
    public bool canControlCharacter = true;
    public bool canOpenMenus = true;
    public bool canOpenUpgrades = true;
    public bool canOpenScrollMenu = true;
    public bool canPerformRituals = true;
    public bool canShoot = true;
    public bool canUsePotions = true;

    public void UnlockAll()
    {
        canMove = true;
        canEmptyCauldron = true;
        canControlCharacter = true;
        canOpenMenus = true;
        canOpenUpgrades = true;
        canOpenScrollMenu = true;
        canPerformRituals = true;
        canShoot = true;
        canUsePotions = true;
    }

    public void LockAll()
    {
        canMove = false;
        canEmptyCauldron = false;
        canControlCharacter = false;
        canOpenMenus = false;
        canOpenUpgrades = false;
        canOpenScrollMenu = false;
        canPerformRituals = false;
        canShoot = false;
        canUsePotions = false;
    }
}
