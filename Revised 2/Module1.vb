Imports System.Drawing
Imports System.IO
Imports Revised_2
Module Module1

    'TODO: 
    Sub Main()
        Console.CursorVisible = False
        SetColour(ConsoleColor.White)
        Dim MenuOptions() As String = {"Recursive Backtracker Algorithm (using iteration)", "Recursive Backtracker Algorithm (using recursion)", "Hunt and Kill Algorithm", "Prim's Algorithm", "Aldous-Broder Algorithm", "Growing Tree Algorithm", "Custom Algorithm", "Binary Tree Algorithm", "Sidewinder Algorithm", "Wilson's Algorithm", "Eller's Algorithm", "Kruskal's Algorithm", "", "Load the previously generated maze", "Save the previously generated maze", "Load a saved maze", "", "Exit"}
        Menu(MenuOptions)

    End Sub
    Sub Playmaze(ByVal AvailablePath As List(Of Node), ByVal ShowPath As Boolean)
        Dim playerPath As New List(Of Node)
        Dim currentPos As New Node(AvailablePath(AvailablePath.Count - 2).X, AvailablePath(AvailablePath.Count - 2).Y)
        Dim start As New Node(AvailablePath(AvailablePath.Count - 2).X, AvailablePath(AvailablePath.Count - 2).Y)
        If Not playerPath.Contains(currentPos) Then playerPath.Add(currentPos)
        SetColour(ConsoleColor.Magenta)
        currentPos.Print("██")
        Dim PreviousPos As Node = currentPos
        Dim target As New Node(AvailablePath(AvailablePath.Count - 1).X, AvailablePath(AvailablePath.Count - 1).Y)
        SetColour(ConsoleColor.Green)
        target.Print("██")
        Console.CursorVisible = False
        While Not currentPos.Equals(target)
            Dim t As New Node(Console.CursorLeft, Console.CursorTop)
            Dim s As New Node(Console.CursorLeft - 1, Console.CursorTop)
            If AvailablePath.Contains(t) Or AvailablePath.Contains(s) Then
                If ShowPath And AvailablePath.Contains(s) Or AvailablePath.Contains(t) Then
                    If playerPath.Contains(t) Or playerPath.Contains(s) Then
                        If AvailablePath.Contains(t) Or AvailablePath.Contains(s) Then
                            If ShowPath Then
                                SetBoth(ConsoleColor.Blue)
                            Else
                                SetBoth(ConsoleColor.White)
                            End If
                        Else
                            SetBoth(ConsoleColor.White)
                        End If
                    Else
                        SetBoth(ConsoleColor.White)
                    End If
                Else
                    If AvailablePath.Contains(t) Or AvailablePath.Contains(s) Then
                        SetBoth(ConsoleColor.White)
                    Else
                        SetBoth(ConsoleColor.Blue)
                    End If
                End If
            Else
                SetBoth(ConsoleColor.Black)
            End If
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "RightArrow"
                    Dim tempNode3 As New Node(currentPos.X + 2, currentPos.Y)
                    PlayMazeKeyPress(currentPos, tempNode3, ShowPath, PreviousPos, playerPath, AvailablePath)
                Case "LeftArrow"
                    Dim tempNode2 As New Node(currentPos.X - 2, currentPos.Y)
                    PlayMazeKeyPress(currentPos, tempNode2, ShowPath, PreviousPos, playerPath, AvailablePath)
                Case "UpArrow"
                    Dim tempNode1 As New Node(currentPos.X, currentPos.Y - 1)
                    PlayMazeKeyPress(currentPos, tempNode1, ShowPath, PreviousPos, playerPath, AvailablePath)
                Case "DownArrow"
                    Dim tempNode As New Node(currentPos.X, currentPos.Y + 1)
                    PlayMazeKeyPress(currentPos, tempNode, ShowPath, PreviousPos, playerPath, AvailablePath)
                Case "Escape"
                    Exit While
                Case Else
            End Select
        End While
        SetColour(ConsoleColor.Yellow)
        If currentPos.Equals(target) Then
            playerPath.Add(start)
            playerPath.Add(target)
            aStar(playerPath, False, False, 0)
            Console.Clear()
            PrintMessageMiddle("EPIC VICTORY ROYALE", Console.WindowHeight / 2, ConsoleColor.Yellow)
            Console.ReadKey()
        End If
    End Sub
    Sub PlayMazeKeyPress(ByRef currentPos As Node, ByVal tempNode As Node, ByVal showpath As Boolean, ByRef PreviousPos As Node, ByRef playerPath As List(Of Node), ByVal AvailablePath As List(Of Node))
        If AvailablePath.Contains(tempNode) Then
            currentPos = tempNode
            If showpath Then
                SetColour(ConsoleColor.Blue)
            Else
                SetColour(ConsoleColor.White)
            End If
            PreviousPos.Print("██")
            SetColour(ConsoleColor.Magenta)
            currentPos.Print("██")
            PreviousPos = currentPos
            If Not playerPath.Contains(currentPos) Then playerPath.Add(currentPos)
        End If
    End Sub

    Sub SetBackGroundColour(ByVal colour As ConsoleColor)
        Console.BackgroundColor = colour
    End Sub
    Sub SetColour(ByVal colour As ConsoleColor)
        Console.ForegroundColor = colour
    End Sub
    Sub GetMazeInfo(ByRef Width As Integer, ByRef Height As Integer, ByRef DelayMS As Integer, ByRef Limits() As Integer, ByRef ShowGeneration As Boolean, ByVal Clear As Boolean, ByVal y As Integer)
        Console.SetCursorPosition(0, y)
        ShowGeneration = HorizontalYesNo(Console.CursorTop, "Do you want to see the maze being generated: ", False, If(Clear, True, False), False)
        Console.SetCursorPosition(0, Console.CursorTop + 1)
        If ShowGeneration Then
            DelayMS = GetIntInputArrowKeys("Delay when making the Maze (MS): ", 100, 0, False)
        Else
            DelayMS = 0
        End If
        Width = GetIntInputArrowKeys($"Width of the Maze: ", Console.WindowWidth - 40, 20, False)
        If Width Mod 2 = 0 Then Width += 1
        Height = GetIntInputArrowKeys($"Height of the Maze: ", Console.WindowHeight - 3, 10, False)
        If Height Mod 2 = 0 Then Height += 1
        Limits = {5, 3, Width, Height}
        Console.Clear()
    End Sub
    Sub MsgColour(ByVal Msg As String, ByVal Colour As ConsoleColor)
        SetColour(Colour)
        Console.WriteLine(Msg)
        SetColour(ConsoleColor.White)
    End Sub
    Function GetIntInputArrowKeys(ByVal message As String, ByVal NumMax As Integer, ByVal NumMin As Integer, ByVal ClearMessage As Boolean)
        Console.Write(message)
        SetColour(ConsoleColor.Magenta)
        Dim cursorleft, cursortop As Integer
        cursorleft = Console.CursorLeft
        cursortop = Console.CursorTop
        Console.SetCursorPosition(cursorleft, cursortop)
        Dim current As Integer = NumMin
        Console.Write(current)
        While 1
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "RightArrow"
                    current += 10
                    If current > NumMax Then current = NumMax
                Case "LeftArrow"
                    current -= 10
                    If current < NumMin Then current = NumMin
                Case "UpArrow"
                    current += 1
                    If current > NumMax Then current = NumMax
                Case "DownArrow"
                    current -= 1
                    If current < NumMin Then current = NumMin
                Case "Enter"
                    Exit While
            End Select
            Console.SetCursorPosition(cursorleft, cursortop)
            Console.Write("   ")
            Console.SetCursorPosition(cursorleft, cursortop)
            Console.Write(current)
        End While
        If ClearMessage Then
            Console.SetCursorPosition(0, cursortop)
            Console.Write("".PadLeft(message.Length + 5, " "c))
        End If
        Console.SetCursorPosition(0, cursortop + 1)
        SetColour(ConsoleColor.White)
        Return current
    End Function
    Sub DisplayAvailablePositions(ByVal count As Integer)
        PrintMessageMiddle($"There are {count} available positions in the maze", 0, ConsoleColor.Magenta)
    End Sub
    Function PreGenMenu(ByVal arr() As String, ByVal Message As String)
        Console.Clear()
        Dim temparr() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0}
        Dim CurrentCol As Integer = Console.CursorTop
        Dim y As Integer = Console.CursorTop
        Dim NumOfOptions As Integer = arr.Count
        MsgColour(Message, ConsoleColor.Yellow)
        MsgColour($"> {arr(0)}", ConsoleColor.Green)
        For i = 1 To arr.Count - 1
            Console.WriteLine($" {arr(i)}")
        Next
        While 1
            SetBackGroundColour(ConsoleColor.Black)
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "DownArrow"
                    y += 1
                    If y = arr.Count Then y = 0
                Case "UpArrow"
                    y -= 1
                    If y = -1 Then y = arr.Count - 1
                Case "Enter"
                    Console.Clear()
                    temparr(y) = 1
                    Return temparr
            End Select
            SetColour(ConsoleColor.White)
            Dim Count As Integer = 1
            For Each MenuOption In arr
                Console.SetCursorPosition(0, Count + CurrentCol)
                Console.Write($" {MenuOption}  ")
                Count += 1
            Next
            Console.SetCursorPosition(0, y + 1)
            MsgColour($"> {arr(y)}", ConsoleColor.Green)
        End While
        Return Nothing
    End Function
    Sub PreSolving(ByVal limits() As Integer, ByVal availablepath As List(Of Node), ByRef previousmaze As List(Of Node), ByRef input As String, ByRef yposaftermaze As Integer)
        SetBackGroundColour(ConsoleColor.Black)
        yposaftermaze = limits(3)
        DisplayAvailablePositions(availablepath.Count)
        Console.SetCursorPosition(0, yposaftermaze + 3)
        input = SolvingMenu(yposaftermaze + 3)
        previousmaze.Clear()
        previousmaze = availablepath
    End Sub
    Sub SolvingInput(ByVal input As String, ByVal showpath As Boolean, ByVal YposAfterMaze As Integer, ByVal solvingdelay As Integer, ByVal availablepath As List(Of Node))
        If input = "astar" Then
            showpath = HorizontalYesNo(YposAfterMaze + 3, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Dim OptimisedAStar As Boolean = HorizontalYesNo(YposAfterMaze + 3, "Do you want to use the optimised version of A*: ", True, False, False)
            If OptimisedAStar Then
                aStar(availablepath, showpath, True, solvingdelay)
            Else
                aStarWiki(availablepath, showpath, True, solvingdelay)
            End If
        ElseIf input = "dijkstras" Then
            showpath = HorizontalYesNo(YposAfterMaze + 3, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            DFS_Iterative(availablepath, showpath, True, solvingdelay)
        ElseIf input = "play" Then
            showpath = HorizontalYesNo(YposAfterMaze + 3, "Do you want to show the steps you have taken in the maze: ", True, False, False)
            Playmaze(availablepath, showpath)
        ElseIf IsNothing(input) Then
        End If
    End Sub
    Sub Solving(ByVal AvailablePath As List(Of Node), ByVal Limits() As Integer, ByRef PreviousMaze As List(Of Node), ByRef Input As String, ByVal YPosAfterMaze As Integer, ByVal ShowPath As Boolean, ByVal SolvingDelay As Integer, ByRef Algorithm As String, ByRef SetPreivousAlgorithm As String)
        If AvailablePath IsNot Nothing Then
            SetPreivousAlgorithm = Algorithm
            PreSolving(Limits, AvailablePath, PreviousMaze, Input, YPosAfterMaze)
            SolvingInput(Input, ShowPath, YPosAfterMaze, SolvingDelay, AvailablePath)
        End If
    End Sub
    Sub Menu(ByVal arr() As String)
        Dim input, PreviousAlgorithm As String
        Dim PreviousMaze, LoadedMaze As New List(Of Node)
        Dim Width, Height, DelayMS, Limits(), SolvingDelay, YPosAfterMaze, y As Integer
        Dim ShowMazeGeneration, ShowPath As Boolean
        Console.Clear()
        Dim CurrentCol As Integer = Console.CursorTop
        Dim NumOfOptions As Integer = arr.Count
        MsgColour("What Maze Generation Algorithm do you want to use: ", ConsoleColor.Yellow)
        MsgColour($"> {arr(0)}", ConsoleColor.Green)
        For i = 1 To arr.Count - 1
            Console.WriteLine($" {arr(i)}")
        Next
        While 1
            SetBackGroundColour(ConsoleColor.Black)
            Dim key = Console.ReadKey
            Console.CursorVisible = False
            Select Case key.Key.ToString
                Case "DownArrow"
                    y += 1
                    If y = arr.Count Then y = 0
                    If y = 12 Or y = 16 Then y += 1
                Case "UpArrow"
                    y -= 1
                    If y = -1 Then y = arr.Count - 1
                    If y = 12 Or y = 16 Then y -= 1
                Case "Enter"
                    Dim AvailablePath As List(Of Node)
                    If y = 0 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = RecursiveBacktracker(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 1 Then
                        Dim r As New Random
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
                        Dim prev As New Cell(Limits(0) + 3, Limits(1) + 2)
                        Dim v As New List(Of Cell)
                        Dim path As New List(Of Node)
                        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
                        path = RecursiveBacktrackerRecursively(CurrentCell, Limits, path, v, prev, r, ShowMazeGeneration, DelayMS)
                        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
                        SetColour(ConsoleColor.White)
                        If Not ShowMazeGeneration Then
                            For Each cell In path
                                cell.Print("██")
                            Next
                        End If
                        AddStartAndEnd(path, v, Limits, 0)
                        Solving(path, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                        PreviousMaze = path
                    ElseIf y = 2 Then
                        Dim Optimised As Boolean = HorizontalYesNo(0, "Do you want to use the optimised version of hunt and kill: ", False, True, True)
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, False, 1)
                        AvailablePath = HuntAndKill(Limits, DelayMS, ShowMazeGeneration, Optimised)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 3 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Prims(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 4 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = AldousBroder(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 5 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        Dim ArrOptions() As String = {"Newest (Recursive Backtracker)", "Random (Prim's)", "Newest/Random, 75/25 split", "Newest/Random, 50/50 split", "Newest/Random, 25/75 split", "Oldest", "Middle", "Newest/Oldest, 50/50 split", "Oldest/Random, 50/50 split"}
                        Dim CellSelectionMethod() As Integer = PreGenMenu(ArrOptions, "What Cell selection method would you like to use: ")
                        AvailablePath = GrowingTree(Limits, DelayMS, CellSelectionMethod, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 6 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Custom(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 7 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        Dim ArrOptions() As String = {"Northwest", "Northeast", "Southwest", "Southeast"}
                        Dim Bias() As Integer = PreGenMenu(ArrOptions, "Cell bias: ")
                        AvailablePath = BinaryTree(Limits, DelayMS, ShowMazeGeneration, Bias)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 8 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Sidewinder(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 9 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Wilsons(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 13 Then
                        Dim GreatestY As Integer = 0
                        If PreviousMaze.Count > 1 Then
                            Console.Clear()
                            Console.SetCursorPosition(0, 0)
                            Dim mess As String = "Algorithm used to generate this maze: "
                            Console.Write(mess)
                            Console.SetCursorPosition(mess.Length, 0)
                            MsgColour(PreviousAlgorithm, ConsoleColor.Green)
                            SetBoth(ConsoleColor.White)
                            For Each node In PreviousMaze
                                node.Print("██")
                                If GreatestY < node.Y Then GreatestY = node.Y
                            Next
                            PrintStartandEnd(PreviousMaze)
                            SetBackGroundColour(ConsoleColor.Black)
                            YPosAfterMaze = GreatestY
                            DisplayAvailablePositions(PreviousMaze.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 3)
                            input = SolvingMenu(YPosAfterMaze + 3)
                            SolvingInput(input, ShowPath, YPosAfterMaze, SolvingDelay, PreviousMaze)
                        Else
                            Console.Clear()
                            MsgColour("No previous maze available", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    ElseIf y = 14 Then
                        If PreviousMaze.Count > 1 Then
                            Console.Clear()
                            Dim filename As String
                            Do
                                Console.Write("File Name (don't include .txt): ")
                                filename = Console.ReadLine
                                filename += ".txt"
                                If System.IO.File.Exists(filename) Then
                                    MsgColour("Invalid filename", ConsoleColor.Red)
                                End If
                            Loop Until Not System.IO.File.Exists(filename)
                            Using writer As StreamWriter = New StreamWriter(filename, True)
                                writer.WriteLine($"{PreviousAlgorithm}")
                                For i = 0 To PreviousMaze.Count - 1
                                    writer.WriteLine(PreviousMaze(i).X)
                                    writer.WriteLine(PreviousMaze(i).Y)
                                Next
                            End Using
                        Else
                            Console.Clear()
                            MsgColour("No previous maze available", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    ElseIf y = 15 Then
                        Dim ValidMaze, XMax, YMax As Integer
                        Dim GreatestY As Integer = 0
                        ValidMaze = 1
                        XMax = Console.WindowWidth - 6
                        YMax = Console.WindowHeight - 4
                        LoadedMaze.Clear()
                        Console.Clear()
                        Dim _x, _y As Integer
                        Console.Write("File Name of the maze to load (don't include .txt): ")
                        Dim filename As String = Console.ReadLine
                        filename += ".txt"
                        If System.IO.File.Exists(filename) Then
                            Dim UsedAlgorithm As String
                            Dim c As Integer = 0
                            Dim e As Boolean = True
                            Console.Clear()
                            Using reader As StreamReader = New StreamReader(filename)
                                Do Until reader.EndOfStream
                                    If e = True Then
                                        UsedAlgorithm = reader.ReadLine
                                        e = False
                                    End If
                                    If c = 0 Then
                                        _x = Int(reader.ReadLine)
                                        If _x > XMax Then
                                            ValidMaze = 0
                                            Exit Do
                                        End If
                                    ElseIf c = 1 Then
                                        _y = Int(reader.ReadLine)
                                        If Int(_y) > GreatestY Then GreatestY = Int(_y)
                                        If _y > YMax Then
                                            ValidMaze = 0
                                            Exit Do
                                        End If
                                    End If
                                    c += 1
                                    If c = 2 Then
                                        Console.WriteLine($"({_x}, {_y})")
                                        LoadedMaze.Add(New Node(_x, _y))
                                        c = 0
                                    End If
                                Loop
                            End Using
                            If LoadedMaze.Count < 1 Then ValidMaze = 2
                            If ValidMaze = 1 Then
                                MsgColour($"Finished loading maze positions, total maze positions: {LoadedMaze.Count}", ConsoleColor.Green)
                                Console.ReadKey()
                                Console.Clear()
                                Console.SetCursorPosition(0, 0)
                                Dim mess As String = "Algorithm used to generate this maze: "
                                Console.Write(mess)
                                Console.SetCursorPosition(mess.Length, 0)
                                MsgColour(UsedAlgorithm, ConsoleColor.Green)
                                For Each node In LoadedMaze
                                    node.Print("██")
                                Next
                                PrintStartandEnd(LoadedMaze)
                                YPosAfterMaze = GreatestY
                                DisplayAvailablePositions(PreviousMaze.Count)
                                Console.SetCursorPosition(0, YPosAfterMaze + 3)
                                PreviousMaze = LoadedMaze
                                input = SolvingMenu(YPosAfterMaze + 3)
                                SolvingInput(input, ShowPath, YPosAfterMaze, SolvingDelay, PreviousMaze)
                            ElseIf ValidMaze = 0 Then
                                Console.Clear()
                                MsgColour("Maze is too big for the screen, please decrease the font size and try again", ConsoleColor.Red)
                                Console.ReadKey()
                            ElseIf ValidMaze = 2 Then
                                Console.Clear()
                                MsgColour("Invalid maze", ConsoleColor.Red)
                                Console.ReadKey()
                            End If
                        Else
                            Console.Clear()
                            MsgColour("File doesn't exist", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    ElseIf y = arr.Count - 1 Then
                        End
                    Else
                        OptionNotReady()
                    End If
                    SetBackGroundColour(ConsoleColor.Black)
                    Console.Clear()
                    MsgColour("What Maze Generation Algorithm do you want to use: ", ConsoleColor.Yellow)
            End Select
            SetColour(ConsoleColor.White)
            Dim Count As Integer = 1
            For Each MenuOption In arr
                Console.SetCursorPosition(0, Count + CurrentCol)
                Console.Write($" {MenuOption}  ")
                Count += 1
            Next
            Console.SetCursorPosition(0, y + 1)
            MsgColour($"> {arr(y)}", ConsoleColor.Green)
        End While
    End Sub
    Sub PrintStartandEnd(ByVal mazePositions As List(Of Node))
        SetColour(ConsoleColor.Red)
        mazePositions(mazePositions.Count - 2).Print("██")
        SetColour(ConsoleColor.Green)
        mazePositions(mazePositions.Count - 1).Print("██")
        SetColour(ConsoleColor.White)
    End Sub
    Sub OptionNotReady()
        Console.Clear()
        Console.WriteLine("Option not Ready Yet")
        Console.ReadKey()
        Console.Clear()
    End Sub
    Function HorizontalYesNo(ByVal ColumnPosition As Integer, ByVal message As String, ByVal ClearMessage As Boolean, ByVal ClearBefore As Boolean, ByVal SetAfter As Boolean)
        If ClearBefore Then Console.Clear()
        SetColour(ConsoleColor.White)
        Dim Choice As Boolean = True
        Dim x, y As Integer
        y = ColumnPosition
        Console.SetCursorPosition(x, y)
        Console.Write(message)
        MsgColour("> Yes", ConsoleColor.Green)
        Console.SetCursorPosition(message.Length + 10, y)
        Console.Write(" No")
        While 1
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "RightArrow"
                    If Choice Then Choice = False
                Case "LeftArrow"
                    If Not Choice Then Choice = True
                Case "Enter"
                    Console.SetCursorPosition(0, y)
                    If ClearMessage Then Console.Write("                                                                                                               ")
                    Console.SetCursorPosition(0, y + If(SetAfter, 1, 0))
                    If Choice Then
                        Return True
                    Else
                        Return False
                    End If
                Case "Escape"
                    Return Nothing
            End Select
            Console.SetCursorPosition(0, y)
            Console.Write($"{message}  Yes        No")
            If Choice Then
                Console.SetCursorPosition(message.Length, y)
                MsgColour("> Yes", ConsoleColor.Green)
                Console.SetCursorPosition(message.Length + 8, y)
                Console.Write("                    ")
                Console.SetCursorPosition(message.Length + 11, y)
                Console.Write("No")
            ElseIf Not Choice Then
                Console.SetCursorPosition(message.Length + 8, y)
                Console.Write("                    ")
                Console.SetCursorPosition(message.Length + 9, y)
                MsgColour("> No", ConsoleColor.Green)
            End If
        End While
        Return Nothing
    End Function
    Function SolvingMenu(ByVal ColumnPosition As Integer)
        SetColour(ConsoleColor.White)
        Dim Option1 As Integer = 1
        Dim x, y As Integer
        y = ColumnPosition
        Console.SetCursorPosition(x, y)
        Console.Write("Do you want to use ")
        MsgColour("> A*", ConsoleColor.Green)
        Console.SetCursorPosition(23, y)
        Console.Write(" Or Dijkstras to solve the maze or do you want to solve the maze yourself")
        While 1
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "RightArrow"
                    Option1 += 1
                    If Option1 = 4 Then Option1 = 1
                Case "LeftArrow"
                    Option1 -= 1
                    If Option1 = 0 Then Option1 = 3
                Case "Enter"
                    Console.SetCursorPosition(0, y)
                    Console.Write("                                                                                                               ")
                    If Option1 = 1 Then
                        Return "astar"
                    ElseIf Option1 = 2 Then
                        Return "dijkstras"
                    ElseIf Option1 = 3 Then
                        Return "play"
                    End If
                Case "Escape"
                    Return Nothing
            End Select
            Console.SetCursorPosition(0, y)
            Console.Write("Do you want to use A* Or Dijkstras to solve the maze or do you want to solve the maze yourself")
            If Option1 = 1 Then
                Console.SetCursorPosition(19, y)
                MsgColour("> A*", ConsoleColor.Green)
                Console.SetCursorPosition(23, y)
                Console.Write(" Or Dijkstras to solve the maze or do you want to solve the maze yourself")

            ElseIf Option1 = 2 Then
                Console.SetCursorPosition(25, y)
                Console.Write("                             ")
                Console.SetCursorPosition(25, y)
                MsgColour("> Dijkstras", ConsoleColor.Green)
                Console.SetCursorPosition(37, y)
                Console.Write("to solve the maze or do you want to solve the maze yourself")
            ElseIf Option1 = 3 Then
                '+36
                Console.SetCursorPosition(71, y)
                MsgColour("> solve the maze yourself", ConsoleColor.Green)

            End If
        End While
        Return Nothing
    End Function
    Function ExitCase()
        If Console.KeyAvailable Then
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "Escape"
                    Return True
            End Select
        End If
    End Function
    Sub DFS_Iterative(ByVal availablepath As List(Of Node), ByVal ShowPath As Boolean, ByVal ShowSolveTime As Boolean, ByVal Delay As Integer)
        Dim start_v As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim goal As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim visited As New Dictionary(Of Node, Boolean)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim li As New List(Of Node)
        Dim cameFrom As New Dictionary(Of Node, Node)
        Dim S As New Stack(Of Node)
        For Each u In availablepath
            visited(u) = False
        Next
        S.Push(start_v)
        SetBoth(ConsoleColor.Red)
        While S.Count > 0
            Dim u As Node = S.Pop
            If u.Equals(goal) Then Exit While
            If ShowPath Then u.Print("XX")
            If visited(u) = False Then
                visited(u) = True
                For Each w As Node In GetNeighbours(u, availablepath)
                    If visited(w) = False Then
                        S.Push(w)
                        cameFrom(w) = u
                    End If
                Next
            End If
        End While
        If ShowSolveTime Then
            ReconstructPath(cameFrom, goal, start_v, $"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds")
        Else
            ReconstructPath(cameFrom, goal, start_v, $"")
        End If
    End Sub
    Sub BFS(ByVal availablepath As List(Of Node), ByVal ShowPath As Boolean, ByVal ShowSolveTime As Boolean, ByVal Delay As Integer)
        Dim start_v As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim goal As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim Discovered As New Dictionary(Of Node, Boolean)
        Dim Q As New Queue(Of Node)
        Dim cameFrom As New Dictionary(Of Node, Node)
        For Each node In availablepath
            Discovered(node) = False
        Next
        Discovered(start_v) = True
        Q.Enqueue(start_v)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While Q.Count > 0
            Dim v As Node = Q.Dequeue
            If v.Equals(goal) Then
                If ShowSolveTime Then
                    ReconstructPath(cameFrom, v, start_v, $"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds")
                Else
                    ReconstructPath(cameFrom, v, start_v, $"")
                End If
                Exit While
            End If
            SetBoth(ConsoleColor.Red)
            For Each w As Node In GetNeighbours(v, availablepath)
                If Not Discovered(w) Then
                    Discovered(w) = True
                    Q.Enqueue(w)
                    If ShowPath Then w.Print("XX")
                    cameFrom(w) = v
                End If
            Next
        End While
    End Sub
    Sub Dijkstras(ByVal availablepath As List(Of Node), ByVal ShowSolving As Boolean, ByVal SolvingDelay As Integer)
        Dim source As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim target As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        SetBackGroundColour(ConsoleColor.Black)
        Dim dist As New Dictionary(Of Node, Integer)
        Dim prev As New Dictionary(Of Node, Node)
        Dim Q As New List(Of Node)
        Dim INFINITY As Integer = Int32.MaxValue
        For Each v In availablepath
            dist(v) = INFINITY
            prev(v) = Nothing
            Q.Add(v)
        Next
        dist(source) = 0
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(ConsoleColor.Red)
        While Q.Count > 0
            If ExitCase() Then Exit While
            Dim u As Node = ExtractMin(Q, dist)
            If ShowSolving Then u.Print("██")
            If u.Equals(target) Then
                Backtrack(prev, target, source, stopwatch)
                Console.ReadKey()
                Exit While
            End If
            Q.Remove(u)
            For Each v As Node In GetNeighbours(u, availablepath)
                Dim alt As Integer = dist(u) + 1
                If alt < dist(v) Then
                    dist(v) = alt
                    prev(v) = u
                End If
            Next
            Threading.Thread.Sleep(SolvingDelay)
        End While
    End Sub
    Sub Backtrack(ByVal prev As Dictionary(Of Node, Node), ByVal target As Node, ByVal source As Node, ByVal watch As Stopwatch)
        Dim s As New List(Of Node)
        Dim u As Node = target
        While prev(u) IsNot Nothing
            s.Add(u)
            u = prev(u)
        End While
        s.Add(source)
        s.Reverse()
        Dim timetaken As String = $"Time Taken to solve: {watch.Elapsed.TotalSeconds} seconds"
        PrintMessageMiddle($"Path length: {s.Count - 1}   {timetaken}", Console.WindowHeight - 1, ConsoleColor.Yellow)
        SetBoth(ConsoleColor.Green)
        For Each node In s
            node.Print("██")
        Next
    End Sub
    Function ExtractMin(ByVal list As List(Of Node), ByVal dist As Dictionary(Of Node, Integer))
        Dim returnnode As Node = list(0)
        For Each node In list
            If dist(node) < dist(returnnode) Then
                returnnode = node
            End If
        Next
        Return returnnode
    End Function
    Sub aStarWiki(ByVal availablepath As List(Of Node), ByVal ShowPath As Boolean, ByVal ShowSolveTime As Boolean, ByVal Delay As Integer)
        Dim openSet, closedSet As New List(Of Node)
        Dim start As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim goal As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim gScore, fScore As New Dictionary(Of Node, Integer)
        Dim cameFrom As New Dictionary(Of Node, Node)
        Dim INFINITY As Integer = Int32.MaxValue
        For Each node In availablepath
            gScore(node) = INFINITY
            fScore(node) = INFINITY
        Next
        gScore(start) = 0
        fScore(start) = GetDistance(start, goal)
        openSet.Add(start)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While openSet.Count > 0
            Dim current As Node = ExtractMin(openSet, fScore)
            If current.Equals(goal) Then
                If ShowSolveTime Then
                    ReconstructPath(cameFrom, current, start, $"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds")
                Else
                    ReconstructPath(cameFrom, current, start, $"")
                End If
                Exit While
            End If
            openSet.Remove(current)
            closedSet.Add(current)
            SetBoth(ConsoleColor.Red)
            If ShowPath Then closedSet(closedSet.Count - 1).Print("XX")
            For Each Neighbour As Node In GetNeighbours(current, availablepath)
                If closedSet.Contains(Neighbour) Then Continue For
                Dim tentative_gScore = gScore(current) + 1
                If Not openSet.Contains(Neighbour) Then
                    openSet.Add(Neighbour)
                ElseIf tentative_gScore >= gScore(Neighbour) Then
                    Continue For
                End If
                cameFrom(Neighbour) = current
                gScore(Neighbour) = tentative_gScore
                fScore(Neighbour) = gScore(Neighbour) + GetDistance(start, goal)
            Next
            Threading.Thread.Sleep(Delay)
        End While
    End Sub
    Sub ReconstructPath(ByVal camefrom As Dictionary(Of Node, Node), ByVal current As Node, ByVal goal As Node, ByVal timetaken As String)
        Dim totalPath As New List(Of Node) From {
            current,
            goal
        }
        SetColour(ConsoleColor.Green)
        While Not current.Equals(goal)
            totalPath.Add(current)
            current = camefrom(current)
            current.Print("DD")
        End While
        totalPath.Add(goal)
        PrintMessageMiddle($"Path length: {totalPath.Count}   {timetaken}", Console.WindowHeight - 1, ConsoleColor.Yellow)
        SetBoth(ConsoleColor.Green)
        For Each node In totalPath
            node.Print("XX")
        Next
        Console.ReadKey()
    End Sub
    Sub aStar(ByVal availablepath As List(Of Node), ByVal ShowPath As Boolean, ByVal ShowSolveTime As Boolean, ByVal Delay As Integer)
        Dim start As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim target As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim current As Node = start
        Dim openSet, closedSet As New HashSet(Of Node)
        SetBoth(ConsoleColor.Red)
        openSet.Add(current)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While openSet.Count > 0
            If ExitCase() Then Exit While
            current = openSet(0)
            For i = 1 To openSet.Count - 1
                If openSet(i).fCost() <= current.fCost() Or openSet(i).hCost = current.hCost Then
                    If openSet(i).hCost < current.hCost Then current = openSet(i)
                End If
            Next
            openSet.Remove(current)
            closedSet.Add(current)
            If ShowPath Then current.Print("██")
            If current.Equals(target) Then
                Exit While
            End If
            For Each Neighbour As Node In GetNeighbours(current, availablepath)
                If closedSet.Contains(Neighbour) Then Continue For
                Dim tentative_gScore As Single = current.gCost + 1 'GetDistance(current, Neighbour)
                If tentative_gScore < Neighbour.gCost Or Not openSet.Contains(Neighbour) Then
                    Neighbour.gCost = tentative_gScore
                    Neighbour.hCost = 1 'GetDistance(current, Neighbour)
                    Neighbour.parent = current
                    openSet.Add(Neighbour)
                End If
            Next
            Threading.Thread.Sleep(Delay)
        End While
        If ShowSolveTime Then
            RetracePath(start, current, $"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds")
        Else
            RetracePath(start, current, $"")
        End If
        Console.ReadKey()
    End Sub
    Sub CurrentExploredPercent(ByVal count As Integer, ByVal availablepath As Integer)
        Dim Percent As Integer = (count / availablepath) * 100
        Dim mess As String = $"Current percentage of the maze that has been explored: "
        SetColour(ConsoleColor.White)
        SetBackGroundColour(ConsoleColor.Black)
        Console.SetCursorPosition((Console.WindowWidth / 2) - ((mess.Count / 2) + 3), 1)
        Console.Write(mess)
        Dim mess2 As String = $"{100 - Percent}%"
        Console.SetCursorPosition(((Console.WindowWidth / 2) - (mess.Count / 2)) + mess.Count - 2, 1)
        MsgColour($"{100 - Percent}%", ConsoleColor.Yellow)
    End Sub
    Sub SetBoth(ByVal colour As ConsoleColor)
        SetColour(colour)
        SetBackGroundColour(colour)
    End Sub
    Function GetDistance(ByVal nodea As Node, ByVal nodeb As Node)
        Dim dstX As Single = Math.Abs(nodea.X - nodeb.X)
        Dim dstY As Single = Math.Abs(nodea.Y - nodeb.Y)
        If dstX > dstY Then
            Return 14 * dstY + 10 * (dstX - dstY)
        Else
            Return 14 * dstX + 10 * (dstY - dstX)
        End If
    End Function
    Sub RetracePath(ByVal startnode As Node, ByVal endnode As Node, ByVal timetaken As String)
        Dim path As New List(Of Node)
        Dim current As Node = endnode
        While Not current.Equals(startnode)
            path.Add(current)
            current = current.parent
        End While
        PrintMessageMiddle($"Path length: {path.Count}   {timetaken}", Console.WindowHeight - 1, ConsoleColor.Yellow)
        SetBoth(ConsoleColor.Green)
        startnode.Print("██")
        path.Reverse()
        For Each node In path
            node.Print("██")
        Next
        SetBackGroundColour(ConsoleColor.Black)
    End Sub
    Sub PrintMessageMiddle(ByVal message As String, ByVal y As Integer, ByVal colour As ConsoleColor)
        SetBackGroundColour(ConsoleColor.Black)
        SetColour(colour)
        Console.SetCursorPosition(Console.WindowWidth / 2 - message.Length / 2, y)
        Console.Write(message)
    End Sub
    'main algorithm functions
    Function Sidewinder(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        While Limits(2) Mod 4 <> 2
            Limits(2) -= 1
        End While
        Dim WallCell As Cell
        Dim VisitedList As New List(Of Cell)
        Dim RunSet As New List(Of Cell)
        Dim Availablepath As New List(Of Node)
        Dim R As New Random
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) Step 4
                If ExitCase() Then Return Nothing
                Dim CurrentCell As New Cell(x, y)
                Availablepath.Add(New Node(CurrentCell.X, CurrentCell.Y))
                VisitedList.Add(New Cell(CurrentCell.X, CurrentCell.Y))
                If ShowMazeGeneration Then CurrentCell.Print("██")
                If y <> Limits(1) Then RunSet.Add(New Cell(CurrentCell.X, CurrentCell.Y))
                Dim EastCell As New Cell(x + 4, y)
                Dim RanNum As Integer = R.Next(1, 101)
                If x + 2 = Limits(2) And y <> Limits(1) Then
                    RanNum = 1
                End If
                If RanNum > 50 Or y = Limits(1) Then
                    If EastCell.WithinLimits(Limits) Then
                        WallCell = MidPoint(CurrentCell, EastCell)
                        If ShowMazeGeneration Then WallCell.Print("██")
                    End If
                Else
                    Dim RandomRunSet As Integer = R.Next(0, RunSet.Count)
                    Dim RandomRunSetCell As Cell = RunSet(RandomRunSet)
                    Dim NorthCell As New Cell(RandomRunSetCell.X, y - 2)
                    WallCell = MidPoint(RandomRunSetCell, NorthCell)
                    If ShowMazeGeneration Then WallCell.Print("██")
                    RunSet.Clear()
                End If
                Availablepath.Add(New Node(WallCell.X, WallCell.Y))
                Threading.Thread.Sleep(Delay)
            Next
        Next
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        SetColour(ConsoleColor.White)
        If Not ShowMazeGeneration Then
            For Each node In Availablepath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(Availablepath, VisitedList, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return Availablepath
    End Function
    Function BinaryTree(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean, ByVal BiasArr() As Integer)
        Dim Availablepath As New List(Of Node)
        Dim VisitedList As New List(Of Cell)
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
                VisitedList.Add(New Cell(tempcell.X, tempcell.Y))
            Next
        Next
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        SetColour(ConsoleColor.White)
        If Not ShowMazeGeneration Then
            For Each node In Availablepath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(Availablepath, VisitedList, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return Availablepath
    End Function
    Function GrowingTree(ByVal Limits() As Integer, ByVal delay As Integer, ByVal CellSelectionMethod() As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim VisitedList, FrontierSet, RecentFrontierSet As New List(Of Cell)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim PreviousCell As Cell = CurrentCell
        Dim WallCell As Cell
        Dim Index As Integer
        Dim ReturnablePath As New List(Of Node)
        VisitedList.Add(CurrentCell)
        If ShowMazeGeneration Then CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While True
            If ExitCase() Then Return Nothing
            For Each cell As Cell In Neighbour(CurrentCell, VisitedList, Limits, True)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
                RecentFrontierSet.Add(cell)
                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.Yellow)
                    cell.Print("██")
                End If
            Next
            Dim RandomNumber As Integer
            If CellSelectionMethod(0) = 1 Then
                'Recursive Backtracker
                If RecentFrontierSet.Count > 0 Then
                    Index = R.Next(0, RecentFrontierSet.Count)
                    CurrentCell = RecentFrontierSet(Index)
                Else
                    If FrontierSet.Count = 0 Then Exit While
                    Index = FrontierSet.Count - 1
                    CurrentCell = FrontierSet(Index)
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedList)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            ElseIf CellSelectionMethod(1) = 1 Then
                'Prim's
                If FrontierSet.Count = 0 Then Exit While
                Index = R.Next(0, FrontierSet.Count)
                CurrentCell = FrontierSet(Index)
                Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedList)
                PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
            ElseIf CellSelectionMethod(2) = 1 Or CellSelectionMethod(3) = 1 Or CellSelectionMethod(4) = 1 Then
                Dim Chance As Integer
                If CellSelectionMethod(2) = 1 Then
                    '75/25 split
                    Chance = 75
                ElseIf CellSelectionMethod(3) = 1 Then
                    '50/50 split
                    Chance = 50
                ElseIf CellSelectionMethod(4) = 1 Then
                    '25/75 split
                    Chance = 25
                End If
                RandomNumber = R.Next(1, 101)
                If RandomNumber < Chance Then
                    'Newest
                    If RecentFrontierSet.Count > 0 Then
                        Index = R.Next(0, RecentFrontierSet.Count)
                        CurrentCell = RecentFrontierSet(Index)
                    Else
                        If FrontierSet.Count = 0 Then Exit While
                        Index = R.Next(0, FrontierSet.Count)
                        CurrentCell = FrontierSet(Index)
                        Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedList)
                        PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                    End If
                Else
                    'Random
                    If FrontierSet.Count = 0 Then Exit While
                    Index = R.Next(0, FrontierSet.Count)
                    CurrentCell = FrontierSet(Index)
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedList)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            ElseIf CellSelectionMethod(5) = 1 Then
                'Oldest
                If FrontierSet.Count = 0 Then Exit While
                Index = 0
                CurrentCell = FrontierSet(Index)
                Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedList)
                PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
            ElseIf CellSelectionMethod(6) = 1 Then
                'Middle
                If FrontierSet.Count = 0 Then Exit While
                Index = FrontierSet.Count / 2
                CurrentCell = FrontierSet(Index)
                Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedList)
                PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
            ElseIf CellSelectionMethod(7) = 1 Then
                'Newest/Oldest, 50/50 split
                RandomNumber = R.Next(1, 101)
                If RandomNumber > 50 Then
                    'Newest
                    If RecentFrontierSet.Count > 0 Then
                        Index = R.Next(0, RecentFrontierSet.Count)
                        CurrentCell = RecentFrontierSet(Index)
                    Else
                        If FrontierSet.Count = 0 Then Exit While
                        Index = R.Next(0, FrontierSet.Count)
                        CurrentCell = FrontierSet(Index)
                        Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedList)
                        PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                    End If
                Else
                    'Oldest
                    If FrontierSet.Count = 0 Then Exit While
                    Index = 0 'R.Next(0, FrontierSet.Count)
                    CurrentCell = FrontierSet(Index)
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedList)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            ElseIf CellSelectionMethod(8) = 1 Then
                'Oldest/Random, 50/50 split
                RandomNumber = R.Next(1, 101)
                If RandomNumber > 50 Then
                    'Oldest
                    If FrontierSet.Count = 0 Then Exit While
                    Index = 0
                    CurrentCell = FrontierSet(Index)
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedList)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                Else
                    'Random
                    If FrontierSet.Count = 0 Then Exit While
                    Index = R.Next(0, FrontierSet.Count)
                    CurrentCell = FrontierSet(Index)
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedList)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            End If
            WallCell = MidPoint(CurrentCell, PreviousCell)
            If ShowMazeGeneration Then
                SetBoth(ConsoleColor.White)
                WallCell.Print("██")
                CurrentCell.Print("██")
            End If
            VisitedList.Add(CurrentCell)
            FrontierSet.Remove(CurrentCell)
            ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
            ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
            Threading.Thread.Sleep(delay)
            RecentFrontierSet.Clear()
            PreviousCell = CurrentCell
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        SetColour(ConsoleColor.White)
        If Not ShowMazeGeneration Then
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, VisitedList, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function Custom(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim VisitedList, FrontierSet, RecentFrontierSet As New List(Of Cell)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim WallCell As Cell
        Dim Index As Integer
        Dim ReturnablePath As New List(Of Node)
        VisitedList.Add(CurrentCell)
        If ShowMazeGeneration Then CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While True
            If ExitCase() Then Return Nothing

            For Each cell As Cell In Neighbour(CurrentCell, VisitedList, Limits, True)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
                RecentFrontierSet.Add(cell)
                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.Yellow)
                    cell.Print("██")
                End If
            Next
            If RecentFrontierSet.Count > 0 Then
                Index = R.Next(0, RecentFrontierSet.Count)
                CurrentCell = RecentFrontierSet(Index)
                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.Red)
                    CurrentCell.Print("██")
                End If
            Else
                If FrontierSet.Count = 0 Then Exit While
                Index = FrontierSet.Count - 1 'R.Next(0, FrontierSet.Count)
                CurrentCell = FrontierSet(Index)
                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.Red)
                    CurrentCell.Print("██")
                End If
            End If
            Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedList)
            Dim PreviousCell As Cell = PickAdjancentCell(CurrentCell, AdjancencyList)
            WallCell = MidPoint(CurrentCell, PreviousCell)
            If ShowMazeGeneration Then
                SetBoth(ConsoleColor.White)
                WallCell.Print("██")
                CurrentCell.Print("██")
            End If
            VisitedList.Add(CurrentCell)
            FrontierSet.Remove(CurrentCell)
            ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
            ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
            Threading.Thread.Sleep(delay)
            RecentFrontierSet.Clear()
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        SetColour(ConsoleColor.White)
        If Not ShowMazeGeneration Then
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, VisitedList, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function AldousBroder(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim TotalCellCount As Integer
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim VisitedList, RecentCells As New List(Of Cell)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim PrevCell, WallPrev As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim PreviousCell As Cell = CurrentCell
        Dim WallCell As Cell
        Dim ReturnablePath As New List(Of Node)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                TotalCellCount += 1
            Next
        Next
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While VisitedList.Count <> TotalCellCount
            If ExitCase() Then Return Nothing
            SetBoth(ConsoleColor.White)
            RecentCells.Clear()
            For Each cell As Cell In RanNeighbour(CurrentCell, Limits)
                RecentCells.Add(cell)
            Next
            Dim Index As Integer = R.Next(0, RecentCells.Count)
            Dim TemporaryCell As Cell = RecentCells(Index)
            Dim TempNodeCell As New Node(TemporaryCell.X, TemporaryCell.Y)
            If Not VisitedList.Contains(TemporaryCell) Then 'Unvisited cell?
                VisitedList.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                WallCell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                ReturnablePath.Add(New Node(TemporaryCell.X, TemporaryCell.Y))
                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.White)
                    PrevCell.Print("██")
                    WallCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    TemporaryCell.Print("██")
                    PrevCell = CurrentCell
                End If
            Else
                CurrentCell = TemporaryCell
                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.White)
                    PrevCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    TemporaryCell.Print("██")
                    PrevCell = CurrentCell
                End If
            End If
            Threading.Thread.Sleep(delay)
        End While
        If ShowMazeGeneration Then
            SetBoth(ConsoleColor.White)
            PrevCell.Print("██")
        End If
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        SetColour(ConsoleColor.White)
        If Not ShowMazeGeneration Then
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, VisitedList, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function Prims(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim VisitedList, FrontierSet As New List(Of Cell)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim WallCell As Cell
        Dim ReturnablePath As New List(Of Node)
        VisitedList.Add(CurrentCell)
        If ShowMazeGeneration Then CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While True
            If ExitCase() Then Return Nothing
            For Each cell As Cell In Neighbour(CurrentCell, VisitedList, Limits, true)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.Yellow)
                    cell.Print("██")
                End If
            Next
            If FrontierSet.Count = 0 Then Exit While
            CurrentCell = FrontierSet(R.Next(0, FrontierSet.Count))
            Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedList)
            Dim PreviousCell As Cell = PickAdjancentCell(CurrentCell, AdjancencyList)
            WallCell = MidPoint(CurrentCell, PreviousCell)
            If ShowMazeGeneration Then
                SetBoth(ConsoleColor.White)
                WallCell.Print("██")
                CurrentCell.Print("██")
            End If
            VisitedList.Add(CurrentCell)
            FrontierSet.Remove(CurrentCell)
            AddToPath(ReturnablePath, CurrentCell, WallCell)
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        SetColour(ConsoleColor.White)
        If Not ShowMazeGeneration Then
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, VisitedList, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function HuntAndKill(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean, ByVal Optimised As Boolean)
        Dim totalcellcount As Integer
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                totalcellcount += 1
            Next
        Next
        SetBackGroundColour(ConsoleColor.White)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim r As New Random
        Dim VisitedList, Stack, VisitedListAndWall, RecentCells As New List(Of Cell)
        Dim ReturnablePath As New List(Of Node)
        Dim Width As Integer = Limits(2) - Limits(0)
        Dim Height As Integer = Limits(3) - Limits(1)
        Dim xCount, ColumnValue As Integer
        ColumnValue = Limits(1)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While VisitedList.Count <> totalcellcount
            If ExitCase() Then Return Nothing
            If IsNothing(CurrentCell) Then Exit While
            If Neighbour(CurrentCell, VisitedList, Limits, False) Then
                RecentCells.Clear()
                For Each cell As Cell In Neighbour(CurrentCell, VisitedList, Limits, True)
                    RecentCells.Add(cell)
                Next
                Dim TemporaryCell As Cell = RecentCells(r.Next(0, RecentCells.Count))
                ReturnablePath.Add(New Node(TemporaryCell.X, TemporaryCell.Y))
                VisitedListAndWall.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                VisitedList.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                VisitedListAndWall.Add(New Cell(WallCell.X, WallCell.Y))
                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.White)
                    TemporaryCell.Print("██")
                    WallCell.Print("██")
                End If
            Else
                Dim ContinueFor As Boolean = True
                For y = ColumnValue To Limits(3) Step 2
                    xCount = 0
                    For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                        Dim newcell As New Cell(x, y)
                        If ShowMazeGeneration Then
                            SetColour(ConsoleColor.Blue)
                            Console.SetCursorPosition(x, y)
                            newcell.Print("██")
                            Console.SetCursorPosition(x + 2, y)
                            If Console.CursorLeft < Limits(2) - 1 Then Console.Write("██")
                        End If
                        If Optimised Then If x = Limits(2) - 3 Or x = Limits(2) - 1 Then ColumnValue += 2
                        Dim AdjancencyList As Integer() = AdjacentCheck(newcell, VisitedList)
                        CurrentCell = PickAdjancentCell(newcell, AdjancencyList)
                        If CurrentCell IsNot Nothing Then
                            Threading.Thread.Sleep(delay)
                            Dim WallCell As Cell = MidPoint(newcell, CurrentCell)
                            AddToPath(ReturnablePath, CurrentCell, WallCell)
                            xCount = x
                            If ShowMazeGeneration Then
                                SetBoth(ConsoleColor.White)
                                WallCell.Print("██")
                                CurrentCell.Print("██")
                                xCount = x
                                EraseLineHaK(Limits, xCount, VisitedListAndWall, y)
                            End If
                            ContinueFor = False
                            Exit For
                        End If
                        xCount = x
                    Next
                    Threading.Thread.Sleep(delay)
                    If ShowMazeGeneration Then
                        EraseLineHaK(Limits, xCount, VisitedListAndWall, y)
                    End If
                    If ContinueFor = False Then Exit For
                Next
            End If
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        SetColour(ConsoleColor.White)
        If Not ShowMazeGeneration Then
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, VisitedList, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function RecursiveBacktrackerRecursively(ByVal cell As Cell, ByVal limits() As Integer, ByVal path As List(Of Node), ByRef visited As List(Of Cell), ByRef cameFrom As Cell, ByVal r As Random, ByVal ShowMazeGeneration As Boolean, ByVal Delay As Integer)
        If Neighbour(cell, visited, limits, False) Then
            Dim RecentCells As New List(Of Cell)
            For Each cell1 As Cell In Neighbour(cell, visited, limits, True)
                RecentCells.Add(cell1)
            Next
            Dim TemporaryCell As Cell = RecentCells(r.Next(0, RecentCells.Count))
            Dim wall As Cell = MidPoint(cameFrom, TemporaryCell)
            If ShowMazeGeneration Then
                wall.Print("██")
                TemporaryCell.Print("██")
                Threading.Thread.Sleep(Delay) : End If
            AddToPath(path, TemporaryCell, wall)
            cameFrom = TemporaryCell
            visited.Add(TemporaryCell)
            RecursiveBacktrackerRecursively(TemporaryCell, limits, path, visited, cameFrom, r, ShowMazeGeneration, Delay)
        Else
            Return Nothing
        End If
        cameFrom = cell
        RecursiveBacktrackerRecursively(cell, limits, path, visited, cameFrom, r, ShowMazeGeneration, Delay)
        Return path
    End Function
    Function RecursiveBacktracker(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim r As New Random
        SetBoth(ConsoleColor.White)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim PrevCell, WallPrev As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim VisitedList, Stack As New List(Of Cell)
        Dim ReturnablePath As New List(Of Node)
        Dim RecentCells As New List(Of Cell)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While True
            If ExitCase() Then Return Nothing
            If ShowMazeGeneration Then
                PrevCell.Print("██")
                SetBoth(ConsoleColor.White)
            End If
            If Neighbour(CurrentCell, VisitedList, Limits, False) Then 'done
                RecentCells.Clear()
                For Each cell As Cell In Neighbour(CurrentCell, VisitedList, Limits, True)
                    RecentCells.Add(cell)
                Next
                Dim TemporaryCell As Cell = RecentCells(r.Next(0, RecentCells.Count))
                VisitedList.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                Stack.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                AddToPath(ReturnablePath, TemporaryCell, WallCell)
                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.White)
                    PrevCell.Print("██")
                    WallCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    TemporaryCell.Print("██")
                    PrevCell = CurrentCell
                End If
            ElseIf Stack.Count > 1 Then
                CurrentCell = CurrentCell.Pop(Stack)
                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.White)
                    PrevCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    CurrentCell.Print("██")
                    SetBoth(ConsoleColor.White)
                    PrevCell = CurrentCell
                End If
            Else
                Exit While
            End If
            Threading.Thread.Sleep(Delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        SetColour(ConsoleColor.White)
        If Not ShowMazeGeneration Then
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, VisitedList, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function Wilsons(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim UST, RecentCells, availablepositions As New List(Of Cell)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim PrevCell, WallPrev As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim ReturnablePath As New List(Of Node)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                availablepositions.Add(New Cell(x, y))
            Next
        Next
        Dim CellCount As Integer = availablepositions.Count
        Dim StartingCell As Cell = PickRandomCell(availablepositions, UST, Limits)
        If ShowMazeGeneration Then
            SetBoth(ConsoleColor.White)
            StartingCell.Print("██")
        End If
        UST.Add(StartingCell)
        Dim Direction, newdir As New Dictionary(Of Cell, String)
        Dim directions As New Dictionary(Of Cell, String)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While 1
            If ExitCase() Then Return Nothing
            RecentCells.Clear()
            Dim TemporaryCell As Cell
            For Each cell As Cell In RanNeighbour(CurrentCell, Limits)
                RecentCells.Add(cell)
            Next
            TemporaryCell = RecentCells(R.Next(0, RecentCells.Count))

            Dim dir As String = GetDirection(CurrentCell, TemporaryCell, directions, ShowMazeGeneration)
            If UST.Contains(TemporaryCell) Then 'Unvisited cell?
                Direction.Add(TemporaryCell, GetDirection(TemporaryCell, CurrentCell, directions, ShowMazeGeneration))
                SetBoth(ConsoleColor.White)
                Dim NewList As New List(Of Cell)
                Dim current As Cell = directions.Keys(0)
                Dim cur As Cell = current
                While 1
                    Dim prev111 As Cell = cur
                    cur = PickNextDir(prev111, directions, ShowMazeGeneration, Delay, ReturnablePath)
                    NewList.Add(cur)
                    availablepositions.Remove(cur)
                    If UST.Contains(cur) Then Exit While
                End While
                Dim newcell As Cell = MidPoint(NewList(0), directions.Keys(0))
                ReturnablePath.Add(New Node(newcell.X, newcell.Y))
                If ShowMazeGeneration Then newcell.Print("██")
                For i = 1 To NewList.Count - 1
                    If ShowMazeGeneration Then NewList(i).Print("██")
                    Dim wall As Cell = MidPoint(NewList(i), NewList(i - 1))
                    If ShowMazeGeneration Then wall.Print("██")
                    Dim tempNode As New Node(NewList(i).X, NewList(i).Y)
                    If Not ReturnablePath.Contains(tempNode) Then ReturnablePath.Add(New Node(NewList(i).X, NewList(i).Y))
                    tempNode.update(wall.X, wall.Y)
                    If Not ReturnablePath.Contains(tempNode) Then ReturnablePath.Add(New Node(wall.X, wall.Y))
                    Threading.Thread.Sleep(Delay)
                Next
                For Each value In NewList
                    If Not UST.Contains(value) Then UST.Add(value)
                Next
                If Not UST.Contains(directions.Keys(0)) Then UST.Add(directions.Keys(0))
                NewList.Clear()
                directions.Clear()
                Threading.Thread.Sleep(Delay)
                If ShowMazeGeneration Then
                    For Each thing In Direction
                        If UST.Contains(thing.Key) Then
                            SetBoth(ConsoleColor.White)
                            thing.Key.Print("  ")
                        Else
                            SetBoth(ConsoleColor.Black)
                            thing.Key.Print("  ")
                        End If
                    Next
                End If
                Direction.Clear()
                If CellCount = UST.Count Then Exit While
                CurrentCell = PickRandomCell(availablepositions, UST, Limits)
            Else
                CurrentCell = TemporaryCell
                If Direction.ContainsKey(CurrentCell) Then
                    Direction(CurrentCell) = GetDirection(CurrentCell, PrevCell, directions, ShowMazeGeneration)
                Else
                    Direction.Add(CurrentCell, GetDirection(CurrentCell, PrevCell, directions, ShowMazeGeneration))
                End If
                SetBoth(ConsoleColor.White)
                PrevCell = CurrentCell
            End If
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        SetColour(ConsoleColor.White)
        If Not ShowMazeGeneration Then
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, availablepositions, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    'Subs/functions for the algorithms
    Function PickRandomCell(ByVal availablepositions As List(Of Cell), ByVal ust As List(Of Cell), ByVal limits() As Integer)
        Dim r As New Random
        Dim startingcell As New Cell(r.Next(limits(1), limits(3)), r.Next(limits(0) + 3, limits(2) - 1))
        Do
            Dim idx As Integer = r.Next(0, availablepositions.Count)
            startingcell.Update(availablepositions(idx).X, availablepositions(idx).Y)
            If Not ust.Contains(startingcell) Then
                Exit Do
            End If
        Loop
        Return startingcell
    End Function
    Function PickNextDir(ByVal currentcell As Cell, ByVal direction As Dictionary(Of Cell, String), ByVal showmazegeneation As Boolean, ByVal delay As Integer, ByRef returnablepath As List(Of Node))
        Threading.Thread.Sleep(delay)
        If showmazegeneation Then currentcell.Print("XX")
        Dim tempNode As New Node(currentcell.X, currentcell.Y)
        If Not returnablepath.Contains(tempNode) Then returnablepath.Add(New Node(currentcell.X, currentcell.Y))
        Dim go As String = direction(currentcell)
        If go = "VV" Then 'down
            Return New Cell(currentcell.X, currentcell.Y + 2)
        ElseIf go = "<<" Then 'left
            Return New Cell(currentcell.X - 4, currentcell.Y)
        ElseIf go = "^^" Then 'up
            Return New Cell(currentcell.X, currentcell.Y - 2)
        ElseIf go = ">>" Then 'right
            Return New Cell(currentcell.X + 4, currentcell.Y)
        End If
    End Function
    Function GetDirection(ByVal cell1 As Cell, ByVal cell2 As Cell, ByRef newdir As Dictionary(Of Cell, String), ByVal showmazegeneration As Boolean)
        Dim tempcell As New Cell(cell2.X, cell2.Y - 2)
        SetBackGroundColour(ConsoleColor.Black)
        SetColour(ConsoleColor.Red)
        If cell1.Equals(tempcell) Then
            If showmazegeneration Then tempcell.Print("VV")
            If newdir.ContainsKey(tempcell) Then
                newdir(tempcell) = "VV"
            Else
                newdir.Add(tempcell, "VV")
            End If
            Return "VV"
        End If
        tempcell.Update(cell2.X + 4, cell2.Y)
        If cell1.Equals(tempcell) Then
            If showmazegeneration Then tempcell.Print("<<")
            If newdir.ContainsKey(tempcell) Then
                newdir(tempcell) = "<<"
            Else
                newdir.Add(tempcell, "<<")
            End If
            Return "<<"
        End If
        tempcell.Update(cell2.X, cell2.Y + 2)
        If cell1.Equals(tempcell) Then
            If showmazegeneration Then tempcell.Print("^^")
            If newdir.ContainsKey(tempcell) Then
                newdir(tempcell) = "^^"
            Else
                newdir.Add(tempcell, "^^")
            End If
            Return "^^"
        End If
        tempcell.Update(cell2.X - 4, cell2.Y)
        If cell1.Equals(tempcell) Then
            If showmazegeneration Then tempcell.Print(">>")
            If newdir.ContainsKey(tempcell) Then
                newdir(tempcell) = ">>"
            Else
                newdir.Add(tempcell, ">>")
            End If
            Return ">>"
        End If
        Return Nothing
    End Function
    Sub AddToPath(ByRef List As List(Of Node), ByVal cell1 As Cell, ByVal cell2 As Cell)
        Dim tempnode As New Node(cell1.X, cell1.Y)
        If Not List.Contains(tempnode) Then List.Add(New Node(cell1.X, cell1.Y))
        tempnode.update(cell2.X, cell2.Y)
        If Not List.Contains(tempnode) Then List.Add(New Node(cell2.X, cell2.Y))
    End Sub
    Sub EraseLineHaK(ByVal limits() As Integer, ByVal xCount As Integer, ByVal VisitedlistAndWall As List(Of Cell), ByVal y As Integer)
        For i = limits(0) + 3 To xCount + 2 Step 2
            Dim tempcell As New Cell(i, y)
            If Not VisitedlistAndWall.Contains(tempcell) Then
                SetBoth(ConsoleColor.Black)
                tempcell.Print("  ")
            Else
                SetBoth(ConsoleColor.White)
                tempcell.Print("██")
            End If
        Next
    End Sub
    Sub AddStartAndEnd(ByRef ReturnablePath As List(Of Node), ByVal VisitedList As List(Of Cell), ByVal Limits() As Integer, ByVal EvenWidth As Integer)
        ReturnablePath.Add(New Node(Limits(0) + 3, Limits(1) - 1))
        SetBoth(ConsoleColor.Red)
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Dim testnode As New Node(Limits(2) + 2, Limits(3))
        Do
            testnode.update(testnode.X - 1, testnode.Y)
        Loop Until ReturnablePath.Contains(testnode)
        ReturnablePath.Add(New Node(testnode.X, testnode.Y + 1))
        SetBoth(ConsoleColor.Green)
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        SetBackGroundColour(ConsoleColor.Black)
    End Sub
    Function AdjacentCheck(ByVal cell As Cell, ByVal visitedcells As List(Of Cell))
        Dim Adjancent() As Integer = {0, 0, 0, 0}
        Dim Neighbours As New List(Of Cell)
        Dim tempcell As New Cell(cell.X, cell.Y - 2)
        If visitedcells.Contains(tempcell) And Not visitedcells.Contains(cell) Then Adjancent(0) = 1
        tempcell.Update(cell.X + 4, cell.Y)
        If visitedcells.Contains(tempcell) And Not visitedcells.Contains(cell) Then Adjancent(1) = 1
        tempcell.Update(cell.X, cell.Y + 2)
        If visitedcells.Contains(tempcell) And Not visitedcells.Contains(cell) Then Adjancent(2) = 1
        tempcell.Update(cell.X - 4, cell.Y)
        If visitedcells.Contains(tempcell) And Not visitedcells.Contains(cell) Then Adjancent(3) = 1
        Return Adjancent
    End Function
    Function PickAdjancentCell(ByVal cell As Cell, ByVal adjancencylist() As Integer)
        Dim ReturnCell As Cell
        Dim Neighbours As New List(Of Cell)
        If adjancencylist(0) = 1 Then
            Neighbours.Add(New Cell(cell.X, cell.Y - 2))
        End If
        If adjancencylist(1) = 1 Then
            Neighbours.Add(New Cell(cell.X + 4, cell.Y))
        End If
        If adjancencylist(2) = 1 Then
            Neighbours.Add(New Cell(cell.X, cell.Y + 2))
        End If
        If adjancencylist(3) = 1 Then
            Neighbours.Add(New Cell(cell.X - 4, cell.Y))
        End If
        Dim R As New Random
        If Neighbours.Count > 0 Then
            ReturnCell = Neighbours(R.Next(0, Neighbours.Count))
        End If
        Return ReturnCell
    End Function
    Function MidPoint(ByVal cell1 As Cell, ByVal cell2 As Cell)
        Dim x, y As Integer
        x = (cell1.X + cell2.X) / 2
        y = (cell1.Y + cell2.Y) / 2
        Dim newpoint As New Cell(((cell1.X + cell2.X) / 2), ((cell1.Y + cell2.Y) / 2))
        Return newpoint
    End Function
    Function RanNeighbour(ByVal current As Cell, ByVal Limits() As Integer)
        Dim neighbours As New List(Of Cell)
        Dim newPoint As New Cell(current.X - 4, current.Y)
        If newPoint.WithinLimits(Limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y - 2)
        If newPoint.WithinLimits(Limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y + 2)
        If newPoint.WithinLimits(Limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X + 4, current.Y)
        If newPoint.WithinLimits(Limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        Return neighbours
    End Function
    Function Neighbour(ByVal current As Cell, ByVal visited As List(Of Cell), ByVal Limits() As Integer, ByVal bool As Boolean)
        Dim neighbours As New List(Of Cell)
        Dim newPoint As New Cell(current.X - 4, current.Y)
        If newPoint.WithinLimits(Limits) Then If Not visited.Contains(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y - 2)
        If newPoint.WithinLimits(Limits) Then If Not visited.Contains(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y + 2)
        If newPoint.WithinLimits(Limits) Then If Not visited.Contains(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X + 4, current.Y)
        If newPoint.WithinLimits(Limits) Then If Not visited.Contains(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        If bool Then
            Return neighbours
        Else
            If neighbours.Count > 0 Then Return True
        End If
        Return False
    End Function
    Function GetNeighbours(ByRef current As Node, ByRef availablepath As List(Of Node))
        Dim neighbours As New List(Of Node)
        Dim newnode As New Node(current.X, current.Y - 1)
        If availablepath.Contains(newnode) Then neighbours.Add(New Node(newnode.X, newnode.Y))
        newnode.update(current.X + 2, current.Y)
        If availablepath.Contains(newnode) Then neighbours.Add(New Node(newnode.X, newnode.Y))
        newnode.update(current.X, current.Y + 1)
        If availablepath.Contains(newnode) Then neighbours.Add(New Node(newnode.X, newnode.Y))
        newnode.update(current.X - 2, current.Y)
        If availablepath.Contains(newnode) Then neighbours.Add(New Node(newnode.X, newnode.Y))
        Return neighbours
    End Function
End Module

Class Cell
    Public X, Y, CellSet As Integer
    Public Sub New(ByVal xpoint As Integer, ByVal ypoint As Integer)
        X = xpoint
        Y = ypoint
    End Sub
    Sub Update(ByVal _x As Integer, ByVal _y As Integer)
        X = _x
        Y = _y
    End Sub
    Function WithinLimits(ByVal limits() As Integer)
        If Me.X >= limits(0) And Me.X <= limits(2) And Me.Y >= limits(1) And Me.Y <= limits(3) Then Return True
        Return False
    End Function
    Public Function Pop(ByVal list As List(Of Cell))
        Dim val As Cell = list(list.Count - 1)
        list.RemoveAt(list.Count - 1)
        Return val
    End Function

    Public Sub Print(ByVal str As String)
        Console.SetCursorPosition(X, Y)
        Console.Write(str)
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim cell = TryCast(obj, Cell)
        Return cell IsNot Nothing AndAlso
               X = cell.X AndAlso
               Y = cell.Y
    End Function

    Public Overrides Function GetHashCode() As Integer
        Dim hashCode As Long = 1855483287
        hashCode = (hashCode * -1521134295 + X.GetHashCode()).GetHashCode()
        hashCode = (hashCode * -1521134295 + Y.GetHashCode()).GetHashCode()
        Return hashCode
    End Function
End Class
Public Class Node
    Public X, Y, gCost, hCost As Integer
    Public parent As Node
    Public Sub Print(ByVal letter As String)
        Console.SetCursorPosition(X, Y)
        Console.Write(letter)
    End Sub

    Public Sub New(ByVal xpoint As Integer, ByVal ypoint As Integer)
        X = xpoint
        Y = ypoint
    End Sub
    Public Sub update(ByVal xpoint As Integer, ByVal ypoint As Integer)
        X = xpoint
        Y = ypoint
    End Sub
    Public Function fCost()
        Return gCost + hCost
    End Function
    Public Overrides Function Equals(obj As Object) As Boolean
        Dim node = TryCast(obj, Node)
        Return node IsNot Nothing AndAlso
               X = node.X AndAlso
               Y = node.Y
    End Function
    Public Overrides Function GetHashCode() As Integer
        Dim hashCode As Long = 1855483287
        hashCode = (hashCode * -1521134295 + X.GetHashCode()).GetHashCode()
        hashCode = (hashCode * -1521134295 + Y.GetHashCode()).GetHashCode()
        Return hashCode
    End Function
End Class
Class Tree
    Public value As Integer
    Public left, right As Tree
    Public Function ContainsRecursive(ByVal current As Tree, ByVal value As Integer)
        If IsNothing(current) Then Return False
        If value = current.value Then Return True
        Return value < current.value
        ContainsRecursive(current.left, value)
        ContainsRecursive(current.right, value)
    End Function
    Public Function AddRecursive(ByVal current As Tree, ByVal value As Integer)
        If IsNothing(current) Then Return New Tree(value)
        If value < current.value Then
            current.left = AddRecursive(current.left, value)
        ElseIf current.value < value Then
            current.right = AddRecursive(current.right, value)
        Else
            Return current
        End If
        Return current
    End Function
    Function ExtractMin(ByVal node As Tree) As Integer
        Dim current As Tree = node
        While Not IsNothing(current.left)
            current = current.left
        End While
        Return current.value
    End Function
    Public Sub New(ByVal valu As Integer)
        value = valu
        left = Nothing
        right = Nothing
    End Sub
End Class