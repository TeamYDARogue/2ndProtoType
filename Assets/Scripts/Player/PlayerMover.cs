using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {

    private Rigidbody m_rigidbody = null;

    private Vector3 m_startPos;
    private Vector3 m_endPos;
    private float m_speed = 0.4f;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        m_startPos = transform.position;
        m_endPos = transform.position;
    }

    void Update()
    {   
        m_startPos = gameObject.transform.position;

        if(Input.GetButtonDown("Up") && gameObject.transform.position == m_endPos)
        {
            m_endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            if (HitProcess()) { m_endPos = transform.position; }
        }
        if (Input.GetButtonDown("Down") && gameObject.transform.position == m_endPos)
        {
            m_endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            if (HitProcess()) { m_endPos = transform.position; }

        }
        if (Input.GetButtonDown("Right") && gameObject.transform.position == m_endPos)
        {
            m_endPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            if (HitProcess()) { m_endPos = transform.position; }

        }
        if (Input.GetButtonDown("Left") && gameObject.transform.position == m_endPos)
        {
            m_endPos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            if (HitProcess()) { m_endPos = transform.position; }

        }

        //Debug.Log("EndPosition"+m_endPos);

        Vector3 pos = Vector3.MoveTowards(m_startPos, m_endPos, m_speed);
        m_rigidbody.MovePosition(pos);
    }

    bool HitProcess()
    {
        var hit = new RaycastHit();
        Physics.Linecast(transform.position, m_endPos, out hit);

        return hit.collider != null;
    }
}
