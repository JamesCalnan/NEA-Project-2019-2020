Module BreadthFirstSearch
    Sub Bfs(g As List(Of Node), showPath As Boolean, showSolveTime As Boolean, delay As Integer, solvingColour As ConsoleColor, Optional useDiagonals As Boolean = False)
        Dim startV = getStart(g)
        Dim goal = getGoal(g)
        Dim discovered As New Dictionary(Of Node, Boolean)
        Dim q As New Queue(Of Node)
        Dim cameFrom As New Dictionary(Of Node, Node)
        For Each node In g
            discovered(node) = False
        Next
        discovered(startV) = True
        q.Enqueue(startV)
        SetBoth(solvingColour)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While q.Count > 0
            Dim v As Node = q.Dequeue
            If showPath Then : v.Print("██") : Threading.Thread.Sleep(delay) : End If
            If v.Equals(goal) Then
                ReconstructPath(cameFrom, goal, startV, If(showSolveTime, $"{stopwatch.Elapsed.TotalSeconds}", ""))
                Console.ReadKey()
                Exit sub
            End If
            For Each w As Node In GetNeighbours(v, g, useDiagonals)
                If Not discovered(w) Then
                    discovered(w) = True
                    q.Enqueue(w)
                    cameFrom(w) = v
                End If
            Next
        End While
        displayPathNotFoundMessage()
    End Sub
    Sub BfsRecursive(g As List(Of Node), q As Queue(Of Node), discovered As Dictionary(Of Node, Boolean), camefrom As Dictionary(Of Node, Node), goal As Node, solvingdelay As Integer, showsolving As Boolean)
        If q.Count = 0 Then Return
        Dim v As Node = q.Dequeue()
        If showsolving Then
            v.Print("XX")
            Threading.Thread.Sleep(solvingdelay)
        End If
        If v.Equals(goal) Then

            Return
        End If
        For Each u As Node In GetNeighbours(v, g)
            If Not discovered(u) Then
                discovered(u) = True
                q.Enqueue(u)
                camefrom(u) = v
            End If
        Next
        BfsRecursive(g, q, discovered, camefrom, goal, solvingdelay, showsolving)
    End Sub
End Module
