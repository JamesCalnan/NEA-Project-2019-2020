Imports System.Drawing
Module Image
    Function getImageBackgroundColour(image As Bitmap) As String
        Return image.GetPixel(1, 1).Name
    End Function

    Function getImagePathColour(image As Bitmap, multiplier As Integer, imageBackGroundColour As String) As String
        Dim colourDictionary As New Dictionary(Of String, Integer)
        For y = 1 To image.Height Step multiplier * 2
            For x = 1 To image.Width Step multiplier * 2
                If image.GetPixel(x, y).Name <> imageBackGroundColour Then
                    Dim currentPixelName = image.GetPixel(x, y).Name
                    If colourDictionary.ContainsKey(currentPixelName) Then
                        colourDictionary(currentPixelName) += 1
                    Else
                        colourDictionary.Add(currentPixelName, 1)
                    End If
                End If
            Next
        Next
        Dim maxKey As String
        Dim previousValue As Integer = colourDictionary.Values(0)
        maxKey = colourDictionary.Keys(0)
        For Each thing In From thing1 In colourDictionary Where thing1.Value > previousValue
            previousValue = thing.Value
            maxKey = thing.Key
        Next
        Return maxKey
    End Function

    Function LoadMazePng(tempArr() As String, ByRef previousAlgorithm As String, pathColour As ConsoleColor, backGroundColour As ConsoleColor, SolvingColour As ConsoleColor)
        'loading a big maze twice exceeds memory limit
        Console.Clear()
        Console.Write("File Name of the maze to load (don't include .png): ")
        Dim filename As String = Console.ReadLine
        If System.IO.File.Exists($"{filename}.png") Then
            Console.Clear()
            Dim maze As New List(Of Node)
            Dim path As New List(Of Node)
            Const multiplier As Integer = 8
            Dim pathOnMaze = False
            Dim image As New Bitmap($"{filename}.png")
            Dim imageBackgroundColour = getImageBackgroundColour(image)
            Dim imagePathColour = getImagePathColour(image, multiplier, imageBackgroundColour)
            Dim greatestX = 0
            Dim greatestY = 0
            Dim greatestAllowedX As Integer = Console.WindowWidth - 56
            Dim greatestAllowedY As Integer = Console.WindowHeight - 5
            For y = 1 To image.Height Step multiplier * 2
                For x = 1 To image.Width Step multiplier * 2
                    Dim pixel As Color = image.GetPixel(x, y)
                    If pixel.Name = imagePathColour Then
                        maze.Add(New Node(x / multiplier, y / (multiplier * 2)))
                        If x / multiplier > greatestX Then greatestX = x / multiplier
                        If y / (multiplier * 2) > greatestY Then greatestY = y / (multiplier * 2)
                        If x / multiplier > greatestAllowedX Or y / (multiplier * 2) > greatestAllowedY Then
                            Return Nothing
                        End If
                    End If
                    If pixel.Name <> imagePathColour And pixel.Name <> imageBackgroundColour Then
                        pathOnMaze = True
                        path.Add(New Node(x / multiplier, y / (multiplier * 2)))
                    End If
                Next
            Next
            image.Dispose()
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
                    maze.AddRange(path)
                    maze.Add(start)
                    maze.Add(finish)
                    Console.ReadKey()
                Else
                    path.RemoveAt(0)
                    path.RemoveAt(path.Count - 1)
                    maze.AddRange(path)
                    maze.Add(start)
                    maze.Add(finish)
                    SetBoth(ConsoleColor.White)
                    PrintMazeHorizontally(maze, greatestX, greatestY)
                    PrintStartandEnd(maze)
                    'Solving of the maze goes here
                    Console.BackgroundColor = ConsoleColor.Black
                    Console.ForegroundColor = ConsoleColor.White
                    Dim input As String = SolvingMenu(tempArr, "What would you like to do with the maze", greatestX + 3, 3)
                    SolvingInput(input, True, greatestY, 0, maze, "", pathColour, backGroundColour, SolvingColour)
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
                Dim input As String = SolvingMenu(tempArr, "What would you like to do with the maze", greatestX + 3, 3)
                SolvingInput(input, True, greatestY, 0, maze, "", pathColour, backGroundColour, SolvingColour)
            End If
            Return maze
        Else
            MsgColour("File doesn't exist", ConsoleColor.Red)
        End If
        Return Nothing
    End Function
    Function consoleColourToBrush(colour As ConsoleColor) As Brush
        Select Case colour
            Case ConsoleColor.Black
                Return Brushes.Black
            Case ConsoleColor.Blue
                Return Brushes.DodgerBlue
            Case ConsoleColor.Cyan
                Return Brushes.Cyan
            Case ConsoleColor.Gray
                Return Brushes.SlateGray
            Case ConsoleColor.Green
                Return Brushes.LimeGreen
            Case ConsoleColor.Magenta
                Return Brushes.Magenta
            Case ConsoleColor.Red
                Return Brushes.Red
            Case ConsoleColor.White
                Return Brushes.White
            Case ConsoleColor.Yellow
                Return Brushes.Yellow
            Case ConsoleColor.DarkBlue
                Return Brushes.DarkBlue
            Case ConsoleColor.DarkCyan
                Return Brushes.DarkCyan
            Case ConsoleColor.DarkGray
                Return Brushes.DarkSlateGray
            Case ConsoleColor.DarkGreen
                Return Brushes.DarkGreen
            Case ConsoleColor.DarkMagenta
                Return Brushes.DarkMagenta
            Case ConsoleColor.DarkRed
                Return Brushes.DarkRed
            Case ConsoleColor.DarkYellow
                Return Brushes.OrangeRed
            Case Else
                Return Nothing
        End Select
    End Function
    Sub SaveMazePng(path As List(Of Node), algorithm As String, fileName As String, pathColour As ConsoleColor, backGroundColour As ConsoleColor)
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
        Dim gPathColour As Brush = consoleColourToBrush(pathColour)
        For Each thing In path
            g.FillRectangle(gPathColour, (thing.X) * multiplier, (thing.Y * 2) * multiplier, 2 * multiplier, 2 * multiplier)
        Next
        If solving Then
            Dim myBrush As New SolidBrush(Color.FromArgb(255, 0, 0, 255))
            DFS_IterativeFORFILE(path, bmp, g, multiplier)
            g.FillRectangle(myBrush, (path(path.Count - 2).X) * multiplier, (path(path.Count - 2).Y * 2) * multiplier, 2 * multiplier, 2 * multiplier)
        End If
        'g.FillRectangle(Brushes.Lime, (Path(Path.Count - 1).X) * Multiplier, (Path(Path.Count - 1).Y * 2) * Multiplier, 2 * Multiplier, 2 * Multiplier)
        g.Dispose()
        bmp.Save($"{fileName}.png", System.Drawing.Imaging.ImageFormat.Png)
        bmp.Dispose()
    End Sub


End Module
