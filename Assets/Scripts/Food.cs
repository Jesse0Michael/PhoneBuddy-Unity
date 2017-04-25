using UnityEngine;
using System.Collections;

public class FoodActivity {
	
	private Dog dog;
	private bool atTarget;
	private Vector3 target;

	public FoodActivity(Dog dog) {
		Init();
		target = new Vector3 (2.15f, -0.6f, 0.0f);
		this.dog = dog;
	}

	public void Init() {
		atTarget = false;
	}
	
	public void Run () {
		if (atTarget) {
			if (dog.statHunger <= 1.0f) {
				dog.AnimTrigger("dogSheet_eat");
				dog.Eating();
				dog.statHunger += 0.0004f;
			} else {
				dog.StopSounds();
				dog.ReturnHome();
				atTarget = false;
			}

		} else if (dog.statHunger <= 0.9f) {
			dog.transform.localPosition = Vector3.MoveTowards(dog.transform.localPosition, target, dog.returnSpeedX);
			dog.AnimTrigger("dogSheet_walk");
			dog.Bark();
			if(dog.transform.localPosition == target) {
				dog.StopSounds();
				atTarget = true;
			}
		}
	}
}
