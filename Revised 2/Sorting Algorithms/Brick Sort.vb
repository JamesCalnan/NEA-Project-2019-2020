Module Brick_Sort
    Sub oddEverSort(arr As List(Of Double), delay As Integer)
        Dim isSorted = False
        Dim n = arr.Count
        While Not isSorted
            If ExitCase() Then Exit Sub
            isSorted = True
            For i = 1 To n - 2 Step 2
                If arr(i) > arr(i + 1) Then
                    AnimateSort(arr, delay)
                    Swap(arr, i, i + 1)
                    isSorted = False
                End If
            Next
            For i = 0 To n - 2 Step 2
                If arr(i) > arr(i + 1) Then
                    AnimateSort(arr, delay)
                    Swap(arr, i, i + 1)
                    isSorted = False
                End If
            Next
        End While
        AnimateSort(arr)
    End Sub
End Module