using UnityEngine;
using System.Collections;

public class FollowingCamera : CameraScript
{
    private Vector2 _mousePos;

    private bool _firstFrame = true;
    private Vector3 _currentAngles;
    private float _offsetLength;
    private Vector3 _back;
    private Vector3 _offset;

    public void OnEnable()
    {
        transform.LookAt(Simulation.Instance.Meteor.transform);
        _currentAngles = transform.rotation.eulerAngles;
        _offsetLength = (transform.position - Simulation.Instance.Meteor.transform.position).magnitude;
        _back = (transform.position - Simulation.Instance.Meteor.transform.position).normalized;
        _offset = _back * _offsetLength;
        transform.position = Simulation.Instance.Meteor.transform.position + _offset;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Simulation.Instance.Meteor == null)
        {
            return;
        }
        if (Input.GetMouseButton(1) && Input.mousePosition.x > Screen.width * 0.62f)
        {
            _offsetLength -= Input.mouseScrollDelta.y * 5.0f;
            _offsetLength = Mathf.Clamp(_offsetLength, 3.0f, float.MaxValue);

            if (_firstFrame)
            {
                _firstFrame = false;
                _mousePos = Input.mousePosition;
            }
            Vector2 currentMousePos = Input.mousePosition;
            Vector2 diff = currentMousePos - _mousePos;
            _mousePos = currentMousePos;
            
            _currentAngles += new Vector3(0.0f, diff.x, -diff.y);
            _offset = Quaternion.Euler(_currentAngles) * _back;
            _offset.Normalize();
            _offset *= _offsetLength;
        }
        else
        {
            _firstFrame = true;
        }

        transform.position = Simulation.Instance.Meteor.transform.position + _offset;
    }

    void LateUpdate()
    {
        if(Simulation.Instance.Meteor == null)
        {
            return;
        }
        transform.LookAt(Simulation.Instance.Meteor.transform);
    }
}
