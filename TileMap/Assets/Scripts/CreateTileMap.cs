using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateTileMap : MonoBehaviour
{
    public Tilemap targetTileMap;
    public TileBase oldTile;
    public TileBase newTile;

    private void Start()
    {
        //只填充一个Grid，可覆盖
        //targetTileMap.SetTile(new Vector3Int(-2, 2, 0), tile);

        //动态清除一个Grid
        //targetTileMap.SetTile(new Vector3Int(-2, 2, 0), null);

        //动态填充一块矩形区域，参数分别是：起笔位置，需要绘制的Tile，矩形参数
        //注意：起笔位置必须在矩形的参数范围内，不能通过此方法批量清除Grid，不可覆盖
        //targetTileMap.BoxFill(Vector3Int.zero, null, -2, -2, 2, 2);

        //使用BoxFill来达到只填充一个Grid的目的
        //targetTileMap.BoxFill(new Vector3Int(1, 2, 0), tile, 1, 2, 1, 2);


        //将指定TileMap中的oldTile替换成newTile
        
        

        //根据给定边界批量获得Grid中的Tile
        //TileBase[] tileBase = targetTileMap.GetTilesBlock(new BoundsInt(-3, -3, 0, 3, 3, 1));
        //foreach (var item in tileBase)
        //{
        //    Debug.Log(item);
        //}

        //根据给定边界和Tile数组批量填充Grid
        //targetTileMap.SetTilesBlock(new BoundsInt(0, 0, 0, 3, 3, 1), tileBase);

        //动态填充一块区域，采用油漆桶模式，参数分别是：起笔位置，需要绘制的Tile，填充范围自动识别
        //注意：可以通过此方法批量清除Grid，清空的范围自动识别
        //targetTileMap.FloodFill(new Vector3Int(-1, -1, 0), null);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetTileMap.SwapTile(oldTile, newTile);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            targetTileMap.SwapTile(newTile, oldTile);
        }
    }
}
