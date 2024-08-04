/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
using HutongGames.PlayMaker.Actions;

public class Testing : MonoBehaviour {


    
    [SerializeField] private PathfindingDebugStepVisual pathfindingDebugStepVisual;
    [SerializeField] private PathfindingVisual pathfindingVisual;
    [SerializeField] private CharacterPathfindingMovementHandler characterPathfinding;
    [SerializeField] private Grid gridsystem;
    private Pathfinding pathfinding;
    private bool testing = true;
    public GameObject goal;
    public GameObject blocker;
    public int terrainChangeCount;
    private Vector3 previousRandomSquare;
    



    private void Start() {
        StartCoroutine(StartMoving());
        

    }

    IEnumerator StartMoving()
    {


        pathfinding = new Pathfinding(20, 10);
        pathfindingDebugStepVisual.Setup(pathfinding.GetGrid());
        pathfindingVisual.SetGrid(pathfinding.GetGrid());
        //^THIS GENERATES THE VISUALS FOR THE GRID ??

        yield return new WaitForSeconds(8.0f);
        Vector3 randomLocation = new Vector3(Random.Range(0.0f, 200.0f), Random.Range(0.0f, 100.0f));
        Vector3 CurrentGoal = randomLocation;
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        pathfinding.GetGrid().GetXY(randomLocation, out int x, out int y);
        List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
            }
        }
        characterPathfinding.SetTargetPosition(randomLocation);

        Debug.Log(mouseWorldPosition);

        pathfinding.GetGrid().GetXY(CurrentGoal, out int GoalposX, out int GoalposY);
        pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        List<PathNode> goalLocation = pathfinding.FindPath(0, 0, x, y);

        CurrentGoal.x = x * 9.5f + 10;
        CurrentGoal.y = y * 9.5f + 10;
        Instantiate(goal, CurrentGoal, Quaternion.identity);

        // TURNED OFF > pathfinding.GetNode(GoalposX, GoalposY).SetIsGoal(!pathfinding.GetNode(GoalposX, GoalposY).isGoal);

        //TRYING TO MARK THAT RANDOM LOCATION AS THE GOAL^^

    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {



           
        }
        //^THIS IS MOVING THE CHARACTER TO THE MOUSES CURRENT POSITION


        if (terrainChangeCount > 0) {



            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();


            Vector3 randomSquareTarget = new Vector3(Random.Range(0.0f, 200.0f), Random.Range(0.0f, 100.0f));

            if (previousRandomSquare != randomSquareTarget)
            {
            pathfinding.GetGrid().GetXY(randomSquareTarget, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
            List<PathNode> blockLocation = pathfinding.FindPath(0, 0, x, y);
            Vector3 previousRandomSquare = randomSquareTarget;
            

            randomSquareTarget.x = x * 9.5f + 10;
            randomSquareTarget.y = y * 9.5f + 10;
            
            //blockLocation = randomSquareTarget;
            //blocklocation = System.Convert.(blockLocaitonVector);
            Instantiate(blocker, randomSquareTarget, Quaternion.identity);

            randomSquareTarget = previousRandomSquare;
            terrainChangeCount--;
            }

        }
        //^THIS IS CHANGING THE SQUARES TO BE BLOCKED WALLS
    }

}
