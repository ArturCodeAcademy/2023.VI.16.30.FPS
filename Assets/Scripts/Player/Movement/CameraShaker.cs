using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class CameraShaker : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera _virtualCamera;
	[SerializeField] private ShakeSettings _defaultSettings;
	
	private ShakeSettings _selected;
	private CinemachineBasicMultiChannelPerlin _perlin;

	private void Awake()
    {
		_perlin	= _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
	}

	private void Start()
	{
		_perlin.m_AmplitudeGain = _defaultSettings.Amplitude;
		_perlin.m_FrequencyGain = _defaultSettings.Frequency;
	}

	private void Update()
	{
		float stopDurationm = _defaultSettings.InDuration;
		float amplitude = _defaultSettings.Amplitude;
		float frequency = _defaultSettings.Frequency;

		if (_selected is not null)
		{
			stopDurationm = _selected.InDuration;
			amplitude = _selected.Amplitude;
			frequency = _selected.Frequency;

			_selected.Duration -= Time.deltaTime;
			if (_selected.Duration <= 0)
				_selected = null;
		}

		_perlin.m_AmplitudeGain = Mathf.Lerp(_perlin.m_AmplitudeGain, amplitude, Time.deltaTime / stopDurationm);
		_perlin.m_FrequencyGain = Mathf.Lerp(_perlin.m_FrequencyGain, frequency, Time.deltaTime / stopDurationm);
    }
	
	public void SetShake(ShakeSettings shakeSettings)
	{
		_selected = Instantiate(shakeSettings);
	}

	public void SetShakePriority(ShakeSettings shakeSettings)
	{
		if (_selected is null || shakeSettings.Priority > _selected.Priority)
			_selected = Instantiate(shakeSettings);
	}
}
