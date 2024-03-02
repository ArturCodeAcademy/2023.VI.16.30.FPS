using System;
using System.Collections.Generic;
using UnityEngine.Events;

[Serializable]
public class AmmoBackpack
{
    public UnityEvent<AmmoType, int> OnAmmunitionChanged;

    private readonly Dictionary<AmmoType, int> _ammunition;

    public AmmoBackpack()
    {
        _ammunition = new Dictionary<AmmoType, int>();
        OnAmmunitionChanged = new UnityEvent<AmmoType, int>();
    }

    public int GetAmmunition(AmmoType type)
    {
        return _ammunition.ContainsKey(type) ? _ammunition[type] : 0;
    }

    public void AddAmmunition(Ammunition ammunition)
    {
        if (_ammunition.ContainsKey(ammunition.Type))
            _ammunition[ammunition.Type] += ammunition.Amount;
        else
            _ammunition.Add(ammunition.Type, ammunition.Amount);

        OnAmmunitionChanged?.Invoke(ammunition.Type, _ammunition[ammunition.Type]);
    }

    public int TakeAmmunition(AmmoType type, int amount)
    {
        if (!_ammunition.ContainsKey(type))
            return 0;

        int availableAmmunition = _ammunition[type];
        int takenAmmunition = Math.Min(availableAmmunition, amount);
        _ammunition[type] -= takenAmmunition;
        OnAmmunitionChanged?.Invoke(type, _ammunition[type]);

        return takenAmmunition;
    }
}
