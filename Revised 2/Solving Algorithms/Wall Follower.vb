Module WallFollower
    Sub WallFollowerAlgorithm(g As List(Of Node), delay As Integer, rule As String, solvingColour As ConsoleColor)
        Dim startV = getStart(g)
        Dim goal = getGoal(g)
        Dim u As Node = startV
        Dim prev As Node = u
        SetBoth(ConsoleColor.Green)
        u.Print("XX")
        Dim currentDirection = "down"
        Dim timer As Stopwatch = Stopwatch.StartNew
        Do
            SetBoth(solvingColour)
            prev.Print("XX")
            currentDirection = GetNextDirection(g, u, currentDirection, rule)
            If currentDirection = "left" Then
                u.Update(u.X - 2, u.Y)
            ElseIf currentDirection = "right" Then
                u.Update(u.X + 2, u.Y)
            ElseIf currentDirection = "up" Then
                u.Update(u.X, u.Y - 1)
            ElseIf currentDirection = "down" Then
                u.Update(u.X, u.Y + 1)
            End If
            'If u.Adjacent(goal) Then Exit Do
            Console.BackgroundColor = ConsoleColor.Black
            Console.ForegroundColor = ConsoleColor.White
            Console.SetCursorPosition(0, 1)
            Console.Write($"Current direction: {currentDirection.Substring(0, 1).ToUpper}{currentDirection.Substring(1, currentDirection.Count - 1)}      ")
            SetBoth(ConsoleColor.Cyan)
            u.Print("XX")
            prev = u
            Threading.Thread.Sleep(delay)
        Loop Until u.Equals(goal)
        SetBoth(ConsoleColor.DarkCyan)
        prev.Print("XX")
        SetBoth(ConsoleColor.Black)
        Console.SetCursorPosition(0, 1)
        Console.Write("                               ")
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, goal.Y + 2)
        Console.Write($"Time taken: {timer.Elapsed.TotalSeconds}")
        Console.ReadKey()
    End Sub

    Private Function GetNextDirection(maze As List(Of Node), currentNode As Node, currentDirection As String, rule As String)
        Dim horizontalDirections As New List(Of String) From {"left", "right"}
        Dim verticalDirections As New List(Of String) From {"up", "down"}
        If currentDirection = "right" Then
            If maze.Contains(New Node(currentNode.X, currentNode.Y + 1 * If(rule = "LHR", -1, 1))) Then Return verticalDirections(If(rule = "LHR", 0, 1))
            If maze.Contains(New Node(currentNode.X + 2, currentNode.Y)) Then Return currentDirection
            If maze.Contains(New Node(currentNode.X, currentNode.Y + 1 * If(rule = "LHR", 1, -1))) Then Return verticalDirections(If(rule = "LHR", 1, 0))
        ElseIf currentDirection = "down" Then
            If maze.Contains(New Node(currentNode.X + 2 * If(rule = "LHR", 1, -1), currentNode.Y)) Then Return horizontalDirections(If(rule = "LHR", 1, 0))
            If maze.Contains(New Node(currentNode.X, currentNode.Y + 1)) Then Return currentDirection
            If maze.Contains(New Node(currentNode.X + 2 * If(rule = "LHR", -1, 1), currentNode.Y)) Then Return horizontalDirections(If(rule = "LHR", 0, 1))
        ElseIf currentDirection = "left" Then
            If maze.Contains(New Node(currentNode.X, currentNode.Y + 1 * If(rule = "LHR", 1, -1))) Then Return verticalDirections(If(rule = "LHR", 1, 0))
            If maze.Contains(New Node(currentNode.X - 2, currentNode.Y)) Then Return currentDirection
            If maze.Contains(New Node(currentNode.X, currentNode.Y + 1 * If(rule = "LHR", -1, 1))) Then Return verticalDirections(If(rule = "LHR", 0, 1))
        ElseIf currentDirection = "up" Then
            If maze.Contains(New Node(currentNode.X + 2 * If(rule = "LHR", -1, 1), currentNode.Y)) Then Return horizontalDirections(If(rule = "LHR", 0, 1))
            If maze.Contains(New Node(currentNode.X, currentNode.Y - 1)) Then Return currentDirection
            If maze.Contains(New Node(currentNode.X + 2 * If(rule = "LHR", 1, -1), currentNode.Y)) Then Return horizontalDirections(If(rule = "LHR", 1, 0))
        End If
        Return ReverseDirection(currentDirection)
    End Function
    Private Function ReverseDirection(currentDirection As String)
        If currentdirection = "right" Then
            Return "left"
        ElseIf currentdirection = "left" Then
            Return "right"
        ElseIf currentdirection = "up" Then
            Return "down"
        ElseIf currentdirection = "down" Then
            Return "up"
        End If
        Return Nothing
    End Function
End Module