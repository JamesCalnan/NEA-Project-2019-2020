Module Straight_Ways
    Function StraightWays(maze As List(Of Node))
        Dim mX = 0
        Dim gx = (From node In maze Select node.X).Concat(New Integer() {0}).Max()
        Dim gy = (From node In maze Select node.Y).Concat(New Integer() {0}).Max()
        For x = 0 To Console.WindowWidth - 40
            If maze.Contains(New Node(x, 3)) Then
                mX = x
                Exit For
            End If
        Next
        Dim corridorCount As New List(Of Integer)
        For x = mX To gx + 1 Step 2
            Dim straightCount = 0
            For y = 3 To gy
                Dim tempNode As New Node(x, y)
                If maze.Contains(tempNode) Then
                    straightCount += 1
                Else
                    If straightCount > 1 Then
                        corridorCount.Add(straightCount)
                    End If
                    straightCount = 0
                End If
            Next
        Next
        For y = 3 To gy
            Dim straightCount = 0
            For x = mX To gx + 1 Step 2
                Dim tempNode As New Node(x, y)
                If maze.Contains(tempNode) Then
                    straightCount += 1
                Else
                    If straightCount > 1 Then
                        corridorCount.Add(straightCount)
                    End If
                    straightCount = 0
                End If
            Next
        Next
        Return corridorCount.Average
    End Function
End Module
