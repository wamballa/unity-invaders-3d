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
        isSolid = false;
        solidBody.SetActive(false);
        transBody.SetActive(true);

    }

    public void ShowSolid()
    {
        isSolid = true;
        solidBody.SetActive(true);
        transBody.SetActive(false);

    }

    public bool GetIsSolid()
    {
        return isSolid;
    }
}
