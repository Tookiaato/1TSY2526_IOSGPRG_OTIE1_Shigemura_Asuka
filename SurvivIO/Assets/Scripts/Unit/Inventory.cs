using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Ammos
{
    public string _name;
    public Weapon _gunType;
    public int _gunAmmoMaxCarry;
    public int _gunAmmoCarry;
}

public class Inventory : MonoBehaviour
{
    public Ammos[] ammos;
    public bool _isSwitched;
    public bool _isHealing;
    public int _healthKits;
    public int _maxHealthKitCarry;

    private Player _player;

    private Gun _primaryWeapon;
    private Gun _secondaryWeapon;
    private int _prevGunAmmo;

    private float _switchTime;
    private float _switchTimer;
    private float _healTime;
    private float _healTimer;

    public float HealTime => _healTime;
    public float HealTimer => _healTimer;

    private void Start()
    {
        _player = GameManager.Instance._player;

        _switchTime = 1f;
        _switchTimer = _switchTime;

        _healTime = 3f;
        _healTimer = _healTime;
    }

    private void Update()
    {
        HandleHealing();
        HandleWeaponSwitchCooldown();
    }

    private void HandleHealing()
    {
        if (_isHealing && !_player.isShooting)
        {
            StartHealTimer();
            GameUI.Instance.UpdateHealTimeBar();
            return;
        }

        if (_player.isShooting)
        {
            _isHealing = false;
            _healTimer = _healTime;
        }
    }

    private void HandleWeaponSwitchCooldown()
    {
        if (_isSwitched && _switchTimer > 0)
        {
            _switchTimer -= Time.deltaTime;
            return;
        }

        if (_switchTimer <= 0)
        {
            _isSwitched = false;
            _switchTimer = _switchTime;
        }
    }

    public void PickUpWeapon(Gun gun)
    {
        _isSwitched = true;

        if (_primaryWeapon != null && _secondaryWeapon == null)
        {
            _prevGunAmmo = _player._currentGun._currentAmmo;
        }

        if (gun._weaponSlotType == WeaponSlot.Primary)
        {
            _primaryWeapon = gun;
            EquipWeapon(_primaryWeapon);

            GameUI.Instance.WeaponSlotColorChange(
                new Color(0, 0, 0, 1f),
                new Color(0, 0, 0, 0.5f)
            );

            GameUI.Instance.primaryImageSlot(_primaryWeapon._logo);
        }
        else if (_secondaryWeapon == null && gun._weaponSlotType == WeaponSlot.Secondary)
        {
            _secondaryWeapon = gun;
            EquipWeapon(_secondaryWeapon);

            GameUI.Instance.WeaponSlotColorChange(
                new Color(0, 0, 0, 0.5f),
                new Color(0, 0, 0, 1f)
            );

            GameUI.Instance.secondaryImageSlot(_secondaryWeapon._logo);
        }
    }

    public void OnPrimaryClick()   => SwitchWeapon(WeaponSlot.Primary);
    public void OnSecondaryClick() => SwitchWeapon(WeaponSlot.Secondary);

    public void SwitchWeapon(WeaponSlot weaponSlot)
    {
        Gun selected = weaponSlot == WeaponSlot.Primary ? _primaryWeapon : _secondaryWeapon;

        if (selected == null)
            return;

        selected._currentAmmo = _prevGunAmmo;
        _prevGunAmmo = _player._currentGun._currentAmmo;

        EquipWeapon(selected);

        if (weaponSlot == WeaponSlot.Primary)
        {
            GameUI.Instance.WeaponSlotColorChange(
                new Color(0, 0, 0, 1f),
                new Color(0, 0, 0, 0.5f)
            );
        }
        else
        {
            GameUI.Instance.WeaponSlotColorChange(
                new Color(0, 0, 0, 0.5f),
                new Color(0, 0, 0, 1f)
            );
        }
    }

    private void EquipWeapon(Gun gun)
    {
        if (_player._currentGun != null &&
            _player._currentGun._weaponType != Weapon.None)
        {
            Destroy(_player._currentGun.gameObject);
        }

        GameObject tempGun = Instantiate(gun.gameObject, _player.transform);
        tempGun.SetActive(true);

        _player.SetCurrentGun(tempGun.GetComponent<Gun>());
        _player._currentGun.gunObj = transform.gameObject;
        _player._currentGun._currentAmmo = 0;

        UpdateAmmo();
    }

    public void AddAmmo(Weapon weapon, int amount)
    {
        int index = (int)weapon;

        if (index < 0 || index >= ammos.Length)
            return;

        Ammos ammo = ammos[index];
        ammo._gunAmmoCarry = Mathf.Min(ammo._gunAmmoCarry + amount, ammo._gunAmmoMaxCarry);
        ammos[index] = ammo;

        GameUI.Instance._gunAmmoCarryUIs[index].text = ammo._gunAmmoCarry.ToString();

        UpdateAmmo();
    }

    public void PickupHealthKit()
    {
        _healthKits = Mathf.Min(_healthKits + 1, _maxHealthKitCarry);
        GameUI.Instance.UpdateHealthKitAmount();
    }

    public void StartHealTimer()
    {
        if (_healTimer > 0)
        {
            _healTimer -= Time.deltaTime;
            return;
        }

        Health health = _player.GetComponent<Health>();
        health.CurrentHealth = Mathf.Min(health.CurrentHealth + 30, health.MaxHealth);

        _healthKits--;
        GameUI.Instance.UpdatePlayerHealth();
        GameUI.Instance.UpdateHealthKitAmount();

        _healTimer = _healTime;
        _isHealing = false;
    }

    private void UpdateAmmo()
    {
        if (_player._currentGun == null)
            return;

        int index = (int)_player._currentGun._weaponType;
        if (index < 0 || index >= ammos.Length)
            return;

        _player._currentGun._maxAmmo = ammos[index]._gunAmmoCarry;
        GameUI.Instance.UpdateAmmoUI();
    }
}
