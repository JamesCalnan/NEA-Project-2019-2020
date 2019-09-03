Imports System.Drawing
Module Depth_First_Search
    Sub DFS_IterativeFORFILE(ByVal availablepath As List(Of Node), ByRef bmp As Bitmap, ByRef g As Graphics, ByVal Multiplier As Integer)
        Dim start_v As New Node(availablepath(availablepath.Count - 2).X, availablepath(availablepath.Count - 2).Y)
        Dim goal As New Node(availablepath(availablepath.Count - 1).X, availablepath(availablepath.Count - 1).Y)
        Dim visited As New Dictionary(Of Node, Boolean)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim li As New List(Of Node)
        Dim cameFrom As New Dictionary(Of Node, Node)
        Dim S As New Stack(Of Node)
        For Each u In availablepath
            visited(u) = False
        Next
        S.Push(start_v)
        While S.Count > 0
            Dim u As Node = S.Pop
            If u.Equals(goal) Then Exit While
            ' bmp.Save($"temp Save.png", System.Drawing.Imaging.ImageFormat.Png)
            'g.FillRectangle(Brushes.SlateGray, (u.X) * Multiplier, (u.Y * 2) * Multiplier, 2 * Multiplier, 2 * Multiplier)
            If Not visited(u) Then
                visited(u) = True
                For Each w As Node In GetNeighbours(u, availablepath)
                    If Not visited(w) Then
                        S.Push(w)
                        cameFrom(w) = u
                    End If
                Next
            End If
        End While
        ReconstructPathFORFILE(cameFrom, goal, start_v, bmp, g, Multiplier)
    End Sub
    Sub DFS_Iterative(ByVal availablepath As Dictionary(Of Node, List(Of Node)), ByVal ShowPath As Boolean, ByVal ShowSolveTime As Boolean, ByVal Delay As Integer)
        Dim start_v As New Node(availablepath.Keys(availablepath.Count - 2).X, availablepath.Keys(availablepath.Count - 2).Y)
        Dim goal As New Node(availablepath.Keys(availablepath.Count - 1).X, availablepath.Keys(availablepath.Count - 1).Y)
        Dim visited As New Dictionary(Of Node, Boolean)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim li As New List(Of Node)
        Dim cameFrom As New Dictionary(Of Node, Node)
        Dim S As New Stack(Of Node)
        For Each u In availablepath.Keys
            visited(u) = False
        Next
        S.Push(start_v)
        SetBoth(ConsoleColor.Red)
        While S.Count > 0
            Dim u As Node = S.Pop
            If u.Equals(goal) Then Exit While
            If ShowPath Then : u.Print("██") : Threading.Thread.Sleep(Delay) : End If
            If Not visited(u) Then
                visited(u) = True
                For Each w As Node In GetNeighboursAd(u, availablepath)
                    If Not visited(w) Then
                        S.Push(w)
                        cameFrom(w) = u
                    End If
                Next
            End If
        End While
        ReconstructPath(cameFrom, goal, start_v, If(ShowSolveTime, $"{stopwatch.Elapsed.TotalSeconds}", ""))
    End Sub
    Function DFS_Recursive(ByVal availablepath As List(Of Node), ByVal v As Node, ByVal visited As Dictionary(Of Node, Boolean), ByRef cameFrom As Dictionary(Of Node, Node), ByVal goal As Node, ByVal showsolving As Boolean, ByVal solvingdelay As Integer, ByRef exitcase As Boolean)
        visited(v) = True
        If v.Equals(goal) Then : exitcase = True : Return Nothing : End If
        For Each w As Node In GetNeighbours(v, availablepath)
            If Not visited(w) Then
                If showsolving Then : w.Print("██") : Threading.Thread.Sleep(solvingdelay) : End If
                cameFrom(w) = v
                DFS_Recursive(availablepath, w, visited, cameFrom, goal, showsolving, solvingdelay, exitcase)
                If exitcase Then Return Nothing
            End If
        Next
    End Function
End Module
