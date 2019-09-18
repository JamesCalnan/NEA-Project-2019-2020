Module Quick_sort_Algorithm
    Function quicksort(ByVal A As List(Of Double), ByVal lo As Integer, ByVal hi As Integer)
        'Initial call would be quicksort(list,0,list.count-1)
        AnimateSort(A, 1)
        If lo < hi Then
            Dim p As Integer = partition(A, lo, hi)
            quicksort(A, lo, p - 1)
            quicksort(A, p + 1, hi)
        End If
        Return A
    End Function
    Function partition(ByVal A As List(Of Double), ByVal lo As Integer, ByVal hi As Integer)
        Dim pivot As Double = A(hi)
        Dim i As Integer = lo
        For j = lo To hi
            If A(j) < pivot Then
                swap(A, i, j)
                i += 1
            End If
        Next
        Dim tempitem As Double = A(i)
        A(i) = A(hi)
        A(hi) = tempitem
        Return i
    End Function
End Module