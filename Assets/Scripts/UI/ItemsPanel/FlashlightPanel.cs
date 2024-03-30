using UnityEngine;
using UnityEngine.UI;

public class FlashlightPanel : MonoBehaviour
{
	[SerializeField] private Image _item;
	[SerializeField] private Image _lights;

	private FlashlightItem? _flashlightItem;

    public void SetFlashlight(FlashlightItem flashlightItem)
	{
		Unsubscribe();
		_flashlightItem = flashlightItem;
		Subscribe();

		UpdateUI();
	}

	private void Unsubscribe()
	{
		if (_flashlightItem is not null)
		{
			_flashlightItem.StateChanged -= UpdateUI;
		}
	}

	private void Subscribe()
	{
		if (_flashlightItem is not null)
		{
			_flashlightItem.StateChanged += UpdateUI;
		}
	}

	private void UpdateUI()
	{
		if (_flashlightItem is null)
			return;

		_item.color = _flashlightItem.LightColor;
		_lights.color = _flashlightItem.LightColor;
		_lights.enabled = _flashlightItem.IsOn;
	}
}
