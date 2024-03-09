using UnityEngine;

public class GiveAmmoToPlayer : MonoBehaviour
{
    [field: SerializeField] public Ammunition Ammunition { get; private set; }

    public void GiveAmmo()
    {
		Player.Instance.Backpack.Ammo.AddAmmunition(Ammunition);
	}
}
