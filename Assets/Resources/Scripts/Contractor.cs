using UnityEngine;
using System.Collections;

public class Contractor : MonoBehaviour {

    /**
     * The Contractor Behavior Builds Levels
     * ---
     * Choose a level Template based on [Conditions XYZ]
     * Determine a maximum level map Bounds Based on the Level # / Difficulty
     * Define a spawn point and Exit Point
     * Between Spawn -> Exit generate and populate peices of Geometry individually
     * Connect next element to previous element until completed building the path
     */

    public LevelTemplates concreteTemplate = LevelTemplates.Flat;
    public int maximumMapSize;

    GameObject SpawnMarker;
    GameObject ExitMarker;


	// Use this for initialization
	void Start () {

        LevelManager lm = gameObject.GetComponent<LevelManager>();
        maximumMapSize = (lm.currentLevelNumber) * 4 + 10;

        SpawnMarker.transform.position = Vector3.zero;
        ExitMarker.transform.position = new Vector3(maximumMapSize, maximumMapSize, 0f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public enum LevelTemplates
    {
        Flat
    }
}
