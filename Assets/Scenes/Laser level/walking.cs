using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Death")
		{
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update()
    {
		transform.position = new Vector3(transform.position.x + Time.deltaTime * 5, transform.position.y, transform.position.z);
    }
}
