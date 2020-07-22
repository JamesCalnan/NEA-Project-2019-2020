Module AStarAlgorithm
    Sub SetBoth(colour As ConsoleColor)
        Console.BackgroundColor = colour
        Console.ForegroundColor = colour
    End Sub
    ''' <summary>
    ''' A star algorithm according the the video series produced by sebastian lague
    ''' </summary>
    Sub AStar(g As List(Of Node), showPath As Boolean, showSolveTime As Boolean, delay As Integer, heuristic As Double, solvingColour As ConsoleColor)
        Dim start = getStart(g)
        Dim target = getGoal(g)
        Dim current As Node = start
        Dim openSet, closedSet As New HashSet(Of Node)
        SetBoth(solvingColour)
        openSet.Add(current)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While openSet.Count > 0
            If ExitCase() Then Exit While
            current = openSet(0)
            For i = 1 To openSet.Count - 1
                If openSet(i).FCost() <= current.FCost() Or openSet(i).HCost = current.HCost Then If openSet(i).HCost < current.HCost Then current = openSet(i)
            Next
            openSet.Remove(current)
            closedSet.Add(current)
            If showPath Then : current.Print("██") : Threading.Thread.Sleep(delay) : End If
            If current.Equals(target) Then
                RetracePath(start, current, If(showSolveTime, $"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds", ""))
                Console.ReadKey()
                Exit Sub
            End If
            For Each neighbour As Node In GetNeighbours(current, g)
                If closedSet.Contains(neighbour) Then Continue For
                Dim tentativeGScore = current.GCost + 1
                If tentativeGScore < neighbour.GCost Or Not openSet.Contains(neighbour) Then
                    neighbour.GCost = tentativeGScore
                    neighbour.HCost = H(neighbour, target, heuristic) 'GetDistance(target, Neighbour)
                    neighbour.Parent = current
                    openSet.Add(neighbour)
                End If
            Next
        End While
        displayPathNotFoundMessage
    End Sub
    Sub AStarWiki(g As List(Of Node), showPath As Boolean, showSolveTime As Boolean, delay As Integer, heuristic As Double, solvingColour As ConsoleColor, Optional useDiagonals As Boolean = False)
        Dim closedSet, visitedSet As New List(Of Node)
        Dim openSet As New PriorityQueue(Of Node)
        Dim start As Node = getStart(g)
        Dim goal As Node = getGoal(g)
        Dim gScore As New Dictionary(Of Node, Double)
        Dim cameFrom As New Dictionary(Of Node, Node)
        Dim infinity As Integer = Int32.MaxValue
        SetBoth(ConsoleColor.Red)
        For Each node In g
            gScore(node) = infinity
        Next
        gScore(start) = 0
        'fScore(start) = H(start, goal, heuristic)
        openSet.Enqueue(start, H(start, goal, heuristic))
        visitedSet.Add(start)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(solvingColour)
        While Not openSet.IsEmpty()
            Dim current As Node = openSet.ExtractMin()
            If current.Equals(goal) Then
                ReconstructPath(cameFrom, goal, start, If(showSolveTime, $"{stopwatch.Elapsed.TotalSeconds}", ""))
                Console.ReadKey()
                Exit sub
            End If
            visitedSet.Add(current)

            If showPath Then : current.Print("██") : Threading.Thread.Sleep(delay) : End If

            For Each neighbour As Node In GetNeighbours(current, g, useDiagonals)
                If visitedSet.Contains(neighbour) Then Continue For
                Dim tentativeGScore = gScore(current) + 1

                If tentativeGScore <= gScore(neighbour) Then
                    cameFrom(neighbour) = current
                    gScore(neighbour) = tentativeGScore
                    If Not visitedSet.Contains(neighbour) Then
                        openSet.Enqueue(neighbour, gScore(neighbour) + H(goal, neighbour, heuristic))
                        visitedSet.Add(neighbour)
                    End If
                End If
            Next
        End While
        displayPathNotFoundMessage
    End Sub
    sub displayPathNotFoundMessage
        Console.ForegroundColor = ConsoleColor.Red
        Console.BackgroundColor = ConsoleColor.Black
        Console.SetCursorPosition(Console.WindowWidth\2 - "Path not found!".Length\2,0)
        Console.Write("Path not found!")
        Console.ReadKey()
    End sub
    Function getStart(maze As List(Of Node)) As Node
        Return maze(maze.Count - 2)
    End Function
    Function getGoal(maze As List(Of Node)) As Node
        Return maze(maze.Count - 1)
    End Function
End Module
