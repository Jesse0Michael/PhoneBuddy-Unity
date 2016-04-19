using UnityEngine;
using System.Collections;

public class FoodActivity {
	
	private Dog dog;
	private bool atTarget;
	private Vector3 target;

	public FoodActivity(Dog dog) {
		atTarget = false;
		target = new Vector3 (2.15f, -0.6f, 0.0f);
		this.dog = dog;
	}
	
	public void Run () {
		if (atTarget) {
			if (dog.statHunger <= 1.0f) {
				dog.AnimTrigger("dogSheet_eat");
				// Game1.appDJ.foodOn = true;
				dog.statHunger += 0.0004f;
			} else {
				dog.returnHome = true;
				atTarget = false;
				// Game1.appDJ.foodOn = false;
				// Game1.appDJ.runningOn = true;
			}

		} else if (dog.statHunger <= 0.9f) {
			dog.me.transform.localPosition = Vector3.MoveTowards(dog.me.transform.localPosition, target, dog.returnSpeedX);
			dog.AnimTrigger("dogSheet_walk");
			// Game1.appDJ.runningOn = true;
			if(dog.me.transform.localPosition == target) {
				// Game1.appDJ.runningOn = false;
				atTarget = true;
			}
		}
	}
}
