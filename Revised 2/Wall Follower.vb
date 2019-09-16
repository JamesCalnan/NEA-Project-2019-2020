Module Wall_Follower
    Sub WallFollower(ByVal Maze As List(Of Node), ByVal ShowPath As Boolean, ByVal Delay As Integer, ByVal rule As String)
        Dim start_v As New Node(Maze(Maze.Count - 2).X, Maze(Maze.Count - 2).Y)
        Dim goal As New Node(Maze(Maze.Count - 1).X, Maze(Maze.Count - 1).Y)
        Dim u As Node = start_v
        Dim prev As Node = u
        SetBoth(ConsoleColor.Green)
        u.Print("XX")
        Dim CurrentDirection As String = "down"
        Dim timer As Stopwatch = Stopwatch.StartNew
        Do
            SetBoth(ConsoleColor.DarkCyan)
            prev.Print("XX")
            CurrentDirection = GetNextDirection(Maze, u, CurrentDirection, rule)
            If CurrentDirection = "left" Then
                u.update(u.X - 2, u.Y)
            ElseIf CurrentDirection = "right" Then
                u.update(u.X + 2, u.Y)
            ElseIf CurrentDirection = "up" Then
                u.update(u.X, u.Y - 1)
            ElseIf CurrentDirection = "down" Then
                u.update(u.X, u.Y + 1)
            End If
            'If u.Adjacent(goal) Then Exit Do
            Console.BackgroundColor = ConsoleColor.Black
            Console.ForegroundColor = ConsoleColor.White
            Console.SetCursorPosition(0, 1)
            Console.Write($"Current direction: {CurrentDirection.Substring(0, 1).ToUpper}{CurrentDirection.Substring(1, CurrentDirection.Count - 1)}      ")
            SetBoth(ConsoleColor.Cyan)
            u.Print("XX")
            prev = u
            Threading.Thread.Sleep(Delay)
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
    Function GetNextDirection(ByVal Maze As List(Of Node), ByVal CurrentNode As Node, ByVal CurrentDirection As String, ByVal Rule As String)
        Dim HorizontalDirections As New List(Of String) From {"left", "right"}
        Dim VerticalDirections As New List(Of String) From {"up", "down"}
        If CurrentDirection = "right" Then
            If Maze.Contains(New Node(CurrentNode.X, CurrentNode.Y + 1 * If(Rule = "LHR", -1, 1))) Then Return VerticalDirections(If(Rule = "LHR", 0, 1))
            If Maze.Contains(New Node(CurrentNode.X + 2, CurrentNode.Y)) Then Return CurrentDirection
            If Maze.Contains(New Node(CurrentNode.X, CurrentNode.Y + 1 * If(Rule = "LHR", 1, -1))) Then Return VerticalDirections(If(Rule = "LHR", 1, 0))
        ElseIf CurrentDirection = "down" Then
            If Maze.Contains(New Node(CurrentNode.X + 2 * If(Rule = "LHR", 1, -1), CurrentNode.Y)) Then Return HorizontalDirections(If(Rule = "LHR", 1, 0))
            If Maze.Contains(New Node(CurrentNode.X, CurrentNode.Y + 1)) Then Return CurrentDirection
            If Maze.Contains(New Node(CurrentNode.X + 2 * If(Rule = "LHR", -1, 1), CurrentNode.Y)) Then Return HorizontalDirections(If(Rule = "LHR", 0, 1))
        ElseIf CurrentDirection = "left" Then
            If Maze.Contains(New Node(CurrentNode.X, CurrentNode.Y + 1 * If(Rule = "LHR", 1, -1))) Then Return VerticalDirections(If(Rule = "LHR", 1, 0))
            If Maze.Contains(New Node(CurrentNode.X - 2, CurrentNode.Y)) Then Return CurrentDirection
            If Maze.Contains(New Node(CurrentNode.X, CurrentNode.Y + 1 * If(Rule = "LHR", -1, 1))) Then Return VerticalDirections(If(Rule = "LHR", 0, 1))
        ElseIf CurrentDirection = "up" Then
            If Maze.Contains(New Node(CurrentNode.X + 2 * If(Rule = "LHR", -1, 1), CurrentNode.Y)) Then Return HorizontalDirections(If(Rule = "LHR", 0, 1))
            If Maze.Contains(New Node(CurrentNode.X, CurrentNode.Y - 1)) Then Return CurrentDirection
            If Maze.Contains(New Node(CurrentNode.X + 2 * If(Rule = "LHR", 1, -1), CurrentNode.Y)) Then Return HorizontalDirections(If(Rule = "LHR", 1, 0))
        End If
        Return ReverseDirection(CurrentDirection)
    End Function
    Function ReverseDirection(ByVal currentdirection As String)
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