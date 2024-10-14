using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public AmmoType basicAmmo, specialAmmo;
    public HUD gameHud;

    [HideInInspector]
    public Vector3 _mouseCoordinates;
    private Gun _basicGun, _specialGun;

    public  void SetGuns()
    {
        _basicGun = gameObject.AddComponent<Gun>();
        _basicGun.SetGun(basicAmmo);

        _specialGun = gameObject.AddComponent<Gun>();
        _specialGun.SetGun(specialAmmo);

        RefreshHUD();
    }
    private void RefreshHUD()
    {
        gameHud.ShowAmmoCounter(_specialGun._currentAmmo, _specialGun._maxAmmo);
    }
    public void ShootBasic()
    {
        _basicGun.Shoot(_mouseCoordinates);
    }
    public void ShootSpecial()
    {
        _specialGun.Shoot(_mouseCoordinates);
        RefreshHUD();
    }
    public void AddSpecialAmmo()
    {
        _specialGun.UpgradeAmmo(1);
        RefreshHUD();
    }
    public void RefillSpecialAmmo()
    {
        _specialGun.RefiilAmmo();
        RefreshHUD();
    }
}
