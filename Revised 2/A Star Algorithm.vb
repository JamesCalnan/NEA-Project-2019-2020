Module AStarAlgorithm
    Sub SetBoth(colour As ConsoleColor)
        Console.BackgroundColor = colour
        Console.ForegroundColor = colour
    End Sub
    Sub AStar(availablepath As List(Of Node), showPath As Boolean, showSolveTime As Boolean, delay As Integer,  solvingColour as ConsoleColor)
        Dim start As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim target As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim current As Node = start
        Dim openSet, closedSet As New HashSet(Of Node)
        SetBoth(SolvingColour)
        openSet.Add(current)
        current.GCost = 0
        current.HCost = H(current, target, 1)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While openSet.Count > 0
            If ExitCase() Then
                Exit While
            End If
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
                    neighbour.HCost = H(neighbour, target, 1) 'GetDistance(target, Neighbour)
                    neighbour.Parent = current
                    openSet.Add(neighbour)
                End If
            Next
        End While
       Console.ReadKey()
    End Sub
    Sub AStarWiki(adjacencyList As Dictionary(Of Node, List(Of Node)), showPath As Boolean, showSolveTime As Boolean, delay As Integer,  solvingColour as ConsoleColor)
        Dim openSet, closedSet As New List(Of Node)
        Dim start As New Node(adjacencyList.Keys(adjacencyList.Count - 2).X, adjacencyList.Keys(adjacencyList.Count - 2).Y)
        Dim goal As New Node(adjacencyList.Keys(adjacencyList.Count - 1).X, adjacencyList.Keys(adjacencyList.Count - 1).Y)
        Dim gScore, fScore As New Dictionary(Of Node, Double)
        Dim cameFrom As New Dictionary(Of Node, Node)
        Dim infinity As Integer = Int32.MaxValue
        For Each node In adjacencyList.Keys
            gScore(node) = infinity
            fScore(node) = infinity
        Next
        Dim heuristic As Double = 5
        gScore(start) = 0
        fScore(start) = H(start, goal, heuristic)
        openSet.Add(start)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(solvingcolour)
        While openSet.Count > 0
            Dim current As Node = ExtractMin(openSet, fScore)
            If current.Equals(goal) Then Exit While
            openSet.Remove(current)
            closedSet.Add(current)
            If showPath Then : current.Print("██") : Threading.Thread.Sleep(delay) : End If
            For Each neighbour As Node In GetNeighboursAd(current, adjacencyList)
                If closedSet.Contains(neighbour) Then Continue For
                Dim tentativeGScore = gScore(current) + H(current, neighbour, heuristic)
                If tentativeGScore <= gScore(neighbour) Then
                    cameFrom(neighbour) = current
                    gScore(neighbour) = tentativeGScore
                    fScore(neighbour) = gScore(neighbour) + H(goal, neighbour, heuristic)
                    If Not openSet.Contains(neighbour) Then openSet.Add(neighbour)
                End If
            Next
        End While
        ReconstructPath(cameFrom, goal, start, If(showSolveTime, $"{stopwatch.Elapsed.TotalSeconds}", ""))
Console.ReadKey()
    End Sub
End Module
