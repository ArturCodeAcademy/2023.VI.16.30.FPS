using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSteathUI : MonoBehaviour
{
    [SerializeField] private PlayerStealth _stealth;

    [Header("Stealth meter")]
    [SerializeField] private GameObject _stealthMeter;
    [SerializeField] private Image _stealthMeterFill;
    [SerializeField] Color _woriedColor = Color.yellow;
    [SerializeField] Color _reactedColor = Color.red;

    [Header("Forget meter")]
    [SerializeField] private GameObject _forgetMeter;
    [SerializeField] private Image _forgetMeterFill;

	private void Start()
	{
		OnCalmDown();
	}

    private void OnEnable()
    {
		_stealth.OnGetWorried.AddListener(OnGetWorried);
		_stealth.OnReact.AddListener(OnReact);
		_stealth.OnSawTarget.AddListener(OnSawTarget);
		_stealth.OnLoseTarget.AddListener(OnLoseTarget);
		_stealth.OnCalmDown.AddListener(OnCalmDown);
	}

	private void OnDisable()
	{
		_stealth.OnGetWorried.RemoveListener(OnGetWorried);
		_stealth.OnReact.RemoveListener(OnReact);
		_stealth.OnSawTarget.RemoveListener(OnSawTarget);
		_stealth.OnLoseTarget.RemoveListener(OnLoseTarget);
		_stealth.OnCalmDown.RemoveListener(OnCalmDown);
	}

	private void OnSawTarget()
	{
		if (_stealth.Reacted)
			_forgetMeter.SetActive(false);
	}

	private void OnGetWorried()
	{
		_stealthMeter.SetActive(true);
		_stealthMeterFill.color = _woriedColor;
	}

	private void OnReact()
	{
		_stealthMeterFill.color = _reactedColor;
	}

	private void OnLoseTarget()
	{
		if (_stealth.Reacted)
			_forgetMeter.SetActive(true);
	}

	private void OnCalmDown()
	{
		_stealthMeter.SetActive(false);
		_forgetMeter.SetActive(false);
	}

	private void Update()
	{
		if (_stealthMeter.activeSelf || _forgetMeter.activeSelf)
		{
			_stealthMeterFill.fillAmount = _stealth.ReactionPercentage;
			_forgetMeterFill.fillAmount = 1 - _stealth.ForgetPercentage;
		}
	}
}
