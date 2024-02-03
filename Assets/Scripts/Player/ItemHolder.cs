using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
	public int ItemCount => _items.Count;

	public event Action<HoldableItem> OnItemAdded;
	public event Action<HoldableItem> OnItemRemoved;
	public event Action<HoldableItem?> OnItemChanged;

    private HoldableItem? _current;
    private int _currentIndex;
    private List<HoldableItem> _items;

	public HoldableItem this[int index] => _items[index];

    private void Awake()
    {
		_items = GetComponentsInChildren<HoldableItem>().ToList();

        foreach (var item in _items)
        {
            item.OnHide();
			item.gameObject.SetActive(false);
		}

        _currentIndex = 0;
        if (_items.Count > 0)
        {
			_current = _items[_currentIndex];
			_current.OnShow();
		}
	}

	private void Update()
	{
		float scroll = Input.mouseScrollDelta.y;
		if (scroll != 0)
		{
			if (scroll > 0)
				NextItem();
			else
				PreviousItem();
		}

		if (Input.GetKeyDown(KeyCode.G))
		{
			if (_current.Droppable)
				RemoveItem(_current);
		}
	}

	private void NextItem()
	{
		if (_items.Count < 2)
			return;

		_current.OnHide();
		_current.gameObject.SetActive(false);
		_currentIndex = (_currentIndex + 1) % _items.Count;
		_current = _items[_currentIndex];
		_current.gameObject.SetActive(true);
		_current.OnShow();

		OnItemChanged?.Invoke(_current);
	}

	private void PreviousItem()
	{
		if (_items.Count < 2)
			return;

		_current.OnHide();
		_current.gameObject.SetActive(false);
		_currentIndex = (_currentIndex + _items.Count - 1) % _items.Count;
		_current = _items[_currentIndex];
		_current.gameObject.SetActive(true);
		_current.OnShow();

		OnItemChanged?.Invoke(_current);
	}

	public void AddItem(HoldableItem item)
	{
		if (_items.Contains(item))
			return;

		_items.Add(item);
		if (_items.Count == 1)
		{
			_currentIndex = 0;
			_current = item;
			_current.transform.parent = transform;
			_current.gameObject.SetActive(true);
			_current.OnShow();
		}
		else
		{
			item.OnHide();
			item.gameObject.SetActive(false);
		}

		item.OnPickup();
		OnItemAdded?.Invoke(item);
	}

	public void RemoveItem(HoldableItem item)
	{
		if (_items.Contains(item))
		{
			item.transform.parent = null;
			_items.Remove(item);
			item.gameObject.SetActive(true);
			item.OnDrop();

			if (_items.Count == 0)
			{
				_current = null;
				_currentIndex = 0;
			}
			else
			{
				if (_items.Count == 0)
					OnItemChanged?.Invoke(null);
				else
					NextItem();
			}

			OnItemRemoved?.Invoke(item);
		}
	}
}
