Imports System.Runtime.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.IO

Public Class EvidencePresetForm
    Dim FormattingCustom As New FormatOutput()
    Dim JsonMerge As New JsonMerging()
    Dim Handlers As New CustomEventHandlers()
    Dim LBHandlers As New ListBoxChanged()
    Dim CtrlValues As New ControlValues()
    Dim Conversion As New ConversionLibrary()

    Private WithEvents listBoxMonitorTimer As New Timer()
    Private listBoxItemCounts As New Dictionary(Of ListBox, Integer)

    Public lastCreated, lastOpened, isPresetMadeOrLoaded, isOutputChanged As Boolean
    Private previousOutput As String = String.Empty
    Private presetDefault As String
    Private presetFileName As String
    Private DDSID As String
    Private currentFileLocation As String
    Private Sub LoadFile()
        Me.Text = "Evidence Preset - " & Form1.newFileName
    End Sub
    Private Sub NewFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewFileToolStripMenuItem.Click
        If isPresetMadeOrLoaded = True AndAlso isOutputChanged = True Then
            Dim result As DialogResult = MessageBox.Show("Save changes? Any unsaved changes will be lost.", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                SavePreset()
                CreateNewEvidencePreset()
            ElseIf result = DialogResult.No Then
                CreateNewEvidencePreset()
            ElseIf result = DialogResult.Cancel Then

            End If
        Else
            CreateNewEvidencePreset()
        End If
    End Sub
    Public Sub CreateNewEvidencePreset()
        Dim folderBrowserDialog As New FolderBrowserDialog()
        folderBrowserDialog.Description = "Select a location to create the new file"

        If folderBrowserDialog.ShowDialog() = DialogResult.OK Then
            Dim fileName As String = InputBox("Enter the name for the new file:", "File Name", "EvidencePreset")
            UpdateComboBoxValues()
            isOutputChanged = False
            If Not String.IsNullOrWhiteSpace(fileName) Then
                Dim newFilePath As String = IO.Path.Combine(folderBrowserDialog.SelectedPath)

                Dim maniOutput As New With {
                     .enabled = True,
                     .fileOrder = New List(Of String) From {"REF:" + fileName.ToLower()},
                     .version = 1
                    }
                Try

                    Dim presetFilePath As String = IO.Path.Combine(newFilePath, fileName & ".sodso.json")
                    currentFileLocation = newFilePath
                    IO.File.WriteAllText(presetFilePath, presetDefault)
                    presetFileName = fileName
                    MessageBox.Show("New file created successfully: " & fileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    lastOpened = False
                    lastCreated = True

                    DDSID = "715b743b-ee3e-4d93-99c5-fc5b1882b2f0"

                    SetUpNewPreset(fileName)

                    isPresetMadeOrLoaded = True
                    pnCover.Enabled = False
                    pnCover.Visible = False

                    txtPresetName.Text = presetFileName
                    txtDDSDocumentID.Text = DDSID.ToString

                    btnGenerate.PerformClick()

                    IO.File.WriteAllText(presetFilePath, txtOutput.Text)

                    Dim manifestFilePath As String = System.IO.Path.Combine(newFilePath, "murdermanifest.sodso.json")

                    If System.IO.File.Exists(manifestFilePath) Then
                        Dim manifestJson As JObject = JObject.Parse(System.IO.File.ReadAllText(manifestFilePath))
                        Dim presetFileName As String = System.IO.Path.GetFileNameWithoutExtension(newFilePath)

                        Dim fileOrderArray As JArray = CType(manifestJson("fileOrder"), JArray)
                        fileOrderArray.Add("REF:" & fileName)

                        Form1.txtManiOutput.Text = manifestJson.ToString()
                        FormattingCustom.FormatJson(Form1.txtManiOutput)

                        System.IO.File.WriteAllText(manifestFilePath, manifestJson.ToString())
                    End If


                Catch ex As Exception
                    MessageBox.Show("Error creating file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                MessageBox.Show("File name cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            isOutputChanged = False

            txtPresetName.Text = fileName



        End If
    End Sub
    Public Sub SetUpNewPreset(fileName)
        Me.Text = "Evidence Preset - " & fileName
        txtOutput.Text = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(presetDefault), Formatting.Indented)
        FormattingCustom.FormatJson(txtOutput)

        txtDDSDocumentID.Text = DDSID
    End Sub
    Private Sub EvidencePresetForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presetDefault = Form1.preset
        pnCover.BringToFront()
        CtrlValues.CaptureDefaultValues(Me)
        Me.Icon = My.Resources.Icon
        txtOutput.Text = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(presetDefault), Formatting.Indented)
        FormattingCustom.FormatJson(txtOutput)
        UpdateComboBoxValues()

        If My.Settings.isAutoGenerationEnabled = True Then
            Handlers.AddValueChangedHandlers(Me, btnGenerate)
            LBHandlers.ListBoxChanged(Me, btnGenerate)
        Else
            Handlers.RemoveValueChangedHandlers(Me)
            LBHandlers.listBoxMonitorTimer.Stop()
        End If
        isOutputChanged = False
    End Sub
    Private Sub UpdateComboBoxValues()
        cmbCopyFromMain.SelectedIndex = 0
        cmbWindowStyle.SelectedIndex = 0
        cmbUseDataKeys.SelectedIndex = 0
        cmbNotifyOfTies.SelectedIndex = 0
        cmbBelongsToInName.SelectedIndex = 0
        cmbIsSingleton.SelectedIndex = 0
        cmbDisableHistory.SelectedIndex = 0
        cmbAllowCustomNames.SelectedIndex = 1
        cmbMarkDiscovered.SelectedIndex = 0
        cmbForceWorldInteraction.SelectedIndex = 0
        cmbUseWindowFocusMode.SelectedIndex = 0
        cmbUseInGamePhoto.SelectedIndex = 0
        cmbUseWriter.SelectedIndex = 0
        cmbCaptureRules.SelectedIndex = 0
        cmbChangeTimeOfDay.SelectedIndex = 0
        nudCaptureTimeOfDay.Value = 12
        cmbUseCaptureLight.SelectedIndex = 1
        cmbUseSurveillanceCapture.SelectedIndex = 0
        cmbItemOwner.SelectedIndex = 0
        cmbItemWriter.SelectedIndex = 0
        cmbItemReceiver.SelectedIndex = 0
        cmbDiscoverOnCreate.SelectedIndex = 0
        cmbIsMatchParent.SelectedIndex = 0
        cmbEnableSummary.SelectedIndex = 1
        cmbEnableFacts.SelectedIndex = 1
        cmbPinnedStyle.SelectedIndex = 0

        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is TextBox Then
                Dim textBox As TextBox = CType(ctrl, TextBox)
                If textBox.Name <> "txtDDSDocumentID" Then
                    textBox.Text = ""
                End If
            End If
        Next

        lbValidKeys.Items.Clear()
        lbAddFactLinks.Items.Clear()
        lbApplicationOnDiscover.Items.Clear()
        lbDiscoveryTriggers.Items.Clear()
        lbFactSetup.Items.Clear()
        lbKeyMergeOnDiscovery.Items.Clear()
        lbPassiveTies.Items.Clear()
        lbPassiveTies.Items.Clear()

    End Sub
    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim defaultPreset As JObject = JObject.Parse(presetDefault)

        Dim presetName As String = If(String.IsNullOrEmpty(txtPresetName.Text), defaultPreset("name")?.ToString(), txtPresetName.Text)
        defaultPreset("presetName") = presetName
        defaultPreset("name") = presetName

        If cmbCopyFromMain.SelectedIndex = 0 Then
            defaultPreset("copyFrom") = Nothing
        Else
            defaultPreset("copyFrom") = "EvidencePreset|" & cmbCopyFromMain.Text
        End If

        defaultPreset("subClass") = txtSubClass.Text

        If cmbWindowStyle.SelectedIndex = 0 Then
            defaultPreset("windowStyle") = Nothing
        Else
            defaultPreset("windowStyle") = "REF:WindowStylePreset|" & cmbWindowStyle.Text
        End If

        defaultPreset("useDataKeys") = Boolean.Parse(cmbUseDataKeys.SelectedItem.ToString())

        Dim validKeys As New JArray()
        For Each item As String In lbValidKeys.Items
            Dim result As Integer
            If Integer.TryParse(item, result) Then
                validKeys.Add(result)
            Else
                validKeys.Add(item)
            End If
        Next
        defaultPreset("validKeys") = validKeys

        Dim passiveTies As New JArray()
        For Each item As String In lbPassiveTies.Items
            Dim result As Integer
            If Integer.TryParse(item, result) Then
                passiveTies.Add(result)
            Else
                passiveTies.Add(item)
            End If
        Next
        defaultPreset("passiveTies") = passiveTies

        defaultPreset("notifyOfTies") = Boolean.Parse(cmbNotifyOfTies.SelectedItem.ToString())
        defaultPreset("useBelongsToInName") = Boolean.Parse(cmbBelongsToInName.SelectedItem.ToString())
        defaultPreset("isSingleton") = Boolean.Parse(cmbIsSingleton.SelectedItem.ToString())
        defaultPreset("disableHistory") = Boolean.Parse(cmbDisableHistory.SelectedItem.ToString())
        defaultPreset("allowCustomNames") = Boolean.Parse(cmbAllowCustomNames.SelectedItem.ToString())
        defaultPreset("markAsDiscoveredOnAnyInteraction") = Boolean.Parse(cmbMarkDiscovered.SelectedItem.ToString())
        defaultPreset("forceWorldInteraction") = Boolean.Parse(cmbForceWorldInteraction.SelectedItem.ToString())
        defaultPreset("useWindowFocusMode") = Boolean.Parse(cmbUseWindowFocusMode.SelectedItem.ToString())

        If txtIconSpriteLarge.Text IsNot Nothing Then
            defaultPreset("iconSpriteLarge") = txtIconSpriteLarge.Text
        Else
            defaultPreset("iconSpriteLarge") = Nothing
        End If

        If txtDefaultNullImage.Text IsNot Nothing Then
            defaultPreset("defaultNullImage") = txtDefaultNullImage.Text
        Else
            defaultPreset("defaultNullImage") = Nothing
        End If

        defaultPreset("useInGamePhoto") = Boolean.Parse(cmbUseInGamePhoto.SelectedItem.ToString())
        defaultPreset("useWriter") = Boolean.Parse(cmbUseWriter.SelectedItem.ToString())
        defaultPreset("relativeCamPhotoPos")("x") = Integer.Parse(nudRelCamPhotoPosX.Value)
        defaultPreset("relativeCamPhotoPos")("y") = Integer.Parse(nudRelCamPhotoPosY.Value)
        defaultPreset("relativeCamPhotoPos")("z") = Integer.Parse(nudRelCamPhotoPosZ.Value)
        defaultPreset("relativeCamPhotoEuler")("x") = Integer.Parse(nudRelCamPhotoEulerX.Value)
        defaultPreset("relativeCamPhotoEuler")("y") = Integer.Parse(nudRelCamPhotoEulerY.Value)
        defaultPreset("relativeCamPhotoEuler")("z") = Integer.Parse(nudRelCamPhotoEulerZ.Value)
        defaultPreset("captureRules") = Integer.Parse(cmbCaptureRules.SelectedIndex)
        defaultPreset("changeTimeOfDay") = Boolean.Parse(cmbChangeTimeOfDay.SelectedItem.ToString())
        defaultPreset("captureTimeOfDay") = Integer.Parse(nudCaptureTimeOfDay.Value)
        defaultPreset("useCaptureLight") = Boolean.Parse(cmbUseCaptureLight.SelectedItem.ToString())
        defaultPreset("useSurveillanceCapture") = Boolean.Parse(cmbUseSurveillanceCapture.SelectedItem.ToString())
        defaultPreset("itemOwner") = Integer.Parse(cmbItemOwner.SelectedIndex)
        defaultPreset("itemWriter") = Integer.Parse(cmbItemWriter.SelectedIndex)
        defaultPreset("itemReceiver") = Integer.Parse(cmbItemReceiver.SelectedIndex)

        Dim factSetup As New JArray()
        For Each item As String In lbFactSetup.Items
            Dim result As Integer
            If Integer.TryParse(item, result) Then
                factSetup.Add(result)
            Else
                factSetup.Add(item)
            End If
        Next
        defaultPreset("factSetup") = factSetup

        Dim addFactLinks As New JArray()
        For Each item As String In lbAddFactLinks.Items
            Dim result As Integer
            If Integer.TryParse(item, result) Then
                addFactLinks.Add(result)
            Else
                addFactLinks.Add(item)
            End If
        Next
        defaultPreset("addFactLinks") = addFactLinks

        defaultPreset("discoverOnCreate") = Boolean.Parse(cmbDiscoverOnCreate.SelectedItem.ToString())

        Dim keyMergeOnDiscovery As New JArray()
        For Each item As String In lbKeyMergeOnDiscovery.Items
            Dim result As Integer
            If Integer.TryParse(item, result) Then
                keyMergeOnDiscovery.Add(result)
            Else
                keyMergeOnDiscovery.Add(item)
            End If
        Next
        defaultPreset("keyMergeOnDiscovery") = keyMergeOnDiscovery

        Dim discoveryTriggers As New JArray()
        For Each item As String In lbDiscoveryTriggers.Items
            Dim result As Integer
            If Integer.TryParse(item, result) Then
                discoveryTriggers.Add(result)
            Else
                discoveryTriggers.Add(item)
            End If
        Next
        defaultPreset("discoveryTriggers") = discoveryTriggers

        Dim applicationOnDiscover As New JArray()
        For Each item As String In lbApplicationOnDiscover.Items
            Dim result As Integer
            If Integer.TryParse(item, result) Then
                applicationOnDiscover.Add(result)
            Else
                applicationOnDiscover.Add(item)
            End If
        Next
        defaultPreset("applicationOnDiscover") = applicationOnDiscover

        defaultPreset("ddsDocumentID") = txtDDSDocumentID.Text
        defaultPreset("isMatchParent") = Boolean.Parse(cmbIsMatchParent.SelectedItem.ToString())

        Dim matchTypes As New JArray()
        For Each item As String In lbMatchTypes.Items
            Dim result As Integer
            If Integer.TryParse(item, result) Then
                matchTypes.Add(result)
            Else
                matchTypes.Add(item)
            End If
        Next
        defaultPreset("matchTypes") = matchTypes

        defaultPreset("enableSummary") = Boolean.Parse(cmbEnableSummary.SelectedItem.ToString())
        defaultPreset("enableFacts") = Boolean.Parse(cmbEnableFacts.SelectedItem.ToString())
        defaultPreset("pinnedStyle") = Integer.Parse(cmbPinnedStyle.SelectedIndex)
        defaultPreset("pinnedBackgroundColour")("r") = Integer.Parse(nudPinnedBGColourR.Value)
        defaultPreset("pinnedBackgroundColour")("g") = Integer.Parse(nudPinnedBGColourG.Value)
        defaultPreset("pinnedBackgroundColour")("b") = Integer.Parse(nudPinnedBGColourB.Value)
        defaultPreset("pinnedBackgroundColour")("a") = Integer.Parse(nudPinnedBGColourA.Value)

        Dim jsonOutput As String = defaultPreset.ToString(Formatting.Indented)
        txtOutput.Text = jsonOutput
        If My.Settings.isRemoveKeysEnabled = True Then
            RemoveUnusedKeys()
        End If

        Dim currentOutput = txtOutput.Text
        If currentOutput <> previousOutput Then
            isOutputChanged = True
            If Not Me.Text.EndsWith("*") Then
                Me.Text += "*"
            End If
        End If
        previousOutput = currentOutput

        FormattingCustom.FormatJson(txtOutput)
    End Sub
    Private Sub btnAddValidKeys_Click(sender As Object, e As EventArgs) Handles btnAddValidKeys.Click
        If txtValidKeys.Text IsNot Nothing Then
            lbValidKeys.Items.Add(txtValidKeys.Text.ToString())
            txtValidKeys.Text = ""
        End If
    End Sub
    Private Sub btnRemoveValidKeys_Click(sender As Object, e As EventArgs) Handles btnRemoveValidKeys.Click
        If lbValidKeys.SelectedItem IsNot Nothing Then
            lbValidKeys.Items.Remove(lbValidKeys.SelectedItem)
        End If
    End Sub
    Private Sub btnAddPassiveTies_Click(sender As Object, e As EventArgs) Handles btnAddPassiveTies.Click
        If txtPassiveTies.Text IsNot Nothing Then
            lbPassiveTies.Items.Add(txtPassiveTies.Text.ToString())
            txtPassiveTies.Text = ""
        End If
    End Sub
    Private Sub btnRemovePassiveTies_Click(sender As Object, e As EventArgs) Handles btnRemovePassiveTies.Click
        If lbPassiveTies.SelectedItem IsNot Nothing Then
            lbPassiveTies.Items.Remove(lbPassiveTies.SelectedItem)
        End If
    End Sub
    Private Sub btnAddFactSetup_Click(sender As Object, e As EventArgs) Handles btnAddFactSetup.Click
        If txtFactSetup.Text IsNot Nothing Then
            lbFactSetup.Items.Add(txtFactSetup.Text.ToString())
            txtFactSetup.Text = ""
        End If
    End Sub
    Private Sub btnRemoveFactSetup_Click(sender As Object, e As EventArgs) Handles btnRemoveFactSetup.Click
        If lbFactSetup.SelectedItem IsNot Nothing Then
            lbFactSetup.Items.Remove(lbFactSetup.SelectedItem)
        End If
    End Sub
    Private Sub btnAddFactLink_Click(sender As Object, e As EventArgs) Handles btnAddFactLink.Click
        If txtAddFactLinks.Text IsNot Nothing Then
            lbAddFactLinks.Items.Add(txtAddFactLinks.Text.ToString())
            txtAddFactLinks.Text = ""
        End If
    End Sub
    Private Sub btnRemoveFactLink_Click(sender As Object, e As EventArgs) Handles btnRemoveFactLink.Click
        If lbAddFactLinks.SelectedItem IsNot Nothing Then
            lbAddFactLinks.Items.Remove(lbAddFactLinks.SelectedItem)
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If txtKeyMergeOnDiscovery.Text IsNot Nothing Then
            lbKeyMergeOnDiscovery.Items.Add(txtKeyMergeOnDiscovery.Text.ToString())
            txtKeyMergeOnDiscovery.Text = ""
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If lbKeyMergeOnDiscovery.SelectedItem IsNot Nothing Then
            lbKeyMergeOnDiscovery.Items.Remove(lbKeyMergeOnDiscovery.SelectedItem)
        End If
    End Sub
    Private Sub btnAddDiscoveryTrigger_Click(sender As Object, e As EventArgs) Handles btnAddDiscoveryTrigger.Click
        If txtDiscoveryTriggers.Text IsNot Nothing Then
            lbDiscoveryTriggers.Items.Add(txtDiscoveryTriggers.Text.ToString())
            txtDiscoveryTriggers.Text = ""
        End If
    End Sub
    Private Sub btnRemoveDiscoveryTrigger_Click(sender As Object, e As EventArgs) Handles btnRemoveDiscoveryTrigger.Click
        If lbDiscoveryTriggers.SelectedItem IsNot Nothing Then
            lbDiscoveryTriggers.Items.Remove(lbDiscoveryTriggers.SelectedItem)
        End If
    End Sub
    Private Sub btnAddApplicationOnDiscover_Click(sender As Object, e As EventArgs) Handles btnAddApplicationOnDiscover.Click
        If txtApplicationOnDiscover.Text IsNot Nothing Then
            lbApplicationOnDiscover.Items.Add(txtApplicationOnDiscover.Text.ToString())
            txtApplicationOnDiscover.Text = ""
        End If
    End Sub
    Private Sub btnRemoveApplicationOnDiscover_Click(sender As Object, e As EventArgs) Handles btnRemoveApplicationOnDiscover.Click
        If lbApplicationOnDiscover.SelectedItem IsNot Nothing Then
            lbApplicationOnDiscover.Items.Remove(lbApplicationOnDiscover.SelectedItem)
        End If
    End Sub
    Private Sub btnAddMatchTypes_Click(sender As Object, e As EventArgs) Handles btnAddMatchTypes.Click
        If txtMatchTypes.Text IsNot Nothing Then
            lbMatchTypes.Items.Add(txtMatchTypes.Text.ToString())
            txtMatchTypes.Text = ""
        End If
    End Sub
    Private Sub btnRemoveMatchTypes_Click(sender As Object, e As EventArgs) Handles btnRemoveMatchTypes.Click
        If lbMatchTypes.SelectedItem IsNot Nothing Then
            lbMatchTypes.Items.Remove(lbMatchTypes.SelectedItem)
        End If
    End Sub
    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        If isPresetMadeOrLoaded = True Then
            btnGenerate.PerformClick()
            SavePreset()
        Else
            MessageBox.Show("Please create or load a case first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub SavePreset()
        Dim saveFileDialog As New SaveFileDialog
        saveFileDialog.Filter = "Shadows of Doubt SOJSON Files (*.sodso.json)|*.sodso.json"
        saveFileDialog.Title = "Save Preset"

        saveFileDialog.FileName = txtPresetName.Text & ".sodso.json"

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Dim saveDirectory As String = IO.Path.GetDirectoryName(saveFileDialog.FileName)

            Dim presetFilePath As String = IO.Path.Combine(saveDirectory, txtPresetName.Text & ".sodso.json")

            Try
                IO.File.WriteAllText(presetFilePath, txtOutput.Text)
                MessageBox.Show("Case file saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                isOutputChanged = False
                If Me.Text.EndsWith("*") Then
                    Me.Text = Me.Text.Substring(0, Me.Text.Length - 1)
                End If
            Catch ex As Exception
                MessageBox.Show("Error saving case file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                isOutputChanged = True
            End Try
        End If
    End Sub
    Private Sub OpenPreseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenPreseToolStripMenuItem.Click
        If isPresetMadeOrLoaded = True AndAlso isOutputChanged = True Then
            Dim result As DialogResult = MessageBox.Show("Save changes? Any unsaved changes will be lost.", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                SavePreset()
                OpenPreset()
            ElseIf result = DialogResult.No Then
                OpenPreset()
            ElseIf result = DialogResult.Cancel Then

            End If
        Else
            OpenPreset()
        End If
    End Sub
    Private Sub OpenPreset()
        Dim openFileDialog As New OpenFileDialog()

        openFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"
        openFileDialog.Title = "Select a preset"

        If openFileDialog.ShowDialog() = DialogResult.OK Then

            Dim filePath As String = openFileDialog.FileName
            Dim folderName As String = IO.Path.GetFileName(IO.Path.GetDirectoryName(filePath))
            Dim folderPath As String = System.IO.Path.GetDirectoryName(filePath)

            currentFileLocation = folderPath

            Dim fileName As String = Path.GetFileName(filePath)
            If fileName.EndsWith(".sodso.json") Then
                fileName = fileName.Substring(0, fileName.Length - 11)
            End If

            presetFileName = fileName

            Dim defaultJson As JObject = JObject.Parse(presetDefault)
            Dim loadedJsonData As JObject

            Try
                Dim jsonContent As String = File.ReadAllText(filePath)
                loadedJsonData = JObject.Parse(jsonContent)
            Catch ex As Exception
                MessageBox.Show("Error reading the JSON file: " & ex.Message)
                Return
            End Try

            CtrlValues.ResetControlToDefault(Me)

            JsonMerge.MergeJson(defaultJson, loadedJsonData)

            File.WriteAllText(filePath, defaultJson.ToString())
            LoadJsonData(filePath)
            txtOutput.Text = defaultJson.ToString(Formatting.Indented)
            If isPresetMadeOrLoaded = False Then
                isPresetMadeOrLoaded = True
            End If

            lastOpened = True
            lastCreated = False
            pnCover.Enabled = False
            pnCover.Visible = False
            txtPresetName.Text = fileName

            Me.Text = "Case Generator - " & fileName
            isPresetMadeOrLoaded = True
        End If
        If My.Settings.isRemoveKeysEnabled = True Then
            RemoveUnusedKeys()
            FormattingCustom.FormatJson(txtOutput)
        Else
            FormattingCustom.FormatJson(txtOutput)
        End If
        isOutputChanged = False
    End Sub
    Private Sub LoadJsonData(ByVal filePath As String)
        Try
            Dim jsonContent As String = File.ReadAllText(filePath)
            Dim jsonData As JObject = JObject.Parse(jsonContent)

            txtPresetName.Text = If(jsonData("presetName") IsNot Nothing AndAlso Not String.IsNullOrEmpty(jsonData("presetName").ToString()),
            jsonData("presetName").ToString(),
            If(jsonData("name") IsNot Nothing AndAlso Not String.IsNullOrEmpty(jsonData("name").ToString()),
            jsonData("name").ToString(),
            String.Empty))

            If jsonData("copyFrom") IsNot Nothing AndAlso jsonData("copyFrom").Type <> JTokenType.Null Then
                Dim copyFromString = jsonData("copyFrom").ToString()
                If copyFromString.StartsWith("EvidencePreset|") Then
                    copyFromString = copyFromString.Replace("EvidencePreset|", "")
                    cmbCopyFromMain.SelectedItem = copyFromString
                End If
                If cmbCopyFromMain.SelectedItem Is Nothing Then
                    cmbCopyFromMain.Text = jsonData("copyFrom").ToString()
                    cmbCopyFromMain.Items.Add(jsonData("copyFrom").ToString())
                End If
            Else
                cmbCopyFromMain.SelectedIndex = 0
            End If

            txtSubClass.Text = If(jsonData("subClass") IsNot Nothing, jsonData("subClass").ToString(), String.Empty)

            If jsonData("windowStyle") IsNot Nothing AndAlso jsonData("windowStyle").Type <> JTokenType.Null Then
                Dim windowStyleString = jsonData("windowStyle").ToString()
                If windowStyleString.StartsWith("REF:WindowStylePreset|") Then
                    windowStyleString = windowStyleString.Replace("REF:WindowStylePreset|", "")
                    cmbWindowStyle.SelectedItem = windowStyleString
                End If
                If cmbWindowStyle.SelectedItem Is Nothing Then
                    cmbWindowStyle.Text = jsonData("windowStyle").ToString()
                    cmbWindowStyle.Items.Add(jsonData("windowStyle").ToString())
                End If
            Else
                cmbWindowStyle.SelectedIndex = 0
            End If

            cmbUseDataKeys.SelectedItem = If(jsonData("useDataKeys") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("useDataKeys")), "true", "false"), "false")

            lbValidKeys.Items.Clear()
            If jsonData("validKeys") IsNot Nothing AndAlso jsonData("validKeys").Type = JTokenType.Array AndAlso jsonData("validKeys").HasValues Then
                For Each validKey In jsonData("validKeys")
                    If TypeOf validKey Is JValue Then
                        Dim validKeyString As String = validKey.ToString()
                        lbValidKeys.Items.Add(validKeyString.ToString())
                    End If
                Next
                txtValidKeys.Text = ""
            End If

            lbPassiveTies.Items.Clear()
            If jsonData("passiveTies") IsNot Nothing AndAlso jsonData("passiveTies").Type = JTokenType.Array AndAlso jsonData("passiveTies").HasValues Then
                For Each passiveTie In jsonData("passiveTies")
                    If TypeOf passiveTie Is JValue Then
                        Dim passiveTieString As String = passiveTie.ToString()
                        lbPassiveTies.Items.Add(passiveTieString.ToString())
                    End If
                Next
                txtPassiveTies.Text = ""
            End If

            cmbNotifyOfTies.SelectedItem = If(jsonData("notifyOfTies") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("notifyOfTies")), "true", "false"), "false")
            cmbBelongsToInName.SelectedItem = If(jsonData("useBelongsToInName") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("useBelongsToInName")), "true", "false"), "false")
            cmbIsSingleton.SelectedItem = If(jsonData("isSingleton") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("isSingleton")), "true", "false"), "false")
            cmbDisableHistory.SelectedItem = If(jsonData("disableHistory") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("disableHistory")), "true", "false"), "false")
            cmbAllowCustomNames.SelectedItem = If(jsonData("allowCustomNames") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("allowCustomNames")), "true", "false"), "true")
            cmbMarkDiscovered.SelectedItem = If(jsonData("markAsDiscoveredOnAnyInteraction") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("markAsDiscoveredOnAnyInteraction")), "true", "false"), "false")
            cmbForceWorldInteraction.SelectedItem = If(jsonData("forceWorldInteraction") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("forceWorldInteraction")), "true", "false"), "false")
            cmbUseWindowFocusMode.SelectedItem = If(jsonData("useWindowFocusMode") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("useWindowFocusMode")), "true", "false"), "false")
            txtIconSpriteLarge.Text = If(jsonData("iconSpriteLarge") IsNot Nothing, jsonData("iconSpriteLarge").ToString(), String.Empty)
            txtDefaultNullImage.Text = If(jsonData("defaultNullImage") IsNot Nothing, jsonData("defaultNullImage").ToString(), String.Empty)
            cmbUseInGamePhoto.SelectedItem = If(jsonData("useInGamePhoto") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("useInGamePhoto")), "true", "false"), "false")
            cmbUseWriter.SelectedItem = If(jsonData("useWriter") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("useWriter")), "true", "false"), "false")

            nudRelCamPhotoPosX.Value = If(jsonData("relativeCamPhotoPos")?("x") IsNot Nothing, CInt(jsonData("relativeCamPhotoPos")("x").ToObject(Of Decimal)()), 0)
            nudRelCamPhotoPosY.Value = If(jsonData("relativeCamPhotoPos")?("y") IsNot Nothing, CInt(jsonData("relativeCamPhotoPos")("y").ToObject(Of Decimal)()), 0)
            nudRelCamPhotoPosZ.Value = If(jsonData("relativeCamPhotoPos")?("z") IsNot Nothing, CInt(jsonData("relativeCamPhotoPos")("z").ToObject(Of Decimal)()), 0)

            nudRelCamPhotoEulerX.Value = If(jsonData("relativeCamPhotoEuler")?("x") IsNot Nothing, CInt(jsonData("relativeCamPhotoEuler")("x").ToObject(Of Decimal)()), 0)
            nudRelCamPhotoEulerY.Value = If(jsonData("relativeCamPhotoEuler")?("y") IsNot Nothing, CInt(jsonData("relativeCamPhotoEuler")("y").ToObject(Of Decimal)()), 0)
            nudRelCamPhotoEulerZ.Value = If(jsonData("relativeCamPhotoEuler")?("z") IsNot Nothing, CInt(jsonData("relativeCamPhotoEuler")("z").ToObject(Of Decimal)()), 0)

            Conversion.SetComboBoxValue(cmbCaptureRules, jsonData("captureRules"))

            cmbChangeTimeOfDay.SelectedItem = If(jsonData("changeTimeOfDay") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("changeTimeOfDay")), "true", "false"), "false")
            nudCaptureTimeOfDay.Value = If(jsonData("captureTimeOfDay") IsNot Nothing, CInt(jsonData("captureTimeOfDay").ToObject(Of Integer)()), 12)
            cmbUseCaptureLight.SelectedItem = If(jsonData("useCaptureLight") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("useCaptureLight")), "true", "false"), "true")
            cmbUseSurveillanceCapture.SelectedItem = If(jsonData("useSurveillanceCapture") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("useSurveillanceCapture")), "true", "false"), "false")

            Conversion.SetComboBoxValue(cmbCaptureRules, jsonData("itemOwner"))
            Conversion.SetComboBoxValue(cmbCaptureRules, jsonData("itemWriter"))
            Conversion.SetComboBoxValue(cmbCaptureRules, jsonData("itemReceiver"))

            lbFactSetup.Items.Clear()
            If jsonData("factSetup") IsNot Nothing AndAlso jsonData("factSetup").Type = JTokenType.Array AndAlso jsonData("factSetup").HasValues Then
                For Each factSetup In jsonData("factSetup")
                    If TypeOf factSetup Is JValue Then
                        Dim factSetupString As String = factSetup.ToString()
                        lbFactSetup.Items.Add(factSetupString.ToString())
                    End If
                Next
                txtFactSetup.Text = ""
            End If

            lbAddFactLinks.Items.Clear()
            If jsonData("addFactLinks") IsNot Nothing AndAlso jsonData("addFactLinks").Type = JTokenType.Array AndAlso jsonData("addFactLinks").HasValues Then
                For Each factLink In jsonData("addFactLinks")
                    If TypeOf factLink Is JValue Then
                        Dim factLinkString As String = factLink.ToString()
                        lbAddFactLinks.Items.Add(factLinkString.ToString())
                    End If
                Next
                txtAddFactLinks.Text = ""
            End If

            cmbDiscoverOnCreate.SelectedItem = If(jsonData("discoverOnCreate") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("discoverOnCreate")), "true", "false"), "false")

            lbKeyMergeOnDiscovery.Items.Clear()
            If jsonData("keyMergeOnDiscovery") IsNot Nothing AndAlso jsonData("keyMergeOnDiscovery").Type = JTokenType.Array AndAlso jsonData("keyMergeOnDiscovery").HasValues Then
                For Each keyMergeOnDiscovery In jsonData("keyMergeOnDiscovery")
                    If TypeOf keyMergeOnDiscovery Is JValue Then
                        Dim keyMergeOnDiscoveryString As String = keyMergeOnDiscovery.ToString()
                        lbKeyMergeOnDiscovery.Items.Add(keyMergeOnDiscoveryString.ToString())
                    End If
                Next
                txtKeyMergeOnDiscovery.Text = ""
            End If

            lbDiscoveryTriggers.Items.Clear()
            If jsonData("discoveryTriggers") IsNot Nothing AndAlso jsonData("discoveryTriggers").Type = JTokenType.Array AndAlso jsonData("discoveryTriggers").HasValues Then
                For Each discoveryTriggers In jsonData("discoveryTriggers")
                    If TypeOf discoveryTriggers Is JValue Then
                        Dim discoveryTriggersString As String = discoveryTriggers.ToString()
                        lbDiscoveryTriggers.Items.Add(discoveryTriggersString.ToString())
                    End If
                Next
                txtDiscoveryTriggers.Text = ""
            End If

            lbApplicationOnDiscover.Items.Clear()
            If jsonData("applicationOnDiscover") IsNot Nothing AndAlso jsonData("applicationOnDiscover").Type = JTokenType.Array AndAlso jsonData("applicationOnDiscover").HasValues Then
                For Each applicationOnDiscover In jsonData("applicationOnDiscover")
                    If TypeOf applicationOnDiscover Is JValue Then
                        Dim applicationOnDiscoverString As String = applicationOnDiscover.ToString()
                        lbApplicationOnDiscover.Items.Add(applicationOnDiscoverString.ToString())
                    End If
                Next
                txtApplicationOnDiscover.Text = ""
            End If

            txtDDSDocumentID.Text = If(jsonData("ddsDocumentID") IsNot Nothing, jsonData("ddsDocumentID").ToString(), String.Empty)
            cmbIsMatchParent.SelectedItem = If(jsonData("isMatchParent") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("isMatchParent")), "true", "false"), "false")

            lbMatchTypes.Items.Clear()
            If jsonData("matchTypes") IsNot Nothing AndAlso jsonData("matchTypes").Type = JTokenType.Array AndAlso jsonData("matchTypes").HasValues Then
                For Each matchTypes In jsonData("matchTypes")
                    If TypeOf matchTypes Is JValue Then
                        Dim matchTypesString As String = matchTypes.ToString()
                        lbMatchTypes.Items.Add(matchTypesString.ToString())
                    End If
                Next
                txtMatchTypes.Text = ""
            End If

            cmbEnableSummary.SelectedItem = If(jsonData("enableSummary") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("enableSummary")), "true", "false"), "true")
            cmbEnableFacts.SelectedItem = If(jsonData("enableFacts") IsNot Nothing, If(Conversion.ConvertToBool(jsonData("enableFacts")), "true", "false"), "true")

            Conversion.SetComboBoxValue(cmbCaptureRules, jsonData("pinnedStyle"))

            nudPinnedBGColourR.Value = If(jsonData("pinnedBackgroundColour")?("r") IsNot Nothing, CInt(jsonData("pinnedBackgroundColour")("r").ToObject(Of Decimal)()), 1)
            nudPinnedBGColourG.Value = If(jsonData("pinnedBackgroundColour")?("g") IsNot Nothing, CInt(jsonData("pinnedBackgroundColour")("g").ToObject(Of Decimal)()), 1)
            nudPinnedBGColourB.Value = If(jsonData("pinnedBackgroundColour")?("b") IsNot Nothing, CInt(jsonData("pinnedBackgroundColour")("b").ToObject(Of Decimal)()), 1)
            nudPinnedBGColourA.Value = If(jsonData("pinnedBackgroundColour")?("a") IsNot Nothing, CInt(jsonData("pinnedBackgroundColour")("a").ToObject(Of Decimal)()), 1)

        Catch ex As Exception

        End Try

    End Sub
    Private Sub RemoveUnusedKeys()
        Dim jsonContent As String = txtOutput.Text

        Dim jsonData As JObject = JObject.Parse(jsonContent)

        Dim keysToRemove As New List(Of String)
        Dim disableHexaco As Boolean = False

        For Each propertyPair In jsonData.Properties()
            Dim key As String = propertyPair.Name
            Dim value As JToken = propertyPair.Value

            If value.Type = JTokenType.Null OrElse
               (value.Type = JTokenType.String AndAlso String.IsNullOrEmpty(value.ToString())) OrElse
               (value.Type = JTokenType.Array AndAlso Not value.Any()) OrElse
               (value.Type = JTokenType.Object AndAlso Not value.HasValues) OrElse
               (value.Type = JTokenType.Integer AndAlso Integer.Parse(value) = 12 AndAlso key = "captureTimeOfDay") OrElse
               (value.Type = JTokenType.Boolean AndAlso Boolean.Parse(value) = False AndAlso key <> "allowCustomNames" Or key = "useCaptureLight" Or key = "enableSummary" Or key = "enableFacts") OrElse
               (value.Type = JTokenType.Boolean AndAlso Boolean.Parse(value) = True AndAlso key = "allowCustomNames" Or key = "useCaptureLight" Or key = "enableSummary" Or key = "enableFacts") OrElse
               (value.Type = JTokenType.Integer AndAlso Integer.Parse(value) = 0) Then
                keysToRemove.Add(key)
            End If
        Next

        For Each key In keysToRemove
            jsonData.Remove(key)
        Next

        RemoveRangeIfDefault(jsonData, "relativeCamPhotoPos")
        RemoveRangeIfDefault(jsonData, "relativeCamPhotoEuler")
        RemoveRGBIfDefault(jsonData, "pinnedBackgroundColour")

        txtOutput.Text = jsonData.ToString()
        FormattingCustom.FormatJson(txtOutput)
    End Sub
    Private Sub RemoveRangeIfDefault(jsonData As JObject, rangeKey As String)
        Dim range As JObject = jsonData(rangeKey)
        If range IsNot Nothing AndAlso
            range("x").ToObject(Of Integer) = 0 AndAlso
            range("y").ToObject(Of Integer) = 0 AndAlso
            range("z").ToObject(Of Integer) = 0 Then
            jsonData.Remove(rangeKey)
        End If
    End Sub
    Private Sub RemoveRGBIfDefault(jsonData As JObject, rangeKey As String)
        Dim range As JObject = jsonData(rangeKey)
        If range IsNot Nothing AndAlso
            range("r").ToObject(Of Integer) = 1 AndAlso
            range("g").ToObject(Of Integer) = 1 AndAlso
            range("b").ToObject(Of Integer) = 1 AndAlso
            range("a").ToObject(Of Integer) = 1 Then
            jsonData.Remove(rangeKey)
        End If
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        If isPresetMadeOrLoaded = True AndAlso isOutputChanged = True Then
            Dim result As DialogResult = MessageBox.Show("Save changes? Any unsaved changes will be lost.", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                btnGenerate.PerformClick()
                SavePreset()
                FullReset()
            ElseIf result = DialogResult.No Then
                FullReset()
            ElseIf result = DialogResult.Cancel Then

            End If

        ElseIf isPresetMadeOrLoaded = True AndAlso isOutputChanged = False Then
            FullReset()
        Else
            MessageBox.Show("Please create or load a case first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub FullReset()
        Me.Text = "Evidence Preset"
        UpdateComboBoxValues()
        pnCover.Enabled = True
        pnCover.Visible = True
        isOutputChanged = False
        isPresetMadeOrLoaded = False
        If Me.Text.EndsWith("*") Then
            Me.Text = Me.Text.Substring(0, Me.Text.Length - 1)
        End If
    End Sub
    Private Sub DeletePresetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeletePresetToolStripMenuItem.Click
        If isPresetMadeOrLoaded = True Then
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this preset?", "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then

                Dim presetFilePath As String = System.IO.Path.Combine(currentFileLocation, presetFileName & ".sodso.json")
                If System.IO.File.Exists(presetFilePath) Then
                    Try
                        System.IO.File.Delete(presetFilePath)
                        FullReset()
                        isOutputChanged = False
                        Dim manifestFilePath As String = System.IO.Path.Combine(currentFileLocation, "murdermanifest.sodso.json")

                        If System.IO.File.Exists(manifestFilePath) Then
                            Dim manifestJson As JObject = JObject.Parse(System.IO.File.ReadAllText(manifestFilePath))

                            Dim fileOrderArray As JArray = CType(manifestJson("fileOrder"), JArray)

                            Dim itemToRemove As String = "REF:" & presetFileName

                            Dim item As JToken = fileOrderArray.FirstOrDefault(Function(i) i.ToString() = itemToRemove)

                            If item IsNot Nothing Then
                                fileOrderArray.Remove(item)
                            End If

                            Form1.txtManiOutput.Text = manifestJson.ToString()
                            FormattingCustom.FormatJson(Form1.txtManiOutput)

                            System.IO.File.WriteAllText(manifestFilePath, manifestJson.ToString())
                        End If
                    Catch ex As Exception
                        MessageBox.Show("An error occurred while deleting the file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Else
                    MessageBox.Show("The preset file does not exist." & vbCrLf & presetFilePath, "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Else
            MessageBox.Show("You do not have a file open.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

End Class