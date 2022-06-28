using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAmInTheWay : MonoBehaviour
{
    [SerializeField] private GameObject solidBody;
    [SerializeField] private GameObject transBody;

    bool isSolid = true;

    // Start is called before the first frame update
    public void ShowTransparent()
    {
        solidBody.SetActive(false);
        transBody.SetActive(true);
        isSolid = false;
    }

    public void ShowSolid()
    {
        solidBody.SetActive(true);
        transBody.SetActive(false);
        isSolid = true;
    }

    public bool GetIsSolid()
    {
        return isSolid;
    }
}
