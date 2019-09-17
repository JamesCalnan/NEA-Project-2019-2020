Module Recursive_Solving_Method
    Function recursiveSolve(ByVal maze As List(Of Node), ByVal Visited As Dictionary(Of Node, Boolean), ByVal correctPath As Dictionary(Of Node, Boolean), ByVal X As Integer, ByVal Y As Integer, ByVal Target As Node, ByVal ShowSteps As Boolean, ByVal Delay As Integer)
        Dim CurrentNode As New Node(X, Y)
        If CurrentNode.Equals(Target) Then Return True
        If Not maze.Contains(CurrentNode) OrElse Visited(CurrentNode) Then Return False
        If ShowSteps Then
            CurrentNode.Print("XX")
            Threading.Thread.Sleep(Delay)
        End If
        Visited(CurrentNode) = True
        If recursiveSolve(maze, Visited, correctPath, X, Y - 1, Target, ShowSteps, Delay) Then
            correctPath(CurrentNode) = True
            Return True
        End If
        If recursiveSolve(maze, Visited, correctPath, X - 2, Y, Target, ShowSteps, Delay) Then
            correctPath(CurrentNode) = True
            Return True
        End If
        If recursiveSolve(maze, Visited, correctPath, X + 2, Y, Target, ShowSteps, Delay) Then
            correctPath(CurrentNode) = True
            Return True
        End If
        If recursiveSolve(maze, Visited, correctPath, X, Y + 1, Target, ShowSteps, Delay) Then
            correctPath(CurrentNode) = True
            Return True
        End If
        Return False
    End Function
End Module
