using UnityEngine;
using System.Collections;

public class missileBehavior : MonoBehaviour {

	public float _timeLife;
	public bool _isRealMissile;
	public float speed;
	public float acceleration;

	private float currentSpeed = 0 ;
	private Rigidbody rigidMissile;
	private Rigidbody rigidParent;
	private Transform transformParent;
	private bool isLounched = false;
	private GameObject _parent;
	// Use this for initialization
	void Start () {
		_isRealMissile = false;
		_parent = gameObject;
		_timeLife = 0f;
		rigidMissile = rigidbody;
	}

	public void setParent(GameObject parent)
	{
		_parent = parent;
		rigidParent = _parent.rigidbody;
		transformParent = _parent.transform;
	}

	public void setMissile(bool isMissile){
		_isRealMissile = isMissile;
	}

	public void launchMissile()
	{
		isLounched = true;
		Vector3 inertialForce = new Vector3 (rigidParent.velocity.x, 0f, 0f);// rigidParent.velocity.z);
		Debug.Log (inertialForce.x + ", " + inertialForce.y + ", " + inertialForce.z);
	//	rigidMissile.AddForce (inertialForce);
//		rigidbody.velocity.Set (rigidParent.velocity.x, 0f, rigidParent.velocity.z);
	}


	// Update is called once per frame
	void Update () {
		_timeLife += Time.deltaTime;

		if (isLounched && currentSpeed < speed)
		{
			currentSpeed += acceleration;
			rigidMissile.AddForce ((transform.right) * acceleration);
		}
		//	Debug.Log ("time " + rigidbody.velocity.x + ", " +rigidbody.velocity.z);
	//	Debug.Log (_parent.position.x + _parent.position.y + _parent.position.z);

		if(_timeLife > 5 && (Mathf.Abs(rigidbody.velocity.x) + Mathf.Abs(rigidbody.velocity.z) > 0))
		   Destroy(gameObject);
	}
}
