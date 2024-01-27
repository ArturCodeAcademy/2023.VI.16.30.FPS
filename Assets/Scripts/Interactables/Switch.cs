using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour, IInteractable
{
	[field: SerializeField] public string Name { get; private set; } = "Switch";
    [field: SerializeField] public bool IsOn { get; private set; } = false;
	public string Info => IsOn ? "On" : "Off";

	public UnityEvent TurnedOn;
	public UnityEvent TurnedOff;

	public void Interact()
	{
		IsOn = !IsOn;
		if (IsOn)
			TurnedOn?.Invoke();
		else
			TurnedOff?.Invoke();
	}
}
