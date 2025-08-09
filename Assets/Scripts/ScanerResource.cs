using System.Collections.Generic;
using UnityEngine;

public class ScanerResource : MonoBehaviour
{
    private float _scanRadius = 45;
    private float _scanDistance;

    public Queue<Resource> Scan(Queue<Resource> resourcesQueue)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);
        for (int i = 0; i< hits.Length; i++)
        {
            if (hits[i].TryGetComponent(out Resource resource))
            {
                if (!resource.IsChecked())
                {
                    resourcesQueue.Enqueue(resource);
                    resource.TacResource();
                }
            }
        }
        Debug.Log("+");
        return resourcesQueue;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }

}
