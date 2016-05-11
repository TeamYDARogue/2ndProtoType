using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
状況
・現状動くsourceには、しているが違和感がある
・打開策としてbool型に直そうとしていて、いまいち書き込めてない
　のでコメントアウトにしています。

*/

public class GimmckSpear: MonoBehaviour
{

    private bool spearact = true;
   

    void Update()
    {
        if (spearact)
        {
            Vector3 pos = gameObject.transform.position;
            pos.y = 0;
            gameObject.transform.position = pos;
            Debug.Log("true");

            if (Input.GetKeyDown(KeyCode.Z))
            {
                spearact = false;
            }
        }
        else
        {
            Vector3 pos = gameObject.transform.position;
            pos.y = -100;
            gameObject.transform.position = pos;
            Debug.Log("false");

            if (Input.GetKeyDown(KeyCode.Z))
            {
                spearact = true;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("atari");
        }

    }

}
