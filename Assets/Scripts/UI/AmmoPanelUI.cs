using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmmoPanelUI : MonoBehaviour
{
    [SerializeField] private AmmoUI _ammoUIPrefab;

    [SerializeField] private AmmoSprites[] _ammunitionSprites;

    [Space(3)]
    [SerializeField] private Backpack _playerBackpack;

    private Dictionary<AmmoType, AmmoUI> _ammoUIs = new();

    private void Start()
    {
        _playerBackpack.Ammo.OnAmmunitionChanged.AddListener(UpdateAmmoUI);
        Init();
    }

    private void OnDestroy()
    {
        _playerBackpack.Ammo.OnAmmunitionChanged.RemoveListener(UpdateAmmoUI);
    }

    private void Init()
    {
        foreach (AmmoType type in Enum.GetValues(typeof(AmmoType)))
        {
            int count = _playerBackpack.Ammo.GetAmmunition(type);
            if (count > 0)
                AddType(type, count);
        }
    }

    private void UpdateAmmoUI(AmmoType type, int count)
    {
        if (_ammoUIs.ContainsKey(type))
        {
            _ammoUIs[type].CountText.text = count.ToString();
        }
        else
        {
            AddType(type, count);
        }
    }

    private void AddType(AmmoType type, int count)
    {
        if (_ammoUIs.ContainsKey(type))
            return;
       
        var ammoUI = Instantiate(_ammoUIPrefab, transform);
        ammoUI.gameObject.SetActive(true);
        ammoUI.AmmoImage.sprite = _ammunitionSprites
            .FirstOrDefault(x => x.Type == type)?.Sprite;
        ammoUI.TypeText.text = type.ToString();
        ammoUI.CountText.text = count.ToString();
        _ammoUIs.Add(type, ammoUI);
    }

    [Serializable]
    public class AmmoSprites
    {
        public AmmoType Type;
        public Sprite Sprite;
    }
}
