Module BreadthFirstSearch
    Sub Bfs(availablepath As List(Of Node), showPath As Boolean, showSolveTime As Boolean, delay As Integer, evaluation As Boolean)
        Dim startV As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim goal As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim discovered As New Dictionary(Of Node, Boolean)
        Dim q As New Queue(Of Node)
        Dim cameFrom As New Dictionary(Of Node, Node)
        For Each node In availablepath
            discovered(node) = False
        Next
        discovered(startV) = True
        q.Enqueue(startV)
        SetBoth(ConsoleColor.Red)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While q.Count > 0
            Dim v As Node = q.Dequeue
            If showPath Then : v.Print("██") : Threading.Thread.Sleep(delay) : End If
            If v.Equals(goal) Then Exit While
            For Each w As Node In GetNeighbours(v, availablepath)
                If Not discovered(w) Then
                    discovered(w) = True
                    q.Enqueue(w)
                    cameFrom(w) = v
                End If
            Next
        End While
        ReconstructPath(cameFrom, goal, startV, If(showSolveTime, $"{stopwatch.Elapsed.TotalSeconds}", ""))
        If Not evaluation Then Console.ReadKey()
    End Sub
End Module
