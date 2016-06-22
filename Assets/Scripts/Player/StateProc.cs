using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateProc
{

    private SwordState swordState = new SwordState();
    private AxeState axeState = new AxeState();
    private ArrowState arrowState = new ArrowState();

    private delegate void WeaponProc();
    private Dictionary<WeaponState, WeaponProc> m_procHash = new Dictionary<WeaponState, WeaponProc>();

    public static WeaponState NowState
    {
        get;
        set;
    }

    public StateProc()
    {
        m_procHash.Add(WeaponState.eSword, swordState.SwordProc);
        m_procHash.Add(WeaponState.eAxe, axeState.AxeProc);
        m_procHash.Add(WeaponState.eArrow, arrowState.ArrowProc);
    }
	
    public void Proc()
    {
        m_procHash[NowState]();
    }

}
