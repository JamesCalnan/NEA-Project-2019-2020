Module BinaryTreeAlgorithm
    Function BinaryTree(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, biasArr() As Integer, pathColour As consolecolor, backGroundColour As consolecolor)
        If backGroundColour <> ConsoleColor.black Then DrawBackground(backGroundColour, limits)
        Dim availablepath As New List(Of Node)
        Dim wallCell As Cell
        Dim r As New Random
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim changeX, changeY As Integer
        If biasArr(2) = 1 Then
            changeX = -4
            changeY = 2
        ElseIf biasArr(3) = 1 Then
            changeX = 4
            changeY = 2
        ElseIf biasArr(0) = 1 Then
            changeX = -4
            changeY = -2
        ElseIf biasArr(1) = 1 Then
            changeX = +4
            changeY = -2
        End If
        SetBoth(pathColour)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) Step 4
                If ExitCase() Then Return Nothing
                Dim tempcell As New Cell(x, y)
                If showMazeGeneration Then tempcell.Print("██")
                availablepath.Add(New Node(tempcell.X, tempcell.Y))
                Dim vCell As New Cell(x + changeX, y)
                Dim hCell As New Cell(x, y + changeY)
                If vCell.WithinLimits(limits) And hCell.WithinLimits(limits) Then
                    Dim randomNumber As Integer = r.Next(1, 101)
                    If randomNumber > 50 Then
                        wallCell = MidPoint(tempcell, vCell)
                        If showMazeGeneration Then wallCell.Print("██")
                        availablepath.Add(New Node(wallCell.X, wallCell.Y))
                    Else
                        wallCell = MidPoint(tempcell, hCell)
                        If showMazeGeneration Then wallCell.Print("██")
                        availablepath.Add(New Node(wallCell.X, wallCell.Y))
                    End If
                ElseIf vCell.WithinLimits(limits) And Not hCell.WithinLimits(limits) Then
                    wallCell = MidPoint(tempcell, vCell)
                    If showMazeGeneration Then wallCell.Print("██")
                    availablepath.Add(New Node(wallCell.X, wallCell.Y))
                ElseIf Not vCell.WithinLimits(limits) And hCell.WithinLimits(limits) Then
                    wallCell = MidPoint(tempcell, hCell)
                    If showMazeGeneration Then wallCell.Print("██")
                    availablepath.Add(New Node(wallCell.X, wallCell.Y))
                End If
                Threading.Thread.Sleep(delay)
            Next
        Next
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(availablepath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(availablepath, limits, pathColour)
        Console.SetCursorPosition(0, ypos)
        Return availablepath
    End Function
    Function BinaryTreeRandom(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, biasArr() As Integer, pathColour As ConsoleColor, backGroundColour As ConsoleColor)
        If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
        Dim availableCells As New List(Of Cell)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                availableCells.Add(New Cell(x, y))
            Next
        Next
        Dim availablepath As New List(Of Node)
        Dim wallCell As Cell
        Dim r As New Random
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim changeX, changeY As Integer
        If biasArr(2) = 1 Then
            changeX = -4
            changeY = 2
        ElseIf biasArr(3) = 1 Then
            changeX = 4
            changeY = 2
        ElseIf biasArr(0) = 1 Then
            changeX = -4
            changeY = -2
        ElseIf biasArr(1) = 1 Then
            changeX = +4
            changeY = -2
        End If
        SetBoth(pathColour)

        While availableCells.Count > 0
            If ExitCase() Then Return Nothing
            Dim tempCell As Cell = availableCells(r.Next(availableCells.Count))
            If showMazeGeneration Then tempcell.Print("██")
            availablepath.Add(New Node(tempcell.X, tempcell.Y))
            Dim vCell As New Cell(tempcell.X + changeX, tempcell.Y)
            Dim hCell As New Cell(tempcell.X, tempcell.Y + changeY)
            If vCell.WithinLimits(limits) And hCell.WithinLimits(limits) Then
                Dim randomNumber As Integer = r.Next(1, 101)
                If randomNumber > 50 Then
                    wallCell = MidPoint(tempcell, vCell)
                    If showMazeGeneration Then wallCell.Print("██")
                    availablepath.Add(New Node(wallCell.X, wallCell.Y))
                Else
                    wallCell = MidPoint(tempcell, hCell)
                    If showMazeGeneration Then wallCell.Print("██")
                    availablepath.Add(New Node(wallCell.X, wallCell.Y))
                End If
            ElseIf vCell.WithinLimits(limits) And Not hCell.WithinLimits(limits) Then
                wallCell = MidPoint(tempcell, vCell)
                If showMazeGeneration Then wallCell.Print("██")
                availablepath.Add(New Node(wallCell.X, wallCell.Y))
            ElseIf Not vCell.WithinLimits(limits) And hCell.WithinLimits(limits) Then
                wallCell = MidPoint(tempcell, hCell)
                If showMazeGeneration Then wallCell.Print("██")
                availablepath.Add(New Node(wallCell.X, wallCell.Y))
            End If
            availableCells.Remove(tempCell)
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(availablepath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(availablepath, limits, pathColour)
        Console.SetCursorPosition(0, ypos)
        Return availablepath
    End Function
End Module
