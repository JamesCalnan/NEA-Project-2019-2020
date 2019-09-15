﻿Imports System.Drawing
Imports System.IO
Imports NEA_2019
Module Module1
    'implement this: https://en.wikipedia.org/wiki/Talk:Selection_sort#Implementations
    'TODO: give user the option to print a path or carve through walls, implement wall follower algorithm
    Sub Main()
        Console.CursorVisible = False
        Console.ForegroundColor = (ConsoleColor.White)
        'Dim r As New Random
        'Console.ReadKey()
        'Dim ind As Integer = 0
        'While 1
        '    Console.CursorVisible = False
        '    Dim list As New List(Of Double)
        '    For i = 1 To Console.WindowHeight - 1
        '        list.Add(i)
        '    Next

        '    Shuffle(list)
        '    Console.WriteLine()
        '    For Each num In list
        '        'Console.WriteLine(num)
        '    Next

        '    'Console.WriteLine($"post shuffling: {list.Count}")
        '    Console.WriteLine()
        '    Dim sl As List(Of Double) = mergesort(list) ', 0, list.Count - 1)
        '    'For Each num In sl
        '    '    Console.WriteLine(num)
        '    'Next
        '    Console.ReadKey()
        '    Console.Clear()
        'End While
        Dim MenuOptions() As String = {"Recursive Backtracker Algorithm (using iteration)", "Recursive Backtracker Algorithm (using recursion)", "Hunt and Kill Algorithm", "Prim's Algorithm (simplified)", "Prim's Algorithm (true)", "Aldous-Broder Algorithm", "Growing Tree Algorithm", "Sidewinder Algorithm", "Binary Tree Algorithm", "Wilson's Algorithm", "Eller's Algorithm", "Kruskal's Algorithm", "Houston's Algorithm", "Spiral Backtracker Algorithm", "Custom Algorithm", "", "Load the previously generated maze", "Save the previously generated maze", "Output the previous maze as a png image", "Load a maze from a text file", "Load a maze from an image file", "", "Exit"}
        Menu(MenuOptions)
        'Console.ReadKey()

        'Dim bmp As New Bitmap(350, 350)
        'Dim g As Graphics
        'g = Graphics.FromImage(bmp)
        'g.FillRectangle(Brushes.Aqua, 0, 0, 250, 250)
        'g.Dispose()
        'bmp.Save("name", System.Drawing.Imaging.ImageFormat.Png)
        'bmp.Dispose()
    End Sub
    Sub Shuffle(ByRef lista As List(Of Double))
        Dim r As New Random
        Dim listb As New List(Of Double)
        For i = 0 To lista.Count - 1
            Dim temp As Integer = r.Next(0, lista.Count)
            listb.Add(lista(temp))
            lista.RemoveAt(temp)
        Next
        lista = listb
    End Sub
    Sub AnimateSort(ByVal a As List(Of Double))
        Console.SetCursorPosition(0, 0)
        For Each number In a
            SetBoth(ConsoleColor.White)
            Console.Write("".PadLeft(number, "X"c))
            SetBoth(ConsoleColor.Black)
            Console.Write("".PadLeft((Console.WindowWidth - number) - 5, "X"c))
            Console.WriteLine()
        Next
    End Sub
    Function recursiveSolve(ByVal maze As List(Of Node), ByVal Visited As Dictionary(Of Node, Boolean), ByVal correctPath As Dictionary(Of Node, Boolean), ByVal X As Integer, ByVal Y As Integer, ByVal Target As Node, ByVal ShowSteps As Boolean, ByVal Delay As Integer)
        Dim CurrentNode As New Node(X, Y)
        If CurrentNode.Equals(Target) Then Return True
        If Not maze.Contains(CurrentNode) OrElse Visited(CurrentNode) Then Return False
        If ShowSteps Then
            CurrentNode.Print("XX")
            Threading.Thread.Sleep(Delay)
        End If
        Visited(CurrentNode) = True
        If recursiveSolve(maze, Visited, correctPath, X, Y - 1, Target, ShowSteps, Delay) Then
            correctPath(CurrentNode) = True
            Return True
        End If
        If recursiveSolve(maze, Visited, correctPath, X - 2, Y, Target, ShowSteps, Delay) Then
            correctPath(CurrentNode) = True
            Return True
        End If
        If recursiveSolve(maze, Visited, correctPath, X + 2, Y, Target, ShowSteps, Delay) Then
            correctPath(CurrentNode) = True
            Return True
        End If
        If recursiveSolve(maze, Visited, correctPath, X, Y + 1, Target, ShowSteps, Delay) Then
            correctPath(CurrentNode) = True
            Return True
        End If
        Return False
    End Function
    Sub SaveMazeAscii(ByVal Maze As List(Of Node))
        Dim GX, GY As Integer
        GX = 0
        GY = 0
        For Each node In Maze
            If node.X > GX Then GX = node.X
            If node.Y > GY Then GY = node.Y
        Next
        Dim lineText As New List(Of String)
        Dim currentLine As String = ""
        For y = 1 To GY + 1
            currentLine = ""
            For x = 6 To GX + 2 Step 2
                Dim newnode As New Node(x, y)

                If Maze.Contains(newnode) Then
                    currentLine += ("XX")
                Else
                    currentLine += ("  ")
                End If
            Next
            lineText.Add(currentLine)
        Next
        Using writer As StreamWriter = New StreamWriter("ascii test10.txt", True)
            For Each Str1 In lineText
                writer.WriteLine(Str1)
            Next
        End Using
    End Sub
    Sub MsgColour(ByVal Msg As String, ByVal Colour As ConsoleColor)
        Console.ForegroundColor = (Colour)
        Console.WriteLine(Msg)
        Console.ForegroundColor = (ConsoleColor.White)
    End Sub

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


    Sub SetBoth(ByVal colour As ConsoleColor)
        Console.ForegroundColor = colour
        Console.BackgroundColor = colour
    End Sub
    Sub SaveMazeTextFile(ByVal path As List(Of Node), ByVal Algorithm As String)
        Console.Clear()
        SaveMazeAscii(path)
        'Dim filename As String
        'Do
        '    Console.Write("File Name (don't include .txt): ")
        '    filename = Console.ReadLine
        '    filename += ".txt"
        '    If System.IO.File.Exists(filename) Then
        '        MsgColour("Invalid filename", ConsoleColor.Red)
        '    End If
        'Loop Until Not System.IO.File.Exists(filename)
        'Using writer As StreamWriter = New StreamWriter(filename, True)
        '    writer.WriteLine($"{Algorithm}")
        '    For i = 0 To path.Count - 1
        '        writer.WriteLine(path(i).X)
        '        writer.WriteLine(path(i).Y)
        '    Next
        'End Using
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
    Function LoadMazePNG()
        'loading a big maze twice exceeds memory limit
        Console.Clear()
        Console.Write("File Name of the maze to load (don't include .txt): ")
        Dim filename As String = Console.ReadLine
        Console.Clear()
        Dim Maze As New List(Of Node)
        Dim Path As New List(Of Node)
        Dim multiplier As Integer = 8
        Dim PathOnMaze As Boolean = False
        Dim image As New Bitmap($"{filename}.png")
        Dim GreatestX As Integer = 0
        Dim GreatestY As Integer = 0
        Dim GreatestAllowedX As Integer = Console.WindowWidth - 56
        Dim GreatestAllowedY As Integer = Console.WindowHeight - 5
        For y = 1 To image.Height Step multiplier * 2
            For x = 1 To image.Width Step multiplier * 2
                Dim pixel As Color = image.GetPixel(x, y)
                If pixel.GetBrightness = 1 Then
                    Dim b As Integer = pixel.GetBrightness
                    Maze.Add(New Node(x / multiplier, y / (multiplier * 2)))
                    If x / multiplier > GreatestX Then GreatestX = x / multiplier
                    If y / (multiplier * 2) > GreatestY Then GreatestY = y / (multiplier * 2)
                    If x / multiplier > GreatestAllowedX Or y / (multiplier * 2) > GreatestAllowedY Then
                        Return Nothing
                    End If
                End If
                If pixel.GetBrightness <> 0 And pixel.GetBrightness <> 1 Then
                    PathOnMaze = True
                    Path.Add(New Node(x / multiplier, y / (multiplier * 2)))
                End If
            Next
        Next
        Dim Finish As Node
        Dim Start As Node
        If PathOnMaze Then
            Start = Path(0)
            Finish = Path(Path.Count - 1)
            Dim showPath As Boolean = HorizontalYesNo(0, "There is already a path on this maze would you like to display it  ", True, True, False)
            If showPath Then
                SetBoth(ConsoleColor.White)
                For Each node In Maze
                    node.Print("XX")
                Next
                SetBoth(ConsoleColor.Green)
                For Each node In Path
                    node.Print("XX")
                Next
                Path.RemoveAt(0)
                Path.RemoveAt(Path.Count - 1)
                For Each node In Path
                    Maze.Add(node)
                Next
                Maze.Add(Start)
                Maze.Add(Finish)
                Console.ReadKey()
            Else
                Path.RemoveAt(0)
                Path.RemoveAt(Path.Count - 1)
                For Each node In Path
                    Maze.Add(node)
                Next
                Maze.Add(Start)
                Maze.Add(Finish)
                SetBoth(ConsoleColor.White)
                PrintMazeHorizontally(Maze, GreatestX, GreatestY)
                PrintStartandEnd(Maze)
                'Solving of the maze goes here
                Console.BackgroundColor = ConsoleColor.Black
                Console.ForegroundColor = ConsoleColor.White
                Dim temparr() As String = {"Solve using the A* algorithm", "Solve using Dijkstra's algorithm", "Solve using Breadth-first search", "Solve using Depth-first search (using iteration)", "Solve using Depth-first search (using recursion)", "Solve using the dead end filling method", "Play the maze", "Braid the maze (remove dead ends)", "Clear the maze and return to the menu"}
                Dim Input As String = SolvingMenu(temparr, "What would you like to do with the maze", GreatestX + 3, 3)
                SolvingInput(Input, True, GreatestY, 0, Maze, "")
            End If
        Else
            Start = Maze(0)
            Finish = Maze(Maze.Count - 1)
            Maze.RemoveAt(0)
            Maze.RemoveAt(Maze.Count - 1)
            Maze.Add(Start)
            Maze.Add(Finish)
            SetBoth(ConsoleColor.White)
            PrintMazeHorizontally(Maze, GreatestX, GreatestY)
            PrintStartandEnd(Maze)
            'Solving of the maze goes here
            Console.BackgroundColor = ConsoleColor.Black
            Console.ForegroundColor = ConsoleColor.White
            Dim temparr() As String = {"Solve using the A* algorithm", "Solve using Dijkstra's algorithm", "Solve using Breadth-first search", "Solve using Depth-first search (using iteration)", "Solve using Depth-first search (using recursion)", "Solve using the dead end filling method", "Play the maze", "Braid the maze (remove dead ends)", "Clear the maze and return to the menu"}
            Dim Input As String = SolvingMenu(temparr, "What would you like to do with the maze", GreatestX + 3, 3)
            SolvingInput(Input, True, GreatestY, 0, Maze, "")
        End If
        Return Maze
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
    Sub WallFollower(ByVal Maze As List(Of Node), ByVal ShowPath As Boolean, ByVal Delay As Integer, ByVal rule As String)
        Dim start_v As New Node(Maze(Maze.Count - 2).X, Maze(Maze.Count - 2).Y)
        Dim goal As New Node(Maze(Maze.Count - 1).X, Maze(Maze.Count - 1).Y)
        Dim u As Node = start_v
        Dim prev As Node = u
        SetBoth(ConsoleColor.Green)
        u.Print("XX")
        Console.ReadKey()
        Dim CurrentDirection As String = "down"
        Do
            SetBoth(ConsoleColor.DarkCyan)
            prev.Print("XX")
            CurrentDirection = GetNextDirection(Maze, u, CurrentDirection,rule)
            If CurrentDirection = "left" Then
                u.update(u.X - 2, u.Y)
            ElseIf CurrentDirection = "right" Then
                u.update(u.X + 2, u.Y)
            ElseIf CurrentDirection = "up" Then
                u.update(u.X, u.Y - 1)
            ElseIf CurrentDirection = "down" Then
                u.update(u.X, u.Y + 1)
            End If
            'If u.Adjacent(goal) Then Exit Do
            Console.BackgroundColor = ConsoleColor.Black
            Console.ForegroundColor = ConsoleColor.White
            Console.SetCursorPosition(0, 1)
            Console.Write($"Current direction: {CurrentDirection.Substring(0, 1).ToUpper}{CurrentDirection.Substring(1, CurrentDirection.Count - 1)}      ")
            SetBoth(ConsoleColor.Green)
            u.Print("XX")
            prev = u
            Threading.Thread.Sleep(100)
        Loop Until u.Equals(goal)
        SetBoth(ConsoleColor.DarkCyan)
        prev.Print("XX")
    End Sub
    Function GetNextDirection(ByVal Maze As List(Of Node), ByVal CurrentNode As Node, ByVal CurrentDirection As String, ByVal Rule As String)
        Dim HorizontalDirections As New List(Of String) From {"left", "right"}
        Dim VerticalDirections As New List(Of String) From {"up", "down"}
        If CurrentDirection = "right" Then
            Dim tempNode As New Node(CurrentNode.X, CurrentNode.Y - 1)
            If Maze.Contains(tempNode) Then Return VerticalDirections(0)
            tempNode.update(CurrentNode.X + 2, CurrentNode.Y)
            If Maze.Contains(tempNode) Then Return "right"
            tempNode.update(CurrentNode.X, CurrentNode.Y + 1)
            If Maze.Contains(tempNode) Then Return VerticalDirections(1)
        End If
        If CurrentDirection = "down" Then
            Dim tempNode As New Node(CurrentNode.X + 2, CurrentNode.Y)
            If Maze.Contains(tempNode) Then Return HorizontalDirections(1)
            tempNode.update(CurrentNode.X, CurrentNode.Y + 1)
            If Maze.Contains(tempNode) Then Return "down"
            tempNode.update(CurrentNode.X - 2, CurrentNode.Y)
            If Maze.Contains(tempNode) Then Return HorizontalDirections(0)
        End If
        If CurrentDirection = "left" Then
            Dim tempNode As New Node(CurrentNode.X, CurrentNode.Y + 1)
            If Maze.Contains(tempNode) Then Return VerticalDirections(1)
            tempNode.update(CurrentNode.X - 2, CurrentNode.Y)
            If Maze.Contains(tempNode) Then Return "left"
            tempNode.update(CurrentNode.X, CurrentNode.Y - 1)
            If Maze.Contains(tempNode) Then Return VerticalDirections(0)
        End If
        If CurrentDirection = "up" Then
            Dim tempNode As New Node(CurrentNode.X - 2, CurrentNode.Y)
            If Maze.Contains(tempNode) Then Return HorizontalDirections(0)
            tempNode.update(CurrentNode.X, CurrentNode.Y - 1)
            If Maze.Contains(tempNode) Then Return "up"
            tempNode.update(CurrentNode.X + 2, CurrentNode.Y)
            If Maze.Contains(tempNode) Then Return HorizontalDirections(1)
        End If
        Return ReverseDirection(CurrentDirection)
        Return CurrentDirection
    End Function
    Function ReverseDirection(ByVal currentdirection As String)
        If currentdirection = "right" Then
            Return "left"
        ElseIf currentdirection = "left" Then
            Return "right"
        ElseIf currentdirection = "up" Then
            Return "down"
        ElseIf currentdirection = "down" Then
            Return "up"
        End If
    End Function
    Sub Backtrack(ByVal prev As Dictionary(Of Node, Node), ByVal target As Node, ByVal source As Node, ByVal watch As Stopwatch)
        Dim u As Node = target
        Dim Pathlength As Integer = 1
        Dim PrevNode As Node = u
        SetBoth(ConsoleColor.Green)
        Dim timetaken As String = $"{watch.Elapsed.TotalSeconds}"
        u.Print("██")
        While prev(u) IsNot Nothing
            u = prev(u)
            DrawBetween(PrevNode, u)
            PrevNode = u
            u.Print("██")
            Pathlength += 1
        End While
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, Console.WindowHeight - 1)
        Console.Write($"Solving                            Time taken: {timetaken}")
        'PrintMessageMiddle($"Path length: {Pathlength}   {timetaken}", Console.WindowHeight - 1, ConsoleColor.Green)
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
        Console.Write($"Solving                            Time taken: {timetaken}")
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

    Function GetNeededNodes(ByVal Maze As List(Of Node)) As List(Of Node)
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, Console.WindowHeight - 3)
        Console.Write("Constructing graph")
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
        While True
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
        If AdjacentCells.Contains(top) And AdjacentCells.Contains(right) Then Return True 'is it a corner
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

    Sub SaveMazePNG(ByVal Path As List(Of Node), ByVal Algorithm As String, ByVal fileName As String)
        Dim solving As Boolean = HorizontalYesNo(0, "Do you want the outputted maze to have the solution on it  ", False, False, False)
        Console.Clear()
        Console.Write("Saving...")
        Dim Multiplier As Integer = 8
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
        'Dim mnum As Byte = Multiplier
        'Dim mulNum() As Byte = mnum
        'g.AddMetafileComment(mulNum)
        'g.DrawString(Algorithm, f, Brushes.White, point)
        g.Dispose()
        bmp.Save($"{fileName} m {Multiplier}.png", System.Drawing.Imaging.ImageFormat.Png)
        bmp.Dispose()
    End Sub
    Function MidPoint(ByVal cell1 As Object, ByVal cell2 As Object)
        If cell1.GetType.ToString = "NEA_2019.Cell" Then
            Return New Cell((cell1.X + cell2.X) / 2, (cell1.Y + cell2.Y) / 2)
        Else
            Return New Node((cell1.X + cell2.X) / 2, (cell1.Y + cell2.Y) / 2)
        End If
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
Class Item
    Public Time As Double
    Public Algorithm As String
    Public Sub New(ByVal _time As Double, ByVal _algorithm As String)
        Time = _time
        Algorithm = _algorithm
    End Sub
End Class
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