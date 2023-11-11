using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField, Range(2, 10)] private float _interactionDistance = 3.0f;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _infoText;

	private IInteractable _interactable;
	private Transform _camera;

	private void Start()
	{
		_camera = Camera.main.transform;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
			_interactable?.Interact();

		RaycastHit hit = Physics.RaycastAll(_camera.position, _camera.forward, _interactionDistance)
			.Where(h => h.transform != Player.Instance.transform).OrderBy(h => h.distance).FirstOrDefault();

		if (hit.transform is null)
		{
			_interactable = null;
			HideInfo();
			return;
		}

		_interactable = hit.transform.GetComponent<IInteractable>();
		if (_interactable is null)
			HideInfo();
		else
			ShowInfo();
	}

	private void HideInfo()
	{
		_nameText.text = string.Empty;
		_infoText.text = string.Empty;
	}

	private void ShowInfo()
	{
		_nameText.text = _interactable.Name;
		_infoText.text = _interactable.Info;
	}
}
