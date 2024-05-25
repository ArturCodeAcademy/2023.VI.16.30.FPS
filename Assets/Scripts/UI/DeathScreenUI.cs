using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenUI : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;

	private void Start()
	{
		_playerHealth.OnValueMin += ShowDeathScreen;
		gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		_playerHealth.OnValueMin -= ShowDeathScreen;
	}

	private void ShowDeathScreen()
	{
		gameObject.SetActive(true);
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Back()
	{
		SceneManager.LoadScene(0);
	}
}
