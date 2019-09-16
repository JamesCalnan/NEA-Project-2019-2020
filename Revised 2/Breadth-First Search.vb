Module Breadth_First_Search
    Sub BFS(ByVal availablepath As List(Of Node), ByVal ShowPath As Boolean, ByVal ShowSolveTime As Boolean, ByVal Delay As Integer, ByVal Evaluation As Boolean)
        Dim start_v As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim goal As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim Discovered As New Dictionary(Of Node, Boolean)
        Dim Q As New Queue(Of Node)
        Dim cameFrom As New Dictionary(Of Node, Node)
        For Each node In availablepath
            Discovered(node) = False
        Next
        Discovered(start_v) = True
        Q.Enqueue(start_v)
        SetBoth(ConsoleColor.Red)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While Q.Count > 0
            Dim v As Node = Q.Dequeue
            If ShowPath Then : v.Print("██") : Threading.Thread.Sleep(Delay) : End If
            If v.Equals(goal) Then Exit While
            For Each w As Node In GetNeighbours(v, availablepath)
                If Not Discovered(w) Then
                    Discovered(w) = True
                    Q.Enqueue(w)
                    cameFrom(w) = v
                End If
            Next
        End While
        ReconstructPath(cameFrom, goal, start_v, If(ShowSolveTime, $"{stopwatch.Elapsed.TotalSeconds}", ""))
        If Not Evaluation Then Console.ReadKey()
    End Sub
End Module
