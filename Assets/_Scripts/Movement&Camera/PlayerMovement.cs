using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int _forwardSpeed, _swipeSpeed;

    [SerializeField]
    private Transform _trayTransform;
    private Quaternion goingRightRotation = Quaternion.Euler(0, 0, 15);
    private Quaternion goingLeftRotation = Quaternion.Euler(0, 0, -15);
    private Quaternion noInputRotation = Quaternion.Euler(Vector3.zero);

    [SerializeField]
    private float _smoothSpeed;

    private float horizontalPosition;


    private void Update() 
    {
        MoveForward();
        TouchControl();
        KeyboardControl();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _forwardSpeed * Time.deltaTime);
    }

    private void TouchControl()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                horizontalPosition = transform.position.x + touch.deltaPosition.x * _swipeSpeed * Time.deltaTime;

                if (horizontalPosition > LevelBoundary._levelBoundaryLeft && horizontalPosition < LevelBoundary._levelBoundaryRight)
                {
                    if (touch.deltaPosition.x > 0)
                    {
                        Quaternion smoothedRotation = Quaternion.Slerp(_trayTransform.rotation, goingRightRotation, _smoothSpeed);
                        _trayTransform.rotation = smoothedRotation;
                    }
                    else if (touch.deltaPosition.x < 0)
                    {
                        Quaternion smoothedRotation = Quaternion.Slerp(_trayTransform.rotation, goingLeftRotation, _smoothSpeed);
                        _trayTransform.rotation = smoothedRotation;
                    }

                    Vector3 desiredPosition = new Vector3(horizontalPosition, transform.position.y, transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, desiredPosition, _smoothSpeed);

                }
            }
            
        }
        else
        {
            Quaternion smoothedRotation = Quaternion.Slerp(_trayTransform.rotation, noInputRotation, _smoothSpeed);
            _trayTransform.rotation = smoothedRotation;
        }
    }

    private void KeyboardControl()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _swipeSpeed * Time.deltaTime);

            Quaternion smoothedRotation = Quaternion.Slerp(_trayTransform.rotation, goingLeftRotation, _smoothSpeed);
            _trayTransform.rotation = smoothedRotation;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _swipeSpeed * Time.deltaTime);
            Quaternion smoothedRotation = Quaternion.Slerp(_trayTransform.rotation, goingRightRotation, _smoothSpeed);
            _trayTransform.rotation = smoothedRotation;
        }
        else
        {
            Quaternion smoothedRotation = Quaternion.Slerp(_trayTransform.rotation, noInputRotation, _smoothSpeed);
            _trayTransform.rotation = smoothedRotation;
        }
    }
}
