[System.Serializable]
public class InputPermissions
{
    public bool canMove = false;
    public bool canEmptyCauldron = false;
    public bool canControlCharacter = false;
    public bool canOpenMenus = false;
    public bool canOpenUpgrades = false;
    public bool canOpenScrollMenu = false;
    public bool canPerformRituals = false;
    public bool canShoot = false;
    public bool canUsePotions = false;

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
}
