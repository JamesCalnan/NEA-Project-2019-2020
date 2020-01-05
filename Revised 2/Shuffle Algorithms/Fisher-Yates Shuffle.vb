Module Fisher_Yates_Shuffle
    Sub FisherYatesShuffle(ByRef unShuffledList As List(Of Double))
        Dim r As New Random
        Dim n = unShuffledList.Count
        For i = n - 1 To 1 Step -1
            Dim j = r.Next(i)
            Swap(unShuffledList, i, j)
            AnimateSort(unShuffledList)
        Next
    End Sub
    Sub FisherYatesShuffleInsideOut(ByRef source As List(Of Double))
        Dim a As List(Of Double) = source.ToList()
        Dim r As New Random
        Dim n = a.Count
        For i = 0 To n - 1
            Dim j = r.Next(i)
            If j <> i Then a(i) = a(j)
            a(j) = source(i)
            AnimateSort(a)
        Next
        source = a
    End Sub
End Module
