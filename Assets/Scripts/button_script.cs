using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_script : MonoBehaviour {

    public int direction = 1; //1 means to the right

    private GameObject belt;
    private GameObject[] belts;

    private float time_since_last_box;
    private float box_time_min; //minimum number of seconds since last box generation
    private float box_time_range; //variance range;
    private float next_time;
    private float wheels_rotation_speed = 30f;

	// Use this for initialization
	void Start () {
        time_since_last_box = Time.time;
        box_time_min = 2f;
        box_time_range = 4f;
        next_time = time_since_last_box + box_time_min + Random.Range(0, box_time_range);
        direction = 1;
        
		if (belts==null)
        {
            belts = GameObject.FindGameObjectsWithTag("belt");
            Debug.Log("Found some belts");
            Debug.Log(belts.Length);
        }
	}

    private void OnMouseDown()
    {

        if (direction == 1)
        {
            direction = -1;
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
            Debug.Log("Turn left", gameObject);
        }
        else
        {
            direction = 1;
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            Debug.Log("Turn right", gameObject);
        }

        for (int i=0;i<belts.Length;i++)
        {
            belts[i].GetComponent<SurfaceEffector2D>().speed *= -1;
            belts[i].transform.Find("left_wheel").transform.Rotate(Vector3.forward * wheels_rotation_speed*Time.deltaTime);
        }
    }


    // Update is called once per frame
    void Update () {

        
        //generate new box at the random time range
        if (Time.time>=next_time)
        {
            GameObject new_box = (GameObject)Instantiate(Resources.Load("box"));
            new_box.transform.localPosition = new Vector3(-3.5f, 4.4f, 0f);
            
            
            Debug.Log("Time for the next box");
            next_time = next_time + box_time_min + Random.Range(0, box_time_range);

            
        }
    }

    
}
