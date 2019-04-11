using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockScript : MonoBehaviour
{

    [SerializeField] private Transform endPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endPoint.position, 0.9f);
        /*
        foreach (Vector3 temp in tracksPositions)
        {
            Vector3 v = new Vector3(temp.x, 0, temp.z);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + v, transform.position + v + Vector3.up);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position + v + Vector3.up, 0.1f);
            Gizmos.color = Color.red;
            
            Vector3 pivot = transform.position + v + Vector3.up;
            Vector3 point = transform.position + v + Vector3.up + Vector3.forward * 0.2f;
            Vector3 newDirection = Quaternion.Euler(new Vector3(0f, temp.y, 0f)) * (point - pivot) + pivot;

            Gizmos.DrawLine(pivot, newDirection);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(newDirection, 0.05f);
        }
        */
    }

    public Vector3 GetStartPoint()
    {
        return transform.position;
    }

    public Vector3 GetEndPoint()
    {
        return endPoint.position;
    }
}
