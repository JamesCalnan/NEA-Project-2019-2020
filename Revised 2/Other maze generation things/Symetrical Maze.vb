Module Symetrical_Maze
    Function MakeMazeSymetrical(Maze As List(Of Node), pathColour As ConsoleColor, backGroundColour As ConsoleColor) As List(Of Node)
        'todo implement foreground and background colours on this
        Dim greatestXValue = 0
        Dim greatestYValue = 0
        For Each node In Maze
            If node.X > greatestXValue Then greatestXValue = node.X
            If node.Y > greatestYValue Then greatestYValue = node.Y
        Next
        Dim start = getStart(Maze)
        Dim goal As New Node(start.X, start.Y + greatestYValue - 1)
        Maze.RemoveAt(Maze.Count - 1)
        Maze.RemoveAt(Maze.Count - 1)
        Dim halfMaze As New List(Of Node)
        Dim fullMaze As New List(Of Node)
        Console.ResetColor()
        Console.Clear()
        SetBoth(ConsoleColor.White)
        For y = 2 To greatestYValue \ 2 + 1
            For x = 5 To greatestXValue
                If Maze.Contains(New Node(x, y)) Then
                    Console.SetCursorPosition(x, y)
                    Console.Write("XX")
                    halfMaze.Add(New Node(x, y))
                End If
            Next
        Next
        fullMaze.AddRange(halfMaze)
        halfMaze.Reverse()
        Dim offset = 1
        Dim prevY As Integer = halfMaze(0).Y
        For Each node In halfMaze
            If prevY <> node.Y Then
                prevY = node.Y
                offset += 2
            End If
            Console.SetCursorPosition(node.X, node.Y + offset)
            Console.Write("XX")
            fullMaze.Add(New Node(node.X, node.Y + offset))
        Next
        fullMaze.Add(start)
        fullMaze.Add(goal)
        PrintStartandEnd(fullMaze)
        Return fullMaze
    End Function

End Module
