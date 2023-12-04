using UnityEngine;

public class StealthCircleUI : MonoBehaviour
{
	[SerializeField] private Transform _scanner;
    [SerializeField] private PlayerStealth _stealth;
    [SerializeField] private RectTransform _stealthCircle;
    [SerializeField] private RectTransform _stealthUI;

    private void Start()
    {
		OnCalmDown();
	}

	private void OnEnable()
	{
		_stealth.OnGetWorried.AddListener(OnGetWorried);
		_stealth.OnCalmDown.AddListener(OnCalmDown);
	}

	private void OnDisable()
	{
		_stealth.OnGetWorried.RemoveListener(OnGetWorried);
		_stealth.OnCalmDown.RemoveListener(OnCalmDown);
	}

	private void Update()
	{
		if (_stealthCircle.gameObject.activeSelf)
		{
			float rotation = GetAngeFromPlayerForwardToScanner();
			_stealthCircle.rotation = Quaternion.Euler(0, 0, rotation);
			_stealthUI.localRotation = Quaternion.Euler(0, 0, -rotation);
		}
	}

	private void OnGetWorried()
	{
		_stealthCircle.gameObject.SetActive(true);
	}

	void OnCalmDown()
    {
		_stealthCircle.gameObject.SetActive(false);
	}

    private float GetAngeFromPlayerForwardToScanner()
    {
		Vector3 playerXZ = Player.Instance.transform.forward;
		Vector3 targetXZ = _scanner.position - Player.Instance.transform.position;
		playerXZ.y = targetXZ.y = 0;

		return Vector3.SignedAngle(playerXZ, targetXZ, Vector3.down);
	}
}
