using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{
    [SerializeField] private List<IAmInTheWay> currentlyInTheWay;
    [SerializeField] private List<IAmInTheWay> alreadyTransparent;
    [SerializeField] private Transform player;
    private Transform camera;


    // Start is called before the first frame update
    void Awake()
    {
        currentlyInTheWay = new List<IAmInTheWay>();
        alreadyTransparent = new List<IAmInTheWay>();

        camera = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetAllObjectsInTheWay();
        MakeObjectsSolid();
        MakeObjectsTransparent();
    }

    void GetAllObjectsInTheWay()
    {
        float cameraPlayerDistance = Vector3.Magnitude(camera.position - player.position);

        Ray ray1Forward = new Ray(camera.position, player.position - camera.position);
        Ray ray1Backward = new Ray(player.position, camera.position - player.position);

        var hits1Forward = Physics.RaycastAll(ray1Forward, cameraPlayerDistance);
        var hits1Backward = Physics.RaycastAll(ray1Backward, cameraPlayerDistance);

        foreach (var hit in hits1Forward)
        {
            if (hit.collider.gameObject.TryGetComponent(out IAmInTheWay inTheWay))
            {
                if (!currentlyInTheWay.Contains(inTheWay))
                {
                    currentlyInTheWay.Add(inTheWay);
                }
            }
        }
        foreach (var hit in hits1Backward)
        {
            if (hit.collider.gameObject.TryGetComponent(out IAmInTheWay inTheWay))
            {
                if (!currentlyInTheWay.Contains(inTheWay))
                {
                    currentlyInTheWay.Add(inTheWay);
                }
            }
        }

    }
    void MakeObjectsTransparent()
    {
        for (int i = 0; i < currentlyInTheWay.Count; i++)
        {
            IAmInTheWay inTheWay = currentlyInTheWay[i];
            if (!alreadyTransparent.Contains(inTheWay))
            {
                inTheWay.ShowTransparent();
                alreadyTransparent.Add(inTheWay);
            }
        }
    }
    void MakeObjectsSolid()
    {
        for (int i = alreadyTransparent.Count; i >= 0; i--)
        {
            IAmInTheWay wasInTheWay = alreadyTransparent[i];
            if (!currentlyInTheWay.Contains(wasInTheWay))
            {
                wasInTheWay.ShowSolid();
                alreadyTransparent.Remove(wasInTheWay);
            }

        }
    }

}
