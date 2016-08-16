using UnityEngine;
using System.Collections;

public class WaterActivity {
	
	private Dog dog;
	private bool atTarget;
	private Vector3 target;

	public WaterActivity(Dog dog) {
		Init();
		target = new Vector3 (-2.15f, -0.6f, 0.0f);
		this.dog = dog;
	}

	public void Init() {
		atTarget = false;
	}
	
	public void Run () {
		if (atTarget) {
			if (dog.statThirst <= 1.0f) {
				dog.AnimTrigger("dogSheet_drink");
				// Game1.appDJ.drinkOn = true;
				dog.statThirst += 0.0004f;
			} else {
				dog.ReturnHome();
				atTarget = false;
				// Game1.appDJ.drinkOn = false;
				// Game1.appDJ.runningOn = true;
			}
		} else if (dog.statThirst <= 0.9f) {
			dog.me.transform.localPosition = Vector3.MoveTowards(dog.me.transform.localPosition, target, dog.returnSpeedX);
			dog.AnimTrigger("dogSheet_walk_left");
			// Game1.appDJ.runningOn = true;
			if(dog.me.transform.localPosition == target) {
				// Game1.appDJ.runningOn = false;
				atTarget = true;
			}
		}
	}
	
	public void Chuck() {
		
	}
}
