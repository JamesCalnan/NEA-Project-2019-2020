Imports System.Drawing
Imports System.IO
Imports NEA_2019

Module Module1

    'TODO: give user the option to print a path or carve through walls, implement wall follower algorithm
    Sub Main()
        Console.CursorVisible = False
        Console.ForegroundColor = (ConsoleColor.White)

        'Dim newtree As New Tree(New Value(20, New Node(5, 5)))
        'For i = 0 To 5
        '    newtree.AddRecursive(newtree, New Value(i, New Node(5, i)))
        '    Console.WriteLine($"i: {i}      node: (5, {i})")
        'Next
        Console.SetWindowSize(Console.LargestWindowWidth - 6, Console.LargestWindowHeight - 3)
        Dim MenuOptions() As String = {"Recursive Backtracker Algorithm (using iteration)", "Recursive Backtracker Algorithm (using recursion)", "Hunt and Kill Algorithm", "Prim's Algorithm (simplified)", "Prim's Algorithm (true)", "Aldous-Broder Algorithm", "Growing Tree Algorithm", "Sidewinder Algorithm", "Binary Tree Algorithm", "Wilson's Algorithm", "Eller's Algorithm", "Kruskal's Algorithm", "Houston's Algorithm", "Spiral Backtracker Algorithm", "Custom Algorithm", "", "Load the previously generated maze", "Save the previously generated maze", "Output the previous maze as a png image", "Load a saved maze", "", "Exit"}
        Menu(MenuOptions)

        'Dim bmp As New Bitmap(350, 350)
        'Dim g As Graphics
        'g = Graphics.FromImage(bmp)
        'g.FillRectangle(Brushes.Aqua, 0, 0, 250, 250)
        'g.Dispose()
        'bmp.Save("name", System.Drawing.Imaging.ImageFormat.Png)
        'bmp.Dispose()
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
        If Width Mod 2 = 0 Then Width += 1
        Height = GetIntInputArrowKeys($"Height of the Maze: ", Console.WindowHeight - 5, 20, False)
        If Height Mod 2 = 0 Then Height += 1
        Limits = {5, 3, Width, Height}
        Console.Clear()
    End Sub
    Sub MsgColour(ByVal Msg As String, ByVal Colour As ConsoleColor)
        Console.ForegroundColor = (Colour)
        Console.WriteLine(Msg)
        Console.ForegroundColor = (ConsoleColor.White)
    End Sub
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
            Console.BackgroundColor = (ConsoleColor.Black)
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
            Console.ForegroundColor = (ConsoleColor.White)
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
        Console.BackgroundColor = (ConsoleColor.Black)
        yposaftermaze = limits(3)
        DisplayAvailablePositions(availablepath.Count)
        Console.SetCursorPosition(0, yposaftermaze + 3)
        Dim temparr() As String = {"Solve using the A* algorithm", "Solve using Dijkstra's algorithm", "Solve using Breadth-first search", "Solve using Depth-first search (using iteration)", "Solve using Depth-first search (using recursion)", "Solve using the dead end filling method", "Play the maze", "Save the maze", "Output the maze as a png image", "Clear the maze and return to the menu"}
        input = SolvingMenu(temparr, "What would you like to do with the maze", limits(2) + 2, 3)
        previousmaze.Clear()
        previousmaze = availablepath
    End Sub
    Sub SolvingInput(ByVal input As String, ByVal showpath As Boolean, ByVal YposAfterMaze As Integer, ByVal solvingdelay As Integer, ByVal availablepath As List(Of Node), ByVal Algorithm As String)
        If input = "Solve using the A* algorithm" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            If HorizontalYesNo(YposAfterMaze + 2, "Do you want to use the optimised version of A*: ", True, False, False) Then
                aStar(availablepath, showpath, True, solvingdelay)
            Else
                Dim neededNodes As List(Of Node) = GetNeededNodes(availablepath)
                Dim AdjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, availablepath)
                aStarWiki(AdjacencyList, showpath, True, solvingdelay)
            End If
        ElseIf input = "Solve using Dijkstra's algorithm" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Dim neededNodes As List(Of Node) = GetNeededNodes(availablepath)
            Dim AdjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, availablepath)
            Dijkstras(AdjacencyList, showpath, solvingdelay)
        ElseIf input = "Solve using Breadth-first search" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            BFS(availablepath, showpath, True, solvingdelay)
        ElseIf input = "Solve using Depth-first search (using iteration)" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Dim neededNodes As List(Of Node) = GetNeededNodes(availablepath)
            Dim AdjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, availablepath)
            DFS_Iterative(AdjacencyList, showpath, True, solvingdelay)
        ElseIf input = "Solve using Depth-first search (using recursion)" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Dim start_v As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
            Dim goal As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
            Dim discovered As New Dictionary(Of Node, Boolean)
            For Each node In availablepath
                discovered(node) = False
            Next
            Dim cameFrom As New Dictionary(Of Node, Node)
            Dim timer As Stopwatch = Stopwatch.StartNew
            Console.ForegroundColor = ConsoleColor.Red
            Console.BackgroundColor = ConsoleColor.Red
            DFS_Recursive(availablepath, start_v, discovered, cameFrom, goal, showpath, solvingdelay, False)
            ReconstructPath(cameFrom, goal, start_v, $"Time Taken to solve: {timer.Elapsed.TotalSeconds} seconds")
        ElseIf input = "Play the maze" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps you have taken in the maze: ", True, False, False)
            Playmaze(availablepath, showpath)
        ElseIf input = "Clear the maze and return to the menu" Then
            Console.Clear()
        ElseIf input = "Save the maze" Then
            SaveMaze(availablepath, $"Algorithm used to generate this maze: {Algorithm}")
        ElseIf input = "Output the maze as a png image" Then
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.White
            Console.Write("File Name of the maze to load (don't include .png): ")
            Dim filename As String = Console.ReadLine
            Console.Clear()
            SaveMazePNG(availablepath, Algorithm, filename)
        ElseIf input = "s" Then
            SD(availablepath)
        ElseIf input = "Solve using the dead end filling method" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            DeadEndFiller(availablepath, showpath, True, solvingdelay)
        ElseIf input = "Solve using the wall follower method" Then
            WallFollower(availablepath, showpath, solvingdelay)
        ElseIf input = "" Then
            Console.Clear()
            Console.WriteLine("A critical error has occured that has caused the program to no longer work")
            End
        End If
    End Sub
    Sub SetBoth(ByVal colour As ConsoleColor)
        Console.ForegroundColor = colour
        Console.BackgroundColor = colour
    End Sub
    Sub Solving(ByVal AvailablePath As List(Of Node), ByVal Limits() As Integer, ByRef PreviousMaze As List(Of Node), ByRef Input As String, ByVal YPosAfterMaze As Integer, ByVal ShowPath As Boolean, ByVal SolvingDelay As Integer, ByRef Algorithm As String, ByRef SetPreivousAlgorithm As String)
        If AvailablePath IsNot Nothing Then
            SetPreivousAlgorithm = Algorithm
            PreSolving(Limits, AvailablePath, PreviousMaze, Input, YPosAfterMaze)
            SolvingInput(Input, ShowPath, YPosAfterMaze, SolvingDelay, AvailablePath, Algorithm)
        End If
    End Sub
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
            Dim Info() As String = {"Information for using this program:", "Use the up and down arrow keys for navigating the menus", "Use the enter or right arrow key to select an option", "", "When inputting integer values:", "The up and down arrow keys increment by 1", "The right and left arrow keys increment by 10", "The 'M' key will set the number to the maximum value it can be", "The 'H' key will set the number to half of the maximum value it can be"}
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
                    If y = 0 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = RecursiveBacktracker(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 1 Then
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
                    ElseIf y = 2 Then
                        'Dim Optimised As Boolean = HorizontalYesNo(0, "Do you want to use the optimised version of hunt and kill: ", False, True, True)
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = HuntAndKillREFACTORED(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 3 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Prims_Simplified(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 4 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Prims_True(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 5 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = AldousBroder(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 6 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        Dim ArrOptions() As String = {"Newest (Recursive Backtracker)", "Random (Prim's simplified)", "Newest/Random, 75/25 split", "Newest/Random, 50/50 split", "Newest/Random, 25/75 split", "Oldest", "Middle", "Newest/Oldest, 50/50 split", "Oldest/Random, 50/50 split"}
                        Dim CellSelectionMethod() As Integer = PreGenMenu(ArrOptions, "What Cell selection method would you like to use: ")
                        AvailablePath = GrowingTree(Limits, DelayMS, CellSelectionMethod, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 7 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Sidewinder(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 8 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        Dim ArrOptions() As String = {"Northwest", "Northeast", "Southwest", "Southeast"}
                        Dim Bias() As Integer = PreGenMenu(ArrOptions, "Cell bias: ")
                        AvailablePath = BinaryTree(Limits, DelayMS, ShowMazeGeneration, Bias)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 9 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Wilsons(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 10 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Ellers(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 11 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Kruskals(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 12 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Houstons(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 13 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = SpiralBacktracker(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 14 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Custom(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 16 Then
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
                            Dim temparr() As String = {"Solve using the A* algorithm", "Solve using Dijkstra's algorithm", "Solve using Breadth-first search", "Solve using Depth-first search (using iteration)", "Solve using Depth-first search (using recursion)", "Solve using the dead end filling method", "Play the maze", "Save the maze", "Output the maze as a png image", "Clear the maze and return to the menu"}
                            input = SolvingMenu(temparr, "What would you like to do with the maze", GreatestX + 3, 3)
                            SolvingInput(input, ShowPath, YPosAfterMaze, SolvingDelay, PreviousMaze, PreviousAlgorithm)
                        Else
                            Console.Clear()
                            MsgColour("No previous maze available", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    ElseIf y = 17 Then
                        If PreviousMaze.Count > 1 Then
                            SaveMaze(PreviousMaze, PreviousAlgorithm)
                        Else
                            Console.Clear()
                            MsgColour("No previous maze available", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    ElseIf y = 18 Then
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
                    ElseIf y = 19 Then
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
                                Dim temparr() As String = {"Solve using the A* algorithm", "Solve using Dijkstra's algorithm", "Solve using Breadth-first search", "Solve using Depth-first search (using iteration)", "Solve using Depth-first search (using recursion)", "Solve using the dead end filling method", "Play the maze", "Clear the maze and return to the menu"}
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
    Sub SaveMaze(ByVal path As List(Of Node), ByVal Algorithm As String)
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
            writer.WriteLine($"{Algorithm}")
            For i = 0 To path.Count - 1
                writer.WriteLine(path(i).X)
                writer.WriteLine(path(i).Y)
            Next
        End Using
    End Sub
    Sub PrintStartandEnd(ByVal mazePositions As List(Of Node))
        Console.ForegroundColor = (ConsoleColor.Red)
        mazePositions(mazePositions.Count - 2).Print("██")
        Console.ForegroundColor = (ConsoleColor.Green)
        mazePositions(mazePositions.Count - 1).Print("██")
        Console.ForegroundColor = (ConsoleColor.White)
    End Sub
    Sub OptionNotReady()
        Console.Clear()
        Console.WriteLine("Option not Ready Yet")
        Console.ReadKey()
        Console.Clear()
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
    Function ExitCase()
        If Console.KeyAvailable Then
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "Escape"
                    Return True
            End Select
        End If
        Return False
    End Function
    Sub WallFollower(ByVal availablepath As List(Of Node), ByVal ShowPath As Boolean, ByVal Delay As Integer)
        Dim start_v As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim goal As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim visited, FinishedNodes As New Dictionary(Of Node, Boolean)
        Dim s As New Stack(Of Node)
        Dim u As Node = start_v
        s.Push(start_v)
        For Each node In availablepath
            visited(node) = False
        Next
        visited(start_v) = True
        Dim JunctionNodes As New List(Of Node)
        While s.Count > 0
            'can the node go anywhere?
            '   pick neighbour make it the current node
            '   push it onto the stack
            '   repeat
            'if not then pop off the stack to go back
            Dim neighbours As List(Of Node) = GetNeighbours(u, availablepath)
            'getting the neighbours
            Dim Unvisitedneighbours As Integer = 0
            For Each node In neighbours
                If Not visited(node) Then Unvisitedneighbours += 1
            Next
            'getting the amount of unvisited neighbours
            If Unvisitedneighbours > 0 Then
                'if there are unvisited neighbours then consider the neighbours
                For Each neighbourOfCurrent In neighbours
                    If visited(neighbourOfCurrent) Then Continue For
                    'if the neighbour has already been visited dont look at it
                    u = neighbourOfCurrent
                    'make the current node the neighbour
                    visited(neighbourOfCurrent) = True
                    'mark the neighbour of the current node as visited
                    s.Push(neighbourOfCurrent)
                    'push the neighbour onto the stack
                    Console.ForegroundColor = (ConsoleColor.Green)
                    u.Print("██")
                    Exit For
                Next
            Else
                'if there are no unvisited neighbours available
                u = s.Pop
                Console.ForegroundColor = ConsoleColor.White
                u.Print("██")
                If u.IsJunction(availablepath) Then JunctionNodes.Add(u)
            End If
            If u.Equals(goal) Then Exit While
            Threading.Thread.Sleep(Delay)
        End While
        Console.ForegroundColor = ConsoleColor.Green
        For Each node1 In JunctionNodes
            For Each node2 In s
                If node1.Adjacent(node2) Then
                    node1.Print("██")
                End If
            Next
        Next
        Console.ReadKey()
    End Sub
    Sub Backtrack(ByVal prev As Dictionary(Of Node, Node), ByVal target As Node, ByVal source As Node, ByVal watch As Stopwatch)
        Dim u As Node = target
        Dim Pathlength As Integer = 1
        Dim PrevNode As Node = u
        SetBoth(ConsoleColor.Green)
        Dim timetaken As String = $"Time Taken to solve: {watch.Elapsed.TotalSeconds} seconds"
        u.Print("██")
        While prev(u) IsNot Nothing
            u = prev(u)
            DrawBetween(PrevNode, u)
            PrevNode = u
            u.Print("██")
            Pathlength += 1
        End While
        PrintMessageMiddle($"Path length: {Pathlength}   {timetaken}", Console.WindowHeight - 1, ConsoleColor.Green)
        Console.ReadKey()
    End Sub
    Function ExtractMin(ByVal list As List(Of Node), ByVal dist As Dictionary(Of Node, Double))
        Dim returnnode As Node = list(0)
        For Each node In list
            If dist(node) < dist(returnnode) Then returnnode = node
        Next
        Return returnnode
    End Function
    Function GetJunctionCount(ByVal availablePath As List(Of Node))
        Dim JunctionCount As Integer = 0
        For Each node In availablePath
            If node.IsJunction(availablePath) Then
                node.Print("JU")
                JunctionCount += 1
            End If
        Next
        Return JunctionCount
    End Function
    Function GetDeadEndCount(ByVal availablePath As List(Of Node))
        Dim start As New Node(availablePath(availablePath.Count - 2).X, availablePath(availablePath.Count - 2).Y)
        Dim target As New Node(availablePath(availablePath.Count - 1).X, availablePath(availablePath.Count - 1).Y)
        Dim DeadEndCount As Integer = 0
        For Each node In availablePath
            If node.Equals(start) Or node.Equals(target) Then Continue For
            Dim neighbours As List(Of Node) = GetNeighbours(node, availablePath)
            If neighbours.Count = 1 Then
                node.Print("DE")
                DeadEndCount += 1
            End If
        Next
        Return DeadEndCount
    End Function
    Function h(ByVal node As Node, ByVal goal As Node, ByVal D As Double)
        Dim dx As Integer = Math.Abs(node.X - goal.X)
        Dim dy As Integer = Math.Abs(node.Y - goal.Y)
        Return D * (dx + dy) ^ 2
    End Function
    Function getBrushColours()
        Dim l As New List(Of Brush) From {
            Brushes.Red,
            Brushes.OrangeRed,
            Brushes.Orange,
            Brushes.Yellow,
            Brushes.YellowGreen,
            Brushes.Green,
            Brushes.SeaGreen,
            Brushes.LightSeaGreen,
            Brushes.RoyalBlue,
            Brushes.Blue,
            Brushes.BlueViolet,
            Brushes.DarkViolet,
            Brushes.Violet,
            Brushes.PaleVioletRed,
            Brushes.PaleVioletRed,
            Brushes.MediumVioletRed
        }
        Return l
    End Function
    Sub ReconstructPathFORFILE(ByVal camefrom As Dictionary(Of Node, Node), ByVal current As Node, ByVal goal As Node, ByRef bmp As Bitmap, ByRef g As Graphics, ByVal Multiplier As Integer)
        Dim totalPath As New List(Of Node) From {
            current,
            goal
        }
        While Not current.Equals(goal)
            totalPath.Add(current)
            current = camefrom(current)
        End While
        totalPath.Add(goal)
        totalPath.Reverse()
        Dim red, green, blue As Double
        red = 0
        green = 0
        blue = 255
        Dim Adding As Double = 0.5
        'Algorithm: https://codepen.io/Codepixl/pen/ogWWaK
        For Each node In totalPath
            Dim myBrush As New SolidBrush(Color.FromArgb(255, red, green, blue))
            If red > 0 And blue = 0 Then
                red -= Adding
                green += Adding
            End If
            If green > 0 And red = 0 Then
                green -= Adding
                blue += Adding
            End If
            If blue > 0 And green = 0 Then
                red += Adding
                blue -= Adding
            End If
            g.FillRectangle(myBrush, (node.X) * Multiplier, (node.Y * 2) * Multiplier, 2 * Multiplier, 2 * Multiplier)
        Next
    End Sub
    Function Floor(ByVal inputNum As Double)
        Return Math.Floor(inputNum)
    End Function
    Sub ReconstructPath(ByVal camefrom As Dictionary(Of Node, Node), ByVal current As Node, ByVal goal As Node, ByVal timetaken As String)
        SetBoth(ConsoleColor.Green)
        Dim PathLength As Integer = 1
        Dim PrevNode As Node = current
        current.Print("██")
        While Not current.Equals(goal)
            current = camefrom(current)
            DrawBetween(PrevNode, current)
            PrevNode = current
            current.Print("██")
            PathLength += 1
        End While
        PrintMessageMiddle($"Path length: {PathLength}", Console.WindowHeight - 1, ConsoleColor.Green)
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, Console.WindowHeight - 1)
        Console.Write($"Solving:                           Time taken: {timetaken}")
        Console.ReadKey()
    End Sub
    Sub DrawBetween(ByVal Node1 As Node, ByVal Node2 As Node)
        If Node1.X = Node2.X Then
            SetBoth(ConsoleColor.Green)
            If Node1.Y < Node2.Y Then
                For i = Node1.Y To Node2.Y
                    Console.SetCursorPosition(Node1.X, i)
                    Console.Write("XX")
                Next
            Else
                For i = Node1.Y To Node2.Y Step -1
                    Console.SetCursorPosition(Node1.X, i)
                    Console.Write("XX")
                Next
            End If
        End If
        If Node1.Y = Node2.Y Then
            If Node1.X <= Node2.X Then
                For i = Node1.X To Node2.X
                    Console.SetCursorPosition(i, Node1.Y)
                    Console.Write("XX")
                Next
            Else
                For i = Node1.X To Node2.X Step -1
                    Console.SetCursorPosition(i, Node1.Y)
                    Console.Write("XX")
                Next
            End If
        End If
    End Sub

    Sub SD(ByVal availablePath As List(Of Node))
        Console.SetCursorPosition(0, 1)
        Console.ForegroundColor = ConsoleColor.Red
        Console.BackgroundColor = ConsoleColor.Black
        Console.Write("SWASTIKA DETECTION MODE ENGAGED")
        Dim Positions As New List(Of Node)
        Dim width, minwidth, minheight, height As Integer
        minwidth = availablePath(availablePath.Count - 2).X
        minheight = availablePath(availablePath.Count - 2).Y + 1
        width = availablePath(availablePath.Count - 1).X
        height = availablePath(availablePath.Count - 1).Y - 1
        Dim numOfsFound As Integer = 0
        For _x = minwidth To width Step 2
            For _y = minheight To height
                For i = 0 To 1
                    For y = -2 To 2
                        For x = -4 To 4 Step 2
                            If i = 1 Then
                                If x = -4 And y = 1 Or x = -2 And y = 1 Then Continue For
                                If x = 2 And y = 1 Or x = 2 And y = 2 Then Continue For
                                If x = -2 And y = -1 Or x = -2 And y = -2 Then Continue For
                                If x = 2 And y = -1 Or x = 4 And y = -1 Then Continue For
                                'swastika
                            Else
                                If x = -2 And y = 2 Or x = -2 And y = 1 Then Continue For
                                If x = 2 And y = 1 Or x = 4 And y = 1 Then Continue For
                                If x = 2 And y = -1 Or x = 2 And y = -2 Then Continue For
                                If x = -2 And y = -1 Or x = -4 And y = -1 Then Continue For
                                'backwards swastika
                            End If
                            If x = 0 And y = 0 Then Continue For
                            Positions.Add(New Node(x + _x, y + _y))
                        Next
                    Next
                    Dim CorrectCount As Integer = 0
                    For Each node In Positions
                        If Not availablePath.Contains(node) Then
                            CorrectCount += 1
                        End If
                    Next
                    If CorrectCount = 16 Then
                        'there is a swastica
                        Console.ForegroundColor = ConsoleColor.Red
                        Console.BackgroundColor = ConsoleColor.Red
                        For Each node In Positions
                            node.Print("XX")
                        Next
                        Console.SetCursorPosition(_x, _y)
                        Console.Write("XX")
                        numOfsFound += 1
                    End If
                    Positions.Clear()
                Next
            Next

        Next

        For Each cell In availablePath
            For i = 0 To 1
                For y = -2 To 2
                    For x = -4 To 4 Step 2
                        If i = 1 Then
                            If x = -4 And y = 1 Or x = -2 And y = 1 Then Continue For
                            If x = 2 And y = 1 Or x = 2 And y = 2 Then Continue For
                            If x = -2 And y = -1 Or x = -2 And y = -2 Then Continue For
                            If x = 2 And y = -1 Or x = 4 And y = -1 Then Continue For
                        Else
                            If x = -2 And y = 2 Or x = -2 And y = 1 Then Continue For
                            If x = 2 And y = 1 Or x = 4 And y = 1 Then Continue For
                            If x = 2 And y = -1 Or x = 2 And y = -2 Then Continue For
                            If x = -2 And y = -1 Or x = -4 And y = -1 Then Continue For
                        End If
                        If x = 0 And y = 0 Then Continue For
                        Positions.Add(New Node(x + cell.X, y + cell.Y))
                    Next
                Next
                Dim CorrectCount As Integer = 0
                For Each node In Positions
                    If availablePath.Contains(node) Then
                        CorrectCount += 1
                    End If
                Next
                If CorrectCount = 16 Then
                    'there is a swastica
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.BackgroundColor = ConsoleColor.Red
                    For Each node In Positions
                        node.Print("XX")
                    Next
                    cell.Print("XX")
                    numOfsFound += 1
                End If
                Positions.Clear()
            Next
        Next
        Console.SetCursorPosition(0, 1)
        Console.ForegroundColor = ConsoleColor.Green
        Console.BackgroundColor = ConsoleColor.Black
        Console.Write($"---------------DONE---------------          {If(numOfsFound = 0, "No swastikas found", $"Number of Swastikas found: {numOfsFound}")}")
        Console.ReadKey()
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
        Dim current As Node = endnode
        SetBoth(ConsoleColor.Green)
        current.Print("██")
        Dim PathLength As Integer = 1
        While Not current.Equals(startnode)
            current = current.parent
            current.Print("██")
            PathLength += 1
        End While
        startnode.Print("██")
        PrintMessageMiddle($"Path length: {PathLength}   {timetaken}", Console.WindowHeight - 1, ConsoleColor.Green)
        Console.BackgroundColor = (ConsoleColor.Black)
    End Sub
    Sub PrintMessageMiddle(ByVal message As String, ByVal y As Integer, ByVal colour As ConsoleColor)
        Console.BackgroundColor = (ConsoleColor.Black)
        Console.ForegroundColor = (colour)
        Console.SetCursorPosition(Console.WindowWidth / 2 - message.Length / 2, y)
        Console.Write(message)
    End Sub
    Function PickRandomStartingCell(ByVal Limits() As Integer)
        Dim Li As New List(Of Cell)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                Li.Add(New Cell(x, y))
            Next
        Next
        Dim r As New Random
        Return Li(r.Next(0, Li.Count - 1))
    End Function
    Sub PrintMazeHorizontally(ByVal Maze As List(Of Node), ByVal GreatestX As Integer, ByVal GreatestY As Integer)
        For __x = 4 To GreatestX + 1 Step 2
            For __y = 1 To GreatestY + 1
                If Maze.Contains(New Node(__x, __y)) Then
                    Console.SetCursorPosition(__x, __y)
                    Console.Write("██")
                End If
            Next
        Next
    End Sub
    Function GetNeededNodes(ByVal Maze As List(Of Node)) As List(Of Node)
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, Console.WindowHeight - 3)
        Console.Write("Finding relevant nodes")
        Dim newlist As New List(Of Node)
        Console.SetCursorPosition(35, Console.WindowHeight - 3)
        Console.Write($"Progress:")
        Dim stopwatch As Stopwatch = Stopwatch.StartNew
        Dim I As Integer = 0
        For Each node In Maze
            If Adjacent(node, Maze) Then newlist.Add(node)
            I += 1
            Console.SetCursorPosition(45, Console.WindowHeight - 3)
            Console.Write($"{Math.Floor((I / Maze.Count) * 100)}%")
        Next
        newlist.Add(Maze(Maze.Count - 2))
        newlist.Add(Maze(Maze.Count - 1))
        Console.SetCursorPosition(35, Console.WindowHeight - 3)
        Console.Write($"Time taken: {stopwatch.Elapsed.TotalSeconds}              ")
        Return newlist
    End Function
    Function ConstructAdjacencyList(ByVal NeededNodes As List(Of Node), ByVal Maze As List(Of Node)) As Dictionary(Of Node, List(Of Node))
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, Console.WindowHeight - 2)
        Console.Write("Constructing adjacency list")
        Dim AdjacenyList As New Dictionary(Of Node, List(Of Node))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew
        Dim I As Integer = 0
        For Each Node In NeededNodes
            Dim TempNode As New Node(Node.X, Node.Y)
            Dim AdjacentNodes As New List(Of Node)
            Dim NodeToAdd3 As Node = FindAdjacentNodes(Node, Maze, NeededNodes, 0, -1)
            If Not IsNothing(NodeToAdd3) Then AdjacentNodes.Add(NodeToAdd3)
            Dim NodeToAdd2 As Node = FindAdjacentNodes(Node, Maze, NeededNodes, 2, 0)
            If Not IsNothing(NodeToAdd2) Then AdjacentNodes.Add(NodeToAdd2)
            Dim NodeToAdd1 As Node = FindAdjacentNodes(Node, Maze, NeededNodes, 0, 1)
            If Not IsNothing(NodeToAdd1) Then AdjacentNodes.Add(NodeToAdd1)
            Dim NodeToAdd As Node = FindAdjacentNodes(Node, Maze, NeededNodes, -2, 0)
            If Not IsNothing(NodeToAdd) Then AdjacentNodes.Add(NodeToAdd)
            AdjacenyList.Add(Node, AdjacentNodes)
            I += 1
            Console.SetCursorPosition(35, Console.WindowHeight - 2)
            Console.Write($"Progress: {Math.Floor((I / NeededNodes.Count) * 100)}%")
        Next
        Console.SetCursorPosition(35, Console.WindowHeight - 2)
        Console.Write($"Time taken: {(stopwatch.Elapsed.TotalSeconds)}")
        Return AdjacenyList
    End Function
    Function FindAdjacentNodes(ByVal CurrentNode As Node, ByVal Maze As List(Of Node), ByVal NeededNodes As List(Of Node), ByVal X As Integer, ByVal Y As Integer)
        Dim tempnode As New Node(CurrentNode.X, CurrentNode.Y)
        While true
            tempnode.update(tempnode.X + X, tempnode.Y + Y)
            If Maze.Contains(tempnode) Then
                If NeededNodes.Contains(tempnode) Then Return tempnode
            Else
                Return Nothing
            End If
        End While
        Return Nothing
    End Function
    Function Adjacent(ByVal CurrentNode As Node, ByVal AdjacentCells As List(Of Node))
        Dim L As New List(Of Node)
        Dim top As New Node(CurrentNode.X, CurrentNode.Y - 1)
        Dim right As New Node(CurrentNode.X + 2, CurrentNode.Y)
        Dim bottom As New Node(CurrentNode.X, CurrentNode.Y + 1)
        Dim left As New Node(CurrentNode.X - 2, CurrentNode.Y)
        If AdjacentCells.Contains(top) Then L.Add(top)
        If AdjacentCells.Contains(right) Then L.Add(right)
        If AdjacentCells.Contains(bottom) Then L.Add(bottom)
        If AdjacentCells.Contains(left) Then L.Add(left)
        If L.Count >= 3 Then Return True 'Is it a junction
        If AdjacentCells.Contains(top) And AdjacentCells.Contains(right) Then Return True 'is it a c
        If AdjacentCells.Contains(right) And AdjacentCells.Contains(bottom) Then Return True
        If AdjacentCells.Contains(bottom) And AdjacentCells.Contains(left) Then Return True
        If AdjacentCells.Contains(left) And AdjacentCells.Contains(top) Then Return True
        Return False
    End Function
    Function InitialiseVisited(ByVal Limits() As Integer)
        Dim dict As New Dictionary(Of Cell, Boolean)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                dict(New Cell(x, y)) = False
            Next
        Next
        Return dict
    End Function
    Sub EliminateDeadEnds(ByRef Maze As List(Of Node))
        SetBoth(ConsoleColor.Black)
        Dim r As New Random
        Dim start_v As New Node(Maze(Maze.Count - 2).X, Maze(Maze.Count - 2).Y)
        Dim goal As New Node(Maze(Maze.Count - 1).X, Maze(Maze.Count - 1).Y)
        Dim NodesToAdd As New List(Of Node)
        For Each Node As Node In Maze
            If Node.Equals(start_v) Or Node.Equals(goal) Then Continue For
            If Node.IsDeadEnd(Maze) Then
                Dim AvailableNodes As New List(Of Node) From {
                    New Node(Node.X, Node.Y - 2),'up
                    New Node(Node.X + 4, Node.Y),'right
                    New Node(Node.X, Node.Y + 2),'down
                    New Node(Node.X - 4, Node.Y) 'left
                }
                Dim DirectNeighbour As New Node(Node.X, Node.Y - 1)
                If Maze.Contains(DirectNeighbour) Then AvailableNodes.RemoveAt(0)
                DirectNeighbour.update(Node.X + 2, Node.Y)
                If Maze.Contains(DirectNeighbour) Then AvailableNodes.RemoveAt(1)
                DirectNeighbour.update(Node.X, Node.Y + 1)
                If Maze.Contains(DirectNeighbour) Then AvailableNodes.RemoveAt(2)
                DirectNeighbour.update(Node.X - 2, Node.Y)
                If Maze.Contains(DirectNeighbour) Then AvailableNodes.RemoveAt(3)
                Dim NodesToRemove As New List(Of Node)
                For i = 0 To AvailableNodes.Count - 1
                    If Not Maze.Contains(AvailableNodes(i)) Then NodesToRemove.Add(AvailableNodes(i))
                Next
                For Each thing In NodesToRemove
                    AvailableNodes.Remove(thing)
                Next
                Dim PositionInMaze As Node = AvailableNodes(r.Next(0, AvailableNodes.Count))
                Dim PosToBeAdded As Node = MidPoint(PositionInMaze, Node)
                NodesToAdd.Add(PosToBeAdded)
                PosToBeAdded.Print("██")
            End If
        Next
        For Each node In NodesToAdd
            Maze.Add(node)
        Next
    End Sub
    Sub SaveMazePNG(ByVal Path As List(Of Node), ByVal Algorithm As String, ByVal fileName As String)
        Dim solving As Boolean = HorizontalYesNo(0, "Do you want the outputted maze to have the solution on it  ", False, False, False)
        Console.Clear()
        Console.Write("Saving...")
        Dim Multiplier As Integer = 10
        Dim Max_X, Max_Y As Integer
        For Each node In Path
            If node.X > Max_X Then Max_X = node.X
            If node.Y > Max_Y Then Max_Y = node.Y
        Next
        Dim Width As Integer = (Max_X + 10) * Multiplier
        Dim Height As Integer = ((Max_Y + 4) * 2) * Multiplier
        Dim bmp As New Bitmap(Width, Height)
        Dim g As Graphics
        g = Graphics.FromImage(bmp)
        g.FillRectangle(Brushes.Black, 0, 0, Width, Height)
        For Each thing In Path
            g.FillRectangle(Brushes.White, (thing.X) * Multiplier, (thing.Y * 2) * Multiplier, 2 * Multiplier, 2 * Multiplier)
        Next
        If solving Then
            Dim myBrush As New SolidBrush(Color.FromArgb(255, 0, 0, 255))
            DFS_IterativeFORFILE(Path, bmp, g, Multiplier)
            g.FillRectangle(myBrush, (Path(Path.Count - 2).X) * Multiplier, (Path(Path.Count - 2).Y * 2) * Multiplier, 2 * Multiplier, 2 * Multiplier)
        End If
        'g.FillRectangle(Brushes.Lime, (Path(Path.Count - 1).X) * Multiplier, (Path(Path.Count - 1).Y * 2) * Multiplier, 2 * Multiplier, 2 * Multiplier)
        Dim f As New Font("Roboto", Width / 60)
        Dim point As New PointF(((Width) / 2) - (Algorithm.Length / 2) * Multiplier, 1)
        'g.DrawString(Algorithm, f, Brushes.White, point)
        g.Dispose()
        bmp.Save($"{fileName}.png", System.Drawing.Imaging.ImageFormat.Png)
        bmp.Dispose()
    End Sub
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
        If showmazegeneation Then currentcell.Print("██")
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
        Return Nothing
    End Function
    Function GetDirection(ByVal cell1 As Cell, ByVal cell2 As Cell, ByRef newdir As Dictionary(Of Cell, String), ByVal showmazegeneration As Boolean)
        Dim tempcell As New Cell(cell2.X, cell2.Y - 2)
        Console.BackgroundColor = (ConsoleColor.Black)
        Console.ForegroundColor = (ConsoleColor.Red)
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
    Sub EraseLineHaK(ByVal limits() As Integer, ByVal xCount As Integer, ByVal VisitedlistAndWall As List(Of Node), ByVal y As Integer)
        For i = limits(0) + 3 To xCount + 2 Step 2
            Dim tempcell As New Node(i, y)
            If Not VisitedlistAndWall.Contains(tempcell) Then
                SetBoth(ConsoleColor.Black)
                tempcell.Print("  ")
            Else
                SetBoth(ConsoleColor.White)
                tempcell.Print("██")
            End If
        Next
    End Sub
    Sub AddStartAndEndPNG(ByRef ReturnablePath As List(Of Node), ByVal VisitedList As List(Of Cell), ByVal Limits() As Integer, ByVal EvenWidth As Integer)
        ReturnablePath.Add(New Node(Limits(0) + 3, Limits(1) - 1))
        Console.ForegroundColor = ConsoleColor.Red
        Console.BackgroundColor = ConsoleColor.Red
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Dim testnode As New Node(Limits(2) + 5, Limits(3))
        Do
            testnode.update(testnode.X - 1, testnode.Y)
        Loop Until ReturnablePath.Contains(testnode)
        ReturnablePath.Add(New Node(testnode.X, testnode.Y + 1))
        Console.ForegroundColor = ConsoleColor.Green
        Console.BackgroundColor = ConsoleColor.Green
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Console.BackgroundColor = (ConsoleColor.Black)
    End Sub
    Sub AddStartAndEnd(ByRef ReturnablePath As List(Of Node), ByVal Limits() As Integer, ByVal EvenWidth As Integer)
        ReturnablePath.Add(New Node(Limits(0) + 3, Limits(1) - 1))
        Console.ForegroundColor = ConsoleColor.Red
        Console.BackgroundColor = ConsoleColor.Red
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Dim testnode As New Node(Limits(2) + 6, Limits(3))
        Do
            testnode.update(testnode.X - 1, testnode.Y)
        Loop Until ReturnablePath.Contains(testnode)
        ReturnablePath.Add(New Node(testnode.X, testnode.Y + 1))
        Console.ForegroundColor = ConsoleColor.Green
        Console.BackgroundColor = ConsoleColor.Green
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Console.BackgroundColor = (ConsoleColor.Black)
    End Sub
    Function AdjacentCheck(ByVal cell As Cell, ByVal visitedcells As Dictionary(Of Cell, Boolean))
        Dim Adjancent() As Integer = {0, 0, 0, 0}
        Dim Neighbours As New List(Of Cell)
        Dim tempcell As New Cell(cell.X, cell.Y - 2)
        If If(visitedcells.ContainsKey(tempcell), visitedcells(tempcell), False) And Not visitedcells(cell) Then Adjancent(0) = 1
        tempcell.Update(cell.X + 4, cell.Y)
        If If(visitedcells.ContainsKey(tempcell), visitedcells(tempcell), False) And Not visitedcells(cell) Then Adjancent(1) = 1
        tempcell.Update(cell.X, cell.Y + 2)
        If If(visitedcells.ContainsKey(tempcell), visitedcells(tempcell), False) And Not visitedcells(cell) Then Adjancent(2) = 1
        tempcell.Update(cell.X - 4, cell.Y)
        If If(visitedcells.ContainsKey(tempcell), visitedcells(tempcell), False) And Not visitedcells(cell) Then Adjancent(3) = 1
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
    Function MidPoint(ByVal cell1 As Object, ByVal cell2 As Object)
        If cell1.GetType.ToString = "NEA_2019.Cell" Then
            Return New Cell((cell1.X + cell2.X) / 2, (cell1.Y + cell2.Y) / 2)
        Else
            Return New Node((cell1.X + cell2.X) / 2, (cell1.Y + cell2.Y) / 2)
        End If
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
    Function Neighbour(ByVal current As Cell, ByVal visited As Dictionary(Of Cell, Boolean), ByVal Limits() As Integer, ByVal bool As Boolean)
        Dim neighbours As New List(Of Cell)
        Dim newPoint As New Cell(current.X - 4, current.Y)
        If newPoint.WithinLimits(Limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y - 2)
        If newPoint.WithinLimits(Limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y + 2)
        If newPoint.WithinLimits(Limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X + 4, current.Y)
        If newPoint.WithinLimits(Limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        If bool Then
            Return neighbours
        Else
            If neighbours.Count > 0 Then Return True
        End If
        Return False
    End Function
    Function GetNeighboursAd(ByRef current As Node, ByRef adjacencyList As Dictionary(Of Node, List(Of Node)))
        Return adjacencyList(current)
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
    Function WithinLimits(ByVal limits() As Integer)
        If Me.X >= limits(0) And Me.X <= limits(2) And Me.Y >= limits(1) And Me.Y <= limits(3) Then Return True
        Return False
    End Function
    Public Sub update(ByVal xpoint As Integer, ByVal ypoint As Integer)
        X = xpoint
        Y = ypoint
    End Sub
    Function IsDeadEnd(ByVal availablePath As List(Of Node))
        Dim curNode As New Node(Me.X, Me.Y)
        Dim Neighbours As List(Of Node) = GetNeighbours(curNode, availablePath)
        If Neighbours.Count = 1 Then Return True
        Return False
    End Function
    Function IsJunction(ByVal availablePath As List(Of Node))
        Dim curNode As New Node(Me.X, Me.Y)
        Dim Neighbours As List(Of Node) = GetNeighbours(curNode, availablePath)
        If Neighbours.Count >= 3 Then Return True
        Return False
    End Function
    Function Adjacent(ByVal checknode As Node)
        Dim curNode As New Node(Me.X, Me.Y)
        curNode.update(Me.X, Me.Y - 1)
        If curNode.Equals(checknode) Then Return True
        curNode.update(Me.X + 2, Me.Y)
        If curNode.Equals(checknode) Then Return True
        curNode.update(Me.X, Me.Y + 1)
        If curNode.Equals(checknode) Then Return True
        curNode.update(Me.X - 2, Me.Y)
        If curNode.Equals(checknode) Then Return True
        Return False
    End Function
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

Class Value
    Public IntValue As Integer
    Public Node As Node
    Public Sub New(ByVal _intvalue As Integer, ByVal _node As Node)
        IntValue = _intvalue
        Node = _node
    End Sub
End Class

Class Tree
    Public value As Value
    Public left, right As Tree
    Public Function Remove(ByVal current As Tree, ByVal value As Value)
        If IsNothing(current) Then Return False
        If value.IntValue = current.value.IntValue Then
            current = Nothing
            Return True
        End If
        Return value.IntValue < current.value.IntValue
        Remove(current.left, value)
        Remove(current.right, value)
    End Function
    Public Function AddRecursive(ByVal current As Tree, ByVal value As Value)
        If IsNothing(current) Then Return New Tree(value)
        If value.IntValue < current.value.IntValue Then
            current.left = AddRecursive(current.left, value)
        ElseIf current.value.IntValue < value.IntValue Then
            current.right = AddRecursive(current.right, value)
        Else
            Return current
        End If
        Return current
    End Function
    Function ExtractMin(ByVal node As Tree)
        Dim current As Tree = node
        While Not IsNothing(current.left)
            current = current.left
        End While
        Dim ReturnNode As Node = current.value.Node
        Return ReturnNode
    End Function
    Public Sub New(ByVal valu As Value)
        value = valu
        left = Nothing
        right = Nothing
    End Sub
End Class