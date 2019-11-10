Module Corners
    Function GetCornerCount(maze As List(Of Node))
        Dim cornerCount = 0
        For Each node In maze
            If IsCorner(node, maze) Then cornerCount += 1
        Next
        Return cornerCount
    End Function
    Function IsCorner(currentNode As Node, adjacentcells As List(Of Node))
        Dim top As New Node(currentNode.X, currentNode.Y - 1)
        Dim right As New Node(currentNode.X + 2, currentNode.Y)
        Dim bottom As New Node(currentNode.X, currentNode.Y + 1)
        Dim left As New Node(currentNode.X - 2, currentNode.Y)
        If adjacentcells.Contains(top) And adjacentcells.Contains(right) Then Return True 'is it a corner
        If adjacentcells.Contains(right) And adjacentcells.Contains(bottom) Then Return True
        If adjacentcells.Contains(bottom) And adjacentcells.Contains(left) Then Return True
        If adjacentcells.Contains(left) And adjacentcells.Contains(top) Then Return True
        Return False
    End Function
End Module
