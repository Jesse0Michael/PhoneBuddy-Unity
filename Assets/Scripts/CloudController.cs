using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour
{

    static public float direction;
    private float cloudTime;

    public GameObject cloudType;

    // Use this for initialization
    void Start()
    {
        resetCloudTime();
        if (Random.Range(0, 2) == 0)
        {
            direction = -1.0f;
        }
        else
        {
            direction = 1.0f;
        }
        for (int i = 0; i <= Random.Range(4, 7); i++)
        {
            spawnCloud(true);
        }
        spawnCloud();
    }

    // Update is called once per frame
    void Update()
    {
        cloudTime -= Time.deltaTime;

        if (cloudTime <= 0.0f)
        {
            spawnCloud();
            resetCloudTime();
        }

    }

    void resetCloudTime()
    {
        cloudTime = Random.Range(40.0f, 100.0f);
        Debug.Log("Cloud time: " + cloudTime);
    }

    void spawnCloud(bool initialCloud = false)
    {
        Vector3 cloudPosition;
        float y = Random.Range(1.5f, 2.25f);
        if (initialCloud)
        {
            cloudPosition = new Vector3(Random.Range(-3.0f, 3.0f), y, 0);
        }
        else
        {
            cloudPosition = new Vector3(-4.5f * direction, y, 0);
        }
        Instantiate(cloudType, cloudPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
    }

}
