Module Dead_Ends
    Function GetDeadEndCount(availablePath As List(Of Node))
        Dim start As New Node(availablePath(availablePath.Count - 2).X, availablePath(availablePath.Count - 2).Y)
        Dim target As New Node(availablePath(availablePath.Count - 1).X, availablePath(availablePath.Count - 1).Y)
        Return (From node In availablePath Where Not node.Equals(start) And Not node.Equals(target) Select GetNeighbours(node, availablePath)).Count(Function(neighbours) DirectCast(neighbours, List(Of Node)).Count = 1)
    End Function
End Module
