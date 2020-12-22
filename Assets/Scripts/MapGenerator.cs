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
    public GameObject roomWall;
    public GameObject door;
    public GameObject corridorCeiling;
    public GameObject roomCeiling;
    private readonly Random _rand = new Random();
    public int width;
    public int height;
    private String[,] _maze;
    private Dictionary<string, List<Coords>> _rooms;
    public int camSpawn;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        _maze = new string[height, width];
        _rooms = new Dictionary<string, List<Coords>>{};
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                _maze[i, j] = ".";
            }
        }
        for (int n = 0; n < 30; n++)
        {
            MakeRoom(2, 2, n);
        }
        //for (int n = 20; n < 40; n++)
        //{
        //    MakeRoom(3, 2, n);
        //}
        //for (int n = 40; n < 60; n++)
        //{
        //    MakeRoom(2, 2, n);
        //}
        // for (int n = 200; n < 300; n++)
        // {
        //     MakeRoom(1, 2, n);
        // }
        // for (int n = 300; n < 350; n++)
        // {
        //     MakeRoom(2, 1, n);
        // }
        MakeDoors();
        Generate();
        for (int i = 0; i < camSpawn; i++)
        {
            bool valid = false;
            while(!valid)
            {
                int roomNo = _rand.Next(0, _rooms.Count - 1);
                if (_rooms.ContainsKey(roomNo.ToString()))
                {
                    var coords = _rooms[roomNo.ToString()];
                    Coords coord = coords[_rand.Next(0, coords.Count)];
                    Instantiate(camera, new Vector3((coord.x * 10) + 5, 1, (coord.y * 10) + 5), Quaternion.Euler(0, 0, 0));
                    valid = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeDoors()
    {
        foreach (var pair in _rooms)
        {
            bool valid = false;
            int count = 1;
            while (!valid)
            {
                int index = _rand.Next(0, pair.Value.Count);
                Coords coords = pair.Value[index];
                int i = coords.y;
                int j = coords.x;
                if (i < height && j < width)
                {
                    if (_maze[i + 1, j] == ".")
                    {
                        _maze[i, j] = pair.Key + "D";
                        _maze[i + 1, j] = ".D";
                        valid = true;
                    }
                    else if (_maze[i, j + 1] == ".")
                    {
                        _maze[i, j] = pair.Key + "D";
                        _maze[i, j + 1] = ".D";
                        valid = true;
                    }   
                }
                if (i > 1 && j > 1)
                {
                    if (_maze[i - 1, j] == ".")
                    {
                        _maze[i, j] = pair.Key + "D";
                        _maze[i - 1, j] = ".D";
                        valid = true;
                    }
                    else if (_maze[i, j - 1] == ".")
                    {
                        _maze[i, j] = pair.Key + "D";
                        _maze[i, j - 1] = ".D";
                        valid = true;
                    }   
                }
                if (count > 1000) valid = true;
                count++;
            }
        }
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
                    Instantiate(corridorCeiling, new Vector3(j * 10, 5, i * 10), Quaternion.Euler(0, 0, 0), parent);
                    if (i == 0) Instantiate(corridorWall, new Vector3(((j * 10)), 00, (i * 10)+0.5f), Quaternion.Euler(0, 0, 0), parent);
                    if (i == height-1) Instantiate(corridorWall, new Vector3((j * 10), 0, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    if (j == 0) Instantiate(corridorWall, new Vector3((j * 10) + 0.5f, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    if (j == width-1) Instantiate(corridorWall, new Vector3((j * 10) + 10, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                }
                else if (_maze[i, j] == ".D")
                {
                    Instantiate(corridor, new Vector3(j * 10, 0, i * 10), Quaternion.Euler(0, 0, 0), parent);
                    Instantiate(corridorCeiling, new Vector3(j * 10, 5, i * 10), Quaternion.Euler(0, 0, 0), parent);
                    if (i == 0) Instantiate(corridorWall, new Vector3(((j * 10)), 00, (i * 10)+0.5f), Quaternion.Euler(0, 0, 0), parent);
                    if (i == height-1) Instantiate(corridorWall, new Vector3((j * 10), 0, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    if (j == 0) Instantiate(corridorWall, new Vector3((j * 10) + 0.5f, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    if (j == width-1) Instantiate(corridorWall, new Vector3((j * 10) + 10, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);   
                }
                else if (_maze[i, j].Contains("D") && !_maze[i, j].Contains("."))
                {
                    Instantiate(room, new Vector3(j * 10, 0, i * 10), Quaternion.Euler(0, 0, 0), parent);
                    Instantiate(roomCeiling, new Vector3(j * 10, 5, i * 10), Quaternion.Euler(0, 0, 0), parent);
                    if (i == 0) Instantiate(roomWall, new Vector3(((j * 10)), 00, (i * 10)+0.5f), Quaternion.Euler(0, 0, 0), parent);
                    if (i == height-1) Instantiate(roomWall, new Vector3((j * 10), 0, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    if (j == 0) Instantiate(roomWall, new Vector3((j * 10) + 0.5f, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    if (j == width-1) Instantiate(roomWall, new Vector3((j * 10) + 10, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    string roomNo = _maze[i, j].Replace("D", "");
                    
                    // DOORS
                    try // North
                    {
                        if (_maze[i+1, j] == ".D") Instantiate(door, new Vector3(((j * 10)), 0, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(roomWall, new Vector3(((j * 10)), 00, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    }
                    try // South
                    {
                        if (_maze[i-1, j] == ".D") Instantiate(door, new Vector3((j * 10), 0, (i * 10)+0.5f), Quaternion.Euler(0, 0, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(roomWall, new Vector3(j * 10, 0, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    }
                    try // East
                    {
                        if (_maze[i, j+1] == ".D") Instantiate(door, new Vector3((j * 10) + 10, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(roomWall, new Vector3((j * 10)+ 10, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                    try // West
                    {
                        if (_maze[i, j-1] == ".D") Instantiate(door, new Vector3((j * 10) + 0.5f, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(roomWall, new Vector3((j * 10) + 0.5f, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                    
                    // WALLS
                    try // North
                    {
                        if (_maze[i+1, j] != roomNo && !_maze[i+1, j].Contains("D")) Instantiate(roomWall, new Vector3(((j * 10)), 0, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(roomWall, new Vector3(((j * 10)), 00, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    }
                    try // South
                    {
                        if (_maze[i-1, j] != roomNo && !_maze[i-1, j].Contains("D")) Instantiate(roomWall, new Vector3((j * 10), 0, (i * 10)+0.5f), Quaternion.Euler(0, 0, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(roomWall, new Vector3(j * 10, 0, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    }
                    try // East
                    {
                        if (_maze[i, j+1] != roomNo && !_maze[i, j+1].Contains("D")) Instantiate(roomWall, new Vector3((j * 10) + 10, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(roomWall, new Vector3((j * 10)+ 10, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                    try // West
                    {
                        if (_maze[i, j-1] != roomNo && !_maze[i, j-1].Contains("D")) Instantiate(roomWall, new Vector3((j * 10) + 0.5f, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(roomWall, new Vector3((j * 10) + 0.5f, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                }
                else if (!_maze[i, j].Contains(".") && !_maze[i, j].Contains("D"))
                {
                    Instantiate(room, new Vector3(j * 10, 0, i * 10), Quaternion.Euler(0, 0, 0), parent);
                    Instantiate(roomCeiling, new Vector3(j * 10, 5, i * 10), Quaternion.Euler(0, 0, 0), parent);
                    if (i == 0) Instantiate(roomWall, new Vector3(((j * 10)), 00, (i * 10)+0.5f), Quaternion.Euler(0, 0, 0), parent);
                    if (i == height-1) Instantiate(roomWall, new Vector3((j * 10), 0, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    if (j == 0) Instantiate(roomWall, new Vector3((j * 10) + 0.5f, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    if (j == width-1) Instantiate(roomWall, new Vector3((j * 10) + 10, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    try // North
                    {
                        if (!_maze[i+1, j].Contains(_maze[i, j])) Instantiate(roomWall, new Vector3(((j * 10)), 00, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(roomWall, new Vector3(((j * 10)), 00, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    }
                    try // South
                    {
                        if (!_maze[i-1, j].Contains(_maze[i, j])) Instantiate(roomWall, new Vector3((j * 10), 0, (i * 10)+0.5f), Quaternion.Euler(0, 0, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(roomWall, new Vector3(j * 10, 0, (i * 10)+10), Quaternion.Euler(0, 0, 0), parent);
                    }
                    try // East
                    {
                        if (!_maze[i, j+1].Contains(_maze[i, j])) Instantiate(roomWall, new Vector3((j * 10) + 10, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(roomWall, new Vector3((j * 10)+ 10, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                    try // West
                    {
                        if (!_maze[i, j-1].Contains(_maze[i, j])) Instantiate(roomWall, new Vector3((j * 10) + 0.5f, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Instantiate(roomWall, new Vector3((j * 10) + 0.5f, 0, (i * 10)+10), Quaternion.Euler(0, 90, 0), parent);
                    }
                }
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
                        int xc = x + j;
                        int yc = y + i;
                        if (_rooms.ContainsKey(roomNo.ToString()))
                        {
                            _rooms[roomNo.ToString()].Add(new Coords(xc, yc));
                        }
                        else
                        {
                            _rooms.Add(roomNo.ToString(), new List<Coords> {new Coords(xc, yc)});
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    _maze[y + i, x + j] = roomNo.ToString();
                    int xc = x + j;
                    int yc = y + i;
                    if (_rooms.ContainsKey(roomNo.ToString()))
                    {
                        _rooms[roomNo.ToString()].Add(new Coords(xc, yc));
                    }
                    else
                    {
                        _rooms.Add(roomNo.ToString(), new List<Coords> {new Coords(xc, yc)});
                    }
                }
            }
        }
    }
}

internal class Coords
{
    public int x;
    public int y;

    public Coords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
