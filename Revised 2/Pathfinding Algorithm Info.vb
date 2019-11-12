Module Pathfinding_Algorithm_Info
    Sub astarINFO()
        Console.WriteLine("The A* algorithm

At each step of the algorithm a value is calculated, this is called the f-cost
The f-value is made up of the sum of two other values, the g and h cost
At each step the algorithm picks the node that has the lowest f-cost
The g-cost is the movement cost from the starting node to the current node
The h-cost is the estimated movement cost to move from the given node to the target node
The h-cost is found by using a heuristic


Pseudocode from wikipedia:

function reconstruct_path(cameFrom, current)
    total_path := {current}
    while current in cameFrom.Keys:
        current := cameFrom[current]
        total_path.prepend(current)
    return total_path

// A* finds a path from start to goal.
// h is the heuristic function. h(n) estimates the cost to reach goal from node n.
function A_Star(start, goal, h)
    // The set of discovered nodes that may need to be (re-)expanded.
    // Initially, only the start node is known.
    openSet := {start}

    // For node n, cameFrom[n] is the node immediately preceding it on the cheapest path from start to n currently known.
    cameFrom := an empty map

    // For node n, gScore[n] is the cost of the cheapest path from start to n currently known.
    gScore := map with default value of Infinity
    gScore[start] := 0

    // For node n, fScore[n] := gScore[n] + h(n).
    fScore := map with default value of Infinity
    fScore[start] := h(start)

    while openSet is not empty
        current := the node in openSet having the lowest fScore[] value
        if current = goal
            return reconstruct_path(cameFrom, current)

        openSet.Remove(current)
        for each neighbor of current
            // d(current,neighbor) is the weight of the edge from current to neighbor
            // tentative_gScore is the distance from start to the neighbor through current
            tentative_gScore := gScore[current] + d(current, neighbor)
            if tentative_gScore < gScore[neighbor]
                // This path to neighbor is better than any previous one. Record it!
                cameFrom[neighbor] := current
                gScore[neighbor] := tentative_gScore
                fScore[neighbor] := gScore[neighbor] + h(neighbor)
                if neighbor not in openSet
                    openSet.add(neighbor)

    // Open set is empty but goal was never reached
    return failure")
    End Sub
    Sub dijkstrasalgorithmINFO()
        Console.WriteLine("Dijkstra's Algorithm

Dijkstra's algorithm can be used to find the shortest path between a source node and any given node
It works by looking at the distance to an unvisited node in the graph

Pseudocode from wikipedia

function Dijkstra(Graph, source):

      create vertex set Q

      for each vertex v in Graph:             
          dist[v] ← INFINITY                  
          prev[v] ← UNDEFINED                 
          add v to Q                      
      dist[source] ← 0                        
      
      while Q is not empty:
          u ← vertex in Q with min dist[u]    
                                              
          remove u from Q 
          
          for each neighbor v of u:           // only v that are still in Q
              alt ← dist[u] + length(u, v)
              if alt < dist[v]:               
                  dist[v] ← alt 
                  prev[v] ← u 

      return dist[], prev[]")
    End Sub
    Sub IDAstarinfo()
        Console.WriteLine("Iterative deppening A*

At each step in the algorithm a depth-first search is performed and when its total cost exceeds a certain number the path is no longer looked at

Pseudocode from wikipeida

path              current search path (acts like a stack)
node              current node (last node in current path)
g                 the cost to reach current node
f                 estimated cost of the cheapest path (root..node..goal)
h(node)           estimated cost of the cheapest path (node..goal)
cost(node, succ)  step cost function
is_goal(node)     goal test
successors(node)  node expanding function, expand nodes ordered by g + h(node)
ida_star(root)    return either NOT_FOUND or a pair with the best path and its cost
 
procedure ida_star(root)
  bound := h(root)
  path := [root]
  loop
    t := search(path, 0, bound)
    if t = FOUND then return (path, bound)
    if t = ∞ then return NOT_FOUND
    bound := t
  end loop
end procedure
 
function search(path, g, bound)
  node := path.last
  f := g + h(node)
  if f > bound then return f
  if is_goal(node) then return FOUND
  min := ∞
  for succ in successors(node) do
    if succ not in path then
      path.push(succ)
      t := search(path, g + cost(node, succ), bound)
      if t = FOUND then return FOUND
      if t < min then min := t
      path.pop()
    end if
  end for
  return min
end function
")
    End Sub
    Sub bestfirstsearchINFO()
        Console.WriteLine("Best-First Search

Best-First Search, like Breadth-First Search, uses a queue data structure
At each iteration an estimate cost to the target node is made, the neighbour node is inserted into the queue in order of the cost to the target node

Pseudocode from geeksforgeeks

Best-First-Search(Grah g, Node start)
    1) Create an empty PriorityQueue
       PriorityQueue pq;
    2) Insert start in pq.
       pq.insert(start)
    3) Until PriorityQueue is empty
          u = PriorityQueue.DeleteMin
          If u is the goal
             Exit
          Else
             Foreach neighbor v of u
                If v Unvisited
                    Mark v Visited                    
                    pq.insert(v)
             Mark u Examined                    
End procedure")
    End Sub

    Sub breadthfirstsearch()
        Console.WriteLine("Breadth-First Search
Breadth-first search is an algorithm for searching a tree")
    End Sub

End Module
