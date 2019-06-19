Imports System.Drawing
Imports Revised_2

Module Module1

    Sub Main()

        Console.CursorVisible = False
        SetColour(ConsoleColor.White)
        Dim MenuOptions() As String = {"Recursive Backtracker", "Hunt and Kill", "Aldous-Broder", "Prim's", "Exit"}
        Menu(MenuOptions)




        Console.ReadKey()
        Dim Limits() As Integer = {5, 2, Console.WindowWidth / 2, Console.WindowHeight / 2}
        Dim path As List(Of Node) = HuntAndKill(Limits, 20)


        Console.ReadKey()
    End Sub
    Sub SetBackGroundColour(ByVal colour As ConsoleColor)
        Console.BackgroundColor = colour
    End Sub
    Sub SetColour(ByVal colour As ConsoleColor)
        Console.ForegroundColor = colour
    End Sub
    Function GetStrInput(ByVal msg As String)
        Console.Write(msg)
        Dim input As String = Console.ReadLine
        Return input
    End Function
    Function GetIntInput(ByVal msg As String)
        SetColour(ConsoleColor.White)
        Console.Write(msg)
        Dim input As Integer = Console.ReadLine
        Return input
    End Function
    Sub GetMazeInfo(ByRef Width As Integer, ByRef Height As Integer, ByRef DelayMS As Integer, ByRef Limits() As Integer)
        Console.Clear()
        DelayMS = GetIntInputArrowKeys("Delay when making the Maze: ")
        Width = GetIntInputArrowKeys("Width of the Maze: ")
        If Width Mod 2 = 0 Then Width += 1
        Height = GetIntInputArrowKeys("Height of the Maze: ")
        If Height Mod 2 = 0 Then Height += 1
        Limits = {1, 2, Width, Height}
        Console.Clear()
    End Sub
    Sub MsgColour(ByVal Msg As String, ByVal Colour As ConsoleColor)
        SetColour(Colour)
        Console.WriteLine(Msg)
        SetColour(ConsoleColor.White)
    End Sub
    Function GetIntInputArrowKeys(ByVal message As String)
        Console.Write(message)
        SetColour(ConsoleColor.Yellow)
        Dim cursorleft, cursortop As Integer
        cursorleft = Console.CursorLeft
        cursortop = Console.CursorTop
        Console.SetCursorPosition(cursorleft, cursortop)
        Console.Write("0")
        Dim current As Integer = 0
        While 1
            Dim key = Console.ReadKey
            Select Case key.Key.ToString
                Case "UpArrow"
                    current += 1
                Case "DownArrow"
                    current -= 1
                    If current < 0 Then current = 0
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
    Sub Menu(ByVal arr() As String)
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
            Select Case key.Key.ToString
                Case "DownArrow"
                    y += 1
                    If y = arr.Count Then y = 0
                Case "UpArrow"
                    y -= 1
                    If y = -1 Then y = arr.Count - 1
                Case "Enter"
                    If y = 0 Then
                        Dim Width, Height, DelayMS, Limits() As Integer
                        GetMazeInfo(Width, Height, DelayMS, Limits)
                        Dim AvailablePath As List(Of Node) = RecursiveBacktracker(Limits, DelayMS)
                        SetBackGroundColour(ConsoleColor.Black)
                        Dim YPosAfterMaze As Integer = Console.CursorTop
                        DisplayAvailablePositions(AvailablePath.Count)
                        Console.SetCursorPosition(0, YPosAfterMaze + 2)
                        Console.CursorVisible = True
                        Console.Write("Do you want to use A: A* or B: Dijstras ")
                        Console.CursorVisible = False
                        Dim input As String = Console.ReadLine.ToUpper
                        If input = "A" Then
                            astar(AvailablePath)
                            Console.ReadKey()
                        Else
                            Console.Clear()
                            Console.Write("Option not read yet")
                        End If
                    ElseIf y = 1 Then
                        Dim Width, Height, DelayMS, Limits() As Integer
                        GetMazeInfo(Width, Height, DelayMS, Limits)
                        Dim AvailablePath As List(Of Node) = HuntAndKill(Limits, DelayMS)
                        SetBackGroundColour(ConsoleColor.Black)
                        Dim YPosAfterMaze As Integer = Console.CursorTop
                        DisplayAvailablePositions(AvailablePath.Count)
                        Console.SetCursorPosition(0, YPosAfterMaze + 2)
                        Console.CursorVisible = True
                        Console.Write("Do you want to use A: A* or B: Dijstras ")
                        Console.CursorVisible = False
                        Dim input As String = Console.ReadLine.ToUpper
                        If input = "A" Then
                            astar(AvailablePath)
                            Console.ReadKey()
                        Else
                            Console.Clear()
                            Console.Write("Option not read yet")
                        End If
                    ElseIf y = arr.Count - 1 Then
                        End
                    Else
                        Console.Clear()
                        Console.WriteLine("Option not Ready Yet")
                        Console.ReadKey()
                        Console.Clear()
                    End If

                    Console.Clear()
                    MsgColour("What Maze Generation Algorithm do you want to use: ", ConsoleColor.Yellow)
            End Select
            SetColour(ConsoleColor.White)
            Dim Count As Integer = 1
            For i = 0 To arr.Count - 1
                Console.SetCursorPosition(0, i + 1 + CurrentCol)
                Console.Write("                                      ")
            Next
            For Each MenuOption In arr
                Console.SetCursorPosition(0, Count + CurrentCol)
                Console.Write($" {MenuOption}")
                Count += 1
            Next
            Console.SetCursorPosition(0, y + 1)
            MsgColour($"> {arr(y)}", ConsoleColor.Green)
        End While
    End Sub
    Sub astar(ByRef availiblepath As List(Of Node))
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.Black
        Console.SetCursorPosition(20, 0)

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
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
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
                Dim Time As String = $"Time Taken to solve: {stopwatch.Elapsed.TotalSeconds} seconds"
                RetracePath(start, current, Time)
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
                    'If neighbour.IsPresent(availiblepath) Then
                    '    neighbour.Print("██")
                    '    'Threading.Thread.Sleep(5)
                    'End If
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
    Sub RetracePath(ByVal startnode As Node, ByVal endnode As Node, ByVal timetaken As String)

        Dim path As New List(Of Node)
        Dim current As Node = endnode
        While Not (current.X = startnode.X And current.Y = startnode.Y)
            path.Add(current)
            current = current.parent
        End While
        Dim mess As String = ($"Path length:{path.Count - 1}   {timetaken}")
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.BackgroundColor = ConsoleColor.Black
        Console.SetCursorPosition(Console.WindowWidth / 2 - mess.Count / 2, Console.WindowHeight - 1)
        Console.Write(mess)
        Console.BackgroundColor = ConsoleColor.Green
        Console.ForegroundColor = ConsoleColor.Green
        path.Reverse()
        For Each node In path
            node.Print("██")
            Threading.Thread.Sleep(2)
        Next
        Console.BackgroundColor = ConsoleColor.Black

    End Sub
    Function HuntAndKill(ByVal Limits() As Integer, ByVal delay As Integer)
        Console.BackgroundColor = ConsoleColor.White
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim VisitedList, Stack, VisitedListAndWall As New List(Of Cell)
        Dim ReturnablePath As New List(Of Node)
        Dim Width As Integer = Limits(2) - Limits(0)
        Dim Height As Integer = Limits(3) - Limits(1)
        Dim xCount As Integer
        While True
            Console.BackgroundColor = ConsoleColor.White
            If IsNothing(CurrentCell) Then Exit While
            If NeighboursAvailable(CurrentCell, VisitedList, Limits) Then
                Dim TemporaryCell As Cell = Neighbour(CurrentCell, VisitedList, Limits)
                If Not TemporaryCell.Usable Then
                    ReturnablePath.Add(New Node(TemporaryCell.X, TemporaryCell.Y))
                    VisitedListAndWall.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                    VisitedList.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                    Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)
                    CurrentCell = TemporaryCell
                    ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                    VisitedListAndWall.Add(New Cell(WallCell.X, WallCell.Y))
                    TemporaryCell.Print("██")
                    WallCell.Print("██")
                    Threading.Thread.Sleep(delay)
                End If
            ElseIf CurrentCell Is Nothing Then
                Exit While
            Else
                Dim list As New List(Of Cell)
                Dim ContinueFor As Boolean = True
                For y = Limits(1) To Limits(3) Step 2
                    xCount = 0
                    For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                        Dim newcell As New Cell(x, y)
                        Console.ForegroundColor = ConsoleColor.Cyan
                        Console.SetCursorPosition(x, y)
                        Console.Write("██")
                        Console.SetCursorPosition(x + 2, y)
                        If Console.CursorLeft < Limits(2) Then Console.Write("██")
                        Console.ForegroundColor = ConsoleColor.White
                        Dim AdjancencyList As Integer() = AdjacentCheck(newcell, VisitedList)
                        CurrentCell = PickAdjancentCell(newcell, AdjancencyList)
                        If CurrentCell IsNot Nothing Then
                            Dim WallCell As Cell = MidPoint(newcell, CurrentCell)
                            ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                            ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
                            WallCell.Print("██")
                            CurrentCell.Print("██")
                            ContinueFor = False
                            xCount = x
                            Exit For
                        End If
                        xCount = x
                    Next
                    If ContinueFor = False Then Exit For
                    For i = Limits(0) + 3 To xCount Step 2
                        Dim tempcell As New Cell(i, y)
                        If Not tempcell.IsPresent(VisitedListAndWall) Then
                            Console.ForegroundColor = ConsoleColor.Black
                            Console.BackgroundColor = ConsoleColor.Black
                            tempcell.Print("  ")
                        Else
                            Console.BackgroundColor = ConsoleColor.White
                            Console.ForegroundColor = ConsoleColor.White
                            tempcell.Print("██")
                        End If
                    Next
                Next
            End If
        End While
        ReturnablePath.Add(New Node(Limits(0) + 3, Limits(1) - 1))
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        ReturnablePath.Add(New Node(Limits(2) - 1, Limits(3)))
        ReturnablePath(ReturnablePath.Count - 1).Print("██")
        Return ReturnablePath
    End Function
    Function PickAdjancentCell(ByVal cell As Cell, ByVal adjancencylist() As Integer)
        Dim ReturnCell As Cell
        Dim Neighbours As New List(Of Cell)
        If adjancencylist(0) = 1 Then
            Neighbours.Add(New Cell(cell.X, cell.Y - 1 * 2))
        End If
        If adjancencylist(1) = 1 Then
            Neighbours.Add(New Cell(cell.X + 1 * 4, cell.Y))
        End If
        If adjancencylist(2) = 1 Then
            Neighbours.Add(New Cell(cell.X, cell.Y + 1 * 2))
        End If
        If adjancencylist(3) = 1 Then
            Neighbours.Add(New Cell(cell.X - 1 * 4, cell.Y))
        End If
        Dim R As New Random
        If Neighbours.Count > 0 Then
            ReturnCell = Neighbours(R.Next(0, Neighbours.Count))
        End If
        Return ReturnCell
    End Function
    Function AdjacentCheck(ByVal cell As Cell, ByVal visitedcells As List(Of Cell))
        Dim Adjancent() As Integer = {0, 0, 0, 0}
        '0 = Top
        '1 = Right
        '2 = Bottom
        '3 = Left
        Dim Neighbours As New List(Of Cell)
        Dim TempCell1 As New Cell(cell.X, cell.Y - 2)
        If TempCell1.IsPresent(visitedcells) And Not cell.IsPresent(visitedcells) Then Adjancent(0) = 1
        Dim TempCell2 As New Cell(cell.X + 4, cell.Y)
        If TempCell2.IsPresent(visitedcells) And Not cell.IsPresent(visitedcells) Then Adjancent(1) = 1
        Dim TempCell3 As New Cell(cell.X, cell.Y + 2)
        If TempCell3.IsPresent(visitedcells) And Not cell.IsPresent(visitedcells) Then Adjancent(2) = 1
        Dim TempCell4 As New Cell(cell.X - 4, cell.Y)
        If TempCell4.IsPresent(visitedcells) And Not cell.IsPresent(visitedcells) Then Adjancent(3) = 1
        Return Adjancent
    End Function
    Function RecursiveBacktracker(ByVal Limits() As Integer, ByVal Delay As Integer)
        Console.BackgroundColor = ConsoleColor.White
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)

        Dim PrevCell, WallPrev As New Cell(Limits(0) + 3, Limits(1) + 2)

        Dim VisitedList, Stack As New List(Of Cell)
        Dim ReturnablePath As New List(Of Node)
        While True
            Console.BackgroundColor = ConsoleColor.White
            Console.ForegroundColor = ConsoleColor.White

            PrevCell.Print("██")
            If NeighboursAvailable(CurrentCell, VisitedList, Limits) Then

                Dim TemporaryCell As Cell = Neighbour(CurrentCell, VisitedList, Limits)
                If Not TemporaryCell.Usable Then
                    ReturnablePath.Add(New Node(TemporaryCell.X, TemporaryCell.Y))

                    VisitedList.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))

                    Stack.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))

                    Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)

                    CurrentCell = TemporaryCell
                    ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))


                    Console.BackgroundColor = ConsoleColor.White
                    Console.ForegroundColor = ConsoleColor.White

                    PrevCell.Print("██")
                    WallCell.Print("██")


                    Console.BackgroundColor = ConsoleColor.Blue
                    Console.ForegroundColor = ConsoleColor.Blue
                    TemporaryCell.Print("██")



                    PrevCell = CurrentCell
                    WallPrev = WallCell


                End If
            ElseIf Stack.Count > 1 Then
                CurrentCell = CurrentCell.Pop(Stack)
                Console.BackgroundColor = ConsoleColor.White
                Console.ForegroundColor = ConsoleColor.White
                PrevCell.Print("██")
                Console.BackgroundColor = ConsoleColor.Blue
                Console.ForegroundColor = ConsoleColor.Blue

                CurrentCell.Print("██")
                Console.ForegroundColor = ConsoleColor.White
                PrevCell = CurrentCell
            Else
                Exit While
            End If

            Threading.Thread.Sleep(Delay)
        End While
        Console.BackgroundColor = ConsoleColor.White
        Console.ForegroundColor = ConsoleColor.White

        PrevCell.Print("██")
        WallPrev.Print("██")
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
        'left
        Dim checkx As Integer = current.X + -1 * 4
        Dim checky As Integer = current.Y + 0 * 2
        Dim newPoint As New Cell(checkx, checky)
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            If Not newPoint.IsPresent(visited) Then
                neighbours.Add(New Cell(checkx, checky))
            End If
        End If
        'up
        checkx = current.X + 0 * 4
        checky = current.Y + -1 * 2
        Dim newPoint2 As New Cell(checkx, checky)
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            If Not newPoint2.IsPresent(visited) Then
                neighbours.Add(New Cell(checkx, checky))
            End If
        End If
        'down
        checkx = current.X + 0 * 4
        checky = current.Y + 1 * 2
        Dim newPoint3 As New Cell(checkx, checky)
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            If Not newPoint3.IsPresent(visited) Then
                neighbours.Add(New Cell(checkx, checky))
            End If
        End If
        'right
        checkx = current.X + 1 * 4
        checky = current.Y + 0 * 2
        Dim newPoint4 As New Cell(checkx, checky)
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            If Not newPoint4.IsPresent(visited) Then
                neighbours.Add(New Cell(checkx, checky))
            End If
        End If


        'x: -1   Y: 0   LEFT
        'x: 0    Y: -1  UP
        'x: 0    Y: 1   DOWN
        'x: 1    Y: 0   RIGHT

        Dim r As New Random
        Randomize()
        Return neighbours(r.Next(0, neighbours.Count))
    End Function
    Function NeighboursAvailable(ByVal current As Cell, ByVal visited As List(Of Cell), ByVal Limits() As Integer)


        Dim newpoint As New Cell(current.X - 4, current.Y)
        Dim checkx As Integer = current.X - 4
        Dim checky As Integer = current.Y
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            If Not newpoint.IsPresent(visited) Then
                Return True
            End If
        End If

        Dim newpoint1 As New Cell(current.X, current.Y - 2)
        checkx = current.X
        checky = current.Y - 2
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            If Not newpoint1.IsPresent(visited) Then
                Return True
            End If
        End If

        Dim newpoint2 As New Cell(current.X, current.Y + 2)
        checkx = current.X
        checky = current.Y + 2
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            If Not newpoint2.IsPresent(visited) Then
                Return True
            End If
        End If


        Dim newpoint3 As New Cell(current.X + 4, current.Y)
        checkx = current.X + 4
        checky = current.Y
        If checkx >= Limits(0) And checkx <= Limits(2) And checky >= Limits(1) And checky <= Limits(3) Then
            If Not newpoint3.IsPresent(visited) Then
                Return True
            End If
        End If




        Return False
    End Function 'TODO: simplify
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
