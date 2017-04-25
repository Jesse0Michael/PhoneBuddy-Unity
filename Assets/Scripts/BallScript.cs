using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	
	public GameObject Shadow;
	public GameObject Ball;

	public bool released;
	public bool returning;
	public bool pickup;
	public float ballLineY;
	private Vector3 furthest;
	private float initialVelocity;
	private float velocity;
	private float oldOldPoint;
	private float oldPoint;
	private float nowPoint;
	private float backTime;
	private float bounceMagnitude;
	private int bounces;

	private const float maxVelocity = 5.0f;
	private const float speed = 0.08f;
	private const float vanishingPoint = 1.2f;
	private Vector3 origin = new Vector3(3, -1.5f, 0);

	public AudioSource thud;

	void Start() {
		thud = GetComponent<AudioSource>();
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

		if(released && !pickup) {
			if (bounces < Mathf.Floor(initialVelocity)) {
				//float lolY = -1.0f / (velocity + Mathf.Pow(ballLineY/2.0f,2.0f)) + 1.2f;
				velocity += speed;
				
				if (ballLineY >= vanishingPoint) {
					ballLineY = vanishingPoint;
				} else {
					ballLineY += speed/10;
				}
				Ball.transform.localPosition = new Vector3(Ball.transform.localPosition.x - speed / 10, 
					ballLineY + (Mathf.Abs(Mathf.Sin(velocity)) * bounceMagnitude), 0);
				Ball.transform.localScale = new Vector3(1 / ((velocity/2) + 1), 1 / ((velocity/2) + 1), 1.0f);


				Shadow.transform.localPosition = new Vector3(Ball.transform.localPosition.x, ballLineY - Ball.transform.localScale.y / 3, 0);
				Shadow.transform.localScale = new Vector3(Ball.transform.localScale.x / (Mathf.Abs(Mathf.Sin(velocity)) + 1),
					Ball.transform.localScale.y / (Mathf.Abs(Mathf.Sin(velocity)) + 1), 1.0f);
					
				oldOldPoint = oldPoint;
				oldPoint = nowPoint;
				nowPoint = Mathf.Abs(Mathf.Sin(velocity));

				if (oldPoint < oldOldPoint && oldPoint < nowPoint) {
					Debug.Log("Bounce: " + bounces);
					Handheld.Vibrate();
					thud.Play();
					bounceMagnitude /= 2.0f;
					bounces++;
				} 
			} else {
				pickup = true;
				Debug.Log("Ball ready for pickup");
			}

		} else if(returning) {
			if (Controller.myDog.returnHome) {
				if(!Controller.myDog.dogAnim.GetCurrentAnimatorStateInfo(0).IsTag("dogSheet_runAway")) {
					Ball.GetComponent<SpriteRenderer> ().sortingOrder = 10;
				}
				Ball.transform.localPosition = new Vector3(Controller.myDog.transform.localPosition.x, 
					Controller.myDog.transform.localPosition.y - (0.08f * Controller.myDog.transform.localScale.y), 0);
				Ball.transform.localScale = Controller.myDog.transform.localScale / 3;
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
		float calcVelocity = (float)((Mathf.Sqrt(
			Mathf.Pow((Ball.transform.localPosition.x - furthest.x), 2) + Mathf.Pow((Ball.transform.localPosition.y - furthest.y), 2)))/(backTime));
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
		Controller.myDog.Bark();
	}
	
	public void Fetched() {
		Debug.Log("Ball fetched");
		released = false;
		returning = true;
		Shadow.GetComponent<SpriteRenderer> ().enabled = false;
		Controller.myDog.ReturnHome();
	}
}
