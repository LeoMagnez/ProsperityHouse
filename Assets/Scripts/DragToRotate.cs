using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToRotate : MonoBehaviour
{
    public float rotationSpeed;

    public void OnMouseDrag()
    {
        float XAxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.down, XAxisRotation);
    }
}
