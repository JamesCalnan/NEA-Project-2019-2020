Imports System.IO

Module Text_File
    Sub SaveMazeTextFile(path As List(Of Node), algorithm As String)
        Using writer = New StreamWriter($"{GetValidFileName(".txt")}.txt", True)
            writer.WriteLine($"{algorithm}")
            For i = 0 To path.Count - 1
                writer.WriteLine(path(i).X)
                writer.WriteLine(path(i).Y)
            Next
        End Using
    End Sub
    Sub LoadMazeTextFile(ByRef loadedMaze As List(Of Node), yPosAfterMaze As Integer, ByRef previousMaze As List(Of Node), tempArr() As String, showPath As Boolean, solvingDelay As Integer, pathColour As ConsoleColor, backGroundColour As ConsoleColor, solvingColour As ConsoleColor)
        Dim validMaze, xMax, yMax As Integer
        Dim greatestY = 0
        Dim greatestX = 0
        validMaze = 1
        xMax = Console.WindowWidth - 50
        yMax = Console.WindowHeight - 8
        loadedMaze.Clear()
        Console.Clear()
        Dim _x, _y As Integer
        Console.Write("File Name of the maze to load (don't include .txt): ")
        Dim filename As String = Console.ReadLine + ".txt"
        If File.Exists(filename) Then
            Dim usedAlgorithm = ""
            Dim c = 0
            Dim e = True
            Console.Clear()
            Using reader = New StreamReader(filename)
                Do Until reader.EndOfStream
                    If e Then
                        usedAlgorithm = reader.ReadLine
                        e = False
                    End If
                    If c = 0 Then
                        _x = Int(reader.ReadLine)
                        If Int(_x) > greatestX Then greatestX = Int(_x)
                        If _x > xMax Then
                            validMaze = 0
                            Exit Do
                        End If
                    ElseIf c = 1 Then
                        _y = Int(reader.ReadLine)
                        If Int(_y) > greatestY Then greatestY = Int(_y)
                        If _y > yMax Then
                            validMaze = 0
                            Exit Do
                        End If
                    End If
                    c += 1
                    If c = 2 Then
                        loadedMaze.Add(New Node(_x, _y))
                        c = 0
                    End If
                Loop
            End Using
            If loadedMaze.Count < 1 Then validMaze = 2
            If validMaze = 1 Then
                Console.ResetColor()
                Console.Clear()
                Console.SetCursorPosition(0, 0)
                Dim mess = "Algorithm used to generate this maze "
                Console.Write(mess)
                Console.SetCursorPosition(mess.Length, 0)
                MsgColour(usedAlgorithm, ConsoleColor.Green)
                SetBoth(ConsoleColor.White)
                Dim gx, gy As Integer
                For Each node In loadedMaze
                    If node.X > gx Then gx = node.X
                    If node.Y > gy Then gy = node.Y
                Next
                DrawBackground(backGroundColour, {5, 3, greatestX + 1, gy - 1})
                SetBoth(pathColour)
                PrintMazeHorizontally(loadedMaze, greatestX, greatestY)
                PrintStartandEnd(loadedMaze)
                yPosAfterMaze = greatestY
                DisplayAvailablePositions(previousMaze.Count)
                Console.SetCursorPosition(0, yPosAfterMaze + 3)
                previousMaze = loadedMaze
                Dim input = SolvingMenu(tempArr, "What would you Like to do with the maze", greatestX + 6, 3)
                SolvingInput(input, showPath, yPosAfterMaze, solvingDelay, previousMaze, usedAlgorithm, pathColour, backGroundColour, solvingColour)
            ElseIf validMaze = 0 Then
                Console.Clear()
                MsgColour("Maze Is too big for the screen, please decrease the font size And try again", ConsoleColor.Red)
                Console.ReadKey()
            ElseIf validMaze = 2 Then
                Console.Clear()
                MsgColour("Invalid maze", ConsoleColor.Red)
                Console.ReadKey()
            End If
        Else
            Console.Clear()
            MsgColour("File doesn't exist", ConsoleColor.Red)
            Console.ReadKey()
        End If
    End Sub
    Sub SaveMazeAscii(maze As List(Of Node))
        Dim gx, gy As Integer
        gx = 0
        gy = 0
        For Each node In maze
            If node.X > gx Then gx = node.X
            If node.Y > gy Then gy = node.Y
        Next
        Dim lineText As New List(Of String)
        For y = 0 To gy + 1
            Dim currentLine = ""
            For x = 0 To gx + 2 Step 2
                Dim newNode As New Node(x, y)
                If maze.Contains(newNode) Then
                    currentLine += ("XX")
                Else
                    currentLine += ("  ")
                End If
            Next
            lineText.Add(currentLine)
        Next
        Using writer = New StreamWriter($"{GetValidFileName(".txt")}.txt", True)
            For Each Str1 In lineText
                writer.WriteLine(Str1)
            Next
        End Using
    End Sub
    Function LoadMazeAscii(tempArr() As String, pathColour As ConsoleColor, backGroundColour As ConsoleColor, solvingColour As ConsoleColor) As List(Of Node)
        Console.Clear()
        Dim y As Integer
        Console.Write("File Name of the maze to load (don't include .txt): ")
        Dim filename As String = Console.ReadLine
        filename += ".txt"
        If System.IO.File.Exists(filename) Then
            Dim maze As New List(Of Node)
            Using reader = New StreamReader(filename)
                Do Until reader.EndOfStream
                    Dim currentLine As String = reader.ReadLine
                    For i = 0 To currentLine.Count - 1 Step 2
                        If currentLine.Chars(i) = "X" Then
                            maze.Add(New Node(i, y))
                        End If
                    Next
                    y += 1
                Loop
            End Using
            Dim start As Node = maze(0)
            Dim finish As Node = maze(maze.Count - 1)
            maze.RemoveAt(0)
            maze.RemoveAt(maze.Count - 1)
            maze.Add(start)
            maze.Add(finish)
            SetBoth(pathColour)
            Dim gX, gY As Integer
            gX = 0
            gY = 0
            For Each node In maze
                If node.X > gX Then gX = node.X
                If node.Y > gY Then gY = node.Y
            Next
            If gX > Console.WindowWidth - 57 Or gY > Console.WindowHeight - 6 Then Return Nothing
            DrawBackground(backGroundColour, {5, 3, gX + 1, gY - 1})
            SetBoth(pathColour)
            PrintMazeHorizontally(maze, gX, gY)
            PrintStartandEnd(maze)
            Console.BackgroundColor = ConsoleColor.Black
            Console.ForegroundColor = ConsoleColor.White
            Dim input As String = SolvingMenu(tempArr, "What would you like to do with the maze", gX + 7, 3)
            SolvingInput(input, True, gY + 2, 0, maze, "", pathColour, backGroundColour, solvingColour)
            Return maze
        Else
            Console.Clear()
            MsgColour("File doesn't exist", ConsoleColor.Red)
            Console.ReadKey()
        End If
        Return Nothing
    End Function
End Module
