using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceArray : BaseRingArray
{

    ArrayManager arrayManager;

    // Start is called before the first frame update
    void Start()
    {
        //print("DEFENCY ARRAY START");
        arrayManager = transform.GetComponentInParent<ArrayManager>();
        base.Start();

    }

    //new void Start()
    //{
    //    base.
    //}

    // Update is called once per frame
    void Update()
    {
        HandleTransparency();
    }

}
