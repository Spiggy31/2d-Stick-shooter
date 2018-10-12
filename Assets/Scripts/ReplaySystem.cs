using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour
{
    private const int bufferFrames = 100;
    private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
    private Rigidbody rigidBody;
    private GameManager manager;
	
    // Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        manager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (manager.recording)
        {
            Record();
        }
        else
        {
            PlayBack();
        }
    }

    void PlayBack()
    {
        rigidBody.isKinematic = true;
        int frame = Time.frameCount % bufferFrames;
        print("Reading frame " + frame);
        transform.position = keyFrames[frame].position;
        transform.rotation = keyFrames[frame].rotation;
    }

    void Record()
    {
        int frame = Time.frameCount % bufferFrames;
        float time = Time.time;
        // Debug.Log("Writing frame " + frame);
        keyFrames[frame] = new MyKeyFrame(time, transform.position, transform.rotation);
	}
}

/// <summary>
/// A structure for storing time, rotation and position.
/// </summary>
public class MyKeyFrame
{
    public float frameTime;
    public Vector3 position;
    public Quaternion rotation;

    public MyKeyFrame(float aTime, Vector3 aPosition, Quaternion aRotation)
    {
        frameTime = aTime;
        position = aPosition;
        rotation = aRotation;
    }
}
