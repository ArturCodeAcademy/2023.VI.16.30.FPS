using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerArea : MonoBehaviour
{
	public bool IsPlayerInArea { get; private set; }

    public UnityEvent EnterArea;
    public UnityEvent ExitArea;

    private void OnTriggerEnter(Collider other)
    {
		if (other.transform == Player.Instance.transform)
		{
			EnterArea.Invoke();
			IsPlayerInArea = true;
		}
	}

    private void OnTriggerExit(Collider other)
    {
		if (other.transform == Player.Instance.transform)
		{
			ExitArea.Invoke();
			IsPlayerInArea = false;
		}
	}
}
