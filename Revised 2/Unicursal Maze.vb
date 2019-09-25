Module Unicursal_Maze
    Sub Unicursal(ByVal Maze As List(Of Node))

        'steps:
        '   Flood fill to find the available path
        '   make the availablepath a wall, this means enlarging the maze so that the path is 3 wide
        '   done
        'https://pbs.twimg.com/media/BLr0vnDCAAE3ZxU.jpg:large
        Console.Clear()
        Maze.RemoveAt(Maze.Count - 1)
        Maze.RemoveAt(Maze.Count - 1)
        Dim AlreadyAdded As New Dictionary(Of Node, Boolean)
        SetBoth(ConsoleColor.Red)
        For Each node In Maze
            node.update(node.X * 2, node.Y * 2)
        Next
        Dim copyMaze As List(Of Node) = Maze
        For Each node In Maze
            AlreadyAdded(node) = False
        Next
        Dim newMaze As New List(Of Node)
        'enlarge maze
        For Each node In Maze
            SetBoth(ConsoleColor.White)
            'node.Print("XX")
            Dim SurroundingNodes As New List(Of Node) From {
                New Node(node.X + 2, node.Y),
                New Node(node.X + 2, node.Y + 1),
                New Node(node.X + 2, node.Y - 1),
                New Node(node.X - 2, node.Y),
                New Node(node.X - 2, node.Y + 1),
                New Node(node.X - 2, node.Y - 1),
                New Node(node.X, node.Y - 1),
                New Node(node.X + 2, node.Y - 1),
                New Node(node.X - 2, node.Y - 1),
                New Node(node.X, node.Y + 1),
                New Node(node.X + 2, node.Y + 1),
                New Node(node.X - 2, node.Y + 1)
            }
            Dim NodesToRemove As New List(Of Node)
            For Each NodetoRemove In NodesToRemove
                SurroundingNodes.Remove(NodetoRemove)
            Next
            For Each FinalNode In SurroundingNodes
                newMaze.Add(FinalNode)
            Next
        Next
        Console.ResetColor()
        Console.Clear()
        For Each node In copyMaze
            Dim PotentialAdjacentCell As New Node(node.X + 4, node.Y)
            If copyMaze.Contains(PotentialAdjacentCell) Then
                newMaze.Remove(New Node(node.X + 2, node.Y))
            End If
            PotentialAdjacentCell.update(node.X - 4, node.Y)
            If copyMaze.Contains(PotentialAdjacentCell) Then
                newMaze.Remove(New Node(node.X - 2, node.Y))
            End If
            PotentialAdjacentCell.update(node.X, node.Y + 2)
            If copyMaze.Contains(PotentialAdjacentCell) Then
                newMaze.Remove(New Node(node.X, node.Y + 1))
            End If
            PotentialAdjacentCell.update(node.X, node.Y - 2)
            If copyMaze.Contains(PotentialAdjacentCell) Then
                newMaze.Remove(New Node(node.X, node.Y - 1))
            End If
        Next

        SetBoth(ConsoleColor.White)
        Dim Gx, Gy As Integer
        For Each node In newMaze
            node.Print("XX")
            If node.X > Gx Then Gx = node.X
            If node.Y > Gy Then Gy = node.Y
        Next
        Dim limits() As Integer = {5, 3, Gx, Gy}
        AddStartAndEnd(Maze, limits, 0)
        Console.ReadKey()
    End Sub
End Module
