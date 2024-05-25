using UnityEngine;

public class PlayerEndHealth : MonoBehaviour
{
	private void Start()
	{
		Player.Instance.Health.OnValueMin += PauseSystem.Pause;
	}

	private void OnDestroy()
	{
		Player.Instance.Health.OnValueMin -= PauseSystem.Pause;
	}
}
