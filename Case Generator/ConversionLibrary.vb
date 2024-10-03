Imports Newtonsoft.Json.Linq

Public Class ConversionLibrary
    Public Function ConvertToBool(value As JToken) As Boolean
        Try
            If value Is Nothing Then
                Throw New ArgumentNullException(NameOf(value), "The JToken value is null.")
            End If

            If value.Type = JTokenType.Boolean Then
                Return value.ToObject(Of Boolean)()
            ElseIf value.Type = JTokenType.Integer Then
                Return value.ToObject(Of Integer)() = 1
            End If

            Return False
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
            Console.WriteLine($"Stack Trace: {ex.StackTrace}")
            Return False
        End Try
    End Function
    Public Sub SetComboBoxValue(comboBox As ComboBox, value As JToken)
        If value Is Nothing Then
            Return
        End If

        Dim stringValue As String = value.ToString()

        If comboBox.Items.Contains(stringValue) Then
            comboBox.SelectedItem = stringValue
        Else
            Dim index As Integer
            If Integer.TryParse(stringValue, index) AndAlso index >= 0 AndAlso index < comboBox.Items.Count Then
                comboBox.SelectedIndex = index
            End If
        End If
    End Sub
End Class
