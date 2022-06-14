using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{
    [SerializeField] private List<IAmInTheWay> currentlyInTheWay;
    [SerializeField] private List<IAmInTheWay> alreadyTransparent;
    [SerializeField] private Transform planet;
    [SerializeField] private Transform player;
    private Transform camera;

    
    public float transDistance = 3.5f;


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

        TransparentNearCamera();

        //GetAllObjectsInTheWay();
        

        //MakeObjectsTransparent();
        //MakeObjectsSolid();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        
    }

    void TransparentNearCamera()
    {
        //float distance = Vector3.Magnitude( camera.position -planet.position);

        RaycastHit[] hits =  Physics.SphereCastAll(
            transform.position, 
            transDistance, 
            transform.forward);

        if (hits.Length > 0)
        {
            IAmInTheWay[] inTheWays = GameObject.FindObjectsOfType<IAmInTheWay>();
            foreach (IAmInTheWay i in inTheWays)
            {
                i.ShowSolid();
            }
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out IAmInTheWay inTheWay))
                {
                    inTheWay.ShowTransparent();
                    //if (!currentlyInTheWay.Contains(inTheWay))
                    //{
                    //    print(">>>> " + inTheWay.gameObject.name);
                    //    //currentlyInTheWay.Add(inTheWay);

                    //    inTheWay.ShowTransparent();

                    //}
                }
            }
        }



    }

    void GetAllObjectsInTheWay()
    {
        float cameraPlayerDistance = Vector3.Magnitude(camera.position - player.position);

        Ray ray1Forward = new Ray(camera.position, player.position - camera.position);
        Ray ray1Backward = new Ray(player.position, camera.position - player.position);

        var hits1Forward = Physics.RaycastAll(ray1Forward, cameraPlayerDistance);
        var hits1Backward = Physics.RaycastAll(ray1Backward, cameraPlayerDistance);

        Debug.DrawRay
            (transform.position,
            player.position - camera.position,
            Color.green);

        //print("already transparent number = " + alreadyTransparent.Count );

        if (hits1Forward.Length > 0)
        {
            //GameObject[] enemies1 = GameObject.FindGameObjectsWithTag("Enemy1");
            IAmInTheWay[] inTheWays = GameObject.FindObjectsOfType<IAmInTheWay>();
            foreach (IAmInTheWay i in inTheWays)
            {
                i.ShowSolid();
            }
            foreach (var hit in hits1Forward)
            {
                if (hit.collider.gameObject.TryGetComponent(out IAmInTheWay inTheWay))
                {
                    if (!currentlyInTheWay.Contains(inTheWay))
                    {
                        print(">>>> " + inTheWay.gameObject.name);
                        //currentlyInTheWay.Add(inTheWay);

                        inTheWay.ShowTransparent();

                    }
                }
            }
        }


        //foreach (var hit in hits1Forward)
        //{
        //    if (hit.collider.gameObject.TryGetComponent(out IAmInTheWay inTheWay))
        //    {
        //        if (!currentlyInTheWay.Contains(inTheWay))
        //        {
        //            print(">>>> " + inTheWay.gameObject.name);
        //            currentlyInTheWay.Add(inTheWay);
        //        }

        //    }
        //}
        //GameObject[] enemies1 = GameObject.FindGameObjectsWithTag("Enemy1");
        //print("Num enemy1 = " + enemies1.Length);
        //for (int i = enemies1.Length - 1; i >= 0; i--)
        //{
        //    IAmInTheWay wasInTheWay = alreadyTransparent[i];
        //    if (!currentlyInTheWay.Contains(wasInTheWay))
        //    {
        //        wasInTheWay.ShowSolid();
        //        alreadyTransparent.Remove(wasInTheWay);
        //    }
        //}



        //foreach (var hit in hits1Forward)
        //{
        //    if (hit.collider.gameObject.TryGetComponent(out IAmInTheWay inTheWay))
        //    {
        //        if (!currentlyInTheWay.Contains(inTheWay))
        //        {
        //            print(">>>> " + inTheWay.gameObject.name);
        //            currentlyInTheWay.Add(inTheWay);
        //        }
        //    }
        //}
        //foreach (var hit in hits1Backward)
        //{
        //    if (hit.collider.gameObject.TryGetComponent(out IAmInTheWay inTheWay))
        //    {
        //        if (!currentlyInTheWay.Contains(inTheWay))
        //        {
        //            currentlyInTheWay.Add(inTheWay);
        //        }
        //    }
        //}

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
        for (int i = alreadyTransparent.Count - 1; i >= 0; i--)
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
