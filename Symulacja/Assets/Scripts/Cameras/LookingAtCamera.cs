using UnityEngine;
using System.Collections;

public class LookingAtCamera : CameraScript
{
    void OnEnable()
    {
        /*Vector3 dir = Simulation.Instance.Meteor.transform.position - Simulation.Instance.Earth.transform.position;
        dir.Normalize();
        transform.position = Simulation.Instance.Meteor.transform.position + dir * 5.5f * Simulation.Instance.Meteor.transform.localScale.x;*/
    }
    
    void Init()
    {
        Vector3 dir = transform.position - Simulation.Instance.Meteor.transform.position;
        float distance = dir.magnitude * 0.5f;
        dir.Normalize();
        Vector3 newDir = Quaternion.Euler(0.0f, -30.0f, 0.0f) * dir;
        newDir *= distance;
        transform.position = Simulation.Instance.Meteor.transform.position + newDir;
        transform.LookAt(Simulation.Instance.Meteor.transform);
    }

    // Update is called once per frame
    void Update ()
    {
        //transform.LookAt(Simulation.Instance.Meteor.transform);
    }
}
