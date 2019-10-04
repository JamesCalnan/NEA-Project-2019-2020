Module User_Create_Map
    Function UsercreateMap(limits() As Integer, pathColour As ConsoleColor, backGroundColour As ConsoleColor)
        Dim WallColour As ConsoleColor = ConsoleColor.White
        Dim wallPresent As New Dictionary(Of Node, Boolean)
        For y = limits(1) To limits(3) Step 1
            For x = limits(0) + 3 To limits(2) - 1 Step 2
                wallPresent(New Node(x, y)) = False
            Next
        Next
        Dim removingWalls As Boolean = False
        Dim AddingWalls As Boolean = False
        Dim currentPos As Node = wallPresent.Keys(0)
        Dim prevPos As Node = currentPos

        While 1
            SetBoth(backGroundColour)
            Dim key = Console.ReadKey
            SetBoth(pathColour)
            Select Case key.Key.ToString
                Case "W"
                    If AddingWalls Then
                        AddingWalls = False
                    Else
                        AddingWalls = True
                        removingWalls = False
                    End If
                Case "R"
                    If Not removingWalls Then
                        removingWalls = True
                        AddingWalls = False
                    Else
                        removingWalls = False
                        AddingWalls = False
                    End If
                Case "RightArrow"
                    WallEditKeyPress(2, 0, currentPos, wallPresent, prevPos, AddingWalls, removingWalls, limits, WallColour)
                Case "LeftArrow"
                    WallEditKeyPress(-2, 0, currentPos, wallPresent, prevPos, AddingWalls, removingWalls, limits, WallColour)
                Case "UpArrow"
                    WallEditKeyPress(0, -1, currentPos, wallPresent, prevPos, AddingWalls, removingWalls, limits, WallColour)
                Case "DownArrow"
                    WallEditKeyPress(0, 1, currentPos, wallPresent, prevPos, AddingWalls, removingWalls, limits, WallColour)
                Case "Enter"
                    Exit While
                Case "Escape"
                    Return Nothing
                Case Else
            End Select
            SetBoth(Console.BackgroundColor)
            Console.SetCursorPosition(0, 0)
        End While
        Dim availableNodes As New List(Of Node)
        For Each thing In wallPresent
            If thing.Value = False Then
                availableNodes.Add(thing.Key)
            End If
        Next
        availableNodes.Add(PickPoint(availableNodes, ConsoleColor.Red, InvertConsoleColour(pathColour), InvertConsoleColour(backGroundColour)))
        availableNodes.Add(PickPoint(availableNodes, ConsoleColor.Green, InvertConsoleColour(pathColour), InvertConsoleColour(backGroundColour)))
        Return availableNodes
    End Function
    Function InvertConsoleColour(colour As ConsoleColor)
        If colour = ConsoleColor.White Then
            Return ConsoleColor.Black
        ElseIf colour = consolecolor.black Then
            Return ConsoleColor.White
        End If
    End Function
    Private Function PickPoint(ByRef maze As List(Of Node), colour As ConsoleColor, pathColour As ConsoleColor, backGroundColour As ConsoleColor) As Node
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

    Private Sub WallEditKeyPress(x As Integer, y As Integer, ByRef currentPos As Node, WallPresent As Dictionary(Of Node, Boolean), ByRef previousPos As Node, addingWalls As Boolean, removingWalls As Boolean, limits() As Integer, wallColour As ConsoleColor)
        Dim tempNode As New Node(currentPos.X + x, currentPos.Y + y)
        If tempNode.WithinLimits(limits) And WallPresent.ContainsKey(tempNode) Then
            'it is on the grid
            currentPos = tempNode
            'current pos is now the valid grid move
            If addingWalls Then
                'adding walls = true
                'need to check if there is already a wall there
                If WallPresent(previousPos) Then
                    'if there is a wall
                    SetBoth(wallColour)
                    previousPos.Print("XX")
                    SetBoth(ConsoleColor.Magenta)
                    currentPos.Print("XX")
                    previousPos = currentPos
                Else
                    'if there isnt a wall, then we need to add one
                    SetBoth(wallColour)
                    previousPos.Print("XX")
                    SetBoth(ConsoleColor.Magenta)
                    currentPos.Print("XX")
                    WallPresent(previousPos) = True
                    previousPos = currentPos
                End If

            Else
                'adding wal = false
                If WallPresent(previousPos) Then
                    'if there is a wall, then the previous point needs to print the wall colour
                    If Not removingWalls Then
                        SetBoth(wallColour)
                        previousPos.Print("XX")
                        SetBoth(ConsoleColor.Magenta)
                        currentPos.Print("XX")
                        previousPos = currentPos
                    Else
                        SetBoth(ConsoleColor.Black)
                        previousPos.Print("XX")
                        SetBoth(ConsoleColor.Magenta)
                        currentPos.Print("XX")
                        WallPresent(previousPos) = False
                        previousPos = currentPos
                    End If
                Else
                    'if there isnt a wall then the previous point needs to print the background colour
                    SetBoth(ConsoleColor.Black)
                    previousPos.Print("XX")
                    SetBoth(ConsoleColor.Magenta)
                    currentPos.Print("XX")
                    previousPos = currentPos
                End If
            End If

        End If
    End Sub
End Module
