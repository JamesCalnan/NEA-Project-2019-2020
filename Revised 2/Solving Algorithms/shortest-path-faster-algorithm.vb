Module shortest_path_faster_algorithm
    Sub SPFA(maze As List(Of Node), showSolving As Boolean, solvingDelay As Integer, solvingColour As ConsoleColor)
        Dim s = getStart(maze)
        Dim target = getGoal(maze)
        Dim d As New Dictionary(Of Node, Double)
        Dim prev As New Dictionary(Of Node, Node)
        Dim Q As New Queue(Of Node)
        d(s) = 0
        For Each v In maze
            If v.Equals(s) Then Continue For
            d(v) = Int32.MaxValue / 2
            prev(v) = Nothing
        Next
        SetBoth(solvingColour)
        Q.Enqueue(s)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While Q.Count > 0
            Dim u = Q.Dequeue
            If u.Equals(target) Then Exit While
            If showSolving Then : u.Print("██") : Threading.Thread.Sleep(solvingDelay) : End If
            For Each v As Node In GetNeighbours(u, maze)
                If d(u) + 1 < d(v) Then
                    d(v) = d(u) + 1
                    prev(v) = u
                    If Not Q.Contains(v) Then
                        Q.Enqueue(v)
                        Small_Label_First(d, Q)
                    End If
                End If
            Next
        End While
        SetBoth(solvingColour)
        ReconstructPath(prev, target, s, stopwatch.Elapsed.TotalSeconds)
        Console.ReadKey()
    End Sub
    Sub Small_Label_First(d As Dictionary(Of Node, Double), q As Queue(Of Node))
        If d(q.Last) < d(q.First) Then
            Dim u = q.Dequeue
            q.Reverse
            q.Enqueue(u)
            q.Reverse
        End If
    End Sub
    Sub Large_Label_Last(d As Dictionary(Of Node, Double), q As Queue(Of Node))
        Dim x = d.Values.Average
        While d(q.First) > x
            Dim u = q.Dequeue
            q.Reverse
            q.Enqueue(u)
            q.Reverse
        End While
    End Sub
End Module
