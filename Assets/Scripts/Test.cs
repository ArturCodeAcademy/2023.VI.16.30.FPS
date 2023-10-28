using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Stamina _stamina;
    [SerializeField] private Health _health;

    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        { 
			_stamina.UseStamina(10 * Time.deltaTime);
		}
		
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _health.Hit(10 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
			_health.Heal(10 * Time.deltaTime);
		}
    }
}
