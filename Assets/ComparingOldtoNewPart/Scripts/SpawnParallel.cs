using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;
using UnityEngine.EventSystems;

public class SpawnParellel : MonoBehaviour
{
    public GameObject sheepPrefab;
  
    const int numSheep = 10000;
    private Transform[] AllSheepTransform;

    struct MoveJob : IJobParallelForTransform
    {
        void IJobParallelForTransform.Execute(int index, TransformAccess transform)
        {
            transform.position += 0.1f * (transform.rotation * new Vector3(0, 0, 1));
            if (transform.position.z > 50)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -50);
            }

        }
    }

    MoveJob moveJob;
    JobHandle moveHandle;
    TransformAccessArray transforms;

    // Start is called before the first frame update
    void Start()
    {
        AllSheepTransform = new Transform[numSheep];
        for (int i = 0; i < numSheep; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            GameObject sheep = Instantiate(sheepPrefab , pos, Quaternion.identity);
            AllSheepTransform[i] = sheep.transform;
        }
        transforms = new TransformAccessArray(AllSheepTransform);
    }

    // Update is called once per frame
    void Update()
    {
        moveJob = new MoveJob { };
        moveHandle = moveJob.Schedule(transforms);//Sceduling this Job in each frame and passing the whole array
    }

    private void LateUpdate()
    {
        moveHandle.Complete(); // this will allow job to run it again
    }
    private void OnDestroy()
    {
        transforms.Dispose();
    }
}
