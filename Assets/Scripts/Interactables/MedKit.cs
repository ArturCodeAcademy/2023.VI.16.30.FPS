using UnityEngine;
using UnityEngine.Events;

public class MedKit : MonoBehaviour, IInteractable
{
	public string Name => "Med kit";

	public string Info => $"Health amount: {HealthAmount:0.}";

	[field: SerializeField] public float HealthAmount { get; private set; }

	[field: SerializeField] public UnityEvent OnInteract;
	
	public void Interact()
	{
		Health health = Player.Instance.GetComponent<Health>();
		float amount = health.Heal(HealthAmount);
		HealthAmount -= amount;

		OnInteract?.Invoke();

		if (HealthAmount <= 0)
		{
			Destroy(gameObject);
		}
	}
}
