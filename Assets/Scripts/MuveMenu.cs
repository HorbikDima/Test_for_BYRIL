using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuveMenu : MonoBehaviour
{
	float speed = -0.1f;
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(new Vector3(0f, speed, 0f));
		if (transform.position.y < -11f)
		{
			transform.position = new Vector3(0f, 12f, 0f);
		}
	}
}
