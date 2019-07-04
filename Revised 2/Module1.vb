Imports System.Drawing
Imports System.IO
Imports Revised_2
Module Module1
    'TODO: add input delay when solving
    Sub Main()
        Console.CursorVisible = False



        SetColour(ConsoleColor.White)
        Dim MenuOptions() As String = {"Recursive Backtracker Algorithm", "Hunt and Kill Algorithm", "Prim's Algorithm", "Aldous-Broder Algorithm", "Growing Tree Algorithm", "Custom Algorithm", "Sidewinder Algorithm", "Binary Tree Algorithm", "Eller's Algorithm", "Load the previously generated maze", "Save the previously generated maze", "Load a saved maze", "Exit"}
        Menu1(MenuOptions)
    End Sub
    Sub Playmaze(ByVal AvailablePath As List(Of Node))
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
                SetBoth(ConsoleColor.White)
            Else
                SetBoth(ConsoleColor.Black)
            End If
            Dim key = Console.ReadKey
            Select Case key.Key.ToString

                Case "RightArrow"
                    Dim tempNode3 As New Node(currentPos.X + 2, currentPos.Y)
                    If AvailablePath.Contains(tempNode3) Then
                        currentPos = tempNode3
                        SetColour(ConsoleColor.White)
                        PreviousPos.Print("██")
                        SetColour(ConsoleColor.Magenta)
                        currentPos.Print("██")
                        PreviousPos = currentPos
                        If Not playerPath.Contains(currentPos) Then playerPath.Add(currentPos)
                    End If
                Case "LeftArrow"
                    Dim tempNode2 As New Node(currentPos.X - 2, currentPos.Y)
                    If AvailablePath.Contains(tempNode2) Then
                        currentPos = tempNode2
                        SetColour(ConsoleColor.White)
                        PreviousPos.Print("██")
                        SetColour(ConsoleColor.Magenta)
                        currentPos.Print("██")
                        PreviousPos = currentPos
                        If Not playerPath.Contains(currentPos) Then playerPath.Add(currentPos)
                    End If
                Case "UpArrow"
                    Dim tempNode1 As New Node(currentPos.X, currentPos.Y - 1)
                    If AvailablePath.Contains(tempNode1) Then
                        currentPos = tempNode1
                        SetColour(ConsoleColor.White)
                        PreviousPos.Print("██")
                        SetColour(ConsoleColor.Magenta)
                        currentPos.Print("██")
                        PreviousPos = currentPos
                        If Not playerPath.Contains(currentPos) Then playerPath.Add(currentPos)
                    End If
                Case "DownArrow"
                    Dim tempNode As New Node(currentPos.X, currentPos.Y + 1)
                    If AvailablePath.Contains(tempNode) Then
                        currentPos = tempNode
                        SetColour(ConsoleColor.White)
                        PreviousPos.Print("██")
                        SetColour(ConsoleColor.Magenta)
                        currentPos.Print("██")
                        PreviousPos = currentPos
                        If Not playerPath.Contains(currentPos) Then playerPath.Add(currentPos)
                    End If
                Case "Enter"
                    Exit While
                Case Else
            End Select

        End While
        SetColour(ConsoleColor.Yellow)
        If currentPos.Equals(target) Then

            playerPath.Add(start)
            playerPath.Add(target)
            aStar(playerPath, False, False)
            SetBackGroundColour(ConsoleColor.Black)
            SetColour(ConsoleColor.Yellow)
            Console.Clear()
            Dim message As String = "EPIC VICTORY ROYALE"
            Console.SetCursorPosition(Console.WindowWidth / 2 - message.Length / 2, Console.WindowHeight / 2)
            For Each letter In message
                Console.Write(letter)
                Threading.Thread.Sleep(30)
            Next



            Console.ReadKey()
        End If
    End Sub
    Sub Dijkstras(ByVal availablepath As List(Of Node), ByVal ShowSolving As Boolean)
        Dim source As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim target As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim startamount As Integer = availablepath.Count - 1
        Dim ExtraPath As List(Of Node) = availablepath
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
        If ShowSolving Then
            SetBoth(ConsoleColor.Red)
            target.Print("██")
            SetBoth(ConsoleColor.Green)
            source.Print("██")
            SetBoth(ConsoleColor.Red)
        End If
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
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
        Dim mess As String = $"Time Taken to solve: {watch.Elapsed.TotalSeconds} seconds"
        SetColour(ConsoleColor.Yellow)
        SetBackGroundColour(ConsoleColor.Black)
        Console.SetCursorPosition(Console.WindowWidth / 2 - mess.Count / 2, Console.WindowHeight - 1)
        Console.Write(mess)
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
    Sub SetBackGroundColour(ByVal colour As ConsoleColor)
        Console.BackgroundColor = colour
    End Sub
    Sub SetColour(ByVal colour As ConsoleColor)
        Console.ForegroundColor = colour
    End Sub
    Sub GetMazeInfo(ByRef Width As Integer, ByRef Height As Integer, ByRef DelayMS As Integer, ByRef Limits() As Integer, ByRef ShowGeneration As Boolean)
        Console.Clear()
        ShowGeneration = HorizontalYesNo(0, "Do you want to see the maze being generated: ")
        Console.SetCursorPosition(0, Console.CursorTop + 1)
        If ShowGeneration Then
            DelayMS = GetIntInputArrowKeys("Delay when making the Maze (MS): ", 100, 0)
        Else
            DelayMS = 0
        End If
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
        Dim temparr() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0}
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
    End Function

    'Sub Menu(ByVal arr() As String)
    '    Dim PreviousMaze, LoadedMaze As New List(Of Node)
    '    Console.Clear()
    '    Dim CurrentCol As Integer = Console.CursorTop
    '    Dim y As Integer = Console.CursorTop
    '    Dim NumOfOptions As Integer = arr.Count
    '    MsgColour("What Maze Generation Algorithm do you want to use: ", ConsoleColor.Yellow)
    '    MsgColour($"> {arr(0)}", ConsoleColor.Green)
    '    For i = 1 To arr.Count - 1
    '        Console.WriteLine($" {arr(i)}")
    '    Next
    '    While 1
    '        SetBackGroundColour(ConsoleColor.Black)
    '        Dim key = Console.ReadKey
    '        Console.CursorVisible = False
    '        Select Case key.Key.ToString
    '            Case "DownArrow"
    '                y += 1
    '                If y = arr.Count Then y = 0
    '            Case "UpArrow"
    '                y -= 1
    '                If y = -1 Then y = arr.Count - 1
    '            Case "Enter"
    '                If y = 0 Then
    '                    Dim Width, Height, DelayMS, Limits() As Integer
    '                    'GetMazeInfo(Width, Height, DelayMS, Limits)
    '                    Dim AvailablePath As List(Of Node) = RecursiveBacktracker(Limits, DelayMS)
    '                    If AvailablePath IsNot Nothing Then
    '                        Dim YPosAfterMaze As Integer = Console.CursorTop
    '                        Dim Input As String = SolvingMenu(YPosAfterMaze + 2)
    '                        If Input = "astar" Then
    '                            aStar(AvailablePath, True, True)
    '                        ElseIf Input = "dijkstras" Then
    '                            'Dijkstras
    '                        ElseIf Input = "play" Then
    '                            'Play maze
    '                        ElseIf IsNothing(Input) Then

    '                        End If
    '                    End If
    '                ElseIf y = arr.Count - 1 Then
    '                    End
    '                Else
    '                    OptionNotReady()
    '                End If
    '                SetBackGroundColour(ConsoleColor.Black)
    '                Console.Clear()
    '                MsgColour("What Maze Generation Algorithm do you want to use: ", ConsoleColor.Yellow)
    '        End Select
    '        SetColour(ConsoleColor.White)
    '        Dim Count As Integer = 1
    '        For Each MenuOption In arr
    '            Console.SetCursorPosition(0, Count + CurrentCol)
    '            Console.Write($" {MenuOption}  ")
    '            Count += 1
    '        Next
    '        Console.SetCursorPosition(0, y + 1)
    '        MsgColour($"> {arr(y)}", ConsoleColor.Green)
    '    End While
    'End Sub



    Sub Menu1(ByVal arr() As String)
        Dim PreviousMaze, LoadedMaze As New List(Of Node)
        Dim Width, Height, DelayMS, Limits() As Integer
        Dim ShowMazeGeneration As Boolean
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
            Console.CursorVisible = False
            Select Case key.Key.ToString
                Case "DownArrow"
                    y += 1
                    If y = arr.Count Then y = 0
                Case "UpArrow"
                    y -= 1
                    If y = -1 Then y = arr.Count - 1
                Case "Enter"
                    If y = 0 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration)
                        Dim AvailablePath As List(Of Node) = RecursiveBacktracker(Limits, DelayMS, ShowMazeGeneration)
                        If AvailablePath IsNot Nothing Then
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Limits(3)
                            DisplayAvailablePositions(AvailablePath.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 3)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 3)
                            Dim ShowPath As Boolean = HorizontalYesNo(YPosAfterMaze + 3, "Do you want to show the steps in solving the maze: ")
                            PreviousMaze.Clear()
                            PreviousMaze = AvailablePath
                            If input = "astar" Then
                                aStar(AvailablePath, ShowPath, True)
                            ElseIf input = "dijkstras" Then
                                Dijkstras(AvailablePath, ShowPath)
                            ElseIf input = "play" Then
                                Playmaze(AvailablePath)
                            ElseIf IsNothing(input) Then

                            End If
                        End If
                    ElseIf y = 1 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration)
                        Dim AvailablePath As List(Of Node) = HuntAndKill(Limits, DelayMS, ShowMazeGeneration)
                        If AvailablePath IsNot Nothing Then
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Limits(3)
                            DisplayAvailablePositions(AvailablePath.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 3)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 3)
                            Dim ShowPath As Boolean = HorizontalYesNo(YPosAfterMaze + 3, "Do you want to show the steps in solving the maze: ")
                            PreviousMaze.Clear()
                            PreviousMaze = AvailablePath
                            If input = "astar" Then
                                aStar(AvailablePath, ShowPath, True)
                            ElseIf input = "dijkstras" Then
                                Dijkstras(AvailablePath, ShowPath)
                            ElseIf input = "play" Then
                                Playmaze(AvailablePath)
                            ElseIf IsNothing(input) Then

                            End If
                        End If
                    ElseIf y = 2 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration)
                        Dim AvailablePath As List(Of Node) = Prims(Limits, DelayMS, ShowMazeGeneration)
                        If AvailablePath IsNot Nothing Then
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Limits(3)
                            DisplayAvailablePositions(AvailablePath.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 3)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 3)
                            Dim ShowPath As Boolean = HorizontalYesNo(YPosAfterMaze + 3, "Do you want to show the steps in solving the maze: ")
                            PreviousMaze.Clear()
                            PreviousMaze = AvailablePath
                            If input = "astar" Then
                                aStar(AvailablePath, ShowPath, True)
                            ElseIf input = "dijkstras" Then
                                Dijkstras(AvailablePath, ShowPath)
                            ElseIf input = "play" Then
                                Playmaze(AvailablePath)
                            ElseIf IsNothing(input) Then

                            End If
                        End If
                    ElseIf y = 3 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration)
                        Dim AvailablePath As List(Of Node) = AldousBroder(Limits, DelayMS, ShowMazeGeneration)
                        If AvailablePath IsNot Nothing Then
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Limits(3)
                            DisplayAvailablePositions(AvailablePath.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 3)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 3)
                            Dim ShowPath As Boolean = HorizontalYesNo(YPosAfterMaze + 3, "Do you want to show the steps in solving the maze: ")
                            PreviousMaze.Clear()
                            PreviousMaze = AvailablePath
                            If input = "astar" Then
                                aStar(AvailablePath, ShowPath, True)
                            ElseIf input = "dijkstras" Then
                                Dijkstras(AvailablePath, ShowPath)
                            ElseIf input = "play" Then
                                Playmaze(AvailablePath)
                            ElseIf IsNothing(input) Then

                            End If
                        End If
                    ElseIf y = 4 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration)
                        Dim ArrOptions() As String = {"Newest (Recursive Backtracker)", "Random (Prim's)", "Newest/Random, 75/25 split", "Newest/Random, 50/50 split", "Newest/Random, 25/75 split", "Oldest", "Middle", "Newest/Oldest, 50/50 split", "Oldest/Random, 50/50 split"}
                        Dim CellSelectionMethod() As Integer = GrowingTreeMenu(ArrOptions)
                        Dim AvailablePath As List(Of Node) = GrowingTree(Limits, DelayMS, CellSelectionMethod, ShowMazeGeneration)
                        If AvailablePath IsNot Nothing Then
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Limits(3)
                            DisplayAvailablePositions(AvailablePath.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 3)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 3)
                            Dim ShowPath As Boolean = HorizontalYesNo(YPosAfterMaze + 3, "Do you want to show the steps in solving the maze: ")
                            PreviousMaze.Clear()
                            PreviousMaze = AvailablePath
                            If input = "astar" Then
                                aStar(AvailablePath, ShowPath, True)
                            ElseIf input = "dijkstras" Then
                                Dijkstras(AvailablePath, ShowPath)
                            ElseIf input = "play" Then
                                Playmaze(AvailablePath)
                            ElseIf IsNothing(input) Then

                            End If
                        End If
                    ElseIf y = 5 Then
                        GetMazeInfo(Width, Height, DelayMS, Limits, ShowMazeGeneration)
                        Dim AvailablePath As List(Of Node) = Custom(Limits, DelayMS, ShowMazeGeneration)
                        If AvailablePath IsNot Nothing Then
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Limits(3)
                            DisplayAvailablePositions(AvailablePath.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 3)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 3)
                            Dim ShowPath As Boolean = HorizontalYesNo(YPosAfterMaze + 3, "Do you want to show the steps in solving the maze: ")
                            PreviousMaze.Clear()
                            PreviousMaze = AvailablePath
                            If input = "astar" Then
                                aStar(AvailablePath, ShowPath, True)
                            ElseIf input = "dijkstras" Then
                                Dijkstras(AvailablePath, ShowPath)
                            ElseIf input = "play" Then
                                Playmaze(AvailablePath)
                            ElseIf IsNothing(input) Then

                            End If
                        End If
                    ElseIf y = 9 Then
                        'Load previous maze
                        If PreviousMaze.Count > 1 Then
                            Console.Clear()
                            SetBoth(ConsoleColor.White)
                            For Each node In PreviousMaze
                                node.Print("██")
                            Next
                            SetBackGroundColour(ConsoleColor.Black)
                            Dim YPosAfterMaze As Integer = Limits(3)
                            DisplayAvailablePositions(PreviousMaze.Count)
                            Console.SetCursorPosition(0, YPosAfterMaze + 3)
                            Dim input As String = SolvingMenu(YPosAfterMaze + 3)
                            Dim ShowPath As Boolean = HorizontalYesNo(YPosAfterMaze + 3, "Do you want to show the steps in solving the maze: ")
                            If input = "astar" Then
                                aStar(PreviousMaze, ShowPath, True)
                            ElseIf input = "dijkstras" Then
                                Dijkstras(PreviousMaze, ShowPath)
                            ElseIf input = "play" Then
                                Playmaze(PreviousMaze)
                            ElseIf IsNothing(input) Then

                            End If
                        Else
                            Console.Clear()
                            MsgColour("No previous maze available", ConsoleColor.Red)
                            Console.ReadKey()
                        End If
                    ElseIf y = 10 Then
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
                        'Console.ReadKey()
                    ElseIf y = 11 Then
                        Dim ValidMaze, XMax, YMax As Integer
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
                                Dim YPosAfterMaze As Integer = Limits(3)
                                DisplayAvailablePositions(PreviousMaze.Count)
                                Console.SetCursorPosition(0, YPosAfterMaze + 3)
                                PreviousMaze = LoadedMaze
                                Dim input As String = SolvingMenu(YPosAfterMaze + 3)
                                Dim ShowPath As Boolean = HorizontalYesNo(YPosAfterMaze + 3, "Do you want to show the steps in solving the maze: ")
                                If input = "astar" Then
                                    aStar(PreviousMaze, ShowPath, True)
                                ElseIf input = "dijkstras" Then
                                    Dijkstras(PreviousMaze, ShowPath)
                                ElseIf input = "play" Then
                                    Playmaze(PreviousMaze)
                                ElseIf IsNothing(input) Then

                                End If
                            Else
                                Console.Clear()
                                MsgColour("Maze is too big for the screen, please decrease the font size and try again", ConsoleColor.Red)
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
    Sub OptionNotReady()
        Console.Clear()
        Console.WriteLine("Option not Ready Yet")
        Console.ReadKey()
        Console.Clear()
    End Sub
    Function HorizontalYesNo(ByVal ColumnPosition As Integer, ByVal message As String)
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
                    'Console.Write("                                                                                                               ")
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
    Sub aStar(ByVal availablepath As List(Of Node), ByVal ShowPath As Boolean, ByVal ShowSolveTime As Boolean)
        Dim start As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim target As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim current As Node = start
        Dim openSet, closedSet As New HashSet(Of Node)

        SetBoth(ConsoleColor.Red)
        target.Print("██")
        SetBoth(ConsoleColor.Green)
        start.Print("██")
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
                Dim tentative_gScore As Single = current.gCost + GetDistance(current, Neighbour)
                If tentative_gScore < Neighbour.gCost Or Not openSet.Contains(Neighbour) Then
                    Neighbour.gCost = tentative_gScore
                    Neighbour.hCost = GetDistance(Neighbour, target)
                    Neighbour.parent = current
                    openSet.Add(Neighbour)
                End If
            Next
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
        PrintMessageMiddle($"Path length: {path.Count - 1}   {timetaken}", Console.WindowHeight - 1, ConsoleColor.Yellow)
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
        CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While True
            If ExitCase() Then Return Nothing

            For Each cell As Cell In NeighbourGrowingTree(CurrentCell, VisitedList, FrontierSet, Limits, False)
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
        If Not ShowMazeGeneration Then
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, VisitedList, Limits)
        PrintMessageMiddle($"Time taken to generate the maze: {Stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
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

            For Each cell As Cell In NeighbourPrims(CurrentCell, VisitedList, FrontierSet, Limits, False)
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
        If Not ShowMazeGeneration Then
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, VisitedList, Limits)
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
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
            For Each cell As Cell In RanNeighbour(CurrentCell, Limits, False)
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
        If Not ShowMazeGeneration Then
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, VisitedList, Limits)
        PrintMessageMiddle($"Time taken to generate the maze: {Stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
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

            For Each cell As Cell In NeighbourPrims(CurrentCell, VisitedList, FrontierSet, Limits, False)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.Yellow)
                    cell.Print("██")
                End If
            Next
            If FrontierSet.Count = 0 Then Exit While
            Dim Index As Integer = R.Next(0, FrontierSet.Count)
            CurrentCell = FrontierSet(Index)
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
        End While
        If Not ShowMazeGeneration Then
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, VisitedList, Limits)
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function HuntAndKill(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
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
                Dim Index As Integer = r.Next(0, RecentCells.Count)
                Dim TemporaryCell As Cell = RecentCells(Index)
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
                        If x = Limits(2) - 3 Or x = Limits(2) - 1 Then
                            ColumnValue += 2
                        End If
                        Dim AdjancencyList As Integer() = AdjacentCheck(newcell, VisitedList)
                        CurrentCell = PickAdjancentCell(newcell, AdjancencyList)
                        If CurrentCell IsNot Nothing Then
                            Threading.Thread.Sleep(delay)
                            Dim WallCell As Cell = MidPoint(newcell, CurrentCell)
                            ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                            ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
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
        If Not ShowMazeGeneration Then
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, VisitedList, Limits)
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
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
    Sub AddStartAndEnd(ByRef ReturnablePath As List(Of Node), ByVal VisitedList As List(Of Cell), ByVal Limits() As Integer)
        ReturnablePath.Add(New Node(Limits(0) + 3, Limits(1) - 1))
        SetBoth(ConsoleColor.Green)
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Dim testnode As New Cell(Limits(2) - 3, Limits(3))
        If VisitedList.Contains(testnode) Then
            ReturnablePath.Add(New Node(Limits(2) - 3, Limits(3) + 1))
        Else
            ReturnablePath.Add(New Node(Limits(2) - 1, Limits(3) + 1))
        End If
        SetBoth(ConsoleColor.Red)
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        SetColour(ConsoleColor.Yellow)
        SetBackGroundColour(ConsoleColor.Black)
    End Sub
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
                Dim Index As Integer = r.Next(0, RecentCells.Count)
                Dim TemporaryCell As Cell = RecentCells(Index)
                'Picking random cell from neighbours
                ReturnablePath.Add(New Node(TemporaryCell.X, TemporaryCell.Y))
                VisitedList.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                Stack.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                'Pushing the cell onto the stack and the visited list
                Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)
                'Getting the position of the wall between the newly chosen cell and the previous cell
                CurrentCell = TemporaryCell
                ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))

                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.White)
                    PrevCell.Print("██")
                    WallCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    TemporaryCell.Print("██")
                    PrevCell = CurrentCell
                End If
                'Displaying the maze to the user
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
        If Not ShowMazeGeneration Then
            SetBoth(ConsoleColor.White)
            For Each node In ReturnablePath
                node.Print("██")
            Next
        End If
        Dim y As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, VisitedList, Limits)
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        Console.SetCursorPosition(0, y)
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
    Function CheckAndAdd(ByRef current As Node, ByRef availablepath As List(Of Node))
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
    Function GetNeighbours(ByVal current As Node, ByRef availablepath As List(Of Node))
        Return CheckAndAdd(current, availablepath)
    End Function
End Module
Class Cell
    Public X, Y As Integer
    Public Sub New(ByVal xpoint As Integer, ByVal ypoint As Integer)
        X = xpoint
        Y = ypoint
    End Sub
    Sub Update(ByVal _x As Integer, ByVal _y As Integer)
        X = _x
        Y = _y
    End Sub
    Public Function Pop(ByVal list As List(Of Cell))
        Dim val As Cell
        val = list(list.Count - 1)
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