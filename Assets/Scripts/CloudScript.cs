using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour {

	public Sprite Cloud1;
	public Sprite Cloud2;
	public Sprite Cloud3;
	public Sprite Cloud4;
	public Sprite Cloud5;

  public AudioClip Bird1;
  public AudioClip Bird2;
  public AudioClip Bird3;
  public AudioClip Bird4;
  public AudioClip Bird5;

	private AudioSource source;

	private float velocity;
	
	// Use this for initialization
	void Start () {
		Sprite[] cloudSprites = new Sprite[5]{Cloud1, Cloud2, Cloud3, Cloud4, Cloud5};
		int cloudSelection = Random.Range(0, 5);
		GetComponent<SpriteRenderer> ().sprite = cloudSprites[cloudSelection];

		velocity = Random.Range(0.01f, 0.05f) / (transform.localPosition.y * 100.0f);

    float cloudSize = (2.5f - transform.localPosition.y) / 2;
		transform.localScale = new Vector3(1 - cloudSize, 1 - cloudSize, 1);
		
		source = GetComponent<AudioSource>();
		AudioClip[] birdSounds = new AudioClip[5]{Bird1, Bird2, Bird3, Bird4, Bird5};
		int birdSelection = Random.Range(0, 5);
		source.clip = birdSounds[birdSelection];
		source.Play();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(5 * CloudController.direction, transform.localPosition.y, transform.localPosition.z), velocity);

		if(transform.localPosition.x >= 5.0f || transform.localPosition.x <= -5.0f){
			Destroy(gameObject);
		}
	}

	
}
