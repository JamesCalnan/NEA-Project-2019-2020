Module shortest_path_faster_algorithm
    Sub SPFA(maze As List(Of Node), showSolving As Boolean, solvingDelay As Integer, solvingColour As ConsoleColor, Optional type As String = "normal")
        Dim s = getStart(maze)
        Dim target = getGoal(maze)
        Dim d As New Dictionary(Of Node, Double)
        Dim prev As New Dictionary(Of Node, Node)
        Dim Q As New SPFAQueue(Of Node)
        For Each v In maze
            d(v) = Int32.MaxValue / 2
            prev(v) = Nothing
        Next
        d(s) = 0
        SetBoth(solvingColour)
        Q.offer(s)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While Not Q.isEmpty
            Dim u = Q.poll
            If u.Equals(target) Then
                SetBoth(solvingColour)
                ReconstructPath(prev, target, s, stopwatch.Elapsed.TotalSeconds)
                Console.ReadKey()
                Exit sub
            End If
            If showSolving Then : u.Print("██") : Threading.Thread.Sleep(solvingDelay) : End If
            For Each v As Node In GetNeighbours(u, maze)
                If d(u) + 1 < d(v) Then
                    d(v) = d(u) + 1
                    prev(v) = u
                    If Not Q.Contains(v) Then
                        If type = "normal" Then
                            Q.offer(v)
                        ElseIf type = "slf" Then
                            Q.offerSmallLabelFirst(v, d)
                        ElseIf type = "llf" Then
                            Q.offerLargeLabelFirst(v, d)
                        End If
                    End If
                End If
            Next
        End While
        displayPathNotFoundMessage
    End Sub
End Module
Class SPFAQueue(Of T)
    Public Property items As Queue(Of T)
    Public Sub New()
        items = New Queue(Of T)
    End Sub
    Public Sub offer(item As T)
        items.Enqueue(item)
    End Sub
    Public Function Contains(item As T)
        Return items.Contains(item)
    End Function
    Public Sub offerLargeLabelFirst(item As T, d As Dictionary(Of T, Double))
        items.Enqueue(item)
        Dim x = d.Values.Average
        While d(items.First) > x
            Dim u = items.Dequeue
            items.Reverse
            items.Enqueue(u)
            items.Reverse
        End While
    End Sub
    Public Sub offerSmallLabelFirst(item As T, d As Dictionary(Of T, Double))
        items.Enqueue(item)
        If d(items.Last) < d(items.First) Then
            Dim u = items.Dequeue
            items.Reverse
            items.Enqueue(u)
            items.Reverse
        End If
    End Sub
    Public Function poll() As T
        Return items.Dequeue
    End Function
    Public Function isEmpty()
        Return items.Count = 0
    End Function
End Class