Module Best_First_Search
    Sub bfs(g As List(Of Node), showPath As Boolean, showSolveTime As Boolean, delay As Integer, solvingColour As ConsoleColor, Optional useDiagonals As Boolean = False)
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
            If v.Equals(goal) Then
                ReconstructPath(cameFrom, goal, startV, If(showSolveTime, $"{stopwatch.Elapsed.TotalSeconds}", ""))
                Console.ReadKey()
                Exit Sub
            End If
            For Each w As Node In GetNeighbours(v, g, useDiagonals)
                If Not discovered(w) Then
                    discovered(w) = True
                    q.Enqueue(w, H(w, goal, 1))
                    cameFrom(w) = v
                End If
            Next
        End While
        displayPathNotFoundMessage
    End Sub
End Module
