using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    private Vector2 _mousePos;

    private bool _firstFrame = true;

	public void SetPosition ()
    {
        Vector3 dir = Simulation.Instance.Meteor.transform.position - Simulation.Instance.Earth.transform.position;
        dir.Normalize();
        transform.position = Simulation.Instance.Meteor.transform.position + dir * 2.5f * Simulation.Instance.Meteor.transform.localScale.x;
        transform.LookAt(Simulation.Instance.Earth.transform);
	}

    public void Update()
    {
        if(Input.GetMouseButton(1))
        {
            if(_firstFrame)
            {
                _firstFrame = false;
                _mousePos = Input.mousePosition;
            }
            Vector2 currentMousePos = Input.mousePosition;
            Vector2 diff = currentMousePos - _mousePos;
            _mousePos = currentMousePos;

            transform.Rotate(new Vector3(-diff.y, diff.x, 0.0f));
        }
        else
        {
            _firstFrame = true;
        }
    }
}
