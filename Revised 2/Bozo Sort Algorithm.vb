Module Bozo_Sort
    Function BozoSort(ByVal A As List(Of Double))
        Dim r As New Random
        While Not IsSorted(A)
            swap(A, r.Next(0, A.Count), r.Next(0, A.Count))
            AnimateSort(A)
        End While
        Return A
    End Function
End Module
