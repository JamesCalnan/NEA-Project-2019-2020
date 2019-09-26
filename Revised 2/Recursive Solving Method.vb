Module RecursiveSolvingMethod
    Function RecursiveSolve(maze As List(Of Node), visited As Dictionary(Of Node, Boolean), correctPath As Dictionary(Of Node, Boolean), x As Integer, y As Integer, target As Node, showSteps As Boolean, delay As Integer)
        Dim currentNode As New Node(x, y)
        If currentNode.Equals(target) Then Return True
        If Not maze.Contains(currentNode) OrElse visited(currentNode) Then Return False
        If showSteps Then
            currentNode.Print("XX")
            Threading.Thread.Sleep(delay)
        End If
        visited(currentNode) = True
        If RecursiveSolve(maze, visited, correctPath, x, y - 1, target, showSteps, delay) Then
            correctPath(currentNode) = True
            Return True
        End If
        If RecursiveSolve(maze, visited, correctPath, x - 2, y, target, showSteps, delay) Then
            correctPath(currentNode) = True
            Return True
        End If
        If RecursiveSolve(maze, visited, correctPath, x + 2, y, target, showSteps, delay) Then
            correctPath(currentNode) = True
            Return True
        End If
        If RecursiveSolve(maze, visited, correctPath, x, y + 1, target, showSteps, delay) Then
            correctPath(currentNode) = True
            Return True
        End If
        Return False
    End Function
End Module
