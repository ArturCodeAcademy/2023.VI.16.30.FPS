using System.Collections.Generic;
using UnityEngine;

public class RemoveBehaviour : MonoBehaviour
{
    [SerializeField] private List<Behaviour> _behaviours = new ();

    public void Remove()
    {
		foreach (var behaviour in _behaviours)
        {
			Destroy(behaviour);
		}
	}
}
