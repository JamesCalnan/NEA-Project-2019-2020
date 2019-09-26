Module BozoSort
    Function BozoSort(a As List(Of Double))
        Dim r As New Random
        While Not IsSorted(a)
            Swap(a, r.Next(0, a.Count), r.Next(0, a.Count))
            AnimateSort(a, 1)
        End While
        Return a
    End Function
End Module
