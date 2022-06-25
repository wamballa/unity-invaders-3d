using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{
    //[SerializeField] private List<IAmInTheWay> currentlyInTheWay;
    //[SerializeField] private List<IAmInTheWay> alreadyTransparent;
    [SerializeField] private Transform planet;
    [SerializeField] private Transform player;
    private Transform camera;

    [SerializeField] private GameObject[] defences;
    [SerializeField] private GameObject[] rings;

    private SphereCollider sphereCollider;


    public float transDistance = 10f;

    //public List<GameObject> objectsInRange;


    // Start is called before the first frame update
    void Awake()
    {
        print("AWAKE");
        //currentlyInTheWay = new List<IAmInTheWay>();
        //alreadyTransparent = new List<IAmInTheWay>();
        camera = this.gameObject.transform;
        //objectsInRange = new List<GameObject>();
        sphereCollider = GetComponent<SphereCollider>();
        float dist = Vector3.Magnitude(planet.position - camera.position);
        sphereCollider.radius = dist;
        //print(dist);


    }

    // Update is called once per frame
    void Update()
    {
        //HandleTransparency();
        //TransparentNearCamera();
        //GetAllObjectsInTheWay();

        //MakeObjectsTransparent();
        //MakeObjectsSolid();

        
        HandleTransparency();

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (!objectsInRange.Contains(collision.gameObject))
    //    {


    //        objectsInRange.Add(collision.gameObject);


    //        if (collision.gameObject.TryGetComponent<IAmInTheWay>(out IAmInTheWay inTheWay))
    //        {
    //            //objectsInRange.Add(collision.gameObject);
    //        }
    //    }
    //}
    //private void OnCollisionStay(Collision collision)
    //{
    //    print("collision " + collision.transform.name);
    //    if (!objectsInRange.Contains(collision.gameObject))
    //    {
    //        objectsInRange.Add(collision.gameObject);
    //        if (collision.gameObject.TryGetComponent<IAmInTheWay>(out IAmInTheWay inTheWay))
    //        {
    //            //objectsInRange.Add(collision.gameObject);
    //        }
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        ////print("trigger " + other.transform.name);
        //if (!objectsInRange.Contains(other.gameObject))
        //{
        //    if (other.gameObject.TryGetComponent<IAmInTheWay>(out IAmInTheWay inTheWay))
        //    {
        //        objectsInRange.Add(other.gameObject);
        //        inTheWay.ShowTransparent();
        //    }
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        ////if (objectsInRange.Contains(other.gameObject))
        ////{
        ////    objectsInRange.Remove(other.gameObject);
        ////}

        //if (other.gameObject.TryGetComponent<IAmInTheWay>(out IAmInTheWay inTheWay))
        //{
        //    objectsInRange.Remove(other.gameObject);
        //    inTheWay.ShowSolid();
        //}

    }
    //private void OnCollisionExit(Collision collision)
    //{

    //}

    void HandleTransparency()
    {
        //if (objectsInRange.Count > 0)
        //{
        //    IAmInTheWay[] inTheWays = GameObject.FindObjectsOfType<IAmInTheWay>();
        //    foreach (IAmInTheWay i in inTheWays)
        //    {
        //        i.ShowSolid();
        //    }

        //} 

        // Defences
        foreach (GameObject go in defences)
        {
            Transform[] allChildren = go.GetComponentsInChildren<Transform>();

            foreach(Transform child in allChildren)
            {
                if (child.rotation.y != 0)
                {
                    child.GetComponent<IAmInTheWay>().ShowTransparent();
                }
            }
        }

    }

}



//void TransparentNearCamera()
//{

//float distance = Vector3.Magnitude( camera.position -planet.position);

//RaycastHit[] hits =  Physics.SphereCastAll(
//    transform.position, 
//    transDistance, 
//    Vector3.zero);

//print("hits length " + hits.Length);

//if (hits.Length > 0)
//{
//    IAmInTheWay[] inTheWays = GameObject.FindObjectsOfType<IAmInTheWay>();
//    foreach (IAmInTheWay i in inTheWays)
//    {
//        i.ShowSolid();
//    }
//    foreach (var hit in hits)
//    {
//        if (hit.collider.gameObject.TryGetComponent(out IAmInTheWay inTheWay))
//        {
//            inTheWay.ShowTransparent();
//        }
//    }
//}
//}

//private void OnDrawGizmos()
//{
//    Gizmos.DrawWireSphere(transform.position, transDistance);
//}