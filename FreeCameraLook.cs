using UnityEngine;
using UnityEditor;

public class FreeCameraLook :Pivot {

	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float turnSpeed = 1.5f;
	[SerializeField] private float turnsmoothing = .1f;
	[SerializeField] private float tiltMax =75f;
	[SerializeField] private float tiltMin =45f;
	[SerializeField] private bool lockCursor = false;




	private float lookAngle;
	private float tiltAngle;

	private const float LookDistance =100f;

	private float SmoothX =0;
	private float SmoothY =0;
	private float smoothXvelocity =0;
	private float smoothYvelocity =0;

	// Use this for initialization
      protected override void Awake () 
	{
		base.Awake();
		Screen.lockCursor =lockCursor;

		cam = GetComponentInChildren<Camera>().transform;

		pivot = cam.parent;

	     

	}
	// Update is called once per frame
protected override void Update () 
	{
		base.Update();
		HandleRotationMovement();
		if(lockCursor && Input.GetMouseButtonUp(0))
		{
			Screen.lockCursor = lockCursor;
	Debug.ClearDeveloperConsole();
			
		}

	}
	void OnDisable()
	{

		Screen.lockCursor =false;
	}


	protected override void Follow(float deltaTime)
	{
		transform.position = Vector3.Lerp(transform.position,target.position,deltaTime * moveSpeed);


	}
	void HandleRotationMovement()
	{

		float x = Input.GetAxis("Mouse X");
		float y = Input.GetAxis("Mouse Y");

		if(turnsmoothing > 0)
		{

			SmoothX = Mathf.SmoothDamp(SmoothX,x,ref smoothXvelocity, turnsmoothing);
			SmoothY = Mathf.SmoothDamp(SmoothY,y,ref smoothYvelocity, turnsmoothing);
		}
		else
		{

			SmoothX = x;
			SmoothY = y;
		}
		lookAngle += SmoothX*turnSpeed;
		transform.rotation = Quaternion.Euler(0f,lookAngle,0);
		tiltAngle -= SmoothY * turnSpeed;
		tiltAngle = Mathf.Clamp(tiltAngle, -tiltMin,tiltMax);

		pivot.localRotation = Quaternion.Euler(tiltAngle,0,0);
	}
}
