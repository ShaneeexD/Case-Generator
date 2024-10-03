Imports Newtonsoft.Json.Linq

Public Class JsonMerging
    Public Sub MergeJson(ByRef defaultJson As JObject, ByVal loadedJsonData As JObject)
        For Each defaultProperty As JProperty In defaultJson.Properties()
            Dim loadedProperty As JToken = loadedJsonData(defaultProperty.Name)

            If loadedProperty IsNot Nothing Then
                defaultProperty.Value = loadedProperty
            End If
        Next

        For Each defaultProperty As JProperty In defaultJson.Properties()
            If loadedJsonData(defaultProperty.Name) Is Nothing Then
                loadedJsonData.Add(defaultProperty.Name, defaultProperty.Value)
            End If
        Next
    End Sub
End Class
