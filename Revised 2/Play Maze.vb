Module Play_Maze
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
End Module
