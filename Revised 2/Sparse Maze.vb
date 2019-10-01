Module SparseMaze
    Sub Sparsify(maze As List(Of Node),pathColour as ConsoleColor,backGroundColour as ConsoleColor)
        Dim deadEnds As New List(Of Node)
        Dim start As New Node(maze(maze.Count - 2).X, maze(maze.Count - 2).Y)
        Dim goal As New Node(maze(maze.Count - 1).X, maze(maze.Count - 1).Y)
        Dim visited As New Dictionary(Of Node, Boolean)
        Dim fillColour = backgroundcolour
        Dim notPath As New List(Of Node)
        Dim r As New Random
        Dim greatestX, greatestY As Integer
        greatestX = 0
        greatestY = 0
        SetBoth(fillColour)
        For Each node In maze
            If node.X > greatestX Then greatestX = node.X
            If node.Y > greatestY Then greatestY = node.Y
            If node.IsDeadEnd(maze) Then
                If node.Equals(start) Or node.Equals(goal) Then Continue For
                deadEnds.Add(node)
            End If
            visited(node) = False
        Next
        If deadEnds.Count > 0 Then
            For Each deadEnd In deadEnds
                If r.Next(1, 3) = 1 Then
                    Dim startingCell As Node = deadEnd
                    startingCell.Print("██")
                    maze.Remove(startingCell)
                    visited(startingCell) = True
                    notPath.Add(startingCell)
                    While Not startingCell.IsJunction(maze)
                        Dim neighbours As List(Of Node) = GetNeighbours(startingCell, maze)
                        if neighbours.count = 0 then Exit While
                        For Each NeighbourNode In neighbours
                            If NeighbourNode.IsJunction(maze) Then
                                maze.Remove(startingCell)
                                Exit While
                            End If
                            maze.Remove(startingCell)
                            If visited.ContainsKey(NeighbourNode) AndAlso visited(NeighbourNode) Then Continue For
                            startingCell = NeighbourNode
                            startingCell.Print("██")
                            notPath.Add(startingCell)
                            visited(NeighbourNode) = True
                        Next
                    End While
                End If
            Next
        Else
            Console.ForegroundColor = ConsoleColor.Green
            Console.BackgroundColor = ConsoleColor.Green
            For Each Node In maze
                If Not notPath.Contains(Node) Then Node.Print("██")
            Next
            Console.ForegroundColor = ConsoleColor.White
            Console.BackgroundColor = ConsoleColor.Black
        End If
    End Sub
End Module