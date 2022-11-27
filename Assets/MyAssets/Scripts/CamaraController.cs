using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotateSpeed;
    public Transform pivot;
    public float maxViewAngle;
    public float minViewAngle;
    public bool invertY;
    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = null; 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pivot.transform.position = target.transform.position;
        //Obtener la posicion en x del mouse y rotar la camara pivote
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivot.Rotate(0, horizontal, 0);
        float joystickH = Input.GetAxis("LJHorizontal") * rotateSpeed;
        pivot.Rotate(0, joystickH, 0);

        //Obtener la posicion en y del mouse y rotar la camara pivote
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        float joystickV = Input.GetAxis("LJVertical") * rotateSpeed;
        if (invertY)
        {
            pivot.Rotate(vertical, 0, 0);
            pivot.Rotate(joystickV, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
            pivot.Rotate(-joystickV, 0, 0);
        }

        //Limitar que tan alto y bajo llega la rotación de la camara
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0,0);
        }
        if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }
        //Mover la camara de acuerdo a la rotacion actual del personaje
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle,desiredYAngle,0);
        transform.position = target.position - (rotation * offset);

        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - .5f, transform.position.z);
        }
        transform.LookAt(target);
    }
}
