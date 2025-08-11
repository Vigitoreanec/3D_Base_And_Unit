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
    private const int _updateCountDron = 3;
    private const int _updateCountBase = 5;
    private CommandCentr _tempCenter;
    private bool _isCreate;
    //private const int _maxCountDron = 5;

    private void Awake()
    {

        for (int i = 0; i < drons.Length; i++)
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
            
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && _counter.GetCount() >= _updateCountBase)
        {
            _counter.MinusByBase();
            SpawnBase();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1) && _counter.GetCount() >= _updateCountDron)
        {
            _counter.MinusByDron();
            SpawnDron();
        }

    }

    private Counter GetCounter(Counter counter)
    {
        counter.gameObject.GetComponentInParent<Counter>();
        counter.transform.position = new Vector3(counter.transform.position.x, 
                                                counter.transform.position.y - 50f, 
                                                    counter.transform.position.z);
        return counter;
    }
    private void SendDrone(Dron dron,  Transform resourse)
    {
        dron.TakeCommand(resourse);
    }

    private void SpawnBase()
    {
        if (_tempCenter == null)
        {
            _tempCenter = this;         
            _isCreate = true;
        }
        else
        {
            _isCreate = false;
        }
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

    public void ClickPosition(Vector3 vector)
    {
        vector = new Vector3(vector.x, 0.5f, vector.z);
        Instantiate(_tempCenter, vector, Quaternion.identity);
    }
    
    public bool IsCreateBase()
    {
        return _isCreate;
    }

}
