using UnityEngine;

public class PlayerHitAudio : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private AudioSource _audioSource;

    [Space(3)]
    [SerializeField] private AudioClip[] _softHitSounds;
    [SerializeField] private AudioClip[] _mediumHitSounds;
    [SerializeField] private AudioClip[] _hardHitSounds;

    [Space(3)]
    [SerializeField, Min(0)] private float _softHitTrashold;
    [SerializeField, Min(0)] private float _mediumHitTrashold;

	private float _previousHealth;

	private void Start()
	{
		_previousHealth = _playerHealth.CurrentValue;
	}

	private void OnEnable()
	{
		_playerHealth.OnValueChanged += PlayHitSound;
	}

	private void OnDisable()
    {
		_playerHealth.OnValueChanged -= PlayHitSound;
	}

	private void PlayHitSound(float current, float _)
	{
		float damage = _previousHealth - current;
		_previousHealth = current;

		if (damage <= 0)
			return;

		if (_audioSource.isPlaying)
			return;

		AudioClip[] sounds = _hardHitSounds;
		if (damage < _mediumHitTrashold)
			sounds = _mediumHitSounds;
		else if (damage < _softHitTrashold)
			sounds = _softHitSounds;

		_audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
	}
}
