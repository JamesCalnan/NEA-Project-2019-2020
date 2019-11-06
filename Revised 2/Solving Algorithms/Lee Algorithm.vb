Module LeeAlgorithm
    Sub Lee(maze As List(Of Node), showSolving As Boolean, delay As Integer, solvingColour as ConsoleColor)
        Dim start As New Node(maze(maze.Count - 2).X, maze(maze.Count - 2).Y)
        Dim goal As New Node(maze(maze.Count - 1).X, maze(maze.Count - 1).Y)
        Dim visitedNode As New Dictionary(Of Node, Boolean)
        Dim nodeValues As New Dictionary(Of Node, Integer)
        Dim i = 0
        For Each node In maze
            visitedNode(node) = False
            nodeValues(node) = Int32.MaxValue
        Next
        Dim unfinishedNodes As New Queue(Of Node)
        nodeValues(start) = i
        visitedNode(start) = True
        unfinishedNodes.Enqueue(start)
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.Black
        SetBoth(solvingColour)
        Dim timer As Stopwatch = Stopwatch.StartNew
        While unfinishedNodes.Count > 0
            Dim currentNode As Node = unfinishedNodes.Dequeue
            For Each adjacentNode As Node In GetNeighbours(currentNode, maze)
                If visitedNode(adjacentNode) Then Continue For
                If adjacentNode.Equals(goal) Then Exit While
                nodeValues(adjacentNode) = i
                If showSolving Then adjacentNode.Print($"XX")
                unfinishedNodes.Enqueue(adjacentNode)
                visitedNode(adjacentNode) = True
            Next
            If showSolving Then Threading.Thread.Sleep(delay)
            i += 1
        End While
        BacktrackUsingWavePropagation(nodeValues, start, goal, maze)
        Console.SetCursorPosition(0, Console.WindowHeight - 1)
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.Black
        Console.Write($"Time Taken to Solve   {timer.Elapsed.TotalSeconds}")
        SetBoth(ConsoleColor.Black)
        Console.ReadKey()
    End Sub
    Sub LeeDoubleFan(maze As List(Of Node), showSolving As Boolean, delay As Integer, solvingColour as ConsoleColor)
        Dim start As New Node(maze(maze.Count - 2).X, maze(maze.Count - 2).Y)
        Dim goal As New Node(maze(maze.Count - 1).X, maze(maze.Count - 1).Y)
        Dim visitedNode As New Dictionary(Of Node, Boolean)
        Dim nodeValues As New Dictionary(Of Node, Integer)
        dim goalWave as new list(of Node)
        dim startWave as new List(Of Node)
        Dim i = 0
        For Each node In maze
            visitedNode(node) = False
            nodeValues(node) = Int32.MaxValue
        Next
        Dim unfinishedNodesStart,unfinishedNodesEnd As New Queue(Of Node)
        nodeValues(start) = i
        visitedNode(start) = True
        goalWave.Add(goal)
        startWave.Add(start)
        unfinishedNodesStart.Enqueue(start)
        unfinishedNodesEnd.Enqueue(goal)
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.Black
        SetBoth(solvingColour)
        Dim timer As Stopwatch = Stopwatch.StartNew
        While unfinishedNodesStart.Count > 0 and unfinishedNodesEnd.count > 0
            Dim currentNodeStart As Node = unfinishedNodesStart.Dequeue
            For Each adjacentNode As Node In GetNeighbours(currentNodeStart, maze)
                If visitedNode(adjacentNode) Then Continue For
                if goalWave.Contains(adjacentNode)
                    SetBoth(ConsoleColor.Magenta)
                    adjacentNode.Print("XX")
                    Console.ReadKey()
                End If
                If adjacentNode.Equals(goal) or unfinishedNodesEnd.Contains(adjacentNode) or goalWave.Contains(adjacentNode)  Then Exit While
                nodeValues(adjacentNode) = i
                SetBoth(ConsoleColor.Red)
                If showSolving Then adjacentNode.Print($"XX")
                unfinishedNodesStart.Enqueue(adjacentNode)
                visitedNode(adjacentNode) = True
                startWave.Add(adjacentNode)
            Next
            Dim currentNodeTarget As Node = unfinishedNodesEnd.Dequeue
            For Each adjacentNode As Node In GetNeighbours(currentNodeTarget, maze)
                If visitedNode(adjacentNode) Then Continue For
                if startWave.Contains(adjacentNode)
                    SetBoth(ConsoleColor.Magenta)
                    adjacentNode.Print("XX")
                    Console.ReadKey()
                End If
                If adjacentNode.Equals(start) or unfinishedNodesstart.Contains(adjacentNode) or startWave.Contains(adjacentNode) Then Exit While
                nodeValues(adjacentNode) = i
                SetBoth(ConsoleColor.Red)
                If showSolving Then adjacentNode.Print($"XX")
                unfinishedNodesEnd.Enqueue(adjacentNode)
                visitedNode(adjacentNode) = True
                goalWave.Add(adjacentNode)
            Next
            If showSolving Then Threading.Thread.Sleep(delay)
            i += 1
        End While
        BacktrackUsingWavePropagation(nodeValues, start, goal, maze)
        Console.SetCursorPosition(0, Console.WindowHeight - 1)
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.Black
        Console.Write($"Time Taken to Solve   {timer.Elapsed.TotalSeconds}")
        SetBoth(ConsoleColor.Black)
        Console.ReadKey()
    End Sub
    Sub BacktrackUsingWavePropagation(nodeValues As Dictionary(Of Node, Integer), start As Node, goal As Node, maze As List(Of Node))
        Dim path As New List(Of Node)
        Dim finalNode As New Node(start.X, start.Y)
        start.Update(start.X, start.Y + 1)
        Dim currentNode As Node = goal
        SetBoth(ConsoleColor.Green)
        Do
            Dim lowestValueNode As Node = currentNode
            lowestValueNode.Print("XX")
            For Each neighbour As Node In GetNeighbours(currentNode, maze)
                If nodeValues(neighbour) < nodeValues(lowestValueNode) Then lowestValueNode = neighbour
            Next
            currentNode = lowestValueNode
        Loop Until currentNode.Equals(start)
        start.Print("XX")
        finalNode.Print("XX")
    End Sub
End Module
