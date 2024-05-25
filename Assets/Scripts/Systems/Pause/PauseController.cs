using UnityEngine;

public class PauseController : MonoBehaviour
{
	private void Start()
	{
		PauseSystem.Unpause();
	}

	private void Update()
	{
		if (Player.Instance.Health.CurrentValue <= 0)
			return;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (PauseSystem.IsPaused)
				PauseSystem.Unpause();
			else
				PauseSystem.Pause();
		}
	}
}
