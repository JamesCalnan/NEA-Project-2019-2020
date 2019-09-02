Module A_Star_Algorithm
    Sub aStar(ByVal availablepath As List(Of Node), ByVal ShowPath As Boolean, ByVal ShowSolveTime As Boolean, ByVal Delay As Integer)
        Dim start As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim target As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim current As Node = start
        Dim openSet, closedSet As New HashSet(Of Node)
        SetBoth(ConsoleColor.Red)
        openSet.Add(current)
        current.gCost = 0
        current.hCost = h(current, target, 1)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While openSet.Count > 0
            If ExitCase() Then Exit While
            current = openSet(0)
            For i = 1 To openSet.Count - 1
                If openSet(i).fCost() <= current.fCost() Or openSet(i).hCost = current.hCost Then If openSet(i).hCost < current.hCost Then current = openSet(i)
            Next
            openSet.Remove(current)
            closedSet.Add(current)
            If ShowPath Then : current.Print("██") : Threading.Thread.Sleep(Delay) : End If
            If current.Equals(target) Then Exit While
            For Each Neighbour As Node In GetNeighbours(current, availablepath)
                If closedSet.Contains(Neighbour) Then Continue For
                Dim tentative_gScore = current.gCost + 1
                If tentative_gScore < Neighbour.gCost Or Not openSet.Contains(Neighbour) Then
                    Neighbour.gCost = tentative_gScore
                    Neighbour.hCost = h(Neighbour, target, 1) 'GetDistance(target, Neighbour)
                    Neighbour.parent = current
                    openSet.Add(Neighbour)
                End If
            Next
        End While
        RetracePath(start, current, If(ShowSolveTime, $"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds", ""))
        Console.ReadKey()
    End Sub
    Sub aStarWiki(ByVal AdjacencyList As Dictionary(Of Node, List(Of Node)), ByVal ShowPath As Boolean, ByVal ShowSolveTime As Boolean, ByVal Delay As Integer)
        Dim openSet, closedSet As New List(Of Node)
        Dim start As New Node(AdjacencyList.Keys(AdjacencyList.Count - 2).X, AdjacencyList.Keys(AdjacencyList.Count - 2).Y)
        Dim goal As New Node(AdjacencyList.Keys(AdjacencyList.Count - 1).X, AdjacencyList.Keys(AdjacencyList.Count - 1).Y)
        Dim gScore, fScore As New Dictionary(Of Node, Double)
        Dim cameFrom As New Dictionary(Of Node, Node)
        Dim INFINITY As Integer = Int32.MaxValue
        For Each node In AdjacencyList.Keys
            gScore(node) = INFINITY
            fScore(node) = INFINITY
        Next
        Dim heuristic As Double = 5
        gScore(start) = 0
        fScore(start) = h(start, goal, heuristic)
        openSet.Add(start)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(ConsoleColor.Red)
        While openSet.Count > 0
            Dim current As Node = ExtractMin(openSet, fScore)
            If current.Equals(goal) Then Exit While
            openSet.Remove(current)
            closedSet.Add(current)
            If ShowPath Then : current.Print("██") : Threading.Thread.Sleep(Delay) : End If
            For Each Neighbour As Node In GetNeighboursAd(current, AdjacencyList)
                If closedSet.Contains(Neighbour) Then Continue For
                Dim tentative_gScore = gScore(current) + 1
                If tentative_gScore <= gScore(Neighbour) Then
                    cameFrom(Neighbour) = current
                    gScore(Neighbour) = tentative_gScore
                    fScore(Neighbour) = gScore(Neighbour) + h(goal, Neighbour, heuristic)
                    If Not openSet.Contains(Neighbour) Then openSet.Add(Neighbour)
                End If
            Next
        End While
        ReconstructPath(cameFrom, goal, start, If(ShowSolveTime, $"{stopwatch.Elapsed.TotalSeconds}", ""))
    End Sub
End Module
