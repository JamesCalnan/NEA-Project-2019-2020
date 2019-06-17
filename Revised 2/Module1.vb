Imports System.Drawing
Imports Revised_2

Module Module1

    Sub Main()

        Console.CursorVisible = False
        SetColour(ConsoleColor.White)
        Menu("Recursive Backtracker", "Hunt and Kill", "Aldous-Broder", "Prim's", 4)



        'Console.Write("What do you want to width and height of the maze to be")
        'Console.WriteLine("(  ,  )")







        Console.SetWindowSize(174, 42)
        Console.ReadKey()
        Console.WriteLine(Console.WindowWidth)
        Console.WriteLine(Console.WindowHeight)
        Console.ReadKey()
        Dim Limits() As Integer = {5, 2, Console.WindowWidth - 6, Console.WindowHeight - 3}
        For x = Limits(0) + 3 To Limits(2) + 1 Step 2
            For y = Limits(1) To Limits(3) Step 1
                Console.SetCursorPosition(x, y)
                Console.Write("X")
            Next
        Next
        Dim path As List(Of Node) = RecursiveBacktracker(Limits, 10)
        Console.ReadKey()
        astar(path)
        Console.ReadKey()
    End Sub
    Sub SetColour(ByVal colour As ConsoleColor)
        Console.ForegroundColor = colour
    End Sub
    Sub Menu(ByVal option1 As String, ByVal option2 As String, ByVal option3 As String, ByVal option4 As String, ByVal NumOfOptions As Integer)
        Dim arr() As String = {option1, option2, option3, option4}
        Console.Clear()
        Dim y As Integer = Console.CursorTop

        Console.WriteLine("What Maze Generation Algorithm do you want to use: ")
        SetColour(ConsoleColor.Green)
        Console.WriteLine($"> {option1}")
        SetColour(ConsoleColor.White)
        Console.WriteLine($" {option2}{Environment.NewLine} {option3}{Environment.NewLine} {option4}")
        While 1

            Console.BackgroundColor = ConsoleColor.Black
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "DownArrow"
                    y += 1
                    If y = NumOfOptions Then y = 0
                Case "UpArrow"
                    y -= 1
                    If y = -1 Then y = NumOfOptions - 1
                Case "Enter"
                    If y = 0 Then
                        Dim Width, Height, DelayMS As Integer
                        Console.Clear()
                        Console.Write("Delay when making the Maze: ")
                        DelayMS = Console.ReadLine
                        Console.Write("Width of the Maze: ")
                        Width = Console.ReadLine
                        If Width Mod 2 = 0 Then Width += 1
                        Console.Write("Height of the Maze: ")
                        Height = Console.ReadLine
                        If Height Mod 2 = 0 Then Height += 1
                        Dim limits() As Integer = {1, 2, Width, Height}
                        Console.Clear()
                        Dim Path As List(Of Node) = RecursiveBacktracker(limits, DelayMS)
                        Console.ReadKey()
                        Console.BackgroundColor = ConsoleColor.Black
                        Console.SetCursorPosition(0, Console.CursorTop + 2)
                        Console.Write("Do you want to use A: A* or B: Dijstras")

                        Dim input As String = Console.ReadLine.ToUpper

                        If input = "A" Then
                            astar(Path)
                        Else
                            Console.Clear()
                            Console.Write("Option not read yet")
                        End If

                        Console.ReadKey()
                            Console.Clear()
                        Else
                            Console.Clear()
                        Console.WriteLine("Option not Ready Yet")
                        Console.ReadKey()
                        Console.Clear()
                    End If
            End Select
            Dim Count As Integer = 1
            For i = 0 To arr.Count - 1
                Console.SetCursorPosition(0, i + 1)
                Console.Write("                              ")
            Next
            For Each MenuOption In arr
                Console.SetCursorPosition(0, Count)
                Console.Write($" {MenuOption}")
                Count += 1
            Next
            Console.SetCursorPosition(0, y + 1)
            Select Case y
                Case 0
                    SetColour(ConsoleColor.Green)
                    Console.WriteLine($"> {option1}")
                    SetColour(ConsoleColor.White)
                Case 1
                    SetColour(ConsoleColor.Green)
                    Console.WriteLine($"> {option2}")
                    SetColour(ConsoleColor.White)
                Case 2
                    SetColour(ConsoleColor.Green)
                    Console.WriteLine($"> {option3}")
                    SetColour(ConsoleColor.White)
                Case 3
                    SetColour(ConsoleColor.Green)
                    Console.WriteLine($"> {option4}")
                    SetColour(ConsoleColor.White)
            End Select
        End While
    End Sub




    Sub astar(ByRef availiblepath As List(Of Node))
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.Black
        Console.SetCursorPosition(20, 0)
        Console.Write(availiblepath.Count)

        Console.ForegroundColor = ConsoleColor.Red
        Dim start As New Node(availiblepath(availiblepath.Count - 2).X, availiblepath(availiblepath.Count - 2).Y)
        Dim target As New Node(availiblepath(availiblepath.Count - 1).X, availiblepath(availiblepath.Count - 1).Y)
        Console.BackgroundColor = ConsoleColor.Red
        Console.ForegroundColor = ConsoleColor.Red
        target.Print("██")
        Dim pathfound As Boolean = False
        Console.BackgroundColor = ConsoleColor.Green
        Console.ForegroundColor = ConsoleColor.Green
        start.Print("██")
        Dim openSet, closedSet, unused As New List(Of Node)
        Dim current As New Node(start.X, start.Y)
        Dim grid As New nGrid
        openSet.Add(current)
        Console.BackgroundColor = ConsoleColor.Yellow
        Console.ForegroundColor = ConsoleColor.Yellow
        While openSet.Count > 0
            current = openSet(0)
            For i = 1 To openSet.Count - 1
                If openSet(i).fCost() <= current.fCost() Or openSet(i).hCost = current.hCost Then
                    If openSet(i).hCost < current.hCost Then current = openSet(i)
                End If
            Next 'finding node with the lowest fcost in the openset
            openSet.Remove(current)
            closedSet.Add(current)
            If current.X = target.X And current.Y = target.Y Then
                RetracePath(start, current)
                pathfound = True
                Exit While
            End If
            For Each neighbour As Node In grid.GetNeighbours(current, availiblepath, False)
                If Not neighbour.Walkable Or neighbour.IsPresent(closedSet) Then Continue For
                Dim newmovementcost As Single = current.gCost + GetDistance(current, neighbour)
                If newmovementcost < neighbour.gCost Or Not neighbour.IsPresent(openSet) Then
                    neighbour.gCost = newmovementcost
                    neighbour.hCost = GetDistance(neighbour, target)
                    neighbour.parent = current
                    If neighbour.IsPresent(availiblepath) Then
                        neighbour.Print("██")
                        'Threading.Thread.Sleep(5)
                    End If
                    If Not neighbour.IsPresent(openSet) Then
                        openSet.Add(neighbour)
                    End If
                End If
            Next
            Remove(availiblepath, current)
        End While
        If pathfound = False Then
            Dim mess As String = "No Path Availible"
            Console.ForegroundColor = ConsoleColor.Red
            Console.BackgroundColor = ConsoleColor.Black
            Console.SetCursorPosition((Console.WindowWidth / 2) - (mess.Count / 2), 0)
            Console.Write(mess)
        End If
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
    Sub RetracePath(ByVal startnode As Node, ByVal endnode As Node)
        Console.BackgroundColor = ConsoleColor.Green
        Console.ForegroundColor = ConsoleColor.Green
        Dim path As New List(Of Node)
        Dim current As Node = endnode
        While Not (current.X = startnode.X And current.Y = startnode.Y)
            path.Add(current)
            current = current.parent
        End While
        path.Reverse()
        For Each node In path
            node.Print("██")
            Threading.Thread.Sleep(2)
        Next
        Console.BackgroundColor = ConsoleColor.Black
        Dim mess As String = ($"path length:{path.Count - 1}")
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.SetCursorPosition(Console.WindowWidth / 2 - mess.Count / 2, Console.WindowHeight - 1)
        Console.Write(mess)
    End Sub
    Function HuntAndKill(ByVal Limits() As Integer)
        Console.BackgroundColor = ConsoleColor.White
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim VisitedList, Stack As New List(Of Cell)
        Dim ReturnablePath As New List(Of Node)
        Dim CurrentCol, CurrentRow As Integer
        CurrentCol = Limits(1)
        Dim Width As Integer = Limits(2) - Limits(0)
        Dim Height As Integer = Limits(3) - Limits(1)
        While True
            Console.BackgroundColor = ConsoleColor.White
            If NeighboursAvailable(CurrentCell, VisitedList, Limits) Then
                Dim TemporaryCell As Cell = Neighbour(CurrentCell, VisitedList, Limits)
                If Not TemporaryCell.Usable Then
                    ReturnablePath.Add(New Node(TemporaryCell.X, TemporaryCell.Y))
                    VisitedList.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                    Stack.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                    Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)
                    CurrentCell = TemporaryCell
                    ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                    TemporaryCell.Print("██")
                    WallCell.Print("██")
                    Threading.Thread.Sleep(4)
                End If
            Else
                Console.BackgroundColor = ConsoleColor.Green
                For i = 5 To Console.WindowWidth - 6 Step 4
                    For j = 2 To Console.WindowHeight - 3
                        Dim newcell As New Cell(i + 3, j)
                        newcell.Print("AA")
                        'Threading.Thread.Sleep(50)
                        If Not newcell.IsPresent(VisitedList) Then
                            CurrentCell = newcell
                            Exit For
                        ElseIf i + 3 = Console.WindowWidth And j = Console.WindowHeight Then
                        End If
                    Next
                Next


                'For i = Limits(0) To Width + 2 Step 4
                '    For j = 2 To Height Step 2
                '        Dim newcell As New Cell(i + 3, j)
                '        Console.BackgroundColor = ConsoleColor.Green
                '        newcell.Print("AA")
                '        If Not newcell.IsPresent(VisitedList) Then
                '            CurrentCell = newcell
                '            Exit For
                '        End If

                '    Next
                'Next
            End If
        End While
        ReturnablePath.Add(New Node(Limits(0) + 3, Limits(1) - 1))
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        ReturnablePath.Add(New Node(If(Console.WindowWidth Mod 2 <> 0, Limits(2) - 3, Limits(2)), If(Console.WindowHeight Mod 2 = 0, Limits(3), Limits(3) + 1)))
        'ReturnablePath.Add(New Node(If(Console.WindowWidth Mod 2 = 0, Limits(2), Limits(2) + 1), If(Console.WindowHeight Mod 2 = 0, Limits(3) - 1, Limits(3)+1)))
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Return ReturnablePath
    End Function
    Function RecursiveBacktracker(ByVal Limits() As Integer, ByVal Delay As Integer)
        Console.BackgroundColor = ConsoleColor.White
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim PrevCell, WallPrev As New Cell(0, 0)

        Dim VisitedList, Stack As New List(Of Cell)
        Dim ReturnablePath As New List(Of Node)
        While True
            If NeighboursAvailable(CurrentCell, VisitedList, Limits) Then
                Dim TemporaryCell As Cell = Neighbour(CurrentCell, VisitedList, Limits)
                If Not TemporaryCell.Usable Then
                    ReturnablePath.Add(New Node(TemporaryCell.X, TemporaryCell.Y))

                    VisitedList.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))

                    Stack.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))

                    Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)

                    CurrentCell = TemporaryCell
                    ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                    Console.BackgroundColor = ConsoleColor.Blue
                    Console.ForegroundColor = ConsoleColor.Blue
                    PrevCell = CurrentCell
                    WallPrev = WallCell
                    PrevCell.Print("██")
                    WallCell.Print("██")

                    Console.BackgroundColor = ConsoleColor.White
                    Console.ForegroundColor = ConsoleColor.White
                    TemporaryCell.Print("██")

                    WallCell.Print("██")



                    'Threading.Thread.Sleep(Delay)
                End If
            ElseIf Stack.Count > 1 Then
                CurrentCell = CurrentCell.Pop(Stack)
            Else
                Exit While
            End If
        End While

        ReturnablePath.Add(New Node(Limits(0) + 3, Limits(1) - 1))
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        ReturnablePath.Add(New Node(Limits(2) - 3, Limits(3)))
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Return ReturnablePath
    End Function
    Function MidPoint(ByVal cell1 As Cell, ByVal cell2 As Cell)
        Dim x, y As Integer
        x = (cell1.X + cell2.X) / 2
        y = (cell1.Y + cell2.Y) / 2
        Dim newpoint As New Cell(((cell1.X + cell2.X) / 2), ((cell1.Y + cell2.Y) / 2))
        Return newpoint
    End Function
    Function Neighbour(ByVal current As Cell, ByVal visited As List(Of Cell), ByVal Limits() As Integer)
        Dim neighbours As New List(Of Cell)
        'For X1 = -1 To 1
        '    For Y1 = -1 To 1
        '        If X1 = 0 And Y1 = 0 Or X1 = -1 And Y1 = -1 Or X1 = 1 And Y1 = -1 Or X1 = -1 And Y1 = 1 Or X1 = 1 And Y1 = 1 Then Continue For
        '        Dim checkx As Integer = current.X + X1 * 4
        '        Dim checky As Integer = current.Y + Y1 + Y1
        '        Dim newpoint As New Cell(checkx, checky)
        '        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
        '            If Not newpoint.IsPresent(visited) Then
        '                neighbours.Add(New Cell(checkx, checky))
        '            End If
        '        End If
        '        Console.WriteLine($"{X1}   {Y1}")
        '    Next
        'Next
        'Console.ReadKey()

        Dim checkx As Integer = current.X + -1 * 4
        Dim checky As Integer = current.Y
        Dim newPoint As New Cell(checkx, checky)
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            If Not newPoint.IsPresent(visited) Then
                neighbours.Add(New Cell(checkx, checky))
            End If
        End If

        checkx = current.X
        checky = current.Y + -1
        Dim newPoint2 As New Cell(checkx, checky)
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            If Not newPoint2.IsPresent(visited) Then
                neighbours.Add(New Cell(checkx, checky))
            End If
        End If

        checkx = current.X
        checky = current.Y + 1
        Dim newPoint3 As New Cell(checkx, checky)
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            If Not newPoint3.IsPresent(visited) Then
                neighbours.Add(New Cell(checkx, checky))
            End If
        End If

        checkx = current.X + 1
        checky = current.Y
        Dim newPoint4 As New Cell(checkx, checky)
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            If Not newPoint4.IsPresent(visited) Then
                neighbours.Add(New Cell(checkx, checky))
            End If
        End If


        '        'x: -1   Y: 0
        'x: 0    Y: -1
        'x: 0    Y: 1
        'x: 1    Y: 0




        If neighbours.Count > 0 Then
            Dim r As New Random
            Randomize()
            Return neighbours(r.Next(0, neighbours.Count))
        End If
        Dim np As New Cell(0, 0)
        np.Usable = False
        Return np
    End Function
    Function NeighboursAvailable(ByVal current As Cell, ByVal visited As List(Of Cell), ByVal Limits() As Integer)
        For X1 = -1 To 1
            For Y1 = -1 To 1
                If X1 = 0 And Y1 = 0 Or X1 = -1 And Y1 = -1 Or X1 = 1 And Y1 = -1 Or X1 = -1 And Y1 = 1 Or X1 = 1 And Y1 = 1 Then Continue For
                Dim checkx As Integer = current.X + X1 + X1 + X1 + X1
                Dim checky As Integer = current.Y + Y1 + Y1
                Dim newpoint As New Cell(checkx, checky)
                If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
                    If Not newpoint.IsPresent(visited) Then
                        Return True
                    End If
                End If
            Next
        Next
        Return False
    End Function
    Sub Remove(ByRef list As List(Of Node), ByVal node As Node)
        If list.FindIndex(Function(p) p.xcord = node.X And p.ycord = node.Y) <> -1 Then list.RemoveAt(list.FindIndex(Function(p) p.xcord = node.X And p.ycord = node.Y))
    End Sub
    Sub Check(ByVal neighbours As List(Of Node), ByVal current As Node, ByVal availablepath As List(Of Node), ByVal currentiteration As Integer)
        'neighbours(currentiteration).Walkable = True
        If current.IsPresent(availablepath) Then
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
    Dim xcord, ycord As Integer
    Public Usable As Boolean
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
    Public Function IsPresent(ByVal list As List(Of Cell))
        If list.Find(Function(p) p.xcord = Me.X And p.ycord = Me.Y) IsNot Nothing Then Return True
        Return False
    End Function
    Public Function Pop(ByVal list As List(Of Cell))
        Dim val As Cell
        val = list(list.Count - 1)
        list.RemoveAt(list.Count - 1)
        Return val
    End Function

    Public Sub Print(ByVal str As String)
        'Console.ForegroundColor = Colour
        Console.SetCursorPosition(Me.X, Me.Y)
        Console.Write(str)
    End Sub
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
    Public Function IsPresent(ByVal list As List(Of Node))
        If list.Find(Function(p) p.xcord = Me.X And p.ycord = Me.Y) IsNot Nothing Then Return True
        Return False
    End Function

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
End Class
