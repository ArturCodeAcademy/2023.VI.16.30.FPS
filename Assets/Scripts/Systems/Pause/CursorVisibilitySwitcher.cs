using UnityEngine;

public class CursorVisibilitySwitcher : MonoBehaviour
{
	private void Start()
	{
		if (PauseSystem.IsPaused)
			ShowCursor();
		else
			HideCursor();
	}

	private void OnEnable()
	{
		PauseSystem.Paused += ShowCursor;
		PauseSystem.Unpaused += HideCursor;
	}

	private void OnDisable()
	{
		PauseSystem.Paused -= ShowCursor;
		PauseSystem.Unpaused -= HideCursor;
	}

	private void HideCursor()
	{
		Cursor.visible = false;
	}

	private void ShowCursor()
	{
		Cursor.visible = true;
	}
}
