using UnityEngine;
using System.Collections;

public class FetchActivity {

	private Dog dog;
	private GameObject ball;

	public FetchActivity(Dog dog, GameObject ball) {
		Init();
		
		this.dog = dog;
		this.ball = ball;
	}

	public void Run () {
		if(ball.GetComponent<BallScript>().released && !dog.returnHome) {
			dog.RunTowards(ball.transform.localPosition, ball.transform.localScale);
			if(ball.GetComponent<BallScript>().pickup) {
				if(dog.me.transform.localPosition == ball.transform.localPosition) {
					ball.GetComponent<BallScript>().Fetched();
				}
			}
		}
	}

	public void Init() {
		// ball.GetComponent<BallScript>().Reset();
	}
}
