
using UnityEngine;

public class TileGenerator : MonoBehaviour {
    [SerializeField]
    GameObject tilePrefab;
    Tile[] tiles;
    [SerializeField]
    int size;
	// Use this for initialization
	void Start () {
        tiles = new Tile[size * size];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = GenerateTile(i);
        }
        //Initialize after all caves have been generated
        GameObject.FindObjectOfType<EnemyManager>().Initialize();
	}

    private Tile GenerateTile(int i)
    {
        Tile tile = GameObject.Instantiate(tilePrefab)
            .GetComponent<Tile>();
        tile.transform.SetParent(transform, false);
        tile.x = i % size;
        tile.y = i / size;
        tile.transform.position = new Vector3(tile.x * 150f, tile.y * 150f);
        tile.Initialize();
        return tile;
    }

    public Vector3 ChooseRandomLocation()
    {
        int i = Random.Range(0, tiles.Length);
        var x = (i % size) * 150f;
        var y = (i / size) * 150f;
        var ret = tiles[i].GetRandomLocation();
        ret.x += x;
        ret.y += y;

        return ret;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
