Module DijkstrasAlgorithm
    Sub DijkstrasAdjacencyList(availablePath As Dictionary(Of Node, List(Of Node)), showSolving As Boolean, solvingDelay As Integer, evaluation As Boolean)
        Dim source As New Node(availablepath.Keys(availablepath.Count - 2).X, availablepath.Keys(availablepath.Count - 2).Y)
        Dim target As New Node(availablepath.Keys(availablepath.Count - 1).X, availablepath.Keys(availablepath.Count - 1).Y)
        Dim dist As New Dictionary(Of Node, Double)
        Dim prev As New Dictionary(Of Node, Node)
        Dim q As New PriorityQueue(Of Node)
        dist(source) = 0
        For Each v In availablePath.Keys
            If Not v.Equals(source) Then dist(v) = Int32.MaxValue / 2
            prev(v) = Nothing
            q.Enqueue(v, dist(v))
        Next
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(ConsoleColor.Red)
        While Not q.IsEmpty
            If ExitCase() Then Exit While
            Dim u As Node = q.ExtractMin
            If u.Equals(target) Then Exit While
            If showSolving Then : u.Print("██") : Threading.Thread.Sleep(solvingDelay) : End If
            For Each v As Node In GetNeighboursAd(u, availablepath)
                Dim alt As Integer = dist(u) + 1 'h(u, v, 1)
                If alt < dist(v) Then
                    dist(v) = alt
                    prev(v) = u
                    q.DecreasePriority(v, alt)
                End If
            Next
        End While
        Backtrack(prev, target, source, stopwatch)
        If Not evaluation Then Console.ReadKey()
    End Sub
    Sub Dijkstras(g As List(Of Node), showSolving As Boolean, solvingDelay As Integer, solvingColour As ConsoleColor, Optional ByRef pathLength As Integer = 0)
        Dim initialValue = pathLength
        Dim source = getStart(g)
        Dim target = getGoal(g)
        Dim dist As New Dictionary(Of Node, Double)
        Dim prev As New Dictionary(Of Node, Node)
        Dim Q As New PriorityQueue(Of Node)
        dist(source) = 0
        For Each v In g
            If Not v.Equals(source) Then dist(v) = Int32.MaxValue / 2
            prev(v) = Nothing
            Q.Enqueue(v, dist(v))
        Next
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(solvingColour)
        While Not Q.IsEmpty
            If ExitCase() Then Exit While
            Dim u As Node = Q.ExtractMin
            If u.Equals(target) Then Exit While
            If showSolving And pathLength = 0 Then : u.Print("██") : Threading.Thread.Sleep(solvingDelay) : End If
            For Each v As Node In GetNeighbours(u, g)
                Dim alt As Integer = dist(u) + H(u, v, 1)
                If alt < dist(v) Then
                    dist(v) = alt
                    prev(v) = u
                    Q.DecreasePriority(v, alt)
                End If
            Next
        End While
        Backtrack(prev, target, source, stopwatch, pathLength)
        If initialValue = 0 Then Console.ReadKey()
    End Sub
End Module