using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
	private void Start()
	{
		PauseSystem.Paused += ShowPauseScreen;
		PauseSystem.Unpaused += HidePauseScreen;
		HidePauseScreen();
	}

	private void OnDestroy()
	{
		PauseSystem.Paused -= ShowPauseScreen;
		PauseSystem.Unpaused -= HidePauseScreen;
	}

	private void ShowPauseScreen()
	{
		gameObject.SetActive(true);
	}

	private void HidePauseScreen()
	{
		gameObject.SetActive(false);
	}

	public void Resume()
	{
		PauseSystem.Unpause();
	}

	public void Restart()
	{
		PauseSystem.Unpause();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Quit()
	{
		PauseSystem.Unpause();
		SceneManager.LoadScene(0);
	}
}
