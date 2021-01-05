using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State<EnemyGreen>
{
    public override void Execute(EnemyGreen agent)
    {
        
        //find the path to the player
        agent.grid.GetComponent<Pathfinding>().FindPath(agent.transform.position, agent.player.transform.position);

        //transform the node path into a vector3 path
     //   if (agent.enemyPath.Count > 1)
    //    {
            Vector3[] path = agent.grid.GetComponent<Pathfinding>().RetracePath(agent.grid.GetComponent<GridP>().NodeFromWorldPoint(agent.transform.position), agent.grid.GetComponent<GridP>().NodeFromWorldPoint(agent.player.transform.position));
            //follow the path
            foreach (Vector3 pathToFollow in path)
            {
                agent.transform.position += pathToFollow * agent.speed * Time.deltaTime;

            }
      //  }

      
        

     //   Debug.LogError("HAIDEEEEEEEE");
    }
}
