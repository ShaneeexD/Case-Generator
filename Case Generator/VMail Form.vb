Imports System.IO

Public Class VMail_Form
    Dim directoryPath As String = System.IO.Path.GetDirectoryName(Form1.tempFilePath)
    Dim folderNameNew As String = Form1.tempFilePath
    Dim ddsContentPath As String


    Dim tempContentPath As String
    Private Sub VMail_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Icon
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim newString As String = rtbString.Text
        Dim folderPath As String = Form1.tempFilePath
        Dim folderName As String = Form1.tempFolderName


        ' Define the base directory for DDS content
        If Form1.lastOpened = True Then
            ddsContentPath = IO.Path.Combine(directoryPath, "DDSContent")
        ElseIf Form1.lastCreated = True Then
            ddsContentPath = IO.Path.Combine(folderNameNew, "DDSContent")
        End If
        ' Create unique identifiers for the files
        Dim uniqueIDBlock As String = Guid.NewGuid().ToString()
        Dim uniqueIDMsg As String = Guid.NewGuid().ToString()
        Dim uniqueIDTree As String = Guid.NewGuid().ToString()

        Dim instanceIDMSG As String = Guid.NewGuid().ToString()
        Dim instanceIDTree As String = Guid.NewGuid().ToString()

        Dim csvFilePath As String = String.Empty
        Dim blockFilePath As String = String.Empty
        Dim messageFilePath As String = String.Empty
        Dim treeFilePath As String = String.Empty

        If Form1.lastOpened = True Then
            blockFilePath = IO.Path.Combine(ddsContentPath, "DDS", "Blocks", uniqueIDBlock & ".BLOCK")
            messageFilePath = IO.Path.Combine(ddsContentPath, "DDS", "Messages", uniqueIDMsg & ".MSG")
            treeFilePath = IO.Path.Combine(ddsContentPath, "DDS", "Trees", uniqueIDTree & ".TREE")
            csvFilePath = IO.Path.Combine(directoryPath, "DDSContent", "Strings", "English", "DDS", "dds.blocks.csv")
        ElseIf Form1.lastCreated = True Then
            blockFilePath = IO.Path.Combine(ddsContentPath, "DDS", "Blocks", uniqueIDBlock & ".BLOCK")
            messageFilePath = IO.Path.Combine(ddsContentPath, "DDS", "Messages", uniqueIDMsg & ".MSG")
            treeFilePath = IO.Path.Combine(ddsContentPath, "DDS", "Trees", uniqueIDTree & ".TREE")
            csvFilePath = IO.Path.Combine(ddsContentPath, "Strings", "English", "DDS", "dds.blocks.csv")
        End If

        ' Extract first few words from vmailText for the name field
        Dim firstFewWords As String = String.Join("", newString.Split().Take(3)).Replace(" ", "").Replace(".", "").Replace(",", "") ' Takes the first 3 words and removes spaces, commas, and periods

        ' Create the name field using the folder name and the first few words
        Dim blockName As String = $"{folderName}-{firstFewWords}"

        Try
            ' Get the default template text for the tree file
            Dim defaultTreeText As String = GetDefaultTreeText()

            ' Replace placeholders with actual values
            defaultTreeText = defaultTreeText.Replace("{uniqueIDTree}", uniqueIDTree)
            defaultTreeText = defaultTreeText.Replace("{instanceIDTree}", instanceIDTree)
            defaultTreeText = defaultTreeText.Replace("{uniqueIDMsg}", uniqueIDMsg)
            defaultTreeText = defaultTreeText.Replace("{blockName}", folderName)

            ' Write to the tree file
            Using writer As New IO.StreamWriter(treeFilePath, True)
                writer.Write(defaultTreeText)
            End Using

            Using writer As New IO.StreamWriter(blockFilePath, True)
                writer.WriteLine($"{{""name"":""{blockName}"",""id"":""{uniqueIDBlock}"",""replacements"":[]}}")
            End Using

            ' Write to the message file in the specified format
            Using writer As New IO.StreamWriter(messageFilePath, True)
                writer.WriteLine($"{{""blocks"":[{{""alwaysDisplay"":true,""blockID"":""{uniqueIDBlock}"",""group"":0,""instanceID"":""{instanceIDMSG}"",""traitConditions"":0,""traits"":[],""useTraits"":false}}],""id"":""{uniqueIDMsg}"",""name"":""{folderName}-DefaultBlock""}}")
            End Using

            Using writer As New IO.StreamWriter(csvFilePath, True)
                Dim currentDateTime As String = DateTime.Now.ToString("HH:mm dd/MM/yyyy")
                writer.WriteLine($"{uniqueIDBlock},,{newString},,,,{currentDateTime}")
            End Using

            '' Show success message
            'MessageBox.Show("Files created successfully:" & Environment.NewLine &
            '                $"{blockFilePath}" & Environment.NewLine &
            '                $"{messageFilePath}" & Environment.NewLine &
            '                $"{treeFilePath}" & Environment.NewLine &
            '                $"{csvFilePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Catch ex As Exception
            ' Handle errors
            MessageBox.Show("Error creating files: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function GetDefaultTreeText() As String
        Return "{
      ""document"": {
        ""background"": """",
        ""colour"": {
          ""a"": 0,
          ""b"": 0,
          ""g"": 0,
          ""r"": 0
        },
        ""fill"": 1,
        ""size"": {
          ""x"": 0,
          ""y"": 0
        }
      },
      ""id"": ""{uniqueIDTree}"",
      ""messages"": [
        {
          ""alignH"": 0,
          ""alignV"": 0,
          ""charSpace"": 0,
          ""col"": {
            ""a"": 1,
            ""b"": 0,
            ""g"": 0,
            ""r"": 0
          },
          ""elementName"": """",
          ""font"": ""Halogen"",
          ""fontSize"": 22,
          ""fontStyle"": 0,
          ""instanceID"": ""{instanceIDTree}"",
          ""lineSpace"": 16,
          ""links"": [],
          ""msgID"": ""{uniqueIDMsg}"",
          ""order"": 0,
          ""paraSpace"": 0,
          ""pos"": {
            ""x"": -30,
            ""y"": -175
          },
          ""rot"": 0,
          ""saidBy"": 0,
          ""saidTo"": 1,
          ""size"": {
            ""x"": 320,
            ""y"": 300
          },
          ""usePages"": false,
          ""wordSpace"": 0
        }
      ],
      ""name"": ""{blockName}-DefaultTree"",
      ""participantA"": {
        ""connection"": 15,
        ""disableInbox"": false,
        ""jobs"": [],
        ""required"": true,
        ""traitConditions"": 0,
        ""traits"": [],
        ""triggers"": [],
        ""useJobs"": false,
        ""useTraits"": false
      },
      ""participantB"": {
        ""connection"": 15,
        ""disableInbox"": false,
        ""jobs"": [],
        ""required"": true,
        ""traitConditions"": 0,
        ""traits"": [],
        ""triggers"": [],
        ""useJobs"": false,
        ""useTraits"": false
      },
      ""participantC"": {
        ""connection"": 15,
        ""disableInbox"": false,
        ""jobs"": [],
        ""required"": false,
        ""traitConditions"": 0,
        ""traits"": [],
        ""triggers"": [],
        ""useJobs"": false,
        ""useTraits"": false
      },
      ""participantD"": {
        ""connection"": 15,
        ""disableInbox"": false,
        ""jobs"": [],
        ""required"": false,
        ""traitConditions"": 0,
        ""traits"": [],
        ""triggers"": [],
        ""useJobs"": false,
        ""useTraits"": false
      },
      ""priority"": 3,
      ""repeat"": 1,
      ""startingMessage"": ""{instanceIDTree}"",
      ""stopMovement"": true,
      ""treeChance"": 1,
      ""treeType"": 1,
      ""triggerPoint"": 3
    }
    "
    End Function
End Class