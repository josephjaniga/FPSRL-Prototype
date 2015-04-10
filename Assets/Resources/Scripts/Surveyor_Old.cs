using UnityEngine;
using System.Collections;

/**
 * This class deals with generating Minecraft Styled voxel cubes which I hate...
 */

public class Surveyor_Old : MonoBehaviour {

	// CONCRETE
	public float prefabScaleFactor = 4f;
	public GameObject prefabTileTypeOne;
	public GameObject prefabTileTypeTwo;

	// DATA
	public int maximumMapSize = 55;
	public Vector3 coreSize = new Vector3(2f, 1f, 2f);
	public Vector3 corePosition = new Vector3(0f, 0f, 0f);
	public byte[,,] theGrid;


	// settings
    public GeometryShapes shape = GeometryShapes.Perlin_01;
	public bool randomTile = false;
	public bool tileImperfections = false;

    // position helpers
    Vector3 centerMapOffset = Vector3.zero;

	void Start () {

		centerMapOffset = new Vector3( -(maximumMapSize-1)*prefabScaleFactor/2f, 0f, -(maximumMapSize-1)*prefabScaleFactor/2f ); 
		theGrid = new byte[maximumMapSize, maximumMapSize, maximumMapSize];

        assignTheLevelData();
        populateGeometryFromData();

	}

    public void assignTheLevelData()
    {

        // ASSIGN THE DATA
        for (int x = 0; x < maximumMapSize; x++) {
            for (int y = 0; y < maximumMapSize; y++) {
                for (int z = 0; z < maximumMapSize; z++) {

                    switch ( shape )
                    {
                        default:
                        case GeometryShapes.Foundation:
                            if (y == 0)
                            {
                                theGrid[x, y, z] = 1;
                            }
                            else
                            {
                                theGrid[x, y, z] = 0;
                            }
                        break;
                        case GeometryShapes.Sphere:
                            if (Vector3.Distance(new Vector3(x, y, z) * prefabScaleFactor + centerMapOffset, Vector3.zero) > maximumMapSize / 2f)
                            {
                                theGrid[x, y, z] = 0;
                            }
                            else
                            {
                                theGrid[x, y, z] = 1;
                            }
                            break;
                        case GeometryShapes.Perlin_01:                            
                            bool conditionOne = Noise.valueAtPoint(new Vector3(x, y, z), 0.25f, 1) > 0.5f && Vector3.Distance(new Vector3(x, y, z) * prefabScaleFactor + centerMapOffset, Vector3.zero) < maximumMapSize / 2f;
                            bool conditionTwo = Noise.valueAtPoint(new Vector3(x, y, z), 0.25f, 1) < 0.0125f;
                            if (conditionTwo)
                            {

                                if (x == 0 || x == maximumMapSize - 1 ||
                                    y == 0 || y == maximumMapSize - 1 ||
                                    z == 0 || z == maximumMapSize - 1)
                                {
                                    theGrid[x, y, z] = 2;
                                }
                                else
                                {
                                    theGrid[x, y, z] = 1;
                                }

                            }
                            else
                            {
                                theGrid[x, y, z] = 0;
                            }
                            break;
                    }

                    /*
                    if (y > maximumMapSize)
                    {
                        theGrid[x, y, z] = 0;
                    }
                     * */

                }
            }
        }

    }

    public void populateGeometryFromData()
    {
        // POPULATE THE OBJECTS - CREATE THE LEVEL GEOMETRY
        GameObject levelGeometry = new GameObject();
        levelGeometry.name = "LevelGeometry";
        for (int x = 0; x < maximumMapSize; x++){
            for (int y = 0; y < maximumMapSize; y++){
                for (int z = 0; z < maximumMapSize; z++){


                    GameObject prefabTile;

                    if (randomTile) {
                        prefabTile = Random.Range(0, 2) == 0 ? prefabTileTypeOne : prefabTileTypeTwo;
                    } else {
                        if (theGrid[x, y, z] == 1) {
                            prefabTile = prefabTileTypeOne;
                        } else {
                            prefabTile = prefabTileTypeTwo;
                        }
                    }

                    Vector3 imperfectionOffset;
                    if (tileImperfections) {
                        imperfectionOffset = new Vector3(0f, Random.Range(-2, 2) * 0.1f, 0f);
                    } else {
                        imperfectionOffset = Vector3.zero;
                    }

                    bool shouldRenderThis = false;

                    // put all of this into a switch
                    switch (shape)
                    {
                        default:
                        case GeometryShapes.Foundation:
                                if (y == 1) { shouldRenderThis = true; }
                            break;
                        case GeometryShapes.Sphere:
                            if (Vector3.Distance(new Vector3(x, y, z), Vector3.zero) <= maximumMapSize * .5f)
                            {
                                shouldRenderThis = true;
                            }
                            break;
                        case GeometryShapes.Perlin_01:
                                if (    theGrid[x, y, z] != 0 &&
                                        adjacentVisibility(x, y, z) &&
                                        y > maximumMapSize * .5f &&
                                        y != maximumMapSize - 1 &&
                                        x != 0 &&
                                        z != 0                          )
                                {
                                    shouldRenderThis = true;
                                }
                            break;
                    }

                    if (shouldRenderThis)
                    {
                        GameObject temp = Instantiate(
                            prefabTile,
                            new Vector3(x, y, z) * prefabScaleFactor + centerMapOffset + imperfectionOffset,
                            Quaternion.identity
                            ) as GameObject;
                        temp.name = "Grid[" + x + "," + y + "," + z + "]";
                        temp.transform.SetParent(levelGeometry.transform);
                    }

                }
            }
        }

    }

	/**
	 * USE THIS TO HOLLOW OUT SOLID MASSES
	 */
	public bool adjacentVisibility(int x, int y, int z)
	{
		bool shouldSpawnBlock = false;

		// if its an edge spawn the block
		if (	x == 0 || x == maximumMapSize-1 ||
				y == 0 || y == maximumMapSize-1 ||
				z == 0 || z == maximumMapSize-1 	){

			shouldSpawnBlock = true;

		} else {

			// if you can see it somehow spawn it
			for ( int i=x-1; i<=x+1; i++){
				for ( int j=y-1; j<=y+1; j++){
					for ( int k=z-1; k<=z+1; k++){
						if ( theGrid[i,j,k] == 0 ){
							shouldSpawnBlock = true;
							return shouldSpawnBlock;
						}
					}
				}
			}

		}

		return shouldSpawnBlock;
	}


	public enum VolumeTypes : byte 
    {
		Empty,
		Solid
	}

    public enum GeometryShapes
    {
        Foundation,
        Sphere,
        Perlin_01
    }

}
