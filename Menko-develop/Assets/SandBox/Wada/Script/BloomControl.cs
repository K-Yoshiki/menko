using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BloomControl : MonoBehaviour {

	public Bloom bloom;
	[SerializeField]
	float speed;
	[SerializeField]
	float blinkSpeed;
	float time = 0;


	// Use this for initialization
	void Start () 
	{
		bloom.bloomIntensity = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		time += speed * Time.deltaTime;
		bloom.bloomIntensity = Mathf.PingPong(time ,blinkSpeed);
	}
}
