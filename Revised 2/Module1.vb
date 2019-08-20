Imports System.Drawing
Imports System.IO
Module Module1

    'TODO: use new menu when solving a maze that has just been loaded from a text file
    Sub Main()
        Console.CursorVisible = False
        Console.ForegroundColor = (ConsoleColor.White)

        'Dim newtree As New Tree(New Value(20, New Node(5, 5)))
        'For i = 0 To 5
        '    newtree.AddRecursive(newtree, New Value(i, New Node(5, i)))
        '    Console.WriteLine($"i: {i}      node: (5, {i})")
        'Next







        'Dim node As Node = newtree.ExtractMin(newtree)
        'Console.WriteLine($"({node.X}, {node.Y})")
        'node = newtree.ExtractMin(newtree)
        'Console.WriteLine($"({node.X}, {node.Y})")


        'Console.ReadKey()


        Console.SetWindowSize(Console.LargestWindowWidth - 6, Console.LargestWindowHeight - 3)
        Dim MenuOptions() As String = {"Recursive Backtracker Algorithm (using iteration)", "Recursive Backtracker Algorithm (using recursion)", "Hunt and Kill Algorithm", "Prim's Algorithm (simplified)", "Prim's Algorithm (true)", "Aldous-Broder Algorithm", "Growing Tree Algorithm", "Sidewinder Algorithm", "Binary Tree Algorithm", "Wilson's Algorithm", "Eller's Algorithm", "Kruskal's Algorithm", "Custom Algorithm", "Houston's Algorithm", "Spiral Backtracker Algorithm", "", "Load the previously generated maze", "Save the previously generated maze", "Output the previous maze as a png image", "Load a saved maze", "", "Exit"}
        Menu(MenuOptions)
        'Dim bmp As New Bitmap(350, 350)
        'Dim g As Graphics
        'g = Graphics.FromImage(bmp)
        'g.FillRectangle(Brushes.Aqua, 0, 0, 250, 250)
        'g.Dispose()
        'bmp.Save("name", System.Drawing.Imaging.ImageFormat.Png)
        'bmp.Dispose()
    End Sub
    Sub Playmaze(ByVal AvailablePath As List(Of Node), ByVal ShowPath As Boolean)
        Dim playerPath As New List(Of Node)
        Dim currentPos As New Node(AvailablePath(AvailablePath.Count - 2).X, AvailablePath(AvailablePath.Count - 2).Y)
        Dim start As New Node(AvailablePath(AvailablePath.Count - 2).X, AvailablePath(AvailablePath.Count - 2).Y)
        If Not playerPath.Contains(currentPos) Then playerPath.Add(currentPos)
        Console.ForegroundColor = (ConsoleColor.Magenta)
        currentPos.Print("██")
        Dim PreviousPos As Node = currentPos
        Dim target As New Node(AvailablePath(AvailablePath.Count - 1).X, AvailablePath(AvailablePath.Count - 1).Y)
        Console.ForegroundColor = (ConsoleColor.Green)
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
                                Console.ForegroundColor = ConsoleColor.Blue
                                Console.BackgroundColor = ConsoleColor.Blue
                            Else
                                Console.ForegroundColor = ConsoleColor.White
                                Console.BackgroundColor = ConsoleColor.White
                            End If
                        Else
                            Console.ForegroundColor = ConsoleColor.White
                            Console.BackgroundColor = ConsoleColor.White
                        End If
                    Else
                        Console.ForegroundColor = ConsoleColor.White
                        Console.BackgroundColor = ConsoleColor.White
                    End If
                Else
                    If AvailablePath.Contains(t) Or AvailablePath.Contains(s) Then
                        Console.ForegroundColor = ConsoleColor.White
                        Console.BackgroundColor = ConsoleColor.White
                    Else
                        Console.ForegroundColor = ConsoleColor.Blue
                        Console.BackgroundColor = ConsoleColor.Blue
                    End If
                End If
            Else
                Console.ForegroundColor = ConsoleColor.Black
                Console.BackgroundColor = ConsoleColor.Black
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
        Console.ForegroundColor = (ConsoleColor.Yellow)
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
                Console.ForegroundColor = (ConsoleColor.Blue)
            Else
                Console.ForegroundColor = (ConsoleColor.White)
            End If
            PreviousPos.Print("██")
            Console.ForegroundColor = (ConsoleColor.Magenta)
            currentPos.Print("██")
            PreviousPos = currentPos
            If Not playerPath.Contains(currentPos) Then playerPath.Add(currentPos)
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
        Dim temparr() As String = {"Solve using the A* algorithm", "Solve using Dijkstra's algorithm", "Solve using Breadth-first search", "Solve using Depth-first search (using iteration)", "Solve using Depth-first search (using recursion)", "Play the maze", "Save the maze", "Output the maze as a png image", "Clear the maze and return to the menu"}
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
                aStarWiki(availablepath, showpath, True, solvingdelay)
            End If
        ElseIf input = "Solve using Dijkstra's algorithm" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            Dijkstras(availablepath, showpath, solvingdelay)
        ElseIf input = "Solve using Breadth-first search" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            BFS(availablepath, showpath, True, solvingdelay)
        ElseIf input = "Solve using Depth-first search (using iteration)" Then
            showpath = HorizontalYesNo(YposAfterMaze + 2, "Do you want to show the steps in solving the maze: ", True, False, False)
            If showpath Then solvingdelay = GetIntInputArrowKeys("Delay when solving the maze: ", 100, 0, True)
            DFS_Iterative(availablepath, showpath, True, solvingdelay)
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
        ElseIf input = "" Then
            Console.Clear
            Console.WriteLine("A critical error has occured that has caused the program to no longer work")
            End
        End If
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
            Dim Info() As String = {"Information for using this program:", "Use arrow keys for navigating the menus", "Use the enter key to select an option", "", "When inputting integer values:", "The right and left arrow keys increment by 1", "The up and down arrow keys increment by 10", "The 'M' key will set the number to the maximum value it can be", "The 'H' key will set the number to half of the maximum value it can be"}
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
                Case "Enter"
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
                        Console.ForegroundColor = (ConsoleColor.White)
                        Console.BackgroundColor = (ConsoleColor.White)
                        path.Add(New Node(CurrentCell.X, CurrentCell.Y))
                        If ShowMazeGeneration Then CurrentCell.Print("██")
                        path = RecursiveBacktrackerRecursively(CurrentCell, Limits, path, v, prev, r, ShowMazeGeneration, DelayMS)
                        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
                        Console.ForegroundColor = (ConsoleColor.White)
                        Console.BackgroundColor = (ConsoleColor.White)
                        If Not ShowMazeGeneration Then
                            For Each cell In path
                                cell.Print("██")
                            Next
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
                        AvailablePath = Custom(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 13 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = Houstons(Limits, DelayMS, ShowMazeGeneration)
                        Solving(AvailablePath, Limits, PreviousMaze, input, YPosAfterMaze, ShowPath, SolvingDelay, arr(y), PreviousAlgorithm)
                    ElseIf y = 14 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration, True, 0)
                        AvailablePath = SpiralBacktracker(Limits, DelayMS, ShowMazeGeneration)
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
                            Console.ForegroundColor = ConsoleColor.White
                            Console.BackgroundColor = ConsoleColor.White
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
                            Dim temparr() As String = {"Solve using the A* algorithm", "Solve using Dijkstra's algorithm", "Solve using Breadth-first search", "Solve using Depth-first search (using iteration)", "Solve using Depth-first search (using recursion)", "Play the maze", "Clear the maze and return to the menu"}
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
                                Dim temparr() As String = {"Solve using the A* algorithm", "Solve using Dijkstra's algorithm", "Solve using Breadth-first search", "Solve using Depth-first search (using iteration)", "Solve using Depth-first search (using recursion)", "Play the maze", "Clear the maze and return to the menu"}
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
            'Threading.Thread.Sleep(20)
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
    Function DFS_Recursive(ByVal availablepath As List(Of Node), ByVal v As Node, ByVal visited As Dictionary(Of Node, Boolean), ByRef cameFrom As Dictionary(Of Node, Node), ByVal goal As Node, ByVal showsolving As Boolean, ByVal solvingdelay As Integer, ByRef exitcase As Boolean)
        visited(v) = True
        If v.Equals(goal) Then : exitcase = True : Return Nothing : End If
        For Each w As Node In GetNeighbours(v, availablepath)
            If Not visited(w) Then
                If showsolving Then : w.Print("██") : Threading.Thread.Sleep(solvingdelay) : End If
                cameFrom(w) = v
                DFS_Recursive(availablepath, w, visited, cameFrom, goal, showsolving, solvingdelay, exitcase)
                If exitcase Then Return Nothing
            End If
        Next
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
        Console.ForegroundColor = ConsoleColor.Red
        Console.BackgroundColor = ConsoleColor.Red
        While S.Count > 0
            Dim u As Node = S.Pop
            If u.Equals(goal) Then Exit While
            If ShowPath Then : u.Print("██") : Threading.Thread.Sleep(Delay) : End If
            If Not visited(u) Then
                visited(u) = True
                For Each w As Node In GetNeighbours(u, availablepath)
                    If Not visited(w) Then
                        S.Push(w)
                        cameFrom(w) = u
                    End If
                Next
            End If
        End While
        ReconstructPath(cameFrom, goal, start_v, If(ShowSolveTime, $"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds", ""))
    End Sub
    Sub DFS_IterativeFORFILE(ByVal availablepath As List(Of Node), ByRef bmp As Bitmap, ByRef g As Graphics, ByVal Multiplier As Integer)
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
        While S.Count > 0
            Dim u As Node = S.Pop
            If u.Equals(goal) Then Exit While
            If Not visited(u) Then
                visited(u) = True
                For Each w As Node In GetNeighbours(u, availablepath)
                    If Not visited(w) Then
                        S.Push(w)
                        cameFrom(w) = u
                    End If
                Next
            End If
        End While
        ReconstructPathFORFILE(cameFrom, goal, start_v, bmp, g, Multiplier)
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
        Console.ForegroundColor = ConsoleColor.Red
        Console.BackgroundColor = ConsoleColor.Red
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While Q.Count > 0
            Dim v As Node = Q.Dequeue
            If ShowPath Then : v.Print("██") : Threading.Thread.Sleep(Delay) : End If
            If v.Equals(goal) Then : ReconstructPath(cameFrom, v, start_v, If(ShowSolveTime, $"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds", "")) : Exit While : End If
            For Each w As Node In GetNeighbours(v, availablepath)
                If Not Discovered(w) Then
                    Discovered(w) = True
                    Q.Enqueue(w)
                    cameFrom(w) = v
                End If
            Next
        End While
    End Sub
    Sub Dijkstras(ByVal availablepath As List(Of Node), ByVal ShowSolving As Boolean, ByVal SolvingDelay As Integer)
        Dim source As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim target As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
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
        Console.ForegroundColor = ConsoleColor.Red
        Console.BackgroundColor = ConsoleColor.Red
        While Q.Count > 0
            If ExitCase() Then Exit While
            Dim u As Node = ExtractMin(Q, dist)
            If ShowSolving Then : u.Print("██") : Threading.Thread.Sleep(SolvingDelay) : End If
            'If u.Equals(target) Then : Backtrack(prev, target, source, stopwatch) : Exit While : End If
            Q.Remove(u)
            For Each v As Node In GetNeighbours(u, availablepath)
                Dim alt As Integer = dist(u) + 1
                If alt < dist(v) Then
                    dist(v) = alt
                    prev(v) = u
                End If
            Next
        End While
        Backtrack(prev, target, source, stopwatch)
    End Sub
    Sub Backtrack(ByVal prev As Dictionary(Of Node, Node), ByVal target As Node, ByVal source As Node, ByVal watch As Stopwatch)
        Dim u As Node = target
        Dim Pathlength As Integer = 1
        Console.ForegroundColor = ConsoleColor.Green
        Console.BackgroundColor = ConsoleColor.Green
        u.Print("██")
        While prev(u) IsNot Nothing
            u = prev(u)
            u.Print("██")
            Pathlength += 1
        End While
        Dim timetaken As String = $"Time Taken to solve: {watch.Elapsed.TotalSeconds} seconds"
        PrintMessageMiddle($"Path length: {Pathlength}   {timetaken}", Console.WindowHeight - 1, ConsoleColor.Yellow)
        Console.ReadKey()
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
    Function ExtractMinCost(ByVal dist As Dictionary(Of Node, Double), ByVal openSet As List(Of Node))
        Dim returnnode As Node = openSet(openSet.Count - 1)
        For Each node In openSet
            If dist(returnnode) > dist(node) Then returnnode = node
        Next
        Return returnnode
    End Function
    Sub aStarWiki(ByVal availablepath As List(Of Node), ByVal ShowPath As Boolean, ByVal ShowSolveTime As Boolean, ByVal Delay As Integer)
        Dim openSet, closedSet As New List(Of Node)
        Dim start As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim goal As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim gScore, fScore As New Dictionary(Of Node, Double)
        Dim cameFrom As New Dictionary(Of Node, Node)
        Dim INFINITY As Integer = Int32.MaxValue
        For Each node In availablepath
            gScore(node) = INFINITY
            fScore(node) = INFINITY
        Next
        Dim heuristic As Double = 0.1
        gScore(start) = 0
        fScore(start) = h(start, goal, heuristic)
        openSet.Add(start)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Console.ForegroundColor = ConsoleColor.Red
        Console.BackgroundColor = ConsoleColor.Red
        While openSet.Count > 0
            Dim current As Node = ExtractMinCost(gScore, openSet)
            If current.Equals(goal) Then
                ReconstructPath(cameFrom, current, start, If(ShowSolveTime, $"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds", ""))
                Exit While
            End If
            openSet.Remove(current)
            closedSet.Add(current)
            If ShowPath Then : current.Print("██") : Threading.Thread.Sleep(Delay) : End If
            For Each Neighbour As Node In GetNeighbours(current, availablepath)
                If closedSet.Contains(Neighbour) Then Continue For
                Dim tentative_gScore = gScore(current) + 10
                If Not openSet.Contains(Neighbour) Then
                    openSet.Add(Neighbour)
                ElseIf tentative_gScore > gScore(Neighbour) Then
                    Continue For
                End If
                cameFrom(Neighbour) = current
                gScore(Neighbour) = tentative_gScore
                fScore(Neighbour) = gScore(Neighbour) + h(goal, Neighbour, heuristic)
            Next
        End While
    End Sub
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
        Return Math.Sqrt((node.X - goal.X) ^ 2 + (node.Y - goal.Y) ^ 2) * D 'D * (dx + dy) ^ 2
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
        Dim colourList As List(Of Brush) = getBrushColours()
        Dim c As Double = 0
        For Each node In totalPath
            g.FillRectangle(colourList(Math.Floor(c)), (node.X) * Multiplier, (node.Y * 2) * Multiplier, 2 * Multiplier, 2 * Multiplier)
            c += 0.008
            If Math.Floor(c) = colourList.Count Then c = 0
        Next
    End Sub
    Sub ReconstructPath(ByVal camefrom As Dictionary(Of Node, Node), ByVal current As Node, ByVal goal As Node, ByVal timetaken As String)
        Console.ForegroundColor = ConsoleColor.Green
        Console.BackgroundColor = ConsoleColor.Green
        Dim PathLength As Integer = 1
        current.Print("██")
        goal.Print("██")
        While Not current.Equals(goal)
            current = camefrom(current)
            current.Print("██")
            PathLength += 1
        End While
        PrintMessageMiddle($"Path length: {PathLength}   {timetaken}", Console.WindowHeight - 1, ConsoleColor.Yellow)
        Console.ReadKey()
    End Sub
    Sub aStar(ByVal availablepath As List(Of Node), ByVal ShowPath As Boolean, ByVal ShowSolveTime As Boolean, ByVal Delay As Integer)
        Dim start As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim target As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim current As Node = start
        Dim openSet, closedSet As New HashSet(Of Node)
        Console.ForegroundColor = ConsoleColor.Red
        Console.BackgroundColor = ConsoleColor.Red
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
            If ShowPath Then : current.Print("██") : Threading.Thread.Sleep(Delay) : End If
            If current.Equals(target) Then Exit While
            For Each Neighbour As Node In GetNeighbours(current, availablepath)
                If closedSet.Contains(Neighbour) Then Continue For
                Dim tentative_gScore = current.gCost + 10
                If tentative_gScore < Neighbour.gCost Or Not openSet.Contains(Neighbour) Then
                    Neighbour.gCost = tentative_gScore
                    Neighbour.hCost = GetDistance(target, Neighbour)
                    Neighbour.parent = current
                    openSet.Add(Neighbour)
                End If
            Next
        End While
        RetracePath(start, current, If(ShowSolveTime, $"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds", ""))
        Console.ReadKey()
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
                            Else
                                If x = -2 And y = 2 Or x = -2 And y = 1 Then Continue For
                                If x = 2 And y = 1 Or x = 4 And y = 1 Then Continue For
                                If x = 2 And y = -1 Or x = 2 And y = -2 Then Continue For
                                If x = -2 And y = -1 Or x = -4 And y = -1 Then Continue For
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
        Console.Write($"---------------DONE---------------                      number of swastikas found: {numOfsFound}")
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
        Console.ForegroundColor = ConsoleColor.Green
        Console.BackgroundColor = ConsoleColor.Green
        current.Print("██")
        Dim PathLength As Integer = 1
        While Not current.Equals(startnode)
            current = current.parent
            current.Print("██")
            PathLength += 1
        End While
        startnode.Print("██")
        PrintMessageMiddle($"Path length: {PathLength}   {timetaken}", Console.WindowHeight - 1, ConsoleColor.Yellow)
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
                Dim tempNode As New Cell(x, y)
                Li.Add(tempNode)
            Next
        Next
        Dim r As New Random
        Return Li(r.Next(0, Li.Count - 1))
    End Function
    'maze algorithm functions
    Function Kruskals(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Limits(3) -= 4
        Dim CellSet As New Dictionary(Of Cell, Integer)
        Dim AvailableCells As New List(Of Cell)
        Dim SetNumber As Integer = 1
        Dim BorderCells As New List(Of Cell)
        Dim Returnpath As New List(Of Node)
        Console.ForegroundColor = (ConsoleColor.Green)
        Dim EdgeWeights As New Dictionary(Of Cell, Integer)
        Dim R As New Random
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) Step 4
                'Assigning the cell a unique set
                Dim temp As New Cell(x, y)
                CellSet(temp) = SetNumber
                SetNumber += 1
                AvailableCells.Add(New Cell(temp.X, temp.Y))
                'Assigning edge weights
                If x < Limits(2) - 2 And x + 2 <> Limits(2) - 1 Then
                    Dim cur As New Cell(x + 2, y)
                    If Not BorderCells.Contains(cur) Then
                        EdgeWeights(cur) = R.Next(0, 99)
                    End If
                End If
                If y < Limits(3) - 1 Then
                    Dim cur As New Cell(x, y + 1)
                    If Not BorderCells.Contains(cur) Then
                        EdgeWeights(cur) = R.Next(0, 99)
                    End If
                End If
            Next
        Next
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.White
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While EdgeWeights.Count > 0
            'find the edge with the lowest weight
            Dim HighestWeightCell As Cell = EdgeWeights.Keys(0)
            For Each cell In EdgeWeights
                If EdgeWeights(HighestWeightCell) < EdgeWeights(cell.Key) Then HighestWeightCell = cell.Key
            Next
            'TempLowest is now the key with the lowest value in the EdgeWeights dictionary
            Dim WallCell As Cell = HighestWeightCell
            'need to find the two adjacent cells
            Dim AdjacentCells As New List(Of Cell)
            Dim TempCell As New Cell(WallCell.X, WallCell.Y - 1)
            If AvailableCells.Contains(New Cell(WallCell.X, WallCell.Y - 1)) Then AdjacentCells.Add(New Cell(WallCell.X, WallCell.Y - 1))
            If AvailableCells.Contains(New Cell(WallCell.X + 2, WallCell.Y)) Then AdjacentCells.Add(New Cell(WallCell.X + 2, WallCell.Y))
            If AvailableCells.Contains(New Cell(WallCell.X, WallCell.Y + 1)) Then AdjacentCells.Add(New Cell(WallCell.X, WallCell.Y + 1))
            If AvailableCells.Contains(New Cell(WallCell.X - 2, WallCell.Y)) Then AdjacentCells.Add(New Cell(WallCell.X - 2, WallCell.Y))
            Console.ForegroundColor = (ConsoleColor.White)
            If AdjacentCells.Count = 1 Then
                If ShowMazeGeneration Then WallCell.Print("██")
                If ShowMazeGeneration Then AdjacentCells(0).Print("██")
                Returnpath.Add(New Node(WallCell.X, WallCell.Y))
                If Not Returnpath.Contains(New Node(AdjacentCells(0).X, AdjacentCells(0).Y)) Then Returnpath.Add(New Node(AdjacentCells(0).X, AdjacentCells(0).Y))
            Else
                If CellSet(AdjacentCells(1)) <> CellSet(AdjacentCells(0)) Then
                    If ShowMazeGeneration Then WallCell.Print("██")
                    Returnpath.Add(New Node(WallCell.X, WallCell.Y))
                    Dim SetNumToBeChanged As Integer = CellSet(AdjacentCells(1))
                    Dim CellsToBeChanged As New List(Of Cell)
                    For Each thing In CellSet
                        If thing.Value = SetNumToBeChanged Then CellsToBeChanged.Add(thing.Key)
                    Next
                    For Each thing In CellsToBeChanged
                        CellSet(thing) = CellSet(AdjacentCells(0))
                    Next
                    For Each cell In AdjacentCells
                        If ShowMazeGeneration Then cell.Print("██")
                        If Not Returnpath.Contains(New Node(cell.X, cell.Y)) Then Returnpath.Add(New Node(cell.X, cell.Y))
                    Next
                End If
            End If
            EdgeWeights.Remove(WallCell)
            Threading.Thread.Sleep(Delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            Console.ForegroundColor = (ConsoleColor.White)
            Console.BackgroundColor = (ConsoleColor.White)
            For Each node In Returnpath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop + 5
        AddStartAndEnd(Returnpath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return Returnpath
    End Function
    Function Ellers(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        While Limits(2) Mod 4 <> 0
            Limits(2) -= 1
        End While
        Dim Row As New List(Of Cell)
        Dim RowSet As New Dictionary(Of Cell, Integer)
        Dim SetNum As Integer = 0
        Dim R As New Random
        Dim ReturnPath As New List(Of Node)
        Dim availableCellPositions As New List(Of Cell)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim availableCells As New Dictionary(Of Cell, Boolean)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) Step 4
                Dim cur As New Cell(x, y)
                availableCells(cur) = True
            Next
        Next
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.White
        For y = Limits(1) To Limits(3) - 2 Step 2
            For i = 0 To 1
                For x = Limits(0) + 3 To Limits(2) Step 4
                    If ExitCase() Then Return Nothing
                    If i = 0 Then
                        'first pass of the row!
                        Dim CurCell As New Cell(x, y)
                        Row.Add(CurCell)
                        If Not RowSet.ContainsKey(CurCell) And availableCells(CurCell) Then
                            SetNum += 1
                            RowSet(CurCell) = SetNum
                            If ShowMazeGeneration Then CurCell.Print($"██")
                            ReturnPath.Add(New Node(CurCell.X, CurCell.Y))
                        End If
                        availableCellPositions.Add(CurCell)
                    Else
                        'second pass of the row, need to join cells together
                        Dim CurCell As New Cell(x, y)
                        Dim NextCell As New Cell(x + 4, y)
                        Dim CurrentCellSet As Integer = RowSet(CurCell)
                        Dim AdjacentCellSet As Integer = -1
                        If Row.Contains(NextCell) Then AdjacentCellSet = RowSet(NextCell)
                        Dim RandomNum As Integer = R.Next(0, 101)
                        If CurrentCellSet <> AdjacentCellSet And RandomNum > 50 And AdjacentCellSet <> -1 Then
                            'join sets together
                            Dim WallCell As Cell = MidPoint(CurCell, NextCell)
                            If ShowMazeGeneration Then WallCell.Print("██")
                            ReturnPath.Add(New Node(WallCell.X, WallCell.Y))
                            Dim SetNumToBeChanged As Integer = RowSet(NextCell)
                            Dim CellsToBeChanged As New List(Of Cell)
                            For Each thing In RowSet
                                If Row.Contains(thing.Key) Then
                                    If thing.Value = SetNumToBeChanged Then
                                        CellsToBeChanged.Add(thing.Key)
                                    End If
                                End If
                            Next
                            For Each thing In CellsToBeChanged
                                RowSet(thing) = RowSet(CurCell)
                            Next
                        ElseIf CurrentCellSet <> AdjacentCellSet And AdjacentCellSet <> -1 And y >= Limits(3) - 3 Then
                            'final row, need to join sets together
                            Dim WallCell As Cell = MidPoint(CurCell, NextCell)
                            If ShowMazeGeneration Then WallCell.Print("██")
                            ReturnPath.Add(New Node(WallCell.X, WallCell.Y))
                            Dim SetNumToBeChanged As Integer = RowSet(NextCell)
                            Dim CellsToBeChanged As New List(Of Cell)
                            For Each thing In RowSet
                                If Row.Contains(thing.Key) Then
                                    If thing.Value = SetNumToBeChanged Then
                                        CellsToBeChanged.Add(thing.Key)
                                    End If
                                End If
                            Next
                            For Each thing In CellsToBeChanged
                                RowSet(thing) = RowSet(CurCell)
                            Next
                        End If
                        If x = Limits(2) And y <> Limits(3) - 2 And y < Limits(3) - 3 Then
                            'need to carve south
                            Dim CurrentSet As New List(Of Cell)
                            Dim thingy As New List(Of List(Of Cell))
                            Dim FinalCell As Cell = Row(Row.Count - 1)
                            For j = 0 To Row.Count - 1
                                If RowSet(Row(j)) = If(Row(j).Equals(FinalCell), True, RowSet(Row(j + 1))) Then
                                    'if the current cell is in the same set as the next cell then they are in the same set
                                    CurrentSet.Add(Row(j))
                                    CurrentSet.Add(Row(j + 1))
                                Else
                                    'the next cell isnt in the same set as the current cell and therefore a path can be carved south from random cells in the set
                                    If CurrentSet.Count = 0 Then
                                        'individual cell
                                        Dim SouthWallCell As New Cell(Row(j).X, Row(j).Y + 1)
                                        Dim southCell As New Cell(Row(j).X, Row(j).Y + 2)
                                        ReturnPath.Add(New Node(southCell.X, southCell.Y))
                                        ReturnPath.Add(New Node(SouthWallCell.X, SouthWallCell.Y))
                                        RowSet(southCell) = RowSet(Row(j))
                                        availableCells(southCell) = False
                                        If ShowMazeGeneration Then southCell.Print($"██")
                                        If ShowMazeGeneration Then SouthWallCell.Print($"██")
                                    Else
                                        'multiple cells
                                        Dim Indexes As New List(Of Integer)
                                        For k = 0 To 1
                                            Dim idx As Integer = R.Next(0, CurrentSet.Count)
                                            If Not Indexes.Contains(idx) Then Indexes.Add(idx)
                                        Next
                                        Dim Positions As New List(Of Cell)
                                        For k = 0 To Indexes.Count - 1
                                            'If Indexes(k) = 1 Then Continue For
                                            Dim SouthWallCell As New Cell(CurrentSet(Indexes(k)).X, CurrentSet(Indexes(k)).Y + 1)
                                            Dim southCell As New Cell(CurrentSet(Indexes(k)).X, CurrentSet(Indexes(k)).Y + 2)
                                            availableCells(southCell) = False
                                            RowSet(southCell) = RowSet(Row(j))
                                            If ShowMazeGeneration Then southCell.Print($"██")
                                            If ShowMazeGeneration Then SouthWallCell.Print($"██")
                                            ReturnPath.Add(New Node(southCell.X, southCell.Y))
                                            ReturnPath.Add(New Node(SouthWallCell.X, SouthWallCell.Y))
                                        Next
                                        For Each position In Positions
                                            RowSet(position) = RowSet(Row(j))
                                            availableCells(position) = False
                                        Next
                                        CurrentSet.Clear()
                                    End If
                                End If
                                Threading.Thread.Sleep(Delay)
                            Next
                            Row.Clear()
                        End If
                    End If
                    Threading.Thread.Sleep(Delay)
                Next
            Next
        Next
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            Console.ForegroundColor = (ConsoleColor.White)
            Console.BackgroundColor = (ConsoleColor.White)
            For Each node In ReturnPath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        Limits(3) -= 2
        AddStartAndEnd(ReturnPath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnPath
    End Function
    Function Sidewinder(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        While Limits(2) Mod 4 <> 2
            Limits(2) -= 1
        End While
        Dim WallCell As Cell
        Dim RunSet As New List(Of Cell)
        Dim Availablepath As New List(Of Node)
        Dim R As New Random
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.White
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) Step 4
                If ExitCase() Then Return Nothing
                Dim CurrentCell As New Cell(x, y)
                Availablepath.Add(New Node(CurrentCell.X, CurrentCell.Y))
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
                        Availablepath.Add(New Node(WallCell.X, WallCell.Y))
                    End If
                Else
                    Dim RandomRunSet As Integer = R.Next(0, RunSet.Count)
                    Dim RandomRunSetCell As Cell = RunSet(RandomRunSet)
                    Dim NorthCell As New Cell(RandomRunSetCell.X, y - 2)
                    WallCell = MidPoint(RandomRunSetCell, NorthCell)
                    Availablepath.Add(New Node(WallCell.X, WallCell.Y))
                    If ShowMazeGeneration Then WallCell.Print("██")
                    RunSet.Clear()
                End If
                Threading.Thread.Sleep(Delay)
            Next
        Next
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            Console.ForegroundColor = (ConsoleColor.White)
            Console.BackgroundColor = (ConsoleColor.White)
            For Each node In Availablepath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(Availablepath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return Availablepath
    End Function
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
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.White
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
            Console.ForegroundColor = (ConsoleColor.White)
            Console.BackgroundColor = (ConsoleColor.White)
            For Each node In Availablepath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(Availablepath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return Availablepath
    End Function
    Function GrowingTree(ByVal Limits() As Integer, ByVal delay As Integer, ByVal CellSelectionMethod() As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim FrontierSet, RecentFrontierSet As New List(Of Cell)
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim PreviousCell As Cell = CurrentCell
        Dim WallCell As Cell
        Dim Index As Integer
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim ReturnablePath As New List(Of Node)
        VisitedCells(CurrentCell) = True
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.White
        If ShowMazeGeneration Then CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.White
        While True
            If ExitCase() Then Return Nothing
            For Each cell As Cell In Neighbour(CurrentCell, VisitedCells, Limits, True)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
                RecentFrontierSet.Add(cell)
                If ShowMazeGeneration Then
                    Console.ForegroundColor = ConsoleColor.Yellow
                    Console.BackgroundColor = ConsoleColor.Yellow
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
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            ElseIf CellSelectionMethod(1) = 1 Then
                'Prim's
                If FrontierSet.Count = 0 Then Exit While
                Index = R.Next(0, FrontierSet.Count)
                CurrentCell = FrontierSet(Index)
                Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
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
                        Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                        PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                    End If
                Else
                    'Random
                    If FrontierSet.Count = 0 Then Exit While
                    Index = R.Next(0, FrontierSet.Count)
                    CurrentCell = FrontierSet(Index)
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            ElseIf CellSelectionMethod(5) = 1 Then
                'Oldest
                If FrontierSet.Count = 0 Then Exit While
                Index = 0
                CurrentCell = FrontierSet(Index)
                Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
            ElseIf CellSelectionMethod(6) = 1 Then
                'Middle
                If FrontierSet.Count = 0 Then Exit While
                Index = FrontierSet.Count / 2
                CurrentCell = FrontierSet(Index)
                Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
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
                        Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                        PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                    End If
                Else
                    'Oldest
                    If FrontierSet.Count = 0 Then Exit While
                    Index = 0 'R.Next(0, FrontierSet.Count)
                    CurrentCell = FrontierSet(Index)
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
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
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                Else
                    'Random
                    If FrontierSet.Count = 0 Then Exit While
                    Index = R.Next(0, FrontierSet.Count)
                    CurrentCell = FrontierSet(Index)
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            End If
            WallCell = MidPoint(CurrentCell, PreviousCell)
            If ShowMazeGeneration Then
                Console.ForegroundColor = ConsoleColor.White
                Console.BackgroundColor = ConsoleColor.White
                WallCell.Print("██")
                CurrentCell.Print("██")
            End If
            VisitedCells(CurrentCell) = True
            FrontierSet.Remove(CurrentCell)
            ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
            ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
            Threading.Thread.Sleep(delay)
            RecentFrontierSet.Clear()
            PreviousCell = CurrentCell
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            Console.ForegroundColor = (ConsoleColor.White)
            Console.BackgroundColor = (ConsoleColor.White)
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function Custom(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim FrontierSet, RecentFrontierSet As New List(Of Cell)
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim WallCell As Cell
        Dim Index As Integer
        Dim ReturnablePath As New List(Of Node)
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        VisitedCells(CurrentCell) = True
        If ShowMazeGeneration Then CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.White
        While True
            If ExitCase() Then Return Nothing
            For Each cell As Cell In Neighbour(CurrentCell, VisitedCells, Limits, True)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
                RecentFrontierSet.Add(cell)
            Next
            If RecentFrontierSet.Count > 0 Then
                Index = R.Next(0, RecentFrontierSet.Count)
                CurrentCell = RecentFrontierSet(Index)
            Else
                If FrontierSet.Count = 0 Then Exit While
                Index = FrontierSet.Count - 1 'R.Next(0, FrontierSet.Count)
                CurrentCell = FrontierSet(Index)
            End If
            Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
            Dim PreviousCell As Cell = PickAdjancentCell(CurrentCell, AdjancencyList)
            WallCell = MidPoint(CurrentCell, PreviousCell)
            If ShowMazeGeneration Then
                Console.ForegroundColor = ConsoleColor.White
                Console.BackgroundColor = ConsoleColor.White
                WallCell.Print("██")
                CurrentCell.Print("██")
            End If
            VisitedCells(CurrentCell) = True
            FrontierSet.Remove(CurrentCell)
            ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
            ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
            Threading.Thread.Sleep(delay)
            RecentFrontierSet.Clear()
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)

        If Not ShowMazeGeneration Then
            Console.ForegroundColor = ConsoleColor.White
            Console.BackgroundColor = ConsoleColor.White
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function AldousBroder(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim TotalCellCount As Integer
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim RecentCells As New List(Of Cell)
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim PrevCell As Cell = PickRandomStartingCell(Limits)
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim PreviousCell As Cell = CurrentCell
        Dim WallCell As Cell
        Dim ReturnablePath As New List(Of Node)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                TotalCellCount += 1
            Next
        Next
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.White
        VisitedCells(CurrentCell) = True
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        If ShowMazeGeneration Then CurrentCell.Print("██")
        Dim UsedCellCount As Integer = 1
        While UsedCellCount <> TotalCellCount
            If ExitCase() Then Return Nothing
            RecentCells.Clear()
            RecentCells = RanNeighbour(CurrentCell, Limits)
            Dim Index As Integer = R.Next(0, RecentCells.Count)
            Dim TemporaryCell As Cell = RecentCells(Index)
            Dim TempNodeCell As New Node(TemporaryCell.X, TemporaryCell.Y)
            If Not VisitedCells(TemporaryCell) Then
                VisitedCells(New Cell(TemporaryCell.X, TemporaryCell.Y)) = True
                UsedCellCount += 1
                WallCell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                ReturnablePath.Add(New Node(TemporaryCell.X, TemporaryCell.Y))
                If ShowMazeGeneration Then
                    Console.ForegroundColor = ConsoleColor.White
                    Console.BackgroundColor = ConsoleColor.White
                    PrevCell.Print("██")
                    WallCell.Print("██")
                    Console.ForegroundColor = ConsoleColor.Blue
                    Console.BackgroundColor = ConsoleColor.Blue
                    TemporaryCell.Print("██")
                    PrevCell = CurrentCell
                End If
            Else
                CurrentCell = TemporaryCell
                If ShowMazeGeneration Then
                    Console.ForegroundColor = ConsoleColor.White
                    Console.BackgroundColor = ConsoleColor.White
                    PrevCell.Print("██")
                    Console.ForegroundColor = ConsoleColor.Blue
                    Console.BackgroundColor = ConsoleColor.Blue
                    TemporaryCell.Print("██")
                    PrevCell = CurrentCell
                End If
            End If
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            Console.ForegroundColor = ConsoleColor.White
            Console.BackgroundColor = ConsoleColor.White
            For Each node In ReturnablePath
                node.Print("██")
            Next
        Else
            Console.ForegroundColor = ConsoleColor.White
            Console.BackgroundColor = ConsoleColor.White
            PrevCell.Print("██")
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function Prims_True(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
        'Assigns a random weight between 0, 99 to each available in the grid, it then chooses the cell with the lowest weight out of the frontier set
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim FrontierSet As New List(Of Cell)
        Dim WallCell As Cell
        Dim ReturnablePath As New List(Of Node)
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim Weights As New Dictionary(Of Cell, Integer)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                Dim tempNode As New Cell(x, y)
                Weights(tempNode) = R.Next(0, 99) 'Assigning random weights to each cell in the grid
            Next
        Next
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(FrontierSet(idx).X, FrontierSet(idx).Y) '(Limits(0) + 3, Limits(1) + 2)
        VisitedCells(CurrentCell) = True
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.White
        If ShowMazeGeneration Then CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        While True
            If ExitCase() Then Return Nothing
            For Each cell As Cell In Neighbour(CurrentCell, VisitedCells, Limits, True)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
                'If ShowMazeGeneration Then
                '    Console.ForegroundColor = ConsoleColor.Yellow
                '    Console.BackgroundColor = ConsoleColor.Yellow
                '    cell.Print("██")
                'End If
            Next
            If FrontierSet.Count = 0 Then Exit While
            Dim HighestWeightCell As Cell = FrontierSet(0)
            For Each cell In FrontierSet
                If Weights(HighestWeightCell) < Weights(cell) Then HighestWeightCell = cell
            Next
            CurrentCell = HighestWeightCell
            Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
            Dim PreviousCell As Cell = PickAdjancentCell(CurrentCell, AdjancencyList)
            WallCell = MidPoint(CurrentCell, PreviousCell)
            If ShowMazeGeneration Then
                Console.ForegroundColor = ConsoleColor.White
                Console.BackgroundColor = ConsoleColor.White
                WallCell.Print("██")
                CurrentCell.Print("██")
            End If
            VisitedCells(CurrentCell) = True
            FrontierSet.Remove(CurrentCell)
            AddToPath(ReturnablePath, CurrentCell, WallCell)
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            Console.ForegroundColor = (ConsoleColor.White)
            Console.BackgroundColor = (ConsoleColor.White)
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function Prims_Simplified(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
        'Assumes the weights of each cell in the grid is the same and therefore chooses a random cell from the frontier set
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim FrontierSet As New List(Of Cell)
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim WallCell As Cell
        Dim ReturnablePath As New List(Of Node)
        VisitedCells(CurrentCell) = True
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.White
        If ShowMazeGeneration Then CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While True
            If ExitCase() Then Return Nothing
            For Each cell As Cell In Neighbour(CurrentCell, VisitedCells, Limits, True)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
                'If ShowMazeGeneration Then
                '    Console.ForegroundColor = ConsoleColor.Yellow
                '    Console.BackgroundColor = ConsoleColor.Yellow
                '    cell.Print("██")
                'End If
            Next
            If FrontierSet.Count = 0 Then Exit While
            CurrentCell = FrontierSet(R.Next(0, FrontierSet.Count))
            Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
            Dim PreviousCell As Cell = PickAdjancentCell(CurrentCell, AdjancencyList)
            WallCell = MidPoint(CurrentCell, PreviousCell)
            If ShowMazeGeneration Then
                Console.ForegroundColor = ConsoleColor.White
                Console.BackgroundColor = ConsoleColor.White
                WallCell.Print("██")
                CurrentCell.Print("██")
            End If
            VisitedCells(CurrentCell) = True
            FrontierSet.Remove(CurrentCell)
            AddToPath(ReturnablePath, CurrentCell, WallCell)
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            Console.ForegroundColor = (ConsoleColor.White)
            Console.BackgroundColor = (ConsoleColor.White)
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function HuntAndKillREFACTORED(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim r As New Random
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim totalcellcount As Integer = VisitedCells.Count
        VisitedCells(CurrentCell) = True
        Dim ReturnablePath As New List(Of Node)
        Dim UsedCellPositions As Integer = 1
        Console.ForegroundColor = (ConsoleColor.White)
        Console.BackgroundColor = (ConsoleColor.White)
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        If ShowMazeGeneration Then CurrentCell.Print("██")
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While UsedCellPositions <> totalcellcount
            If ExitCase() Then Return Nothing
            If Neighbour(CurrentCell, VisitedCells, Limits, False) Then
                Dim RecentCells As List(Of Cell) = Neighbour(CurrentCell, VisitedCells, Limits, True)
                Dim TemporaryCell As Cell = RecentCells(r.Next(0, RecentCells.Count))
                Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
                ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                UsedCellPositions += 1
                RecentCells.Clear()
                If ShowMazeGeneration Then
                    Console.ForegroundColor = (ConsoleColor.White)
                    Console.BackgroundColor = (ConsoleColor.White)
                    WallCell.Print("██")
                    CurrentCell.Print("██")
                End If
                VisitedCells(CurrentCell) = True
            Else
                Dim CellFound As Boolean = False
                For y = Limits(1) To Limits(3) Step 2
                    For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                        If ShowMazeGeneration Then
                            Console.ForegroundColor = (ConsoleColor.Blue)
                            Console.BackgroundColor = (ConsoleColor.Blue)
                            Console.SetCursorPosition(x, y)
                            Console.Write("██")
                            If x + 2 < Limits(2) - 1 Then
                                Console.SetCursorPosition(x + 2, y)
                                Console.Write("██")
                            End If
                        End If
                        Dim AdjancencyList As Integer() = AdjacentCheck(New Cell(x, y), VisitedCells)
                        Dim pathCell As Cell = PickAdjancentCell(New Cell(x, y), AdjancencyList)
                        If Not IsNothing(pathCell) Then
                            Dim WallCell As Cell = MidPoint(pathCell, New Cell(x, y))
                            CurrentCell = New Cell(x, y)
                            If ShowMazeGeneration Then
                                Console.ForegroundColor = (ConsoleColor.White)
                                Console.BackgroundColor = (ConsoleColor.White)
                                WallCell.Print("██")
                                CurrentCell.Print("██")
                                EraseLineHaK(Limits, x + 1, ReturnablePath, y)
                            End If
                            UsedCellPositions += 1
                            ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
                            ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                            CellFound = True
                            VisitedCells(CurrentCell) = True
                            Exit For
                        End If
                    Next
                    If ShowMazeGeneration Then
                        Threading.Thread.Sleep(delay)
                        EraseLineHaK(Limits, Limits(2), ReturnablePath, y)
                    End If
                    If CellFound Then Exit For
                Next
            End If
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        'EliminateDeadEnds(ReturnablePath)
        If Not ShowMazeGeneration Then
            Console.ForegroundColor = (ConsoleColor.White)
            Console.BackgroundColor = (ConsoleColor.White)
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function RecursiveBacktrackerRecursively(ByVal cell As Cell, ByVal limits() As Integer, ByVal path As List(Of Node), ByRef visited As Dictionary(Of Cell, Boolean), ByRef cameFrom As Cell, ByVal r As Random, ByVal ShowMazeGeneration As Boolean, ByVal Delay As Integer)
        If Neighbour(cell, visited, limits, False) Then
            Dim RecentCells As List(Of Cell) = Neighbour(cell, visited, limits, True)
            Dim TemporaryCell As Cell = RecentCells(r.Next(0, RecentCells.Count))
            Dim wall As Cell = MidPoint(cameFrom, TemporaryCell)
            If ShowMazeGeneration Then
                wall.Print("██")
                TemporaryCell.Print("██")
                Threading.Thread.Sleep(Delay) : End If
            AddToPath(path, TemporaryCell, wall)
            cameFrom = TemporaryCell
            visited(TemporaryCell) = True
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
        Dim back As ConsoleColor = ConsoleColor.White
        If back <> ConsoleColor.White Then
            Console.ForegroundColor = ConsoleColor.White
            Console.BackgroundColor = ConsoleColor.White
            For y = Limits(1) - 1 To Limits(3) + 1
                For x = Limits(0) + 1 To Limits(2) + 1
                    Console.SetCursorPosition(x, y)
                    Console.Write("XX")
                Next
            Next
        End If
        Console.ForegroundColor = back
        Console.BackgroundColor = back
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim PrevCell As Cell = CurrentCell '(Limits(0) + 3, Limits(1) + 2)
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim Stack As New List(Of Cell)
        Dim ReturnablePath As New List(Of Node)
        Dim RecentCells As New List(Of Cell)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        VisitedCells(CurrentCell) = True
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        If ShowMazeGeneration Then CurrentCell.Print("██")
        While True
            If ExitCase() Then Return Nothing
            If ShowMazeGeneration Then
                PrevCell.Print("██")
                Console.ForegroundColor = back
                Console.BackgroundColor = back
            End If
            If Neighbour(CurrentCell, VisitedCells, Limits, False) Then 'done
                RecentCells.Clear()
                RecentCells = Neighbour(CurrentCell, VisitedCells, Limits, True)
                Dim TemporaryCell As Cell = RecentCells(r.Next(0, RecentCells.Count))
                VisitedCells(TemporaryCell) = True
                Stack.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                AddToPath(ReturnablePath, TemporaryCell, WallCell)
                If ShowMazeGeneration Then
                    Console.ForegroundColor = back
                    Console.BackgroundColor = back
                    PrevCell.Print("██")
                    WallCell.Print("██")
                    Console.ForegroundColor = ConsoleColor.Blue
                    Console.BackgroundColor = ConsoleColor.Blue
                    TemporaryCell.Print("██")
                    PrevCell = CurrentCell
                End If
            ElseIf Stack.Count > 1 Then
                CurrentCell = CurrentCell.Pop(Stack)
                If ShowMazeGeneration Then
                    Console.ForegroundColor = back
                    Console.BackgroundColor = back
                    PrevCell.Print("██")
                    Console.ForegroundColor = ConsoleColor.Blue
                    Console.BackgroundColor = ConsoleColor.Blue
                    CurrentCell.Print("██")
                    Console.ForegroundColor = back
                    Console.BackgroundColor = back
                    PrevCell = CurrentCell
                End If
            Else
                Exit While
            End If
            Threading.Thread.Sleep(Delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            Console.ForegroundColor = (back)
            Console.BackgroundColor = (back)
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function SpiralBacktracker(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim r As New Random
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits)
        Dim PrevCell As Cell = CurrentCell
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim Stack As New List(Of Cell)
        Dim ReturnablePath As New List(Of Node)
        Dim RecentCells As New List(Of Cell)
        Dim Adding As Double = GetIntInputArrowKeys("Cell Reach: ", 100, 0, True)
        Dim CurrentCellReach As Double = 0
        Dim Dir As String = "UP"
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.White
        VisitedCells(CurrentCell) = True
        If ShowMazeGeneration Then CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While True
            If ExitCase() Then Return Nothing
            If ShowMazeGeneration Then
                PrevCell.Print("██")
                Console.ForegroundColor = ConsoleColor.White
                Console.BackgroundColor = ConsoleColor.White
            End If
            If Neighbour(CurrentCell, VisitedCells, Limits, False) Then 'done
                Dim TempCell As New Cell(-1, -1)
                Dim ValidNextCell As Boolean = False
                Do
                    If Dir = "UP" Then
                        TempCell.Update(CurrentCell.X, CurrentCell.Y - 2)
                        If VisitedCells.ContainsKey(TempCell) AndAlso Not VisitedCells(TempCell) And TempCell.WithinLimits(Limits) Then
                            ValidNextCell = True
                        Else
                            Dir = "RIGHT"
                        End If
                    ElseIf Dir = "RIGHT" Then
                        TempCell.Update(CurrentCell.X + 4, CurrentCell.Y)
                        If VisitedCells.ContainsKey(TempCell) AndAlso Not VisitedCells(TempCell) And TempCell.WithinLimits(Limits) Then
                            ValidNextCell = True
                        Else
                            Dir = "DOWN"
                        End If
                    ElseIf Dir = "DOWN" Then
                        TempCell.Update(CurrentCell.X, CurrentCell.Y + 2)
                        If VisitedCells.ContainsKey(TempCell) AndAlso Not VisitedCells(TempCell) And TempCell.WithinLimits(Limits) Then
                            ValidNextCell = True
                        Else
                            Dir = "LEFT"
                        End If
                    ElseIf Dir = "LEFT" Then
                        TempCell.Update(CurrentCell.X - 4, CurrentCell.Y)
                        If VisitedCells.ContainsKey(TempCell) AndAlso Not VisitedCells(TempCell) And TempCell.WithinLimits(Limits) Then
                            ValidNextCell = True
                        Else
                            Dir = "UP"
                        End If
                    End If
                Loop Until ValidNextCell
                Dim TemporaryCell As Cell = TempCell
                If Math.Floor(CurrentCellReach) = 0 Then
                    Dir = "UP"
                ElseIf Math.Floor(CurrentCellReach) = 1 Then
                    Dir = "RIGHT"
                ElseIf Math.Floor(CurrentCellReach) = 2 Then
                    Dir = "DOWN"
                ElseIf Math.Floor(CurrentCellReach) = 3 Then
                    Dir = "LEFT"
                End If
                CurrentCellReach += Adding / 100
                If Math.Floor(CurrentCellReach) = 5 Then CurrentCellReach = 0
                VisitedCells(TemporaryCell) = True
                Stack.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                AddToPath(ReturnablePath, TemporaryCell, WallCell)
                If ShowMazeGeneration Then
                    Console.ForegroundColor = ConsoleColor.White
                    Console.BackgroundColor = ConsoleColor.White
                    PrevCell.Print("██")
                    WallCell.Print("██")
                    Console.ForegroundColor = ConsoleColor.Blue
                    Console.BackgroundColor = ConsoleColor.Blue
                    TemporaryCell.Print("██")
                    PrevCell = CurrentCell
                End If
            ElseIf Stack.Count > 1 Then
                CurrentCell = CurrentCell.Pop(Stack)
                If ShowMazeGeneration Then
                    Console.ForegroundColor = ConsoleColor.White
                    Console.BackgroundColor = ConsoleColor.White
                    PrevCell.Print("██")
                    Console.ForegroundColor = ConsoleColor.Blue
                    Console.BackgroundColor = ConsoleColor.Blue
                    CurrentCell.Print("██")
                    Console.ForegroundColor = ConsoleColor.White
                    Console.BackgroundColor = ConsoleColor.White
                    PrevCell = CurrentCell
                End If
            Else
                Exit While
            End If
            Threading.Thread.Sleep(Delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            Console.ForegroundColor = (ConsoleColor.White)
            Console.BackgroundColor = (ConsoleColor.White)
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function Wilsons(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim UST, RecentCells, availablepositions As New List(Of Cell)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim PrevCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim ReturnablePath As New List(Of Node)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                availablepositions.Add(New Cell(x, y))
            Next
        Next
        Dim CellCount As Integer = availablepositions.Count
        Dim StartingCell As Cell = PickRandomCell(availablepositions, UST, Limits)
        If ShowMazeGeneration Then
            Console.ForegroundColor = ConsoleColor.White
            Console.BackgroundColor = ConsoleColor.White
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
                Console.ForegroundColor = ConsoleColor.White
                Console.BackgroundColor = ConsoleColor.White
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
                            Console.ForegroundColor = ConsoleColor.White
                            Console.BackgroundColor = ConsoleColor.White
                            thing.Key.Print("  ")
                        Else
                            Console.ForegroundColor = ConsoleColor.Black
                            Console.BackgroundColor = ConsoleColor.Black
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
                Console.ForegroundColor = ConsoleColor.White
                Console.BackgroundColor = ConsoleColor.White
                PrevCell = CurrentCell
            End If
        End While



        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            Console.ForegroundColor = (ConsoleColor.White)
            Console.BackgroundColor = (ConsoleColor.White)
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function Houstons(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim Direction, newdir As New Dictionary(Of Cell, String)
        Dim directions As New Dictionary(Of Cell, String)
        Dim TotalCellCount As Integer
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim VisitedList, RecentCells As New List(Of Cell)
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim PrevCell As Cell = PickRandomStartingCell(Limits)
        Dim PreviousCell As Cell = CurrentCell
        Dim WallCell As Cell
        Dim ReturnablePath As New List(Of Node)
        Dim AvailableCells As New List(Of Cell)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                TotalCellCount += 1
                AvailableCells.Add(New Cell(x, y))
            Next
        Next
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.White
        While 1
            If TotalCellCount / 3 > VisitedList.Count Then
                If ExitCase() Then Return Nothing
                RecentCells.Clear()
                For Each cell As Cell In RanNeighbour(CurrentCell, Limits)
                    RecentCells.Add(cell)
                Next
                Dim Index As Integer = R.Next(0, RecentCells.Count)
                Dim TemporaryCell As Cell = RecentCells(Index)
                Dim TempNodeCell As New Node(TemporaryCell.X, TemporaryCell.Y)
                If Not VisitedList.Contains(TemporaryCell) Then
                    VisitedList.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                    WallCell = MidPoint(CurrentCell, TemporaryCell)
                    CurrentCell = TemporaryCell
                    ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                    ReturnablePath.Add(New Node(TemporaryCell.X, TemporaryCell.Y))
                    If ShowMazeGeneration Then
                        Console.ForegroundColor = ConsoleColor.White
                        Console.BackgroundColor = ConsoleColor.White
                        PrevCell.Print("██")
                        WallCell.Print("██")
                        Console.ForegroundColor = ConsoleColor.Blue
                        Console.BackgroundColor = ConsoleColor.Blue
                        TemporaryCell.Print("██")
                        PrevCell = CurrentCell
                    End If
                Else
                    CurrentCell = TemporaryCell
                    If ShowMazeGeneration Then
                        Console.ForegroundColor = ConsoleColor.White
                        Console.BackgroundColor = ConsoleColor.White
                        PrevCell.Print("██")
                        Console.ForegroundColor = ConsoleColor.Blue
                        Console.BackgroundColor = ConsoleColor.Blue
                        TemporaryCell.Print("██")
                        PrevCell = CurrentCell
                    End If
                End If
                Threading.Thread.Sleep(Delay)
            Else
                'wilsons
                If ExitCase() Then Return Nothing
                RecentCells.Clear()
                Dim TemporaryCell As Cell
                For Each cell As Cell In RanNeighbour(CurrentCell, Limits)
                    RecentCells.Add(cell)
                Next
                TemporaryCell = RecentCells(R.Next(0, RecentCells.Count))
                Dim dir As String = GetDirection(CurrentCell, TemporaryCell, directions, ShowMazeGeneration)
                If VisitedList.Contains(TemporaryCell) Then 'Unvisited cell?
                    Direction.Add(TemporaryCell, GetDirection(TemporaryCell, CurrentCell, directions, ShowMazeGeneration))
                    Console.ForegroundColor = ConsoleColor.White
                    Console.BackgroundColor = ConsoleColor.White
                    Dim NewList As New List(Of Cell)
                    Dim current As Cell = directions.Keys(0)
                    Dim cur As Cell = current
                    While 1
                        Dim prev111 As Cell = cur
                        cur = PickNextDir(prev111, directions, ShowMazeGeneration, Delay, ReturnablePath)
                        NewList.Add(cur)
                        AvailableCells.Remove(cur)
                        If VisitedList.Contains(cur) Then Exit While
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
                        If Not VisitedList.Contains(value) Then VisitedList.Add(value)
                    Next
                    If Not VisitedList.Contains(directions.Keys(0)) Then VisitedList.Add(directions.Keys(0))
                    NewList.Clear()
                    directions.Clear()
                    Threading.Thread.Sleep(Delay)
                    If ShowMazeGeneration Then
                        For Each thing In Direction
                            If VisitedList.Contains(thing.Key) Then
                                Console.ForegroundColor = ConsoleColor.White
                                Console.BackgroundColor = ConsoleColor.White
                                thing.Key.Print("  ")
                            Else
                                Console.ForegroundColor = ConsoleColor.Black
                                Console.BackgroundColor = ConsoleColor.Black
                                thing.Key.Print("  ")
                            End If
                        Next
                    End If
                    Direction.Clear()
                    If TotalCellCount = VisitedList.Count Then Exit While
                    CurrentCell = PickRandomCell(AvailableCells, VisitedList, Limits)
                Else
                    CurrentCell = TemporaryCell
                    If Direction.ContainsKey(CurrentCell) Then
                        Direction(CurrentCell) = GetDirection(CurrentCell, PrevCell, directions, ShowMazeGeneration)
                    Else
                        Direction.Add(CurrentCell, GetDirection(CurrentCell, PrevCell, directions, ShowMazeGeneration))
                    End If
                    Console.ForegroundColor = ConsoleColor.White
                    Console.BackgroundColor = ConsoleColor.White
                    PrevCell = CurrentCell
                End If
            End If
        End While




        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            Console.ForegroundColor = (ConsoleColor.White)
            Console.BackgroundColor = (ConsoleColor.White)
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath


    End Function
    'Subs/functions for the algorithms
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
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.White
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
        Dim Multiplier As Integer = 5
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
        If solving Then DFS_IterativeFORFILE(Path, bmp, g, Multiplier)
        g.FillRectangle(Brushes.Red, (Path(Path.Count - 2).X) * Multiplier, (Path(Path.Count - 2).Y + 2) * Multiplier, 2 * Multiplier, 2 * Multiplier)
        g.FillRectangle(Brushes.Lime, (Path(Path.Count - 1).X) * Multiplier, (Path(Path.Count - 1).Y * 2) * Multiplier, 2 * Multiplier, 2 * Multiplier)
        Dim f As New Font("Roboto", Width / 60)
        Dim point As New PointF(((Width) / 2) - (Algorithm.Length / 2) * Multiplier, 1)
        g.DrawString(Algorithm, f, Brushes.White, point)
        g.Dispose()
        bmp.Save($"{fileName}.PNG", System.Drawing.Imaging.ImageFormat.Png)
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
                Console.ForegroundColor = ConsoleColor.Black
                Console.BackgroundColor = ConsoleColor.Black
                tempcell.Print("  ")
            Else
                Console.ForegroundColor = ConsoleColor.White
                Console.BackgroundColor = ConsoleColor.White
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
        Dim ReturnCell As  Cell
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
    Public Function ContainsRecursive(ByVal current As Tree, ByVal value As Value)
        If IsNothing(current) Then Return False
        If value.IntValue = current.value.IntValue Then Return True
        Return value.IntValue < current.value.IntValue
        ContainsRecursive(current.left, value)
        ContainsRecursive(current.right, value)
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