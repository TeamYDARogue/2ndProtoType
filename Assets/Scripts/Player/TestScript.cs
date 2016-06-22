using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

    StateProc proc = new StateProc();
    
	// Use this for initialization
	void Start () {
        StateProc.NowState = WeaponState.eSword;
	}
	
	// Update is called once per frame
	void Update () {
        proc.Proc();
        ChangeSword();
        ChangeAxe();
        ChangeArrow();
	}


    void ChangeSword()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StateProc.NowState = WeaponState.eSword;
        }
    }

    void ChangeAxe()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StateProc.NowState = WeaponState.eAxe;
        }
    }
    void ChangeArrow()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StateProc.NowState = WeaponState.eArrow;
        }
    }
}
