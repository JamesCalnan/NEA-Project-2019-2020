Module Binary_Tree_Algorithm
    Function BinaryTree(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean, ByVal BiasArr() As Integer)
        Dim Availablepath As New List(Of Node)
        Dim WallCell As Cell
        Dim R As New Random
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim ChangeX, ChangeY As Integer
        If BiasArr(2) = 1 Then
            ChangeX = -4
            ChangeY = 2
        ElseIf BiasArr(3) = 1 Then
            ChangeX = 4
            ChangeY = 2
        ElseIf BiasArr(0) = 1 Then
            ChangeX = -4
            ChangeY = -2
        ElseIf BiasArr(1) = 1 Then
            ChangeX = +4
            ChangeY = -2
        End If
        SetBoth(ConsoleColor.White)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) Step 4
                If ExitCase() Then Return Nothing
                Dim tempcell As New Cell(x, y)
                If ShowMazeGeneration Then tempcell.Print("██")
                Availablepath.Add(New Node(tempcell.X, tempcell.Y))
                Dim VCell As New Cell(x + ChangeX, y)
                Dim HCell As New Cell(x, y + ChangeY)
                If VCell.WithinLimits(Limits) And HCell.WithinLimits(Limits) Then
                    Dim RandomNumber As Integer = R.Next(1, 101)
                    If RandomNumber > 50 Then
                        WallCell = MidPoint(tempcell, VCell)
                        If ShowMazeGeneration Then WallCell.Print("██")
                        Availablepath.Add(New Node(WallCell.X, WallCell.Y))
                    Else
                        WallCell = MidPoint(tempcell, HCell)
                        If ShowMazeGeneration Then WallCell.Print("██")
                        Availablepath.Add(New Node(WallCell.X, WallCell.Y))
                    End If
                ElseIf VCell.WithinLimits(Limits) And Not HCell.WithinLimits(Limits) Then
                    WallCell = MidPoint(tempcell, VCell)
                    If ShowMazeGeneration Then WallCell.Print("██")
                    Availablepath.Add(New Node(WallCell.X, WallCell.Y))
                ElseIf Not VCell.WithinLimits(Limits) And HCell.WithinLimits(Limits) Then
                    WallCell = MidPoint(tempcell, HCell)
                    If ShowMazeGeneration Then WallCell.Print("██")
                    Availablepath.Add(New Node(WallCell.X, WallCell.Y))
                End If
                Threading.Thread.Sleep(Delay)
            Next
        Next
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            SetBoth(ConsoleColor.White)
            PrintMazeHorizontally(Availablepath, Limits(2), Limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(Availablepath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return Availablepath
    End Function
End Module
