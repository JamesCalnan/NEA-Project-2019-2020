Imports System.Drawing
Module DepthFirstSearch
    Sub DFS_IterativeFORFILE(gr As List(Of Node), ByRef bmp As Bitmap, ByRef g As Graphics, multiplier As Integer)
        Dim startV As New Node(gr(gr.Count - 2).X, gr(gr.Count - 2).Y)
        Dim goal As New Node(gr(gr.Count - 1).X, gr(gr.Count - 1).Y)
        Dim visited As New Dictionary(Of Node, Boolean)
        Dim cameFrom As New Dictionary(Of Node, Node)
        Dim s As New Stack(Of Node)
        For Each u In gr
            visited(u) = False
        Next
        s.Push(startV)
        While s.Count > 0
            Dim u As Node = s.Pop
            If u.Equals(goal) Then Exit While
            If Not visited(u) Then
                visited(u) = True
                For Each w As Node In GetNeighbours(u, gr)
                    If Not visited(w) Then
                        s.Push(w)
                        cameFrom(w) = u
                    End If
                Next
            End If
        End While
        ReconstructPathForfile(cameFrom, goal, startV, bmp, g, multiplier)
    End Sub
    Sub DFS_Iterative(g As List(Of Node), showPath As Boolean, showSolveTime As Boolean, delay As Integer, solvingColour As ConsoleColor)
        Dim startV As New Node(g(g.Count - 2).X, g(g.Count - 2).Y)
        Dim goal As New Node(g(g.Count - 1).X, g(g.Count - 1).Y)
        Dim visited As New Dictionary(Of Node, Boolean)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim li As New List(Of Node)
        Dim cameFrom As New Dictionary(Of Node, Node)
        Dim s As New Stack(Of Node)
        For Each u In g
            visited(u) = False
        Next
        s.Push(startV)
        SetBoth(solvingColour)
        While s.Count > 0
            Dim u As Node = s.Pop
            If u.Equals(goal) Then Exit While
            If showPath Then : u.Print("██") : Threading.Thread.Sleep(delay) : End If
            If Not visited(u) Then
                visited(u) = True
                For Each w As Node In GetNeighbours(u, g)
                    If Not visited(w) Then
                        s.Push(w)
                        cameFrom(w) = u
                    End If
                Next
            End If
        End While
        ReconstructPath(cameFrom, goal, startV, If(showSolveTime, $"{stopwatch.Elapsed.TotalSeconds}", ""))
        Console.ReadKey()
    End Sub

    Function DFS_Recursive(g As List(Of Node), v As Node, visited As Dictionary(Of Node, Boolean), ByRef cameFrom As Dictionary(Of Node, Node), goal As Node, showsolving As Boolean, solvingdelay As Integer, ByRef exitcase As Boolean)
        visited(v) = True
        If v.Equals(goal) Then : exitcase = True : Return Nothing : End If
        For Each w As Node In GetNeighbours(v, g)
            If Not visited(w) Then
                If showsolving Then : w.Print("██") : Threading.Thread.Sleep(solvingdelay) : End If
                cameFrom(w) = v
                DFS_Recursive(g, w, visited, cameFrom, goal, showsolving, solvingdelay, exitcase)
                If exitcase Then Return Nothing
            End If
        Next
    End Function
End Module
