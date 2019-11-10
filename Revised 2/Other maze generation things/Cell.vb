Public Class Cell
    Public X, Y As Integer
    Public Sub New(xPoint As Integer, yPoint As Integer)
        X = xPoint
        Y = yPoint
    End Sub
    Public Function ToNode() As Node
        Return New Node(Me.X, Me.Y)
    End Function
    Sub Update(x As Integer, y As Integer)
        Me.X = x
        Me.Y = y
    End Sub
    Function WithinLimits(limits() As Integer)
        If Me.X >= limits(0) And Me.X <= limits(2) And Me.Y >= limits(1) And Me.Y <= limits(3) Then Return True
        Return False
    End Function
    Public Sub Print(str As String)
        Console.SetCursorPosition(X, Y)
        Console.Write(str)
    End Sub
    Public Overrides Function Equals(obj As Object) As Boolean
        Dim cell = TryCast(obj, Cell)
        Return cell IsNot Nothing AndAlso
               X = cell.X AndAlso
               Y = cell.Y
    End Function
    Public Overrides Function GetHashCode() As Integer
        Dim hashCode As Long = 1855483287
        hashCode = (hashCode * -1521134295 + X.GetHashCode()).GetHashCode()
        hashCode = (hashCode * -1521134295 + Y.GetHashCode()).GetHashCode()
        Return hashCode
    End Function
End Class
