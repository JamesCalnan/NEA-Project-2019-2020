Module Junctions
    Function GetJunctionCount(availablePath As List(Of Node))
        Dim junctionCount = 0
        For Each node In availablePath
            If node.IsJunction(availablePath) Then junctionCount += 1
        Next
        Return junctionCount
    End Function
End Module
