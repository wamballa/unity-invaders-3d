using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAmInTheWay : MonoBehaviour
{
    [SerializeField] private GameObject solidBody;
    [SerializeField] private GameObject transBody;

    // Start is called before the first frame update
    public void ShowTransparent()
    {
        solidBody.SetActive(false);
        transBody.SetActive(true);
    }

    public void ShowSolid()
    {
        solidBody.SetActive(true);
        transBody.SetActive(false);
    }
}
