Module SparseMaze
    Sub Sparsify(maze As List(Of Node),pathColour as ConsoleColor,backGroundColour as ConsoleColor)
        Dim deadEnds As New List(Of Node)
        Dim start As New Node(maze(maze.Count - 2).X, maze(maze.Count - 2).Y)
        Dim goal As New Node(maze(maze.Count - 1).X, maze(maze.Count - 1).Y)
        Dim visited As New Dictionary(Of Node, Boolean)
        Dim fillColour = backGroundColour
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
                If r.Next(101) < 33 Then
                    Dim startingCell As Node = deadEnd
                    visited(startingCell) = True
                    startingCell.Print("██")
                    While Not startingCell.IsJunction(maze)
                        Dim neighbours As List(Of Node) = GetNeighbours(startingCell, maze)
                        For Each NeighbourNode In neighbours
                            If NeighbourNode.IsJunction(maze) Then
                                maze.Remove(startingCell)
                                Exit While
                            End If
                            maze.Remove(startingCell)
                            If visited(NeighbourNode) Then Continue For
                            startingCell = NeighbourNode
                            startingCell.Print("██")
                            visited(NeighbourNode) = True
                        Next
                    End While
                End If
            Next
        End If
    End Sub
End Module