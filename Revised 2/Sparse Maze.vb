Module Sparse_Maze
    Sub Sparsify(ByVal Maze As List(Of Node))
        Dim DeadEnds As New List(Of Node)
        Dim Start As New Node(Maze(Maze.Count - 2).X, Maze(Maze.Count - 2).Y)
        Dim Goal As New Node(Maze(Maze.Count - 1).X, Maze(Maze.Count - 1).Y)
        Dim Visited As New Dictionary(Of Node, Boolean)
        Dim FillColour As ConsoleColor = ConsoleColor.Black
        Dim EditedMaze As New List(Of Node)
        Dim NotPath As New List(Of Node)
        Dim r As New Random
        Dim GreatestX, GreatestY As Integer
        GreatestX = 0
        GreatestY = 0
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.Black
        Console.SetCursorPosition(1, 1)
        Console.Write("Current Process: Finding dead ends")
        Console.ForegroundColor = FillColour
        Console.BackgroundColor = FillColour
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        For Each node In Maze
            If node.X > GreatestX Then GreatestX = node.X
            If node.Y > GreatestY Then GreatestY = node.Y
            If node.IsDeadEnd(Maze) Then
                If node.Equals(Start) Or node.Equals(Goal) Then Continue For
                DeadEnds.Add(node)
            End If
            Visited(node) = False
        Next
        If DeadEnds.Count > 0 Then
            Console.ForegroundColor = ConsoleColor.White
            Console.BackgroundColor = ConsoleColor.Black
            Console.SetCursorPosition(1, 1)
            Console.Write("Current Process: Filling dead ends      ")
            Console.ForegroundColor = FillColour
            Console.BackgroundColor = FillColour
            For Each deadEnd In DeadEnds
                If r.Next(1, 3) = 1 Then
                    Dim StartingCell As Node = deadEnd
                    StartingCell.Print("██")
                    Maze.Remove(StartingCell)
                    Visited(StartingCell) = True
                    NotPath.Add(StartingCell)
                    While Not StartingCell.IsJunction(Maze)
                        Dim Neighbours As List(Of Node) = GetNeighbours(StartingCell, Maze)
                        For Each NeighbourNode In Neighbours
                            If NeighbourNode.IsJunction(Maze) Then
                                Maze.Remove(StartingCell)
                                Exit While
                            End If
                            Maze.Remove(StartingCell)
                            If Visited(NeighbourNode) Then Continue For
                            StartingCell = NeighbourNode
                            StartingCell.Print("██")
                            NotPath.Add(StartingCell)
                            Visited(NeighbourNode) = True
                        Next
                    End While
                End If
            Next
        Else
            Console.ForegroundColor = ConsoleColor.Green
            Console.BackgroundColor = ConsoleColor.Green
            For Each Node In Maze
                If Not NotPath.Contains(Node) Then Node.Print("██")
            Next
            Console.ForegroundColor = ConsoleColor.White
            Console.BackgroundColor = ConsoleColor.Black
        End If
    End Sub
End Module