Module Bogosort_Algorithm
    Function BogoSort(ByRef lista As List(Of Double))
        Dim r As New Random
        Do
            Dim listb As New List(Of Double)
            For i = 0 To lista.Count - 1
                Dim temp As Integer = r.Next(0, lista.Count)
                listb.Add(lista(temp))
                lista.RemoveAt(temp)
            Next
            lista = listb
        Loop Until IsSorted(lista)
        Return lista
    End Function
    Function IsSorted(ByVal list As List(Of Double))
        For i = 0 To list.Count - 2
            If list(i) > list(i + 1) Then Return False
        Next
        Return True
    End Function
End Module