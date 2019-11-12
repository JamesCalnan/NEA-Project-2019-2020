Module AStarAlgorithm
    Sub SetBoth(colour As ConsoleColor)
        Console.BackgroundColor = colour
        Console.ForegroundColor = colour
    End Sub
    ''' <summary>
    ''' A star algorithm according the the video series produced by sebastian lague
    ''' </summary>
    Sub AStar(availablepath As List(Of Node), showPath As Boolean, showSolveTime As Boolean, delay As Integer, heuristic As Double, solvingColour As ConsoleColor)
        Dim start = GetStart(availablepath)
        Dim target = GetGoal(availablepath)
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
                Exit While
            End If
            For Each neighbour As Node In GetNeighbours(current, availablepath)
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
        Console.ReadKey()
    End Sub
    Sub AStarWiki(availablepath As List(Of Node), showPath As Boolean, showSolveTime As Boolean, delay As Integer, heuristic As Double, solvingColour As ConsoleColor)
        Dim closedSet, visitedSet As New List(Of Node)
        Dim openSet As New PriorityQueue(Of Node)
        Dim start As Node = GetStart(availablepath)
        Dim goal As Node = GetGoal(availablepath)
        Dim gScore As New Dictionary(Of Node, Double)
        Dim cameFrom As New Dictionary(Of Node, Node)
        Dim infinity As Integer = Int32.MaxValue
        For Each node In availablepath
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
            If current.Equals(goal) Then Exit While
            visitedSet.Add(current)
            If showPath Then : current.Print("██") : Threading.Thread.Sleep(delay) : End If
            For Each neighbour As Node In GetNeighbours(current, availablepath)
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
        ReconstructPath(cameFrom, goal, start, If(showSolveTime, $"{stopwatch.Elapsed.TotalSeconds}", ""))
        Console.ReadKey()
    End Sub
    Function getStart(maze As List(Of Node)) As Node
        Return maze(maze.Count - 2)
    End Function
    Function getGoal(maze As List(Of Node)) As Node
        Return maze(maze.Count - 1)
    End Function
End Module
