using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeGenerator : MonoBehaviour
{
    private static Scene currentScene;

    // Fast as lightning
    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();

        List<List<string>> finalMaze = MazeMain();
        int hardCount = (int)AdaptativeDifficulty.actual_diff/10;
        for(int i = 0; i < finalMaze.Count; i++) //height
            for(int j = 0; j < finalMaze[i].Count; j++) //width
                if (finalMaze[i][j] == "c"){

                
                    if (j == 0)
                        continue;
                    else if (j == finalMaze[i].Count - 1)
                        InstantiateCells(j, i, finalMaze, hardCount, "F");
                    else
                        InstantiateCells(j, i, finalMaze, hardCount);
                    hardCount -= 1;
                }
                    
    }

    /**
    * Find the number of surrounding cells to the wall given 
    */
    static int SurroundingCells(List<int> rand_wall, List<List<string>> maze)
    {
        int surr_cells = 0;
        if (maze[rand_wall[1] - 1][rand_wall[0]] == "c")
            surr_cells++;
        if (maze[rand_wall[1] + 1][rand_wall[0]] == "c")
            surr_cells++;
        if (maze[rand_wall[1]][rand_wall[0] - 1] == "c")
            surr_cells++;
        if (maze[rand_wall[1]][rand_wall[0] + 1] == "c")
            surr_cells++;
        return surr_cells;
    }

    static void PrintMaze(List<List<string>> maze)
    {
        Debug.Log("Print Maze:\n");
        foreach (List<string> list in maze)
        {
            foreach (string s in list)
                Debug.Log(s + ",");
            Debug.Log("\n");
        }
    }

    static void InstantiateCells(int x, int y, List<List<string>> maze, int hardCount, string neighbours = "") {
        string diff = "";
        if (string.IsNullOrWhiteSpace(neighbours)) {
            if (maze[y - 1][x] == "c")
                neighbours += "H";
            if (maze[y][x - 1] == "c")
                neighbours += "G";
            if (maze[y + 1][x] == "c")
                neighbours += "B";
            if (maze[y][x + 1] == "c")
                neighbours += "D";
        }
        
        if (hardCount > 0 && neighbours != "F"){
            //Debug.Log(hardCount);
            diff = "hard";
        }
        else {
            diff = "normal";
        }
            

        if (neighbours == "F")
            Instantiate(Resources.Load($"MapsPrefabs/MapBOSS"), new Vector2(x*23, (-y*14)+1000), Quaternion.identity);
        else {
            
            Instantiate(Resources.Load($"MapsPrefabs/Map{neighbours}_{diff}"), new Vector2(x * 23, -y * 14 + 1000), Quaternion.identity);
        }
    }


    /**
     * Method to add a wall
     */
    static List<List<int>> AddWall(List<List<int>> walls, int x, int y)
    {
        List<int> new_wall = new List<int>
        {
            y,
            x
        };
        if (!walls.Contains(new_wall))
            walls.Add(new_wall);
        return walls;
    }

    static List<List<string>> MazeMain()
    {
        string wall = "w";
        //string cell = "c";
        string unvisited = "u";
        int height = 0;
        int width = 0;
        if (currentScene.name == "LevelGame")
        {
            height = 6;
            width = 6;
        }
        else if (currentScene.name == "LevelTest")
        {
            height = 4;
            width = 3;
        }

        int s_cells;

        // Maze skeleton creation
        List<List<string>> maze = new();
        List<List<int>> walls = new();

        // Maze filling with unvisited cells
        for (int i = 0; i < height; i++)
        {
            maze.Add(new List<string>());
            for (int j = 0; j < width; j++)
                maze[i].Add(unvisited);
        }

        // Mark starting point as a cell
        int starting_height = height - 2;
        int starting_width = 1;
        maze[starting_height][starting_width] = "c";

        // Mark its surroundings as walls
        walls = AddWall(walls, starting_height - 1, starting_width);
        walls = AddWall(walls, starting_height + 1, starting_width);
        walls = AddWall(walls, starting_height, starting_width + 1);
        walls = AddWall(walls, starting_height, starting_width - 1);
        maze[starting_height - 1][starting_width] = wall;
        maze[starting_height + 1][starting_width] = wall;
        maze[starting_height][starting_width - 1] = wall;
        maze[starting_height][starting_width + 1] = wall;
        while (walls.Count != 0)
        {
            // Pick a random wall
            int rnd_int = Random.Range(0, walls.Count);
            List<int> rand_wall = walls[rnd_int];

            // Check if it is a right wall
            if (rand_wall[0] != width - 1 & rand_wall[0] != 0 && maze[rand_wall[1]][rand_wall[0] + 1] == "c")
            {
                s_cells = SurroundingCells(rand_wall, maze);
                if (s_cells < 2)
                {
                    CheckCell(true, false, true, true, width, height, rand_wall, maze, walls);
                    continue;
                }
            }

            // Check if it is a left wall
            if (rand_wall[0] != width - 1 & rand_wall[0] != 0 && maze[rand_wall[1]][rand_wall[0] - 1] == "c")
            {
                s_cells = SurroundingCells(rand_wall, maze);
                if (s_cells < 2)
                {
                    CheckCell(false, true, true, true, width, height, rand_wall, maze, walls);
                    continue;
                }
            }

            // Check if it is an upper wall
            if (rand_wall[1] != height - 1 & rand_wall[1] != 0 && maze[rand_wall[1] - 1][rand_wall[0]] == "c")
            {
                s_cells = SurroundingCells(rand_wall, maze);
                if (s_cells < 2)
                {
                    CheckCell(true, true, false, true, width, height, rand_wall, maze, walls);
                    continue;
                }
            }

            // Check if it is a bottom wall
            if (rand_wall[1] != height - 1 & rand_wall[1] != 0 && maze[rand_wall[1] + 1][rand_wall[0]] == "c")
            {
                s_cells = SurroundingCells(rand_wall, maze);
                if (s_cells < 2)
                {
                    CheckCell(true, true, true, false, width, height, rand_wall, maze, walls);
                    continue;
                }
            }

            for (int i = 0; i < walls.Count; i++)
                if (walls[i][0] == rand_wall[0] & walls[i][1] == rand_wall[1])
                    walls.Remove(walls[i]);
        }

        // Mark all unvisited as wall
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                if (maze[i][j] == "u")
                    maze[i][j] = "w";

        // Set entrance
        maze[height - 2][0] = "c";

        // Set exit
        bool endSetNeeded = true;
        while (endSetNeeded) {
            for (int i = 0; i < height - 1; i++) {
                if (maze[i][width - 2] == "c")
                {
                    maze[i][width - 1] = "c";
                    endSetNeeded = false;
                    break;
                }
            }
        }


        return maze;
    }

    private static void CheckCell(bool left, bool right, bool upper, bool bottom, int width, int height, List<int> rand_wall, List<List<string>> maze, List<List<int>> walls)
    {
        maze[rand_wall[1]][rand_wall[0]] = "c";

        // Mark the new walls
        if (upper && rand_wall[1] != 0 && maze[rand_wall[1] - 1][rand_wall[0]] != "c")
        {
            // Upper cell
            maze[rand_wall[1] - 1][rand_wall[0]] = "w";
            walls = AddWall(walls, rand_wall[1] - 1, rand_wall[0]);
        }

        if (left && rand_wall[0] != 0 && maze[rand_wall[1]][rand_wall[0] - 1] != "c")
        {
            // Leftmost cell
            maze[rand_wall[1]][rand_wall[0] - 1] = "w";
            walls = AddWall(walls, rand_wall[1], rand_wall[0] - 1);           
        }

        if (bottom && rand_wall[1] != height - 1 && maze[rand_wall[1] + 1][rand_wall[0]] != "c")
        {
            // Bottom cell
            maze[rand_wall[1] + 1][rand_wall[0]] = "w";
            walls = AddWall(walls, rand_wall[1] + 1, rand_wall[0]);
        }

        if (right && rand_wall[0] != width - 1 && maze[rand_wall[1]][rand_wall[0] + 1] != "c")
        {
            // Right cell
            maze[rand_wall[1]][rand_wall[0] + 1] = "w";
            walls = AddWall(walls, rand_wall[1], rand_wall[0] + 1);          
        }

        for (int i = 0; i < walls.Count; i++)
            if (walls[i][0] == rand_wall[0] & walls[i][1] == rand_wall[1])
                walls.Remove(walls[i]);
    }

}
