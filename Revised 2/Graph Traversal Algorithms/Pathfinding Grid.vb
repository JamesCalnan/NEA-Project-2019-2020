﻿Module Pathfinding_Grid
    Function returnPathfindingGrid(wallColour As consolecolor) As List(Of Node)
        dim chanceOfObstical = GetIntInputArrowKeys("Chance of an obstical at each position (%): ", 100, 0, False)
        Console.Clear()

        Dim availableCells As New List(Of Node)
        Dim start As New Node(5, 3)
        dim xEndValue = 0
        if (Console.WindowWidth - 6) Mod 2 = 0
            xEndValue = console.WindowWidth - 7
        Else 
            xEndValue = console.WindowWidth - 6
        End If
        Dim goal As New Node(xEndValue, Console.WindowHeight - 3)
        Dim invalidNodes As List(Of Node) = returnInvalidNodes(start, goal)
        SetBoth(ConsoleColor.White)
        Dim r As New Random
        For x = 5 To Console.WindowWidth - 5 Step 2
            For y = 3 To Console.WindowHeight - 3
                If r.Next(100) < chanceOfObstical And Not invalidNodes.Contains(New Node(x, y)) Then
                    If x = 5 And y = 3 Or x = If(Console.WindowWidth - 6 Mod 2 = 0, Console.WindowWidth - 5, Console.WindowWidth - 6) And y = Console.WindowHeight - 3 Then Continue For
                    Console.SetCursorPosition(x, y)
                    Console.Write("XX")
                    Continue For
                Else 
                    availableCells.Add(New Node(x, y))
                End If
                
            Next
        Next
        availableCells.Add(start)
        availableCells.Add(goal)
        PrintStartandEnd(availableCells)
        Return availableCells
    End Function
    Function returnInvalidNodes(start As Node, goal As Node) As List(Of Node)
        Dim invalidNodes As New List(Of Node) From {
            New Node(start.X + 2, start.Y),
            New Node(start.X + 4, start.Y),
            New Node(start.X, start.Y + 1),
            New Node(start.X, start.Y + 2),
            New Node(start.X + 2, start.Y + 1),
            New Node(start.X + 4, start.Y + 1),
            New Node(start.X + 2, start.Y + 2),
            New Node(start.X + 4, start.Y + 2),
            New Node(goal.X - 2, goal.Y),
            New Node(goal.X - 4, goal.Y),
            New Node(goal.X, goal.Y - 1),
            New Node(goal.X, goal.Y - 2),
            New Node(goal.X - 2, goal.Y - 1),
            New Node(goal.X - 4, goal.Y - 1),
            New Node(goal.X - 2, goal.Y - 2),
            New Node(goal.X - 4, goal.Y - 2)
        }
        Return invalidNodes
    End Function
End Module
