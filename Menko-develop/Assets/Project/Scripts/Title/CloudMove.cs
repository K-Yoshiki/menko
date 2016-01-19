using UnityEngine;
using System.Collections;

public class CloudMove : MonoBehaviour {

    public float movespd = -1.0f;

    void Update()
    {
        Vector3 vector3 = new Vector3(transform.position.x + movespd, transform.position.y, transform.position.z);
        transform.position = vector3;

    }

	
}
