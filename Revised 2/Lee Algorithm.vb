Module Lee_Algorithm
    Sub Lee(ByVal Maze As List(Of Node), ByVal ShowSolving As Boolean, ByVal Delay As Integer)
        Dim start As New Node(Maze(Maze.Count - 2).X, Maze(Maze.Count - 2).Y)
        Dim goal As New Node(Maze(Maze.Count - 1).X, Maze(Maze.Count - 1).Y)
        Dim VisitedNode As New Dictionary(Of Node, Boolean)
        Dim NodeValues As New Dictionary(Of Node, Integer)
        Dim i As Integer = 0
        For Each node In Maze
            VisitedNode(node) = False
            NodeValues(node) = Int32.MaxValue
        Next
        Dim UnfinishedNodes As New Queue(Of Node)
        NodeValues(start) = i
        VisitedNode(start) = True
        UnfinishedNodes.Enqueue(start)
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.Black
        SetBoth(ConsoleColor.Red)
        Dim Timer As Stopwatch = Stopwatch.StartNew
        While UnfinishedNodes.Count > 0
            Dim currentNode As Node = UnfinishedNodes.Dequeue
            For Each AdjacentNode As Node In GetNeighbours(currentNode, Maze)
                If VisitedNode(AdjacentNode) Then Continue For
                If AdjacentNode.Equals(goal) Then Exit While
                NodeValues(AdjacentNode) = i
                If ShowSolving Then AdjacentNode.Print($"XX")
                UnfinishedNodes.Enqueue(AdjacentNode)
                VisitedNode(AdjacentNode) = True
            Next
            If ShowSolving Then Threading.Thread.Sleep(Delay)
            i += 1
        End While

        BacktrackUsingWavePropagation(NodeValues, start, goal, Maze)
        Console.SetCursorPosition(0, Console.WindowHeight - 1)
        Console.ForegroundColor = ConsoleColor.White
        Console.BackgroundColor = ConsoleColor.Black
        Console.Write($"Time Taken to Solve   {Timer.Elapsed.TotalSeconds}")
        SetBoth(ConsoleColor.Black)
        Console.ReadKey()
    End Sub
    Sub BacktrackUsingWavePropagation(ByVal NodeValues As Dictionary(Of Node, Integer), ByVal start As Node, ByVal goal As Node, ByVal Maze As List(Of Node))
        Dim Path As New List(Of Node)
        Dim finalNode As New Node(start.X, start.Y)
        start.update(start.X, start.Y + 1)
        Dim CurrentNode As Node = goal
        SetBoth(ConsoleColor.Green)
        Do
            Dim LowestValueNode As Node = CurrentNode
            LowestValueNode.Print("XX")
            For Each neighbour As Node In GetNeighbours(CurrentNode, Maze)
                If NodeValues(neighbour) < NodeValues(LowestValueNode) Then LowestValueNode = neighbour
            Next
            CurrentNode = LowestValueNode
        Loop Until CurrentNode.Equals(start)
        start.Print("XX")
        finalNode.Print("XX")
    End Sub
End Module
