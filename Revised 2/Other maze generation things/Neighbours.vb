Module Neighbours
    Function GetNeighboursAd(ByRef current As Node, ByRef adjacencyList As Dictionary(Of Node, List(Of Node)))
        Return adjacencyList(current)
    End Function
    Function GetNeighbours(ByRef current As Node, ByRef availablepath As List(Of Node), Optional useDiagonal As Boolean = False)
        Dim neighbours As New List(Of Node)

        Dim newNode As New Node(current.X, current.Y - 1)
        If availablepath.Contains(newNode) Then neighbours.Add(New Node(newNode.X, newNode.Y))

        newNode.Update(current.X + 2, current.Y)
        If availablepath.Contains(newNode) Then neighbours.Add(New Node(newNode.X, newNode.Y))

        newNode.Update(current.X, current.Y + 1)
        If availablepath.Contains(newNode) Then neighbours.Add(New Node(newNode.X, newNode.Y))

        newNode.Update(current.X - 2, current.Y)
        If availablepath.Contains(newNode) Then neighbours.Add(New Node(newNode.X, newNode.Y))

        if not useDiagonal then Return neighbours

        newNode.Update(current.X + 2, current.Y + 1)
        If availablepath.Contains(newNode) Then
            neighbours.Add(new Node(newNode.X, newNode.Y))
        End If

        newNode.Update(current.X + 2, current.Y - 1)
        If availablepath.Contains(newNode) Then
            neighbours.Add(New Node(newNode.X, newNode.Y))
        End If
        newNode.Update(current.X - 2, current.Y + 1)
        If availablepath.Contains(newNode) Then
            neighbours.Add(New Node(newNode.X, newNode.Y))
        End If
        newNode.Update(current.X - 2, current.Y - 1)
        If availablepath.Contains(newNode) Then
            neighbours.Add(New Node(newNode.X, newNode.Y))
        End If
        Return neighbours

    End Function
    Function GetNeighboursCell(ByRef current As Cell, ByRef availablepath As List(Of Cell))
        Dim neighbours As New List(Of Cell)
        Dim newNode As New Cell(current.X, current.Y - 1)
        If availablepath.Contains(newNode) Then neighbours.Add(New Cell(newNode.X, newNode.Y))
        newNode.Update(current.X + 2, current.Y)
        If availablepath.Contains(newNode) Then neighbours.Add(New Cell(newNode.X, newNode.Y))
        newNode.Update(current.X, current.Y + 1)
        If availablepath.Contains(newNode) Then neighbours.Add(New Cell(newNode.X, newNode.Y))
        newNode.Update(current.X - 2, current.Y)
        If availablepath.Contains(newNode) Then neighbours.Add(New Cell(newNode.X, newNode.Y))
        Return neighbours
    End Function

    Function RanNeighbour(current As Cell, limits() As Integer)
        Dim neighbours As New List(Of Cell)
        Dim newPoint As New Cell(current.X - 4, current.Y)
        If newPoint.WithinLimits(limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y - 2)
        If newPoint.WithinLimits(limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y + 2)
        If newPoint.WithinLimits(limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X + 4, current.Y)
        If newPoint.WithinLimits(limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        Return neighbours
    End Function
    Function Neighbour(current As Cell, visited As Dictionary(Of Cell, Boolean), limits() As Integer) As List(Of Cell)
        Dim neighbours As New List(Of Cell)
        Dim newPoint As New Cell(current.X - 4, current.Y)
        If visited.ContainsKey(newPoint) Then If newPoint.WithinLimits(limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y - 2)
        If visited.ContainsKey(newPoint) Then If newPoint.WithinLimits(limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y + 2)
        If visited.ContainsKey(newPoint) Then If newPoint.WithinLimits(limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X + 4, current.Y)
        If visited.ContainsKey(newPoint) Then If newPoint.WithinLimits(limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        Return neighbours
    End Function



End Module
