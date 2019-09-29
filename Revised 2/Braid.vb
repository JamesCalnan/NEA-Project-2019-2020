Imports NEA_2019
Module Braid
    Sub EliminateDeadEnds(ByRef maze As List(Of Node),pathColour as ConsoleColor,backGroundColour as ConsoleColor)
        SetBoth(pathcolour)
        Dim r As New Random
        Dim startV As New Node(maze(maze.Count - 2).X, maze(maze.Count - 2).Y)
        Dim goal As New Node(maze(maze.Count - 1).X, maze(maze.Count - 1).Y)
        maze.Remove(startV)
        maze.Remove(goal)
        Dim nodesToAdd As New List(Of Node)
        For Each node As Node In maze
            If node.Equals(startV) Or node.Equals(goal) Then Continue For
            If node.IsDeadEnd(maze) Then
                Dim availableNodes As New List(Of Node) From {
                        New Node(node.X, node.Y - 2),'up
                        New Node(node.X + 4, node.Y),'right
                        New Node(node.X, node.Y + 2),'down
                        New Node(node.X - 4, node.Y) 'left
                        }
                Dim directNeighbour As New Node(node.X, node.Y - 1)
                If maze.Contains(directNeighbour) Then availableNodes.RemoveAt(0)
                directNeighbour.Update(node.X + 2, node.Y)
                If maze.Contains(directNeighbour) Then availableNodes.RemoveAt(1)
                directNeighbour.Update(node.X, node.Y + 1)
                If maze.Contains(directNeighbour) Then availableNodes.RemoveAt(2)
                directNeighbour.Update(node.X - 2, node.Y)
                If maze.Contains(directNeighbour) Then availableNodes.RemoveAt(3)
                Dim nodesToRemove As New List(Of Node)
                For i = 0 To availableNodes.Count - 1
                    If Not maze.Contains(availableNodes(i)) Then nodesToRemove.Add(availableNodes(i))
                Next
                For Each thing In nodesToRemove
                    availableNodes.Remove(thing)
                Next
                If availableNodes.Count <> 0 Then
                    Dim positionInMaze As Node = availableNodes(r.Next(0, availableNodes.Count))
                    Dim posToBeAdded As Node = MidPoint(positionInMaze, node)
                    nodesToAdd.Add(posToBeAdded)
                    posToBeAdded.Print("██")
                End If
            End If
        Next
        For Each node In nodesToAdd
            maze.Add(node)
        Next
        maze.Add(startV)
        maze.Add(goal)
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
    End Sub
    Sub PartialBraid(ByRef maze As List(Of Node),pathColour as ConsoleColor,backGroundColour as ConsoleColor)
        SetBoth(pathColour)
        Dim r As New Random
        Dim startV As New Node(maze(maze.Count - 2).X, maze(maze.Count - 2).Y)
        Dim goal As New Node(maze(maze.Count - 1).X, maze(maze.Count - 1).Y)
        maze.Remove(startV)
        maze.Remove(goal)
        Dim nodesToAdd As New List(Of Node)
        Dim deadEnds As New List(Of Node)
        For Each node In maze
            If node.IsDeadEnd(maze) Then deadEnds.Add(node)
        Next
        For Each node In deadEnds
            If r.Next(1, 3) = 1 Then
                If node.Equals(startV) Or node.Equals(goal) Then Continue For
                If node.IsDeadEnd(maze) Then
                    Dim availableNodes As New List(Of Node) From {
                        New Node(node.X, node.Y - 2),'up
                        New Node(node.X + 4, node.Y),'right
                        New Node(node.X, node.Y + 2),'down
                        New Node(node.X - 4, node.Y) 'left
                    }
                    Dim directNeighbour As New Node(node.X, node.Y - 1)
                    If maze.Contains(directNeighbour) Then availableNodes.RemoveAt(0)
                    directNeighbour.Update(node.X + 2, node.Y)
                    If maze.Contains(directNeighbour) Then availableNodes.RemoveAt(1)
                    directNeighbour.Update(node.X, node.Y + 1)
                    If maze.Contains(directNeighbour) Then availableNodes.RemoveAt(2)
                    directNeighbour.Update(node.X - 2, node.Y)
                    If maze.Contains(directNeighbour) Then availableNodes.RemoveAt(3)
                    Dim nodesToRemove As New List(Of Node)
                    For j = 0 To availableNodes.Count - 1
                        If Not maze.Contains(availableNodes(j)) Then nodesToRemove.Add(availableNodes(j))
                    Next
                    For Each thing In nodesToRemove
                        availableNodes.Remove(thing)
                    Next
                    If availableNodes.Count <> 0 Then
                        Dim positionInMaze As Node = availableNodes(r.Next(0, availableNodes.Count))
                        Dim posToBeAdded As Node = MidPoint(positionInMaze, node)
                        nodesToAdd.Add(posToBeAdded)
                        posToBeAdded.Print("██")
                    End If
                End If
            End If
        Next
        maze.AddRange(nodesToAdd)
        maze.Add(startV)
        maze.Add(goal)
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
    End Sub
End Module