using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class RoomManager : Singleton<RoomManager>
{
    public Transform buildingTransform;
    public GameObject roomPrefab;
    public GameObject finalRoomPrefab;
    public List<Room> roomsList;
    public List<Room> presetRooms;
    public float roomHeight;
    public int roomsToGenerate = 50;
    public int roofNumber = 30;
    private float roofHeight;
    public AnimationCurve enemyCountRamp;
    public int maxHostiles;

    public static event Action OnRoomsGenerated;

    public Room finalRoom;

    private void Start()
    {
        GenerateRooms();
    }

    private void GenerateRooms()
    {
        roofHeight = presetRooms[0].transform.position.y;
        Room previousRoom = null;
        //Add preset rooms to roomsList
        for (int roomNumber = 0; roomNumber < presetRooms.Count; roomNumber++)
        {
            Room presetRoom = presetRooms[roomNumber];
            //Save previous room
            if(previousRoom)
                presetRoom.previousRoom = previousRoom;
            //Update previous room
            previousRoom = presetRoom;
            roomsList.Add(presetRoom);

            presetRoom.PopulateRoom(roofNumber - roomNumber, enemyCountRamp, roofNumber, maxHostiles);
        }

        float roomYPosition = 0;
        //Generate up to roomsToGenerate number of rooms
        for (int roomNumber = roomsList.Count + 1; roomNumber < roomsToGenerate; roomNumber++)
        {
            //Create new room
            Room newRoom = Instantiate(roomPrefab, buildingTransform).GetComponent<Room>();

            //Save previous room
            newRoom.previousRoom = previousRoom;
            //Update previous room
            previousRoom = newRoom;

            //Calculate & Set the height of the room
            roomYPosition = roofHeight - roomHeight * roomsList.Count;
            newRoom.transform.position = new Vector3(0, roomYPosition, 0);

            //Save reference
            roomsList.Add(newRoom);

            newRoom.PopulateRoom(roofNumber - roomNumber, enemyCountRamp, roofNumber, maxHostiles);
        }
        //Generate Final Room
        finalRoom = Instantiate(finalRoomPrefab, buildingTransform).GetComponent<Room>();
        finalRoom.previousRoom = previousRoom;
        previousRoom = finalRoom;
        roomsList.Add(finalRoom);
        //Calculate & Set the height of the room
        roomYPosition = roofHeight - roomHeight * roomsList.Count;
        
        finalRoom.transform.position = new Vector3(0, roomYPosition, 0);

        //Call room generation complete 2 seconds after 
        Invoke("RoomsGenerated", 2);
    }

    void RoomsGenerated()
    {
        OnRoomsGenerated?.Invoke();
    }
}
