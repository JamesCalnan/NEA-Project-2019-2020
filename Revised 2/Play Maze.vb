Module PlayMaze
    Sub PlaymazeSubroutine(availablePath As List(Of Node), showPath As Boolean, pathColour as ConsoleColor, backGroundColour as ConsoleColor)
        Dim playerPath As New List(Of Node)
        Dim currentPos As New Node(availablePath(availablePath.Count - 2).X, availablePath(availablePath.Count - 2).Y)
        Dim start As New Node(availablePath(availablePath.Count - 2).X, availablePath(availablePath.Count - 2).Y)
        If Not playerPath.Contains(currentPos) Then playerPath.Add(currentPos)
        Console.ForegroundColor = (ConsoleColor.Magenta)
        currentPos.Print("██")
        Dim previousPos As Node = currentPos
        Dim target As New Node(availablePath(availablePath.Count - 1).X, availablePath(availablePath.Count - 1).Y)
        Console.ForegroundColor = (ConsoleColor.Green)
        target.Print("██")
        Console.CursorVisible = False
        While Not currentPos.Equals(target)
            Dim t As New Node(Console.CursorLeft, Console.CursorTop)
            Dim s As New Node(Console.CursorLeft - 1, Console.CursorTop)
            If availablePath.Contains(t) Or availablePath.Contains(s) Then
                If showPath And availablePath.Contains(s) Or availablePath.Contains(t) Then
                    If playerPath.Contains(t) Or playerPath.Contains(s) Then
                        If availablePath.Contains(t) Or availablePath.Contains(s) Then
                            If showPath Then
                                SetBoth(ConsoleColor.Blue)
                            Else
                                SetBoth(pathColour)
                            End If
                        Else
                            SetBoth(pathColour)
                        End If
                    Else
                        SetBoth(pathColour)
                    End If
                Else
                    If availablePath.Contains(t) Or availablePath.Contains(s) Then
                        SetBoth(pathColour)
                    Else
                        SetBoth(ConsoleColor.Blue)
                    End If
                End If
            Else
                setboth(backGroundColour)
            End If
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "RightArrow"
                    Dim tempNode3 As New Node(currentPos.X + 2, currentPos.Y)
                    PlayMazeKeyPress(currentPos, tempNode3, showPath, previousPos, playerPath, availablePath)
                Case "LeftArrow"
                    Dim tempNode2 As New Node(currentPos.X - 2, currentPos.Y)
                    PlayMazeKeyPress(currentPos, tempNode2, showPath, previousPos, playerPath, availablePath)
                Case "UpArrow"
                    Dim tempNode1 As New Node(currentPos.X, currentPos.Y - 1)
                    PlayMazeKeyPress(currentPos, tempNode1, showPath, previousPos, playerPath, availablePath)
                Case "DownArrow"
                    Dim tempNode As New Node(currentPos.X, currentPos.Y + 1)
                    PlayMazeKeyPress(currentPos, tempNode, showPath, previousPos, playerPath, availablePath)
                Case "Escape"
                    Exit While
                Case Else
            End Select
        End While
        Console.ForegroundColor = (ConsoleColor.Yellow)
        If currentPos.Equals(target) Then
            playerPath.Add(start)
            playerPath.Add(target)
            AStar(playerPath, False, False, 0, ConsoleColor.Black)
            Console.Clear()
            PrintMessageMiddle("EPIC VICTORY ROYALE", Console.WindowHeight / 2, ConsoleColor.Yellow)
            Console.ReadKey()
        End If
    End Sub
    Sub PlayMazeKeyPress(ByRef currentPos As Node, tempNode As Node, showpath As Boolean, ByRef previousPos As Node, ByRef playerPath As List(Of Node), availablePath As List(Of Node))
        If availablePath.Contains(tempNode) Then
            currentPos = tempNode
            If showpath Then
                Console.ForegroundColor = (ConsoleColor.Blue)
            Else
                Console.ForegroundColor = (ConsoleColor.White)
            End If
            previousPos.Print("██")
            Console.ForegroundColor = (ConsoleColor.Magenta)
            currentPos.Print("██")
            previousPos = currentPos
            If Not playerPath.Contains(currentPos) Then playerPath.Add(currentPos)
        End If
    End Sub
End Module
