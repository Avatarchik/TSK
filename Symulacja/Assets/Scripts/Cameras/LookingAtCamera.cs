using UnityEngine;
using System.Collections;

public class LookingAtCamera : CameraScript
{
    void OnEnable()
    {
        Vector3 dir = Simulation.Instance.Meteor.transform.position - Simulation.Instance.Earth.transform.position;
        dir.Normalize();
        transform.position = Simulation.Instance.Meteor.transform.position + dir * 5.5f * Simulation.Instance.Meteor.transform.localScale.x;
    }

	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(Simulation.Instance.Meteor.transform);
    }
}
