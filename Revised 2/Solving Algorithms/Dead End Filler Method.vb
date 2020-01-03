Module DeadEndFillerMethod
    Function DeadEndFiller(g As List(Of Node), showPath As Boolean, delay As Integer, pathColour As ConsoleColor, solvingcolour As ConsoleColor, Optional fillPath As Boolean = True, Optional printmessages As Boolean = True)
        Dim deadEnds As New List(Of Node)
        Dim start = getStart(g)
        Dim goal = getGoal(g)
        Dim visited As New Dictionary(Of Node, Boolean)
        Dim maze As New List(Of Node)
        Dim copyMaze As New List(Of Node)
        Dim notPath As New List(Of Node)
        Dim greatestX, greatestY As Integer
        greatestX = 0
        greatestY = 0
        If printmessages Then
            Console.ForegroundColor = ConsoleColor.White
            Console.BackgroundColor = ConsoleColor.Black
            Console.SetCursorPosition(1, 1)
            Console.Write("Current Process: Finding dead ends")
        End If
        SetBoth(solvingcolour)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        For Each node In g
            maze.Add(node)
            copyMaze.Add(node)
            If node.X > greatestX Then greatestX = node.X
            If node.Y > greatestY Then greatestY = node.Y
            If node.IsDeadEnd(g) Then
                If node.Equals(start) Or node.Equals(goal) Then Continue For
                If showPath Then node.Print("██")
                deadEnds.Add(node)
            End If
            visited(node) = False
        Next
        If deadEnds.Count > 0 Then
            If printmessages Then
                Console.ForegroundColor = ConsoleColor.White
                Console.BackgroundColor = ConsoleColor.Black
                Console.SetCursorPosition(1, 1)
                Console.Write("Current Process: Filling dead ends      ")
            End If
            SetBoth(solvingcolour)
            For Each deadEnd In deadEnds
                Dim startingCell As Node = deadEnd
                copyMaze.Remove(startingCell)
                visited(startingCell) = True
                If showPath Then startingCell.Print("██")
                notPath.Add(startingCell)
                While Not startingCell.IsJunction(maze)
                    Dim neighbours As List(Of Node) = GetNeighbours(startingCell, maze)
                    For Each NeighbourNode In neighbours
                        If NeighbourNode.IsJunction(maze) Then
                            maze.Remove(startingCell)
                            copyMaze.Remove(startingCell)
                            Exit While
                        End If
                        If visited(NeighbourNode) Then Continue For
                        startingCell = NeighbourNode
                        copyMaze.Remove(startingCell)
                        If showPath Then startingCell.Print("██")
                        notPath.Add(startingCell)
                        visited(NeighbourNode) = True
                    Next
                    Threading.Thread.Sleep(delay)
                End While
            Next
            If printmessages Then
                Console.ForegroundColor = ConsoleColor.White
                Console.BackgroundColor = ConsoleColor.Black
                Console.SetCursorPosition(1, 1)
                Console.Write("                                             ")
                SetBoth(ConsoleColor.Green)
                If showPath Then start.Print("██")
                PrintMessageMiddle($"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds", Console.WindowHeight - 1, ConsoleColor.Green)
            End If
            If fillPath Then
                If showPath Then
                    For __x = 1 To greatestX + 1
                        For __y = 3 To greatestY + 1
                            If g.Contains(New Node(__x, __y)) Then
                                If Not notPath.Contains(New Node(__x, __y)) Then
                                    SetBoth(ConsoleColor.Green)
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
                    SetBoth(ConsoleColor.Green)
                    For Each Node In From Node1 In maze Where Not notPath.Contains(Node1)
                        Node.Print("██")
                    Next
                End If
                Console.ReadKey()
            End If
        Else
            SetBoth(ConsoleColor.Black)
            Console.SetCursorPosition(1, 1)
            Console.Write("                                             ")
            PrintMessageMiddle("No deadends detected", Console.WindowHeight - 1, ConsoleColor.Red)
            Console.ReadKey()
        End If
        If Not fillPath Then
            Return copyMaze
        End If

    End Function
End Module