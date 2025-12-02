using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public int _maxAmmo;
    public int _damage;
    public int _currentAmmo;

    [HideInInspector] public GameObject gunObj;

    [SerializeField] private float _reloadSpeed;
    [SerializeField] private int _numberOfBullets;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected float _spreadDegree;
    [SerializeField] protected int maxClip;

    [SerializeField] protected GameObject bullet;
    public Transform nozzle;
    public Sprite _logo;

    public Weapon _weaponType;
    public WeaponSlot _weaponSlotType;
    public bool _isInfiniteAmmo;
    public bool _isReloading;
    public bool _isReloaded;

    private bool isClipEmpty;
    private float _reloadTimer;
    protected float _fireTimer;

    private void Start()
    {
        if (_isInfiniteAmmo)
        {
            _reloadTimer = _reloadSpeed * 2;
        }
        else
        {
            _reloadTimer = _reloadSpeed;
        }
        gunObj = transform.parent.gameObject;
    }

    private void Update()
    {
        if (_currentAmmo <= 0)
        {
            isClipEmpty = true;
        }

        if (_currentAmmo <= 0 && _maxAmmo > 0 || _currentAmmo <= 0 && _isInfiniteAmmo)
        {
            Reload();
        }

        if (GameManager.Instance._player != null && !GameManager.Instance._player._button.buttonHeld && _fireRate <= 0)
        {
            _fireTimer = 0;
        }

        if (_fireTimer > 0)
        {
            _fireTimer -= Time.deltaTime;
        }
    }

    public virtual void Shoot()
    {
        if (isClipEmpty)
        {
            return;
        }

        if (_fireTimer <= 0 || _fireTimer == _fireRate)
        {
            for (int i = 0; i < _numberOfBullets; i++)
            {
                float halfSpread = _spreadDegree / 2.0f;
                float randomOffset = Random.Range(-halfSpread, halfSpread);
                Instantiate(bullet, gunObj.GetComponent<Unit>()._currentGun.nozzle.gameObject.transform.position, gunObj.GetComponent<Unit>()._currentGun.nozzle.gameObject.transform.rotation * Quaternion.Euler(0, 0, randomOffset).normalized);
                bullet.GetComponent<Bullet>().bulletDamage = _damage;
            }
            _currentAmmo--;
            _fireTimer = _fireRate;
        }
    }

    public virtual void Reload()
    {
        if (_reloadTimer > 0)
        {
            _reloadTimer -= Time.deltaTime;
            _isReloading = true;
        }

        if (_reloadTimer <= 0)
        {
            if (_isInfiniteAmmo)
            {
                _currentAmmo = maxClip;
            }
            _isReloading = false;

            _maxAmmo -= maxClip - _currentAmmo;

            if (_maxAmmo < _currentAmmo && _currentAmmo == 0)
            {
                _currentAmmo = _maxAmmo + maxClip;
            }
            else
            {
                _currentAmmo = maxClip;
            }

            
            if (_maxAmmo < 0)
            {
                _maxAmmo = 0;
            }

            if (_isInfiniteAmmo)
            {
                _reloadTimer = _reloadSpeed * 2;
            }
            else
            {
                _reloadTimer = _reloadSpeed;
            }

            _isReloaded = true;
            isClipEmpty = false;
        }
    }
}
