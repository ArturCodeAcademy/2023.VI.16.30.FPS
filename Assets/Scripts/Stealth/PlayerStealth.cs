using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class PlayerStealth : MonoBehaviour
{
	public UnityEvent OnGetWorried;
	public UnityEvent OnReact;
	public UnityEvent OnLoseTarget;
	public UnityEvent OnCalmDown;

	[field: SerializeField, Min(0)] public float TimeToReact;
	[field: SerializeField, Min(0)] public float TimeToForgetTarget;
	public float ReactionPercentage => _woriedTimer / TimeToReact;

	[SerializeField] private PlayerScanner _scanner;

	private bool _reacted = false;
	private float _woriedTimer = 0;
	private float _forgetTimer = 0;

	void Awake()
	{
		OnGetWorried ??= new UnityEvent();
		OnReact ??= new UnityEvent();
		OnLoseTarget ??= new UnityEvent();
		OnCalmDown ??= new UnityEvent();
	}

	void Update()
	{
		if (_reacted)
		{

		}
	}

	void CalmDown()
	{
		_woriedTimer = 0;
		_forgetTimer = 0;
		_reacted = false;
		OnCalmDown?.Invoke();
	}
}
