using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneratorSeed : MonoBehaviour
{
    public Transform forward;
    public Transform back;
    public Transform left;
    public Transform right;
    public Transform up;
    public Transform down;

    public GameObject[] prefabs;

    GameObject gameObjectReference;

    Transform gameObjectReferenceTransform;

    RoomGeneratorSeed gameObjectReferenceRoomGeneratorSeed;

    float timer = 1.0f;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer += 1.0f;
            SpawnInDirection((int)Random.Range(0.5f, 6.5f));
        }
    }

    void SpawnInDirection(int directionIndex)
    {
        switch (directionIndex)
        {
            case 1:
                gameObjectReference = Instantiate(prefabs[(int)Random.Range(0.5f, (float)prefabs.Length + 0.5f)], forward.position, Quaternion.identity);
                gameObjectReferenceTransform = gameObjectReference.transform;
                gameObjectReferenceRoomGeneratorSeed = gameObjectReference.GetComponent<RoomGeneratorSeed>();
                gameObjectReferenceTransform.position -= gameObjectReferenceRoomGeneratorSeed.back.position - this.forward.position;
                break;
            case 2:
                gameObjectReference = Instantiate(prefabs[(int)Random.Range(0.5f, (float)prefabs.Length + 0.5f)], right.position, Quaternion.identity);
                gameObjectReferenceTransform = gameObjectReference.transform;
                gameObjectReferenceRoomGeneratorSeed = gameObjectReference.GetComponent<RoomGeneratorSeed>();
                gameObjectReferenceTransform.position -= gameObjectReferenceRoomGeneratorSeed.left.position - this.right.position;
                break;
            case 3:
                gameObjectReference = Instantiate(prefabs[(int)Random.Range(0.5f, (float)prefabs.Length + 0.5f)], up.position, Quaternion.identity);
                gameObjectReferenceTransform = gameObjectReference.transform;
                gameObjectReferenceRoomGeneratorSeed = gameObjectReference.GetComponent<RoomGeneratorSeed>();
                gameObjectReferenceTransform.position -= gameObjectReferenceRoomGeneratorSeed.down.position - this.up.position;
                break;
            case 4:
                gameObjectReference = Instantiate(prefabs[(int)Random.Range(0.5f, (float)prefabs.Length + 0.5f)], back.position, Quaternion.identity);
                gameObjectReferenceTransform = gameObjectReference.transform;
                gameObjectReferenceRoomGeneratorSeed = gameObjectReference.GetComponent<RoomGeneratorSeed>();
                gameObjectReferenceTransform.position -= gameObjectReferenceRoomGeneratorSeed.forward.position - this.back.position;
                break;
            case 5:
                gameObjectReference = Instantiate(prefabs[(int)Random.Range(0.5f, (float)prefabs.Length + 0.5f)], left.position, Quaternion.identity);
                gameObjectReferenceTransform = gameObjectReference.transform;
                gameObjectReferenceRoomGeneratorSeed = gameObjectReference.GetComponent<RoomGeneratorSeed>();
                gameObjectReferenceTransform.position -= gameObjectReferenceRoomGeneratorSeed.right.position - this.left.position;
                break;
            case 6:
                gameObjectReference = Instantiate(prefabs[(int)Random.Range(0.5f, (float)prefabs.Length + 0.5f)], down.position, Quaternion.identity);
                gameObjectReferenceTransform = gameObjectReference.transform;
                gameObjectReferenceRoomGeneratorSeed = gameObjectReference.GetComponent<RoomGeneratorSeed>();
                gameObjectReferenceTransform.position -= gameObjectReferenceRoomGeneratorSeed.up.position - this.down.position;
                break;
        }
    }
}
