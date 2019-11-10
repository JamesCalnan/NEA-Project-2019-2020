Module Simple_Shuffle
    Sub Shuffle(ByRef lista As List(Of Double))
        Dim r As New Random
        Dim listb As New List(Of Double)
        AnimateSort(lista)
        For i = 0 To lista.Count - 1
            Dim temp As Integer = r.Next(0, lista.Count)
            listb.Add(lista(temp))
            lista.RemoveAt(temp)
            AnimateSort(listb)

        Next
        lista = listb
    End Sub
End Module
