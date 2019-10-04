Module ShortestPathFasterAlgorithm
    Sub SPFA(maze As List(Of Node), showPath As Boolean, showSolveTime As Boolean, delay As Integer, solvingColour As ConsoleColor)
        Dim s = GetStart(maze)
        Dim target = GetGoal(maze)
        Dim d As New Dictionary(Of Node, Integer)
        For Each v In maze
            d(v) = Int32.MaxValue
        Next



    End Sub
End Module
