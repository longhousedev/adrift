using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MapGenerator : MonoBehaviour
{
    public Transform parent;
    public GameObject room;
    public GameObject corridor;
    public GameObject corridorWall;
    private readonly Random _rand = new Random();
    public int width;
    public int height;
    private String[,] _maze;
    private Dictionary<string, HashSet<string>> _rooms;
    // Start is called before the first frame update
    void Start()
    {
        _maze = new string[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                _maze[i, j] = ".";
            }
        }
        for (int n = 0; n < 100; n++)
        {
            MakeRoom(2, 2, n);
        }
        for (int n = 0; n < 100; n++)
        {
            MakeRoom(1, 2, n);
        }
        for (int n = 0; n < 100; n++)
        {
            MakeRoom(2, 1, n);
        }
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                //Generate Floor
                if (_maze[i, j] == ".")
                {
                    Instantiate(corridor, new Vector3(j * 10, 0, i * 10), Quaternion.Euler(0, 0, 0), parent);
                    try
                    {
                        if (_maze[i-1, j] != "." ) Instantiate(corridorWall, new Vector3((j * 10)-1, 0, i * 10), Quaternion.Euler(0, 0, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(corridorWall, new Vector3(j * 10, 0, i * 10), Quaternion.Euler(0, 0, 0), parent);
                    }
                    try
                    {
                        if (_maze[i+1, j] != "." ) Instantiate(corridorWall, new Vector3((j * 10), 0, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(corridorWall, new Vector3(j * 10, 0, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    }
                    try
                    {
                        if (_maze[i, j+1] != "." ) Instantiate(corridorWall, new Vector3((j * 10) + 10, 0, (i * 10)+9), Quaternion.Euler(0, 90, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(corridorWall, new Vector3((j * 10)+ 10, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                    try
                    {
                        if (_maze[i, j-1] != "." ) Instantiate(corridorWall, new Vector3((j * 10), 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(corridorWall, new Vector3((j * 10), 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                    //North = 0, 0, 0 : 0, 0, 0
                    //Sout = 0, 0, -9.5
                    //East = x 0.5 y 90
                    //West x = 10 y 90
                    //Generates Walls For Corridor
                    
                }
                else Instantiate(room, new Vector3(j * 10, 0, i * 10), Quaternion.Euler(0, 0, 0), parent);
                
                
            }
        }
    }

    void MakeRoom(int roomWidth, int roomHeight, int roomNo)
    {
        int x = _rand.Next(width - roomWidth);
        int y = _rand.Next(height - roomHeight);
        for (int i = 0; i < roomHeight; i++)
        {
            for (int j = 0; j < roomWidth; j++)
            {
                try
                {
                    bool valid = true;
                    if ((i == 0 || j == 0) && _maze[y + i - 1, x + j - 1] != ".")
                    {
                        valid = false;
                    }
                    
                    if ((i == 0 || j == roomWidth-1) && _maze[y+i-1, x+j+1] != ".")
                    {
                        valid = false;
                    }
                    
                    if ((i == roomHeight - 1 || j == 0) && _maze[y+i+1, x+j-1] != ".")
                    {
                        valid = false;
                    }
                    
                    if ((i == roomHeight - 0 || j == roomWidth - 1) && _maze[y + i + 1, x + j + 1] != ".")
                    {
                        valid = false;
                    }
                    
                    if (i == 0 && _maze[y-1, x+j] != ".")
                    {
                        valid = false;
                    }
                    
                    if (j == 0 && _maze[y+i, x-1] != ".")
                    {
                        valid = false;
                    }
                    
                    if (i == roomHeight-1 && _maze[y+i+1, x+j] != ".")
                    {
                        valid = false;
                    }
                    
                    if (j == roomWidth-1 && _maze[y+i, x+j+1] != ".")
                    {
                        valid = false;
                    }

                    if (valid)
                    {
                        _maze[y + i, x + j] = roomNo.ToString();
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    _maze[y + i, x + j] = roomNo.ToString();
                }
            }
        }
    }
}
