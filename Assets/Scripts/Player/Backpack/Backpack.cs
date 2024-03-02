using UnityEngine;

public class Backpack : MonoBehaviour
{
    [field:SerializeField] public AmmoBackpack Ammo { get; private set; }

    [SerializeField] private Ammunition[] _initialAmmunition;

    private void Awake()
    {
        Ammo = new AmmoBackpack();

        foreach (var ammunition in _initialAmmunition)
            Ammo.AddAmmunition(ammunition);
        _initialAmmunition = null;
    }
}
