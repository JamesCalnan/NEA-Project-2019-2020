Imports System.Drawing
Imports System.IO
Imports NEA_2019
Module Module1
    'implement this: https://en.wikipedia.org/wiki/Talk:Selection_sort#Implementations, http://users.eecs.northwestern.edu/~haizhou/357/lec6.pdf
    'https://en.wikipedia.org/wiki/Bellman%E2%80%93Ford_algorithm, https://en.wikipedia.org/wiki/Floyd%E2%80%93Warshall_algorithm#Algorithm
    'get rid of dead-end filling method when the maze has been braided
    'give option for partial braid
    'TODO: give user the option to print a path or carve through walls, put in solving menu when the user has loaded an ascii maze from a text file, give the user the option to chose back and fore colour, solving menu colours still need to be done
    'solving menu needs to have thepathcolour and backgroundcolour implemented
    Sub Main()
        ''Console.ReadKey()
        ''Dim ind As Integer = 0
        ''While 1
        ''    Console.CursorVisible = False
        ''    Dim list As New List(Of Double)
        ''    For i = 1 To (Console.WindowHeight - 1)
        ''        list.Add(i * 1)
        ''    Next
        ''    AnimateSort(list, 1)
        ''    Threading.Thread.Sleep(20)
        ''    Shuffle(list)
        ''    AnimateSort(list, 1)
        ''    Threading.Thread.Sleep(20)
        ''    Dim sl As List(Of Double) = BubbleSortOptimisedAlternate(list) ', 0, list.Count - 1)
        ''    AnimateSort(sl, 1)
        ''    'For Each num In sl
        ''    '    Console.WriteLine(num)
        ''    'Next
        ''    Threading.Thread.Sleep(80)
        ''    Console.Clear()
        ''End While
        'Console.ResetColor()
        'Console.BackgroundColor = ConsoleColor.Black
        'Console.ForegroundColor = ConsoleColor.DarkGray
        'Console.WriteLine("hello there")
        'Console.ReadKey()
        
'        dim b as ConsoleColor = consolecolor.Black
'        
        Do
            Console.SetCursorPosition(0, 0)
            Console.Write("Please make the window full screen")
        Loop Until Console.WindowWidth > Console.LargestWindowWidth - 10 And Console.WindowHeight > Console.LargestWindowHeight - 5
        Dim menuOptions() As String = {
            "Generate a maze using one of the following algorithms",
            "   Recursive Backtracker Algorithm (using iteration)",
            "   Recursive Backtracker Algorithm (using recursion)",
            "   Hunt and Kill Algorithm",
            "   Prim's Algorithm (simplified)",
            "   Prim's Algorithm (true)",
            "   Aldous-Broder Algorithm",
            "   Growing Tree Algorithm",
            "   Sidewinder Algorithm",
            "   Binary Tree Algorithm",
            "   Wilson's Algorithm",
            "   Eller's Algorithm",
            "   Kruskal's Algorithm",
            "   Houston's Algorithm",
            "   Spiral Backtracker Algorithm",
            "   Custom Algorithm",
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
            "",
            "Save the previously generated maze as a list of points",
            "Save the previous maze as a png image",
            "Save the previous maze to ascii text file",
            "",
            "Exit"
        }
        Menu(menuOptions, "Menu")

        dim newColour as ConsoleColor = ColourChange()
        Console.ForegroundColor = newColour
        Console.WriteLine("MESSSSSSSSSSAGE")
        Console.ReadKey()
        ''Dim bmp As New Bitmap(350, 350)
        ''Dim g As Graphics
        ''g = Graphics.FromImage(bmp)
        ''g.FillRectangle(Brushes.Aqua, 0, 0, 250, 250)
        ''g.Dispose()
        ''bmp.Save("name", System.Drawing.Imaging.ImageFormat.Png)
        ''bmp.Dispose()
    End Sub
    function GetAllConsoleColours as string()
        dim colourArr(15) as string
        for i = 0 to 15
            dim curColour as ConsoleColor = i
            colourArr(i) = curColour.ToString()
        Next
        return colourArr'{ConsoleColor.Black,ConsoleColor.Blue,ConsoleColor.Cyan,ConsoleColor.Gray,ConsoleColor.Gray,ConsoleColor.Green,ConsoleColor.Magenta,ConsoleColor.Red,ConsoleColor.White,ConsoleColor.Yellow,ConsoleColor.DarkBlue,ConsoleColor.DarkCyan,ConsoleColor.DarkGreen,ConsoleColor.DarkMagenta,ConsoleColor.DarkGray}
    End function
    function ColourChange as ConsoleColor
        Console.Clear()
        Console.CursorVisible = False
        dim colourArr() as String = GetAllConsoleColours()
        dim returnValue as string = SolvingMenu(colourArr,"What colour would you like to change to",0,0)
        for i = 0 to 15
            dim curColour as ConsoleColor = i
            Console.Clear()
            if curColour.ToString = returnValue then return curcolour
        Next
        return 0
    End function
    Function StraightWays(maze As List(Of Node))
        dim mX = 0
        Dim gx = (From node In maze Select node.X).Concat(new Integer() {0}).Max()
        Dim gy = (From node In maze Select node.y).Concat(new Integer() {0}).Max()
        for x = 0 to Console.WindowWidth-40
            if maze.Contains(New Node(x,3)) then
                mX = x
                Exit For
            End If
        Next
        Dim corridorCount As New List(Of Integer)
        For x = mx To gx + 1 step 2
            Dim straightCount = 0
            For y = 3 To gy
                Dim tempNode As New Node(x, y)
                If maze.Contains(tempNode) Then
                    straightCount += 1
                Else
                    If straightCount > 1 Then 
                        corridorCount.Add(straightCount)
                    End If
                    straightCount = 0
                End If
                Threading.Thread.Sleep(5)
            Next
        Next
        For y = 3 To gy
            Dim straightCount = 0
            For x = mx To gx + 1  step 2
                Dim tempNode As New Node(x, y)
                If maze.Contains(tempNode) Then
                    straightCount += 1
                Else
                    If straightCount > 1 Then 
                        corridorCount.Add(straightCount)
                    End If
                    straightCount = 0
                End If
                Threading.Thread.Sleep(5)
            Next
        Next
        Return corridorCount.Average
    End Function
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
    Sub AnimateSort(a As List(Of Double), n As Integer)
        Console.SetCursorPosition(0, 0)
        Dim c = 0
        For Each number In a
            SetBoth(ConsoleColor.White)
            If c = n Then SetBoth(ConsoleColor.Green)
            Console.Write("".PadLeft(number, "X"c))
            SetBoth(ConsoleColor.Black)
            Console.Write("".PadLeft((Console.WindowWidth - number) - 5, "X"c))
            Console.WriteLine()
            c += 1
        Next
    End Sub
    Function LoadMazeAscii(pathColour as ConsoleColor, backGroundColour as ConsoleColor,solvingColour as ConsoleColor) As List(Of Node)
        Console.Clear()
        Dim y As Integer
        Console.Write("File Name of the maze to load (don't include .txt): ")
        Dim filename As String = Console.ReadLine
        filename += ".txt"
        If System.IO.File.Exists(filename) Then
            Dim maze As New List(Of Node)
            Using reader = New StreamReader(filename)
                Do Until reader.EndOfStream
                    Dim currentLine As String = reader.ReadLine
                    For i = 0 To currentLine.Count - 1 Step 2
                        If currentLine.Chars(i) = "X" Then
                            maze.Add(New Node(i, y))
                        End If
                    Next
                    y += 1
                Loop
            End Using
            Dim start As Node = maze(0)
            Dim finish As Node = maze(maze.Count - 1)
            maze.RemoveAt(0)
            maze.RemoveAt(maze.Count - 1)
            maze.Add(start)
            maze.Add(finish)
            SetBoth(pathColour)
            Dim gX, gY As Integer
            gX = 0
            gY = 0
            For Each node In maze
                If node.X > gX Then gX = node.X
                If node.Y > gY Then gY = node.Y
            Next
            If gX > Console.WindowWidth - 57 Or gY > Console.WindowHeight - 6 Then Return Nothing
            DrawBackground(backGroundColour,{5,3,gX+1,gY-1})
            SetBoth(pathColour)
            PrintMazeHorizontally(maze, gX, gY)
            PrintStartandEnd(maze)
            Console.BackgroundColor = ConsoleColor.Black
            Console.ForegroundColor = ConsoleColor.White
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
            Dim input As String = SolvingMenu(temparr, "What would you like to do with the maze", gX + 7, 3)
            SolvingInput(input, True, gY + 2, 0, maze, "",pathColour,backGroundColour,SolvingColour)
            Return maze
        Else
            Console.Clear()
            MsgColour("File doesn't exist", ConsoleColor.Red)
            Console.ReadKey()
        End If
        Return Nothing
    End Function

    Sub SaveMazeAscii(maze As List(Of Node))
        Dim gx, gy As Integer
        gx = 0
        gy = 0
        For Each node In maze
            If node.X > gx Then gx = node.X
            If node.Y > gy Then gy = node.Y
        Next
        Dim lineText As New List(Of String)
        Dim currentLine = ""
        For y = 0 To gy + 1
            currentLine = ""
            For x = 0 To gx + 2 Step 2
                Dim newnode As New Node(x, y)
                If maze.Contains(newnode) Then
                    currentLine += ("XX")
                Else
                    currentLine += ("  ")
                End If
            Next
            lineText.Add(currentLine)
        Next
        Using writer = New StreamWriter($"{GetValidFileName()}.txt", True)
            For Each Str1 In lineText
                writer.WriteLine(Str1)
            Next
        End Using
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
    sub DrawBackground(backgroundColour as ConsoleColor, limits() as Integer)
        SetBoth(backgroundColour)
        For y = limits(1) - 1 To limits(3) + 1
            For x = limits(0) + 1 To limits(2) + 1
                Console.SetCursorPosition(x, y)
                Console.Write("XX")
            Next
        Next
    End sub
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

    Function GetValidFileName()
        Dim invalidCharacters = "\/:*?""<>|"
        Console.Clear()
        Dim filename As String
        Dim validname As Boolean
        Do
            validname = True
            Console.Write("File Name (don't include file type): ")
            filename = Console.ReadLine
            For Each character In filename
                For Each invalidcharacter In invalidCharacters
                    If character = invalidcharacter Then validname = False
                Next
            Next
            If System.IO.File.Exists(filename) Then
                MsgColour("Invalid filename", ConsoleColor.Red)
            ElseIf Not validname Then
                MsgColour("Invalid character in filename", ConsoleColor.Red)
            End If
        Loop Until Not System.IO.File.Exists(filename) And validname
        Return filename
    End Function
    Sub SaveMazeTextFile(path As List(Of Node), algorithm As String)
        Using writer = New StreamWriter($"{GetValidFileName()}.txt", True)
            writer.WriteLine($"{algorithm}")
            For i = 0 To path.Count - 1
                writer.WriteLine(path(i).X)
                writer.WriteLine(path(i).Y)
            Next
        End Using
    End Sub
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
    Function LoadMazePng(pathColour as ConsoleColor, backGroundColour as ConsoleColor,SolvingColour as ConsoleColor)
        'loading a big maze twice exceeds memory limit
        Console.Clear()
        Console.Write("File Name of the maze to load (don't include .png): ")
        Dim filename As String = Console.ReadLine
        If System.IO.File.Exists($"{filename}.png") Then
            Console.Clear()
            Dim maze As New List(Of Node)
            Dim path As New List(Of Node)
            Dim multiplier = 8
            Dim pathOnMaze = False
            Dim image As New Bitmap($"{filename}.png")
            Dim greatestX = 0
            Dim greatestY = 0
            Dim greatestAllowedX As Integer = Console.WindowWidth - 56
            Dim greatestAllowedY As Integer = Console.WindowHeight - 5
            For y = 1 To image.Height Step multiplier * 2
                For x = 1 To image.Width Step multiplier * 2
                    Dim pixel As Color = image.GetPixel(x, y)
                    If pixel.GetBrightness = 1 Then
                        Dim b As Integer = pixel.GetBrightness
                        maze.Add(New Node(x / multiplier, y / (multiplier * 2)))
                        If x / multiplier > greatestX Then greatestX = x / multiplier
                        If y / (multiplier * 2) > greatestY Then greatestY = y / (multiplier * 2)
                        If x / multiplier > greatestAllowedX Or y / (multiplier * 2) > greatestAllowedY Then
                            Return Nothing
                        End If
                    End If
                    If pixel.GetBrightness <> 0 And pixel.GetBrightness <> 1 Then
                        pathOnMaze = True
                        path.Add(New Node(x / multiplier, y / (multiplier * 2)))
                    End If
                Next
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
            Dim finish As Node
            Dim start As Node
            If pathOnMaze Then
                start = path(0)
                finish = path(path.Count - 1)
                Dim showPath As Boolean = HorizontalYesNo(0, "There is already a path on this maze would you like to display it  ", True, True, False)
                If showPath Then
                    SetBoth(ConsoleColor.White)
                    For Each node In maze
                        node.Print("XX")
                    Next
                    SetBoth(ConsoleColor.Green)
                    For Each node In path
                        node.Print("XX")
                    Next
                    path.RemoveAt(0)
                    path.RemoveAt(path.Count - 1)
                    For Each node In path
                        maze.Add(node)
                    Next
                    maze.Add(start)
                    maze.Add(finish)
                    Console.ReadKey()
                Else
                    path.RemoveAt(0)
                    path.RemoveAt(path.Count - 1)
                    For Each node In path
                        maze.Add(node)
                    Next
                    maze.Add(start)
                    maze.Add(finish)
                    SetBoth(ConsoleColor.White)
                    PrintMazeHorizontally(maze, greatestX, greatestY)
                    PrintStartandEnd(maze)
                    'Solving of the maze goes here
                    Console.BackgroundColor = ConsoleColor.Black
                    Console.ForegroundColor = ConsoleColor.White

                    Dim input As String = SolvingMenu(temparr, "What would you like to do with the maze", greatestX + 3, 3)
                    SolvingInput(input, True, greatestY, 0, maze, "",pathColour,backGroundColour,SolvingColour)
                End If
            Else
                start = maze(0)
                finish = maze(maze.Count - 1)
                maze.RemoveAt(0)
                maze.RemoveAt(maze.Count - 1)
                maze.Add(start)
                maze.Add(finish)
                SetBoth(ConsoleColor.White)
                PrintMazeHorizontally(maze, greatestX, greatestY)
                PrintStartandEnd(maze)
                'Solving of the maze goes here
                Console.BackgroundColor = ConsoleColor.Black
                Console.ForegroundColor = ConsoleColor.White
                Dim input As String = SolvingMenu(temparr, "What would you like to do with the maze", greatestX + 3, 3)
                SolvingInput(input, True, greatestY, 0, maze, "",pathColour,backGroundColour,SolvingColour)
            End If
            Return maze
        Else
            MsgColour("File doesn't exist", ConsoleColor.Red)
        End If
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
    Sub Backtrack(prev As Dictionary(Of Node, Node), target As Node, source As Node, watch As Stopwatch)
        Dim u As Node = target
        Dim pathlength = 1
        Dim prevNode As Node = u
        SetBoth(ConsoleColor.Green)
        Dim timetaken As String = $"{watch.Elapsed.TotalSeconds}"
        u.Print("██")
        While prev(u) IsNot Nothing
            u = prev(u)
            DrawBetween(prevNode, u)
            prevNode = u
            u.Print("██")
            pathlength += 1
        End While
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, Console.WindowHeight - 1)
        Console.Write($"Solving                            Time taken: {timetaken}")
        'PrintMessageMiddle($"Path length: {Pathlength}   {timetaken}", Console.WindowHeight - 1, Color.Green)
    End Sub
    Function ExtractMin(list As List(Of Node), dist As Dictionary(Of Node, Double))
        Dim returnnode As Node = list(0)
        For Each node In list
            If dist(node) < dist(returnnode) Then returnnode = node
        Next
        Return returnnode
    End Function
    Function GetJunctionCount(availablePath As List(Of Node))
        Dim junctionCount = 0
        For Each node In availablePath
            If node.IsJunction(availablePath) Then
                junctionCount += 1
            End If
        Next
        Return junctionCount
    End Function
    Function GetDeadEndCount(availablePath As List(Of Node))
        Dim start As New Node(availablePath(availablePath.Count - 2).X, availablePath(availablePath.Count - 2).Y)
        Dim target As New Node(availablePath(availablePath.Count - 1).X, availablePath(availablePath.Count - 1).Y)
        Dim deadEndCount = 0
        For Each node In availablePath
            If node.Equals(start) Or node.Equals(target) Then Continue For
            Dim neighbours As List(Of Node) = GetNeighbours(node, availablePath)
            If neighbours.Count = 1 Then
                deadEndCount += 1
            End If
        Next
        Return deadEndCount
    End Function
    Function H(node As Node, goal As Node, d As Double)
        Dim dx As Integer = Math.Abs(node.X - goal.X)
        Dim dy As Integer = Math.Abs(node.Y - goal.Y)
        Return d * (dx + dy) ^ 2
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
    Sub Sd(availablePath As List(Of Node))
        Console.SetCursorPosition(0, 1)
        Console.ForegroundColor = ConsoleColor.Magenta
        Console.BackgroundColor = ConsoleColor.Black
        Console.Write("SWASTIKA DETECTION MODE ENGAGED")
        Dim positions As New List(Of Node)
        Dim width, minwidth, minheight, height As Integer
        minwidth = availablePath(availablePath.Count - 2).X
        minheight = availablePath(availablePath.Count - 2).Y + 1
        width = availablePath(availablePath.Count - 1).X
        height = availablePath(availablePath.Count - 1).Y - 1
        Dim numOfsFound = 0
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
                            positions.Add(New Node(x + _x, y + _y))
                        Next
                    Next
                    Dim correctCount = 0
                    For Each node In positions
                        If Not availablePath.Contains(node) Then
                            correctCount += 1
                        End If
                    Next
                    If correctCount = 16 Then
                        'there is a swastica
                        Console.ForegroundColor = ConsoleColor.Magenta
                        Console.BackgroundColor = ConsoleColor.Magenta
                        For Each node In positions
                            node.Print("XX")
                        Next
                        Console.SetCursorPosition(_x, _y)
                        Console.Write("XX")
                        numOfsFound += 1
                    End If
                    positions.Clear()
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
                        positions.Add(New Node(x + cell.X, y + cell.Y))
                    Next
                Next
                Dim correctCount = 0
                For Each node In positions
                    If availablePath.Contains(node) Then
                        correctCount += 1
                    End If
                Next
                If correctCount = 16 Then
                    'there is a swastica
                    Console.ForegroundColor = ConsoleColor.Magenta
                    Console.BackgroundColor = ConsoleColor.Magenta
                    For Each node In positions
                        node.Print("XX")
                    Next
                    cell.Print("XX")
                    numOfsFound += 1
                End If
                positions.Clear()
            Next
        Next
        Console.SetCursorPosition(0, 1)
        Console.ForegroundColor = ConsoleColor.Green
        Console.BackgroundColor = ConsoleColor.Black
        Console.Write($"---------------DONE---------------          {If(numOfsFound = 0, "No swastikas found", $"Number of Swastikas found: {numOfsFound}")}")
        Console.ReadKey()
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
    Function PickRandomStartingCell(limits() As Integer)
        Dim li As New List(Of Cell)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                li.Add(New Cell(x, y))
            Next
        Next
        Dim r As New Random
        Return li(r.Next(0, li.Count - 1))
    End Function

    Function GetNeededNodes(maze As List(Of Node)) As List(Of Node)
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, Console.WindowHeight - 3)
        Console.Write("Constructing graph")
        Dim newlist As New List(Of Node)
        Console.SetCursorPosition(35, Console.WindowHeight - 3)
        Console.Write($"Progress:")
        Dim stopwatch As Stopwatch = Stopwatch.StartNew
        Dim I = 0
        For Each node In maze
            If CornerJunction(node, maze) Then newlist.Add(node)
            I += 1
            Console.SetCursorPosition(45, Console.WindowHeight - 3)
            Console.Write($"{Math.Floor((I / maze.Count) * 100)}%")
        Next
        newlist.Add(maze(maze.Count - 2))
        newlist.Add(maze(maze.Count - 1))
        Console.SetCursorPosition(35, Console.WindowHeight - 3)
        Console.Write($"Time taken: {stopwatch.Elapsed.TotalSeconds}              ")
        Return newlist
    End Function
    Function ConstructAdjacencyList(neededNodes As List(Of Node), maze As List(Of Node)) As Dictionary(Of Node, List(Of Node))
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, Console.WindowHeight - 2)
        Console.Write("Constructing adjacency list")
        Dim adjacenyList As New Dictionary(Of Node, List(Of Node))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew
        Dim I = 0
        For Each Node In neededNodes
            Dim tempNode As New Node(Node.X, Node.Y)
            Dim adjacentNodes As New List(Of Node)
            Dim nodeToAdd3 As Node = FindAdjacentNodes(Node, maze, neededNodes, 0, -1)
            If Not IsNothing(nodeToAdd3) Then adjacentNodes.Add(nodeToAdd3)
            Dim nodeToAdd2 As Node = FindAdjacentNodes(Node, maze, neededNodes, 2, 0)
            If Not IsNothing(nodeToAdd2) Then adjacentNodes.Add(nodeToAdd2)
            Dim nodeToAdd1 As Node = FindAdjacentNodes(Node, maze, neededNodes, 0, 1)
            If Not IsNothing(nodeToAdd1) Then adjacentNodes.Add(nodeToAdd1)
            Dim nodeToAdd As Node = FindAdjacentNodes(Node, maze, neededNodes, -2, 0)
            If Not IsNothing(nodeToAdd) Then adjacentNodes.Add(nodeToAdd)
            adjacenyList.Add(Node, adjacentNodes)
            I += 1
            Console.SetCursorPosition(35, Console.WindowHeight - 2)
            Console.Write($"Progress: {Math.Floor((I / neededNodes.Count) * 100)}%")
        Next
        Console.SetCursorPosition(35, Console.WindowHeight - 2)
        Console.Write($"Time taken: {(stopwatch.Elapsed.TotalSeconds)}")
        Return adjacenyList
    End Function
    Function FindAdjacentNodes(currentNode As Node, maze As List(Of Node), neededNodes As List(Of Node), x As Integer, y As Integer)
        Dim tempnode As New Node(currentNode.X, currentNode.Y)
        While True
            tempnode.Update(tempnode.X + x, tempnode.Y + y)
            If maze.Contains(tempnode) Then
                If neededNodes.Contains(tempnode) Then Return tempnode
            Else
                Return Nothing
            End If
        End While
        Return Nothing
    End Function
    Function CornerJunction(currentNode As Node, adjacentCells As List(Of Node))
        Dim l As New List(Of Node)
        Dim top As New Node(currentNode.X, currentNode.Y - 1)
        Dim right As New Node(currentNode.X + 2, currentNode.Y)
        Dim bottom As New Node(currentNode.X, currentNode.Y + 1)
        Dim left As New Node(currentNode.X - 2, currentNode.Y)
        If adjacentCells.Contains(top) Then l.Add(top)
        If adjacentCells.Contains(right) Then l.Add(right)
        If adjacentCells.Contains(bottom) Then l.Add(bottom)
        If adjacentCells.Contains(left) Then l.Add(left)
        If l.Count >= 3 Then Return True 'Is it a junction
        If adjacentCells.Contains(top) And adjacentCells.Contains(right) Then Return True 'is it a corner
        If adjacentCells.Contains(right) And adjacentCells.Contains(bottom) Then Return True
        If adjacentCells.Contains(bottom) And adjacentCells.Contains(left) Then Return True
        If adjacentCells.Contains(left) And adjacentCells.Contains(top) Then Return True
        Return False
    End Function
    Function GetCornerCount(maze As List(Of Node))
        Dim cCount = 0
        For Each node In maze
            If IsCorner(node, maze) Then
                cCount += 1
            End If
        Next
        Return cCount
    End Function
    Function IsCorner(currentNode As Node, adjacentcells As List(Of Node))
        Dim top As New Node(currentNode.X, currentNode.Y - 1)
        Dim right As New Node(currentNode.X + 2, currentNode.Y)
        Dim bottom As New Node(currentNode.X, currentNode.Y + 1)
        Dim left As New Node(currentNode.X - 2, currentNode.Y)
        If adjacentcells.Contains(top) And adjacentcells.Contains(right) Then Return True 'is it a corner
        If adjacentcells.Contains(right) And adjacentcells.Contains(bottom) Then Return True
        If adjacentcells.Contains(bottom) And adjacentcells.Contains(left) Then Return True
        If adjacentcells.Contains(left) And adjacentcells.Contains(top) Then Return True
        Return False
    End Function
    Function InitialiseVisited(limits() As Integer)
        Dim dict As New Dictionary(Of Cell, Boolean)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                dict(New Cell(x, y)) = False
            Next
        Next
        Return dict
    End Function
    function consoleColourToBrush(colour as ConsoleColor)
        if colour = ConsoleColor.Black
            return Brushes.Black
        elseif colour = ConsoleColor.Blue
            return Brushes.Blue
        elseif colour = consolecolor.Cyan
            return Brushes.Cyan
        elseif colour = ConsoleColor.Gray
            return Brushes.SlateGray
        elseif colour = ConsoleColor.Green
            return Brushes.LimeGreen
        elseif colour = ConsoleColor.Magenta
            return Brushes.Magenta
        elseif colour = ConsoleColor.Red
            return Brushes.Red
        elseif colour = ConsoleColor.White
            return brushes.White
        elseif colour = ConsoleColor.Yellow
            return Brushes.Yellow
        elseif colour = ConsoleColor.DarkBlue
            return Brushes.DarkBlue
        elseif colour = ConsoleColor.DarkCyan
            return Brushes.DarkCyan
        elseif colour = ConsoleColor.DarkGray
            return Brushes.DarkSlateGray
        elseif colour = ConsoleColor.DarkGreen
            Return Brushes.DarkGreen
        elseif colour = ConsoleColor.DarkMagenta
            return Brushes.DarkMagenta
        elseif colour = ConsoleColor.DarkRed
            return Brushes.DarkRed
        ElseIf colour = consolecolor.DarkYellow
            return Brushes.OrangeRed
        End If
        return nothing
    End function
    Sub SaveMazePng(path As List(Of Node), algorithm As String, fileName As String, pathColour as consolecolor, backGroundColour as consolecolor)
        Console.Clear()
        Dim solving As Boolean = HorizontalYesNo(0, "Do you want the outputted maze to have the solution on it  ", False, False, False)
        Console.Clear()
        Console.Write("Saving...")
        Dim multiplier = 8
        Dim maxX, maxY As Integer
        For Each node In path
            If node.X > maxX Then maxX = node.X
            If node.Y > maxY Then maxY = node.Y
        Next
        Dim width As Integer = (maxX + 10) * multiplier
        Dim height As Integer = ((maxY + 4) * 2) * multiplier
        Dim bmp As New Bitmap(width, height)
        Dim g As Graphics
        g = Graphics.FromImage(bmp)
        g.FillRectangle(consoleColourToBrush(backGroundColour), 0, 0, width, height)
        dim gPathColour as Brush = consoleColourToBrush(pathColour)
        For Each thing In path
            g.FillRectangle(gPathColour, (thing.X) * multiplier, (thing.Y * 2) * multiplier, 2 * multiplier, 2 * multiplier)
        Next
        If solving Then
            Dim myBrush As New SolidBrush(Color.FromArgb(255, 0, 0, 255))
            DFS_IterativeFORFILE(path, bmp, g, multiplier)
            g.FillRectangle(myBrush, (path(path.Count - 2).X) * multiplier, (path(path.Count - 2).Y * 2) * multiplier, 2 * multiplier, 2 * multiplier)
        End If
        'g.FillRectangle(Brushes.Lime, (Path(Path.Count - 1).X) * Multiplier, (Path(Path.Count - 1).Y * 2) * Multiplier, 2 * Multiplier, 2 * Multiplier)
        Dim f As New Font("Roboto", width / 60)
        Dim point As New PointF(((width) / 2) - (algorithm.Length / 2) * multiplier, 1)
        'Dim mnum As Byte = Multiplier
        'Dim mulNum() As Byte = mnum
        'g.AddMetafileComment(mulNum)
        'g.DrawString(Algorithm, f, Brushes.White, point)
        g.Dispose()
        bmp.Save($"{fileName}.png", System.Drawing.Imaging.ImageFormat.Png)
        bmp.Dispose()
    End Sub
    Function MidPoint(cell1 As Object, cell2 As Object)
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
        Dim newNode As New Node(current.X, current.Y - 1)
        If availablepath.Contains(newnode) Then neighbours.Add(New Node(newnode.X, newnode.Y))
        newnode.Update(current.X + 2, current.Y)
        If availablepath.Contains(newnode) Then neighbours.Add(New Node(newnode.X, newnode.Y))
        newnode.Update(current.X, current.Y + 1)
        If availablepath.Contains(newnode) Then neighbours.Add(New Node(newnode.X, newnode.Y))
        newnode.Update(current.X - 2, current.Y)
        If availablepath.Contains(newnode) Then neighbours.Add(New Node(newnode.X, newnode.Y))
        Return neighbours
    End Function
End Module
Class Cell
    Public X, Y, CellSet As Integer
    Public Sub New(xpoint As Integer, ypoint As Integer)
        X = xpoint
        Y = ypoint
    End Sub
    Sub Update(x As Integer, y As Integer)
        Me.X = x
        Me.Y = y
    End Sub
    Function WithinLimits(limits() As Integer)
        If Me.X >= limits(0) And Me.X <= limits(2) And Me.Y >= limits(1) And Me.Y <= limits(3) Then Return True
        Return False
    End Function
    Public Sub Print(str As String)
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
    Public X, Y, GCost, HCost As Integer
    Public Parent As Node
    Public Sub Print(letter As String)
        Console.SetCursorPosition(X, Y)
        Console.Write(letter)
    End Sub
    Public Sub New(xPoint As Integer, yPoint As Integer)
        X = xpoint
        Y = ypoint
    End Sub
    Function WithinLimits(limits() As Integer)
        If Me.X >= limits(0) And Me.X <= limits(2) And Me.Y >= limits(1) And Me.Y <= limits(3) Then Return True
        Return False
    End Function
    Public Sub Update(xPoint As Integer, yPoint As Integer)
        X = xpoint
        Y = ypoint
    End Sub
    Function IsDeadEnd(availablePath As List(Of Node))
        Dim curNode As New Node(Me.X, Me.Y)
        Dim neighbours As List(Of Node) = GetNeighbours(curNode, availablePath)
        If neighbours.Count = 1 Then Return True
        Return False
    End Function
    Function IsJunction(availablePath As List(Of Node))
        Dim curNode As New Node(Me.X, Me.Y)
        Dim neighbours As List(Of Node) = GetNeighbours(curNode, availablePath)
        If neighbours.Count >= 3 Then Return True
        Return False
    End Function
    Function Adjacent(checknode As Node)
        Dim curNode As New Node(Me.X, Me.Y)
        curNode.Update(Me.X, Me.Y - 1)
        If curNode.Equals(checknode) Then Return True
        curNode.Update(Me.X + 2, Me.Y)
        If curNode.Equals(checknode) Then Return True
        curNode.Update(Me.X, Me.Y + 1)
        If curNode.Equals(checknode) Then Return True
        curNode.Update(Me.X - 2, Me.Y)
        If curNode.Equals(checknode) Then Return True
        Return False
    End Function
    Public Function FCost()
        Return GCost + HCost
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
