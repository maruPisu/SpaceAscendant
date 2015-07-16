using UnityEngine;
using System.Collections;

public class addComponent : MonoBehaviour {
	
	public int maxSpeedF;
	public int maxSpeedB;
	public int maxRotation;
	public float rechargeMissileTime;
	public float rechargeBulletTime;
	public GameObject ship;
	public ParticleSystem engineParticles;


	private float lastMissileTime;
	private float lastBulletTime;
	private Animator shipAnimator;

	// Use this for initialization
	void Start () {
		lastMissileTime = 0f;
		lastBulletTime = 0f;
		shipAnimator = ship.GetComponent<Animator>();
		engineParticles.enableEmission = false;

	}

	void FixedUpdate(){
	//	transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y+ rot, transform.rotation.z);
	//	rot = rot + 1.5f;
	//	transform.Rotate (new Vector3 (0, 1.5f, 0));
		
		lastMissileTime += Time.deltaTime;
		lastBulletTime += Time.deltaTime;
		if(Input.GetAxis("Vertical") > 0.5f) {
			
			engineParticles.enableEmission = true;
			if(rigidbody.velocity.z < maxSpeedF)
				rigidbody.AddForce((transform.forward) * 10);
			
		}else
			engineParticles.enableEmission = false;
		if(Input.GetAxis("Vertical") < -0.5f) {
			
			if(rigidbody.velocity.z > -maxSpeedB)
				rigidbody.AddForce((transform.forward) * -10);
		}
		
		shipAnimator.SetFloat ("Direction", -Input.GetAxis("Horizontal"));
		if(Input.GetAxis("Horizontal") > 0.5f) {
			if(rigidbody.angularVelocity.y < maxRotation)
				rigidbody.AddTorque(Vector3.up * 1);
			//transform.Rotate (new Vector3 (0, 1.5f, 0));
			
		}
		if(Input.GetAxis("Horizontal") < -0.5f) {
			
			if(rigidbody.angularVelocity.y > -maxRotation)
				rigidbody.AddTorque(Vector3.up * -1);
			//transform.Rotate (new Vector3 (0, -1.5f, 0));

		}
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			
			if(lastMissileTime > rechargeMissileTime){
				shotMissile();
				lastMissileTime = 0;
			}
			
		}

		if(Input.GetKey(KeyCode.LeftShift)) {
			
			if(lastBulletTime > rechargeBulletTime){
				shotBullet();
				lastBulletTime = 0;
			}
			
		}
	}
	
	void shotMissile(){
		
		GameObject missile = GameObject.FindGameObjectWithTag("missile");
		Vector3 newPos = transform.position + new Vector3 (0, 1, 0);
		Quaternion newRot = Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y + 270,transform.eulerAngles.z);
		GameObject part = (GameObject)Instantiate(missile, newPos, newRot);
		//	part.transform.parent = transform;
		
		missileBehavior partBehavior = (missileBehavior) part.GetComponent(typeof(missileBehavior));
		partBehavior.setMissile (true);
		partBehavior.setParent (gameObject);
		partBehavior.launchMissile ();
	}

	void shotBullet(){
		
		GameObject bullet = GameObject.FindGameObjectWithTag("bullet");
		Vector3 newPos = transform.position + new Vector3 (0, 1, 0);
		Quaternion newRot = Quaternion.Euler(transform.eulerAngles.x + 90,transform.eulerAngles.y,transform.eulerAngles.z);
		GameObject part = (GameObject)Instantiate(bullet, newPos, newRot);
		//	part.transform.parent = transform;
		
		missileBehavior partBehavior = (missileBehavior) part.GetComponent(typeof(missileBehavior));
		partBehavior.setMissile (true);
		Rigidbody rigidMissile = part.rigidbody;
		rigidMissile.AddForce((part.transform.up) * 500);
	}
}
