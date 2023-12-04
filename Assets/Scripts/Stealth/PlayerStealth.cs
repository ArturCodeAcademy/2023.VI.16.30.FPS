using UnityEngine;
using UnityEngine.Events;

public class PlayerStealth : MonoBehaviour
{
	public UnityEvent OnGetWorried;
	public UnityEvent OnReact;
	public UnityEvent OnSawTarget;
	public UnityEvent OnLoseTarget;
	public UnityEvent OnCalmDown;

	[field: SerializeField, Min(0)] public float TimeToReact;
	[field: SerializeField, Min(0)] public float TimeToForgetTarget;

	public float ReactionPercentage => Mathf.Clamp01(_woriedTimer / TimeToReact);
	public float ForgetPercentage => Mathf.Clamp01(_forgetTimer / TimeToForgetTarget);
	public bool Reacted { get; private set; } = false;

	[SerializeField] private PlayerScanner _scanner;

	private float _woriedTimer = 0;
	private float _forgetTimer = 0;

	void Awake()
	{
		OnGetWorried ??= new UnityEvent();
		OnReact ??= new UnityEvent();
		OnSawTarget ??= new UnityEvent();
		OnLoseTarget ??= new UnityEvent();
		OnCalmDown ??= new UnityEvent();
	}

	private void OnEnable()
	{
		_scanner.OnScannerViewEnter.AddListener(OnScannerViewEnter);
		_scanner.OnScannerViewExit.AddListener(OnScannerViewExit);
	}

	private void OnDisable()
	{
		_scanner.OnScannerViewEnter.RemoveListener(OnScannerViewEnter);
		_scanner.OnScannerViewExit.RemoveListener(OnScannerViewExit);
	}

	void Update()
	{
		if (Reacted)
		{
			if (_scanner.IsVisible)
			{
				_forgetTimer = 0;
			}
			else
			{
				_forgetTimer += Time.deltaTime;
				if (_forgetTimer >= TimeToForgetTarget)
				{
					CalmDown();
				}
			}
		}
		else
		{
			if (_scanner.IsVisible)
			{
				_woriedTimer += Time.deltaTime;
				if (_woriedTimer >= TimeToReact)
				{
					Reacted = true;
					OnReact?.Invoke();
				}
			}
			else if (_woriedTimer > 0)
			{
				_woriedTimer = Mathf.Max(_woriedTimer -= Time.deltaTime, 0);
				if (_woriedTimer <= 0)
				{
					CalmDown();
				}
			}
		}
	}

	private void OnScannerViewEnter()
	{
		if (!Reacted)
			OnGetWorried?.Invoke();
		OnSawTarget?.Invoke();
	}

	private void OnScannerViewExit()
	{
		if (Reacted)
			OnLoseTarget?.Invoke();
	}

	void CalmDown()
	{
		_woriedTimer = 0;
		_forgetTimer = 0;
		Reacted = false;
		OnCalmDown?.Invoke();
	}
}
