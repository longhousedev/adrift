using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLists : MonoBehaviour
{
    //A collection of arrays storing possible spawns for each room
    
    //North Hallways
    public GameObject[] hallwayNorth;
    public GameObject[] hallwayNorthEntrance;
    
    
    //South Hallways
    public GameObject[] hallwaySouth;
    public GameObject[] hallwaySouthEntrance;

    
    //East Hallways
    public GameObject[] hallwayEast;
    public GameObject[] hallwayEastEntrance;

    
    //West Hallways
    public GameObject[] hallwayWest;
    public GameObject[] hallwayWestEntrance;

    
    //North to East Connectors
    public GameObject[] northToEast;
    public GameObject[] northToEastEntrance;

    
    //North to West Connectors
    public GameObject[] northToWest;
    public GameObject[] northToWestEntrance;

    
    //South to East Connectors
    public GameObject[] southToEast;
    public GameObject[] southToEastEntrance;

    
    //South to West Connectors
    public GameObject[] southToWest;
    public GameObject[] southToWestEntrance;
    
    
    //Spawn Room
    public GameObject[] spawnRoomNorth;
    public GameObject[] spawnRoomEast;
    public GameObject[] spawnRoomWest;
    
}
