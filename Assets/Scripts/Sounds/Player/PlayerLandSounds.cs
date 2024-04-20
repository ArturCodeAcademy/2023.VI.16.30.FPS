using UnityEngine;

public class PlayerLandSounds : MonoBehaviour
{
	[SerializeField] private PlayerMovement _playerMovement;

	[Space(3)]
	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private AudioClip[] _softLandSounds;
	[SerializeField] private AudioClip[] _hardLandSounds;
	[SerializeField] private float _softLandVelocityThreshold = -5;

	
	private void OnEnable()
	{
		_playerMovement.OnGetGrounded += PlayLandSound;
	}

	private void OnDisable()
	{
		_playerMovement.OnGetGrounded -= PlayLandSound;
	}

	private void PlayLandSound(float verticalVelocity)
	{
		AudioClip[] sounds = verticalVelocity > _softLandVelocityThreshold ? _softLandSounds : _hardLandSounds;
		AudioClip sound = sounds[Random.Range(0, sounds.Length)];
		_audioSource.PlayOneShot(sound);
	}
}
