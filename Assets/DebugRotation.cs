using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugRotation : MonoBehaviour
{

    public Transform[] ringArrays;
    public TMP_Text debugText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = "";
        //foreach (Transform ring in ringArrays)
        for (int i=0; i < ringArrays.Length;i++)
        {

            //float localRotation = ringArrays[i].rotation.eulerAngles.y;
            //int angleInt = Mathf.RoundToInt(localRotation);
            int angleInt = ringArrays[i].GetComponent<RingArray>().GetRotation();
            debugText.text += "> " + ringArrays[i].name + " > "+angleInt +"\n";
        }
    }
}
