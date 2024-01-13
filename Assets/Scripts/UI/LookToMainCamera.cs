using UnityEngine;

public class LookToMainCamera : MonoBehaviour
{
	private void LateUpdate()
	{
		Vector3 direction = Camera.main.transform.position - transform.position;
		transform.forward = -direction;
	}
}
