using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandCentr : MonoBehaviour
{
    [SerializeField] private ScanerResource scanerResource;
    [SerializeField] private Dron[] drons;
    [SerializeField] private Counter _counter;
    [SerializeField] private Transform _spawnPosition;

    private const KeyCode scanDron = KeyCode.Space;
    private Queue<Dron> dronsQueue = new Queue<Dron>();
    private Queue<Resource> resourcesQueue = new Queue<Resource>();
    private Transform target;
    private int _updateCount = 3;

    private void Awake()
    {
        for(int i = 0; i < drons.Length; i++)
        {
            dronsQueue.Enqueue(drons[i]);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(scanDron))
        {
            resourcesQueue = scanerResource.Scan(resourcesQueue);
        }
        if (dronsQueue.Count > 0)
        {
            target = resourcesQueue.Count > 0 ? resourcesQueue.Dequeue().transform : null;
            if (target != null)
            {
                SendDrone(dronsQueue.Dequeue(), target);
            }
            if (_counter.GetCount() == _updateCount)
            {
                _counter.Minus();
                SpawnDron();
            }
        }
    }

    private void SendDrone(Dron dron,  Transform resourse)
    {
        dron.TakeCommand(resourse);
    }

    public void AddDron(Dron dron)
    {
        dronsQueue.Enqueue(dron);
    }
    public void AddCount()
    {
        _counter.CountersAdd();
    }

    public void SpawnDron()
    {
        Dron dron = Instantiate(drons[Random.Range(0, drons.Length)], _spawnPosition.position, Quaternion.identity);
        dronsQueue.Enqueue(dron);
    }
}
