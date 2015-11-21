using UnityEngine;
using System.Collections;

public class LookingAtCamera : CameraScript
{
    public float FasterModeMultiplier = 1.0f;

    private bool _rmbDown = false;
    private Vector3 _mousePosition = Vector3.zero;

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

        if(Input.GetMouseButtonDown(1) && Input.mousePosition.x <= Screen.width * 0.5f)
        {
            _rmbDown = true;
            _mousePosition = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(1) && Input.mousePosition.x <= Screen.width * 0.5f)
        {
            _rmbDown = false;
        }

        if(_rmbDown && Input.mousePosition.x <= Screen.width * 0.5f)
        {
            if (Input.GetKeyDown(KeyCode.F) && Simulation.Instance.Meteor != null)
            {
                transform.LookAt(Simulation.Instance.Meteor.transform);
            }

            Vector3 currentMousePosition = Input.mousePosition;
            float dx = currentMousePosition.x - _mousePosition.x;
            float dy = currentMousePosition.y - _mousePosition.y;
            _mousePosition = currentMousePosition;

            transform.Rotate(new Vector3(-dy, dx, 0.0f));

            Vector3 movement = new Vector3(
                                          Input.GetAxis("Horizontal"),
                                          0.0f,
                                          Input.GetAxis("Vertical")
                                          );

            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                movement *= FasterModeMultiplier;
            }

            movement = transform.rotation * movement;

            transform.position += movement * Time.deltaTime * 100.0f;
        }
    }
}
