Module Pancake_Sort
    Sub Flip(ByRef arr As List(Of Double), i As Integer)
        Dim start = 0
        While start < i
            Dim temp = arr(start)
            arr(start) = arr(i)
            arr(i) = temp
            start += 1
            i -= 1
        End While
    End Sub
    Function findMax(arr As List(Of Double), n As Double)
        Dim mi = 0
        For i = 0 To n - 1
            If arr(i) > arr(mi) Then mi = i
        Next
        Return mi
    End Function

    Sub pancakeSort(arr As List(Of Double), delay As Integer)
        Dim n = arr.Count
        Dim curr_size = n
        While curr_size > 1
            If ExitCase() Then Exit Sub
            Dim mi = findMax(arr, curr_size)
            If Not mi = curr_size - 1 Then
                Flip(arr, mi)
                AnimateSort(arr, delay)
                Flip(arr, curr_size - 1)
                AnimateSort(arr, delay)
            End If
            curr_size -= 1
        End While
    End Sub
End Module
