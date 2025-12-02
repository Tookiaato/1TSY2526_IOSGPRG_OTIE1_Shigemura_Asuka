using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
{
    public List<TextMeshProUGUI> _gunAmmoCarryUIs;
    public TextMeshProUGUI _pistolAmmoCarryUI;
    public TextMeshProUGUI _shotgunAmmoCarryUI;
    public TextMeshProUGUI _automaticRifleAmmoCarryUI;

    public TextMeshProUGUI _currentAmmoUI;
    public TextMeshProUGUI _maxAmmoUI;

    public TextMeshProUGUI _healthKitsUI;
    public TextMeshProUGUI _healTime;

    public TextMeshProUGUI _enemiesLeft;

    public Image _primaryBtn;
    public Image _secondaryBtn;

    public Image _primaryBtnLogo;
    public Image _secondaryBtnLogo;
    
    public Slider _hpSlider;
    public Slider _healTimeBarSlider;

    public Spawner spawner;

    private void Start()
    {
        _hpSlider.enabled = false;
        _healTimeBarSlider.enabled = false;
        _healTimeBarSlider.gameObject.SetActive(false);
    }

    public void WeaponSlotColorChange(Color primarySlotColor, Color secondarySlotColor)
    {
       _primaryBtn.GetComponent<Image>().color = primarySlotColor;
       _secondaryBtn.GetComponent<Image>().color = secondarySlotColor;
    }

    public void primaryImageSlot(Sprite primaryImage)
    {
        _primaryBtnLogo.sprite = primaryImage;
    }

    public void secondaryImageSlot(Sprite secondaryImage)
    {
        _secondaryBtnLogo.sprite = secondaryImage;
    }

    public void UpdateAmmoUI()
    {
        if (GameManager.Instance._player != null)
        {
            _currentAmmoUI.text = "" + GameManager.Instance._player._currentGun.GetComponent<Gun>()._currentAmmo;
            _maxAmmoUI.text = "" + GameManager.Instance._player._currentGun.GetComponent<Gun>()._maxAmmo;
        }
    }

    public void UpdatePlayerHealth()
    {
        if (GameManager.Instance._player == null)
        {
            _hpSlider.value = 0;
            return;
        }

        Health health = GameManager.Instance._player.gameObject.GetComponent<Health>();
        _hpSlider.value = (float)health.CurrentHealth / (float)health.MaxHealth;
        
    }

    public void UpdateHealthKitAmount()
    {
        _healthKitsUI.text = "" + GameManager.Instance._inventory._healthKits;
    }

    public void UpdateHealTimeBar()
    {
        Inventory inventory = GameManager.Instance._inventory;
        if (inventory._isHealing)
        {
            _healTimeBarSlider.gameObject.SetActive(true);
            _healTimeBarSlider.value = inventory.HealTimer / inventory.HealTime;
            _healTime.text = "" + inventory.HealTimer.ToString("F2");
        }
        else
        {
            _healTimeBarSlider.gameObject.SetActive(false);
        }
    }

    public void UpdateEnemyCount()
    {
        _enemiesLeft.text = "" + spawner._enemies.Count;
    }
}
