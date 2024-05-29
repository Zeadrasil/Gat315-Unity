using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] short baseSeedX;
    [SerializeField] short baseSeedY;
    [SerializeField] short baseSeedRotationDegrees;
    [SerializeField] short baseSeedRandomSeed;
    [SerializeField] Tile yesTile;
    [SerializeField] Tile noTile;
    [SerializeField] Tilemap tilemap;
    [SerializeField] int scaling = 20000000;
    [SerializeField] int startGeneration = 200;
    [SerializeField] float swapPoint = 0.45f;
    private int resultSeedX;
    private int resultSeedY;
    private int resultSeedRotationDegrees;
    public void generateAt(int x, int y)
    {
        //actual placement gets altered by the displacement
        float newX = x + resultSeedX;
        float newY = y + resultSeedY;

        //scaling changes how big all of the patches are
        newX *= scaling;
        newY *= scaling;

        //ensures that it is a number between 0 and 1
        newX /= int.MaxValue * 0.5f + 0.5f;
        newY /= int.MaxValue * 0.5f + 0.5f;

        //turns it into a Vector so that rotation can be applied
        Vector2 at = new Vector2(newX, newY);

        //rotates the Vector
        at = Quaternion.AngleAxis((float)resultSeedRotationDegrees / 360, new Vector3(0, 0, 1)) * at;

        //sets the tile as path if it meets path criteria, not path if it does not
        tilemap.SetTile(new Vector3Int(x, y, 0), Mathf.PerlinNoise(at.x, at.y) > swapPoint ? yesTile : noTile);
    }
    private void Start()
    {
        //seeds the rng so that the exact details of the tile generation are always the same
        System.Random rand = new System.Random(baseSeedRandomSeed + baseSeedX + baseSeedY + baseSeedRotationDegrees);

        //creates Vector to hold displacement so it can be rotated later
        Vector2 displacement = new Vector2Int();

        //multiplies the displacement by a random number between 0 and 10
        displacement.x = (short)(rand.NextDouble() * 10 * baseSeedX);
        displacement.y = (short)(rand.NextDouble() * 10 * baseSeedY);

        //multiplies the rotation by a random number between 0 and 10
        resultSeedRotationDegrees = (short)(rand.NextDouble() * baseSeedRotationDegrees * 10);

        //rotates the displacement by the randomized rotation value
        displacement = Quaternion.AngleAxis((float)resultSeedRotationDegrees / 360, new Vector3(0, 0, 1)) * displacement;
        
        //sets the final displacement values
        resultSeedX = (int)displacement.x;
        resultSeedY = (int)displacement.y;
        
        //Generates some tiles for use as an example map
        for (int x = -startGeneration; x <= startGeneration;  x++)
        {
            for(int y = -startGeneration; y <= startGeneration; y++)
            {
                generateAt(x, y);
            }
        }
    }
}
