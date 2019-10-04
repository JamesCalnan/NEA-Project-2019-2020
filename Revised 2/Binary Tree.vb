Imports NEA_2019
Imports System.Drawing
Public Class PriorityQueue(Of T)
    Public Sub New()
        Items = New BinaryTree(Of T)
    End Sub
    Public Sub Clear()
        Items = New BinaryTree(Of T)
    End Sub
    Public Property Items As BinaryTree(Of T)
    Public Sub Enqueue(value As T, Optional ByVal priority As Integer = 0)
        Items.Insert(New QueueItem(Of T)(value, priority))
    End Sub
    Public Function ExtractMin() As T
        Dim minPriority As QueueItem(Of T) = Items.MinValue()
        Dim output As T = Items.FindFirst(minPriority).Value
        Items.Delete(minPriority)
        Return output
    End Function

    Public Function PeekMin() As T
        Dim minPriority As QueueItem(Of T) = Items.MinValue()
        Dim output As T = Items.FindFirst(minPriority).Value
        Return output
    End Function

    Public Sub DecreasePriority(value As T, newPriority As Integer)
        Dim tempItem As New QueueItem(Of T)(value, 0)
        Dim item As QueueItem(Of T) = Items.FindExact(tempItem).Clone()
        Items.Delete(item, True)
        Items.Insert(New QueueItem(Of T)(value, newPriority))
    End Sub
    Public  Sub Delete(value as T)
        Dim tempItem As New QueueItem(Of T)(value, 0)
        Dim item As QueueItem(Of T) = Items.FindExact(tempItem).Clone()
        Items.Delete(item,true)
    End Sub
    Public Function Contains(value As T) As Boolean

        'Return Not IsNothing(Items.Contains(value))
    End Function

    Public Function IsEmpty()
        Return Items.IsEmpty
    End Function
End Class
Public Class QueueItem(Of T)
    Public Value As T
    Public Priority As Integer
    Public Sub New(inputValue As T, inputPriority As Integer)
        Value = inputValue
        Priority = inputPriority
    End Sub
    Public Function CompareTo(other As QueueItem(Of T)) As Integer
        If ReferenceEquals(Me, other) Then Return 0
        If ReferenceEquals(Nothing, other) Then Return 1
        Return Priority.CompareTo(other.Priority)
    End Function
    Public Function Clone() As QueueItem(Of T)
        Return New QueueItem(Of T)(Value, Priority)
    End Function
    Public Overrides Function Equals(obj As Object) As Boolean
        Dim item = TryCast(obj, QueueItem(Of T))
        Return item IsNot Nothing AndAlso
               EqualityComparer(Of T).Default.Equals(Value, item.Value)
    End Function
    Public Overrides Function GetHashCode() As Integer
        Dim hashCode As Long = 1113510858
        hashCode = (hashCode * -1521134295 + EqualityComparer(Of T).Default.GetHashCode(Value)).GetHashCode()
        Return hashCode
    End Function
End Class

Public Class BinaryTree(Of T)
    Private Root As TreeItem(Of T)
    Public Sub Binary_Tree()
        Root = Nothing
    End Sub
    Public Sub Delete(value As QueueItem(Of T), Optional ByVal strict As Boolean = False)
        Root = DeleteRecursive(Root, value, strict)
    End Sub
    Public Function DeleteRecursive(root As TreeItem(Of T), value As QueueItem(Of T), strict As Boolean)
        If IsNothing(root) Then Return root
        If value.CompareTo(root.Value) < 0 Then
            root.Left = DeleteRecursive(root.Left, value, strict)
        ElseIf value.CompareTo(root.Value) > 0 Or strict And Not value.Equals(root.Value) And value.CompareTo(root.Value) = 0 Then
            root.Right = DeleteRecursive(root.Right, value, strict)
        Else
            If IsNothing(root.Left) Then Return root.Right
            If IsNothing(root.Right) Then Return root.Left
            root.Value = MinValue(root.Right)
            root.Right = DeleteRecursive(root.Right, root.Value, strict)
        End If
        Return root
    End Function
    Public Sub Insert(value As QueueItem(Of T))
        Root = InsertRecursive(Root, value)
    End Sub
    Private Function InsertRecursive(root As TreeItem(Of T), value As QueueItem(Of T))
        If IsNothing(root) Then Return New TreeItem(Of T)(value)
        If value.CompareTo(root.Value) < 0 Then
            root.Left = InsertRecursive(root.Left, value)
        ElseIf value.CompareTo(root.Value) >= 0 Then
            root.Right = InsertRecursive(root.Right, value)
        End If
        Return root
    End Function
    'Public Sub traverseInOrder(ByVal node As TreeItem)
    '    If Not IsNothing(node) Then
    '        traverseInOrder(node.left)
    '        Console.WriteLine(node.value)
    '        traverseInOrder(node.right)
    '    End If
    'End Sub
    'Private Function containsValueRecursive(ByVal current As TreeItem, ByVal value As Integer) As Boolean
    '    If IsNothing(current) Then Return False
    '    If value = current.value Then Return True
    '    Return If(value < current.value, containsValueRecursive(current.left, value), containsValueRecursive(current.right, value))
    'End Function
    'Public Function containsValue(ByVal value As Integer)
    '    Return containsValueRecursive(root, value)
    'End Function
    Public Function FindFirst(value As QueueItem(Of T)) As QueueItem(Of T)
        Return FindFirstRecursive(Root, value).Value
    End Function
    Private Function FindFirstRecursive(root As TreeItem(Of T), value As QueueItem(Of T)) As TreeItem(Of T)
        If value.CompareTo(root.Value) < 0 Then Return FindFirstRecursive(root.Left, value)
        If value.CompareTo(root.Value) > 0 Then Return FindFirstRecursive(root.Right, value)
        Return root
    End Function
    Public Function Contains(value As QueueItem(Of T))
        Return FindExact(value)
    End Function
    Public Function FindExact(value As QueueItem(Of T)) As QueueItem(Of T)
        Return FindExactRecursive(Root, value).Value
    End Function
    Private Function FindExactRecursive(root As TreeItem(Of T), value As QueueItem(Of T)) As TreeItem(Of T)
        If IsNothing(root) Then Return Nothing
        If value.Equals(root.Value) Then Return root
        Dim output As TreeItem(Of T)
        output = FindExactRecursive(root.Left, value)
        If Not IsNothing(output) Then Return output
        output = FindExactRecursive(root.Right, value)
        If Not IsNothing(output) Then Return output
        Return output
    End Function
    Private Shared Function MinValue(root As TreeItem(Of T)) As QueueItem(Of T)
        Dim min As QueueItem(Of T) = root.Value
        While Not IsNothing(root.Left)
            min = root.Left.Value
            root = root.Left
        End While
        Return min
    End Function
    Public Function MinValue() As QueueItem(Of T)
        Return MinValue(Root)
    End Function
    'Private Function findSmallestValue(ByVal root As TreeItem)
    '    Return If(IsNothing(root.left), root.value, findSmallestValue(root.left))
    'End Function
    'Public Function ExtractMin()
    '    Return findSmallestValue(root)
    'End Function
    Public Function GetSize() As Integer
        Return GetSizeRecursive(Root)
    End Function
    Private Function GetSizeRecursive(current As TreeItem(Of T))
        Return If(IsNothing(current), 0, GetSizeRecursive(current.Left) + 1 + GetSizeRecursive(current.Right))
    End Function
    Public Function IsEmpty() As Boolean
        Return IsNothing(Root)
    End Function
End Class
Public Class TreeItem(Of T)
    Public Value As QueueItem(Of T)
    Public Left, Right As TreeItem(Of T)
    Public Sub New(val As QueueItem(Of T))
        Value = val
        Left = Nothing
        Right = Nothing
    End Sub
End Class