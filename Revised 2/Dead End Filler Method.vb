Module Dead_End_Filler_Method
    Sub DeadEndFiller(ByVal List As List(Of Node), ByVal ShowPath As Boolean, ByVal ShowSolveTime As Boolean, ByVal Delay As Integer, ByVal Evaluation As Boolean)
        Dim DeadEnds As New List(Of Node)
        Dim Start As New Node(List(List.Count - 2).X, List(List.Count - 2).Y)
        Dim Goal As New Node(List(List.Count - 1).X, List(List.Count - 1).Y)
        Dim Visited As New Dictionary(Of Node, Boolean)
        Dim FillColour As ConsoleColor = ConsoleColor.DarkCyan
        Dim Maze As New List(Of Node)
        Dim EditedMaze As New List(Of Node)
        Dim NotPath As New List(Of Node)
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
        For Each node In List
            Maze.Add(node)
            If node.X > GreatestX Then GreatestX = node.X
            If node.Y > GreatestY Then GreatestY = node.Y
            If node.IsDeadEnd(List) Then
                If node.Equals(Start) Or node.Equals(Goal) Then Continue For
                If ShowPath Then node.Print("██")
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
                Dim StartingCell As Node = deadEnd
                Visited(StartingCell) = True
                If ShowPath Then StartingCell.Print("██")
                NotPath.Add(StartingCell)
                While Not StartingCell.IsJunction(Maze)
                    Dim Neighbours As List(Of Node) = GetNeighbours(StartingCell, Maze)
                    For Each NeighbourNode In Neighbours
                        If NeighbourNode.IsJunction(Maze) Then
                            Maze.Remove(StartingCell)
                            Exit While
                        End If
                        If Visited(NeighbourNode) Then Continue For
                        StartingCell = NeighbourNode
                        If ShowPath Then StartingCell.Print("██")
                        NotPath.Add(StartingCell)
                        Visited(NeighbourNode) = True
                    Next
                    Threading.Thread.Sleep(Delay)
                End While
            Next
            Console.ForegroundColor = ConsoleColor.White
            Console.BackgroundColor = ConsoleColor.Black
            Console.SetCursorPosition(1, 1)
            Console.Write("                                             ")
            Console.ForegroundColor = ConsoleColor.Green
            Console.BackgroundColor = ConsoleColor.Green
            If ShowPath Then Start.Print("██")
            PrintMessageMiddle($"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds", Console.WindowHeight - 1, ConsoleColor.Green)
            If ShowPath Then
                For __x = 1 To GreatestX + 1
                    For __y = 3 To GreatestY + 1
                        If List.Contains(New Node(__x, __y)) Then
                            If Not NotPath.Contains(New Node(__x, __y)) Then
                                Console.ForegroundColor = ConsoleColor.Green
                                Console.BackgroundColor = ConsoleColor.Green
                                Console.SetCursorPosition(__x, __y)
                                Console.Write("██")
                            Else
                                Console.ForegroundColor = ConsoleColor.White
                                Console.BackgroundColor = ConsoleColor.White
                                Console.SetCursorPosition(__x, __y)
                                If ShowPath Then Console.Write("██")
                            End If
                        End If
                    Next
                    Threading.Thread.Sleep(Delay)
                Next
            Else
                Console.ForegroundColor = ConsoleColor.Green
                Console.BackgroundColor = ConsoleColor.Green
                For Each Node In Maze
                    If Not NotPath.Contains(Node) Then Node.Print("██")
                Next
            End If
        Else
            Console.ForegroundColor = ConsoleColor.Black
            Console.BackgroundColor = ConsoleColor.Black
            Console.SetCursorPosition(1, 1)
            Console.Write("                                             ")
            PrintMessageMiddle("No deadends detected", Console.WindowHeight - 1, ConsoleColor.Red)
        End If
        Console.ReadKey()
    End Sub
End Module