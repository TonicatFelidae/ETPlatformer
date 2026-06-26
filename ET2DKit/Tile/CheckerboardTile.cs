using UnityEngine;
using UnityEngine.Tilemaps;
using ET.SupportKit;

// Tile that repeats two sprites in checkerboard pattern
namespace ET.Tilemapkit
{
    [CreateAssetMenu(fileName = "CheckerboardTile", menuName = "ET/Tiles/CheckerboardTile")]
    public class CheckerboardTile : TileBase
    {
        public Sprite spriteA;
        public Sprite spriteB;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            bool evenCell = Mathf.Abs(position.y + position.x) % 2 > 0;
            tileData.sprite = evenCell ? spriteA : spriteB;
        }
    }
}
