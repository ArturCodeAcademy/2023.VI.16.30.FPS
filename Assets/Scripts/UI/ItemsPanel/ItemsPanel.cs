using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPanel : MonoBehaviour
{
    [SerializeField] private GunPanel _gunPanel;

	[Space(3)]
	[SerializeField] private ItemHolder _itemHolder;

    private List<GameObject> _allPanels;

	private void Awake()
	{
		_allPanels = new()
		{
			_gunPanel.gameObject
		};

		foreach (var panel in _allPanels)
			panel.SetActive(false);
	}

	private void OnEnable()
	{
		_itemHolder.OnItemChanged += OnItemChanged;
	}

	private void OnDisable()
	{
		_itemHolder.OnItemChanged -= OnItemChanged;
	}

	private void OnItemChanged(HoldableItem item)
	{
		foreach (var panel in _allPanels)
			panel.SetActive(false);

		switch (item)
		{
			case GunBase gunBase:
				RaycastShootingBase raycastShootingBase = gunBase.GetComponent<RaycastShootingBase>();
				_gunPanel.gameObject.SetActive(true);
				_gunPanel.SetGun(raycastShootingBase);
				break;
		}
	}
}
