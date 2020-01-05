Module Brick_Sort
    Sub oddEverSort(a As List(Of Double), delay As Integer)
        Dim isSorted = False
        Dim n = a.Count
        While Not isSorted
            If ExitCase() Then Exit Sub
            isSorted = True
            For i = 1 To n - 2 Step 2
                If a(i) > a(i + 1) Then
                    AnimateSort(a, delay)
                    Swap(a, i, i + 1)
                    isSorted = False
                End If
            Next
            For i = 0 To n - 2 Step 2
                If a(i) > a(i + 1) Then
                    AnimateSort(a, delay)
                    Swap(a, i, i + 1)
                    isSorted = False
                End If
            Next
        End While
        AnimateSort(a)
    End Sub
End Module