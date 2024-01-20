using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IInteractable
{
	[field: SerializeField] public string Name { get; private set; } = "Button";

	[field: SerializeField] public string Info { get; private set; } = "Press E to interact";

	public UnityEvent Interacted;

	public void Interact()
	{
		Interacted?.Invoke();
	}
}
