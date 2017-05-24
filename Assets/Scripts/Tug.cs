using UnityEngine;
using System.Collections;

public class TugActivity {
	
	private Dog dog;
	private bool atTarget;
	private Vector3 target;
	private Vector3 targetScale;
	
	public GameObject TugRope;
	public GameObject TugRope60;

	public TugActivity (Dog thatDog, GameObject tug, GameObject tug60) {
		Init();
		target = new Vector3 (0.0f, -1.25f, 0.0f);
		targetScale = new Vector3 (4.0f, 4.0f, 1.0f);
		dog = thatDog;
		TugRope = tug;
		TugRope60 = tug60;
	}
	
	public void Init() {
		atTarget = false;
	}
	
	public void Run () {
		if (atTarget) {
			if(TugRope.GetComponent<SpriteRenderer> ().isVisible) {
				bool changed;
				if(TugRope.transform.localPosition.x > 1.3f) {
					if(TugRope.transform.localPosition.y > 0.0f) {
						changed = dog.AnimTrigger("tugSheet_UR");
					} else {
						changed = dog.AnimTrigger("tugSheet_LR");
					}
				} else if(TugRope.transform.localPosition.x < -1.3f){
					if(TugRope.transform.localPosition.y > 0.0f) {
						changed = dog.AnimTrigger("tugSheet_UL");
					} else {
						changed = dog.AnimTrigger("tugSheet_LL");
					}
				} else {
					changed = dog.AnimTrigger("tugSheet_C");
				}
				
				if(changed) {
					#if UNITY_ANDROID || UNITY_IPHONE
						Handheld.Vibrate();
					#endif
					Controller.myDog.Growl();
				}
			} else {
				dog.AnimTrigger("dogSheet_idle");
			}
			
		} else {
			dog.RunTowards(target, targetScale, dog.returnSpeedX, dog.returnSpeedS);
			// dog.me.transform.localPosition = Vector3.MoveTowards(dog.me.transform.localPosition, target, dog.returnSpeedX);
			// dog.me.transform.localScale = Vector3.MoveTowards(dog.me.transform.localScale, targetScale, dog.returnSpeedS);
			// dog.AnimTrigger("dogSheet_runTowards");
			// Game1.appDJ.runningOn = true;
			if(dog.transform.localPosition == target && dog.transform.localScale == targetScale) {
				// Game1.appDJ.runningOn = false;
				TugRope60.GetComponent<SpriteRenderer> ().enabled = true;
				TugRope60.GetComponent<CircleCollider2D> ().enabled = true;
				// dog.AnimTrigger("dogSheet_idle");
				atTarget = true;
			}
		}
	}
}
