Module UnicursalMaze
    function Unicursal(mazeOriginal As List(Of Node),pathColour as ConsoleColor,backGroundColour as ConsoleColor)
        'https://pbs.twimg.com/media/BLr0vnDCAAE3ZxU.jpg:large
        dim maze as List(Of Node) = mazeOriginal.ToList()
        Console.Clear()
        maze.RemoveAt(maze.Count - 1)
        maze.RemoveAt(maze.Count - 1)
        Dim alreadyAdded As New List(Of Node)
        SetBoth(ConsoleColor.Red)
        For Each node In maze
            node.Update((node.X * 2)-5, (node.Y * 2)-2)
            if node.x > Console.WindowWidth-60 or node.Y > Console.WindowHeight-10 then Return nothing
        Next
        Dim copyMaze As List(Of Node) = maze
        Dim newMaze As New List(Of Node)
        'enlarge maze
        For Each node In maze
            Dim surroundingNodes As New List(Of Node) From {
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
                New Node(node.X - 2, node.Y + 1)}
            Dim nodesToRemove As List(Of Node) = (From surroundingNode In surroundingNodes Where alreadyAdded.Contains(surroundingNode)).ToList()
            For Each NodetoRemove In nodesToRemove
                surroundingNodes.Remove(NodetoRemove)
            Next
            newMaze.AddRange(surroundingNodes)
        Next
        Console.ResetColor()
        Console.Clear()
        For Each node In copyMaze
            Dim potentialAdjacentCell As New Node(node.X + 4, node.Y)
            If copyMaze.Contains(potentialAdjacentCell) Then newMaze.Remove(New Node(node.X + 2, node.Y))
            potentialAdjacentCell.Update(node.X - 4, node.Y)
            If copyMaze.Contains(potentialAdjacentCell) Then newMaze.Remove(New Node(node.X - 2, node.Y))
            potentialAdjacentCell.Update(node.X, node.Y + 2)
            If copyMaze.Contains(potentialAdjacentCell) Then newMaze.Remove(New Node(node.X, node.Y + 1))
            potentialAdjacentCell.Update(node.X, node.Y - 2)
            If copyMaze.Contains(potentialAdjacentCell) Then newMaze.Remove(New Node(node.X, node.Y - 1))
        Next
        Dim gx, gy As Integer
        For Each node In newMaze
            If node.X > gx Then gx = node.X
            If node.Y > gy Then gy = node.Y
        Next
        Dim limits() As Integer = {5, 3, gx, gy}
        if backGroundColour <> ConsoleColor.Black then DrawBackground(backGroundColour,{6, 3, gx+1, gy})
          AddStartAndEnd(newMaze, limits, pathcolour, True)
        SetBoth(pathcolour)
        for each node in newMaze
            node.Print("XX")
        Next
        Return newMaze
    End function
End Module
