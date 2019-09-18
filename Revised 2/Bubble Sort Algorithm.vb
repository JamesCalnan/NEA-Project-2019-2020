Module Bubble_Sort_Algorithm
    Function BubbleSort(ByVal A As List(Of Double))
        Dim n As Integer = A.Count
        Dim swapped As Boolean
        Do
            swapped = False
            For i = 1 To n - 1
                If A(i - 1) > A(i) Then
                    swap(A, i - 1, i)
                    swapped = True
                End If
            Next
            AnimateSort(A, 1)
        Loop Until Not swapped
        Return A
    End Function
    Function BubbleSortOptimisedAlternate(ByVal A As List(Of Double))
        Dim n As Integer = A.Count
        Do
            Dim newn As Integer = 0
            For i = 1 To n - 1
                If A(i - 1) > A(i) Then
                    swap(A, i - 1, i)
                    newn = i
                    AnimateSort(A, i)
                End If
            Next

            n = newn
        Loop Until n <= 1
        Return A
    End Function
    Function BubbleSortOptimised(ByVal A As List(Of Double))
        Dim n As Integer = A.Count
        Dim swapped As Boolean
        Do
            swapped = False
            For i = 1 To n - 1
                If A(i - 1) > A(i) Then
                    swap(A, i - 1, i)
                    swapped = True
                End If
            Next
            n -= 1
        Loop Until Not swapped
        Return A
    End Function
End Module
