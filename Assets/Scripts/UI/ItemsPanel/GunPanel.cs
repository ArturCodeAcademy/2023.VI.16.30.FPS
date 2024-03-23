using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private Image _ammoTypeImage;
    [SerializeField] private Image _reloadFill;
	[SerializeField] private GameObject _reloadSlider;

	[SerializeField] protected AmmoPanelUI.AmmoSprites[] _ammunitionSprites;

	private RaycastShootingBase _shooting;
	private bool _isReloading = false;

	public void SetGun(RaycastShootingBase? shooting)
	{
		Unsubscribe();
		_shooting = shooting;
		Subscribe();

		UpdateUI();
	}

	private void Unsubscribe()
	{
		_isReloading = false;
		_reloadSlider.SetActive(false);
		if (_shooting is not null)
		{
			_shooting.OnShoot -= UpdateUI;
			_shooting.OnStartReload -= OnStartReloading;
			_shooting.OnEndReload -= OnEndReloading;
		}
	}

	private void Subscribe()
	{
		if (_shooting is not null)
		{
			_shooting.OnShoot += UpdateUI;
			_shooting.OnStartReload += OnStartReloading;
			_shooting.OnEndReload += OnEndReloading;
			_reloadSlider.SetActive(true);
		}
	}

	private void OnStartReloading()
	{
		_isReloading = true;
		_reloadSlider.SetActive(true);
	}

	private void OnEndReloading()
	{
		_isReloading = false;
		_reloadSlider.SetActive(false);
		UpdateUI();
	}

	private void Update()
	{
		if (_isReloading)
			_reloadFill.fillAmount = _shooting.ReloadProgress;
	}

	private void UpdateUI()
	{
		if (_shooting is null)
			return;

		int ammoCount = Player.Instance.Backpack.Ammo.GetAmmunition(_shooting.AmmoType);
		_ammoText.text = $"{_shooting.MagazineAmmoCount} / {ammoCount}";
		_ammoTypeImage.sprite = _ammunitionSprites.FirstOrDefault(x => x.Type == _shooting.AmmoType)?.Sprite;
	}
}
