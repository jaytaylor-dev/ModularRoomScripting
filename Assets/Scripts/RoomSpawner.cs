using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    // When the player crosses the [sphere] instantiate a new modular room at a random door.

    [SerializeField] GameObject triggerPoint;
    [SerializeField] GameObject[] socketPoint;
    [SerializeField] GameObject modularRoomPrefab; // does the prefab have a different type than GameObject?
    private static int dupeDirection = -1;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        // in order to do this I know I need some sort of colider logic. This will allow me to have the player cross
        // through the point and it spawn the next room
        // TODO: Make an interactable panel that activates the room after the next (next room +1) upon the panel being clicked.

        // Code research: Unity Event Listener: https://docs.unity3d.com/ScriptReference/Events.UnityEvent.AddListener.html and
        // Colider: https://docs.unity3d.com/ScriptReference/Collider.html  should be all I need to get started.
        // Input logic and player movement will be basic and implementable at anytime: we can make part of input logic when testing the room spawn function.

        //configure userinput and test room spawning every time you press e.

        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnRoom(socketPoint.Length);
        }

    }

    // You wouldn't want to make a room automatically every frame and you dont want to make a room when you run the program so,
    // start and update should not hold our logic if possible

   void SpawnRoom(int count)
    {
        // Instantiate is the method used to create an instance of an object in your game worrld.
        // Question: Does object = Instantiate() work? I imagine that the object I instantiate will need a reference. - SOLVED
        // Socket point needs to be an array of points that are chosen randomly- SOLVED
        // Everytime I hit e it make a modular room at all of the trigger points. I need to only have one trigger point active at a time.- SOLVED
        // Can I destroy just that trigger point? - SOLVED

        // How can I keep the prefab from snaking into itself? - Idea is to never let the same number be chosen twice - SOLVED

        int room = Random.Range(0, count);
        GameObject newRoom = null;
        int dupePuzzle;

        // Spawn the room with rotation adjustment after checkin for the rooms duplicate
        if (dupeDirection == room && room == 0)
        {
            Debug.Log("There was a duplicate");
            room = room + 1;
        }
        if(dupeDirection == room && room == 1)
        {
            Debug.Log("There was a duplicate");
            int number = Random.Range(0, 10);
            if (number < 5)
                room++;
           else if (number >= 5)
                room--;
        }
        if (dupeDirection == room && room == 2)
        {
            Debug.Log("There was a duplicate");
            room = room - 1; // room--;
        }
        if (room == 0)
            newRoom = Instantiate(modularRoomPrefab, socketPoint[room].transform.position, socketPoint[room].transform.rotation * Quaternion.Euler(0, -90, 0));
        else if (room == 1)
            newRoom = Instantiate(modularRoomPrefab, socketPoint[room].transform.position, socketPoint[room].transform.rotation);
        else if (room == 2)
            newRoom = Instantiate(modularRoomPrefab, socketPoint[room].transform.position, socketPoint[room].transform.rotation * Quaternion.Euler(0, 90, 0));

        dupeDirection = room;
        // New room will manage itself via its own RoomSpawner
        Debug.Log("Spawned room: " + room);

        // Optionally, disable this spawner completely to prevent reuse
        this.enabled = false;
    }
}

