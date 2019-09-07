Module Braid
    Sub EliminateDeadEnds(ByRef Maze As List(Of Node))
        SetBoth(ConsoleColor.White)
        Dim r As New Random
        Dim start_v As New Node(Maze(Maze.Count - 2).X, Maze(Maze.Count - 2).Y)
        Dim goal As New Node(Maze(Maze.Count - 1).X, Maze(Maze.Count - 1).Y)
        Maze.Remove(start_v)
        Maze.Remove(goal)
        Dim NodesToAdd As New List(Of Node)
        For Each Node As Node In Maze
            If Node.Equals(start_v) Or Node.Equals(goal) Then Continue For
            If Node.IsDeadEnd(Maze) Then
                Dim AvailableNodes As New List(Of Node) From {
                    New Node(Node.X, Node.Y - 2),'up
                    New Node(Node.X + 4, Node.Y),'right
                    New Node(Node.X, Node.Y + 2),'down
                    New Node(Node.X - 4, Node.Y) 'left
                }
                Dim DirectNeighbour As New Node(Node.X, Node.Y - 1)
                If Maze.Contains(DirectNeighbour) Then AvailableNodes.RemoveAt(0)
                DirectNeighbour.update(Node.X + 2, Node.Y)
                If Maze.Contains(DirectNeighbour) Then AvailableNodes.RemoveAt(1)
                DirectNeighbour.update(Node.X, Node.Y + 1)
                If Maze.Contains(DirectNeighbour) Then AvailableNodes.RemoveAt(2)
                DirectNeighbour.update(Node.X - 2, Node.Y)
                If Maze.Contains(DirectNeighbour) Then AvailableNodes.RemoveAt(3)
                Dim NodesToRemove As New List(Of Node)
                For i = 0 To AvailableNodes.Count - 1
                    If Not Maze.Contains(AvailableNodes(i)) Then NodesToRemove.Add(AvailableNodes(i))
                Next
                For Each thing In NodesToRemove
                    AvailableNodes.Remove(thing)
                Next
                Dim PositionInMaze As Node = AvailableNodes(r.Next(0, AvailableNodes.Count))
                Dim PosToBeAdded As Node = MidPoint(PositionInMaze, Node)
                NodesToAdd.Add(PosToBeAdded)
                PosToBeAdded.Print("██")
            End If
        Next
        For Each node In NodesToAdd
            Maze.Add(node)
        Next
        Maze.Add(start_v)
        Maze.Add(goal)
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
    End Sub
End Module