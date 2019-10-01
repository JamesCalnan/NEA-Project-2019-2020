Module UserCreateMaze
    Function UserCreateMaze(limits() As Integer, pathColour As ConsoleColor, backGroundColour As ConsoleColor)
        Dim visited As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim unvisitedCells As List(Of Cell) = (From node In visited Select node.Key).ToList()
        SetBoth(backGroundColour)
        If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
        Dim userPath As New List(Of Cell)
        Dim path As New List(Of Node)
        Dim currentPos As Cell = PickRandomStartingCell(limits)
        Dim previousPos As Cell = currentPos
        visited(currentPos) = True
        unvisitedCells.Remove(currentPos)
        path.Add(currentPos.ToNode())
        SetBoth(ConsoleColor.Magenta)
        currentPos.Print("XX")
        While unvisitedCells.Count > 0
            SetBoth(backGroundColour)
            Dim key = Console.ReadKey
            SetBoth(pathColour)
            Select Case key.Key.ToString
                Case "RightArrow"
                    CreateMazeKeyPress(4, 0, currentPos, previousPos, visited, unvisitedCells, path, limits, pathColour)
                Case "LeftArrow"
                    CreateMazeKeyPress(-4, 0, currentPos, previousPos, visited, unvisitedCells, path, limits, pathColour)
                Case "UpArrow"
                    CreateMazeKeyPress(0, -2, currentPos, previousPos, visited, unvisitedCells, path, limits, pathColour)
                Case "DownArrow"
                    CreateMazeKeyPress(0, 2, currentPos, previousPos, visited, unvisitedCells, path, limits, pathColour)
                Case "Escape"
                    Return Nothing
                Case Else
            End Select
            userPath.Add(currentPos)
            SetBoth(Console.BackgroundColor)
            Console.SetCursorPosition(0, 0)
        End While
        path.Add(currentPos.ToNode)
        SetBoth(pathColour)
        currentPos.Print("XX")
        Console.BackgroundColor = ConsoleColor.Black
        Dim choice = HorizontalYesNo(limits(3) + 3, "do you want to add the start and end yourself:   ", True, False, False)
        If choice Then
            path.Add(PickPoint(path, ConsoleColor.Red, pathColour, backGroundColour))
            path.Add(PickPoint(path, ConsoleColor.Green, pathColour, backGroundColour))
        Else
            AddStartAndEnd(path, limits, pathColour)
        End If
        Return path
    End Function
    Function PickPoint(ByRef maze As List(Of Node), colour As ConsoleColor, pathColour As ConsoleColor, backGroundColour As ConsoleColor) As Node
        Dim pointChosen = False
        Dim currentPos = maze(0)
        Dim previousPos = currentPos
        SetBoth(colour)
        currentPos.Print("XX")
        Do
            SetBoth(ConsoleColor.Black)
            Console.SetCursorPosition(0, 0)
            Dim key = Console.ReadKey
            Select Case key.Key.ToString()
                Case "RightArrow"
                    Dim tempNode As New Node(currentPos.X + 2, currentPos.Y)
                    If tempNode.AdjacentToMaze(maze) Then
                        currentPos = tempNode
                        SetBoth(If(maze.Contains(previousPos), pathColour, backGroundColour))
                        previousPos.Print("XX")
                        SetBoth(colour)
                        currentPos.Print("XX")
                        previousPos = currentPos
                    End If
                Case "LeftArrow"
                    Dim tempNode As New Node(currentPos.X - 2, currentPos.Y)
                    If tempNode.AdjacentToMaze(maze) Then
                        currentPos = tempNode
                        SetBoth(If(maze.Contains(previousPos), pathColour, backGroundColour))
                        previousPos.Print("XX")
                        SetBoth(colour)
                        currentPos.Print("XX")
                        previousPos = currentPos
                    End If
                Case "UpArrow"
                    Dim tempNode As New Node(currentPos.X, currentPos.Y - 1)
                    If tempNode.AdjacentToMaze(maze) Then
                        currentPos = tempNode
                        SetBoth(If(maze.Contains(previousPos), pathColour, backGroundColour))
                        previousPos.Print("XX")
                        SetBoth(colour)
                        currentPos.Print("XX")
                        previousPos = currentPos
                    End If
                Case "DownArrow"
                    Dim tempNode As New Node(currentPos.X, currentPos.Y + 1)
                    If tempNode.AdjacentToMaze(maze) Then
                        currentPos = tempNode
                        SetBoth(If(maze.Contains(previousPos), pathColour, backGroundColour))
                        previousPos.Print("XX")
                        SetBoth(colour)
                        currentPos.Print("XX")
                        previousPos = currentPos
                    End If
                Case "Enter"
                    SetBoth(colour)
                    currentPos.Print("XX")
                    Return currentPos
            End Select
        Loop Until pointChosen
        Return Nothing
    End Function

    Sub CreateMazeKeyPress(x As Integer, y As Integer, ByRef currentPos As Cell, ByRef previousPos As Cell, ByRef visited As Dictionary(Of Cell, Boolean), ByRef unvisitedCells As List(Of Cell), ByRef path As List(Of Node), limits() As Integer, pathColour As ConsoleColor)
        Dim tempNode As New Cell(currentPos.X + x, currentPos.Y + y)
        If tempNode.WithinLimits(limits) Then
            currentPos = tempNode
            If Not visited(currentPos) Then
                Dim wallCell As Cell = MidPoint(currentPos, previousPos)
                SetBoth(pathColour)
                wallCell.Print("XX")
                visited(currentPos) = True
                unvisitedCells.Remove(currentPos)
                AddToPath(path, currentPos, wallCell)
            End If
            previousPos.Print("XX")
            SetBoth(ConsoleColor.Magenta)
            currentPos.Print("XX")
            If Not path.Contains(currentPos.ToNode) Then path.Add(currentPos.ToNode)
            previousPos = currentPos
        End If
    End Sub
End Module
