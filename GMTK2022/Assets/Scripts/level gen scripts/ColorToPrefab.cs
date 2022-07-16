using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
   public Color color;
   public GameObject prefab;
   public CollisionType collisionType;
}

public enum CollisionType {
   Full,
   Half,
   None
}