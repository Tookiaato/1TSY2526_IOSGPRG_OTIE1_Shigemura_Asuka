using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : Unit
{
    public ButtonHold _button;
    public bool isShooting;

    [SerializeField] private Joystick _moveJoystick;
    [SerializeField] private Joystick _aimJoystick;

    private Rigidbody2D _rb2D;

    private void Start()
    {
        base.Initialize("Player", 100, 10);
        GameManager.Instance.InitializePlayer(this);

        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        Aim();
        HandleShooting();
        HandleReloadFinish();
    }

    public override void Shoot()
    {
        base.Shoot();
        
        Debug.Log($"{_name} is Shooting");
    }

    public override void Reload()
    {
        base.Reload();
    }

    public void SetCurrentGun(Gun gun)
    {
        _currentGun = gun;
    }

    private void Movement()
    {
        _rb2D.linearVelocity = new Vector3(
            _moveJoystick.Horizontal * _speed,
            _moveJoystick.Vertical * _speed,
            0f
        );
    }

    private void Aim()
    {
        Vector3 moveVector = Vector3.up * _aimJoystick.Vertical -
                             Vector3.left * _aimJoystick.Horizontal;

        if (_aimJoystick.Horizontal != 0 || _aimJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        }
    }

    private void HandleShooting()
    {
        if (_currentGun != null &&
            _button.buttonHeld &&
            !GameManager.Instance._inventory._isSwitched)
        {
            Shoot();
            GameUI.Instance.UpdateAmmoUI();

            GameUI.Instance._healTimeBarSlider.gameObject.SetActive(false);

            isShooting = true;
        }
        else if (!_button.buttonHeld)
        {
            isShooting = false;
        }
    }

    private void HandleReloadFinish()
    {
        if (_currentGun == null ||
            _currentGun._isReloading ||
            !_currentGun._isReloaded)
            return;

        Inventory inv = GameManager.Instance._inventory;
        int weaponIndex = (int)_currentGun._weaponType;

        inv.ammos[weaponIndex]._gunAmmoCarry = _currentGun._maxAmmo;

        GameUI.Instance._gunAmmoCarryUIs[weaponIndex].text =
            inv.ammos[weaponIndex]._gunAmmoCarry.ToString();

        GameUI.Instance.UpdateAmmoUI();

        _currentGun._isReloaded = false;
    }
}