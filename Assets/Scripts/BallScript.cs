using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	
	public GameObject Shadow;
	public GameObject Ball;

	public bool released;
	public bool returning;
	public bool pickup;
	private Vector3 furthest;
	private float initialVelocity;
	private float velocity;
	private float oldOldPoint;
	private float oldPoint;
	private float nowPoint;
	private float backTime;
	private float bounceMagnitude;
	private float ballLineY;
	private int bounces;

	private const float maxVelocity = 5.0f;
	private const float velocityReduceSpeed = 100.0f;
	private Vector3 vanishingPoint = new Vector3(0, 1, 0);
	private Vector3 origin = new Vector3(3, -1.5f, 0);

	void Start() {
		
	}

	// Use this for initialization
	public void Reset () {
		returning = false;
		released = false;
		pickup = false;
		Ball.GetComponent<SpriteRenderer> ().sortingOrder = 10;
		Shadow.GetComponent<SpriteRenderer> ().enabled = false;
		furthest = new Vector3(3, -1.5f, 0);
		initialVelocity = 0.0f;
		velocity = 0.0f;
		oldOldPoint = 0.0f;
		oldPoint = 0.0f;
		nowPoint = 0.0f;
		backTime = 0.0f;
		bounces = 0;
		Ball.transform.localPosition = furthest;
		Ball.transform.localScale = Vector3.one;
		Shadow.transform.localPosition = origin;
		Shadow.transform.localScale = Vector3.one;
	}
	
	// Update is called once per frame
	void Update () {
		backTime += Time.deltaTime;

		if(released && ! pickup) {
			if (bounces < Mathf.Floor(initialVelocity)) {
				// ballRot -= Mathf.PI / (8 * initialVelocity);
				//https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
				float speed = .08f;//.01f * initialVelocity;
				if (ballLineY >= 1.2f) {
					ballLineY = 1.2f;
				} else {
					ballLineY += speed/10;
				}
				//float lolY = -1.0f / (velocity + Mathf.Pow(ballLineY/2.0f,2.0f)) + 1.2f;
				velocity += speed;
				
				Ball.transform.localPosition = new Vector3(Ball.transform.localPosition.x - speed / 10, ballLineY + (Mathf.Abs(Mathf.Sin(velocity)) * bounceMagnitude), 0);
				// Ball.transform.localRotation = Quaternion.RotateTowards(Ball.transform.localRotation, new Quaternion(0,0,speed*100, 0), 40.0f * speed);
				Ball.transform.localScale = new Vector3(1 / ((velocity/2) + 1), 1 / ((velocity/2) + 1), 1.0f);


				Shadow.transform.localPosition = new Vector3(Ball.transform.localPosition.x, ballLineY - Ball.transform.localScale.y / 3, 0);
				Shadow.transform.localScale = new Vector3(Ball.transform.localScale.x / (Mathf.Abs(Mathf.Sin(velocity)) + 1),
					Ball.transform.localScale.y / (Mathf.Abs(Mathf.Sin(velocity)) + 1), 1.0f);
					
				oldOldPoint = oldPoint;
				oldPoint = nowPoint;
				nowPoint = Mathf.Abs(Mathf.Sin(velocity));

				if (oldPoint < oldOldPoint && oldPoint < nowPoint) {
					Debug.Log("Bounce: " + bounces);
					//Controller.vibrate();
					//thud(1.0f - (float)(bounceCount/10));
					bounceMagnitude /= 2.0f;
					bounces++;
				} 
			} else {
				pickup = true;
			}

		} else if(returning) {
			if (Controller.myDog.returnHome) {
				Ball.transform.localPosition = Controller.myDog.me.transform.localPosition;
				Ball.transform.localScale = Controller.myDog.me.transform.localScale;
			} else {
				// Controller.myDog.statEntertainment += 0.4f;
				Reset();
			}
		}
	}
	
	void OnMouseDown() {
		if(!released && !returning) {
			Debug.Log("Ball down");
			backTime = 0.0f;
		}
	}
	
	void OnMouseDrag() {
		if(!released && !returning) {
			Debug.Log("Ball drag");
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Shadow.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
			Ball.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
			if(Ball.transform.localPosition.y < furthest.y) {
				furthest = Ball.transform.localPosition;
				backTime = 0.0f;
			}
		}
	}
	
	void OnMouseUp() {
		Debug.Log("Ball up");
		Shadow.GetComponent<SpriteRenderer> ().enabled = false;
		Shadow.transform.localPosition = Vector3.zero;
		float calcVelocity = (float)((Mathf.Sqrt(Mathf.Pow((Ball.transform.localPosition.x - furthest.x), 2) + Mathf.Pow((Ball.transform.localPosition.y - furthest.y), 2)))/(backTime));
		initialVelocity = Mathf.Max(1.0f, Mathf.Min(calcVelocity, maxVelocity));
		velocity = 0.0f;
		ballLineY = Ball.transform.localPosition.y;
		bounceMagnitude = initialVelocity / 2;
		Debug.Log("Ball calculated velocity: " + calcVelocity + " used velocity: " + initialVelocity);
		release();
	}

	private void release() {
		released = true;
		Ball.GetComponent<SpriteRenderer> ().sortingOrder = 0;
		Shadow.GetComponent<SpriteRenderer> ().enabled = true;
	}
	
	public void Fetched() {
		released = false;
		returning = true;
		Ball.GetComponent<SpriteRenderer> ().sortingOrder = 10;
		Shadow.GetComponent<SpriteRenderer> ().enabled = false;
		Controller.myDog.ReturnHome();
	}
}
