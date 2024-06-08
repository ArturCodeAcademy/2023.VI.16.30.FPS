using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIButton = UnityEngine.UI.Button;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private BuildScene[] _scenes;
    [SerializeField] private UIButton _sceneButtonPrefab;

    private void Start()
    {
		foreach (var scene in _scenes)
        {
			var button = Instantiate(_sceneButtonPrefab, transform);
			button.GetComponentInChildren<TMP_Text>().text = scene.Name;
            int buildIndex = scene.BuildIndex;
			button.onClick.AddListener(() => LoadScene(buildIndex));
			button.gameObject.SetActive(true);
		}
	}

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void Quit()
    {
		Application.Quit();
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}

	[Serializable]
	public struct BuildScene
	{
		public string Name;
		public int BuildIndex;
	}
}
