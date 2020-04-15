using UnityEngine;

public class Shape : MonoBehaviour
{
    #region Fields
    public Vector3 rotationPoint;
    public float previousTime;

    private GameControl gameControl;
    private AudioManager audioManager;

    private bool buttonIsUp = true;
    #endregion

    #region Start and Update
    private void Start()
    {
        
        gameControl = FindObjectOfType<GameControl>();
        audioManager = FindObjectOfType<AudioManager>();

        gameControl.UpdateScore();

        if (!IsInBoundary())
        {
            Destroy(gameObject);
            GameControl.gameIsOver = true;
        }
    }

    void Update()
    {
        #region Input
        float horizontalIn = Input.GetAxisRaw("Horizontal");

        if (horizontalIn == 0)
        {
            buttonIsUp = true;
        }

        // Moves Left
        if (horizontalIn < 0 && buttonIsUp && !GameControl.gameIsPaused)
        {
            buttonIsUp = false;
            audioManager.Play("TetrisMove");

            transform.position += new Vector3(-1, 0, 0);
            if (!IsInBoundary())
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }

        // Moves Right
        if (horizontalIn > 0 && buttonIsUp && !GameControl.gameIsPaused)
        {
            buttonIsUp = false;
            audioManager.Play("TetrisMove");

            transform.position += new Vector3(1, 0, 0);
            if (!IsInBoundary())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }

        // Moves Down
        if (Time.time - previousTime > ((Input.GetAxisRaw("Vertical") < 0) ? 0.8f / 10 : gameControl.fallTime) && !GameControl.gameIsPaused)
        {
            audioManager.Play("TetrisMove");

            transform.position += new Vector3(0, -1, 0);
            if (!IsInBoundary())
            {
                transform.position += new Vector3(0, 1, 0);
                AddToGrid();
                CheckLines();
                enabled = false;
                FindObjectOfType<ShapeSpawner>().SpawnShape();
            }

            previousTime = Time.time;
        }

        // Rotate
        if (Input.GetButtonDown("Fire1") && !GameControl.gameIsPaused)
        {
            audioManager.Play("TetrisRotate");

            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!IsInBoundary())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
        #endregion
    }
    #endregion

    #region Helper Functions
    private void CheckLines()
    {
        for (int row = 19; row >= 0; row--)
        {
            if (HasCompletedLine(row))
            {
                DeleteLine(row);
                MoveRowDown(row);
            }
        }
    }

    // Moves down every rows above current row
    private void MoveRowDown(int row)
    {
        for (int currentRow = row; currentRow < 20; currentRow++)
        {
            for (int currentColumn = 0; currentColumn < 10; currentColumn++)
            {
                if (GameGrid.grid[currentColumn, currentRow] != null)
                {
                    GameGrid.grid[currentColumn, currentRow - 1] = GameGrid.grid[currentColumn, currentRow];
                    GameGrid.grid[currentColumn, currentRow] = null;
                    GameGrid.grid[currentColumn, currentRow - 1].position += new Vector3(0, -1, 0);
                }

            }
        }
    }

    private void DeleteLine(int row)
    {
        audioManager.Play("LineClear");

        for (int column = 0; column < 10; column++)
        {
            Destroy(GameGrid.grid[column, row].gameObject);
            GameGrid.grid[column, row] = null;
        }

        gameControl.IncreasDifficulty();

        // Updates score and high score after deleting line
        GameControl.score++;

        if(GameControl.score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", GameControl.score);
        }

        FindObjectOfType<GameControl>().UpdateScore();
    }

    // Returns whether the row is completed or not
    private bool HasCompletedLine(int row)
    {
        for (int column = 0; column < 10; column++)
        {
            if (GameGrid.grid[column, row] == null)
            {
                return false;
            }
        }

        return true;
    }

    // Returns whether every child block is within the boundary
    public bool IsInBoundary()
    {
        foreach (Transform childBlock in transform)
        {
            int roundedX = Mathf.RoundToInt(childBlock.transform.position.x);
            int roundedY = Mathf.RoundToInt(childBlock.transform.position.y);

            if (roundedX < 0 || roundedX > 9 || roundedY < 0)
            {
                return false;
            }

            if (GameGrid.grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }

    // Add every child block of the tetromino to the GameBoard grid
    void AddToGrid()
    {
        foreach (Transform childBlock in transform)
        {
            int roundedX = Mathf.RoundToInt(childBlock.transform.position.x);
            int roundedY = Mathf.RoundToInt(childBlock.transform.position.y);

            GameGrid.grid[roundedX, roundedY] = childBlock;
        }
    }
    #endregion
}
