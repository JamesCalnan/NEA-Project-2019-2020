Module DeadEndFillerMethod
    Sub DeadEndFiller(list As List(Of Node), showPath As Boolean, showSolveTime As Boolean, delay As Integer, pathColour as ConsoleColor, solvingcolour as ConsoleColor)
        Dim deadEnds As New List(Of Node)
        Dim start As New Node(list(list.Count - 2).X, list(list.Count - 2).Y)
        Dim goal As New Node(list(list.Count - 1).X, list(list.Count - 1).Y)
        Dim visited As New Dictionary(Of Node, Boolean)
        Dim fillColour = ConsoleColor.DarkCyan
        Dim maze As New List(Of Node)
        Dim editedMaze As New List(Of Node)
        Dim notPath As New List(Of Node)
        Dim greatestX, greatestY As Integer
        greatestX = 0
        greatestY = 0
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.Black
        Console.SetCursorPosition(1, 1)
        Console.Write("Current Process: Finding dead ends")
        SetBoth(solvingcolour)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        For Each node In list
            maze.Add(node)
            If node.X > greatestX Then greatestX = node.X
            If node.Y > greatestY Then greatestY = node.Y
            If node.IsDeadEnd(list) Then
                If node.Equals(start) Or node.Equals(goal) Then Continue For
                If showPath Then node.Print("██")
                deadEnds.Add(node)
            End If
            visited(node) = False
        Next
        If deadEnds.Count > 0 Then
            Console.ForegroundColor = ConsoleColor.White
            Console.BackgroundColor = ConsoleColor.Black
            Console.SetCursorPosition(1, 1)
            Console.Write("Current Process: Filling dead ends      ")
            Console.ForegroundColor = fillColour
            Console.BackgroundColor = fillColour
            For Each deadEnd In deadEnds
                Dim startingCell As Node = deadEnd
                visited(startingCell) = True
                If showPath Then startingCell.Print("██")
                notPath.Add(startingCell)
                While Not startingCell.IsJunction(maze)
                    Dim neighbours As List(Of Node) = GetNeighbours(startingCell, maze)
                    For Each NeighbourNode In neighbours
                        If NeighbourNode.IsJunction(maze) Then
                            maze.Remove(startingCell)
                            Exit While
                        End If
                        If visited(NeighbourNode) Then Continue For
                        startingCell = NeighbourNode
                        If showPath Then startingCell.Print("██")
                        notPath.Add(startingCell)
                        visited(NeighbourNode) = True
                    Next
                    Threading.Thread.Sleep(delay)
                End While
            Next
            Console.ForegroundColor = ConsoleColor.White
            Console.BackgroundColor = ConsoleColor.Black
            Console.SetCursorPosition(1, 1)
            Console.Write("                                             ")
            setboth(ConsoleColor.Green)
            If showPath Then start.Print("██")
            PrintMessageMiddle($"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds", Console.WindowHeight - 1, ConsoleColor.Green)
            If showPath Then
                For __x = 1 To greatestX + 1
                    For __y = 3 To greatestY + 1
                        If list.Contains(New Node(__x, __y)) Then
                            If Not notPath.Contains(New Node(__x, __y)) Then
                                setboth(ConsoleColor.Green)
                                Console.SetCursorPosition(__x, __y)
                                Console.Write("██")
                            Else
                                SetBoth(pathColour)
                                Console.SetCursorPosition(__x, __y)
                                If showPath Then Console.Write("██")
                            End If
                        End If
                    Next
                    Threading.Thread.Sleep(delay)
                Next
            Else
                setboth(ConsoleColor.Green)
                For Each Node In maze
                    If Not notPath.Contains(Node) Then Node.Print("██")
                Next
            End If
        Else
            setboth(ConsoleColor.Black)
            Console.SetCursorPosition(1, 1)
            Console.Write("                                             ")
            PrintMessageMiddle("No deadends detected", Console.WindowHeight - 1, ConsoleColor.Red)
        End If
        Console.ReadKey()
    End Sub
End Module