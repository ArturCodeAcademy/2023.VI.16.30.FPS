using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScanner : MonoBehaviour
{
	public UnityEvent OnScannerViewEnter;
	public UnityEvent OnScannerViewExit;

	[SerializeField, Min(0)] private float _viewDistance;
	[SerializeField, Range(0, 360)] private float _viewAngle;
	[SerializeField] private Vector3 _centerOffset;

	public bool IsVisible = false;

	void FixedUpdate()
	{
		CheckPlayer();
	}

	private void CheckPlayer()
	{
		Player player = Player.Instance;
		if (IsVisible && Vector3.Distance(player.Center, transform.position) > _viewDistance)
		{
			OnScannerViewExit?.Invoke();
			IsVisible = false;
			return;
		}

		float angle = Vector3.Angle(transform.forward, player.Center - transform.position);

		if (angle <= _viewAngle / 2)
		{
			Vector3 direction = player.Center - transform.position;
			if (Physics.Raycast(transform.position, direction, out RaycastHit hit, _viewDistance))
			{
				Debug.DrawRay(transform.position, direction, Color.red, 1);
				if (!IsVisible && hit.transform == player.transform)
				{
					IsVisible = true;
					OnScannerViewEnter?.Invoke();
				}
				else if (IsVisible && hit.transform != player.transform)
				{
					IsVisible = false;
					OnScannerViewExit?.Invoke();
				}
			}
		}
		else if (IsVisible && angle > _viewAngle / 2)
		{
			IsVisible = false;
			OnScannerViewExit?.Invoke();
		}
	}

#if UNITY_EDITOR

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, _viewDistance);

		Gizmos.color = Color.blue;
		float yRotation = Mathf.Deg2Rad * transform.rotation.eulerAngles.y;
		float leftRad = Mathf.Deg2Rad * _viewAngle / 2 + yRotation;
		float rightRad = -Mathf.Deg2Rad * _viewAngle / 2 + yRotation;
		Vector3 leftAngle = new Vector3(Mathf.Sin(leftRad), 0, Mathf.Cos(leftRad)).normalized;
		Vector3 rightAngle = new Vector3(Mathf.Sin(rightRad), 0, Mathf.Cos(rightRad)).normalized;
		leftAngle *= _viewDistance;
		rightAngle *= _viewDistance;
		Gizmos.DrawLine(transform.position, transform.position + leftAngle);
		Gizmos.DrawLine(transform.position, transform.position + rightAngle);
	}

#endif
}