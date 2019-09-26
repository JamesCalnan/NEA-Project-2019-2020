Module LeeAlgorithm
    Sub Lee(maze As List(Of Node), showSolving As Boolean, delay As Integer)
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
        SetBoth(ConsoleColor.Red)
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
