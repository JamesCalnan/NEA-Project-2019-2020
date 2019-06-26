Imports System.Drawing
Imports System.IO
Imports Revised_2

Module Module1
    'TODO:  fix when loading the previous maze twice
    Sub Main()

        Console.CursorVisible = False
        SetColour(ConsoleColor.White)
        Dim MenuOptions() As String = {"Recursive Backtracker Algorithm", "Hunt and Kill Algorithm", "Prim's Algorithm", "Aldous-Broder Algorithm", "Growing Tree Algorithm", "Custom Algorithm", "Sidewinder Algorithm", "Binary Tree Algorithm", "Load the previously generated maze", "Save the previously generated maze", "Load a saved maze", "Exit"}
        Menu(MenuOptions)
        Console.ReadKey()
    End Sub

    Sub SetBackGroundColour(ByVal colour As ConsoleColor)
        Console.BackgroundColor = colour
    End Sub
    Sub SetColour(ByVal colour As ConsoleColor)
        Console.ForegroundColor = colour
    End Sub
    Sub GetMazeInfo(ByRef Width As Integer, ByRef Height As Integer, ByRef DelayMS As Integer, ByRef Limits() As Integer)
        Console.Clear()
        DelayMS = GetIntInputArrowKeys("Delay when making the Maze (MS): ", 100, 0)
        Width = GetIntInputArrowKeys($"Width of the Maze: ", Console.WindowWidth - 6, 20)
        If Width Mod 2 = 0 Then Width += 1
        Height = GetIntInputArrowKeys($"Height of the Maze: ", Console.WindowHeight - 6, 10)
        If Height Mod 2 = 0 Then Height += 1
        Limits = {5, 3, Width, Height}
        Console.Clear()
    End Sub
    Sub MsgColour(ByVal Msg As String, ByVal Colour As ConsoleColor)
        SetColour(Colour)
        Console.WriteLine(Msg)
        SetColour(ConsoleColor.White)
    End Sub
    Function GetIntInputArrowKeys(ByVal message As String, ByVal NumMax As Integer, ByVal NumMin As Integer)
        Console.Write(message)
        SetColour(ConsoleColor.Yellow)
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
        Console.SetCursorPosition(0, cursortop + 1)
        SetColour(ConsoleColor.White)
        Return current
    End Function
    Sub DisplayAvailablePositions(ByVal count As Integer)
        Dim mess As String = $"There are {count} available positions in the maze"
        Console.SetCursorPosition((Console.WindowWidth / 2) - (mess.Count / 2), 0)
        Console.Write(mess)
    End Sub
    Function GrowingTreeMenu(ByVal arr() As String)
        Console.Clear()
        Dim CurrentCol As Integer = Console.CursorTop
        Dim y As Integer = Console.CursorTop
        Dim NumOfOptions As Integer = arr.Count
        MsgColour("What Cell Selection Method: ", ConsoleColor.Yellow)
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
                    If y = 0 Then
                        Return {1, 0, 0, 0, 0, 0, 0, 0, 0}
                    ElseIf y = 1 Then
                        Return {0, 1, 0, 0, 0, 0, 0, 0, 0}
                    ElseIf y = 2 Then
                        Return {0, 0, 1, 0, 0, 0, 0, 0, 0}
                    ElseIf y = 3 Then
                        Return {0, 0, 0, 1, 0, 0, 0, 0, 0}
                    ElseIf y = 4 Then
                        Return {0, 0, 0, 0, 1, 0, 0, 0, 0}
                    ElseIf y = 5 Then
                        Return {0, 0, 0, 0, 0, 1, 0, 0, 0}
                    ElseIf y = 6 Then
                        Return {0, 0, 0, 0, 0, 0, 1, 0, 0}
                    ElseIf y = 7 Then
                        Return {0, 0, 0, 0, 0, 0, 0, 1, 0}
                    ElseIf y = 8 Then
                        Return {0, 0, 0, 0, 0, 0, 0, 0, 1}
                    Else
                        OptionNotReady()
                    End If
                    SetBackGroundColour(ConsoleColor.Black)
                    Console.Clear()
                    MsgColour("What cell selection method would you like to use: ", ConsoleColor.Yellow)
            End Select
            SetColour(ConsoleColor.White)
            Dim Count As Integer = 1
            For i = 0 To arr.Count - 1
                Console.SetCursorPosition(0, i + 1 + CurrentCol)
                Console.Write("                                      ")
            Next
            For Each MenuOption In arr
                Console.SetCursorPosition(0, Count + CurrentCol)
                Console.Write($" {MenuOption}")
                Count += 1
            Next
            Console.SetCursorPosition(0, y + 1)
            MsgColour($"> {arr(y)}", ConsoleColor.Green)
        End While
    End Function
    Sub Menu(ByVal arr() As String)
        Dim PreviousMaze, LoadedMaze As New List(Of Node)
        Console.Clear()
        Dim CurrentCol As Integer = Console.CursorTop
        Dim y As Integer = Console.CursorTop
        Dim NumOfOptions As Integer = arr.Count
        MsgColour("What Maze Generation Algorithm do you want to use: ", ConsoleColor.Yellow)
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
                    If y = 0 Then
                        Dim Width, Height, DelayMS, Limits() As Integer
                        GetMazeInfo(Width, Height, DelayMS, Limits)
                        Dim AvailablePath As List(Of Node) = RecursiveBacktracker(Limits, DelayMS)
                        If AvailablePath IsNot Nothing Then
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Console.CursorTop
                            DisplayAvailablePositions(AvailablePath.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 2)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 2)
                            PreviousMaze.Clear()
                            For Each node In AvailablePath
                                PreviousMaze.Add(New Node(node.X, node.Y))
                            Next
                            If input = "astar" Then
                                AStar(AvailablePath)
                            Else
                                OptionNotReady()
                            End If
                        End If
                    ElseIf y = 1 Then
                        Dim Width, Height, DelayMS, Limits() As Integer
                        GetMazeInfo(Width, Height, DelayMS, Limits)
                        Dim AvailablePath As List(Of Node) = HuntAndKill(Limits, DelayMS)
                        If AvailablePath IsNot Nothing Then
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Console.CursorTop
                            DisplayAvailablePositions(AvailablePath.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 2)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 2)
                            PreviousMaze.Clear()
                            For Each node In AvailablePath
                                PreviousMaze.Add(New Node(node.X, node.Y))
                            Next
                            If input = "astar" Then
                                AStar(AvailablePath)
                            Else
                                OptionNotReady()
                            End If
                        End If
                    ElseIf y = 2 Then
                        Dim Width, Height, DelayMS, Limits() As Integer
                        GetMazeInfo(Width, Height, DelayMS, Limits)
                        Dim AvailablePath As List(Of Node) = Prims(Limits, DelayMS)
                        If AvailablePath IsNot Nothing Then
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Console.CursorTop
                            DisplayAvailablePositions(AvailablePath.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 2)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 2)
                            PreviousMaze.Clear()
                            For Each node In AvailablePath
                                PreviousMaze.Add(New Node(node.X, node.Y))
                            Next
                            If input = "astar" Then
                                AStar(AvailablePath)
                            Else
                                OptionNotReady()
                            End If
                        End If
                    ElseIf y = 3 Then
                        Dim Width, Height, DelayMS, Limits() As Integer
                        GetMazeInfo(Width, Height, DelayMS, Limits)
                        Dim AvailablePath As List(Of Node) = AldousBroder(Limits, DelayMS)
                        If AvailablePath IsNot Nothing Then
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Console.CursorTop
                            DisplayAvailablePositions(AvailablePath.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 2)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 2)
                            PreviousMaze.Clear()
                            For Each node In AvailablePath
                                PreviousMaze.Add(New Node(node.X, node.Y))
                            Next
                            If input = "astar" Then
                                AStar(AvailablePath)
                            Else
                                OptionNotReady()
                            End If
                        End If
                    ElseIf y = 4 Then
                        Dim Width, Height, DelayMS, Limits() As Integer
                        GetMazeInfo(Width, Height, DelayMS, Limits)
                        Dim ArrOptions() As String = {"Newest (Recursive Backtracker)", "Random (Prim's)", "Newest/Random, 75/25 split", "Newest/Random, 50/50 split", "Newest/Random, 25/75 split", "Oldest", "Middle", "Newest/Oldest, 50/50 split", "Oldest/Random, 50/50 split"}
                        Dim CellSelectionMethod() As Integer = GrowingTreeMenu(ArrOptions)
                        Dim AvailablePath As List(Of Node) = GrowingTree(Limits, DelayMS, CellSelectionMethod)
                        If AvailablePath IsNot Nothing Then
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Console.CursorTop
                            DisplayAvailablePositions(AvailablePath.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 2)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 2)
                            PreviousMaze.Clear()
                            For Each node In AvailablePath
                                PreviousMaze.Add(New Node(node.X, node.Y))
                            Next
                            If input = "astar" Then
                                AStar(AvailablePath)
                            Else
                                OptionNotReady()
                            End If
                        End If
                    ElseIf y = 5 Then
                        Dim Width, Height, DelayMS, Limits() As Integer
                        GetMazeInfo(Width, Height, DelayMS, Limits)
                        Dim AvailablePath As List(Of Node) = Custom(Limits, DelayMS)
                        If AvailablePath IsNot Nothing Then
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Console.CursorTop
                            DisplayAvailablePositions(AvailablePath.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 2)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 2)
                            PreviousMaze.Clear()
                            For Each node In AvailablePath
                                PreviousMaze.Add(New Node(node.X, node.Y))
                            Next
                            If input = "astar" Then
                                AStar(AvailablePath)

                            Else
                                OptionNotReady()
                            End If
                        End If
                    ElseIf y = 8 Then
                        'Load previous maze
                        If PreviousMaze.Count > 1 Then
                            Console.Clear()
                            SetBoth(ConsoleColor.White)
                            For Each node In PreviousMaze
                                node.Print("██")
                            Next
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Console.CursorTop
                            DisplayAvailablePositions(PreviousMaze.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 2)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 2)
                            If input = "astar" Then
                                AStar(PreviousMaze)
                            Else
                                OptionNotReady()
                            End If
                        Else
                            Console.Clear()
                            MsgColour("No previous maze available", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    ElseIf y = 9 Then
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
                        Console.ReadKey()
                    ElseIf y = 10 Then
                        Dim ValidMaze, XMax, YMax As Integer
                        ValidMaze = 1
                        XMax = Console.WindowWidth - 6
                        YMax = Console.WindowHeight - 6
                        LoadedMaze.Clear()
                        Console.Clear()
                        Dim _x, _y As Integer
                        Console.Write("File Name of the maze to load (don't include .txt): ")
                        Dim filename As String = Console.ReadLine
                        filename += ".txt"
                        If System.IO.File.Exists(filename) Then
                            Dim c As Integer = 0
                            Console.Clear()
                            Using reader As StreamReader = New StreamReader(filename)
                                Do Until reader.EndOfStream
                                    If c = 0 Then
                                        _x = Int(reader.ReadLine)
                                        If _x > XMax Then
                                            ValidMaze = 0
                                            Exit Do
                                        End If
                                    ElseIf c = 1 Then
                                        _y = Int(reader.ReadLine)
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
                            If ValidMaze = 1 Then
                                MsgColour($"Finished loading maze positions, total maze positions: {LoadedMaze.Count}", ConsoleColor.Green)
                                Console.ReadKey()
                                Console.Clear()
                                For Each node In LoadedMaze
                                    node.Print("██")
                                Next
                                Dim YPosAfterMaze As Integer = Console.CursorTop
                                DisplayAvailablePositions(LoadedMaze.Count)
                                Console.SetCursorPosition(0, YPosAfterMaze + 2)
                                Dim input As String = SolvingMenu(YPosAfterMaze + 2)
                                PreviousMaze.Clear()
                                For Each node In LoadedMaze
                                    PreviousMaze.Add(New Node(node.X, node.Y))
                                Next
                                If input = "astar" Then
                                    AStar(LoadedMaze)
                                Else
                                    OptionNotReady()
                                End If
                            Else
                                Console.Clear()
                                MsgColour("Maze is too big for the screen, please decrease font size and try again", ConsoleColor.Red)
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
            For i = 0 To arr.Count - 1
                Console.SetCursorPosition(0, i + 1 + CurrentCol)
                Console.Write("                                      ")
            Next
            For Each MenuOption In arr
                Console.SetCursorPosition(0, Count + CurrentCol)
                Console.Write($" {MenuOption}")
                Count += 1
            Next
            Console.SetCursorPosition(0, y + 1)
            MsgColour($"> {arr(y)}", ConsoleColor.Green)
        End While
    End Sub
    Sub OptionNotReady()
        Console.Clear()
        Console.WriteLine("Option not Ready Yet")
        Console.ReadKey()
        Console.Clear()
    End Sub
    Function SolvingMenu(ByVal ColumnPosition As Integer)
        SetColour(ConsoleColor.White)
        Dim AStarOption As Boolean = True
        Dim x, y As Integer
        y = ColumnPosition
        Console.SetCursorPosition(x, y)
        Console.Write("Do you want to use ")
        MsgColour("> A*", ConsoleColor.Green)
        Console.SetCursorPosition(23, y)
        Console.Write(" Or Dijkstras to solve the maze")
        While 1
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "RightArrow"
                    If AStarOption Then AStarOption = False
                Case "LeftArrow"
                    If Not AStarOption Then AStarOption = True
                Case "Enter"
                    If AStarOption Then
                        Return "astar"
                    Else
                        Return "dijkstras"
                    End If
            End Select
            Console.SetCursorPosition(0, y)
            Console.Write("Do you want to use A* Or Dijkstras to solve the maze")
            If AStarOption Then
                Console.SetCursorPosition(19, y)
                MsgColour("> A*", ConsoleColor.Green)
                Console.SetCursorPosition(23, y)
                Console.Write(" Or Dijkstras to solve the maze")
            Else
                Console.SetCursorPosition(25, y)
                Console.Write("                             ")
                Console.SetCursorPosition(25, y)
                MsgColour("> Dijkstras", ConsoleColor.Green)
                Console.SetCursorPosition(37, y)
                Console.Write("to solve the maze")
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
    Sub AStar(ByVal availablepath As List(Of Node))
        Dim startamount As Integer = availablepath.Count - 1
        Dim ExtraPath As List(Of Node) = availablepath
        SetBoth(ConsoleColor.White)
        Dim start As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim target As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        SetBoth(ConsoleColor.Red)
        target.Print("██")
        Dim pathfound As Boolean = False
        SetBoth(ConsoleColor.Green)
        start.Print("██")
        Dim openSet, closedSet, mazepath As New List(Of Node)
        Dim current As New Node(start.X, start.Y)
        Dim grid As New nGrid
        openSet.Add(current)
        SetBoth(ConsoleColor.Yellow)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While openSet.Count > 0
            If ExitCase() Then
                pathfound = True
                Exit While
            End If
            current = openSet(0)
            For i = 1 To openSet.Count - 1
                If openSet(i).fCost() <= current.fCost() Or openSet(i).hCost = current.hCost Then
                    If openSet(i).hCost < current.hCost Then current = openSet(i)
                End If
            Next 'finding node with the lowest fcost in the openset
            openSet.Remove(current)
            closedSet.Add(current)
            SetBoth(ConsoleColor.Red)
            If availablepath.Contains(closedSet(closedSet.Count - 1)) Then
                closedSet(closedSet.Count - 1).Print("██")
            End If
            SetBoth(ConsoleColor.Yellow)
            If current.X = target.X And current.Y = target.Y Then
                Dim Time As String = $"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds"
                RetracePath(start, current, Time)
                pathfound = True
                Console.ReadKey()
                Exit While
            End If
            For Each neighbour As Node In grid.GetNeighbours(current, availablepath, False)
                If Not neighbour.Walkable Or closedSet.Contains(neighbour) Then Continue For
                Dim newmovementcost As Single = current.gCost + GetDistance(current, neighbour)
                If newmovementcost < neighbour.gCost Or Not openSet.Contains(neighbour) Then
                    neighbour.gCost = newmovementcost
                    neighbour.hCost = GetDistance(neighbour, target)
                    neighbour.parent = current
                    If availablepath.Contains(neighbour) Then
                        neighbour.Print("██")
                    End If
                    If Not openSet.Contains(neighbour) Then
                        openSet.Add(neighbour)
                    End If
                End If
            Next
            ExtraPath.Remove(current)
            CurrentExploredPercent(ExtraPath.Count - 1, startamount)
        End While
        If pathfound = False Then
            Dim mess As String = "No Path Availible"
            SetColour(ConsoleColor.Red)
            SetBackGroundColour(ConsoleColor.Black)
            Console.SetCursorPosition((Console.WindowWidth / 2) - (mess.Count / 2), 0)
            Console.Write(mess)
            Console.ReadKey()
        End If
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
        While Not (current.X = startnode.X And current.Y = startnode.Y)
            path.Add(current)
            current = current.parent
        End While
        Dim mess As String = ($"Path length:{path.Count - 1}   {timetaken}")
        SetColour(ConsoleColor.Yellow)
        SetBackGroundColour(ConsoleColor.Black)
        Console.SetCursorPosition(Console.WindowWidth / 2 - mess.Count / 2, Console.WindowHeight - 1)
        Console.Write(mess)
        SetBoth(ConsoleColor.Green)
        startnode.Print("██")
        path.Reverse()
        For Each node In path
            node.Print("██")
            Threading.Thread.Sleep(2)
        Next
        SetBackGroundColour(ConsoleColor.Black)
    End Sub
    Function GrowingTree(ByVal Limits() As Integer, ByVal delay As Integer, ByVal CellSelectionMethod() As Integer)
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim grid As New nGrid
        Dim VisistedList, FrontierSet, RecentFrontierSet As New List(Of Cell)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim PreviousCell As Cell = CurrentCell
        Dim WallCell As Cell
        Dim Index As Integer
        Dim ReturnablePath As New List(Of Node)
        VisistedList.Add(CurrentCell)
        CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        While True
            If ExitCase() Then Return Nothing
            SetBoth(ConsoleColor.Yellow)
            For Each cell As Cell In NeighbourGrowingTree(CurrentCell, VisistedList, FrontierSet, Limits, False)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
                RecentFrontierSet.Add(cell)
                cell.Print("██")
            Next
            Dim RandomNumber As Integer
            If CellSelectionMethod(0) = 1 Then
                'Recursive Backtracker
                If RecentFrontierSet.Count > 0 Then
                    Index = R.Next(0, RecentFrontierSet.Count)
                    CurrentCell = RecentFrontierSet(Index)
                    SetBoth(ConsoleColor.Red)
                    CurrentCell.Print("██")
                Else
                    If FrontierSet.Count = 0 Then Exit While
                    Index = FrontierSet.Count - 1
                    CurrentCell = FrontierSet(Index)
                    SetBoth(ConsoleColor.Red)
                    CurrentCell.Print("██")
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisistedList)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            ElseIf CellSelectionMethod(1) = 1 Then
                'Prim's
                If FrontierSet.Count = 0 Then Exit While
                Index = R.Next(0, FrontierSet.Count)
                CurrentCell = FrontierSet(Index)
                SetBoth(ConsoleColor.Red)
                CurrentCell.Print("██")
                Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisistedList)
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
                        SetBoth(ConsoleColor.Red)
                        CurrentCell.Print("██")
                    Else
                        If FrontierSet.Count = 0 Then Exit While
                        Index = R.Next(0, FrontierSet.Count)
                        CurrentCell = FrontierSet(Index)
                        SetBoth(ConsoleColor.Red)
                        CurrentCell.Print("██")
                        Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisistedList)
                        PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                    End If
                Else
                    'Random
                    If FrontierSet.Count = 0 Then Exit While
                    Index = R.Next(0, FrontierSet.Count)
                    CurrentCell = FrontierSet(Index)
                    SetBoth(ConsoleColor.Red)
                    CurrentCell.Print("██")
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisistedList)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            ElseIf CellSelectionMethod(5) = 1 Then
                'Oldest
                If FrontierSet.Count = 0 Then Exit While
                Index = 0
                CurrentCell = FrontierSet(Index)
                SetBoth(ConsoleColor.Red)
                CurrentCell.Print("██")
                Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisistedList)
                PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
            ElseIf CellSelectionMethod(6) = 1 Then
                'Middle
                If FrontierSet.Count = 0 Then Exit While
                Index = FrontierSet.Count / 2
                CurrentCell = FrontierSet(Index)
                SetBoth(ConsoleColor.Red)
                CurrentCell.Print("██")
                Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisistedList)
                PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
            ElseIf CellSelectionMethod(7) = 1 Then
                'Newest/Oldest, 50/50 split
                RandomNumber = R.Next(1, 101)
                If RandomNumber > 50 Then
                    'Newest
                    If RecentFrontierSet.Count > 0 Then
                        Index = R.Next(0, RecentFrontierSet.Count)
                        CurrentCell = RecentFrontierSet(Index)
                        SetBoth(ConsoleColor.Red)
                        CurrentCell.Print("██")
                    Else
                        If FrontierSet.Count = 0 Then Exit While
                        Index = R.Next(0, FrontierSet.Count)
                        CurrentCell = FrontierSet(Index)
                        SetBoth(ConsoleColor.Red)
                        CurrentCell.Print("██")
                        Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisistedList)
                        PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                    End If
                Else
                    'Oldest
                    If FrontierSet.Count = 0 Then Exit While
                    Index = 0 'R.Next(0, FrontierSet.Count)
                    CurrentCell = FrontierSet(Index)
                    SetBoth(ConsoleColor.Red)
                    CurrentCell.Print("██")
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisistedList)
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
                    SetBoth(ConsoleColor.Red)
                    CurrentCell.Print("██")
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisistedList)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                Else
                    'Random
                    If FrontierSet.Count = 0 Then Exit While
                    Index = R.Next(0, FrontierSet.Count)
                    CurrentCell = FrontierSet(Index)
                    SetBoth(ConsoleColor.Red)
                    CurrentCell.Print("██")
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisistedList)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            End If
            WallCell = MidPoint(CurrentCell, PreviousCell)
            SetBoth(ConsoleColor.White)
            WallCell.Print("██")
            CurrentCell.Print("██")
            VisistedList.Add(CurrentCell)
            FrontierSet.Remove(CurrentCell)
            ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
            ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
            Threading.Thread.Sleep(delay)
            RecentFrontierSet.Clear()
            PreviousCell = CurrentCell
        End While
        ReturnablePath.Add(New Node(Limits(0) + 3, Limits(1) - 1))
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Dim testnode As New Cell(Limits(2) - 3, Limits(3))
        If VisistedList.Contains(testnode) Then
            ReturnablePath.Add(New Node(Limits(2) - 3, Limits(3) + 1))
        Else
            ReturnablePath.Add(New Node(Limits(2) - 1, Limits(3) + 1))
        End If
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Return ReturnablePath
    End Function
    Function Custom(ByVal Limits() As Integer, ByVal delay As Integer)
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim grid As New nGrid
        Dim VisistedList, FrontierSet, RecentFrontierSet As New List(Of Cell)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim WallCell As Cell
        Dim Index As Integer
        Dim ReturnablePath As New List(Of Node)
        VisistedList.Add(CurrentCell)
        CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        While True
            If ExitCase() Then Return Nothing
            SetBoth(ConsoleColor.Yellow)
            For Each cell As Cell In NeighbourPrims(CurrentCell, VisistedList, FrontierSet, Limits, False)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
                'End If
                RecentFrontierSet.Add(cell)
                cell.Print("██")
                'Exit For
            Next
            If RecentFrontierSet.Count > 0 Then
                Index = R.Next(0, RecentFrontierSet.Count)
                CurrentCell = RecentFrontierSet(Index)
                SetBoth(ConsoleColor.Red)
                CurrentCell.Print("██")
            Else
                If FrontierSet.Count = 0 Then Exit While
                Index = FrontierSet.Count - 1 'R.Next(0, FrontierSet.Count)
                CurrentCell = FrontierSet(Index)
                SetBoth(ConsoleColor.Red)
                CurrentCell.Print("██")
            End If
            Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisistedList)
            Dim PreviousCell As Cell = PickAdjancentCell(CurrentCell, AdjancencyList)
            WallCell = MidPoint(CurrentCell, PreviousCell)
            SetBoth(ConsoleColor.White)
            WallCell.Print("██")
            CurrentCell.Print("██")
            VisistedList.Add(CurrentCell)
            FrontierSet.Remove(CurrentCell)
            ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
            ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
            Threading.Thread.Sleep(delay)
            RecentFrontierSet.Clear()
        End While
        ReturnablePath.Add(New Node(Limits(0) + 3, Limits(1) - 1))
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Dim testnode As New Cell(Limits(2) - 3, Limits(3))
        If VisistedList.Contains(testnode) Then
            ReturnablePath.Add(New Node(Limits(2) - 3, Limits(3) + 1))
        Else
            ReturnablePath.Add(New Node(Limits(2) - 1, Limits(3) + 1))
        End If
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Return ReturnablePath
    End Function
    Function AldousBroder(ByVal Limits() As Integer, ByVal delay As Integer)
        Dim TotalCellCount As Integer
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim grid As New nGrid
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
        While VisitedList.Count <> TotalCellCount
            If ExitCase() Then Return Nothing
            SetBoth(ConsoleColor.White)
            RecentCells.Clear()
            For Each cell As Cell In RanNeighbour(CurrentCell, Limits, False)
                RecentCells.Add(cell)
            Next
            Dim Index As Integer = R.Next(0, RecentCells.Count)
            Dim TemporaryCell As Cell = RecentCells(Index)
            Dim TempNodeCell As New Node(TemporaryCell.X, TemporaryCell.Y)
            If Not ReturnablePath.Contains(TempNodeCell) Then ReturnablePath.Add(New Node(TemporaryCell.X, TemporaryCell.Y))
            If Not VisitedList.Contains(TemporaryCell) Then
                VisitedList.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                WallCell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                SetBoth(ConsoleColor.White)
                PrevCell.Print("██")
                WallCell.Print("██")
                SetBoth(ConsoleColor.Blue)
                TemporaryCell.Print("██")
                PrevCell = CurrentCell
            Else
                CurrentCell = TemporaryCell
                SetBoth(ConsoleColor.White)
                PrevCell.Print("██")
                SetBoth(ConsoleColor.Blue)
                TemporaryCell.Print("██")
                PrevCell = CurrentCell
            End If
            Threading.Thread.Sleep(delay)
        End While
        SetBoth(ConsoleColor.White)
        PrevCell.Print("██")
        ReturnablePath.Add(New Node(Limits(0) + 3, Limits(1) - 1))
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Dim testnode As New Cell(Limits(2) - 3, Limits(3))
        If VisitedList.Contains(testnode) Then
            ReturnablePath.Add(New Node(Limits(2) - 3, Limits(3) + 1))
        Else
            ReturnablePath.Add(New Node(Limits(2) - 1, Limits(3) + 1))
        End If
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        SetColour(ConsoleColor.White)
        SetBackGroundColour(ConsoleColor.Black)
        Return ReturnablePath
    End Function
    Function Prims(ByVal Limits() As Integer, ByVal delay As Integer)
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim grid As New nGrid
        Dim VisistedList, FrontierSet As New List(Of Cell)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim WallCell As Cell
        Dim ReturnablePath As New List(Of Node)
        VisistedList.Add(CurrentCell)
        CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        While True
            If ExitCase() Then Return Nothing
            SetBoth(ConsoleColor.Yellow)
            For Each cell As Cell In NeighbourPrims(CurrentCell, VisistedList, FrontierSet, Limits, False)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
                cell.Print("██")
            Next
            If FrontierSet.Count = 0 Then Exit While
            Dim Index As Integer = R.Next(0, FrontierSet.Count)
            CurrentCell = FrontierSet(Index)
            Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisistedList)
            Dim PreviousCell As Cell = PickAdjancentCell(CurrentCell, AdjancencyList)
            WallCell = MidPoint(CurrentCell, PreviousCell)
            SetBoth(ConsoleColor.White)
            WallCell.Print("██")
            CurrentCell.Print("██")
            VisistedList.Add(CurrentCell)
            FrontierSet.Remove(CurrentCell)
            ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
            ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
            Threading.Thread.Sleep(delay)
        End While
        ReturnablePath.Add(New Node(Limits(0) + 3, Limits(1) - 1))
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Dim testnode As New Cell(Limits(2) - 3, Limits(3))
        If VisistedList.Contains(testnode) Then
            ReturnablePath.Add(New Node(Limits(2) - 3, Limits(3) + 1))
        Else
            ReturnablePath.Add(New Node(Limits(2) - 1, Limits(3) + 1))
        End If
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Return ReturnablePath
    End Function
    Function HuntAndKill(ByVal Limits() As Integer, ByVal delay As Integer)
        SetBackGroundColour(ConsoleColor.White)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim r As New Random
        Dim VisitedList, Stack, VisitedListAndWall, RecentCells As New List(Of Cell)
        Dim ReturnablePath As New List(Of Node)
        Dim Width As Integer = Limits(2) - Limits(0)
        Dim Height As Integer = Limits(3) - Limits(1)
        Dim xCount As Integer
        While True
            If ExitCase() Then Return Nothing
            SetBackGroundColour(ConsoleColor.White)
            If IsNothing(CurrentCell) Then Exit While
            SetBoth(ConsoleColor.White)
            If Neighbour(CurrentCell, VisitedList, Limits, False) Then
                RecentCells.Clear()
                For Each cell As Cell In Neighbour(CurrentCell, VisitedList, Limits, True)
                    RecentCells.Add(cell)
                Next
                Dim Index As Integer = r.Next(0, RecentCells.Count)
                Dim TemporaryCell As Cell = RecentCells(Index)
                ReturnablePath.Add(New Node(TemporaryCell.X, TemporaryCell.Y))
                VisitedListAndWall.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                VisitedList.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                VisitedListAndWall.Add(New Cell(WallCell.X, WallCell.Y))
                TemporaryCell.Print("██")
                WallCell.Print("██")
            ElseIf CurrentCell Is Nothing Then
                Exit While
            Else
                Dim list As New List(Of Cell)
                Dim ContinueFor As Boolean = True
                For y = Limits(1) To Limits(3) Step 2
                    xCount = 0
                    For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                        Dim newcell As New Cell(x, y)
                        SetColour(ConsoleColor.Blue)
                        Console.SetCursorPosition(x, y)
                        Console.Write("██")
                        Console.SetCursorPosition(x + 2, y)
                        If Console.CursorLeft < Limits(2) - 1 Then Console.Write("██")
                        SetBoth(ConsoleColor.White)
                        Dim AdjancencyList As Integer() = AdjacentCheck(newcell, VisitedList)
                        CurrentCell = PickAdjancentCell(newcell, AdjancencyList)
                        If CurrentCell IsNot Nothing Then
                            Dim WallCell As Cell = MidPoint(newcell, CurrentCell)
                            ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                            ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
                            WallCell.Print("██")
                            CurrentCell.Print("██")
                            ContinueFor = False
                            xCount = x
                            Exit For
                        End If
                        xCount = x
                    Next
                    If ContinueFor = False Then Exit For
                    For i = Limits(0) + 3 To xCount Step 2
                        Dim tempcell As New Cell(i, y)
                        If Not VisitedListAndWall.Contains(tempcell) Then
                            SetBoth(ConsoleColor.Black)
                            tempcell.Print("  ")
                        Else
                            SetBoth(ConsoleColor.White)
                            tempcell.Print("██")
                        End If
                    Next
                Next
            End If
            Threading.Thread.Sleep(delay)
        End While
        ReturnablePath.Add(New Node(Limits(0) + 3, Limits(1) - 1))
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Dim testnode As New Cell(Limits(2) - 3, Limits(3))
        If VisitedList.Contains(testnode) Then
            ReturnablePath.Add(New Node(Limits(2) - 3, Limits(3) + 1))
        Else
            ReturnablePath.Add(New Node(Limits(2) - 1, Limits(3) + 1))
        End If
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Return ReturnablePath
    End Function
    Function RecursiveBacktracker(ByVal Limits() As Integer, ByVal Delay As Integer)
        Dim r As New Random
        SetBoth(ConsoleColor.White)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim PrevCell, WallPrev As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim VisitedList, Stack As New List(Of Cell)
        Dim ReturnablePath As New List(Of Node)
        Dim RecentCells As New List(Of Cell)
        While True
            If ExitCase() Then Return Nothing
            SetBoth(ConsoleColor.White)
            PrevCell.Print("██")
            If Neighbour(CurrentCell, VisitedList, Limits, False) Then
                RecentCells.Clear()
                For Each cell As Cell In Neighbour(CurrentCell, VisitedList, Limits, True)
                    RecentCells.Add(cell)
                Next
                Dim Index As Integer = r.Next(0, RecentCells.Count)
                Dim TemporaryCell As Cell = RecentCells(Index)
                ReturnablePath.Add(New Node(TemporaryCell.X, TemporaryCell.Y))
                VisitedList.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                Stack.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                SetBoth(ConsoleColor.White)
                PrevCell.Print("██")
                WallCell.Print("██")
                SetBoth(ConsoleColor.Blue)
                TemporaryCell.Print("██")
                PrevCell = CurrentCell
                WallPrev = WallCell
            ElseIf Stack.Count > 1 Then
                CurrentCell = CurrentCell.Pop(Stack)
                SetBoth(ConsoleColor.White)
                PrevCell.Print("██")
                SetBoth(ConsoleColor.Blue)
                CurrentCell.Print("██")
                SetBoth(ConsoleColor.White)
                PrevCell = CurrentCell
            Else
                Exit While
            End If
            Threading.Thread.Sleep(Delay)
        End While
        SetBoth(ConsoleColor.White)
        PrevCell.Print("██")
        WallPrev.Print("██")
        ReturnablePath.Add(New Node(Limits(0) + 3, Limits(1) - 1))
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Dim testnode As New Cell(Limits(2) - 3, Limits(3))
        If VisitedList.Contains(testnode) Then
            ReturnablePath.Add(New Node(Limits(2) - 3, Limits(3) + 1))
        Else
            ReturnablePath.Add(New Node(Limits(2) - 1, Limits(3) + 1))
        End If
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Return ReturnablePath
    End Function
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
        'up
        If adjancencylist(0) = 1 Then
            Neighbours.Add(New Cell(cell.X, cell.Y - 1 * 2))
        End If
        'right
        If adjancencylist(1) = 1 Then
            Neighbours.Add(New Cell(cell.X + 1 * 4, cell.Y))
        End If
        'down
        If adjancencylist(2) = 1 Then
            Neighbours.Add(New Cell(cell.X, cell.Y + 1 * 2))
        End If
        'left
        If adjancencylist(3) = 1 Then
            Neighbours.Add(New Cell(cell.X - 1 * 4, cell.Y))
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
    Function NeighbourGrowingTree(ByVal current As Cell, ByVal visited As List(Of Cell), ByVal frontier As List(Of Cell), ByVal Limits() As Integer, ByVal bool As Boolean)
        Dim neighbours As New List(Of Cell)
        'left
        Dim newPoint As New Cell(current.X - 4, current.Y)
        If newPoint.X >= Limits(0) And newPoint.X <= Limits(2) And newPoint.Y >= Limits(1) And newPoint.Y <= Limits(3) Then
            If Not visited.Contains(newPoint) Then
                neighbours.Add(New Cell(newPoint.X, newPoint.Y))
            End If
        End If
        'up
        newPoint.Update(current.X, current.Y - 2)
        If newPoint.X >= Limits(0) And newPoint.X <= Limits(2) And newPoint.Y >= Limits(1) And newPoint.Y <= Limits(3) Then
            If Not visited.Contains(newPoint) Then
                neighbours.Add(New Cell(newPoint.X, newPoint.Y))
            End If
        End If
        'down
        newPoint.Update(current.X, current.Y + 2)
        If newPoint.X >= Limits(0) And newPoint.X <= Limits(2) And newPoint.Y >= Limits(1) And newPoint.Y <= Limits(3) Then
            If Not visited.Contains(newPoint) Then
                neighbours.Add(New Cell(newPoint.X, newPoint.Y))
            End If
        End If
        'right
        newPoint.Update(current.X + 4, current.Y)
        If newPoint.X >= Limits(0) And newPoint.X <= Limits(2) And newPoint.Y >= Limits(1) And newPoint.Y <= Limits(3) Then
            If Not visited.Contains(newPoint) Then
                neighbours.Add(New Cell(newPoint.X, newPoint.Y))
            End If
        End If
        Dim r As New Random
        If bool Then Return neighbours(r.Next(0, neighbours.Count))
        Return neighbours
    End Function
    Function NeighbourPrims(ByVal current As Cell, ByVal visited As List(Of Cell), ByVal frontier As List(Of Cell), ByVal Limits() As Integer, ByVal bool As Boolean)
        Dim neighbours As New List(Of Cell)
        'left
        Dim newPoint As New Cell(current.X - 4, current.Y)
        If newPoint.X >= Limits(0) And newPoint.X <= Limits(2) And newPoint.Y >= Limits(1) And newPoint.Y <= Limits(3) Then
            If Not visited.Contains(newPoint) And Not frontier.Contains(newPoint) Then
                neighbours.Add(New Cell(newPoint.X, newPoint.Y))
            End If
        End If
        'up
        newPoint.Update(current.X, current.Y - 2)
        If newPoint.X >= Limits(0) And newPoint.X <= Limits(2) And newPoint.Y >= Limits(1) And newPoint.Y <= Limits(3) Then
            If Not visited.Contains(newPoint) And Not frontier.Contains(newPoint) Then
                neighbours.Add(New Cell(newPoint.X, newPoint.Y))
            End If
        End If
        'down
        newPoint.Update(current.X, current.Y + 2)
        If newPoint.X >= Limits(0) And newPoint.X <= Limits(2) And newPoint.Y >= Limits(1) And newPoint.Y <= Limits(3) Then
            If Not visited.Contains(newPoint) And Not frontier.Contains(newPoint) Then
                neighbours.Add(New Cell(newPoint.X, newPoint.Y))
            End If
        End If
        'right
        newPoint.Update(current.X + 4, current.Y)
        If newPoint.X >= Limits(0) And newPoint.X <= Limits(2) And newPoint.Y >= Limits(1) And newPoint.Y <= Limits(3) Then
            If Not visited.Contains(newPoint) And Not frontier.Contains(newPoint) Then
                neighbours.Add(New Cell(newPoint.X, newPoint.Y))
            End If
        End If
        Dim r As New Random
        If bool Then Return neighbours(r.Next(0, neighbours.Count))
        Return neighbours
    End Function
    Function RanNeighbour(ByVal current As Cell, ByVal Limits() As Integer, ByVal bool As Boolean)
        Dim neighbours As New List(Of Cell)
        'left
        Dim checkx As Integer = current.X - 4
        Dim checky As Integer = current.Y
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            neighbours.Add(New Cell(checkx, checky))
        End If
        'up
        checkx = current.X
        checky = current.Y - 2
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            neighbours.Add(New Cell(checkx, checky))
        End If
        'down
        checkx = current.X
        checky = current.Y + 2
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            neighbours.Add(New Cell(checkx, checky))
        End If
        'right
        checkx = current.X + 4
        checky = current.Y
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            neighbours.Add(New Cell(checkx, checky))
        End If
        Dim r As New Random
        If bool Then Return neighbours(r.Next(0, neighbours.Count))
        Return neighbours
    End Function
    Function Neighbour(ByVal current As Cell, ByVal visited As List(Of Cell), ByVal Limits() As Integer, ByVal bool As Boolean)
        Dim neighbours As New List(Of Cell)
        'left
        Dim newPoint As New Cell(current.X - 4, current.Y)
        If newPoint.X >= Limits(0) And newPoint.X <= Limits(2) And newPoint.Y >= Limits(1) And newPoint.Y <= Limits(3) Then
            If Not visited.Contains(newPoint) Then
                neighbours.Add(New Cell(newPoint.X, newPoint.Y))
            End If
        End If
        'up
        newPoint.Update(current.X, current.Y - 2)
        If newPoint.X >= Limits(0) And newPoint.X <= Limits(2) And newPoint.Y >= Limits(1) And newPoint.Y <= Limits(3) Then
            If Not visited.Contains(newPoint) Then
                neighbours.Add(New Cell(newPoint.X, newPoint.Y))
            End If
        End If
        'down
        newPoint.Update(current.X, current.Y + 2)
        If newPoint.X >= Limits(0) And newPoint.X <= Limits(2) And newPoint.Y >= Limits(1) And newPoint.Y <= Limits(3) Then
            If Not visited.Contains(newPoint) Then
                neighbours.Add(New Cell(newPoint.X, newPoint.Y))
            End If
        End If
        'right
        newPoint.Update(current.X + 4, current.Y)
        If newPoint.X >= Limits(0) And newPoint.X <= Limits(2) And newPoint.Y >= Limits(1) And newPoint.Y <= Limits(3) Then
            If Not visited.Contains(newPoint) Then
                neighbours.Add(New Cell(newPoint.X, newPoint.Y))
            End If
        End If
        Dim r As New Random
        If bool Then
            'Return neighbours(r.Next(0, neighbours.Count))
            Return neighbours
        Else
            If neighbours.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Sub Check(ByVal neighbours As List(Of Node), ByVal current As Node, ByVal availablepath As List(Of Node), ByVal currentiteration As Integer)
        If availablepath.Contains(current) Then
            neighbours(currentiteration).Walkable = True
        Else
            neighbours(currentiteration).Walkable = False
        End If
    End Sub
    Function CheckAndAdd(ByRef current As Node, ByRef availablepath As List(Of Node))
        Dim neighbours As New List(Of Node) From {
            New Node(current.X, current.Y - 1), 'top
            New Node(current.X + 2, current.Y), 'right
            New Node(current.X, current.Y + 1), 'down
            New Node(current.X - 2, current.Y) 'left
            }
        For i = 0 To 3
            Check(neighbours, current, availablepath, i)
        Next
        Return neighbours
    End Function
End Module
Class Cell
    Private xcord, ycord As Integer
    Public Sub New(ByVal xpoint As Integer, ByVal ypoint As Integer)
        xcord = xpoint
        ycord = ypoint
    End Sub
    Public Function X()
        Return xcord
    End Function
    Public Function Y()
        Return ycord
    End Function
    Sub Update(ByVal x As Integer, ByVal y As Integer)
        xcord = x
        ycord = y
    End Sub
    Public Function Pop(ByVal list As List(Of Cell))
        Dim val As Cell
        val = list(list.Count - 1)
        list.RemoveAt(list.Count - 1)
        Return val
    End Function

    Public Sub Print(ByVal str As String)
        Console.SetCursorPosition(Me.X, Me.Y)
        Console.Write(str)
    End Sub
    Public Sub RemoveWhere(ByRef list As List(Of Cell))
        list.RemoveAt(list.FindIndex(Function(p) p.xcord = Me.X And p.ycord = Me.Y))
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim cell = TryCast(obj, Cell)
        Return cell IsNot Nothing AndAlso
               xcord = cell.xcord AndAlso
               ycord = cell.ycord
    End Function

    Public Overrides Function GetHashCode() As Integer
        Dim hashCode As Long = 1855483287
        hashCode = (hashCode * -1521134295 + xcord.GetHashCode()).GetHashCode()
        hashCode = (hashCode * -1521134295 + ycord.GetHashCode()).GetHashCode()
        Return hashCode
    End Function
End Class
Public Class nGrid
    Function GetNeighbours(ByVal current As Node, ByRef availablepath As List(Of Node), ByVal something As Boolean)
        Return CheckAndAdd(current, availablepath)
    End Function
End Class
Public Class Node
    Public xcord, ycord, gCost, hCost As Integer
    Public parent As Node
    Public Walkable As Boolean
    Public Sub Print(ByVal letter As String)
        Console.SetCursorPosition(Me.xcord, Me.ycord)
        Console.Write(letter)
    End Sub

    Public Sub New(ByVal xpoint As Integer, ByVal ypoint As Integer)
        xcord = xpoint
        ycord = ypoint
    End Sub
    Public Sub update(ByVal xpoint As Integer, ByVal ypoint As Integer)
        xcord = xpoint
        ycord = ypoint
    End Sub
    Public Function X()
        Return xcord
    End Function
    Public Function Y()
        Return ycord
    End Function
    Public Function fCost()
        Return gCost + hCost
    End Function
    Public Sub RemoveWhere(ByRef list As List(Of Node))
        list.RemoveAt(list.FindIndex(Function(p) p.xcord = Me.X And p.ycord = Me.Y))

    End Sub
    Public Overrides Function Equals(obj As Object) As Boolean
        Dim node = TryCast(obj, Node)
        Return node IsNot Nothing AndAlso
               xcord = node.xcord AndAlso
               ycord = node.ycord
    End Function

    Public Overrides Function GetHashCode() As Integer
        Dim hashCode As Long = 1855483287
        hashCode = (hashCode * -1521134295 + xcord.GetHashCode()).GetHashCode()
        hashCode = (hashCode * -1521134295 + ycord.GetHashCode()).GetHashCode()
        Return hashCode
    End Function
End Class