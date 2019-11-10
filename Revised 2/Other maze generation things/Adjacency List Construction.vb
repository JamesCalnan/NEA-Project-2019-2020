Module Adjacency_List_Construction
    Function GetNeededNodes(maze As List(Of Node)) As List(Of Node)
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, Console.WindowHeight - 3)
        Console.Write("Constructing graph")
        Dim newlist As New List(Of Node)
        Console.SetCursorPosition(35, Console.WindowHeight - 3)
        Console.Write($"Progress:")
        Dim stopwatch As Stopwatch = Stopwatch.StartNew
        Dim I = 0
        For Each node In maze
            If CornerOrJunction(node, maze) Then newlist.Add(node)
            I += 1
            Console.SetCursorPosition(45, Console.WindowHeight - 3)
            Console.Write($"{Math.Floor((I / maze.Count) * 100)}%")
        Next
        newlist.Add(maze(maze.Count - 2))
        newlist.Add(maze(maze.Count - 1))
        Console.SetCursorPosition(35, Console.WindowHeight - 3)
        Console.Write($"Time taken: {stopwatch.Elapsed.TotalSeconds}              ")
        Return newlist
    End Function
    Function ConstructAdjacencyList(neededNodes As List(Of Node), maze As List(Of Node)) As Dictionary(Of Node, List(Of Node))
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
        Console.SetCursorPosition(0, Console.WindowHeight - 2)
        Console.Write("Constructing adjacency list")
        Dim adjacenyList As New Dictionary(Of Node, List(Of Node))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew
        Dim I = 0
        For Each Node In neededNodes
            Dim adjacentNodes As New List(Of Node)
            Dim nodeToAdd3 As Node = FindAdjacentNodes(Node, maze, neededNodes, 0, -1)
            If Not IsNothing(nodeToAdd3) Then adjacentNodes.Add(nodeToAdd3)
            Dim nodeToAdd2 As Node = FindAdjacentNodes(Node, maze, neededNodes, 2, 0)
            If Not IsNothing(nodeToAdd2) Then adjacentNodes.Add(nodeToAdd2)
            Dim nodeToAdd1 As Node = FindAdjacentNodes(Node, maze, neededNodes, 0, 1)
            If Not IsNothing(nodeToAdd1) Then adjacentNodes.Add(nodeToAdd1)
            Dim nodeToAdd As Node = FindAdjacentNodes(Node, maze, neededNodes, -2, 0)
            If Not IsNothing(nodeToAdd) Then adjacentNodes.Add(nodeToAdd)
            adjacenyList.Add(Node, adjacentNodes)
            I += 1
            Console.SetCursorPosition(35, Console.WindowHeight - 2)
            Console.Write($"Progress: {Math.Floor((I / neededNodes.Count) * 100)}%")
        Next
        Console.SetCursorPosition(35, Console.WindowHeight - 2)
        Console.Write($"Time taken: {(stopwatch.Elapsed.TotalSeconds)}")
        Return adjacenyList
    End Function
    Function FindAdjacentNodes(currentNode As Node, maze As List(Of Node), neededNodes As List(Of Node), x As Integer, y As Integer)
        Dim tempnode As New Node(currentNode.X, currentNode.Y)
        While True
            tempnode.Update(tempnode.X + x, tempnode.Y + y)
            If maze.Contains(tempnode) Then
                If neededNodes.Contains(tempnode) Then Return tempnode
            Else
                Return Nothing
            End If
        End While
    End Function
    Function CornerOrJunction(currentNode As Node, adjacentCells As List(Of Node))
        Dim l As New List(Of Node)
        Dim top As New Node(currentNode.X, currentNode.Y - 1)
        Dim right As New Node(currentNode.X + 2, currentNode.Y)
        Dim bottom As New Node(currentNode.X, currentNode.Y + 1)
        Dim left As New Node(currentNode.X - 2, currentNode.Y)
        If adjacentCells.Contains(top) Then l.Add(top)
        If adjacentCells.Contains(right) Then l.Add(right)
        If adjacentCells.Contains(bottom) Then l.Add(bottom)
        If adjacentCells.Contains(left) Then l.Add(left)
        If l.Count >= 3 Then Return True 'Is it a junction
        If adjacentCells.Contains(top) And adjacentCells.Contains(right) Then Return True 'is it a corner
        If adjacentCells.Contains(right) And adjacentCells.Contains(bottom) Then Return True
        If adjacentCells.Contains(bottom) And adjacentCells.Contains(left) Then Return True
        If adjacentCells.Contains(left) And adjacentCells.Contains(top) Then Return True
        Return False
    End Function
End Module
