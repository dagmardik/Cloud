using UnityEngine;

public class PlayerController : MonoBehaviour
{

    /* 
    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }

    */

    Rigidbody m_Rigidbody;
    CapsuleCollider m_CapsuleCollider;

    public float m_MovementSpeed;
    public float m_TurningSpeed;
    public float m_JumpStrength;

    public bool m_IsGrounded;


    Vector3 m_GravityVector;
    float m_ForwardStrength;
    float m_TurningStrength;

    float radius;
	Vector3 point1;
	Vector3 point2;
	Vector3 direction;
	RaycastHit hitInfo;


    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_GravityVector = new Vector3(0, -9.8f, 0);
        m_CapsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        Move();
        CheckIfGrounded();
    }

    void CheckIfGrounded()
    {
        point1 =  transform.position + m_CapsuleCollider.center + Vector3.up*(m_CapsuleCollider.height/2-m_CapsuleCollider.radius);
		point2 = transform.position + m_CapsuleCollider.center + Vector3.up*(-m_CapsuleCollider.height/2+m_CapsuleCollider.radius);

        RaycastHit[] hits;
	    hits = Physics.CapsuleCastAll(point1,point2,radius*0.95f,Vector3.down,1f,LayerMask.GetMask("Ground"));
        Debug.Log(hits.Length);

		if(hits.Length>=1)
			m_IsGrounded = true;
		else
			m_IsGrounded = false;
    }

    void HandleInput()
    {
        m_ForwardStrength = Input.GetAxis("Vertical") * m_MovementSpeed;
        m_TurningStrength = Input.GetAxis("Horizontal") * m_TurningSpeed;

        if(m_IsGrounded)
        {
            CheckForJumpInput();
        }
       
    }

    void CheckForJumpInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpStrength, m_Rigidbody.velocity.z);
    }


    void Move()
    {
        //m_Rigidbody.velocity = m_Rigidbody.velocity + transform.forward * m_ForwardStrength;
        m_Rigidbody.velocity = new Vector3((transform.forward * m_ForwardStrength).x, 
                                        m_Rigidbody.velocity.y, 
                                        (transform.forward * m_ForwardStrength).z);
    
        transform.Rotate(0, m_TurningStrength, 0);
        //transform.Translate(0, 0, m_ForwardStrength);
    }

}