using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
       // Update is called once per frame
    void Update()
    {

        this.transform.Translate(0, 0, 0.1f);
        if (this.transform.position.z > 50) 
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -50);
        }

    }
}
