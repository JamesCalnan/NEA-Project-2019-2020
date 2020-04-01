Imports System.Drawing
Imports System.IO
Imports NEA_2019
Module Module1
    'todo play the maze white square bug
    Sub Main()
        Console.ForegroundColor = ConsoleColor.White
        Console.WriteLine("please make the font size 16 by clicking the little picture in the top left of the window
then selecting properties and then changing the font size, then press any button")


        Console.ReadKey()
        Console.CursorVisible = False
        Console.Clear()
        Do
            Console.SetCursorPosition(0, 0)
            Console.Write("Please make the window full screen")
        Loop Until Console.WindowWidth > Console.LargestWindowWidth - 10 And Console.WindowHeight > Console.LargestWindowHeight - 5
        'Dim bool = HorizontalYesNo(0, "Do you want to have the exit option available on the menu:   ", True, True, False)
        'Console.ReadKey()
        Dim menuOptions() As String = {
            "Generate a maze using one of the following algorithms",
            "   Recursive Backtracker Algorithm (3 options)",
            "   Randomised Breadth-First Search",
            "   Hunt and Kill Algorithm (first cell)",
            "   Hunt and Kill Algorithm (random cell)",
            "   Prim's Algorithm (simplified)",
            "   Prim's Algorithm (true)",
            "   Aldous-Broder Algorithm",
            "   Growing Tree Algorithm",
            "   Sidewinder Algorithm",
            "   Borůvka's Algorithm (top down)",
            "   Borůvka's Algorithm (random)",
            "   Binary Tree Algorithm (top down)",
            "   Binary Tree Algorithm (random)",
            "   Wilson's Algorithm (9 options)",
            "   Eller's Algorithm",
            "   Kruskal's Algorithm (simplified)",
            "   Kruskal's Algorithm (true)",
            "   Houston's Algorithm",
            "   Spiral Backtracker Algorithm",
            "   Reverse-Delete Algorithm (best-first search)",
            "   Reverse-Delete Algorithm (breadth-first search)",
            "   Reverse-Delete Algorithm (depth-first search)",
            "   Custom Algorithm",
            "   Dungeon Creation Algorithm",
            "   Conway's game of life (Maze generation)",
            "   Make your own maze",
            "",
            "Load the previously generated maze",
            "",
            "Change the path colour           current colour: ",
            "Change the background colour     current colour: ",
            "Change the solving colour        current colour: ",
            "",
            "Load a maze from a text file (list of points)",
            "Load a maze from an image file",
            "Load a maze from an ascii text file",
            "Save the previously generated maze as a list of points",
            "Save the previous maze as a png image",
            "Save the previous maze to ascii text file",
            "",
            "Path finding visualisations on a grid",
            "   A* algorithm",
            "   Iterative deepening A* (very slow)",
            "   Dijkstra's algorithm",
            "   Best-first search",
            "   Breadth-first search (using iteration)",
            "   Breadth-first search (using recursion)",
            "   Depth-first search (using iteration)",
            "   Depth-first search (using recursion)",
            "   Lee Algorithm (Wave Propagation)",
            "   Shortest Path Faster Algorithm (normal)",
            "   Shortest Path Faster Algorithm (Large Label First)",
            "   Shortest Path Faster Algorithm (Small Label First)",
            "",
            "Conway's game of life",
            "",
            "Sorting Algorithm visualisations",
            "",
            "Information on using this program",
            "Useful terms",
            "",
            "Exit"
        }
        Menu(menuOptions, "Menu")

        Dim colour As New List(Of String)
        For i = 0 To 15
            Dim bmp As New Bitmap(350, 350)
            Dim g As Graphics
            g = Graphics.FromImage(bmp)
            Dim backGroundColour As ConsoleColor = i
            g.FillRectangle(consoleColourToBrush(backGroundColour), 0, 0, 349, 349)
            g.Dispose()
            bmp.Save($"name{i}.png", Imaging.ImageFormat.Png)
            bmp.Dispose()
            Dim image As New Bitmap($"name{i}.png")
            Dim saveString As String = backGroundColour.ToString()
            For j = backGroundColour.ToString().Count() To 20
                saveString += " "
            Next
            saveString += getImageBackgroundColour(image)
            colour.Add(saveString)
        Next
        Using writer = New StreamWriter($"colour names.txt", True)
            For Each thing In colour
                writer.WriteLine(thing)
            Next
        End Using
    End Sub
    Sub PrintPreviousMaze(previousMaze As List(Of Node), previousAlgorithm As String, showPath As Boolean, ByRef yPosAfterMaze As Integer, solvingDelay As Integer, tempArr() As String, pathColour As ConsoleColor, backGroundColour As ConsoleColor, solvingColour As ConsoleColor)
        If Not IsNothing(previousMaze) And previousMaze.Count > 0 Then
            Console.Clear()
            If previousAlgorithm <> "" Or previousAlgorithm <> " " Then
                Console.SetCursorPosition(0, 0)
                Const mess = "Algorithm used to generate this maze: "
                Console.Write(mess)
                Console.SetCursorPosition(mess.Length, 0)
                MsgColour(previousAlgorithm, ConsoleColor.Green)
            End If

            Dim gX, gY As Integer
            gX = 0
            gY = 0
            For Each node In previousMaze
                If gX < node.X Then gX = node.X
                If gY < node.Y Then gY = node.Y
            Next
            If previousAlgorithm = "   Make your own maze" Then gY += 1
            If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, {5, 3, gX + 1, gY - 1})
            SetBoth(pathColour)
            PrintMazeHorizontally(previousMaze, gX, gY)
            Console.BackgroundColor = (ConsoleColor.Black)
            DisplayAvailablePositions(previousMaze.Count)
            yPosAfterMaze = gY
            Console.SetCursorPosition(0, yPosAfterMaze + 3)
            Dim input = SolvingMenu(tempArr, "What would you like to do with the maze", gX + 6, 3, {})
            SolvingInput(input, showPath, yPosAfterMaze, solvingDelay, previousMaze, previousAlgorithm, pathColour, backGroundColour, solvingColour)
        Else
            Console.Clear()
            MsgColour("No previous maze available", ConsoleColor.Red)
            Console.ReadKey()
        End If
    End Sub

    Function GetAllConsoleColours() As String()
        Dim colourArr(15) As String
        For i = 0 To 15
            Dim curColour As ConsoleColor = i
            colourArr(i) = curColour.ToString()
        Next
        Return colourArr '{ConsoleColor.Black,ConsoleColor.Blue,ConsoleColor.Cyan,ConsoleColor.Gray,ConsoleColor.Gray,ConsoleColor.Green,ConsoleColor.Magenta,ConsoleColor.Red,ConsoleColor.White,ConsoleColor.Yellow,ConsoleColor.DarkBlue,ConsoleColor.DarkCyan,ConsoleColor.DarkGreen,ConsoleColor.DarkMagenta,ConsoleColor.DarkGray}
    End Function
    Function ColourChange(ByVal colourArr() As String) As ConsoleColor
        Console.Clear()
        Console.CursorVisible = False
        Dim returnValue As String = SolvingMenu(colourArr, "What colour would you like to change to", 0, 0, {})
        Console.Clear()
        For i = 0 To 15
            Dim curColour As ConsoleColor = i
            If curColour.ToString = returnValue Then Return curColour
        Next
        Return 0
    End Function




    Sub AnimateSort(a As List(Of Double), Optional delay As Integer = 0, Optional sublist As Integer = -1, Optional totalLength As Integer = 0)
        Console.SetCursorPosition(0, 0)
        If sublist <> -1 Then
            If sublist = 0 Then
                Console.SetCursorPosition(0, 0)
                For i = 0 To totalLength \ 2
                    SetBoth(ConsoleColor.Black)
                    Console.WriteLine("".PadLeft(Console.WindowWidth - 6, "X"c))
                Next
                Console.SetCursorPosition(0, 0)
            ElseIf sublist = 1 Then
                Console.SetCursorPosition(0, totalLength - totalLength \ 2)
                For i = totalLength - totalLength \ 2 To totalLength
                    SetBoth(ConsoleColor.Black)
                    Console.WriteLine("".PadLeft(Console.WindowWidth - 6, "X"c))
                Next
                Console.SetCursorPosition(0, totalLength - totalLength \ 2)
            End If
        End If
        For Each number In a
            SetBoth(ConsoleColor.White)
            Console.Write("".PadLeft(number, "X"c))
            Console.BackgroundColor = ConsoleColor.Black
            Console.Write($" {number}")
            SetBoth(ConsoleColor.Black)
            Console.Write("".PadLeft((Console.WindowWidth - number) - 5, "X"c))
            Console.WriteLine()
        Next
        If delay <> 0 Then Threading.Thread.Sleep(delay)
    End Sub
    Sub MsgColour(msg As String, colour As ConsoleColor)
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = (colour)
        Console.WriteLine(msg)
        Console.ForegroundColor = (ConsoleColor.White)
    End Sub
    Sub DisplayAvailablePositions(count As Integer)
        PrintMessageMiddle($"There are {count} available positions in the maze", 0, ConsoleColor.Magenta)
    End Sub
    Sub DrawBackground(backgroundColour As ConsoleColor, limits() As Integer)
        SetBoth(backgroundColour)
        For y = limits(1) - 1 To limits(3) + 1
            Console.SetCursorPosition(limits(0) + 1, y)
            Console.Write("".PadLeft(limits(2) - 3, " "c))
        Next
    End Sub
    Function PreGenMenu(arr() As String, message As String)
        Console.Clear()
        Dim temparr() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0}
        Dim currentCol As Integer = Console.CursorTop
        Dim y As Integer = Console.CursorTop
        Dim numOfOptions As Integer = arr.Count
        MsgColour(message, ConsoleColor.Yellow)
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
            Dim count = 1
            For Each MenuOption In arr
                Console.SetCursorPosition(0, count + currentCol)
                Console.Write($" {MenuOption}  ")
                count += 1
            Next
            Console.SetCursorPosition(0, y + 1)
            MsgColour($"> {arr(y)}", ConsoleColor.Green)
        End While
        Return Nothing
    End Function

    Function GetValidFileName(fileType As String)
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.Black
        Dim invalidCharacters = "\/:*?""<>|"
        Console.Clear()
        Dim filename As String
        Dim validname As Boolean
        Do
            validname = True
            Console.Write("File Name (don't include file type): ")
            filename = Console.ReadLine
            For Each character In filename
                For Each invalidcharacter In From invalidcharacter1 In invalidCharacters Where character = invalidcharacter1
                    validname = False
                Next
            Next
            If File.Exists(filename + fileType) Then
                MsgColour("A file with that name already exists", ConsoleColor.Red)
                validname = False
            ElseIf Not validname Then
                MsgColour("Invalid character in filename", ConsoleColor.Red)
            End If
        Loop Until Not System.IO.File.Exists(filename + fileType) And validname
        Return filename
    End Function

    Sub PrintStartandEnd(mazePositions As List(Of Node))
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
    Sub Backtrack(prev As Dictionary(Of Node, Node), target As Node, source As Node, watch As Stopwatch, Optional ByRef returnPathLength As Integer = 0)
        Dim u As Node = target
        Dim pathlength = 1
        Dim prevNode As Node = u
        SetBoth(ConsoleColor.Green)
        Dim timetaken As String = $"{watch.Elapsed.TotalSeconds}"
        If returnPathLength = 0 Then u.Print("██")
        While prev.ContainsKey(u) AndAlso prev(u) IsNot Nothing
            u = prev(u)
            If returnPathLength = 0 Then DrawBetween(prevNode, u)
            prevNode = u
            If returnPathLength = 0 Then u.Print("██")
            pathlength += 1
        End While
        returnPathLength = pathlength
        If returnPathLength = 0 Then
            Console.BackgroundColor = ConsoleColor.Black
            Console.ForegroundColor = ConsoleColor.White
            Console.SetCursorPosition(0, Console.WindowHeight - 1)
            Console.Write($"Solving                            Time taken: {timetaken}")
        End If
        'PrintMessageMiddle($"Path length: {Pathlength}   {timetaken}", Console.WindowHeight - 1, Color.Green)
    End Sub
    Function ExtractMin(list As List(Of Node), dist As Dictionary(Of Node, Double))
        Dim returnnode As Node = list(0)
        For Each node In From node1 In list Where dist(node1) < dist(returnnode)
            returnnode = node
        Next
        Return returnnode
    End Function


    Function H(node As Node, goal As Node, d As Double)
        Dim dx As Integer = Math.Abs(node.X - goal.X)
        Dim dy As Integer = Math.Abs(node.Y - goal.Y)
        Return d * Math.Sqrt(dx * dx + dy * dy) '(dx + dy) ^ 2
    End Function
    Sub ReconstructPathForfile(camefrom As Dictionary(Of Node, Node), current As Node, goal As Node, ByRef bmp As Bitmap, ByRef g As Graphics, multiplier As Integer)
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
        Dim adding = 0.5
        'Algorithm: https://codepen.io/Codepixl/pen/ogWWaK
        For Each node In totalPath
            Dim myBrush As New SolidBrush(Color.FromArgb(255, red, green, blue))
            If red > 0 And blue = 0 Then
                red -= adding
                green += adding
            End If
            If green > 0 And red = 0 Then
                green -= adding
                blue += adding
            End If
            If blue > 0 And green = 0 Then
                red += adding
                blue -= adding
            End If
            g.FillRectangle(myBrush, (node.X) * multiplier, (node.Y * 2) * multiplier, 2 * multiplier, 2 * multiplier)
        Next
    End Sub
    Sub ReconstructPath(camefrom As Dictionary(Of Node, Node), current As Node, goal As Node, timetaken As String)
        SetBoth(ConsoleColor.Green)
        Dim pathLength = 1
        Dim prevNode As Node = current
        current.Print("██")
        While Not current.Equals(goal)
            current = camefrom(current)
            DrawBetween(prevNode, current)
            prevNode = current
            current.Print("██")
            pathLength += 1
        End While
        PrintMessageMiddle($"Path length: {pathLength}", Console.WindowHeight - 1, ConsoleColor.Green)
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, Console.WindowHeight - 1)
        Console.Write($"Solving                            Time taken: {timetaken}")
    End Sub
    Sub DrawBetween(node1 As Node, node2 As Node)
        If node1.X = node2.X Then
            SetBoth(ConsoleColor.Green)
            If node1.Y < node2.Y Then
                For i = node1.Y To node2.Y
                    Console.SetCursorPosition(node1.X, i)
                    Console.Write("XX")
                Next
            Else
                For i = node1.Y To node2.Y Step -1
                    Console.SetCursorPosition(node1.X, i)
                    Console.Write("XX")
                Next
            End If
        End If
        If node1.Y = node2.Y Then
            If node1.X <= node2.X Then
                For i = node1.X To node2.X
                    Console.SetCursorPosition(i, node1.Y)
                    Console.Write("XX")
                Next
            Else
                For i = node1.X To node2.X Step -1
                    Console.SetCursorPosition(i, node1.Y)
                    Console.Write("XX")
                Next
            End If
        End If
    End Sub
    Function GetDistance(nodea As Node, nodeb As Node)
        Dim dstX As Single = Math.Abs(nodea.X - nodeb.X)
        Dim dstY As Single = Math.Abs(nodea.Y - nodeb.Y)
        If dstX > dstY Then
            Return 14 * dstY + 10 * (dstX - dstY)
        Else
            Return 14 * dstX + 10 * (dstY - dstX)
        End If
    End Function
    Sub RetracePath(startnode As Node, endnode As Node, timetaken As String)
        Dim current As Node = endnode
        SetBoth(ConsoleColor.Green)
        current.Print("██")
        Dim pathLength = 1
        While Not current.Equals(startnode)
            current = current.Parent
            current.Print("██")
            pathLength += 1
        End While
        startnode.Print("██")
        PrintMessageMiddle($"Path length: {pathLength}   {timetaken}", Console.WindowHeight - 1, ConsoleColor.Green)
        Console.BackgroundColor = (ConsoleColor.Black)
    End Sub
    Sub PrintMessageMiddle(message As String, y As Integer, colour As ConsoleColor)
        Console.BackgroundColor = (ConsoleColor.Black)
        Console.ForegroundColor = (colour)
        Console.SetCursorPosition(Console.WindowWidth / 2 - message.Length / 2, y)
        Console.Write(message)
    End Sub



End Module