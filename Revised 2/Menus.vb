Imports System.IO
Imports System.Drawing
Module Menus
    Sub Menu(arr() As String, topitem As String)
        Dim input = ""
        Dim previousAlgorithm = ""
        Dim previousMaze, loadedMaze As New List(Of Node)
        Dim width, height, delayMs, solvingDelay, yPosAfterMaze, y As Integer
        y = 1
        Dim limits(3) As Integer
        Dim screenWidth As Integer = Console.WindowWidth / 2
        Dim showMazeGeneration, showPath As Boolean
        Console.Clear()
        Dim currentCol As Integer = Console.CursorTop
        Dim NumOfOptions As Integer = arr.Count
        Console.ResetColor()
        MsgColour($"{topitem}: ", ConsoleColor.White)
        For i = 0 To arr.Count - 1
            If arr(i) = arr(y) Then
                MsgColour($"> {arr(1)}  ", ConsoleColor.Green)
            Else
                Console.WriteLine($" {arr(i)}")
            End If
        Next
        While 1
            Dim info() As String = {"Information for using this program:", "Use the up and down arrow keys for navigating the menus", "Use the enter key to select an option", "Press 'I' to bring up information on the algorithm", "", "When inputting integer values:", "The up and down arrow keys increment by 1", "The right and left arrow keys increment by 10", "The 'M' key will set the number to the maximum value it can be", "The 'H' key will set the number to half of the maximum value it can be"}
            For i = 0 To info.Count - 1
                Console.SetCursorPosition(screenWidth - info(i).Length / 2, i)
                If i <> 0 Then Console.ForegroundColor = (ConsoleColor.Magenta)
                Console.Write(info(i))
            Next
            SetBoth(ConsoleColor.Black)
            Dim key = Console.ReadKey
            Console.CursorVisible = False
            Select Case key.Key.ToString
                Case "DownArrow"
                    y += 1
                    If y = arr.Count Then y = 1
                    If arr(y) = "" Or arr(y) = "Generate a maze using one of the following algorithms" Then y += 1
                Case "UpArrow"
                    y -= 1
                    If y = 0 Then y = arr.Count - 1
                    If arr(y) = "" Or arr(y) = "Generate a maze using one of the following algorithms" Then y -= 1
                Case "Enter"
                    Console.ForegroundColor = ConsoleColor.White
                    Dim availablePath As List(Of Node)
                    If arr(y) = "   Recursive Backtracker Algorithm (using iteration)" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        availablePath = RecursiveBacktracker.RecursiveBacktracker(limits, delayMs, showMazeGeneration)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "   Recursive Backtracker Algorithm (using recursion)" Then
                        Dim r As New Random
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        Dim CurrentCell As Cell = PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
                        Dim prev As Cell = CurrentCell '(Limits(0) + 3, Limits(1) + 2)
                        Dim v As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
                        v(CurrentCell) = True
                        Dim path As New List(Of Node)
                        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
                        SetBoth(ConsoleColor.White)
                        path.Add(New Node(CurrentCell.X, CurrentCell.Y))
                        If showMazeGeneration Then CurrentCell.Print("██")
                        path = RecursiveBacktrackerRecursively(CurrentCell, limits, path, v, prev, r, showMazeGeneration, delayMs)
                        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
                        SetBoth(ConsoleColor.White)
                        If Not showMazeGeneration Then
                            PrintMazeHorizontally(path, limits(2), limits(3))
                        End If
                        AddStartAndEnd(path, limits, 0)
                        Solving(path, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                        previousMaze = path
                    ElseIf arr(y) = "   Hunt and Kill Algorithm" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        availablePath = HuntAndKillRefactored(limits, delayMs, showMazeGeneration)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "   Prim's Algorithm (simplified)" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        availablePath = Prims_Simplified(limits, delayMs, showMazeGeneration)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "   Prim's Algorithm (true)" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        availablePath = Prims_True(limits, delayMs, showMazeGeneration)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "   Aldous-Broder Algorithm" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        availablePath = AldousBroder.AldousBroder(limits, delayMs, showMazeGeneration)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "   Growing Tree Algorithm" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        Dim arrOptions() As String = {"Newest (Recursive Backtracker)", "Random (Prim's simplified)", "Newest/Random, 75/25 split", "Newest/Random, 50/50 split", "Newest/Random, 25/75 split", "Oldest", "Middle", "Newest/Oldest, 50/50 split", "Oldest/Random, 50/50 split"}
                        Dim cellSelectionMethod() As Integer = PreGenMenu(arrOptions, "What Cell selection method would you like to use: ")
                        availablePath = GrowingTree(limits, delayMs, cellSelectionMethod, showMazeGeneration)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "   Sidewinder Algorithm" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        availablePath = Sidewinder(limits, delayMs, showMazeGeneration)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "   Binary Tree Algorithm" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        Dim ArrOptions() As String = {"Northwest", "Northeast", "Southwest", "Southeast"}
                        Dim Bias() As Integer = PreGenMenu(ArrOptions, "Cell bias: ")
                        availablePath = BinaryTree(limits, delayMs, showMazeGeneration, Bias)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "   Wilson's Algorithm" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        availablePath = Wilsons(limits, delayMs, showMazeGeneration)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "   Eller's Algorithm" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        availablePath = Ellers(limits, delayMs, showMazeGeneration)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "   Kruskal's Algorithm" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        availablePath = Kruskals(limits, delayMs, showMazeGeneration)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "   Houston's Algorithm" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        availablePath = Houstons(limits, delayMs, showMazeGeneration)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "   Spiral Backtracker Algorithm" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        availablePath = SpiralBacktracker(limits, delayMs, showMazeGeneration)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "   Custom Algorithm" Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0)
                        availablePath = Custom(limits, delayMs, showMazeGeneration)
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm)
                    ElseIf arr(y) = "Load the previously generated maze" Then
                        Dim greatestX, greatestY As Integer
                        If previousMaze.Count > 1 And Not IsNothing(previousMaze) Then
                            Console.Clear()
                            Console.SetCursorPosition(0, 0)
                            Dim mess = "Algorithm used to generate this maze: "
                            Console.Write(mess)
                            Console.SetCursorPosition(mess.Length, 0)
                            MsgColour(previousAlgorithm, ConsoleColor.Green)
                            SetBoth(ConsoleColor.White)
                            For Each node In previousMaze
                                'node.Print("██")
                                If greatestX < node.X Then greatestX = node.X
                                If greatestY < node.Y Then greatestY = node.Y
                            Next
                            For __x = 1 To greatestX + 1
                                For __y = 3 To greatestY + 1
                                    If previousMaze.Contains(New Node(__x, __y)) Then
                                        Console.SetCursorPosition(__x, __y)
                                        Console.Write("██")
                                    End If
                                Next
                            Next
                            PrintStartandEnd(previousMaze)
                            Console.BackgroundColor = (ConsoleColor.Black)
                            DisplayAvailablePositions(previousMaze.Count)
                            yPosAfterMaze = greatestY - 1
                            Console.SetCursorPosition(0, yPosAfterMaze + 3)
                            Dim temparr() As String = {"Solve using the A* algorithm",
                                "Solve using Dijkstra's algorithm",
                                "Solve using Breadth-first search",
                                "Solve using Depth-first search (using iteration)",
                                "Solve using Depth-first search (using recursion)",
                                "Solve using a recursive algorithm",
                                "Solve using the Lee Algorithm (Wave Propagation)",
                                "Solve using the dead end filling method",
                                "Solve using the left-hand rule",
                                "Solve using the right-hand rule",
                                "",
                                "Play the maze", "Braid (remove dead ends)", "Partial braid (remove some dead ends)", "Make the maze sparse (remove some passages)",
                                "",
            "Get the average corridor length",
            "Get the amount of corners in the maze",
            "Get the amount of junctions in the maze",
            "Get the amount of Dead-ends in the maze",
            "",
                                "Save the maze as points",
                                "Save the maze as a png image",
                                "Save the maze as an ascii text file",
                                "",
                                "Clear the maze and return to the menu"}
                            input = SolvingMenu(temparr, "What would you like to do with the maze", greatestX + 3, 3)
                            SolvingInput(input, showPath, yPosAfterMaze, solvingDelay, previousMaze, previousAlgorithm)
                        Else
                            Console.Clear()
                            MsgColour("No previous maze available", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    ElseIf arr(y) = "Save the previously generated maze as a list of points" Then
                        If previousMaze.Count > 1 Then
                            SaveMazeTextFile(previousMaze, previousAlgorithm)
                        Else
                            Console.Clear()
                            MsgColour("No previous maze available", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    ElseIf arr(y) = "Save the previous maze as a png image" Then
                        If previousMaze.Count > 1 Then
                            Console.Clear()
                            Console.ForegroundColor = ConsoleColor.White
                            Dim filename As String = GetValidFileName()
                            SaveMazePng(previousMaze, $"Algorithm used to generate this maze: {previousAlgorithm}", filename)
                        Else
                            Console.Clear()
                            MsgColour("No previous maze available", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    ElseIf arr(y) = "Load a maze from a text file (list of points)" Then
                        Dim validMaze, xMax, yMax As Integer
                        Dim greatestY = 0
                        Dim greatestX = 0
                        validMaze = 1
                        xMax = Console.WindowWidth - 6
                        yMax = Console.WindowHeight - 4
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
                                        Console.WriteLine($"({_x}, {_y})")
                                        loadedMaze.Add(New Node(_x, _y))
                                        c = 0
                                    End If
                                Loop
                            End Using
                            If loadedMaze.Count < 1 Then validMaze = 2
                            If validMaze = 1 Then
                                MsgColour($"Finished loading maze positions, total maze positions: {loadedMaze.Count}", ConsoleColor.Green)
                                Console.ReadKey()
                                Console.Clear()
                                Console.SetCursorPosition(0, 0)
                                Dim mess = "Algorithm used to generate this maze: "
                                Console.Write(mess)
                                Console.SetCursorPosition(mess.Length, 0)
                                MsgColour(usedAlgorithm, ConsoleColor.Green)
                                SetBoth(ConsoleColor.White)
                                For Each node In loadedMaze
                                    node.Print("██")
                                Next
                                PrintStartandEnd(loadedMaze)
                                yPosAfterMaze = greatestY
                                DisplayAvailablePositions(previousMaze.Count)
                                Console.SetCursorPosition(0, yPosAfterMaze + 3)
                                previousMaze = loadedMaze
                                Dim temparr() As String = {"Solve using the A* algorithm",
                                "Solve using Dijkstra's algorithm",
                                "Solve using Breadth-first search",
                                "Solve using Depth-first search (using iteration)",
                                "Solve using Depth-first search (using recursion)",
                                "Solve using a recursive algorithm",
                                "Solve using the Lee Algorithm (Wave Propagation)",
                                "Solve using the dead end filling method",
                                "Solve using the left-hand rule",
                                "Solve using the right-hand rule",
                                "",
                                "Play the maze", "Braid (remove dead ends)", "Partial braid (remove some dead ends)", "Make the maze sparse (remove some passages)",
                                "",
            "Get the average corridor length",
            "Get the amount of corners in the maze",
            "Get the amount of junctions in the maze",
            "Get the amount of Dead-ends in the maze",
            "",
                                "Save the maze as points",
                                "Save the maze as a png image",
                                "Save the maze as an ascii text file",
                                "",
                                "Clear the maze and return to the menu"}
                                input = SolvingMenu(temparr, "What would you like to do with the maze", greatestX + 3, 3)
                                SolvingInput(input, showPath, yPosAfterMaze, solvingDelay, previousMaze, usedAlgorithm)
                            ElseIf validMaze = 0 Then
                                Console.Clear()
                                MsgColour("Maze is too big for the screen, please decrease the font size and try again", ConsoleColor.Red)
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
                    ElseIf arr(y) = "Load a maze from an image file" Then
                        Dim tempMaze As List(Of Node) = LoadMazePng()
                        If IsNothing(tempMaze) Then
                            Console.Clear()
                            Console.ForegroundColor = ConsoleColor.Red
                            Console.BackgroundColor = ConsoleColor.Black
                            Console.WriteLine("The maze that you tried to load is invalid")
                            Console.ReadKey()
                        Else
                            previousMaze = tempMaze
                        End If
                    ElseIf arr(y) = "Save the previous maze to ascii text file" Then
                        If previousMaze.Count > 0 Then
                            SaveMazeAscii(previousMaze)
                        Else
                            Console.Clear()
                            Console.ForegroundColor = ConsoleColor.Red
                            Console.WriteLine($"No previous maze available")
                            Console.ReadKey()
                        End If
                    ElseIf arr(y) = "Load a maze from an ascii text file" Then
                        Console.Clear()
                        Dim tempMaze As List(Of Node) = LoadMazeAscii()
                        If IsNothing(tempMaze) Then
                            Console.Clear()
                            Console.ForegroundColor = ConsoleColor.Red
                            Console.BackgroundColor = ConsoleColor.Black
                            Console.WriteLine("The maze that you tried to load is too big for the console window, please decrease font size and try again")
                            Console.ReadKey()
                        Else
                            previousMaze = tempMaze
                        End If
                    ElseIf y = arr.Count - 1 Then
                        End
                    Else
                        OptionNotReady()
                    End If
                    Console.BackgroundColor = (ConsoleColor.Black)
                    Console.Clear()
                    MsgColour($"{topitem}: ", ConsoleColor.Yellow)
                Case "I"
                    If y < 16 Then
                        InitialiseScreen()
                        If arr(y) = "   Recursive Backtracker Algorithm (using iteration)" Then
                            RecrusiveBacktrackerInfo()
                        ElseIf arr(y) = "   Recursive Backtracker Algorithm (using recursion)" Then
                            RecrusiveBacktrackerRecursionInfo()
                        ElseIf arr(y) = "   Hunt and Kill Algorithm" Then
                            HuntAndKillInfo()
                        ElseIf arr(y) = "   Prim's Algorithm (simplified)" Then
                            Prims_SimplifiedINFO()
                        ElseIf arr(y) = "   Prim's Algorithm (true)" Then
                            Prims_TrueINFO()
                        ElseIf arr(y) = "   Aldous-Broder Algorithm" Then
                            AldousBroderInfo()
                        ElseIf arr(y) = "   Growing Tree Algorithm" Then
                            GrowingTreeInfo()
                        ElseIf arr(y) = "   Sidewinder Algorithm" Then
                            SidewinderInfo()
                        ElseIf arr(y) = "   Binary Tree Algorithm" Then
                            BinaryTreeInfo()
                        ElseIf arr(y) = "   Wilson's Algorithm" Then
                            WilsonsInfo()
                        ElseIf arr(y) = "   Eller's Algorithm" Then
                            EllersInfo()
                        ElseIf arr(y) = "   Kruskal's Algorithm" Then
                            KruskalsInfo()
                        ElseIf arr(y) = "   Houston's Algorithm" Then
                            HoustonsInfo()
                        ElseIf arr(y) = "   Spiral Backtracker Algorithm" Then
                            SpiralBacktrackerInfo()
                        ElseIf arr(y) = "   Custom Algorithm" Then
                            CustomAlgorithmInfo()
                        End If
                        Console.ReadKey()
                        Console.BackgroundColor = (ConsoleColor.Black)
                        Console.Clear()
                        MsgColour("What Maze Generation Algorithm do you want to use: ", ConsoleColor.Yellow)
                    End If
            End Select
            Console.ResetColor()
            Console.ForegroundColor = (ConsoleColor.White)
            Dim Count = 1
            For Each MenuOption In arr
                Console.SetCursorPosition(0, Count + currentCol)
                Console.Write($" {MenuOption}    ")
                Count += 1
            Next
            Console.SetCursorPosition(0, y + 1)
            MsgColour($"> {arr(y)}  ", ConsoleColor.Green)
        End While
    End Sub
    Sub Solving(AvailablePath As List(Of Node), Limits() As Integer, ByRef PreviousMaze As List(Of Node), ByRef Input As String, YPosAfterMaze As Integer, ShowPath As Boolean, SolvingDelay As Integer, ByRef Algorithm As String, ByRef SetPreivousAlgorithm As String)
        If AvailablePath IsNot Nothing Then
            SetPreivousAlgorithm = Algorithm
            PreSolving(Limits, AvailablePath, PreviousMaze, Input, YPosAfterMaze)
            SolvingInput(Input, ShowPath, YPosAfterMaze, SolvingDelay, AvailablePath, Algorithm)
        End If
    End Sub
    Sub PreSolving(limits() As Integer, availablepath As List(Of Node), ByRef previousmaze As List(Of Node), ByRef input As String, ByRef yposaftermaze As Integer)
        Console.BackgroundColor = (ConsoleColor.Black)
        yposaftermaze = limits(3)
        DisplayAvailablePositions(availablepath.Count)
        Console.SetCursorPosition(0, yposaftermaze + 3)
        Dim temparr() As String = {"Solve using the A* algorithm",
            "Solve using Dijkstra's algorithm",
            "Solve using Breadth-first search",
            "Solve using Depth-first search (using iteration)",
            "Solve using Depth-first search (using recursion)",
            "Solve using a recursive algorithm",
            "Solve using the Lee Algorithm (Wave Propagation)",
            "Solve using the dead end filling method",
            "Solve using the left-hand rule",
            "Solve using the right-hand rule",
            "",
            "Play the maze",
            "Braid (remove dead ends)", "Partial braid (remove some dead ends)", "Make the maze sparse (remove some passages)","Make the maze unicursal",
            "",
            "Get the average corridor length",
            "Get the amount of corners in the maze",
            "Get the amount of junctions in the maze",
            "Get the amount of Dead-ends in the maze",
            "",
            "Save the maze as points",
            "Save the maze as a png image",
            "Save the maze as an ascii text file",
            "",
            "Clear the maze and return to the menu"}
        input = SolvingMenu(temparr, "What would you like to do with the maze", limits(2) + 2, 3)
        previousmaze.Clear()
        previousmaze = availablepath
    End Sub
    Sub ClearHorizontal(y As Integer, ClearMessage As Boolean, setafter As Boolean)
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, y)
        If ClearMessage Then Console.Write("                                                                                                               ")
        Console.SetCursorPosition(0, y + If(setafter, 1, 0))
    End Sub
    Function HorizontalYesNo(ColumnPosition As Integer, message As String, ClearMessage As Boolean, ClearBefore As Boolean, SetAfter As Boolean)
        If ClearBefore Then Console.Clear()
        Console.ForegroundColor = (ConsoleColor.White)
        Dim Choice = True
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
    Function SolvingMenu(arr() As String, Message As String, X As Integer, Y_ As Integer)
        Dim temparr() As String = arr
        Dim CurrentCol = 0 'Console.CursorTop
        Dim y = 0
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
                    If arr(y) = "" Then y += 1
                Case "UpArrow"
                    y -= 1
                    If y = -1 Then y = arr.Count - 1
                    If arr(y) = "" Then y -= 1
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
            Dim count = 1
            For Each MenuOption In arr
                Console.SetCursorPosition(X, count + CurrentCol + Y_)
                Console.Write($" {MenuOption}  ")
                count += 1
            Next
            Console.SetCursorPosition(X, y + 1 + Y_)
            MsgColour($"> {arr(y)}", ConsoleColor.Green)
        End While
        Return Nothing
    End Function
    Function GetIntInputArrowKeys(message As String, NumMax As Integer, NumMin As Integer, ClearMessage As Boolean)
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
    Sub SolvingInput(input As String, showpath As Boolean, YposAfterMaze As Integer, solvingdelay As Integer, Maze As List(Of Node), Algorithm As String)
        If input = "Solve using the A* algorithm" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            If HorizontalYesNo(YposAfterMaze + 2, "Do you want to use the optimised version of A*: ", True, False, False) Then
                AStar(Maze, showpath, True, solvingdelay, False)
            Else
                Dim neededNodes As List(Of Node) = GetNeededNodes(Maze)
                Dim adjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, Maze)
                AStarWiki(adjacencyList, showpath, True, solvingdelay, False)
            End If
        ElseIf input = "Solve using Dijkstra's algorithm" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            'Dim neededNodes As List(Of Node) = GetNeededNodes(Maze)
            'Dim AdjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, Maze)
            Dijkstras(Maze, showpath, solvingdelay, False)
        ElseIf input = "Solve using Breadth-first search" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Bfs(Maze, showpath, True, solvingdelay, False)
        ElseIf input = "Solve using a recursive algorithm" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Dim visited As New Dictionary(Of Node, Boolean)
            Dim correctPath As New Dictionary(Of Node, Boolean)
            For Each node In Maze
                visited(node) = False
            Next
            SetBoth(ConsoleColor.Black)
            Console.ForegroundColor = ConsoleColor.White
            Console.SetCursorPosition(0, Console.WindowHeight - 1)
            Console.Write("Solving                            ")
            SetBoth(ConsoleColor.Red)
            Dim stopwatch As Stopwatch = Stopwatch.StartNew()
            Dim b As Boolean = RecursiveSolve(Maze, visited, correctPath, Maze(Maze.Count - 2).X, Maze(Maze.Count - 2).Y, New Node(Maze(Maze.Count - 1).X, Maze(Maze.Count - 1).Y), showpath, solvingdelay)
            SetBoth(ConsoleColor.Green)
            For Each thing In correctPath
                If thing.Value Then thing.Key.Print("XX")
            Next
            SetBoth(ConsoleColor.Black)
            Console.ForegroundColor = ConsoleColor.White
            Console.SetCursorPosition(35, Console.WindowHeight - 1)
            Console.Write($"Time taken: {stopwatch.Elapsed.TotalSeconds}")
            Console.ReadKey()
        ElseIf input = "Solve using Depth-first search (using iteration)" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Dim neededNodes As List(Of Node) = GetNeededNodes(Maze)
            Dim adjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, Maze)
            DFS_Iterative(adjacencyList, showpath, True, solvingdelay, False)
        ElseIf input = "Solve using Depth-first search (using recursion)" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Dim startV As New Node(Maze(Maze.Count - 2).X, Maze(Maze.Count - 2).Y)
            Dim goal As New Node(Maze(Maze.Count - 1).X, Maze(Maze.Count - 1).Y)
            Dim discovered As New Dictionary(Of Node, Boolean)
            For Each node In Maze
                discovered(node) = False
            Next
            Dim cameFrom As New Dictionary(Of Node, Node)
            Dim timer As Stopwatch = Stopwatch.StartNew
            Console.ForegroundColor = ConsoleColor.Red
            Console.BackgroundColor = ConsoleColor.Red
            DFS_Recursive(Maze, startV, discovered, cameFrom, goal, showpath, solvingdelay, False)
            ReconstructPath(cameFrom, goal, startV, $"{timer.Elapsed.TotalSeconds}")
            Console.ReadKey()
        ElseIf input = "Play the maze" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps you have taken in the maze: ", True, False, False)
            PlaymazeSubroutine(Maze, showpath)
        ElseIf input = "Clear the maze and return to the menu" Then
            Console.Clear()
        ElseIf input = "Save the maze as points" Then
            SaveMazeTextFile(Maze, $"Algorithm used to generate this maze: {Algorithm}")
        ElseIf input = "Save the maze as a png image" Then
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.White
            Dim filename As String = GetValidFileName()
            SaveMazePng(Maze, Algorithm, filename)
        ElseIf input = "s" Then
            Sd(Maze)
        ElseIf input = "Solve using the dead end filling method" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            DeadEndFiller(Maze, showpath, True, solvingdelay, False)
        ElseIf input = "Solve using the left-hand rule" Then
            Console.SetCursorPosition(0, YposAfterMaze + 2)
            solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            WallFollowerAlgorithm(Maze, solvingdelay, "LHR")
        ElseIf input = "Solve using the right-hand rule" Then
            Console.SetCursorPosition(0, YposAfterMaze + 2)
            solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            WallFollowerAlgorithm(Maze,solvingdelay,"")
        ElseIf input = "Solve using the Lee Algorithm (Wave Propagation)" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Lee(Maze, showpath, solvingdelay)
        Elseif input = "Make the maze unicursal"
            dim mazecopy as List(Of Node) = (From node In Maze Select New Node(node.X, node.y)).ToList()
            dim uniMaze as list(of node) = unicursal(mazecopy)
            if IsNothing(uniMaze)
                MsgColour("The maze that you tried to make unicursal is too big, please try a smaller maze",ConsoleColor.Red)
                Console.readkey
                Else     
                    Dim greatestX As Integer = (From node In uniMaze Select node.X).Concat(new Integer() {greatestX}).Max()
                    dim greatestY as Integer = (from node in uniMaze Select node.Y).Concat(new Integer() {greatesty}).Max()
                    Dim temparr() As String = {"Solve using the A* algorithm",
                                    "Solve using Dijkstra's algorithm",
                                    "Solve using Breadth-first search",
                                    "Solve using Depth-first search (using iteration)",
                                    "Solve using Depth-first search (using recursion)",
                                    "Solve using a recursive algorithm",
                                    "Solve using the Lee Algorithm (Wave Propagation)",
                                     "Solve using the dead end filling method",
                                    "Solve using the left-hand rule",
                                    "Solve using the right-hand rule",
                                    "",
                                    "Play the maze",
                                    "",
                                    "Get the average corridor length",
                                    "Get the amount of corners in the maze","Get the amount of junctions in the maze",
                                   "Get the amount of Dead-ends in the maze",
                                   "",
                                   "Save the maze as points",
                                   "Save the maze as a png image",
                                   "Save the maze as an ascii text file",
                                   "",
                                   "Clear the maze and return to the menu"}
                    input = SolvingMenu(temparr, "What would you like to do with the maze", greatestX + 5, 3)
                    SolvingInput(input, true, greatestY+2, solvingdelay, unimaze, "")
            End If
        ElseIf input = "Braid (remove dead ends)" Or input = "Partial braid (remove some dead ends)" Then
            If input = "Braid (remove dead ends)" Then
                EliminateDeadEnds(Maze)
            ElseIf input = "Partial braid (remove some dead ends)" Then
                PartialBraid(Maze)
            End If
            Dim greatestX As Integer = (From node In Maze Select node.X).Concat(new Integer() {greatestX}).Max()
            Dim temparr() As String = {"Solve using the A* algorithm",
                "Solve using Dijkstra's algorithm",
                "Solve using Breadth-first search",
                "Solve using Depth-first search (using iteration)",
                "Solve using Depth-first search (using recursion)",
                "Solve using a recursive algorithm",
                "Solve using the Lee Algorithm (Wave Propagation)",
                 "Solve using the dead end filling method",
                "Solve using the left-hand rule",
                "Solve using the right-hand rule",
                "",
                "Play the maze","Make the maze unicursal",
                "",
                "Get the average corridor length",
                "Get the amount of corners in the maze","Get the amount of junctions in the maze",
               "Get the amount of Dead-ends in the maze",
               "",
               "Save the maze as points",
               "Save the maze as a png image",
               "Save the maze as an ascii text file",
               "",
               "Clear the maze and return to the menu"}
            input = SolvingMenu(temparr, "What would you like to do with the maze", greatestX + 3, 3)
            SolvingInput(input, showpath, YposAfterMaze, solvingdelay, Maze, "")
        ElseIf input = "Make the maze sparse (remove some passages)" Then
            Sparsify(Maze)
            Dim greatestX As Integer
            For Each node In Maze
                If greatestX < node.X Then greatestX = node.X
            Next
            Dim temparr() As String = {"Solve using the A* algorithm",
            "Solve using Dijkstra's algorithm",
            "Solve using Breadth-first search",
            "Solve using Depth-first search (using iteration)",
            "Solve using Depth-first search (using recursion)",
            "Solve using a recursive algorithm",
            "Solve using the Lee Algorithm (Wave Propagation)",
            "Solve using the dead end filling method",
            "Solve using the left-hand rule",
            "Solve using the right-hand rule",
            "",
            "Play the maze",
            "Braid (remove dead ends)", "Partial braid (remove some dead ends)", "Make the maze sparse (remove some passages)",
            "",
            "Get the average corridor length",
            "Get the amount of corners in the maze",
            "Get the amount of junctions in the maze",
            "Get the amount of Dead-ends in the maze",
            "",
            "Save the maze as points",
            "Save the maze as a png image",
            "Save the maze as an ascii text file",
            "",
            "Clear the maze and return to the menu"}
            input = SolvingMenu(temparr, "What would you like to do with the maze", greatestX + 3, 3)
            SolvingInput(input, showpath, YposAfterMaze, solvingdelay, Maze, "")
        ElseIf input = "Get the amount of Dead-ends in the maze" Then
                Console.SetCursorPosition(0, Console.WindowHeight - 1)
                SetBoth(ConsoleColor.Black)
                Console.ForegroundColor = ConsoleColor.White
                Dim deCount As Integer = GetDeadEndCount(Maze)
                Console.Write($"Number of dead-ends: {deCount}     Percentage of the maze: {Math.Ceiling((deCount / Maze.Count) * 100)}%")
                Console.ReadKey()
            ElseIf input = "Get the amount of junctions in the maze" Then
                Console.SetCursorPosition(0, Console.WindowHeight - 1)
                SetBoth(ConsoleColor.Black)
                Console.ForegroundColor = ConsoleColor.White
                Dim jCount As Integer = GetJunctionCount(Maze)
                Console.Write($"Number of junctions: {jCount}       Percentage of the maze: {Math.Ceiling((jCount / Maze.Count) * 100)}%")
                Console.ReadKey()
            ElseIf input = "Get the amount of corners in the maze" Then
                Console.SetCursorPosition(0, Console.WindowHeight - 1)
                SetBoth(ConsoleColor.Black)
                Console.ForegroundColor = ConsoleColor.White
                Dim cCount As Integer = GetCornerCount(Maze)
                Console.Write($"Number of corners: {cCount}     Percentage of the maze: {Math.Ceiling((cCount / Maze.Count) * 100)}%")
                Console.ReadKey()
            ElseIf input = "Get the average corridor length" Then
                Console.SetCursorPosition(0, Console.WindowHeight - 1)
                SetBoth(ConsoleColor.Black)
                Console.ForegroundColor = ConsoleColor.White
                Console.Write($"Average corridor length: {Math.Ceiling(StraightWays(Maze))}")
                Console.ReadKey()
            ElseIf input = "Save the maze as an ascii text file" Then
                SaveMazeAscii(Maze)
            ElseIf input = "" Then
                Console.Clear()
            Console.WriteLine("A critical error has occured that has caused the program to no longer work")
            End
        End If
    End Sub
    Sub GetMazeInfo(ByRef Width As Integer, ByRef Height As Integer, ByRef DelayMS As Integer, ByRef Limits() As Integer, ByRef ShowGeneration As Boolean, Clear As Boolean, y As Integer)
        Console.SetCursorPosition(0, y)
        ShowGeneration = HorizontalYesNo(Console.CursorTop, "Do you want to see the maze being generated: ", False, If(Clear, True, False), False)
        Console.SetCursorPosition(0, Console.CursorTop + 1)
        If ShowGeneration Then
            DelayMS = GetIntInputArrowKeys("Delay when making the Maze (MS): ", 100, 0, False)
        Else
            DelayMS = 0
        End If
        Width = GetIntInputArrowKeys($"Width of the Maze: ", (Console.WindowWidth - 58) / 2, 20, False) * 2
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