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

    
    public float transDistance = 10f;

    public List<GameObject> objectsInRange;


    // Start is called before the first frame update
    void Awake()
    {
        //currentlyInTheWay = new List<IAmInTheWay>();
        //alreadyTransparent = new List<IAmInTheWay>();

        objectsInRange = new List<GameObject>();

        camera = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

        //TransparentNearCamera();
        //GetAllObjectsInTheWay();

        //MakeObjectsTransparent();
        //MakeObjectsSolid();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!objectsInRange.Contains(collision.gameObject))
        {


            objectsInRange.Add(collision.gameObject);


            if (collision.gameObject.TryGetComponent<IAmInTheWay>(out IAmInTheWay inTheWay))
            {
                //objectsInRange.Add(collision.gameObject);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        
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