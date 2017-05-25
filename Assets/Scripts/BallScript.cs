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
	private Vector3 origin;

	private const float maxVelocity = 5.0f;
	private const float speed = 0.08f;
	private const float vanishingPoint = 1.2f;

	public AudioSource thud;

	void Start() {
		thud = GetComponent<AudioSource>();
		origin = GameObject.Find("Ball").transform.position;
	}

	// Use this for initialization
	public void Reset () {
		returning = false;
		released = false;
		pickup = false;
		Ball.GetComponent<SpriteRenderer> ().sortingOrder = 10;
		Ball.GetComponent<SpriteRenderer> ().enabled = true;
		Shadow.GetComponent<SpriteRenderer> ().enabled = false;
		furthest = origin;
		initialVelocity = 0.0f;
		velocity = 0.0f;
		oldOldPoint = 0.0f;
		oldPoint = 0.0f;
		nowPoint = 0.0f;
		backTime = 0.0f;
		bounces = 0;
		Ball.transform.position = origin;
		Ball.transform.localScale = new Vector3(0.6f, 0.6f, 1.0f);
		Shadow.transform.localPosition = origin;
		Shadow.transform.localScale = Vector3.one;
	}
	
	// Update is called once per frame
	void Update () {
		backTime += Time.deltaTime;
		if(Controller.myActivity == Activity.dogFetch) {
			if(released && !pickup) {
				if (bounces < Mathf.Floor(initialVelocity)) {
					//float lolY = -1.0f / (velocity + Mathf.Pow(ballLineY/2.0f,2.0f)) + 1.2f;
					velocity += speed;
					
					if (ballLineY >= vanishingPoint) {
						ballLineY = vanishingPoint;
					} else {
						ballLineY += speed/10;
					}
					Ball.transform.position = new Vector3(Ball.transform.position.x - speed / 10, 
						ballLineY + (Mathf.Abs(Mathf.Sin(velocity)) * bounceMagnitude), 0);
					Ball.transform.localScale = new Vector3(0.6f / ((velocity) + 1), 0.6f / ((velocity) + 1), 1.0f);


					Shadow.transform.localPosition = new Vector3(Ball.transform.position.x, ballLineY - Ball.transform.localScale.y / 2, 0);
					Shadow.transform.localScale = new Vector3((Ball.transform.localScale.x * 2) / (Mathf.Abs(Mathf.Sin(velocity)) + 1),
						(Ball.transform.localScale.y * 2) / (Mathf.Abs(Mathf.Sin(velocity)) + 1), 1.0f);
						
					oldOldPoint = oldPoint;
					oldPoint = nowPoint;
					nowPoint = Mathf.Abs(Mathf.Sin(velocity));

					if (oldPoint < oldOldPoint && oldPoint < nowPoint) {
						Debug.Log("Bounce: " + bounces);
						#if UNITY_ANDROID || UNITY_IPHONE
							Handheld.Vibrate();
						#endif
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
					if(Controller.myDog.dogAnim.GetCurrentAnimatorStateInfo(0).IsName("dogSheet_runAway")) {
						Ball.GetComponent<SpriteRenderer> ().enabled = false;
					} else {
						Ball.GetComponent<SpriteRenderer> ().enabled = true;
					}
					Ball.transform.position = new Vector3(Controller.myDog.transform.localPosition.x, 
						Controller.myDog.transform.localPosition.y - (0.08f * Controller.myDog.transform.localScale.y), 0);
					Ball.transform.localScale = Controller.myDog.transform.localScale / 5;
				} else {
					// Controller.myDog.statEntertainment += 0.4f;
					Reset();
				}
			}
		}
	}
	
	void OnMouseDown() {
		if(!released && !returning && Controller.myActivity == Activity.dogFetch) {
			Debug.Log("Ball down");
			backTime = 0.0f;
		}
	}
	
	void OnMouseDrag() {
		if(!released && !returning && Controller.myActivity == Activity.dogFetch) {
			Debug.Log("Ball drag");
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Shadow.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
			Ball.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
			if(Ball.transform.position.y < furthest.y) {
				furthest = Ball.transform.position;
				backTime = 0.0f;
			}
		}
	}
	
	void OnMouseUp() {
		if(!released && !returning && Controller.myActivity == Activity.dogFetch) {
			Debug.Log("Ball up");
			Shadow.GetComponent<SpriteRenderer> ().enabled = false;
			Shadow.transform.localPosition = Vector3.zero;
			float calcVelocity = (float)((Mathf.Sqrt(
				Mathf.Pow((Ball.transform.position.x - furthest.x), 2) + Mathf.Pow((Ball.transform.position.y - furthest.y), 2)))/(backTime));
			initialVelocity = Mathf.Max(1.0f, Mathf.Min(calcVelocity, maxVelocity));
			velocity = 0.0f;
			ballLineY = Ball.transform.position.y;
			bounceMagnitude = initialVelocity / 2;
			Debug.Log("Ball calculated velocity: " + calcVelocity + " used velocity: " + initialVelocity);
			release();
		}
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
		Ball.GetComponent<SpriteRenderer> ().sortingOrder = 10;
		Controller.myDog.returnSpeedS = .02f;
		Controller.myDog.ReturnHome();
	}
}
