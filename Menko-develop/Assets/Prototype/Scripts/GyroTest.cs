using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GyroTest : MonoBehaviour
{
	public Text show;
	public List<Vector3> accels;
	Gyroscope gyro;

	void Start()
	{
		this.gyro = Input.gyro;
		this.gyro.enabled = true;
	}

	void Update()
	{
		var accel = this.gyro.userAcceleration;

		if (accel.z >= 1.2f)
		{
			accels.Add(accel);
		}

		show.text = string.Format(
			"x:{0}" + "\n" + "y:{1}" + "\n" + "z:{2}",
			accel.x.ToString("0.0"),
			accel.y.ToString("0.0"),
			accel.z.ToString("0.0")
		);
	}
}