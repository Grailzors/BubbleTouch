using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

    [Header("Repulse Control")]
    public float radius = 1f;
    public float strength = 10f;
    public float distanceMin = 0f;
    public float distanceMax = 0f;

    private BoxCollider collider;


    // Use this for initialization
    void Start ()
    {
        collider = GetComponent<BoxCollider>();
        collider.size = new Vector3(radius, radius, radius);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}


    private void OnCollisionStay(Collision col)
    {
        RepulseObjects(col);
    }

    void RepulseObjects(Collision col)
    {
        if (col.rigidbody.tag == "Puck")
        {
            /*
            float d = Vector3.Distance(transform.position, col.transform.position);
            print(d);

            col.rigidbody.AddForce(new Vector3(1,1,1) * strength);
            */

            Vector3 dir = col.contacts[0].point - transform.position;
            dir = dir.normalized;

            col.rigidbody.AddForce(dir + (Vector3.Normalize(dir) * strength));

        }
    }

}
