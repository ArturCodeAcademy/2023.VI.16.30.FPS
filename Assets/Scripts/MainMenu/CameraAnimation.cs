using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
	[SerializeField] private float _skipSpeed = 3f;

    private Animator _animator;

    private void Awake()
    {
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			_animator.speed = _skipSpeed;
		}
	}
}
