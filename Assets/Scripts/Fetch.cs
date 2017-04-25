using UnityEngine;
using System.Collections;

public class FetchActivity {

	private Dog dog;
	private GameObject ball;
	private const float minScale = 0.3f;

	public FetchActivity(Dog dog, GameObject ball) {
		Init();
		
		this.dog = dog;
		this.ball = ball;
	}

	public void Run () {
		if(ball.GetComponent<BallScript>().released && !dog.returnHome) {
			float ballLineY = ball.GetComponent<BallScript>().ballLineY;
			Vector3 target = new Vector3(ball.transform.localPosition.x, ballLineY, 0);
			Vector3 targetScale = new Vector3(Mathf.Max(minScale, ball.transform.localScale.x), 
				Mathf.Max(minScale, ball.transform.localScale.y), 1);
			dog.RunTowards(target, targetScale, dog.returnSpeedX / (1.5f + ballLineY), dog.returnSpeedX / (6.5f - ballLineY));
			if(ball.GetComponent<BallScript>().pickup) {
				if(dog.transform.localPosition.x == ball.transform.localPosition.x) {
					ball.GetComponent<BallScript>().Fetched();
				}
			}
		}
	}

	public void Init() {
		
	}
}
