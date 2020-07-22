Module IDAstar
    Sub ida_star(maze As List(Of Node), showPath As Boolean, delay As Integer, solvingColour As ConsoleColor)
        Dim root = getStart(maze)
        Dim goal = getGoal(maze)
        Dim bound = manhattenDistance(root, goal)
        Dim path As New Stack(Of Node)
        Dim cameFrom As New Dictionary(Of Node, Node)
        path.Push(root)
        Dim timetaken As Stopwatch = Stopwatch.StartNew()
        Do
            Dim t = search(path, 0, bound, goal, maze, cameFrom, showPath, delay, solvingColour)
            If t = -1 Then
                ReconstructPath(cameFrom, goal, root, timetaken.Elapsed.TotalSeconds)
                Console.ReadKey()
                Exit Sub
            ElseIf t = -2
                Exit Sub
            End If
            bound = t
        Loop
    End Sub
    Function search(path As Stack(Of Node), g As Integer, bound As Integer, goal As Node, maze As List(Of Node), ByRef camefrom As Dictionary(Of Node, Node), showPath As Boolean, delay As Integer, solvingColour As ConsoleColor)
        Dim node = path.Peek()
        If showPath Then
            SetBoth(solvingColour)
            node.Print("XX")
        End If
        Dim f = g + manhattenDistance(node, goal)
        if ExitCase() Then Return -2
        If f > bound Then Return f
        If node.Equals(goal) Then Return -1
        Dim min = Int32.MaxValue
        SetBoth(ConsoleColor.Red)
        For Each neighbour As Node In GetNeighbours(node, maze)
            If Not path.Contains(neighbour) Then
                If showPath Then
                    neighbour.Print("XX")
                    Threading.Thread.Sleep(delay)
                End If
                path.Push(neighbour)
                camefrom(neighbour) = node
                Dim t = search(path, g + manhattenDistance(node, neighbour), bound, goal, maze, camefrom, showPath, delay, solvingColour)
                If t = -1 Then Return -1
                if t = -2 Then Return -2
                If t < min Then min = t
                path.Pop()
            End If
        Next
        Return min
    End Function
    Function manhattenDistance(node1 As Node, node2 As Node)
        Return Math.Abs(node1.X - node2.X) + Math.Abs(node1.Y - node2.Y)
    End Function
End Module