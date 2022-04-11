using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField] [Range(2, 99)] private int width = 10;
    [SerializeField] [Range(2, 99)] private int height = 10;
    [SerializeField] private float size = 1f;
    [SerializeField] private Transform wallPrefab = null;
    [SerializeField] private GameObject floorGameObject = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject end = null;
    private int seed = new System.Random().Next();


    void Start()
    {
        player.transform.position = new Vector3(width*size*wallPrefab.localScale.x/2-size/2,-height*size*wallPrefab.localScale.y+size/2,0f);
        end.transform.position = new Vector3(-width*size*wallPrefab.localScale.x/2+size/2,height*size*wallPrefab.localScale.y-size/2,0f);
        // TODO: floor moves with the camera
        floorGameObject.transform.localScale = new Vector3(Math.Max(width*0.08f,1), Math.Max(height*0.08f,1), 1); 
        WallState[,] maze = MazeGenerator.Generate(width, height, seed);
        Draw(maze);
    }

    private void Draw(WallState[,] maze)
    {

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                WallState cell = maze[i, j];
                Vector3 position = new Vector3(-width / 2 + i, -height / 2 + j + size/2, 0);

                if (cell.HasFlag(WallState.LEFT))
                {
                    Transform leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    // topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }

                if (cell.HasFlag(WallState.UP))
                {
                    Transform topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, size / 2, 0);
                    // leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    topWall.eulerAngles = new Vector3(0, 0, 90);
                }

                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        Transform rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+size / 2 /*0*/, 0 /*-size / 2*/, 0);
                        // rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        // rightWall.eulerAngles = new Vector3(0, 0, 90);
                    }
                }

                if (j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        Transform bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0/*+size / 2*/, -size / 2 /*0*/, 0);
                        // bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                        bottomWall.eulerAngles = new Vector3(0, 0, 90);
                    }
                }
            }

        }

    }
}
