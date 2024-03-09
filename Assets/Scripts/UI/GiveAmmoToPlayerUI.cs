using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiveAmmoToPlayerUI : MonoBehaviour
{
    [SerializeField] private GiveAmmoToPlayer _giveAmmoToPlayer;

    [SerializeField] private TMP_Text _ammunitionNameText;

    private void Start()
	{
		UpdateUI();
	}

	private void UpdateUI()
	{
		_ammunitionNameText.text = $"{_giveAmmoToPlayer.Ammunition.Type}\n{_giveAmmoToPlayer.Ammunition.Amount}";
	}

#if UNITY_EDITOR

	private void OnValidate()
	{
		_giveAmmoToPlayer ??= GetComponent<GiveAmmoToPlayer>();
		if (_giveAmmoToPlayer is not null)
		{
			UpdateUI();
		}
	}

#endif
}
