Module Best_First_Search
    Sub bfs(g As List(Of Node), showPath As Boolean, showSolveTime As Boolean, delay As Integer, solvingColour As ConsoleColor)
        Dim startV = getStart(g)
        Dim goal = getGoal(g)
        Dim discovered As New Dictionary(Of Node, Boolean)
        Dim q As New PriorityQueue(Of Node)
        Dim cameFrom As New Dictionary(Of Node, Node)
        For Each node In g
            discovered(node) = False
        Next
        discovered(startV) = True
        q.Enqueue(startV)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(solvingColour)
        While Not q.IsEmpty()
            Dim v As Node = q.ExtractMin()
            If showPath Then : v.Print("██") : Threading.Thread.Sleep(delay) : End If
            If v.Equals(goal) Then Exit While
            For Each w As Node In GetNeighbours(v, g)
                If Not discovered(w) Then
                    discovered(w) = True
                    q.Enqueue(w, H(w, goal, 1))
                    cameFrom(w) = v
                End If
            Next
        End While
        ReconstructPath(cameFrom, goal, startV, If(showSolveTime, $"{stopwatch.Elapsed.TotalSeconds}", ""))
        Console.ReadKey()
    End Sub
End Module
