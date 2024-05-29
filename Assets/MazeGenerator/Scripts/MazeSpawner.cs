using UnityEngine;
using System.Collections;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour
{
    public enum MazeGenerationAlgorithm
    {
        PureRecursive,
        RecursiveTree,
        RandomTree,
        OldestTree,
        RecursiveDivision,
    }

    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom = false;
    public int RandomSeed = 12345;
    public GameObject Floor = null;
    public GameObject Wall = null;
    public GameObject Pillar = null;
    public GameObject GoalPrefab = null;
    public GameObject CharacterPrefab = null;
    public int Rows = 10;
    public int Columns = 10;
    public float CellWidth = 10;
    public float CellHeight = 10;
    public bool AddGaps = true;
    

    private BasicMazeGenerator mMazeGenerator = null;

    void Awake()

    {
        if (GoalPrefab != null)
        {
            // Instantiate the GoalPrefab
        }
        else
        {
            // Find a GameObject named "Point"
            GameObject pointObject = GameObject.Find("PirateCoin");

            if (pointObject != null)
            {
                // Use the found GameObject as the goal prefab
                GoalPrefab = pointObject;
            }
            else
            {
                Debug.LogError("GoalPrefab is null, and no GameObject named 'PirateCoin' was found.");
            }
        }


        if (!FullRandom)
        {
            Random.seed = RandomSeed;
        }
        switch (Algorithm)
        {
            case MazeGenerationAlgorithm.PureRecursive:
                mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveTree:
                mMazeGenerator = new RecursiveTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RandomTree:
                mMazeGenerator = new RandomTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.OldestTree:
                mMazeGenerator = new OldestTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveDivision:
                mMazeGenerator = new DivisionMazeGenerator(Rows, Columns);
                break;
        }
        mMazeGenerator.GenerateMaze();
        // Set the goal cell to the middle of the maze
        int goalRow = Rows / 3;
        int goalColumn = Columns / 3;
        if (goalRow < Rows && goalColumn < Columns)
        {
            Debug.Log($" row {Rows}, column {Columns}");
            MazeCell goalCell = mMazeGenerator.GetMazeCell(goalRow, goalColumn);
            goalCell.IsGoal = true;
            Debug.Log($"Goal cell set at row {goalRow}, column {goalColumn}");

            // Verify that the goal cell was set correctly
            Debug.Log($"goalCell.IsGoal: {goalCell.IsGoal}");
            if (GoalPrefab != null)
            {

                Debug.Log($"GoalPrefab: {GoalPrefab.name}");
            }
            else
            {
                Debug.LogError("GoalPrefab is null.");
            }


        }
        else
        {
            Debug.LogError("Goal cell coordinates are out of bounds.");
        }

        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? .2f : 0));
                float z = row * (CellHeight + (AddGaps ? .2f : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;
                tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                tmp.transform.parent = transform;
                if (cell.WallRight)
                {
                    tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
                    tmp.transform.parent = transform;
                }
                if (cell.WallFront)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
                    tmp.transform.parent = transform;
                }
                if (cell.WallLeft)
                {
                    tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;// left
                    tmp.transform.parent = transform;
                }
                if (cell.WallBack)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;// back
                    tmp.transform.parent = transform;
                }
                if (cell.IsGoal && GoalPrefab != null )
                {
                    tmp = Instantiate(GoalPrefab, new Vector3(x, 1, z), Quaternion.Euler(90, 0, 0)) as GameObject;
                    tmp.transform.parent = transform;
                }
            }
        }
        if (Pillar != null)
        {
            for (int row = 0; row < Rows + 1; row++)
            {
                for (int column = 0; column < Columns + 1; column++)
                {
                    float x = column * (CellWidth + (AddGaps ? .2f : 0));
                    float z = row * (CellHeight + (AddGaps ? .2f : 0));
                    GameObject tmp = Instantiate(Pillar, new Vector3(x - CellWidth / 2, 0, z - CellHeight / 2), Quaternion.identity) as GameObject;
                    tmp.transform.parent = transform;
                }
            }
        }

        if (CharacterPrefab != null)
        {
            SpawnCharacterAtStart();
        }
    }
    private void SpawnCharacterAtStart()
    {
        int startX = 0;
        int startZ = 0;
        float x = startX * (CellWidth + (AddGaps ? .2f : 0));
        float z = startZ * (CellHeight + (AddGaps ? .2f : 0));
        GameObject character = Instantiate(CharacterPrefab, new Vector3(x, 1, z), Quaternion.identity);
        character.transform.position = new Vector3(x, 1, z);
    }
}