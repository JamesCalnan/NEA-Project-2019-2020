Imports System.IO
Imports System.Drawing
Module Menus
    Sub Menu(arr() As String, topitem As String)
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
                                   "Play the maze", "Braid (remove dead ends)", "Partial braid (remove some dead ends)", "Make the maze sparse (remove some passages)", "Make the maze unicursal",
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
        Dim allColours() As String = GetAllConsoleColours()
        Dim pathColour = ConsoleColor.white
        dim backGroundColour = ConsoleColor.black
        dim solvingColour = ConsoleColor.Red
        Dim input = ""
        Dim previousAlgorithm = ""
        Dim previousMaze, loadedMaze As New List(Of Node)
        Dim width, height, delayMs, solvingDelay, yPosAfterMaze, y, lastMazeGenItem As Integer
        for i = 0 to arr.count-1
            if arr(i) = ""
                lastMazeGenItem = i
                Exit For
            End If
        Next
        y = 1
        Dim limits(3) As Integer
        Dim screenWidth As Integer = Console.WindowWidth / 2
        Dim showMazeGeneration, showPath As Boolean
        Console.Clear()
        Dim currentCol As Integer = Console.CursorTop
        Console.ResetColor()
        MsgColour($"{topitem}: ", ConsoleColor.White)
        For i = 0 To arr.Count - 1
            If arr(i) = arr(y) Then
                MsgColour($"> {arr(1)}  ", ConsoleColor.Green)
            Else
                if arr(i) =  "Change the path colour           current colour: " Then 
                    Console.WriteLine($" {arr(i)}{pathColour.ToString()}")
                elseif arr(i) = "Change the background colour     current colour: "
                    Console.WriteLine($" {arr(i)}{backGroundColour.ToString()}")
                elseif arr(i) = "Change the solving colour        current colour: "
                    Console.WriteLine($" {arr(i)}{solvingColour.ToString()}")
                Else 
                    Console.WriteLine($" {arr(i)}")
                End If
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
                    if y <= lastMazeGenItem Then
                        GetMazeInfo(width, height, delayMs, limits, showMazeGeneration, True, 0, If(arr(y) = "   Make your own maze", True, False))
                        If arr(y) = "   Recursive Backtracker Algorithm (using iteration)" Then
                            availablePath = RecursiveBacktracker.RecursiveBacktracker(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Recursive Backtracker Algorithm (using recursion)" Then
                            Dim r As New Random
                            If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
                            Dim currentCell As Cell = PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
                            Dim prev As Cell = currentCell '(Limits(0) + 3, Limits(1) + 2)
                            Dim v As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
                            v(currentCell) = True
                            Dim path As New List(Of Node)
                            Dim stopwatch As Stopwatch = Stopwatch.StartNew()
                            SetBoth(pathColour)
                            path.Add(New Node(currentCell.X, currentCell.Y))
                            If showMazeGeneration Then currentCell.Print("██")
                            path = RecursiveBacktrackerRecursively(currentCell, limits, path, v, prev, r, showMazeGeneration, delayMs, pathColour)
                            PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
                            SetBoth(pathcolour)
                            If Not showMazeGeneration Then PrintMazeHorizontally(path, limits(2), limits(3))
                            AddStartAndEnd(path, limits, pathcolour)
                            availablePath = path
                        ElseIf arr(y) = "   Hunt and Kill Algorithm" Then
                            availablePath = HuntAndKillRefactored(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Prim's Algorithm (simplified)" Then
                            availablePath = Prims_Simplified(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Prim's Algorithm (true)" Then
                            availablePath = Prims_True(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Aldous-Broder Algorithm" Then
                            availablePath = AldousBroder.AldousBroder(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Growing Tree Algorithm" Then
                            Dim arrOptions() As String = {"Newest (Recursive Backtracker)", "Random (Prim's simplified)", "Newest/Random, 75/25 split", "Newest/Random, 50/50 split", "Newest/Random, 25/75 split", "Oldest", "Middle", "Newest/Oldest, 50/50 split", "Oldest/Random, 50/50 split"}
                            Dim cellSelectionMethod() As Integer = PreGenMenu(arrOptions, "What Cell selection method would you like to use: ")
                            availablePath = GrowingTree(limits, delayMs, cellSelectionMethod, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Sidewinder Algorithm" Then
                            availablePath = Sidewinder(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Binary Tree Algorithm" Then
                            Dim arrOptions() As String = {"Northwest", "Northeast", "Southwest", "Southeast"}
                            Dim bias() As Integer = PreGenMenu(arrOptions, "Cell bias: ")
                            availablePath = BinaryTree(limits, delayMs, showMazeGeneration, bias, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Wilson's Algorithm" Then
                            availablePath = Wilsons(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Eller's Algorithm" Then
                            availablePath = Ellers(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Kruskal's Algorithm (simplified)" Then
                            availablePath = Kruskals(limits, delayMs, showMazeGeneration, pathColour, backGroundColour, "simplified")
                        ElseIf arr(y) = "   Kruskal's Algorithm (true)" Then
                            availablePath = Kruskals(limits, delayMs, showMazeGeneration, pathColour, backGroundColour, "true")
                        ElseIf arr(y) = "   Houston's Algorithm" Then
                            availablePath = Houstons(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Spiral Backtracker Algorithm" Then
                            availablePath = SpiralBacktracker(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Custom Algorithm" Then
                            availablePath = Custom(limits, delayMs, showMazeGeneration, pathColour, backGroundColour)
                        ElseIf arr(y) = "   Make your own maze" Then
                            availablePath = UserCreateMaze.UserCreateMaze(limits, pathColour, backGroundColour)
                        End If
                        Solving(availablePath, limits, previousMaze, input, yPosAfterMaze, showPath, solvingDelay, arr(y), previousAlgorithm,temparr,pathColour,backGroundColour,solvingColour)
                    Else
                        If arr(y) = "Load the previously generated maze" Then
                            PrintPreviousMaze(previousMaze, previousAlgorithm, showPath, yPosAfterMaze, solvingDelay, temparr, pathColour, backGroundColour, solvingColour)
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
                            SaveMazePng(previousMaze, $"Algorithm used to generate this maze: {previousAlgorithm}", filename,pathColour,backGroundColour)
                        Else
                            Console.Clear()
                            MsgColour("No previous maze available", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    elseif arr(y) = "Change the path colour           current colour: " Then
                            pathColour = ColourChange(allColours)
                        ElseIf arr(y) = "Change the background colour     current colour: "Then
                            backGroundColour = ColourChange(allColours)
                        ElseIf arr(y) = "Change the solving colour        current colour: "Then
                            solvingColour = ColourChange(allColours)
                        ElseIf arr(y) = "Load a maze from a text file (list of points)" Then
                        LoadMazeTextFile(loadedMaze,yPosAfterMaze,previousMaze,temparr,showPath,solvingDelay,pathColour,backGroundColour,solvingColour)
                    ElseIf arr(y) = "Load a maze from an image file" Then
                        Dim tempMaze As List(Of Node) = LoadMazePng(temparr, pathColour,backGroundColour,solvingColour)
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
                        Dim tempMaze As List(Of Node) = LoadMazeAscii(temparr,pathColour,backGroundColour,solvingColour)
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
                        ElseIf arr(y) = "   Kruskal's Algorithm (simplified)" Then
                            KruskalsInfo()
                        elseif arr(y) = "   Kruskal's Algorithm (true)"
                            
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
                if MenuOption = "Change the path colour           current colour: "
                    Console.Write($" {MenuOption}{pathColour.ToString()}    ")
                elseif MenuOption = "Change the background colour     current colour: "
                    Console.Write($" {MenuOption}{backgroundcolour.ToString()}    ")
                elseif MenuOption = "Change the solving colour        current colour: "
                    Console.WriteLine($" {MenuOption}{solvingColour.ToString()}      ")
                Else 
                    Console.Write($" {MenuOption}    ")
                End If
                Count += 1
            Next
            Console.SetCursorPosition(0, y + 1)
            if arr(y) =  "Change the path colour           current colour: "
                MsgColour($"> {arr(y)}{pathColour.ToString()}  ", ConsoleColor.Green)
            elseif arr(y) = "Change the background colour     current colour: "
                MsgColour($"> {arr(y)}{backGroundColour.ToString()}  ", ConsoleColor.Green)
            elseif arr(y) = "Change the solving colour        current colour: "
                MsgColour($"> {arr(y)}{solvingcolour.ToString()}  ", ConsoleColor.Green)
            Else 
                 MsgColour($"> {arr(y)}  ", ConsoleColor.Green)
            End If
        End While
    End Sub
    Sub Solving(availablePath As List(Of Node), Limits() As Integer, ByRef previousMaze As List(Of Node), ByRef Input As String, yPosAfterMaze As Integer, showPath As Boolean, solvingDelay As Integer, ByRef algorithm As String, ByRef setPreivousAlgorithm As String,tempArr() as String,pathColour as ConsoleColor,backGroundColour as consolecolor,solvingColour as ConsoleColor)
        If availablePath IsNot Nothing Then
            setPreivousAlgorithm = algorithm
            previousMaze.Clear()
            previousMaze.AddRange(availablePath)
            PreSolving(Limits, availablePath, previousMaze, Input, yPosAfterMaze,tempArr)
            SolvingInput(Input, showPath, yPosAfterMaze, solvingDelay, availablePath, algorithm,pathColour,backGroundColour,solvingColour)
        End If
    End Sub
    Sub PreSolving(limits() As Integer, availablePath As List(Of Node), ByRef previousMaze As List(Of Node), ByRef input As String, ByRef yPosAfterMaze As Integer,tempArr() as String)
        Console.BackgroundColor = (ConsoleColor.Black)
        yposaftermaze = limits(3) + 1
        DisplayAvailablePositions(availablepath.Count)
        Console.SetCursorPosition(0, yposaftermaze + 3)
        input = SolvingMenu(temparr, "What would you like to do with the maze", limits(2) + 4, 3)
    End Sub
    Sub ClearHorizontal(y As Integer, ClearMessage As Boolean, setafter As Boolean)
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, y)
        If ClearMessage Then Console.Write("                                                                                                               ")
        Console.SetCursorPosition(0, y + If(setafter, 1, 0))
    End Sub
    Function HorizontalYesNo(ColumnPosition As Integer, message As String, ClearMessage As Boolean, ClearBefore As Boolean, SetAfter As Boolean) As Boolean
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
    Sub SolvingInput(input As String, showpath As Boolean, YposAfterMaze As Integer, solvingdelay As Integer, Maze As List(Of Node), Algorithm As String,pathColour as ConsoleColor,backGroundColour as ConsoleColor,solvingColour as ConsoleColor)
        If input = "Solve using the A* algorithm" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            If HorizontalYesNo(YposAfterMaze + 2, "Do you want to use the optimised version of A*: ", True, False, False) Then
                AStar(Maze, showpath, True, solvingdelay, solvingColour)
            Else
                Dim neededNodes As List(Of Node) = GetNeededNodes(Maze)
                Dim adjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, Maze)
                AStarWiki(adjacencyList, showpath, True, solvingdelay, solvingColour)
            End If
        ElseIf input = "Solve using Dijkstra's algorithm" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            'Dim neededNodes As List(Of Node) = GetNeededNodes(Maze)
            'Dim AdjacencyList As Dictionary(Of Node, List(Of Node)) = ConstructAdjacencyList(neededNodes, Maze)
            Dijkstras(Maze, showpath, solvingdelay, solvingcolour)
        ElseIf input = "Solve using Breadth-first search" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Bfs(Maze, showpath, True, solvingdelay, solvingColour)
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
            SetBoth(solvingcolour)
            Dim stopwatch As Stopwatch = Stopwatch.StartNew()
            Dim b As Boolean = RecursiveSolve(Maze, visited, correctPath, Maze(Maze.Count - 2).X, Maze(Maze.Count - 2).Y, New Node(Maze(Maze.Count - 1).X, Maze(Maze.Count - 1).Y), showpath, solvingdelay)
            SetBoth(ConsoleColor.Green)
            For Each thing In correctPath
                If thing.Value Then thing.Key.Print("XX")
            Next
            Maze(Maze.Count - 1).Print("XX")
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
            DFS_Iterative(adjacencyList, showpath, True, solvingdelay, solvingcolour)
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
            setboth(solvingColour)
            DFS_Recursive(Maze, startV, discovered, cameFrom, goal, showpath, solvingdelay, False)
            ReconstructPath(cameFrom, goal, startV, $"{timer.Elapsed.TotalSeconds}")
            Console.ReadKey()
        ElseIf input = "Play the maze" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps you have taken in the maze: ", True, False, False)
            PlaymazeSubroutine(Maze, showpath,pathColour,backGroundColour)
        ElseIf input = "Clear the maze and return to the menu" Then
            Console.Clear()
        ElseIf input = "Save the maze as points" Then
            SaveMazeTextFile(Maze, $"Algorithm used to generate this maze: {Algorithm}")
        ElseIf input = "Save the maze as a png image" Then
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.White
            Dim filename As String = GetValidFileName()
            SaveMazePng(Maze, Algorithm, filename,pathColour,backGroundColour)
        ElseIf input = "s" Then
            Sd(Maze)
        ElseIf input = "Solve using the dead end filling method" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            DeadEndFiller(Maze, showpath, True, solvingdelay, pathcolour,solvingcolour)
        ElseIf input = "Solve using the left-hand rule" Then
            Console.SetCursorPosition(0, YposAfterMaze + 2)
            solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            WallFollowerAlgorithm(Maze, solvingdelay, "LHR",solvingColour)
        ElseIf input = "Solve using the right-hand rule" Then
            Console.SetCursorPosition(0, YposAfterMaze + 2)
            solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            WallFollowerAlgorithm(Maze,solvingdelay,"",solvingColour)
        ElseIf input = "Solve using the Lee Algorithm (Wave Propagation)" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            lee(Maze, showpath, solvingdelay,solvingColour)
        Elseif input = "Make the maze unicursal"
            dim mazeCopy as List(Of Node) = (From node In Maze Select New Node(node.X, node.y)).ToList()
            dim uniMaze as list(of node) = unicursal(mazecopy,pathColour,backGroundColour)
            if IsNothing(uniMaze)
                MsgColour("The maze that you tried to make unicursal is too big, please try a smaller maze",ConsoleColor.Red)
                Console.readkey
                Else     
                    maze = uniMaze
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
                     
                    SolvingInput(input, true, greatestY+2, solvingdelay, unimaze, "",pathColour,backGroundColour,solvingColour)
            End If
        ElseIf input = "Braid (remove dead ends)" Or input = "Partial braid (remove some dead ends)" Then
            If input = "Braid (remove dead ends)" Then
                EliminateDeadEnds(Maze,pathColour,backGroundColour)
            ElseIf input = "Partial braid (remove some dead ends)" Then
                PartialBraid(Maze,pathColour,backGroundColour)
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
            input = SolvingMenu(temparr, "What would you like to do with the maze", greatestX + 6, 3)
            SolvingInput(input, showpath, YposAfterMaze, solvingdelay, Maze, "",pathColour,backGroundColour,solvingColour)
        ElseIf input = "Make the maze sparse (remove some passages)" Then
            Sparsify(Maze,pathColour,backGroundColour)
            Dim greatestX As Integer
            greatestX = (From node In Maze Select node.X).Concat(new Integer() {greatestX}).Max()
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
            input = SolvingMenu(temparr, "What would you like to do with the maze", greatestX + 6, 3)
            SolvingInput(input, showpath, YposAfterMaze, solvingdelay, Maze, "",pathColour,backGroundColour,solvingColour)
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
    Sub GetMazeInfo(ByRef Width As Integer, ByRef Height As Integer, ByRef DelayMS As Integer, ByRef Limits() As Integer, ByRef ShowGeneration As Boolean, Clear As Boolean, y As Integer, Optional NeedExtraInfo As Boolean = True)
        Console.SetCursorPosition(0, y)
        If Not NeedExtraInfo Then
            ShowGeneration = HorizontalYesNo(Console.CursorTop, "Do you want to see the maze being generated: ", False, If(Clear, True, False), False)
            Console.SetCursorPosition(0, Console.CursorTop + 1)
            If ShowGeneration Then
                DelayMS = GetIntInputArrowKeys("Delay when making the Maze (MS): ", 100, 0, False)
            Else
                DelayMS = 0
            End If
        End If
        If NeedExtraInfo Then Console.Clear()
        Width = GetIntInputArrowKeys($"Width of the Maze: ", (Console.WindowWidth - 58) / 2, 20, False) * 2
        Height = GetIntInputArrowKeys($"Height of the Maze: ", Console.WindowHeight - 7, 20, False)
        If Width Mod 2 = 0 Then
            Width += 1
        End If
        If Height Mod 2 = 0 Then
            Height += 1
        End If
        Limits = {5, 3, Width, Height}
        Console.Clear()
    End Sub
End Module