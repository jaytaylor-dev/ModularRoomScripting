using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] GameObject triggerPoint;
    [SerializeField] GameObject[] socketPoint;
    [SerializeField] GameObject modularRoomPrefab;

    // Shared across all RoomSpawner instances to prevent immediate repetition
    private static int dupeDirection = -1;

    // no start function necessary yet

    void Update()
    {
        // Press E to trigger room spawning (for testing)
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnRoom(socketPoint.Length);
        }
    }

    /// <summary>
    /// Instantiates a new room at a randomly selected socket point,
    /// ensuring that the same room isn't selected twice in a row.
    /// </summary>
    /// <param name="count">Number of available socket points</param>
    void SpawnRoom(int count)
    {
        int room = Random.Range(0, count);
        GameObject newRoom = null;

        // Check for immediate repetition and adjust if necessary
        if (dupeDirection == room && room == 0)
        {
            Debug.Log("Duplicate detected (room 0), shifting to 1.");
            room++;
        }
        else if (dupeDirection == room && room == 1)
        {
            Debug.Log("Duplicate detected (room 1), randomly shifting to 0 or 2.");
            int number = Random.Range(0, 10);
            room += (number < 5) ? 1 : -1;
        }
        else if (dupeDirection == room && room == 2)
        {
            Debug.Log("Duplicate detected (room 2), shifting to 1.");
            room--;
        }

        // Instantiate the room with a custom rotation adjustment
        if (room == 0)
            newRoom = Instantiate(modularRoomPrefab, socketPoint[room].transform.position, socketPoint[room].transform.rotation * Quaternion.Euler(0, -90, 0));
        else if (room == 1)
            newRoom = Instantiate(modularRoomPrefab, socketPoint[room].transform.position, socketPoint[room].transform.rotation);
        else if (room == 2)
            newRoom = Instantiate(modularRoomPrefab, socketPoint[room].transform.position, socketPoint[room].transform.rotation * Quaternion.Euler(0, 90, 0));

        dupeDirection = room;

        Debug.Log("Spawned room: " + room);

        // Disable this spawner to prevent it from spawning multiple times
        this.enabled = false;
    }
}
