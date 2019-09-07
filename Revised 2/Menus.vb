Imports System.IO
Module Menus
    Sub Menu(ByVal arr() As String)
        Dim input As String = ""
        Dim PreviousAlgorithm As String = ""
        Dim PreviousMaze, LoadedMaze As New List(Of Node)
        Dim Width, Height, DelayMS, SolvingDelay, YPosAfterMaze, y As Integer
        Dim Limits(3) As Integer
        Dim ScreenWidth As Integer = Console.WindowWidth / 2
        Dim ShowMazeGeneration, ShowPath As Boolean
        Console.Clear()
        Dim CurrentCol As Integer = Console.CursorTop
        Dim NumOfOptions As Integer = arr.Count
        MsgColour("What Maze Generation Algorithm do you want to use: ", ConsoleColor.Yellow)
        MsgColour($"> {arr(0)}  ", ConsoleColor.Green)
        For i = 1 To arr.Count - 1
            Console.WriteLine($" {arr(i)}")
        Next
        While 1
            Console.BackgroundColor = (ConsoleColor.Black)
            Console.ForegroundColor = (ConsoleColor.White)
            Dim Info() As String = {"Information for using this program:", "Use the up and down arrow keys for navigating the menus", "Use the enter or right arrow key to select an option", "Press 'I' to bring up information on the algorithm", "", "When inputting integer values:", "The up and down arrow keys increment by 1", "The right and left arrow keys increment by 10", "The 'M' key will set the number to the maximum value it can be", "The 'H' key will set the number to half of the maximum value it can be"}
            For i = 0 To Info.Count - 1
                Console.SetCursorPosition(ScreenWidth - Info(i).Length / 2, i)
                If i <> 0 Then Console.ForegroundColor = (ConsoleColor.Magenta)
                Console.Write(Info(i))
            Next
            Dim key = Console.ReadKey
            Console.CursorVisible = False
            Select Case key.Key.ToString
                Case "DownArrow"
                    y += 1
                    If y = arr.Count Then y = 0
                    If y = 15 Or y = 20 Then y += 1
                Case "UpArrow"
                    y -= 1
                    If y = -1 Then y = arr.Count - 1
                    If y = 15 Or y = 20 Then y -= 1
                Case "Enter", "RightArrow"
                    Dim AvailablePath As List(Of Node)
                    If arr(y) = "Recursive Backtracker Algorithm (using iteration)" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = RecursiveBacktracker(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Recursive Backtracker Algorithm (using recursion)" Then
                        Dim r As New Random
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
                        Dim prev As Cell = CurrentCell '(Limits(0) + 3, Limits(1) + 2)
                        Dim v As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
                        v(CurrentCell) = True
                        Dim path As New List(Of Node)
                        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
                        SetBoth(ConsoleColor.White)
                        path.Add(New Node(CurrentCell.X, CurrentCell.Y))
                        If ShowMazeGeneration Then CurrentCell.Print("██")
                        path = RecursiveBacktrackerRecursively(CurrentCell, Limits, path, v, prev, r, ShowMazeGeneration, DelayMS)
                        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
                        SetBoth(ConsoleColor.White)
                        If Not ShowMazeGeneration Then
                            PrintMazeHorizontally(path, Limits(2), Limits(3))
                        End If
                        AddStartAndEnd(path, Limits, 0)
                        Solving(path, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                        PreviousMaze = path
                    ElseIf arr(y) = "Hunt and Kill Algorithm" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = HuntAndKillREFACTORED(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Prim's Algorithm (simplified)" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Prims_Simplified(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Prim's Algorithm (true)" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Prims_True(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Aldous-Broder Algorithm" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = AldousBroder(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Growing Tree Algorithm" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        Dim ArrOptions() As String = {"Newest (Recursive Backtracker)", "Random (Prim's simplified)", "Newest/Random, 75/25 split", "Newest/Random, 50/50 split", "Newest/Random, 25/75 split", "Oldest", "Middle", "Newest/Oldest, 50/50 split", "Oldest/Random, 50/50 split"}
                        Dim CellSelectionMethod() As Integer = PreGenMenu(ArrOptions, "What Cell selection method would you like to use: ")
                        AvailablePath = GrowingTree(Limits, DelayMS, CellSelectionMethod, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Sidewinder Algorithm" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Sidewinder(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Binary Tree Algorithm" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        Dim ArrOptions() As String = {"Northwest", "Northeast", "Southwest", "Southeast"}
                        Dim Bias() As Integer = PreGenMenu(ArrOptions, "Cell bias: ")
                        AvailablePath = BinaryTree(Limits, DelayMS, ShowMazeGeneration, Bias)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Wilson's Algorithm" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Wilsons(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Eller's Algorithm" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Ellers(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Kruskal's Algorithm" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Kruskals(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Houston's Algorithm" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Houstons(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Spiral Backtracker Algorithm" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = SpiralBacktracker(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Custom Algorithm" Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Custom(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf arr(y) = "Load the previously generated maze" Then
                        Dim GreatestX, GreatestY As Integer
                        If PreviousMaze.Count > 1 Then
                            Console.Clear()
                            Console.SetCursorPosition(0, 0)
                            Dim mess As String = "Algorithm used to generate this maze: "
                            Console.Write(mess)
                            Console.SetCursorPosition(mess.Length, 0)
                            MsgColour(PreviousAlgorithm, ConsoleColor.Green)
                            SetBoth(ConsoleColor.White)
                            For Each node In PreviousMaze
                                'node.Print("██")
                                If GreatestX < node.X Then GreatestX = node.X
                                If GreatestY < node.Y Then GreatestY = node.Y
                            Next
                            For __x = 1 To GreatestX + 1
                                For __y = 3 To GreatestY + 1
                                    If PreviousMaze.Contains(New Node(__x, __y)) Then
                                        Console.SetCursorPosition(__x, __y)
                                        Console.Write("██")
                                    End If
                                Next
                            Next
                            PrintStartandEnd(PreviousMaze)
                            Console.BackgroundColor = (ConsoleColor.Black)
                            DisplayAvailablePositions(PreviousMaze.Count)
                            YPosAfterMaze = GreatestY - 1
                            Console.SetCursorPosition(0, YPosAfterMaze + 3)
                            Dim temparr() As String = {"Solve using the A* algorithm", "Solve using Dijkstra's algorithm", "Solve using Breadth-first search", "Solve using Depth-first search (using iteration)", "Solve using Depth-first search (using recursion)", "Solve using the dead end filling method", "Play the maze", "Braid the maze (remove dead ends)", "Save the maze", "Output the maze as a png image", "Clear the maze and return to the menu"}
                            input = SolvingMenu(temparr, "What would you like to do with the maze", GreatestX + 3, 3)
                            SolvingInput(input, ShowPath, YPosAfterMaze, SolvingDelay, PreviousMaze, PreviousAlgorithm)
                        Else
                            Console.Clear()
                            MsgColour("No previous maze available", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    ElseIf arr(y) = "Save the previously generated maze" Then
                        If PreviousMaze.Count > 1 Then
                            SaveMaze(PreviousMaze, PreviousAlgorithm)
                        Else
                            Console.Clear()
                            MsgColour("No previous maze available", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    ElseIf arr(y) = "Output the previous maze as a png image" Then
                        If PreviousMaze.Count > 1 Then
                            Console.Clear()
                            Console.ForegroundColor = ConsoleColor.White
                            Console.Write("File Name of the maze to load (don't include .png): ")
                            Dim filename As String = Console.ReadLine
                            If Not System.IO.File.Exists(filename) Then
                                SaveMazePNG(PreviousMaze, $"Algorithm used to generate this maze: {PreviousAlgorithm}", filename)
                            Else
                                Console.Clear()
                                Console.ForegroundColor = ConsoleColor.Red
                                Console.WriteLine($"A file with the name {filename} already exists")
                                Console.ReadKey()
                            End If
                        Else
                            Console.Clear()
                            MsgColour("No previous maze available", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    ElseIf arr(y) = "Load a saved maze" Then
                        Dim ValidMaze, XMax, YMax As Integer
                        Dim GreatestY As Integer = 0
                        Dim GreatestX As Integer = 0
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
                            Dim UsedAlgorithm As String = ""
                            Dim c As Integer = 0
                            Dim e As Boolean = True
                            Console.Clear()
                            Using reader As StreamReader = New StreamReader(filename)
                                Do Until reader.EndOfStream
                                    If e Then
                                        UsedAlgorithm = reader.ReadLine
                                        e = False
                                    End If
                                    If c = 0 Then
                                        _x = Int(reader.ReadLine)
                                        If Int(_x) > GreatestX Then GreatestX = Int(_x)
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
                                Dim temparr() As String = {"Solve using the A* algorithm", "Solve using Dijkstra's algorithm", "Solve using Breadth-first search", "Solve using Depth-first search (using iteration)", "Solve using Depth-first search (using recursion)", "Solve using the dead end filling method", "Play the maze", "Braid the maze (remove dead ends)", "Output the maze as a png image", "Clear the maze and return to the menu"}
                                input = SolvingMenu(temparr, "What would you like to do with the maze", GreatestX + 3, 3)
                                SolvingInput(input, ShowPath, YPosAfterMaze, SolvingDelay, PreviousMaze, UsedAlgorithm)
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
                    Console.BackgroundColor = (ConsoleColor.Black)
                    Console.Clear()
                    MsgColour("What Maze Generation Algorithm do you want to use: ", ConsoleColor.Yellow)
                Case "I"
                    InitialiseScreen()
                    If arr(y) = "Recursive Backtracker Algorithm (using iteration)" Then
                        RecrusiveBacktrackerINFO()
                    ElseIf arr(y) = "Recursive Backtracker Algorithm (using recursion)" Then
                        RecrusiveBacktrackerRecursionINFO()
                    ElseIf arr(y) = "Hunt and Kill Algorithm" Then
                        HuntAndKillINFO()
                    ElseIf arr(y) = "Prim's Algorithm (simplified)" Then
                        Prims_SimplifiedINFO()
                    ElseIf arr(y) = "Prim's Algorithm (true)" Then
                        Prims_TrueINFO()
                    ElseIf arr(y) = "Aldous-Broder Algorithm" Then
                        AldousBroderINFO()
                    ElseIf arr(y) = "Growing Tree Algorithm" Then
                        GrowingTreeINFO()
                    ElseIf arr(y) = "Sidewinder Algorithm" Then
                        SidewinderINFO()
                    ElseIf arr(y) = "Binary Tree Algorithm" Then
                        BinaryTreeINFO()
                    ElseIf arr(y) = "Wilson's Algorithm" Then
                        WilsonsINFO()
                    ElseIf arr(y) = "Eller's Algorithm" Then
                        EllersINFO()
                    ElseIf arr(y) = "Kruskal's Algorithm" Then
                        KruskalsINFO()
                    ElseIf arr(y) = "Houston's Algorithm" Then
                        HoustonsINFO()
                    ElseIf arr(y) = "Spiral Backtracker Algorithm" Then
                        SpiralBacktrackerINFO()
                    ElseIf arr(y) = "Custom Algorithm" Then
                        CustomAlgorithmINFO()
                    End If
                    Console.ReadKey()
                    Console.BackgroundColor = (ConsoleColor.Black)
                    Console.Clear()
                    MsgColour("What Maze Generation Algorithm do you want to use: ", ConsoleColor.Yellow)
            End Select
            Console.ForegroundColor = (ConsoleColor.White)
            Dim Count As Integer = 1
            For Each MenuOption In arr
                Console.SetCursorPosition(0, Count + CurrentCol)
                Console.Write($" {MenuOption}    ")
                Count += 1
            Next
            Console.SetCursorPosition(0, y + 1)
            MsgColour($"> {arr(y)}  ", ConsoleColor.Green)
        End While
    End Sub
    Sub Solving(ByVal AvailablePath As List(Of Node), ByVal Limits() As Integer, ByRef PreviousMaze As List(Of Node), ByRef Input As String, ByVal YPosAfterMaze As Integer, ByVal ShowPath As Boolean, ByVal SolvingDelay As Integer, ByRef Algorithm As String, ByRef SetPreivousAlgorithm As String)
        If AvailablePath IsNot Nothing Then
            SetPreivousAlgorithm = Algorithm
            PreSolving(Limits, AvailablePath, PreviousMaze, Input, YPosAfterMaze)
            SolvingInput(Input, ShowPath, YPosAfterMaze, SolvingDelay, AvailablePath, Algorithm)
        End If
    End Sub
    Sub PreSolving(ByVal limits() As Integer, ByVal availablepath As List(Of Node), ByRef previousmaze As List(Of Node), ByRef input As String, ByRef yposaftermaze As Integer)
        Console.BackgroundColor = (ConsoleColor.Black)
        yposaftermaze = limits(3)
        DisplayAvailablePositions(availablepath.Count)
        Console.SetCursorPosition(0, yposaftermaze + 3)
        Dim temparr() As String = {"Solve using the A* algorithm", "Solve using Dijkstra's algorithm", "Solve using Breadth-first search", "Solve using Depth-first search (using iteration)", "Solve using Depth-first search (using recursion)", "Solve using the dead end filling method", "Play the maze", "Save the maze", "Braid the maze (remove dead ends)", "Output the maze as a png image", "Clear the maze and return to the menu"}
        input = SolvingMenu(temparr, "What would you like to do with the maze", limits(2) + 2, 3)
        previousmaze.Clear()
        previousmaze = availablepath
    End Sub
    Sub ClearHorizontal(ByVal y As Integer, ByVal ClearMessage As Boolean, ByVal setafter As Boolean)
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, y)
        If ClearMessage Then Console.Write("                                                                                                               ")
        Console.SetCursorPosition(0, y + If(setafter, 1, 0))
    End Sub
    Function HorizontalYesNo(ByVal ColumnPosition As Integer, ByVal message As String, ByVal ClearMessage As Boolean, ByVal ClearBefore As Boolean, ByVal SetAfter As Boolean)
        If ClearBefore Then Console.Clear()
        Console.ForegroundColor = (ConsoleColor.White)
        Dim Choice As Boolean = True
        Dim x, y As Integer
        y = ColumnPosition
        Console.SetCursorPosition(x, y)
        Console.Write(message)
        MsgColour("> Yes", ConsoleColor.Green)
        Console.SetCursorPosition(message.Length + 10, y)
        Console.Write(" No")
        While 1
            Console.ForegroundColor = ConsoleColor.Black
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "RightArrow"
                    If Choice Then Choice = False
                Case "LeftArrow"
                    If Not Choice Then Choice = True
                Case "Enter"
                    ClearHorizontal(y, ClearMessage, SetAfter)
                    If Choice Then
                        Return True
                    Else
                        Return False
                    End If
                Case "Escape"
                    Return Nothing
                Case "Y"
                    ClearHorizontal(y, ClearMessage, SetAfter)
                    Return True
                Case "N"
                    ClearHorizontal(y, ClearMessage, SetAfter)
                    Return False
            End Select
            Console.SetCursorPosition(0, y)
            Console.ForegroundColor = ConsoleColor.White
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
    Function SolvingMenu(ByVal arr() As String, ByVal Message As String, ByVal X As Integer, ByVal Y_ As Integer)
        Dim temparr() As String = arr
        Dim CurrentCol As Integer = 0 'Console.CursorTop
        Dim y As Integer = 0
        Dim NumOfOptions As Integer = arr.Count
        Console.SetCursorPosition(X, y + Y_)
        MsgColour(Message, ConsoleColor.Yellow)
        Console.SetCursorPosition(X, y + 1 + Y_)
        MsgColour($"> {arr(0)}", ConsoleColor.Green)
        For i = 1 To arr.Count - 1
            Console.SetCursorPosition(X, i + 1 + Y_)
            Console.Write($" {arr(i)}")
        Next
        While 1
            Console.BackgroundColor = (ConsoleColor.Black)
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "DownArrow"
                    y += 1
                    If y = arr.Count Then y = 0
                Case "UpArrow"
                    y -= 1
                    If y = -1 Then y = arr.Count - 1
                Case "S"
                    For i = 0 To arr.Count
                        Console.SetCursorPosition(X, i + Y_)
                        Console.Write("                                                        ")
                    Next
                    Return "s"
                Case "Enter"
                    For i = 0 To arr.Count
                        Console.SetCursorPosition(X, i + Y_)
                        Console.Write("                                                        ")
                    Next
                    Return temparr(y)
            End Select
            Console.ForegroundColor = (ConsoleColor.White)
            Dim Count As Integer = 1
            For Each MenuOption In arr
                Console.SetCursorPosition(X, Count + CurrentCol + Y_)
                Console.Write($" {MenuOption}  ")
                Count += 1
            Next
            Console.SetCursorPosition(X, y + 1 + Y_)
            MsgColour($"> {arr(y)}", ConsoleColor.Green)
        End While
        Return Nothing
    End Function
    Function GetIntInputArrowKeys(ByVal message As String, ByVal NumMax As Integer, ByVal NumMin As Integer, ByVal ClearMessage As Boolean)
        Console.Write(message)
        Console.ForegroundColor = (ConsoleColor.Magenta)
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
                Case "M"
                    current = NumMax
                Case "H"
                    current = NumMax / 2
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
        Console.ForegroundColor = (ConsoleColor.White)
        Return current
    End Function
    Sub SolvingInput(ByVal input As String, ByVal showpath As Boolean, ByVal YposAfterMaze As Integer, ByVal solvingdelay As Integer, ByVal Maze As List(Of Node), ByVal Algorithm As String)
        If input = "Solve using the A* algorithm" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            If HorizontalYesNo(YposAfterMaze + 2, "Do you want to use the optimised version of A*: ", True, False, False) Then
                aStar(Maze, showpath, True, solvingdelay)
            Else
                Dim neededNodes As List(Of Node) = GetNeededNodes(Maze)
                Dim AdjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, Maze)
                aStarWiki(AdjacencyList, showpath, True, solvingdelay)
            End If
        ElseIf input = "Solve using Dijkstra's algorithm" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Dim neededNodes As List(Of Node) = GetNeededNodes(Maze)
            Dim AdjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, Maze)
            Dijkstras(AdjacencyList, showpath, solvingdelay)
        ElseIf input = "Solve using Breadth-first search" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            BFS(Maze, showpath, True, solvingdelay)
        ElseIf input = "Solve using Depth-first search (using iteration)" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Dim neededNodes As List(Of Node) = GetNeededNodes(Maze)
            Dim AdjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, Maze)
            DFS_Iterative(AdjacencyList, showpath, True, solvingdelay)
        ElseIf input = "Solve using Depth-first search (using recursion)" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Dim start_v As New Node(Maze(Maze.Count - 2).X, Maze(Maze.Count - 2).Y)
            Dim goal As New Node(Maze(Maze.Count - 1).X, Maze(Maze.Count - 1).Y)
            Dim discovered As New Dictionary(Of Node, Boolean)
            For Each node In Maze
                discovered(node) = False
            Next
            Dim cameFrom As New Dictionary(Of Node, Node)
            Dim timer As Stopwatch = Stopwatch.StartNew
            Console.ForegroundColor = ConsoleColor.Red
            Console.BackgroundColor = ConsoleColor.Red
            DFS_Recursive(Maze, start_v, discovered, cameFrom, goal, showpath, solvingdelay, False)
            ReconstructPath(cameFrom, goal, start_v, $"Time Taken to solve: {timer.Elapsed.TotalSeconds} seconds")
        ElseIf input = "Play the maze" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps you have taken in the maze: ", True, False, False)
            Playmaze(Maze, showpath)
        ElseIf input = "Clear the maze and return to the menu" Then
            Console.Clear()
        ElseIf input = "Save the maze" Then
            SaveMaze(Maze, $"Algorithm used to generate this maze: {Algorithm}")
        ElseIf input = "Output the maze as a png image" Then
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.White
            Console.Write("File Name of the maze to load (don't include .png): ")
            Dim filename As String = Console.ReadLine
            Console.Clear()
            SaveMazePNG(Maze, Algorithm, filename)
        ElseIf input = "s" Then
            SD(Maze)
        ElseIf input = "Solve using the dead end filling method" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            DeadEndFiller(Maze, showpath, True, solvingdelay)
        ElseIf input = "Solve using the wall follower method" Then
            WallFollower(Maze, showpath, solvingdelay)
        ElseIf input = "Braid the maze (remove dead ends)" Then
            EliminateDeadEnds(Maze)
            Dim GreatestX As Integer
            For Each node In Maze
                If GreatestX < node.X Then GreatestX = node.X
            Next
            Dim temparr() As String = {"Solve using the A* algorithm", "Solve using Dijkstra's algorithm", "Solve using Breadth-first search", "Solve using Depth-first search (using iteration)", "Solve using Depth-first search (using recursion)", "Solve using the dead end filling method", "Play the maze", "Save the maze", "Output the maze as a png image", "Clear the maze and return to the menu"}
            input = SolvingMenu(temparr, "What would you like to do with the maze", GreatestX + 3, 3)
            SolvingInput(input, showpath, YposAfterMaze, solvingdelay, Maze, "")
        ElseIf input = "" Then
            Console.Clear()
            Console.WriteLine("A critical error has occured that has caused the program to no longer work")
            End
        End If
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
        Width = (GetIntInputArrowKeys($"Width of the Maze: ", (Console.WindowWidth - 54) / 2, 20, False)) * 2
        Height = GetIntInputArrowKeys($"Height of the Maze: ", Console.WindowHeight - 7, 20, False)
        If Width Mod 2 = 0 Then
            Width += 1
            Height += 1
        End If
        If Height Mod 2 = 0 Then
            Width += 1
            Height += 1
        End If
        Limits = {5, 3, Width, Height}
        Console.Clear()
    End Sub
End Module