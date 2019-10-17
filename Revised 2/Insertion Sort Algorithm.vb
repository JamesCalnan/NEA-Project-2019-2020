Module InsertionSortAlgorithm
    Function InsertionsortR(ByRef a As List(Of Double), n As Integer, Optional delay As Integer = 0)
        If ExitCase() Then Exit Function
        'initial call would be insertionSortR(A, length(A)-1) 
        If n > 0 Then
            InsertionsortR(a, n - 1)
            Dim x As Double = a(n)
            Dim j As Integer = n - 1
            While j >= 0 AndAlso a(j) > x
                a(j + 1) = a(j)
                j -= 1

            End While
            AnimateSort(a, delay)
            a(j + 1) = x
        End If
    End Function
    Function InsertionsortI(a As List(Of Double), Optional delay As Integer = 0)
        Dim i = 1
        While i < a.Count
            If ExitCase() Then Exit Function
            Dim j = i
            While j > 0 AndAlso a(j - 1) > a(j)
                Swap(a, j, j - 1)
                j -= 1
            End While
            AnimateSort(a, delay)
            i += 1
        End While
        AnimateSort(a, delay)
    End Function
End Module