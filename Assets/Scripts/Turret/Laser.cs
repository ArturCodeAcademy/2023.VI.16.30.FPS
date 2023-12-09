using UnityEngine;

public class Laser : MonoBehaviour
{
	[field:SerializeField] public bool Enabled { get; set; } = true;

	[SerializeField] private LineRenderer _lineRenderer;
	[SerializeField, Min(0)] private float _maxDistance = 500;

	private void Start()
	{
		if (Enabled)
			TurnOn();
		else
			TurnOff();
	}

	public void TurnOn()
	{
		_lineRenderer.enabled = true;
	}

	public void TurnOff()
	{
		_lineRenderer.enabled = false;
	}

	private void Update()
	{
		if (!_lineRenderer.enabled)
			return;

		if (Physics.Raycast(_lineRenderer.transform.position, _lineRenderer.transform.forward, out RaycastHit hit))
		{
			_lineRenderer.SetPosition(1, Vector3.forward * hit.distance);
		}
		else
		{
			_lineRenderer.SetPosition(1, new Vector3(0, 0, _maxDistance));
		}
	}
}