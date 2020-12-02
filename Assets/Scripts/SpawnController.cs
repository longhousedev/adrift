using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    private RoomLists rooms;
    private int rand;
    private bool spawned;
    private Transform parent;
    
    // Start is called before the first frame update
    void Start()
    {
        //Gets the components required
        rooms = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomLists>();
        parent = GameObject.FindGameObjectWithTag("GeometryParent").transform;
        //Starts the spawn function with a slight delay to allow for conflict detection
        Invoke("Spawn", 0.1f);
    }

    // Update is called once per frame
    void Spawn()
    {
        //Validation to prevent double spawning
        if (spawned) return;
        //Will find the array for the prefab and instantiate one of the possible rooms at the spawn position
        switch (gameObject.name)
        {
            case "Hallway North":
                rand = Random.Range(0, rooms.hallwayNorth.Length);
                Instantiate(rooms.hallwayNorth[rand], transform.position, rooms.hallwayNorth[rand].transform.rotation, parent);
                break;
            case "Hallway North Entrance":
                rand = Random.Range(0, rooms.hallwayNorthEntrance.Length);
                Instantiate(rooms.hallwayNorthEntrance[rand], transform.position, rooms.hallwayNorthEntrance[rand].transform.rotation,  parent);
                break;
            case "Hallway South":
                rand = Random.Range(0, rooms.hallwaySouth.Length);
                Instantiate(rooms.hallwaySouth[rand], transform.position, rooms.hallwaySouth[rand].transform.rotation,  parent);
                break;
            case "Hallway South Entrance":
                rand = Random.Range(0, rooms.hallwaySouthEntrance.Length);
                Instantiate(rooms.hallwaySouthEntrance[rand], transform.position, rooms.hallwaySouthEntrance[rand].transform.rotation,  parent);
                break;
            case "Hallway East":
                rand = Random.Range(0, rooms.hallwayEast.Length);
                Instantiate(rooms.hallwayEast[rand], transform.position, rooms.hallwayEast[rand].transform.rotation,  parent);
                break;
            case "Hallway East Entrance":
                rand = Random.Range(0, rooms.hallwayEastEntrance.Length);
                Instantiate(rooms.hallwayEastEntrance[rand], transform.position, rooms.hallwayEastEntrance[rand].transform.rotation,  parent);
                break;
            case "Hallway West":
                rand = Random.Range(0, rooms.hallwayWest.Length);
                Instantiate(rooms.hallwayWest[rand], transform.position, rooms.hallwayWest[rand].transform.rotation,  parent);
                break;
            case "Hallway West Entrance":
                rand = Random.Range(0, rooms.hallwayWestEntrance.Length);
                Instantiate(rooms.hallwayWestEntrance[rand], transform.position, rooms.hallwayWestEntrance[rand].transform.rotation,  parent);
                break;
            case "North to East":
                rand = Random.Range(0, rooms.northToEast.Length);
                Instantiate(rooms.northToEast[rand], transform.position, rooms.northToEastEntrance[rand].transform.rotation,  parent);
                break;
            case "North to East Entrance":
                rand = Random.Range(0, rooms.northToEastEntrance.Length);
                Instantiate(rooms.northToEastEntrance[rand], transform.position, rooms.northToEastEntrance[rand].transform.rotation,  parent);
                break;
            case "North to West":
                rand = Random.Range(0, rooms.northToWest.Length);
                Instantiate(rooms.northToWest[rand], transform.position, rooms.northToWest[rand].transform.rotation,  parent);
                break;
            case "North to West Entrance":
                rand = Random.Range(0, rooms.northToWestEntrance.Length);
                Instantiate(rooms.northToWestEntrance[rand], transform.position, rooms.northToWestEntrance[rand].transform.rotation,  parent);
                break;
            case "South to East":
                rand = Random.Range(0, rooms.southToEast.Length);
                Instantiate(rooms.southToEast[rand], transform.position, rooms.southToEast[rand].transform.rotation,  parent);
                break;
            case "South to East Entrance":
                rand = Random.Range(0, rooms.southToEastEntrance.Length);
                Instantiate(rooms.southToEastEntrance[rand], transform.position, rooms.southToEastEntrance[rand].transform.rotation,  parent);
                break;
            case "South to West":
                rand = Random.Range(0, rooms.southToWest.Length);
                Instantiate(rooms.southToWest[rand], transform.position, rooms.southToWest[rand].transform.rotation,  parent);
                break;
            case "South to West Entrance":
                rand = Random.Range(0, rooms.southToWestEntrance.Length);
                Instantiate(rooms.southToWestEntrance[rand], transform.position, rooms.southToWestEntrance[rand].transform.rotation,  parent);
                break;
            case "Spawn Room North":
                rand = Random.Range(0, rooms.spawnRoomNorth.Length);
                Instantiate(rooms.spawnRoomNorth[rand], transform.position, rooms.spawnRoomNorth[rand].transform.rotation,  parent);
                break;
            case "Spawn Room East":
                rand = Random.Range(0, rooms.spawnRoomEast.Length);
                Instantiate(rooms.spawnRoomEast[rand], transform.position, rooms.spawnRoomEast[rand].transform.rotation,  parent);
                break;
            case "Spawn Room West":
                rand = Random.Range(0, rooms.spawnRoomWest.Length);
                Instantiate(rooms.spawnRoomWest[rand], transform.position, rooms.spawnRoomWest[rand].transform.rotation,  parent);
                break;
        }
        spawned = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Will destroy this spawner if a room has already spawned at its destination
        if(other.CompareTag("Spawn Point") && other.GetComponent<SpawnController>().spawned == true) Destroy(gameObject);
    }
}
