using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public Texture2D map;
    public Tile[] colorMappings;
    public CollisionType[,] tileCollidables;

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel() {
        
        for (int x=0; x<map.width;x++) {
            tileCollidables = new CollisionType[map.width, map.height];
            for (int y=0; y<map.height ;y++) {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y) {
        Color pixelColor = map.GetPixel(x, y);
        foreach(Tile colorMapping in colorMappings){
            if (colorMapping.color.Equals(pixelColor)) {
                Vector2 position = new Vector2(x,y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
                tileCollidables[x, y] = colorMapping.collisionType;
            } else {
                tileCollidables[x, y] = CollisionType.None;
            }
        }
    }
}
