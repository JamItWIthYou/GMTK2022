using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public CollisionType[,] tileCollidables;

    public GameObject[,] tileMap;

    private GameController gameController;

    void Start()
    {
        gameController = GameController.getGameController();
        GenerateLevel();
    }

    void GenerateLevel() {
        tileCollidables = new CollisionType[map.width, map.height];
        tileMap = new GameObject[map.width, map.height];
        for (int x=0; x<map.width;x++) {
            for (int y=0; y<map.height ;y++) {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y) {
        Color pixelColor = map.GetPixel(x, y);
        foreach(ColorToPrefab colorMapping in colorMappings){
            if (colorMapping.color.Equals(pixelColor)) {
                Vector2 position = new Vector2(x,y);
                tileMap[x,y] = Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
                tileCollidables[x, y] = colorMapping.collisionType;
                return;
            }
        }
        tileCollidables[x,y] = CollisionType.None;
    }
}
