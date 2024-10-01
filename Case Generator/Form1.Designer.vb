<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        txtPresetName = New TextBox()
        lblCaseName = New Label()
        txtOutput = New RichTextBox()
        lblOutput = New Label()
        txtNotes = New TextBox()
        lblDesc = New Label()
        lblDisabled = New Label()
        cmbDisabled = New ComboBox()
        lblCompatWith = New Label()
        cmbCompatibleWith = New ComboBox()
        Label6 = New Label()
        cmbUpdateThis = New ComboBox()
        Label7 = New Label()
        Label8 = New Label()
        btnGenerateCase = New Button()
        tabControlCase = New TabControl()
        Label9 = New Label()
        cmbCopyFromMain = New ComboBox()
        lblCopyFromMain = New Label()
        cmbVictimSocialClassRange = New ComboBox()
        txtManiOutput = New RichTextBox()
        lblManifest = New Label()
        btnAllowedWeapons = New Button()
        SaveFileDialog1 = New SaveFileDialog()
        cmbSniperVantage = New ComboBox()
        Label13 = New Label()
        cmbBlockWeaponDrops = New ComboBox()
        Label14 = New Label()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        btnAddPlayerTauntTab = New Button()
        txtMonkier = New TextBox()
        Label11 = New Label()
        btnAddMOlead = New Button()
        btnOpenHexaco = New Button()
        Label19 = New Label()
        cmbUseHexaco = New ComboBox()
        nudRndScoreY = New NumericUpDown()
        nudRndScoreX = New NumericUpDown()
        btnRemoveCompatibleWith = New Button()
        btnAddCompat = New Button()
        lstCompatibleWith = New ListBox()
        nudMaxPotScore = New NumericUpDown()
        nudBaseDifficulty = New NumericUpDown()
        Label15 = New Label()
        TabPage2 = New TabPage()
        btnAddConfessionalTab = New Button()
        btnAddGraffiti = New Button()
        btnCallingCard = New Button()
        Label23 = New Label()
        nudMurdererIsTenantBoost = New NumericUpDown()
        Label22 = New Label()
        nudLikeSuitBoost = New NumericUpDown()
        Label21 = New Label()
        nudAttractSuitBoost = New NumericUpDown()
        Label20 = New Label()
        nudAcquaintSuitBoost = New NumericUpDown()
        nudSameWork = New NumericUpDown()
        nudMurdererCRBoost = New NumericUpDown()
        btnAddMurdererTraits = New Button()
        btnAddMurdererJobModifier = New Button()
        nudMurdererCrY = New NumericUpDown()
        btnAddNewMurdererCompModifier = New Button()
        Label16 = New Label()
        Label18 = New Label()
        cmbMurdererClassRange = New ComboBox()
        Label17 = New Label()
        nudMurdererCrX = New NumericUpDown()
        TabPage3 = New TabPage()
        Label27 = New Label()
        nudVictimCRBoost = New NumericUpDown()
        nudVictimCrY = New NumericUpDown()
        Label25 = New Label()
        Label26 = New Label()
        nudVictimCrX = New NumericUpDown()
        btnAddVictimCompanyModifier = New Button()
        btnAddVictimJobModifier = New Button()
        btnAddVictimTraits = New Button()
        nudVictimRndCrY = New NumericUpDown()
        nudVictimRndCrX = New NumericUpDown()
        Label24 = New Label()
        pnCover = New Panel()
        lblOpen = New Label()
        msMain = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        NewToolStripMenuItem = New ToolStripMenuItem()
        OpenToolStripMenuItem = New ToolStripMenuItem()
        SaveCaseToolStripMenuItem = New ToolStripMenuItem()
        CloseCaseToolStripMenuItem = New ToolStripMenuItem()
        SearchToolStripMenuItem = New ToolStripMenuItem()
        ToolsToolStripMenuItem = New ToolStripMenuItem()
        NewDDSToolStripMenuItem = New ToolStripMenuItem()
        LoadStringListToolStripMenuItem = New ToolStripMenuItem()
        RemoveUnusedKeysToolStripMenuItem = New ToolStripMenuItem()
        HelpToolStripMenuItem = New ToolStripMenuItem()
        UpdateToolStripMenuItem = New ToolStripMenuItem()
        itemAutoGen = New ToolStripMenuItem()
        itemRemoveKeys = New ToolStripMenuItem()
        itemToolTips = New ToolStripMenuItem()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        CType(nudRndScoreY, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudRndScoreX, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudMaxPotScore, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudBaseDifficulty, ComponentModel.ISupportInitialize).BeginInit()
        TabPage2.SuspendLayout()
        CType(nudMurdererIsTenantBoost, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudLikeSuitBoost, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudAttractSuitBoost, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudAcquaintSuitBoost, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudSameWork, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudMurdererCRBoost, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudMurdererCrY, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudMurdererCrX, ComponentModel.ISupportInitialize).BeginInit()
        TabPage3.SuspendLayout()
        CType(nudVictimCRBoost, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudVictimCrY, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudVictimCrX, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudVictimRndCrY, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudVictimRndCrX, ComponentModel.ISupportInitialize).BeginInit()
        msMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' txtPresetName
        ' 
        txtPresetName.Location = New Point(162, 12)
        txtPresetName.Margin = New Padding(3, 4, 3, 4)
        txtPresetName.Name = "txtPresetName"
        txtPresetName.Size = New Size(206, 27)
        txtPresetName.TabIndex = 0
        ' 
        ' lblCaseName
        ' 
        lblCaseName.AutoSize = True
        lblCaseName.Location = New Point(3, 16)
        lblCaseName.Name = "lblCaseName"
        lblCaseName.Size = New Size(84, 20)
        lblCaseName.TabIndex = 1
        lblCaseName.Text = "Case Name"
        ' 
        ' txtOutput
        ' 
        txtOutput.BackColor = SystemColors.HighlightText
        txtOutput.Enabled = False
        txtOutput.Location = New Point(967, 92)
        txtOutput.Margin = New Padding(3, 4, 3, 4)
        txtOutput.Name = "txtOutput"
        txtOutput.Size = New Size(506, 583)
        txtOutput.TabIndex = 20
        txtOutput.Text = ""
        ' 
        ' lblOutput
        ' 
        lblOutput.AutoSize = True
        lblOutput.Enabled = False
        lblOutput.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblOutput.Location = New Point(1176, 41)
        lblOutput.Name = "lblOutput"
        lblOutput.Size = New Size(108, 37)
        lblOutput.TabIndex = 3
        lblOutput.Text = "Output"
        ' 
        ' txtNotes
        ' 
        txtNotes.Location = New Point(162, 51)
        txtNotes.Margin = New Padding(3, 4, 3, 4)
        txtNotes.Name = "txtNotes"
        txtNotes.Size = New Size(206, 27)
        txtNotes.TabIndex = 1
        ' 
        ' lblDesc
        ' 
        lblDesc.AutoSize = True
        lblDesc.Location = New Point(3, 55)
        lblDesc.Name = "lblDesc"
        lblDesc.Size = New Size(85, 20)
        lblDesc.TabIndex = 5
        lblDesc.Text = "Description"
        ' 
        ' lblDisabled
        ' 
        lblDisabled.AutoSize = True
        lblDisabled.Location = New Point(3, 132)
        lblDisabled.Name = "lblDisabled"
        lblDisabled.Size = New Size(68, 20)
        lblDisabled.TabIndex = 7
        lblDisabled.Text = "Disabled"
        ' 
        ' cmbDisabled
        ' 
        cmbDisabled.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDisabled.FormattingEnabled = True
        cmbDisabled.Items.AddRange(New Object() {"false", "true"})
        cmbDisabled.Location = New Point(162, 128)
        cmbDisabled.Margin = New Padding(3, 4, 3, 4)
        cmbDisabled.Name = "cmbDisabled"
        cmbDisabled.Size = New Size(206, 28)
        cmbDisabled.TabIndex = 2
        ' 
        ' lblCompatWith
        ' 
        lblCompatWith.AutoSize = True
        lblCompatWith.Location = New Point(3, 171)
        lblCompatWith.Name = "lblCompatWith"
        lblCompatWith.Size = New Size(122, 20)
        lblCompatWith.TabIndex = 10
        lblCompatWith.Text = "Compatible With"
        ' 
        ' cmbCompatibleWith
        ' 
        cmbCompatibleWith.FormattingEnabled = True
        cmbCompatibleWith.Items.AddRange(New Object() {"Hitman", "Kidnapper", "SerialKiller", "Sniper"})
        cmbCompatibleWith.Location = New Point(162, 167)
        cmbCompatibleWith.Margin = New Padding(3, 4, 3, 4)
        cmbCompatibleWith.Name = "cmbCompatibleWith"
        cmbCompatibleWith.Size = New Size(206, 28)
        cmbCompatibleWith.TabIndex = 3
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(3, 317)
        Label6.Name = "Label6"
        Label6.Size = New Size(140, 20)
        Label6.TabIndex = 13
        Label6.Text = "Max Potential Score"
        ' 
        ' cmbUpdateThis
        ' 
        cmbUpdateThis.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUpdateThis.FormattingEnabled = True
        cmbUpdateThis.Items.AddRange(New Object() {"false", "true"})
        cmbUpdateThis.Location = New Point(162, 353)
        cmbUpdateThis.Margin = New Padding(3, 4, 3, 4)
        cmbUpdateThis.Name = "cmbUpdateThis"
        cmbUpdateThis.Size = New Size(206, 28)
        cmbUpdateThis.TabIndex = 5
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(3, 355)
        Label7.Name = "Label7"
        Label7.Size = New Size(88, 20)
        Label7.TabIndex = 14
        Label7.Text = "Update This"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(3, 393)
        Label8.Name = "Label8"
        Label8.Size = New Size(182, 20)
        Label8.TabIndex = 17
        Label8.Text = "Pick Random Score Range"
        ' 
        ' btnGenerateCase
        ' 
        btnGenerateCase.Enabled = False
        btnGenerateCase.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point)
        btnGenerateCase.Location = New Point(24, 804)
        btnGenerateCase.Margin = New Padding(3, 4, 3, 4)
        btnGenerateCase.Name = "btnGenerateCase"
        btnGenerateCase.Size = New Size(362, 31)
        btnGenerateCase.TabIndex = 19
        btnGenerateCase.Text = "Generate Case"
        btnGenerateCase.UseVisualStyleBackColor = True
        ' 
        ' tabControlCase
        ' 
        tabControlCase.Enabled = False
        tabControlCase.Location = New Point(429, 61)
        tabControlCase.Margin = New Padding(3, 4, 3, 4)
        tabControlCase.Name = "tabControlCase"
        tabControlCase.SelectedIndex = 0
        tabControlCase.Size = New Size(535, 783)
        tabControlCase.TabIndex = 21
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(6, 467)
        Label9.Name = "Label9"
        Label9.Size = New Size(162, 20)
        Label9.TabIndex = 24
        Label9.Text = "Same Workplace Boost"
        ' 
        ' cmbCopyFromMain
        ' 
        cmbCopyFromMain.FormattingEnabled = True
        cmbCopyFromMain.Items.AddRange(New Object() {"null", "CorporateJealousy", "ExCopSniper", "FinancialKidnapper", "Hitman", "MadScientistKidnapper", "RedGumsKiller", "TheCoporateKiller", "TheDoveKiller", "TheftGoneWrong", "TheRetiredKiller", "TheScammer", "VoyeurSniper"})
        cmbCopyFromMain.Location = New Point(162, 89)
        cmbCopyFromMain.Margin = New Padding(3, 4, 3, 4)
        cmbCopyFromMain.Name = "cmbCopyFromMain"
        cmbCopyFromMain.Size = New Size(206, 28)
        cmbCopyFromMain.TabIndex = 25
        cmbCopyFromMain.Text = "null"
        ' 
        ' lblCopyFromMain
        ' 
        lblCopyFromMain.AutoSize = True
        lblCopyFromMain.Location = New Point(3, 93)
        lblCopyFromMain.Name = "lblCopyFromMain"
        lblCopyFromMain.Size = New Size(81, 20)
        lblCopyFromMain.TabIndex = 26
        lblCopyFromMain.Text = "Copy From"
        ' 
        ' cmbVictimSocialClassRange
        ' 
        cmbVictimSocialClassRange.DropDownStyle = ComboBoxStyle.DropDownList
        cmbVictimSocialClassRange.FormattingEnabled = True
        cmbVictimSocialClassRange.Items.AddRange(New Object() {"false", "true"})
        cmbVictimSocialClassRange.Location = New Point(197, 159)
        cmbVictimSocialClassRange.Margin = New Padding(3, 4, 3, 4)
        cmbVictimSocialClassRange.Name = "cmbVictimSocialClassRange"
        cmbVictimSocialClassRange.Size = New Size(174, 28)
        cmbVictimSocialClassRange.TabIndex = 27
        ' 
        ' txtManiOutput
        ' 
        txtManiOutput.BackColor = SystemColors.HighlightText
        txtManiOutput.Enabled = False
        txtManiOutput.Location = New Point(967, 729)
        txtManiOutput.Margin = New Padding(3, 4, 3, 4)
        txtManiOutput.Name = "txtManiOutput"
        txtManiOutput.Size = New Size(506, 113)
        txtManiOutput.TabIndex = 31
        txtManiOutput.Text = ""
        ' 
        ' lblManifest
        ' 
        lblManifest.AutoSize = True
        lblManifest.Enabled = False
        lblManifest.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblManifest.Location = New Point(1176, 680)
        lblManifest.Name = "lblManifest"
        lblManifest.Size = New Size(130, 37)
        lblManifest.TabIndex = 32
        lblManifest.Text = "Manifest"
        ' 
        ' btnAllowedWeapons
        ' 
        btnAllowedWeapons.Location = New Point(7, 235)
        btnAllowedWeapons.Margin = New Padding(3, 4, 3, 4)
        btnAllowedWeapons.Name = "btnAllowedWeapons"
        btnAllowedWeapons.Size = New Size(365, 31)
        btnAllowedWeapons.TabIndex = 34
        btnAllowedWeapons.Text = "Add Weapons"
        btnAllowedWeapons.UseVisualStyleBackColor = True
        ' 
        ' cmbSniperVantage
        ' 
        cmbSniperVantage.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSniperVantage.FormattingEnabled = True
        cmbSniperVantage.Items.AddRange(New Object() {"false", "true"})
        cmbSniperVantage.Location = New Point(165, 273)
        cmbSniperVantage.Margin = New Padding(3, 4, 3, 4)
        cmbSniperVantage.Name = "cmbSniperVantage"
        cmbSniperVantage.Size = New Size(206, 28)
        cmbSniperVantage.TabIndex = 36
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Location = New Point(6, 277)
        Label13.Name = "Label13"
        Label13.Size = New Size(170, 20)
        Label13.TabIndex = 37
        Label13.Text = "Requires Sniper Vantage"
        ' 
        ' cmbBlockWeaponDrops
        ' 
        cmbBlockWeaponDrops.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBlockWeaponDrops.FormattingEnabled = True
        cmbBlockWeaponDrops.Items.AddRange(New Object() {"false", "true"})
        cmbBlockWeaponDrops.Location = New Point(174, 312)
        cmbBlockWeaponDrops.Margin = New Padding(3, 4, 3, 4)
        cmbBlockWeaponDrops.Name = "cmbBlockWeaponDrops"
        cmbBlockWeaponDrops.Size = New Size(197, 28)
        cmbBlockWeaponDrops.TabIndex = 38
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(6, 316)
        Label14.Name = "Label14"
        Label14.Size = New Size(178, 20)
        Label14.TabIndex = 39
        Label14.Text = "Block Dropping Weapons"
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Enabled = False
        TabControl1.Location = New Point(14, 60)
        TabControl1.Margin = New Padding(3, 4, 3, 4)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(402, 820)
        TabControl1.TabIndex = 44
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(btnAddPlayerTauntTab)
        TabPage1.Controls.Add(txtMonkier)
        TabPage1.Controls.Add(Label11)
        TabPage1.Controls.Add(btnAddMOlead)
        TabPage1.Controls.Add(btnOpenHexaco)
        TabPage1.Controls.Add(Label19)
        TabPage1.Controls.Add(cmbUseHexaco)
        TabPage1.Controls.Add(nudRndScoreY)
        TabPage1.Controls.Add(nudRndScoreX)
        TabPage1.Controls.Add(btnRemoveCompatibleWith)
        TabPage1.Controls.Add(btnAddCompat)
        TabPage1.Controls.Add(lstCompatibleWith)
        TabPage1.Controls.Add(nudMaxPotScore)
        TabPage1.Controls.Add(nudBaseDifficulty)
        TabPage1.Controls.Add(Label15)
        TabPage1.Controls.Add(lblCaseName)
        TabPage1.Controls.Add(txtPresetName)
        TabPage1.Controls.Add(txtNotes)
        TabPage1.Controls.Add(lblDesc)
        TabPage1.Controls.Add(lblDisabled)
        TabPage1.Controls.Add(cmbDisabled)
        TabPage1.Controls.Add(lblCompatWith)
        TabPage1.Controls.Add(cmbCompatibleWith)
        TabPage1.Controls.Add(Label6)
        TabPage1.Controls.Add(Label7)
        TabPage1.Controls.Add(cmbUpdateThis)
        TabPage1.Controls.Add(Label8)
        TabPage1.Controls.Add(cmbCopyFromMain)
        TabPage1.Controls.Add(lblCopyFromMain)
        TabPage1.Location = New Point(4, 29)
        TabPage1.Margin = New Padding(3, 4, 3, 4)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3, 4, 3, 4)
        TabPage1.Size = New Size(394, 787)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Main"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' btnAddPlayerTauntTab
        ' 
        btnAddPlayerTauntTab.Location = New Point(7, 579)
        btnAddPlayerTauntTab.Margin = New Padding(3, 4, 3, 4)
        btnAddPlayerTauntTab.Name = "btnAddPlayerTauntTab"
        btnAddPlayerTauntTab.Size = New Size(365, 31)
        btnAddPlayerTauntTab.TabIndex = 65
        btnAddPlayerTauntTab.Text = "Player Taunts"
        btnAddPlayerTauntTab.UseVisualStyleBackColor = True
        ' 
        ' txtMonkier
        ' 
        txtMonkier.Location = New Point(176, 544)
        txtMonkier.Margin = New Padding(3, 4, 3, 4)
        txtMonkier.Name = "txtMonkier"
        txtMonkier.Size = New Size(193, 27)
        txtMonkier.TabIndex = 52
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(3, 549)
        Label11.Name = "Label11"
        Label11.Size = New Size(185, 20)
        Label11.TabIndex = 53
        Label11.Text = "Monkier DDS Message List"
        ' 
        ' btnAddMOlead
        ' 
        btnAddMOlead.Location = New Point(7, 505)
        btnAddMOlead.Margin = New Padding(3, 4, 3, 4)
        btnAddMOlead.Name = "btnAddMOlead"
        btnAddMOlead.Size = New Size(365, 31)
        btnAddMOlead.TabIndex = 30
        btnAddMOlead.Text = "Add New Lead"
        btnAddMOlead.UseVisualStyleBackColor = True
        ' 
        ' btnOpenHexaco
        ' 
        btnOpenHexaco.Location = New Point(7, 467)
        btnOpenHexaco.Margin = New Padding(3, 4, 3, 4)
        btnOpenHexaco.Name = "btnOpenHexaco"
        btnOpenHexaco.Size = New Size(365, 31)
        btnOpenHexaco.TabIndex = 51
        btnOpenHexaco.Text = "Hexaco Settings"
        btnOpenHexaco.UseVisualStyleBackColor = True
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(3, 432)
        Label19.Name = "Label19"
        Label19.Size = New Size(87, 20)
        Label19.TabIndex = 50
        Label19.Text = "Use Hexaco"
        ' 
        ' cmbUseHexaco
        ' 
        cmbUseHexaco.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUseHexaco.FormattingEnabled = True
        cmbUseHexaco.Items.AddRange(New Object() {"false", "true"})
        cmbUseHexaco.Location = New Point(162, 428)
        cmbUseHexaco.Margin = New Padding(3, 4, 3, 4)
        cmbUseHexaco.Name = "cmbUseHexaco"
        cmbUseHexaco.Size = New Size(209, 28)
        cmbUseHexaco.TabIndex = 49
        ' 
        ' nudRndScoreY
        ' 
        nudRndScoreY.Location = New Point(281, 391)
        nudRndScoreY.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudRndScoreY.Name = "nudRndScoreY"
        nudRndScoreY.Size = New Size(90, 27)
        nudRndScoreY.TabIndex = 48
        nudRndScoreY.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' nudRndScoreX
        ' 
        nudRndScoreX.Location = New Point(176, 391)
        nudRndScoreX.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudRndScoreX.Name = "nudRndScoreX"
        nudRndScoreX.Size = New Size(87, 27)
        nudRndScoreX.TabIndex = 47
        ' 
        ' btnRemoveCompatibleWith
        ' 
        btnRemoveCompatibleWith.Location = New Point(11, 235)
        btnRemoveCompatibleWith.Name = "btnRemoveCompatibleWith"
        btnRemoveCompatibleWith.Size = New Size(85, 31)
        btnRemoveCompatibleWith.TabIndex = 46
        btnRemoveCompatibleWith.Text = "Remove"
        btnRemoveCompatibleWith.UseVisualStyleBackColor = True
        ' 
        ' btnAddCompat
        ' 
        btnAddCompat.Location = New Point(11, 203)
        btnAddCompat.Name = "btnAddCompat"
        btnAddCompat.Size = New Size(85, 31)
        btnAddCompat.TabIndex = 45
        btnAddCompat.Text = "Add"
        btnAddCompat.UseVisualStyleBackColor = True
        ' 
        ' lstCompatibleWith
        ' 
        lstCompatibleWith.FormattingEnabled = True
        lstCompatibleWith.ItemHeight = 20
        lstCompatibleWith.Items.AddRange(New Object() {"SerialKiller"})
        lstCompatibleWith.Location = New Point(102, 203)
        lstCompatibleWith.Name = "lstCompatibleWith"
        lstCompatibleWith.Size = New Size(267, 64)
        lstCompatibleWith.TabIndex = 44
        ' 
        ' nudMaxPotScore
        ' 
        nudMaxPotScore.Location = New Point(162, 316)
        nudMaxPotScore.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudMaxPotScore.Name = "nudMaxPotScore"
        nudMaxPotScore.Size = New Size(207, 27)
        nudMaxPotScore.TabIndex = 43
        ' 
        ' nudBaseDifficulty
        ' 
        nudBaseDifficulty.Location = New Point(162, 280)
        nudBaseDifficulty.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudBaseDifficulty.Name = "nudBaseDifficulty"
        nudBaseDifficulty.Size = New Size(207, 27)
        nudBaseDifficulty.TabIndex = 42
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Location = New Point(3, 283)
        Label15.Name = "Label15"
        Label15.Size = New Size(104, 20)
        Label15.TabIndex = 41
        Label15.Text = "Base Difficulty"
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(btnAddConfessionalTab)
        TabPage2.Controls.Add(btnAddGraffiti)
        TabPage2.Controls.Add(btnCallingCard)
        TabPage2.Controls.Add(Label23)
        TabPage2.Controls.Add(nudMurdererIsTenantBoost)
        TabPage2.Controls.Add(Label22)
        TabPage2.Controls.Add(nudLikeSuitBoost)
        TabPage2.Controls.Add(Label21)
        TabPage2.Controls.Add(nudAttractSuitBoost)
        TabPage2.Controls.Add(Label20)
        TabPage2.Controls.Add(nudAcquaintSuitBoost)
        TabPage2.Controls.Add(Label9)
        TabPage2.Controls.Add(btnAllowedWeapons)
        TabPage2.Controls.Add(cmbBlockWeaponDrops)
        TabPage2.Controls.Add(nudSameWork)
        TabPage2.Controls.Add(nudMurdererCRBoost)
        TabPage2.Controls.Add(Label14)
        TabPage2.Controls.Add(btnAddMurdererTraits)
        TabPage2.Controls.Add(cmbSniperVantage)
        TabPage2.Controls.Add(Label13)
        TabPage2.Controls.Add(btnAddMurdererJobModifier)
        TabPage2.Controls.Add(nudMurdererCrY)
        TabPage2.Controls.Add(btnAddNewMurdererCompModifier)
        TabPage2.Controls.Add(Label16)
        TabPage2.Controls.Add(Label18)
        TabPage2.Controls.Add(cmbMurdererClassRange)
        TabPage2.Controls.Add(Label17)
        TabPage2.Controls.Add(nudMurdererCrX)
        TabPage2.Location = New Point(4, 29)
        TabPage2.Margin = New Padding(3, 4, 3, 4)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3, 4, 3, 4)
        TabPage2.Size = New Size(394, 787)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Murderer"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' btnAddConfessionalTab
        ' 
        btnAddConfessionalTab.Location = New Point(6, 613)
        btnAddConfessionalTab.Margin = New Padding(3, 4, 3, 4)
        btnAddConfessionalTab.Name = "btnAddConfessionalTab"
        btnAddConfessionalTab.Size = New Size(365, 31)
        btnAddConfessionalTab.TabIndex = 54
        btnAddConfessionalTab.Text = "Confessional DDS Responses"
        btnAddConfessionalTab.UseVisualStyleBackColor = True
        ' 
        ' btnAddGraffiti
        ' 
        btnAddGraffiti.Location = New Point(7, 536)
        btnAddGraffiti.Margin = New Padding(3, 4, 3, 4)
        btnAddGraffiti.Name = "btnAddGraffiti"
        btnAddGraffiti.Size = New Size(365, 31)
        btnAddGraffiti.TabIndex = 64
        btnAddGraffiti.Text = "Add Graffiti"
        btnAddGraffiti.UseVisualStyleBackColor = True
        ' 
        ' btnCallingCard
        ' 
        btnCallingCard.Location = New Point(7, 575)
        btnCallingCard.Margin = New Padding(3, 4, 3, 4)
        btnCallingCard.Name = "btnCallingCard"
        btnCallingCard.Size = New Size(365, 31)
        btnCallingCard.TabIndex = 31
        btnCallingCard.Text = "Calling Card Pool "
        btnCallingCard.UseVisualStyleBackColor = True
        ' 
        ' Label23
        ' 
        Label23.AutoSize = True
        Label23.Location = New Point(6, 503)
        Label23.Name = "Label23"
        Label23.Size = New Size(174, 20)
        Label23.TabIndex = 62
        Label23.Text = "Murderer Is Tenant Boost"
        ' 
        ' nudMurdererIsTenantBoost
        ' 
        nudMurdererIsTenantBoost.Location = New Point(165, 499)
        nudMurdererIsTenantBoost.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudMurdererIsTenantBoost.Name = "nudMurdererIsTenantBoost"
        nudMurdererIsTenantBoost.Size = New Size(207, 27)
        nudMurdererIsTenantBoost.TabIndex = 63
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.Location = New Point(6, 431)
        Label22.Name = "Label22"
        Label22.Size = New Size(147, 20)
        Label22.TabIndex = 60
        Label22.Text = "Like Suitability Boost"
        ' 
        ' nudLikeSuitBoost
        ' 
        nudLikeSuitBoost.Location = New Point(165, 427)
        nudLikeSuitBoost.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudLikeSuitBoost.Name = "nudLikeSuitBoost"
        nudLikeSuitBoost.Size = New Size(207, 27)
        nudLikeSuitBoost.TabIndex = 61
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Location = New Point(6, 395)
        Label21.Name = "Label21"
        Label21.Size = New Size(203, 20)
        Label21.TabIndex = 58
        Label21.Text = "Attracted To Suitability Boost"
        ' 
        ' nudAttractSuitBoost
        ' 
        nudAttractSuitBoost.Location = New Point(191, 391)
        nudAttractSuitBoost.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudAttractSuitBoost.Name = "nudAttractSuitBoost"
        nudAttractSuitBoost.Size = New Size(181, 27)
        nudAttractSuitBoost.TabIndex = 59
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(6, 359)
        Label20.Name = "Label20"
        Label20.Size = New Size(197, 20)
        Label20.TabIndex = 56
        Label20.Text = "Acquainted Suitability Boost"
        ' 
        ' nudAcquaintSuitBoost
        ' 
        nudAcquaintSuitBoost.Location = New Point(191, 355)
        nudAcquaintSuitBoost.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudAcquaintSuitBoost.Name = "nudAcquaintSuitBoost"
        nudAcquaintSuitBoost.Size = New Size(181, 27)
        nudAcquaintSuitBoost.TabIndex = 57
        ' 
        ' nudSameWork
        ' 
        nudSameWork.Location = New Point(165, 463)
        nudSameWork.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudSameWork.Name = "nudSameWork"
        nudSameWork.Size = New Size(207, 27)
        nudSameWork.TabIndex = 45
        ' 
        ' nudMurdererCRBoost
        ' 
        nudMurdererCRBoost.Location = New Point(165, 197)
        nudMurdererCRBoost.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudMurdererCRBoost.Name = "nudMurdererCRBoost"
        nudMurdererCRBoost.Size = New Size(207, 27)
        nudMurdererCRBoost.TabIndex = 47
        ' 
        ' btnAddMurdererTraits
        ' 
        btnAddMurdererTraits.Location = New Point(7, 8)
        btnAddMurdererTraits.Margin = New Padding(3, 4, 3, 4)
        btnAddMurdererTraits.Name = "btnAddMurdererTraits"
        btnAddMurdererTraits.Size = New Size(365, 31)
        btnAddMurdererTraits.TabIndex = 40
        btnAddMurdererTraits.Text = "New Murderer Traits"
        btnAddMurdererTraits.UseVisualStyleBackColor = True
        ' 
        ' btnAddMurdererJobModifier
        ' 
        btnAddMurdererJobModifier.Location = New Point(7, 47)
        btnAddMurdererJobModifier.Margin = New Padding(3, 4, 3, 4)
        btnAddMurdererJobModifier.Name = "btnAddMurdererJobModifier"
        btnAddMurdererJobModifier.Size = New Size(365, 31)
        btnAddMurdererJobModifier.TabIndex = 49
        btnAddMurdererJobModifier.Text = "New Murderer Job Modifier"
        btnAddMurdererJobModifier.UseVisualStyleBackColor = True
        ' 
        ' nudMurdererCrY
        ' 
        nudMurdererCrY.Location = New Point(275, 160)
        nudMurdererCrY.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudMurdererCrY.Name = "nudMurdererCrY"
        nudMurdererCrY.Size = New Size(96, 27)
        nudMurdererCrY.TabIndex = 55
        nudMurdererCrY.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' btnAddNewMurdererCompModifier
        ' 
        btnAddNewMurdererCompModifier.Location = New Point(7, 85)
        btnAddNewMurdererCompModifier.Margin = New Padding(3, 4, 3, 4)
        btnAddNewMurdererCompModifier.Name = "btnAddNewMurdererCompModifier"
        btnAddNewMurdererCompModifier.Size = New Size(365, 31)
        btnAddNewMurdererCompModifier.TabIndex = 50
        btnAddNewMurdererCompModifier.Text = "New Murderer Company Modifier"
        btnAddNewMurdererCompModifier.UseVisualStyleBackColor = True
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(3, 128)
        Label16.Name = "Label16"
        Label16.Size = New Size(225, 20)
        Label16.TabIndex = 52
        Label16.Text = "Murderer Use Social Class Range"
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(3, 200)
        Label18.Name = "Label18"
        Label18.Size = New Size(130, 20)
        Label18.TabIndex = 46
        Label18.Text = "Class Range Boost"
        ' 
        ' cmbMurdererClassRange
        ' 
        cmbMurdererClassRange.DropDownStyle = ComboBoxStyle.DropDownList
        cmbMurdererClassRange.FormattingEnabled = True
        cmbMurdererClassRange.Items.AddRange(New Object() {"false", "true"})
        cmbMurdererClassRange.Location = New Point(214, 123)
        cmbMurdererClassRange.Margin = New Padding(3, 4, 3, 4)
        cmbMurdererClassRange.Name = "cmbMurdererClassRange"
        cmbMurdererClassRange.Size = New Size(157, 28)
        cmbMurdererClassRange.TabIndex = 51
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(3, 164)
        Label17.Name = "Label17"
        Label17.Size = New Size(88, 20)
        Label17.TabIndex = 53
        Label17.Text = "Class Range"
        ' 
        ' nudMurdererCrX
        ' 
        nudMurdererCrX.Location = New Point(165, 160)
        nudMurdererCrX.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudMurdererCrX.Name = "nudMurdererCrX"
        nudMurdererCrX.Size = New Size(96, 27)
        nudMurdererCrX.TabIndex = 54
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(Label27)
        TabPage3.Controls.Add(nudVictimCRBoost)
        TabPage3.Controls.Add(nudVictimCrY)
        TabPage3.Controls.Add(Label25)
        TabPage3.Controls.Add(Label26)
        TabPage3.Controls.Add(nudVictimCrX)
        TabPage3.Controls.Add(btnAddVictimCompanyModifier)
        TabPage3.Controls.Add(btnAddVictimJobModifier)
        TabPage3.Controls.Add(btnAddVictimTraits)
        TabPage3.Controls.Add(nudVictimRndCrY)
        TabPage3.Controls.Add(nudVictimRndCrX)
        TabPage3.Controls.Add(Label24)
        TabPage3.Controls.Add(cmbVictimSocialClassRange)
        TabPage3.Location = New Point(4, 29)
        TabPage3.Margin = New Padding(3, 4, 3, 4)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(3, 4, 3, 4)
        TabPage3.Size = New Size(394, 787)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Victim"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' Label27
        ' 
        Label27.AutoSize = True
        Label27.Location = New Point(3, 163)
        Label27.Name = "Label27"
        Label27.Size = New Size(206, 20)
        Label27.TabIndex = 62
        Label27.Text = "Victim Use Social Class Range"
        ' 
        ' nudVictimCRBoost
        ' 
        nudVictimCRBoost.Location = New Point(165, 233)
        nudVictimCRBoost.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudVictimCRBoost.Name = "nudVictimCRBoost"
        nudVictimCRBoost.Size = New Size(207, 27)
        nudVictimCRBoost.TabIndex = 57
        ' 
        ' nudVictimCrY
        ' 
        nudVictimCrY.Location = New Point(275, 196)
        nudVictimCrY.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudVictimCrY.Name = "nudVictimCrY"
        nudVictimCrY.Size = New Size(96, 27)
        nudVictimCrY.TabIndex = 60
        nudVictimCrY.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Label25
        ' 
        Label25.AutoSize = True
        Label25.Location = New Point(3, 236)
        Label25.Name = "Label25"
        Label25.Size = New Size(130, 20)
        Label25.TabIndex = 56
        Label25.Text = "Class Range Boost"
        ' 
        ' Label26
        ' 
        Label26.AutoSize = True
        Label26.Location = New Point(3, 200)
        Label26.Name = "Label26"
        Label26.Size = New Size(88, 20)
        Label26.TabIndex = 58
        Label26.Text = "Class Range"
        ' 
        ' nudVictimCrX
        ' 
        nudVictimCrX.Location = New Point(165, 196)
        nudVictimCrX.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudVictimCrX.Name = "nudVictimCrX"
        nudVictimCrX.Size = New Size(96, 27)
        nudVictimCrX.TabIndex = 59
        ' 
        ' btnAddVictimCompanyModifier
        ' 
        btnAddVictimCompanyModifier.Location = New Point(7, 85)
        btnAddVictimCompanyModifier.Margin = New Padding(3, 4, 3, 4)
        btnAddVictimCompanyModifier.Name = "btnAddVictimCompanyModifier"
        btnAddVictimCompanyModifier.Size = New Size(365, 31)
        btnAddVictimCompanyModifier.TabIndex = 54
        btnAddVictimCompanyModifier.Text = "New Victim Company Modifier"
        btnAddVictimCompanyModifier.UseVisualStyleBackColor = True
        ' 
        ' btnAddVictimJobModifier
        ' 
        btnAddVictimJobModifier.Location = New Point(7, 47)
        btnAddVictimJobModifier.Margin = New Padding(3, 4, 3, 4)
        btnAddVictimJobModifier.Name = "btnAddVictimJobModifier"
        btnAddVictimJobModifier.Size = New Size(365, 31)
        btnAddVictimJobModifier.TabIndex = 53
        btnAddVictimJobModifier.Text = "New Victim Job Modifier"
        btnAddVictimJobModifier.UseVisualStyleBackColor = True
        ' 
        ' btnAddVictimTraits
        ' 
        btnAddVictimTraits.Location = New Point(7, 8)
        btnAddVictimTraits.Margin = New Padding(3, 4, 3, 4)
        btnAddVictimTraits.Name = "btnAddVictimTraits"
        btnAddVictimTraits.Size = New Size(365, 31)
        btnAddVictimTraits.TabIndex = 52
        btnAddVictimTraits.Text = "New Victim Traits"
        btnAddVictimTraits.UseVisualStyleBackColor = True
        ' 
        ' nudVictimRndCrY
        ' 
        nudVictimRndCrY.Location = New Point(288, 121)
        nudVictimRndCrY.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudVictimRndCrY.Name = "nudVictimRndCrY"
        nudVictimRndCrY.Size = New Size(83, 27)
        nudVictimRndCrY.TabIndex = 51
        nudVictimRndCrY.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' nudVictimRndCrX
        ' 
        nudVictimRndCrX.Location = New Point(192, 121)
        nudVictimRndCrX.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudVictimRndCrX.Name = "nudVictimRndCrX"
        nudVictimRndCrX.Size = New Size(83, 27)
        nudVictimRndCrX.TabIndex = 50
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.Location = New Point(3, 125)
        Label24.Name = "Label24"
        Label24.Size = New Size(198, 20)
        Label24.TabIndex = 49
        Label24.Text = "Victim Random Score Range"
        ' 
        ' pnCover
        ' 
        pnCover.BackColor = Color.Silver
        pnCover.Location = New Point(0, 36)
        pnCover.Margin = New Padding(3, 4, 3, 4)
        pnCover.Name = "pnCover"
        pnCover.Size = New Size(1482, 831)
        pnCover.TabIndex = 47
        ' 
        ' lblOpen
        ' 
        lblOpen.AutoSize = True
        lblOpen.BackColor = Color.Transparent
        lblOpen.Font = New Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point)
        lblOpen.Location = New Point(395, 333)
        lblOpen.Name = "lblOpen"
        lblOpen.Size = New Size(786, 81)
        lblOpen.TabIndex = 46
        lblOpen.Text = "Open or create a new case."
        ' 
        ' msMain
        ' 
        msMain.BackColor = Color.White
        msMain.ImageScalingSize = New Size(20, 20)
        msMain.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem, SearchToolStripMenuItem, ToolsToolStripMenuItem, HelpToolStripMenuItem, UpdateToolStripMenuItem, itemAutoGen, itemRemoveKeys, itemToolTips})
        msMain.Location = New Point(0, 0)
        msMain.Name = "msMain"
        msMain.Padding = New Padding(0, 0, 0, 3)
        msMain.Size = New Size(1481, 27)
        msMain.TabIndex = 45
        msMain.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.BackColor = Color.White
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {NewToolStripMenuItem, OpenToolStripMenuItem, SaveCaseToolStripMenuItem, CloseCaseToolStripMenuItem})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Padding = New Padding(0, 0, 4, 0)
        FileToolStripMenuItem.Size = New Size(40, 24)
        FileToolStripMenuItem.Text = "File"
        ' 
        ' NewToolStripMenuItem
        ' 
        NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        NewToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.N
        NewToolStripMenuItem.Size = New Size(216, 26)
        NewToolStripMenuItem.Text = "New Case"
        ' 
        ' OpenToolStripMenuItem
        ' 
        OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        OpenToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.O
        OpenToolStripMenuItem.Size = New Size(216, 26)
        OpenToolStripMenuItem.Text = "Open Case"
        ' 
        ' SaveCaseToolStripMenuItem
        ' 
        SaveCaseToolStripMenuItem.Name = "SaveCaseToolStripMenuItem"
        SaveCaseToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.S
        SaveCaseToolStripMenuItem.Size = New Size(216, 26)
        SaveCaseToolStripMenuItem.Text = "Save Case"
        ' 
        ' CloseCaseToolStripMenuItem
        ' 
        CloseCaseToolStripMenuItem.Name = "CloseCaseToolStripMenuItem"
        CloseCaseToolStripMenuItem.Size = New Size(216, 26)
        CloseCaseToolStripMenuItem.Text = "Close Case"
        ' 
        ' SearchToolStripMenuItem
        ' 
        SearchToolStripMenuItem.BackColor = Color.White
        SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
        SearchToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.F
        SearchToolStripMenuItem.Size = New Size(121, 24)
        SearchToolStripMenuItem.Text = "Search (Ctrl+F)"
        SearchToolStripMenuItem.Visible = False
        ' 
        ' ToolsToolStripMenuItem
        ' 
        ToolsToolStripMenuItem.BackColor = Color.White
        ToolsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {NewDDSToolStripMenuItem, LoadStringListToolStripMenuItem, RemoveUnusedKeysToolStripMenuItem})
        ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        ToolsToolStripMenuItem.Size = New Size(58, 24)
        ToolsToolStripMenuItem.Text = "Tools"
        ToolsToolStripMenuItem.Visible = False
        ' 
        ' NewDDSToolStripMenuItem
        ' 
        NewDDSToolStripMenuItem.Name = "NewDDSToolStripMenuItem"
        NewDDSToolStripMenuItem.Size = New Size(233, 26)
        NewDDSToolStripMenuItem.Text = "New DDS String"
        NewDDSToolStripMenuItem.ToolTipText = "Create a new DDS string (V-mails etc)"
        ' 
        ' LoadStringListToolStripMenuItem
        ' 
        LoadStringListToolStripMenuItem.Name = "LoadStringListToolStripMenuItem"
        LoadStringListToolStripMenuItem.Size = New Size(233, 26)
        LoadStringListToolStripMenuItem.Text = "View String List"
        LoadStringListToolStripMenuItem.ToolTipText = "Open the list of strings in the project."
        ' 
        ' RemoveUnusedKeysToolStripMenuItem
        ' 
        RemoveUnusedKeysToolStripMenuItem.Name = "RemoveUnusedKeysToolStripMenuItem"
        RemoveUnusedKeysToolStripMenuItem.Size = New Size(233, 26)
        RemoveUnusedKeysToolStripMenuItem.Text = "Remove Unused Keys"
        RemoveUnusedKeysToolStripMenuItem.ToolTipText = "Removes unused/default keys."
        ' 
        ' HelpToolStripMenuItem
        ' 
        HelpToolStripMenuItem.BackColor = Color.White
        HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        HelpToolStripMenuItem.Size = New Size(55, 24)
        HelpToolStripMenuItem.Text = "Help"
        ' 
        ' UpdateToolStripMenuItem
        ' 
        UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem"
        UpdateToolStripMenuItem.Size = New Size(146, 24)
        UpdateToolStripMenuItem.Text = "Check For Updates"
        ' 
        ' itemAutoGen
        ' 
        itemAutoGen.Margin = New Padding(135, 0, 0, 0)
        itemAutoGen.Name = "itemAutoGen"
        itemAutoGen.RightToLeft = RightToLeft.Yes
        itemAutoGen.Size = New Size(132, 24)
        itemAutoGen.Text = "Auto Generation"
        itemAutoGen.ToolTipText = "Automatically generate when an input has changed."
        ' 
        ' itemRemoveKeys
        ' 
        itemRemoveKeys.Name = "itemRemoveKeys"
        itemRemoveKeys.Size = New Size(164, 24)
        itemRemoveKeys.Text = "Remove Unused Keys"
        itemRemoveKeys.ToolTipText = "Automatically remove unused/default keys."
        ' 
        ' itemToolTips
        ' 
        itemToolTips.Name = "itemToolTips"
        itemToolTips.Size = New Size(79, 24)
        itemToolTips.Text = "ToolTips"
        itemToolTips.ToolTipText = "Toggle ToolTips"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        BackColor = Color.LightGray
        ClientSize = New Size(1481, 843)
        Controls.Add(btnGenerateCase)
        Controls.Add(TabControl1)
        Controls.Add(lblManifest)
        Controls.Add(txtManiOutput)
        Controls.Add(tabControlCase)
        Controls.Add(lblOutput)
        Controls.Add(txtOutput)
        Controls.Add(msMain)
        Controls.Add(pnCover)
        Controls.Add(lblOpen)
        MainMenuStrip = msMain
        Margin = New Padding(3, 4, 3, 4)
        MaximizeBox = False
        MaximumSize = New Size(1499, 890)
        MinimumSize = New Size(1499, 890)
        Name = "Form1"
        Text = "Case Generator"
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        CType(nudRndScoreY, ComponentModel.ISupportInitialize).EndInit()
        CType(nudRndScoreX, ComponentModel.ISupportInitialize).EndInit()
        CType(nudMaxPotScore, ComponentModel.ISupportInitialize).EndInit()
        CType(nudBaseDifficulty, ComponentModel.ISupportInitialize).EndInit()
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        CType(nudMurdererIsTenantBoost, ComponentModel.ISupportInitialize).EndInit()
        CType(nudLikeSuitBoost, ComponentModel.ISupportInitialize).EndInit()
        CType(nudAttractSuitBoost, ComponentModel.ISupportInitialize).EndInit()
        CType(nudAcquaintSuitBoost, ComponentModel.ISupportInitialize).EndInit()
        CType(nudSameWork, ComponentModel.ISupportInitialize).EndInit()
        CType(nudMurdererCRBoost, ComponentModel.ISupportInitialize).EndInit()
        CType(nudMurdererCrY, ComponentModel.ISupportInitialize).EndInit()
        CType(nudMurdererCrX, ComponentModel.ISupportInitialize).EndInit()
        TabPage3.ResumeLayout(False)
        TabPage3.PerformLayout()
        CType(nudVictimCRBoost, ComponentModel.ISupportInitialize).EndInit()
        CType(nudVictimCrY, ComponentModel.ISupportInitialize).EndInit()
        CType(nudVictimCrX, ComponentModel.ISupportInitialize).EndInit()
        CType(nudVictimRndCrY, ComponentModel.ISupportInitialize).EndInit()
        CType(nudVictimRndCrX, ComponentModel.ISupportInitialize).EndInit()
        msMain.ResumeLayout(False)
        msMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtPresetName As TextBox
    Friend WithEvents lblCaseName As Label
    Friend WithEvents txtOutput As RichTextBox
    Friend WithEvents lblOutput As Label
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents lblDesc As Label
    Friend WithEvents lblDisabled As Label
    Friend WithEvents cmbDisabled As ComboBox
    Friend WithEvents lblCompatWith As Label
    Friend WithEvents cmbCompatibleWith As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbUpdateThis As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents btnGenerateCase As Button
    Friend WithEvents tabControlCase As TabControl
    Friend WithEvents Label9 As Label
    Friend WithEvents cmbCopyFromMain As ComboBox
    Friend WithEvents lblCopyFromMain As Label
    Friend WithEvents cmbVictimSocialClassRange As ComboBox
    Friend WithEvents txtManiOutput As RichTextBox
    Friend WithEvents lblManifest As Label
    Friend WithEvents btnAllowedWeapons As Button
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents cmbSniperVantage As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents cmbBlockWeaponDrops As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents btnAddMurdererTraits As Button
    Friend WithEvents btnCallingCard As Button
    Friend WithEvents btnAddMOlead As Button
    Friend WithEvents nudBaseDifficulty As NumericUpDown
    Friend WithEvents Label15 As Label
    Friend WithEvents nudMaxPotScore As NumericUpDown
    Friend WithEvents btnAddCompat As Button
    Friend WithEvents lstCompatibleWith As ListBox
    Friend WithEvents btnRemoveCompatibleWith As Button
    Friend WithEvents nudSameWork As NumericUpDown
    Friend WithEvents nudRndScoreY As NumericUpDown
    Friend WithEvents nudRndScoreX As NumericUpDown
    Friend WithEvents btnAddMurdererJobModifier As Button
    Friend WithEvents btnAddNewMurdererCompModifier As Button
    Friend WithEvents cmbMurdererClassRange As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents nudMurdererCrY As NumericUpDown
    Friend WithEvents nudMurdererCrX As NumericUpDown
    Friend WithEvents Label17 As Label
    Friend WithEvents nudMurdererCRBoost As NumericUpDown
    Friend WithEvents Label18 As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Label19 As Label
    Friend WithEvents cmbUseHexaco As ComboBox
    Friend WithEvents btnOpenHexaco As Button
    Friend WithEvents Label23 As Label
    Friend WithEvents nudMurdererIsTenantBoost As NumericUpDown
    Friend WithEvents Label22 As Label
    Friend WithEvents nudLikeSuitBoost As NumericUpDown
    Friend WithEvents Label21 As Label
    Friend WithEvents nudAttractSuitBoost As NumericUpDown
    Friend WithEvents Label20 As Label
    Friend WithEvents nudAcquaintSuitBoost As NumericUpDown
    Friend WithEvents nudVictimRndCrY As NumericUpDown
    Friend WithEvents nudVictimRndCrX As NumericUpDown
    Friend WithEvents Label24 As Label
    Friend WithEvents btnAddVictimTraits As Button
    Friend WithEvents btnAddVictimJobModifier As Button
    Friend WithEvents btnAddVictimCompanyModifier As Button
    Friend WithEvents Label27 As Label
    Friend WithEvents nudVictimCRBoost As NumericUpDown
    Friend WithEvents nudVictimCrY As NumericUpDown
    Friend WithEvents Label25 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents nudVictimCrX As NumericUpDown
    Friend WithEvents txtMonkier As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents btnAddConfessionalTab As Button
    Friend WithEvents btnAddGraffiti As Button
    Friend WithEvents btnAddPlayerTauntTab As Button
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveCaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SearchToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewDDSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadStringListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lblOpen As Label
    Friend WithEvents pnCover As Panel
    Friend WithEvents RemoveUnusedKeysToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseCaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UpdateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents itemAutoGen As ToolStripMenuItem
    Friend WithEvents itemRemoveKeys As ToolStripMenuItem
    Friend WithEvents itemToolTips As ToolStripMenuItem
End Class
