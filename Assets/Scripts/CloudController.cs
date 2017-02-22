using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {

	private int direction;
  private float cloudTime;

  public GameObject cloudType;
	
	// Use this for initialization
	void Start () {
    resetCloudTime();
		direction = Random.Range(0, 2);
		int clouds = Random.Range(5, 8);
	}
	
	// Update is called once per frame
	void Update () {
    cloudTime -= Time.deltaTime;

    if(cloudTime <= 0.0f)
    {
      spawnCloud();
    }
	
	}

  void resetCloudTime() {
    cloudTime = Random.Range(25.0f, 65.0f);
  }

  void spawnCloud(bool initialCloud = false) {
    Vector3 cloudPosition;
    if(initialCloud) {

    }
    // Instantiate (cloudType, , Quaternion.Euler (new Vector3 (0, 0, 0)));
  }
	
}
