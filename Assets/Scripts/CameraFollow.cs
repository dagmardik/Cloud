using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject m_Player;

    public Vector3 m_Offset;

    //hey is this working

    public float m_Speed = 3.5f;
    private float X;
    private float Y;

    // offset equels the transform - the player. Aligns the camera
    void Start()
    {
        m_Offset = transform.position - m_Player.transform.position;
    }

    // Update is called once per frame
    // Camera is the player position plus the offset. Aligns the camera
    // When the player presses the mouse button and drags it, rotate the camera
    void Update()
    {
        transform.position = m_Player.transform.position + m_Offset;

        if (Input.GetMouseButton(0))
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * m_Speed, -Input.GetAxis("Mouse X") * m_Speed, 0));
            X = 30;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
        }
    }
}
