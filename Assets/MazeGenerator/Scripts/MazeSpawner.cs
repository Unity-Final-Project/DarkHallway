using Unity.AI.Navigation;
using UnityEngine;

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
    public GameObject EnemyPrefab = null;
    public GameObject CeilingPrefab = null; 
    public GameObject LampPrefab = null; 
    public GameObject AudioSourceBackGround = null;
    public int Rows = 10;
    public int Columns = 10;
    public float CellWidth = 10;
    public float CellHeight = 10;
    public bool AddGaps = true;
    public static int TotalPoints = 0;

    private BasicMazeGenerator mMazeGenerator = null;
    private NavMeshSurface navMeshSurface;

    void Awake()
    {
        // Play Background Music
        if(AudioSourceBackGround == null)
        {

            AudioSourceBackGround = GameObject.Find("BackgroundMusic");
        }
        else
        {
        AudioSourceBackGround.GetComponent<AudioSource>().Play();
        }

        // Get or add the NavMeshSurface component
        navMeshSurface = gameObject.GetComponent<NavMeshSurface>();
        if (navMeshSurface == null)
        {
            navMeshSurface = gameObject.AddComponent<NavMeshSurface>();
        }

        // Set the layer mask to exclude obstacles (assuming layer 8 is "Obstacle")
        navMeshSurface.layerMask = ~LayerMask.GetMask("Obstacle");

        // Setup GoalPrefab
        if (GoalPrefab != null)
        {
            // Instantiate the GoalPrefab
        }
        else
        {
            // Find a GameObject named "PirateCoin"
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

        // Set Random Seed if not FullRandom
        if (!FullRandom)
        {
            Random.seed = RandomSeed;
        }

        // Select Maze Generation Algorithm
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

        // Set Goal Cell
        int goalRow = Rows / 3;
        int goalColumn = Columns / 3;
        if (goalRow < Rows && goalColumn < Columns)
        {
            MazeCell goalCell = mMazeGenerator.GetMazeCell(goalRow, goalColumn);
            goalCell.IsGoal = true;
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


        // Instantiate Maze
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? .2f : 0));
                float z = row * (CellHeight + (AddGaps ? .2f : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;

                // Instantiate Floor
                tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.identity);
                tmp.transform.parent = transform;
                
                //Instantiate Ceiling
                if (CeilingPrefab != null)
                {
                    tmp = Instantiate(CeilingPrefab, new Vector3(x, 4, z), Quaternion.identity);
                    tmp.transform.parent = transform;
                }

                // Instantiate Walls
                if (cell.WallRight)
                {
                    tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2, 0, z), Quaternion.Euler(0, 90, 0));
                    tmp.transform.parent = transform;
                
                }
                if (cell.WallFront)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z + CellHeight / 2), Quaternion.identity);
                    tmp.transform.parent = transform;

                 
                }
                if (cell.WallLeft)
                {
                    tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2, 0, z), Quaternion.Euler(0, 270, 0));
                    tmp.transform.parent = transform;
                 
                }
                if (cell.WallBack)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z - CellHeight / 2), Quaternion.Euler(0, 180, 0));
                    tmp.transform.parent = transform;
                
                }
                if (cell.IsGoal && GoalPrefab != null)
                {
                    tmp = Instantiate(GoalPrefab, new Vector3(x, 1, z), Quaternion.Euler(90, 0, 0)) as GameObject;   
                    tmp.transform.parent = transform;
                    TotalPoints++;
                }

            }
        }



        // Instantiate Pillars
        if (Pillar != null)
        {
            for (int row = 0; row < Rows + 1; row++)
            {
                for (int column = 0; column < Columns + 1; column++)
                {
                    float x = column * (CellWidth + (AddGaps ? .2f : 0));
                    float z = row * (CellHeight + (AddGaps ? .2f : 0));
                    GameObject tmp = Instantiate(Pillar, new Vector3(x - CellWidth / 2, 0, z - CellHeight / 2), Quaternion.identity);
                    tmp.transform.parent = transform;
                }
            }
        }

        // Spawn Character
        if (CharacterPrefab != null)
        {
            SpawnCharacterAtStart();
        }
        if (EnemyPrefab != null)
        {
            SpawnEnemyAtTheEnd();
        }

        // Bake the NavMesh after the maze is generated
        navMeshSurface.BuildNavMesh();

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
    private void SpawnEnemyAtTheEnd()
    {
        int endX = Rows - 1;
        int endZ = Columns - 1;
        float x = endX * (CellWidth + (AddGaps ? .2f : 0));
        float z = endZ * (CellHeight + (AddGaps ? .2f : 0));
        GameObject enemy = Instantiate(EnemyPrefab, new Vector3(x, 1, z), Quaternion.identity);
        enemy.transform.position = new Vector3(x, 1, z);
    }


}
