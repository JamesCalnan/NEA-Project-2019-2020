Module Dijkstras_Algorithm
    Sub Dijkstras(ByVal availablepath As Dictionary(Of Node, List(Of Node)), ByVal ShowSolving As Boolean, ByVal SolvingDelay As Integer, ByVal Evaluation As Boolean)
        Dim source As New Node(availablepath.Keys(availablepath.Count - 2).X, availablepath.Keys(availablepath.Count - 2).Y)
        Dim target As New Node(availablepath.Keys(availablepath.Count - 1).X, availablepath.Keys(availablepath.Count - 1).Y)
        Dim dist As New Dictionary(Of Node, Double)
        Dim prev As New Dictionary(Of Node, Node)
        Dim Q As New List(Of Node)
        Dim INFINITY As Integer = Int32.MaxValue
        For Each v In availablepath.Keys
            dist(v) = INFINITY
            prev(v) = Nothing
            Q.Add(v)
        Next
        dist(source) = 0
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(ConsoleColor.Red)
        While Q.Count > 0
            If ExitCase() Then Exit While
            Dim u As Node = ExtractMin(Q, dist)
            If ShowSolving Then : u.Print("██") : Threading.Thread.Sleep(SolvingDelay) : End If
            If u.Equals(target) Then Exit While
            Q.Remove(u)
            For Each v As Node In GetNeighboursAd(u, availablepath)
                Dim alt As Integer = dist(u) + h(u, v, 1)
                If alt < dist(v) Then
                    dist(v) = alt
                    prev(v) = u
                End If
            Next
        End While
        Backtrack(prev, target, source, stopwatch)
        If Not Evaluation Then Console.ReadKey()
    End Sub
End Module