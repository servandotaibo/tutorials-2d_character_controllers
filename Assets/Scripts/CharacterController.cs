using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float m_maxSpeed = 10f;

    private Rigidbody2D m_rb2d;
    private Animator m_animator;
    private Transform m_transform;

    private bool m_facingRight = true;

    private bool m_grounded = false;
    public float m_jumpForce = 700f;
    public Transform m_feet;
    public LayerMask m_groundLayerMask;

	// Use this for initialization
	void Start ()
    {
        m_rb2d = (Rigidbody2D) GetComponent<Rigidbody2D>();
        m_animator = (Animator)GetComponent<Animator>();
        m_transform = transform;
	}
		
	void FixedUpdate ()
    {
        m_grounded = Physics2D.OverlapCircle(m_feet.position, 0.02f, m_groundLayerMask);
        Debug.Log("grounded = " + m_grounded);
        m_animator.SetBool("Grounded", m_grounded);
        m_animator.SetFloat("vSpeed", m_rb2d.velocity.y);

        float move = Input.GetAxis("Horizontal");
        m_animator.SetFloat("Speed", Mathf.Abs(move));
        m_rb2d.velocity = new Vector2(move * m_maxSpeed, m_rb2d.velocity.y);

        if ((move < 0 && m_facingRight) ||
            (move > 0 && !m_facingRight))
        {
            Flip();
        }
	}

    private void Update()
    {
        if (m_grounded && Input.GetKeyDown(KeyCode.Space))
        {
            m_rb2d.AddForce(new Vector2(0f, m_jumpForce));
            m_animator.SetBool("Grounded", false);
            m_grounded = false;
        }            
    }

    private void Flip()
    {
        m_facingRight = !m_facingRight;
        Vector3 scale = m_transform.localScale;
        scale.x = -scale.x;
        m_transform.localScale = scale;
        
    }
}
