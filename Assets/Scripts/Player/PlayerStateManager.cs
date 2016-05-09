using UnityEngine;
using System.Collections;

public class PlayerStateManager : MonoBehaviour {

    private enum State
    {
        Idle = 0,
        Move,
    }
    private State m_state = State.Idle;

    public bool IsMove { get { return m_state == State.Move; } }
    public bool IsIdle { get { return m_state == State.Idle; } }

    public void ChangeMove()
    {
        if (IsMove) return;

        m_state = State.Move;
    }

    public void ChangeIdle()
    {
        if (IsIdle) return;

        m_state = State.Idle;
    }

    void Update()
    {
        if (!IsMove) return;
        ChangeIdle();
    }
}
