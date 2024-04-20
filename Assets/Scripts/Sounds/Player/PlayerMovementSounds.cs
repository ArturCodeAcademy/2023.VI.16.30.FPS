using System.Collections;
using UnityEngine;
using static PlayerControll;

public class PlayerMovementSounds : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerControll _playerControll;

    [Space(3)]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _walkSounds;
    [SerializeField] private AudioClip[] _runSounds;
    [SerializeField, Min(0.001f)] private float _walkSoundRepeatDelay;
    [SerializeField, Min(0.001f)] private float _runSoundRepeatDelay;

    private Coroutine _soundCoroutine;

	private void OnEnable()
	{
		_playerMovement.OnMove += PlayMoveSound;
        _playerMovement.OnStop += StopMoveSound;
	}

    private void OnDisable()
    {
		_playerMovement.OnMove -= PlayMoveSound;
        _playerMovement.OnStop -= StopMoveSound;
	}

    private void PlayMoveSound()
    {
        _soundCoroutine ??= StartCoroutine(PlaySoundRepeatedly());
	}

    private void StopMoveSound()
    {
		if (_soundCoroutine is not null)
        {
			StopCoroutine(_soundCoroutine);
            _soundCoroutine = null;
        }
	}

    private IEnumerator PlaySoundRepeatedly()
    {
        while (_playerMovement.IsGrounded)
        {
            AudioClip[] sounds = _playerControll.LastMove == HorizontalMoveType.Run ? _runSounds : _walkSounds;
			AudioClip sound = sounds[Random.Range(0, sounds.Length)];
			_audioSource.PlayOneShot(sound);

			float delay = _playerControll.LastMove == HorizontalMoveType.Run ? _runSoundRepeatDelay : _walkSoundRepeatDelay;
            delay += sound.length;
			yield return new WaitForSeconds(delay);
		}

        _soundCoroutine = null;
    }
}
