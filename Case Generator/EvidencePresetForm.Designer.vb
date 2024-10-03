<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EvidencePresetForm
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
        lblCaseName = New Label()
        txtPresetName = New TextBox()
        cmbCopyFromMain = New ComboBox()
        lblCopyFromMain = New Label()
        lblOutput = New Label()
        txtOutput = New RichTextBox()
        tcMain = New TabControl()
        TabPage1 = New TabPage()
        Label5 = New Label()
        cmbNotifyOfTies = New ComboBox()
        txtPassiveTies = New TextBox()
        btnRemovePassiveTies = New Button()
        btnAddPassiveTies = New Button()
        Label4 = New Label()
        lbPassiveTies = New ListBox()
        txtValidKeys = New TextBox()
        btnRemoveValidKeys = New Button()
        btnAddValidKeys = New Button()
        Label3 = New Label()
        lbValidKeys = New ListBox()
        lblDisabled = New Label()
        cmbUseDataKeys = New ComboBox()
        cmbWindowStyle = New ComboBox()
        Label2 = New Label()
        txtSubClass = New TextBox()
        Label1 = New Label()
        TabPage2 = New TabPage()
        cmbCaptureRules = New ComboBox()
        Label19 = New Label()
        nudRelCamPhotoEulerY = New NumericUpDown()
        nudRelCamPhotoEulerX = New NumericUpDown()
        nudRelCamPhotoEulerZ = New NumericUpDown()
        Label18 = New Label()
        nudRelCamPhotoPosY = New NumericUpDown()
        nudRelCamPhotoPosX = New NumericUpDown()
        nudRelCamPhotoPosZ = New NumericUpDown()
        Label17 = New Label()
        Label16 = New Label()
        cmbUseWriter = New ComboBox()
        Label15 = New Label()
        cmbUseInGamePhoto = New ComboBox()
        txtDefaultNullImage = New TextBox()
        Label14 = New Label()
        txtIconSpriteLarge = New TextBox()
        Label13 = New Label()
        cmbUseWindowFocusMode = New ComboBox()
        Label12 = New Label()
        cmbForceWorldInteraction = New ComboBox()
        Label11 = New Label()
        Label10 = New Label()
        cmbMarkDiscovered = New ComboBox()
        Label9 = New Label()
        cmbAllowCustomNames = New ComboBox()
        Label8 = New Label()
        cmbDisableHistory = New ComboBox()
        Label7 = New Label()
        cmbIsSingleton = New ComboBox()
        Label6 = New Label()
        cmbBelongsToInName = New ComboBox()
        TabPage3 = New TabPage()
        txtAddFactLinks = New TextBox()
        btnRemoveFactLink = New Button()
        btnAddFactLink = New Button()
        Label29 = New Label()
        lbAddFactLinks = New ListBox()
        txtFactSetup = New TextBox()
        btnRemoveFactSetup = New Button()
        btnAddFactSetup = New Button()
        Label27 = New Label()
        lbFactSetup = New ListBox()
        cmbItemReceiver = New ComboBox()
        Label26 = New Label()
        cmbItemWriter = New ComboBox()
        Label25 = New Label()
        cmbItemOwner = New ComboBox()
        Label24 = New Label()
        cmbUseSurveillanceCapture = New ComboBox()
        Label23 = New Label()
        cmbUseCaptureLight = New ComboBox()
        Label22 = New Label()
        nudCaptureTimeOfDay = New NumericUpDown()
        Label21 = New Label()
        cmbChangeTimeOfDay = New ComboBox()
        Label20 = New Label()
        TabPage4 = New TabPage()
        txtDDSDocumentID = New TextBox()
        Label33 = New Label()
        txtApplicationOnDiscover = New TextBox()
        btnRemoveApplicationOnDiscover = New Button()
        btnAddApplicationOnDiscover = New Button()
        Label32 = New Label()
        lbApplicationOnDiscover = New ListBox()
        txtDiscoveryTriggers = New TextBox()
        btnRemoveDiscoveryTrigger = New Button()
        btnAddDiscoveryTrigger = New Button()
        Label31 = New Label()
        lbDiscoveryTriggers = New ListBox()
        txtKeyMergeOnDiscovery = New TextBox()
        Button1 = New Button()
        Button2 = New Button()
        Label30 = New Label()
        lbKeyMergeOnDiscovery = New ListBox()
        cmbDiscoverOnCreate = New ComboBox()
        Label28 = New Label()
        TabPage5 = New TabPage()
        Label43 = New Label()
        Label42 = New Label()
        Label41 = New Label()
        Label40 = New Label()
        nudPinnedBGColourA = New NumericUpDown()
        nudPinnedBGColourB = New NumericUpDown()
        nudPinnedBGColourG = New NumericUpDown()
        nudPinnedBGColourR = New NumericUpDown()
        Label39 = New Label()
        cmbPinnedStyle = New ComboBox()
        Label38 = New Label()
        cmbEnableFacts = New ComboBox()
        Label37 = New Label()
        cmbEnableSummary = New ComboBox()
        Label36 = New Label()
        txtMatchTypes = New TextBox()
        btnRemoveMatchTypes = New Button()
        btnAddMatchTypes = New Button()
        Label35 = New Label()
        lbMatchTypes = New ListBox()
        cmbIsMatchParent = New ComboBox()
        Label34 = New Label()
        btnGenerate = New Button()
        MenuMain = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        NewFileToolStripMenuItem = New ToolStripMenuItem()
        OpenPreseToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem2 = New ToolStripMenuItem()
        ToolStripMenuItem3 = New ToolStripMenuItem()
        DeletePresetToolStripMenuItem = New ToolStripMenuItem()
        pnCover = New Panel()
        tcMain.SuspendLayout()
        TabPage1.SuspendLayout()
        TabPage2.SuspendLayout()
        CType(nudRelCamPhotoEulerY, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudRelCamPhotoEulerX, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudRelCamPhotoEulerZ, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudRelCamPhotoPosY, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudRelCamPhotoPosX, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudRelCamPhotoPosZ, ComponentModel.ISupportInitialize).BeginInit()
        TabPage3.SuspendLayout()
        CType(nudCaptureTimeOfDay, ComponentModel.ISupportInitialize).BeginInit()
        TabPage4.SuspendLayout()
        TabPage5.SuspendLayout()
        CType(nudPinnedBGColourA, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudPinnedBGColourB, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudPinnedBGColourG, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudPinnedBGColourR, ComponentModel.ISupportInitialize).BeginInit()
        MenuMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' lblCaseName
        ' 
        lblCaseName.AutoSize = True
        lblCaseName.Location = New Point(9, 12)
        lblCaseName.Name = "lblCaseName"
        lblCaseName.Size = New Size(93, 20)
        lblCaseName.TabIndex = 28
        lblCaseName.Text = "Preset Name"
        ' 
        ' txtPresetName
        ' 
        txtPresetName.Location = New Point(168, 8)
        txtPresetName.Margin = New Padding(3, 4, 3, 4)
        txtPresetName.Name = "txtPresetName"
        txtPresetName.Size = New Size(206, 27)
        txtPresetName.TabIndex = 27
        ' 
        ' cmbCopyFromMain
        ' 
        cmbCopyFromMain.FormattingEnabled = True
        cmbCopyFromMain.Items.AddRange(New Object() {"null", "AdCandorLeft", "AdCandorRight", "AdDiner", "AdDove", "AddressBook", "AdElGen", "AdEnforcers", "AdGemsteader", "AdHospital", "AdKensington", "AdLEM", "AdStarch", "AdTheFields", "AdTheFields2", "AdTheFields3", "AdTheFields4", "AppleTailingReport", "BankStatement", "BarNewManagement", "BillFinalNotice", "Bin", "BirthCertificate", "BirthdayCard", "bloodPool", "BookCubPoster", "BotanyClubPoster", "building", "BulletCasing", "bulletHole", "BusinessCard", "BusinessCardKiller", "Calendar", "CallLogs", "CandorSubscription", "CheatersLetter", "CheatersPoem", "ChessClubPoster", "citizen", "CityDirectory", "CompanyRoster", "CreditCard", "CrumpledDrawing", "CrumpledHalfWrittenLetter", "CrumpledNameCipher", "CrumpledPaper", "CrumpledPaperPoetry", "CrumpledPaperStickupEvidence", "CrumpledWriting", "DailyAffirmations", "date", "DebtCollectionLetter", "Diary", "DinnerLetter", "DoctorsPrescription", "DodgyNoteRat", "DonorCard", "DoveKillerBurnedNote", "DoveKillerFanLetter", "DramaGroupPoster", "ElevatorControls", "ElGenPartyInvite", "EmployeePhoto", "EmployeeRecord", "EmploymentContract", "EnforcerNote", "EntryWound", "ExitWound", "FieldsLetter", "FieldsLetter1", "FieldsLetter2", "FieldsLetter4", "fingerprint", "FlowersNote", "FlowersNoteAffair", "footprint", "HomeFile", "HospitalBed", "HotelNoticeBar", "HotelNoticeShoes", "JobNote", "Keypad", "KidnapperMoneyDebtFlyer", "KidnapperMoneyDenDetails", "KidnapperMoneyLetterIndigo", "KidnapperMoneyMarkList", "KidnapperMoneyShoppingList", "KidnapperMoneyVictimList", "KidnapperScienceDenDetails", "KidnapperScienceDiary", "KidnapperScienceNotes", "KidnapperScienceReport", "KillerTauntNoteCorp", "KillerTauntNoteGeneral", "KillerTauntNoteRedGum", "KillerTauntNoteRetire", "KillerTauntNoteSniper", "KillerTauntNoteVLove", "KillerTauntNoteVoyeur", "KindapperLureNote", "KolaSweepstake", "LEMPersonalLetter", "LEMPersonalLetter1", "LEMPersonalLetter2", "Letter", "location", "LostNote", "Menu", "Murder_HalfWrittenLetter", "Murder_Journal", "Murder_NoteFromSupervisor", "Murder_NoteOnBoard", "Murder_NoteToSelf", "Murder_SightingLog", "MurderWeaponSingleton", "NamePlacard", "Note", "OpticiansPrescription", "PaperStack", "PersonalLetterRedgums", "Photograph", "PlayerStickyNote", "PokerCubPoster", "PoliceSupportGroup", "PrintedCitizenFile", "PrintedEmployeeRecord", "PrintedResidentFile", "PrintedSurveillance", "PrintedVmail", "ProbationNotice", "PurchasedTime", "RansomNote1", "RansomNote2", "RansomNote3", "RansomNote4", "Receipt", "RedGumKillerDiary", "RedGumMeetingNote", "RedgumsLeaflet1", "RedgumsLeaflet2", "RepossessionNotice", "ResidentFile", "ResidentRoster", "ResidentsContract", "RetailItemNoPurchaseDiscovery", "RetailItemPurchaseDiscovery", "RetailItemSingleton", "RetiredKillerFieldsApp", "RetiredKillerNote", "RetiredKillerNote2", "SaleNote", "SalesRecords", "ScienceLabNoteForumula", "ScienceLabNoteList", "ScienceLabNoteProgress", "ScienceLabNoteSorry", "ScienceLabNoteSubjects", "SecuritySystem", "SensitiveDocument1", "ShoppingList", "SniperKillerCertificate", "SniperKillerCompetition", "SniperKillerDiary", "street", "SuicideNote", "Telephone", "TelephoneCall", "TelephoneNumber", "time", "TimeOfDeath", "TradingCard", "TravelReceipt", "VoyuerRandomNote", "VoyuerScopeSaleNote", "VoyuerTrackingNotes", "Wallet", "WorkID", "WorkRota"})
        cmbCopyFromMain.Location = New Point(168, 43)
        cmbCopyFromMain.Margin = New Padding(3, 4, 3, 4)
        cmbCopyFromMain.Name = "cmbCopyFromMain"
        cmbCopyFromMain.Size = New Size(206, 28)
        cmbCopyFromMain.TabIndex = 29
        cmbCopyFromMain.Text = "null"
        ' 
        ' lblCopyFromMain
        ' 
        lblCopyFromMain.AutoSize = True
        lblCopyFromMain.Location = New Point(9, 47)
        lblCopyFromMain.Name = "lblCopyFromMain"
        lblCopyFromMain.Size = New Size(81, 20)
        lblCopyFromMain.TabIndex = 30
        lblCopyFromMain.Text = "Copy From"
        ' 
        ' lblOutput
        ' 
        lblOutput.AutoSize = True
        lblOutput.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblOutput.Location = New Point(676, 25)
        lblOutput.Name = "lblOutput"
        lblOutput.Size = New Size(108, 37)
        lblOutput.TabIndex = 31
        lblOutput.Text = "Output"
        ' 
        ' txtOutput
        ' 
        txtOutput.BackColor = SystemColors.HighlightText
        txtOutput.Location = New Point(486, 66)
        txtOutput.Margin = New Padding(3, 4, 3, 4)
        txtOutput.Name = "txtOutput"
        txtOutput.Size = New Size(506, 571)
        txtOutput.TabIndex = 32
        txtOutput.Text = ""
        ' 
        ' tcMain
        ' 
        tcMain.Controls.Add(TabPage1)
        tcMain.Controls.Add(TabPage2)
        tcMain.Controls.Add(TabPage3)
        tcMain.Controls.Add(TabPage4)
        tcMain.Controls.Add(TabPage5)
        tcMain.Location = New Point(13, 37)
        tcMain.Name = "tcMain"
        tcMain.SelectedIndex = 0
        tcMain.Size = New Size(452, 562)
        tcMain.TabIndex = 33
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(Label5)
        TabPage1.Controls.Add(cmbNotifyOfTies)
        TabPage1.Controls.Add(txtPassiveTies)
        TabPage1.Controls.Add(btnRemovePassiveTies)
        TabPage1.Controls.Add(btnAddPassiveTies)
        TabPage1.Controls.Add(Label4)
        TabPage1.Controls.Add(lbPassiveTies)
        TabPage1.Controls.Add(txtValidKeys)
        TabPage1.Controls.Add(btnRemoveValidKeys)
        TabPage1.Controls.Add(btnAddValidKeys)
        TabPage1.Controls.Add(Label3)
        TabPage1.Controls.Add(lbValidKeys)
        TabPage1.Controls.Add(lblDisabled)
        TabPage1.Controls.Add(cmbUseDataKeys)
        TabPage1.Controls.Add(cmbWindowStyle)
        TabPage1.Controls.Add(Label2)
        TabPage1.Controls.Add(txtSubClass)
        TabPage1.Controls.Add(Label1)
        TabPage1.Controls.Add(cmbCopyFromMain)
        TabPage1.Controls.Add(lblCopyFromMain)
        TabPage1.Controls.Add(txtPresetName)
        TabPage1.Controls.Add(lblCaseName)
        TabPage1.Location = New Point(4, 29)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(444, 529)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Page 1"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(9, 478)
        Label5.Name = "Label5"
        Label5.Size = New Size(100, 20)
        Label5.TabIndex = 48
        Label5.Text = "Notify Of Ties"
        ' 
        ' cmbNotifyOfTies
        ' 
        cmbNotifyOfTies.DropDownStyle = ComboBoxStyle.DropDownList
        cmbNotifyOfTies.FormattingEnabled = True
        cmbNotifyOfTies.Items.AddRange(New Object() {"false", "true"})
        cmbNotifyOfTies.Location = New Point(168, 474)
        cmbNotifyOfTies.Margin = New Padding(3, 4, 3, 4)
        cmbNotifyOfTies.Name = "cmbNotifyOfTies"
        cmbNotifyOfTies.Size = New Size(206, 28)
        cmbNotifyOfTies.TabIndex = 47
        ' 
        ' txtPassiveTies
        ' 
        txtPassiveTies.Location = New Point(168, 329)
        txtPassiveTies.Margin = New Padding(3, 4, 3, 4)
        txtPassiveTies.Name = "txtPassiveTies"
        txtPassiveTies.Size = New Size(206, 27)
        txtPassiveTies.TabIndex = 46
        ' 
        ' btnRemovePassiveTies
        ' 
        btnRemovePassiveTies.Location = New Point(280, 419)
        btnRemovePassiveTies.Name = "btnRemovePassiveTies"
        btnRemovePassiveTies.Size = New Size(94, 50)
        btnRemovePassiveTies.TabIndex = 45
        btnRemovePassiveTies.Text = "Remove"
        btnRemovePassiveTies.UseVisualStyleBackColor = True
        ' 
        ' btnAddPassiveTies
        ' 
        btnAddPassiveTies.Location = New Point(280, 363)
        btnAddPassiveTies.Name = "btnAddPassiveTies"
        btnAddPassiveTies.Size = New Size(94, 50)
        btnAddPassiveTies.TabIndex = 44
        btnAddPassiveTies.Text = "Add"
        btnAddPassiveTies.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(9, 332)
        Label4.Name = "Label4"
        Label4.Size = New Size(85, 20)
        Label4.TabIndex = 43
        Label4.Text = "Passive Ties"
        ' 
        ' lbPassiveTies
        ' 
        lbPassiveTies.FormattingEnabled = True
        lbPassiveTies.ItemHeight = 20
        lbPassiveTies.Location = New Point(9, 363)
        lbPassiveTies.Name = "lbPassiveTies"
        lbPassiveTies.Size = New Size(265, 104)
        lbPassiveTies.TabIndex = 42
        ' 
        ' txtValidKeys
        ' 
        txtValidKeys.Location = New Point(168, 186)
        txtValidKeys.Margin = New Padding(3, 4, 3, 4)
        txtValidKeys.Name = "txtValidKeys"
        txtValidKeys.Size = New Size(206, 27)
        txtValidKeys.TabIndex = 41
        ' 
        ' btnRemoveValidKeys
        ' 
        btnRemoveValidKeys.Location = New Point(280, 276)
        btnRemoveValidKeys.Name = "btnRemoveValidKeys"
        btnRemoveValidKeys.Size = New Size(94, 50)
        btnRemoveValidKeys.TabIndex = 40
        btnRemoveValidKeys.Text = "Remove"
        btnRemoveValidKeys.UseVisualStyleBackColor = True
        ' 
        ' btnAddValidKeys
        ' 
        btnAddValidKeys.Location = New Point(280, 220)
        btnAddValidKeys.Name = "btnAddValidKeys"
        btnAddValidKeys.Size = New Size(94, 50)
        btnAddValidKeys.TabIndex = 39
        btnAddValidKeys.Text = "Add"
        btnAddValidKeys.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(9, 189)
        Label3.Name = "Label3"
        Label3.Size = New Size(76, 20)
        Label3.TabIndex = 38
        Label3.Text = "Valid Keys"
        ' 
        ' lbValidKeys
        ' 
        lbValidKeys.FormattingEnabled = True
        lbValidKeys.ItemHeight = 20
        lbValidKeys.Location = New Point(9, 220)
        lbValidKeys.Name = "lbValidKeys"
        lbValidKeys.Size = New Size(265, 104)
        lbValidKeys.TabIndex = 37
        ' 
        ' lblDisabled
        ' 
        lblDisabled.AutoSize = True
        lblDisabled.Location = New Point(9, 154)
        lblDisabled.Name = "lblDisabled"
        lblDisabled.Size = New Size(103, 20)
        lblDisabled.TabIndex = 36
        lblDisabled.Text = "Use Data Keys"
        ' 
        ' cmbUseDataKeys
        ' 
        cmbUseDataKeys.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUseDataKeys.FormattingEnabled = True
        cmbUseDataKeys.Items.AddRange(New Object() {"false", "true"})
        cmbUseDataKeys.Location = New Point(168, 150)
        cmbUseDataKeys.Margin = New Padding(3, 4, 3, 4)
        cmbUseDataKeys.Name = "cmbUseDataKeys"
        cmbUseDataKeys.Size = New Size(206, 28)
        cmbUseDataKeys.TabIndex = 35
        ' 
        ' cmbWindowStyle
        ' 
        cmbWindowStyle.FormattingEnabled = True
        cmbWindowStyle.Items.AddRange(New Object() {"null", "ApartmentDecor", "ApartmentPurchase", "Buy", "BuySell", "CallLogs", "CaseResolve", "CaseResults", "CityDirectory", "ColourPicker", "DetectivesNotebook", "Evidence", "EvidenceCardLandscape", "EvidenceItem", "JobNote", "Lock", "MaterialKey", "SalesRecords", "SelectItem", "SelectPhoto", "Telephone"})
        cmbWindowStyle.Location = New Point(168, 114)
        cmbWindowStyle.Margin = New Padding(3, 4, 3, 4)
        cmbWindowStyle.Name = "cmbWindowStyle"
        cmbWindowStyle.Size = New Size(206, 28)
        cmbWindowStyle.TabIndex = 33
        cmbWindowStyle.Text = "null"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(9, 118)
        Label2.Name = "Label2"
        Label2.Size = New Size(100, 20)
        Label2.TabIndex = 34
        Label2.Text = "Window Style"
        ' 
        ' txtSubClass
        ' 
        txtSubClass.Location = New Point(168, 79)
        txtSubClass.Margin = New Padding(3, 4, 3, 4)
        txtSubClass.Name = "txtSubClass"
        txtSubClass.Size = New Size(206, 27)
        txtSubClass.TabIndex = 31
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(9, 83)
        Label1.Name = "Label1"
        Label1.Size = New Size(67, 20)
        Label1.TabIndex = 32
        Label1.Text = "SubClass"
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(cmbCaptureRules)
        TabPage2.Controls.Add(Label19)
        TabPage2.Controls.Add(nudRelCamPhotoEulerY)
        TabPage2.Controls.Add(nudRelCamPhotoEulerX)
        TabPage2.Controls.Add(nudRelCamPhotoEulerZ)
        TabPage2.Controls.Add(Label18)
        TabPage2.Controls.Add(nudRelCamPhotoPosY)
        TabPage2.Controls.Add(nudRelCamPhotoPosX)
        TabPage2.Controls.Add(nudRelCamPhotoPosZ)
        TabPage2.Controls.Add(Label17)
        TabPage2.Controls.Add(Label16)
        TabPage2.Controls.Add(cmbUseWriter)
        TabPage2.Controls.Add(Label15)
        TabPage2.Controls.Add(cmbUseInGamePhoto)
        TabPage2.Controls.Add(txtDefaultNullImage)
        TabPage2.Controls.Add(Label14)
        TabPage2.Controls.Add(txtIconSpriteLarge)
        TabPage2.Controls.Add(Label13)
        TabPage2.Controls.Add(cmbUseWindowFocusMode)
        TabPage2.Controls.Add(Label12)
        TabPage2.Controls.Add(cmbForceWorldInteraction)
        TabPage2.Controls.Add(Label11)
        TabPage2.Controls.Add(Label10)
        TabPage2.Controls.Add(cmbMarkDiscovered)
        TabPage2.Controls.Add(Label9)
        TabPage2.Controls.Add(cmbAllowCustomNames)
        TabPage2.Controls.Add(Label8)
        TabPage2.Controls.Add(cmbDisableHistory)
        TabPage2.Controls.Add(Label7)
        TabPage2.Controls.Add(cmbIsSingleton)
        TabPage2.Controls.Add(Label6)
        TabPage2.Controls.Add(cmbBelongsToInName)
        TabPage2.Location = New Point(4, 29)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(444, 529)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Page 2"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' cmbCaptureRules
        ' 
        cmbCaptureRules.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCaptureRules.FormattingEnabled = True
        cmbCaptureRules.Items.AddRange(New Object() {"building", "location", "item", "citizen"})
        cmbCaptureRules.Location = New Point(164, 467)
        cmbCaptureRules.Margin = New Padding(3, 4, 3, 4)
        cmbCaptureRules.Name = "cmbCaptureRules"
        cmbCaptureRules.Size = New Size(206, 28)
        cmbCaptureRules.TabIndex = 85
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(5, 471)
        Label19.Name = "Label19"
        Label19.Size = New Size(100, 20)
        Label19.TabIndex = 84
        Label19.Text = "Capture Rules"
        ' 
        ' nudRelCamPhotoEulerY
        ' 
        nudRelCamPhotoEulerY.Location = New Point(255, 433)
        nudRelCamPhotoEulerY.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudRelCamPhotoEulerY.Name = "nudRelCamPhotoEulerY"
        nudRelCamPhotoEulerY.Size = New Size(54, 27)
        nudRelCamPhotoEulerY.TabIndex = 82
        ' 
        ' nudRelCamPhotoEulerX
        ' 
        nudRelCamPhotoEulerX.Location = New Point(184, 433)
        nudRelCamPhotoEulerX.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudRelCamPhotoEulerX.Name = "nudRelCamPhotoEulerX"
        nudRelCamPhotoEulerX.Size = New Size(54, 27)
        nudRelCamPhotoEulerX.TabIndex = 81
        ' 
        ' nudRelCamPhotoEulerZ
        ' 
        nudRelCamPhotoEulerZ.Location = New Point(324, 433)
        nudRelCamPhotoEulerZ.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudRelCamPhotoEulerZ.Name = "nudRelCamPhotoEulerZ"
        nudRelCamPhotoEulerZ.Size = New Size(54, 27)
        nudRelCamPhotoEulerZ.TabIndex = 80
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(5, 438)
        Label18.Name = "Label18"
        Label18.Size = New Size(176, 20)
        Label18.TabIndex = 79
        Label18.Text = "Relative Cam Photo Euler"
        ' 
        ' nudRelCamPhotoPosY
        ' 
        nudRelCamPhotoPosY.Location = New Point(255, 400)
        nudRelCamPhotoPosY.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudRelCamPhotoPosY.Name = "nudRelCamPhotoPosY"
        nudRelCamPhotoPosY.Size = New Size(54, 27)
        nudRelCamPhotoPosY.TabIndex = 78
        ' 
        ' nudRelCamPhotoPosX
        ' 
        nudRelCamPhotoPosX.Location = New Point(184, 400)
        nudRelCamPhotoPosX.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudRelCamPhotoPosX.Name = "nudRelCamPhotoPosX"
        nudRelCamPhotoPosX.Size = New Size(54, 27)
        nudRelCamPhotoPosX.TabIndex = 77
        ' 
        ' nudRelCamPhotoPosZ
        ' 
        nudRelCamPhotoPosZ.Location = New Point(324, 400)
        nudRelCamPhotoPosZ.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudRelCamPhotoPosZ.Name = "nudRelCamPhotoPosZ"
        nudRelCamPhotoPosZ.Size = New Size(54, 27)
        nudRelCamPhotoPosZ.TabIndex = 76
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(5, 405)
        Label17.Name = "Label17"
        Label17.Size = New Size(165, 20)
        Label17.TabIndex = 72
        Label17.Text = "Relative Cam Photo Pos"
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(5, 369)
        Label16.Name = "Label16"
        Label16.Size = New Size(78, 20)
        Label16.TabIndex = 70
        Label16.Text = "Use Writer"
        ' 
        ' cmbUseWriter
        ' 
        cmbUseWriter.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUseWriter.FormattingEnabled = True
        cmbUseWriter.Items.AddRange(New Object() {"false", "true"})
        cmbUseWriter.Location = New Point(164, 365)
        cmbUseWriter.Margin = New Padding(3, 4, 3, 4)
        cmbUseWriter.Name = "cmbUseWriter"
        cmbUseWriter.Size = New Size(206, 28)
        cmbUseWriter.TabIndex = 69
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Location = New Point(5, 333)
        Label15.Name = "Label15"
        Label15.Size = New Size(135, 20)
        Label15.TabIndex = 68
        Label15.Text = "Use In Game Photo"
        ' 
        ' cmbUseInGamePhoto
        ' 
        cmbUseInGamePhoto.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUseInGamePhoto.FormattingEnabled = True
        cmbUseInGamePhoto.Items.AddRange(New Object() {"false", "true"})
        cmbUseInGamePhoto.Location = New Point(164, 329)
        cmbUseInGamePhoto.Margin = New Padding(3, 4, 3, 4)
        cmbUseInGamePhoto.Name = "cmbUseInGamePhoto"
        cmbUseInGamePhoto.Size = New Size(206, 28)
        cmbUseInGamePhoto.TabIndex = 67
        ' 
        ' txtDefaultNullImage
        ' 
        txtDefaultNullImage.Location = New Point(164, 294)
        txtDefaultNullImage.Margin = New Padding(3, 4, 3, 4)
        txtDefaultNullImage.Name = "txtDefaultNullImage"
        txtDefaultNullImage.Size = New Size(206, 27)
        txtDefaultNullImage.TabIndex = 65
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(5, 298)
        Label14.Name = "Label14"
        Label14.Size = New Size(135, 20)
        Label14.TabIndex = 66
        Label14.Text = "Default Null Image"
        ' 
        ' txtIconSpriteLarge
        ' 
        txtIconSpriteLarge.Location = New Point(164, 259)
        txtIconSpriteLarge.Margin = New Padding(3, 4, 3, 4)
        txtIconSpriteLarge.Name = "txtIconSpriteLarge"
        txtIconSpriteLarge.Size = New Size(206, 27)
        txtIconSpriteLarge.TabIndex = 63
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Location = New Point(5, 263)
        Label13.Name = "Label13"
        Label13.Size = New Size(121, 20)
        Label13.TabIndex = 64
        Label13.Text = "Icon Sprite Large"
        ' 
        ' cmbUseWindowFocusMode
        ' 
        cmbUseWindowFocusMode.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUseWindowFocusMode.FormattingEnabled = True
        cmbUseWindowFocusMode.Items.AddRange(New Object() {"false", "true"})
        cmbUseWindowFocusMode.Location = New Point(187, 223)
        cmbUseWindowFocusMode.Margin = New Padding(3, 4, 3, 4)
        cmbUseWindowFocusMode.Name = "cmbUseWindowFocusMode"
        cmbUseWindowFocusMode.Size = New Size(183, 28)
        cmbUseWindowFocusMode.TabIndex = 61
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(5, 227)
        Label12.Name = "Label12"
        Label12.Size = New Size(176, 20)
        Label12.TabIndex = 62
        Label12.Text = "Use Window Focus Mode"
        ' 
        ' cmbForceWorldInteraction
        ' 
        cmbForceWorldInteraction.DropDownStyle = ComboBoxStyle.DropDownList
        cmbForceWorldInteraction.FormattingEnabled = True
        cmbForceWorldInteraction.Items.AddRange(New Object() {"false", "true"})
        cmbForceWorldInteraction.Location = New Point(175, 187)
        cmbForceWorldInteraction.Margin = New Padding(3, 4, 3, 4)
        cmbForceWorldInteraction.Name = "cmbForceWorldInteraction"
        cmbForceWorldInteraction.Size = New Size(195, 28)
        cmbForceWorldInteraction.TabIndex = 59
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(5, 191)
        Label11.Name = "Label11"
        Label11.Size = New Size(164, 20)
        Label11.TabIndex = 60
        Label11.Text = "Force World Interaction"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(5, 155)
        Label10.Name = "Label10"
        Label10.Size = New Size(267, 20)
        Label10.TabIndex = 58
        Label10.Text = "Mark As Discovered On Any Interaction"
        ' 
        ' cmbMarkDiscovered
        ' 
        cmbMarkDiscovered.DropDownStyle = ComboBoxStyle.DropDownList
        cmbMarkDiscovered.FormattingEnabled = True
        cmbMarkDiscovered.Items.AddRange(New Object() {"false", "true"})
        cmbMarkDiscovered.Location = New Point(278, 151)
        cmbMarkDiscovered.Margin = New Padding(3, 4, 3, 4)
        cmbMarkDiscovered.Name = "cmbMarkDiscovered"
        cmbMarkDiscovered.Size = New Size(92, 28)
        cmbMarkDiscovered.TabIndex = 57
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(5, 119)
        Label9.Name = "Label9"
        Label9.Size = New Size(151, 20)
        Label9.TabIndex = 56
        Label9.Text = "Allow Custom Names"
        ' 
        ' cmbAllowCustomNames
        ' 
        cmbAllowCustomNames.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAllowCustomNames.FormattingEnabled = True
        cmbAllowCustomNames.Items.AddRange(New Object() {"false", "true"})
        cmbAllowCustomNames.Location = New Point(164, 115)
        cmbAllowCustomNames.Margin = New Padding(3, 4, 3, 4)
        cmbAllowCustomNames.Name = "cmbAllowCustomNames"
        cmbAllowCustomNames.Size = New Size(206, 28)
        cmbAllowCustomNames.TabIndex = 55
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(5, 83)
        Label8.Name = "Label8"
        Label8.Size = New Size(110, 20)
        Label8.TabIndex = 54
        Label8.Text = "Disable History"
        ' 
        ' cmbDisableHistory
        ' 
        cmbDisableHistory.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDisableHistory.FormattingEnabled = True
        cmbDisableHistory.Items.AddRange(New Object() {"false", "true"})
        cmbDisableHistory.Location = New Point(164, 79)
        cmbDisableHistory.Margin = New Padding(3, 4, 3, 4)
        cmbDisableHistory.Name = "cmbDisableHistory"
        cmbDisableHistory.Size = New Size(206, 28)
        cmbDisableHistory.TabIndex = 53
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(5, 47)
        Label7.Name = "Label7"
        Label7.Size = New Size(86, 20)
        Label7.TabIndex = 52
        Label7.Text = "Is Singleton"
        ' 
        ' cmbIsSingleton
        ' 
        cmbIsSingleton.DropDownStyle = ComboBoxStyle.DropDownList
        cmbIsSingleton.FormattingEnabled = True
        cmbIsSingleton.Items.AddRange(New Object() {"false", "true"})
        cmbIsSingleton.Location = New Point(164, 43)
        cmbIsSingleton.Margin = New Padding(3, 4, 3, 4)
        cmbIsSingleton.Name = "cmbIsSingleton"
        cmbIsSingleton.Size = New Size(206, 28)
        cmbIsSingleton.TabIndex = 51
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(5, 11)
        Label6.Name = "Label6"
        Label6.Size = New Size(142, 20)
        Label6.TabIndex = 50
        Label6.Text = "Belongs To In Name"
        ' 
        ' cmbBelongsToInName
        ' 
        cmbBelongsToInName.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBelongsToInName.FormattingEnabled = True
        cmbBelongsToInName.Items.AddRange(New Object() {"false", "true"})
        cmbBelongsToInName.Location = New Point(164, 7)
        cmbBelongsToInName.Margin = New Padding(3, 4, 3, 4)
        cmbBelongsToInName.Name = "cmbBelongsToInName"
        cmbBelongsToInName.Size = New Size(206, 28)
        cmbBelongsToInName.TabIndex = 49
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(txtAddFactLinks)
        TabPage3.Controls.Add(btnRemoveFactLink)
        TabPage3.Controls.Add(btnAddFactLink)
        TabPage3.Controls.Add(Label29)
        TabPage3.Controls.Add(lbAddFactLinks)
        TabPage3.Controls.Add(txtFactSetup)
        TabPage3.Controls.Add(btnRemoveFactSetup)
        TabPage3.Controls.Add(btnAddFactSetup)
        TabPage3.Controls.Add(Label27)
        TabPage3.Controls.Add(lbFactSetup)
        TabPage3.Controls.Add(cmbItemReceiver)
        TabPage3.Controls.Add(Label26)
        TabPage3.Controls.Add(cmbItemWriter)
        TabPage3.Controls.Add(Label25)
        TabPage3.Controls.Add(cmbItemOwner)
        TabPage3.Controls.Add(Label24)
        TabPage3.Controls.Add(cmbUseSurveillanceCapture)
        TabPage3.Controls.Add(Label23)
        TabPage3.Controls.Add(cmbUseCaptureLight)
        TabPage3.Controls.Add(Label22)
        TabPage3.Controls.Add(nudCaptureTimeOfDay)
        TabPage3.Controls.Add(Label21)
        TabPage3.Controls.Add(cmbChangeTimeOfDay)
        TabPage3.Controls.Add(Label20)
        TabPage3.Location = New Point(4, 29)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(3)
        TabPage3.Size = New Size(444, 529)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Page 3"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' txtAddFactLinks
        ' 
        txtAddFactLinks.Location = New Point(164, 404)
        txtAddFactLinks.Margin = New Padding(3, 4, 3, 4)
        txtAddFactLinks.Name = "txtAddFactLinks"
        txtAddFactLinks.Size = New Size(206, 27)
        txtAddFactLinks.TabIndex = 115
        ' 
        ' btnRemoveFactLink
        ' 
        btnRemoveFactLink.Location = New Point(277, 488)
        btnRemoveFactLink.Name = "btnRemoveFactLink"
        btnRemoveFactLink.Size = New Size(94, 35)
        btnRemoveFactLink.TabIndex = 114
        btnRemoveFactLink.Text = "Remove"
        btnRemoveFactLink.UseVisualStyleBackColor = True
        ' 
        ' btnAddFactLink
        ' 
        btnAddFactLink.Location = New Point(277, 438)
        btnAddFactLink.Name = "btnAddFactLink"
        btnAddFactLink.Size = New Size(94, 35)
        btnAddFactLink.TabIndex = 113
        btnAddFactLink.Text = "Add"
        btnAddFactLink.UseVisualStyleBackColor = True
        ' 
        ' Label29
        ' 
        Label29.AutoSize = True
        Label29.Location = New Point(6, 407)
        Label29.Name = "Label29"
        Label29.Size = New Size(103, 20)
        Label29.TabIndex = 112
        Label29.Text = "Add Fact Links"
        ' 
        ' lbAddFactLinks
        ' 
        lbAddFactLinks.FormattingEnabled = True
        lbAddFactLinks.ItemHeight = 20
        lbAddFactLinks.Location = New Point(6, 438)
        lbAddFactLinks.Name = "lbAddFactLinks"
        lbAddFactLinks.Size = New Size(265, 84)
        lbAddFactLinks.TabIndex = 111
        ' 
        ' txtFactSetup
        ' 
        txtFactSetup.Location = New Point(164, 259)
        txtFactSetup.Margin = New Padding(3, 4, 3, 4)
        txtFactSetup.Name = "txtFactSetup"
        txtFactSetup.Size = New Size(206, 27)
        txtFactSetup.TabIndex = 105
        ' 
        ' btnRemoveFactSetup
        ' 
        btnRemoveFactSetup.Location = New Point(276, 349)
        btnRemoveFactSetup.Name = "btnRemoveFactSetup"
        btnRemoveFactSetup.Size = New Size(94, 50)
        btnRemoveFactSetup.TabIndex = 104
        btnRemoveFactSetup.Text = "Remove"
        btnRemoveFactSetup.UseVisualStyleBackColor = True
        ' 
        ' btnAddFactSetup
        ' 
        btnAddFactSetup.Location = New Point(276, 293)
        btnAddFactSetup.Name = "btnAddFactSetup"
        btnAddFactSetup.Size = New Size(94, 50)
        btnAddFactSetup.TabIndex = 103
        btnAddFactSetup.Text = "Add"
        btnAddFactSetup.UseVisualStyleBackColor = True
        ' 
        ' Label27
        ' 
        Label27.AutoSize = True
        Label27.Location = New Point(5, 262)
        Label27.Name = "Label27"
        Label27.Size = New Size(77, 20)
        Label27.TabIndex = 102
        Label27.Text = "Fact Setup"
        ' 
        ' lbFactSetup
        ' 
        lbFactSetup.FormattingEnabled = True
        lbFactSetup.ItemHeight = 20
        lbFactSetup.Location = New Point(5, 293)
        lbFactSetup.Name = "lbFactSetup"
        lbFactSetup.Size = New Size(265, 104)
        lbFactSetup.TabIndex = 101
        ' 
        ' cmbItemReceiver
        ' 
        cmbItemReceiver.DropDownStyle = ComboBoxStyle.DropDownList
        cmbItemReceiver.FormattingEnabled = True
        cmbItemReceiver.Items.AddRange(New Object() {"self", "partner", "paramour", "boss", "doctor", "landlord"})
        cmbItemReceiver.Location = New Point(183, 223)
        cmbItemReceiver.Margin = New Padding(3, 4, 3, 4)
        cmbItemReceiver.Name = "cmbItemReceiver"
        cmbItemReceiver.Size = New Size(187, 28)
        cmbItemReceiver.TabIndex = 100
        ' 
        ' Label26
        ' 
        Label26.AutoSize = True
        Label26.Location = New Point(5, 227)
        Label26.Name = "Label26"
        Label26.Size = New Size(99, 20)
        Label26.TabIndex = 99
        Label26.Text = "Item Receiver"
        ' 
        ' cmbItemWriter
        ' 
        cmbItemWriter.DropDownStyle = ComboBoxStyle.DropDownList
        cmbItemWriter.FormattingEnabled = True
        cmbItemWriter.Items.AddRange(New Object() {"self", "partner", "paramour", "boss", "doctor", "landlord"})
        cmbItemWriter.Location = New Point(183, 187)
        cmbItemWriter.Margin = New Padding(3, 4, 3, 4)
        cmbItemWriter.Name = "cmbItemWriter"
        cmbItemWriter.Size = New Size(187, 28)
        cmbItemWriter.TabIndex = 98
        ' 
        ' Label25
        ' 
        Label25.AutoSize = True
        Label25.Location = New Point(5, 191)
        Label25.Name = "Label25"
        Label25.Size = New Size(84, 20)
        Label25.TabIndex = 97
        Label25.Text = "Item Writer"
        ' 
        ' cmbItemOwner
        ' 
        cmbItemOwner.DropDownStyle = ComboBoxStyle.DropDownList
        cmbItemOwner.FormattingEnabled = True
        cmbItemOwner.Items.AddRange(New Object() {"self", "partner", "paramour", "boss", "doctor", "landlord"})
        cmbItemOwner.Location = New Point(183, 151)
        cmbItemOwner.Margin = New Padding(3, 4, 3, 4)
        cmbItemOwner.Name = "cmbItemOwner"
        cmbItemOwner.Size = New Size(187, 28)
        cmbItemOwner.TabIndex = 96
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.Location = New Point(5, 155)
        Label24.Name = "Label24"
        Label24.Size = New Size(86, 20)
        Label24.TabIndex = 95
        Label24.Text = "Item Owner"
        ' 
        ' cmbUseSurveillanceCapture
        ' 
        cmbUseSurveillanceCapture.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUseSurveillanceCapture.FormattingEnabled = True
        cmbUseSurveillanceCapture.Items.AddRange(New Object() {"false", "true"})
        cmbUseSurveillanceCapture.Location = New Point(183, 115)
        cmbUseSurveillanceCapture.Margin = New Padding(3, 4, 3, 4)
        cmbUseSurveillanceCapture.Name = "cmbUseSurveillanceCapture"
        cmbUseSurveillanceCapture.Size = New Size(187, 28)
        cmbUseSurveillanceCapture.TabIndex = 94
        ' 
        ' Label23
        ' 
        Label23.AutoSize = True
        Label23.Location = New Point(5, 119)
        Label23.Name = "Label23"
        Label23.Size = New Size(172, 20)
        Label23.TabIndex = 93
        Label23.Text = "Use Surveillance Capture"
        ' 
        ' cmbUseCaptureLight
        ' 
        cmbUseCaptureLight.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUseCaptureLight.FormattingEnabled = True
        cmbUseCaptureLight.Items.AddRange(New Object() {"false", "true"})
        cmbUseCaptureLight.Location = New Point(164, 79)
        cmbUseCaptureLight.Margin = New Padding(3, 4, 3, 4)
        cmbUseCaptureLight.Name = "cmbUseCaptureLight"
        cmbUseCaptureLight.Size = New Size(206, 28)
        cmbUseCaptureLight.TabIndex = 92
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.Location = New Point(5, 83)
        Label22.Name = "Label22"
        Label22.Size = New Size(126, 20)
        Label22.TabIndex = 91
        Label22.Text = "Use Capture Light"
        ' 
        ' nudCaptureTimeOfDay
        ' 
        nudCaptureTimeOfDay.Location = New Point(164, 45)
        nudCaptureTimeOfDay.Minimum = New Decimal(New Integer() {100, 0, 0, Integer.MinValue})
        nudCaptureTimeOfDay.Name = "nudCaptureTimeOfDay"
        nudCaptureTimeOfDay.Size = New Size(206, 27)
        nudCaptureTimeOfDay.TabIndex = 90
        nudCaptureTimeOfDay.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Location = New Point(5, 47)
        Label21.Name = "Label21"
        Label21.Size = New Size(148, 20)
        Label21.TabIndex = 89
        Label21.Text = "Capture Time Of Day"
        ' 
        ' cmbChangeTimeOfDay
        ' 
        cmbChangeTimeOfDay.DropDownStyle = ComboBoxStyle.DropDownList
        cmbChangeTimeOfDay.FormattingEnabled = True
        cmbChangeTimeOfDay.Items.AddRange(New Object() {"false", "true"})
        cmbChangeTimeOfDay.Location = New Point(164, 7)
        cmbChangeTimeOfDay.Margin = New Padding(3, 4, 3, 4)
        cmbChangeTimeOfDay.Name = "cmbChangeTimeOfDay"
        cmbChangeTimeOfDay.Size = New Size(206, 28)
        cmbChangeTimeOfDay.TabIndex = 87
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(5, 11)
        Label20.Name = "Label20"
        Label20.Size = New Size(146, 20)
        Label20.TabIndex = 86
        Label20.Text = "Change Time Of Day"
        ' 
        ' TabPage4
        ' 
        TabPage4.Controls.Add(txtDDSDocumentID)
        TabPage4.Controls.Add(Label33)
        TabPage4.Controls.Add(txtApplicationOnDiscover)
        TabPage4.Controls.Add(btnRemoveApplicationOnDiscover)
        TabPage4.Controls.Add(btnAddApplicationOnDiscover)
        TabPage4.Controls.Add(Label32)
        TabPage4.Controls.Add(lbApplicationOnDiscover)
        TabPage4.Controls.Add(txtDiscoveryTriggers)
        TabPage4.Controls.Add(btnRemoveDiscoveryTrigger)
        TabPage4.Controls.Add(btnAddDiscoveryTrigger)
        TabPage4.Controls.Add(Label31)
        TabPage4.Controls.Add(lbDiscoveryTriggers)
        TabPage4.Controls.Add(txtKeyMergeOnDiscovery)
        TabPage4.Controls.Add(Button1)
        TabPage4.Controls.Add(Button2)
        TabPage4.Controls.Add(Label30)
        TabPage4.Controls.Add(lbKeyMergeOnDiscovery)
        TabPage4.Controls.Add(cmbDiscoverOnCreate)
        TabPage4.Controls.Add(Label28)
        TabPage4.Location = New Point(4, 29)
        TabPage4.Name = "TabPage4"
        TabPage4.Padding = New Padding(3)
        TabPage4.Size = New Size(444, 529)
        TabPage4.TabIndex = 3
        TabPage4.Text = "Page 4"
        TabPage4.UseVisualStyleBackColor = True
        ' 
        ' txtDDSDocumentID
        ' 
        txtDDSDocumentID.Location = New Point(143, 483)
        txtDDSDocumentID.Margin = New Padding(3, 4, 3, 4)
        txtDDSDocumentID.Name = "txtDDSDocumentID"
        txtDDSDocumentID.Size = New Size(228, 27)
        txtDDSDocumentID.TabIndex = 129
        ' 
        ' Label33
        ' 
        Label33.AutoSize = True
        Label33.Location = New Point(6, 486)
        Label33.Name = "Label33"
        Label33.Size = New Size(131, 20)
        Label33.TabIndex = 128
        Label33.Text = "DDS Document ID"
        ' 
        ' txtApplicationOnDiscover
        ' 
        txtApplicationOnDiscover.Location = New Point(183, 338)
        txtApplicationOnDiscover.Margin = New Padding(3, 4, 3, 4)
        txtApplicationOnDiscover.Name = "txtApplicationOnDiscover"
        txtApplicationOnDiscover.Size = New Size(188, 27)
        txtApplicationOnDiscover.TabIndex = 127
        ' 
        ' btnRemoveApplicationOnDiscover
        ' 
        btnRemoveApplicationOnDiscover.Location = New Point(277, 428)
        btnRemoveApplicationOnDiscover.Name = "btnRemoveApplicationOnDiscover"
        btnRemoveApplicationOnDiscover.Size = New Size(94, 50)
        btnRemoveApplicationOnDiscover.TabIndex = 126
        btnRemoveApplicationOnDiscover.Text = "Remove"
        btnRemoveApplicationOnDiscover.UseVisualStyleBackColor = True
        ' 
        ' btnAddApplicationOnDiscover
        ' 
        btnAddApplicationOnDiscover.Location = New Point(277, 372)
        btnAddApplicationOnDiscover.Name = "btnAddApplicationOnDiscover"
        btnAddApplicationOnDiscover.Size = New Size(94, 50)
        btnAddApplicationOnDiscover.TabIndex = 125
        btnAddApplicationOnDiscover.Text = "Add"
        btnAddApplicationOnDiscover.UseVisualStyleBackColor = True
        ' 
        ' Label32
        ' 
        Label32.AutoSize = True
        Label32.Location = New Point(6, 341)
        Label32.Name = "Label32"
        Label32.Size = New Size(170, 20)
        Label32.TabIndex = 124
        Label32.Text = "Application On Discover"
        ' 
        ' lbApplicationOnDiscover
        ' 
        lbApplicationOnDiscover.FormattingEnabled = True
        lbApplicationOnDiscover.ItemHeight = 20
        lbApplicationOnDiscover.Location = New Point(6, 372)
        lbApplicationOnDiscover.Name = "lbApplicationOnDiscover"
        lbApplicationOnDiscover.Size = New Size(265, 104)
        lbApplicationOnDiscover.TabIndex = 123
        ' 
        ' txtDiscoveryTriggers
        ' 
        txtDiscoveryTriggers.Location = New Point(183, 191)
        txtDiscoveryTriggers.Margin = New Padding(3, 4, 3, 4)
        txtDiscoveryTriggers.Name = "txtDiscoveryTriggers"
        txtDiscoveryTriggers.Size = New Size(188, 27)
        txtDiscoveryTriggers.TabIndex = 122
        ' 
        ' btnRemoveDiscoveryTrigger
        ' 
        btnRemoveDiscoveryTrigger.Location = New Point(277, 281)
        btnRemoveDiscoveryTrigger.Name = "btnRemoveDiscoveryTrigger"
        btnRemoveDiscoveryTrigger.Size = New Size(94, 50)
        btnRemoveDiscoveryTrigger.TabIndex = 121
        btnRemoveDiscoveryTrigger.Text = "Remove"
        btnRemoveDiscoveryTrigger.UseVisualStyleBackColor = True
        ' 
        ' btnAddDiscoveryTrigger
        ' 
        btnAddDiscoveryTrigger.Location = New Point(277, 225)
        btnAddDiscoveryTrigger.Name = "btnAddDiscoveryTrigger"
        btnAddDiscoveryTrigger.Size = New Size(94, 50)
        btnAddDiscoveryTrigger.TabIndex = 120
        btnAddDiscoveryTrigger.Text = "Add"
        btnAddDiscoveryTrigger.UseVisualStyleBackColor = True
        ' 
        ' Label31
        ' 
        Label31.AutoSize = True
        Label31.Location = New Point(6, 194)
        Label31.Name = "Label31"
        Label31.Size = New Size(130, 20)
        Label31.TabIndex = 119
        Label31.Text = "Discovery Triggers"
        ' 
        ' lbDiscoveryTriggers
        ' 
        lbDiscoveryTriggers.FormattingEnabled = True
        lbDiscoveryTriggers.ItemHeight = 20
        lbDiscoveryTriggers.Location = New Point(6, 225)
        lbDiscoveryTriggers.Name = "lbDiscoveryTriggers"
        lbDiscoveryTriggers.Size = New Size(265, 104)
        lbDiscoveryTriggers.TabIndex = 118
        ' 
        ' txtKeyMergeOnDiscovery
        ' 
        txtKeyMergeOnDiscovery.Location = New Point(183, 46)
        txtKeyMergeOnDiscovery.Margin = New Padding(3, 4, 3, 4)
        txtKeyMergeOnDiscovery.Name = "txtKeyMergeOnDiscovery"
        txtKeyMergeOnDiscovery.Size = New Size(188, 27)
        txtKeyMergeOnDiscovery.TabIndex = 117
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(277, 136)
        Button1.Name = "Button1"
        Button1.Size = New Size(94, 50)
        Button1.TabIndex = 116
        Button1.Text = "Remove"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(277, 80)
        Button2.Name = "Button2"
        Button2.Size = New Size(94, 50)
        Button2.TabIndex = 115
        Button2.Text = "Add"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Label30
        ' 
        Label30.AutoSize = True
        Label30.Location = New Point(6, 49)
        Label30.Name = "Label30"
        Label30.Size = New Size(171, 20)
        Label30.TabIndex = 114
        Label30.Text = "Key Merge On Discovery"
        ' 
        ' lbKeyMergeOnDiscovery
        ' 
        lbKeyMergeOnDiscovery.FormattingEnabled = True
        lbKeyMergeOnDiscovery.ItemHeight = 20
        lbKeyMergeOnDiscovery.Location = New Point(6, 80)
        lbKeyMergeOnDiscovery.Name = "lbKeyMergeOnDiscovery"
        lbKeyMergeOnDiscovery.Size = New Size(265, 104)
        lbKeyMergeOnDiscovery.TabIndex = 113
        ' 
        ' cmbDiscoverOnCreate
        ' 
        cmbDiscoverOnCreate.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDiscoverOnCreate.FormattingEnabled = True
        cmbDiscoverOnCreate.Items.AddRange(New Object() {"false", "true"})
        cmbDiscoverOnCreate.Location = New Point(165, 10)
        cmbDiscoverOnCreate.Margin = New Padding(3, 4, 3, 4)
        cmbDiscoverOnCreate.Name = "cmbDiscoverOnCreate"
        cmbDiscoverOnCreate.Size = New Size(206, 28)
        cmbDiscoverOnCreate.TabIndex = 112
        ' 
        ' Label28
        ' 
        Label28.AutoSize = True
        Label28.Location = New Point(6, 14)
        Label28.Name = "Label28"
        Label28.Size = New Size(136, 20)
        Label28.TabIndex = 111
        Label28.Text = "Discover On Create"
        ' 
        ' TabPage5
        ' 
        TabPage5.Controls.Add(Label43)
        TabPage5.Controls.Add(Label42)
        TabPage5.Controls.Add(Label41)
        TabPage5.Controls.Add(Label40)
        TabPage5.Controls.Add(nudPinnedBGColourA)
        TabPage5.Controls.Add(nudPinnedBGColourB)
        TabPage5.Controls.Add(nudPinnedBGColourG)
        TabPage5.Controls.Add(nudPinnedBGColourR)
        TabPage5.Controls.Add(Label39)
        TabPage5.Controls.Add(cmbPinnedStyle)
        TabPage5.Controls.Add(Label38)
        TabPage5.Controls.Add(cmbEnableFacts)
        TabPage5.Controls.Add(Label37)
        TabPage5.Controls.Add(cmbEnableSummary)
        TabPage5.Controls.Add(Label36)
        TabPage5.Controls.Add(txtMatchTypes)
        TabPage5.Controls.Add(btnRemoveMatchTypes)
        TabPage5.Controls.Add(btnAddMatchTypes)
        TabPage5.Controls.Add(Label35)
        TabPage5.Controls.Add(lbMatchTypes)
        TabPage5.Controls.Add(cmbIsMatchParent)
        TabPage5.Controls.Add(Label34)
        TabPage5.Location = New Point(4, 29)
        TabPage5.Name = "TabPage5"
        TabPage5.Padding = New Padding(3)
        TabPage5.Size = New Size(444, 529)
        TabPage5.TabIndex = 4
        TabPage5.Text = "Page 5"
        TabPage5.UseVisualStyleBackColor = True
        ' 
        ' Label43
        ' 
        Label43.AutoSize = True
        Label43.Location = New Point(268, 356)
        Label43.Name = "Label43"
        Label43.Size = New Size(19, 20)
        Label43.TabIndex = 137
        Label43.Text = "A"
        ' 
        ' Label42
        ' 
        Label42.AutoSize = True
        Label42.Location = New Point(216, 356)
        Label42.Name = "Label42"
        Label42.Size = New Size(18, 20)
        Label42.TabIndex = 136
        Label42.Text = "B"
        ' 
        ' Label41
        ' 
        Label41.AutoSize = True
        Label41.Location = New Point(159, 356)
        Label41.Name = "Label41"
        Label41.Size = New Size(19, 20)
        Label41.TabIndex = 135
        Label41.Text = "G"
        ' 
        ' Label40
        ' 
        Label40.AutoSize = True
        Label40.Location = New Point(101, 356)
        Label40.Name = "Label40"
        Label40.Size = New Size(18, 20)
        Label40.TabIndex = 134
        Label40.Text = "R"
        ' 
        ' nudPinnedBGColourA
        ' 
        nudPinnedBGColourA.Location = New Point(260, 326)
        nudPinnedBGColourA.Name = "nudPinnedBGColourA"
        nudPinnedBGColourA.Size = New Size(50, 27)
        nudPinnedBGColourA.TabIndex = 133
        nudPinnedBGColourA.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' nudPinnedBGColourB
        ' 
        nudPinnedBGColourB.Location = New Point(204, 326)
        nudPinnedBGColourB.Name = "nudPinnedBGColourB"
        nudPinnedBGColourB.Size = New Size(50, 27)
        nudPinnedBGColourB.TabIndex = 132
        nudPinnedBGColourB.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' nudPinnedBGColourG
        ' 
        nudPinnedBGColourG.Location = New Point(148, 326)
        nudPinnedBGColourG.Name = "nudPinnedBGColourG"
        nudPinnedBGColourG.Size = New Size(50, 27)
        nudPinnedBGColourG.TabIndex = 131
        nudPinnedBGColourG.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' nudPinnedBGColourR
        ' 
        nudPinnedBGColourR.Location = New Point(90, 326)
        nudPinnedBGColourR.Name = "nudPinnedBGColourR"
        nudPinnedBGColourR.Size = New Size(50, 27)
        nudPinnedBGColourR.TabIndex = 130
        nudPinnedBGColourR.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Label39
        ' 
        Label39.AutoSize = True
        Label39.Location = New Point(107, 303)
        Label39.Name = "Label39"
        Label39.Size = New Size(185, 20)
        Label39.TabIndex = 129
        Label39.Text = "Pinned Background Colour"
        ' 
        ' cmbPinnedStyle
        ' 
        cmbPinnedStyle.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPinnedStyle.FormattingEnabled = True
        cmbPinnedStyle.Items.AddRange(New Object() {"polaroid", "stickNote"})
        cmbPinnedStyle.Location = New Point(165, 261)
        cmbPinnedStyle.Margin = New Padding(3, 4, 3, 4)
        cmbPinnedStyle.Name = "cmbPinnedStyle"
        cmbPinnedStyle.Size = New Size(206, 28)
        cmbPinnedStyle.TabIndex = 128
        ' 
        ' Label38
        ' 
        Label38.AutoSize = True
        Label38.Location = New Point(6, 265)
        Label38.Name = "Label38"
        Label38.Size = New Size(90, 20)
        Label38.TabIndex = 127
        Label38.Text = "Pinned Style"
        ' 
        ' cmbEnableFacts
        ' 
        cmbEnableFacts.DropDownStyle = ComboBoxStyle.DropDownList
        cmbEnableFacts.FormattingEnabled = True
        cmbEnableFacts.Items.AddRange(New Object() {"false", "true"})
        cmbEnableFacts.Location = New Point(165, 225)
        cmbEnableFacts.Margin = New Padding(3, 4, 3, 4)
        cmbEnableFacts.Name = "cmbEnableFacts"
        cmbEnableFacts.Size = New Size(206, 28)
        cmbEnableFacts.TabIndex = 126
        ' 
        ' Label37
        ' 
        Label37.AutoSize = True
        Label37.Location = New Point(6, 229)
        Label37.Name = "Label37"
        Label37.Size = New Size(90, 20)
        Label37.TabIndex = 125
        Label37.Text = "Enable Facts"
        ' 
        ' cmbEnableSummary
        ' 
        cmbEnableSummary.DropDownStyle = ComboBoxStyle.DropDownList
        cmbEnableSummary.FormattingEnabled = True
        cmbEnableSummary.Items.AddRange(New Object() {"false", "true"})
        cmbEnableSummary.Location = New Point(165, 189)
        cmbEnableSummary.Margin = New Padding(3, 4, 3, 4)
        cmbEnableSummary.Name = "cmbEnableSummary"
        cmbEnableSummary.Size = New Size(206, 28)
        cmbEnableSummary.TabIndex = 124
        ' 
        ' Label36
        ' 
        Label36.AutoSize = True
        Label36.Location = New Point(6, 193)
        Label36.Name = "Label36"
        Label36.Size = New Size(120, 20)
        Label36.TabIndex = 123
        Label36.Text = "Enable Summary"
        ' 
        ' txtMatchTypes
        ' 
        txtMatchTypes.Location = New Point(165, 44)
        txtMatchTypes.Margin = New Padding(3, 4, 3, 4)
        txtMatchTypes.Name = "txtMatchTypes"
        txtMatchTypes.Size = New Size(206, 27)
        txtMatchTypes.TabIndex = 122
        ' 
        ' btnRemoveMatchTypes
        ' 
        btnRemoveMatchTypes.Location = New Point(277, 134)
        btnRemoveMatchTypes.Name = "btnRemoveMatchTypes"
        btnRemoveMatchTypes.Size = New Size(94, 50)
        btnRemoveMatchTypes.TabIndex = 121
        btnRemoveMatchTypes.Text = "Remove"
        btnRemoveMatchTypes.UseVisualStyleBackColor = True
        ' 
        ' btnAddMatchTypes
        ' 
        btnAddMatchTypes.Location = New Point(277, 78)
        btnAddMatchTypes.Name = "btnAddMatchTypes"
        btnAddMatchTypes.Size = New Size(94, 50)
        btnAddMatchTypes.TabIndex = 120
        btnAddMatchTypes.Text = "Add"
        btnAddMatchTypes.UseVisualStyleBackColor = True
        ' 
        ' Label35
        ' 
        Label35.AutoSize = True
        Label35.Location = New Point(6, 47)
        Label35.Name = "Label35"
        Label35.Size = New Size(91, 20)
        Label35.TabIndex = 119
        Label35.Text = "Match Types"
        ' 
        ' lbMatchTypes
        ' 
        lbMatchTypes.FormattingEnabled = True
        lbMatchTypes.ItemHeight = 20
        lbMatchTypes.Location = New Point(6, 78)
        lbMatchTypes.Name = "lbMatchTypes"
        lbMatchTypes.Size = New Size(265, 104)
        lbMatchTypes.TabIndex = 118
        ' 
        ' cmbIsMatchParent
        ' 
        cmbIsMatchParent.DropDownStyle = ComboBoxStyle.DropDownList
        cmbIsMatchParent.FormattingEnabled = True
        cmbIsMatchParent.Items.AddRange(New Object() {"false", "true"})
        cmbIsMatchParent.Location = New Point(165, 8)
        cmbIsMatchParent.Margin = New Padding(3, 4, 3, 4)
        cmbIsMatchParent.Name = "cmbIsMatchParent"
        cmbIsMatchParent.Size = New Size(206, 28)
        cmbIsMatchParent.TabIndex = 114
        ' 
        ' Label34
        ' 
        Label34.AutoSize = True
        Label34.Location = New Point(6, 12)
        Label34.Name = "Label34"
        Label34.Size = New Size(109, 20)
        Label34.TabIndex = 113
        Label34.Text = "Is Match Parent"
        ' 
        ' btnGenerate
        ' 
        btnGenerate.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point)
        btnGenerate.Location = New Point(17, 606)
        btnGenerate.Margin = New Padding(3, 4, 3, 4)
        btnGenerate.Name = "btnGenerate"
        btnGenerate.Size = New Size(444, 31)
        btnGenerate.TabIndex = 34
        btnGenerate.Text = "Generate"
        btnGenerate.UseVisualStyleBackColor = True
        ' 
        ' MenuMain
        ' 
        MenuMain.ImageScalingSize = New Size(20, 20)
        MenuMain.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem})
        MenuMain.Location = New Point(0, 0)
        MenuMain.Name = "MenuMain"
        MenuMain.Size = New Size(1004, 28)
        MenuMain.TabIndex = 35
        MenuMain.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {NewFileToolStripMenuItem, OpenPreseToolStripMenuItem, ToolStripMenuItem2, ToolStripMenuItem3, DeletePresetToolStripMenuItem})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(46, 24)
        FileToolStripMenuItem.Text = "File"
        ' 
        ' NewFileToolStripMenuItem
        ' 
        NewFileToolStripMenuItem.Name = "NewFileToolStripMenuItem"
        NewFileToolStripMenuItem.Size = New Size(224, 26)
        NewFileToolStripMenuItem.Text = "New Preset"
        ' 
        ' OpenPreseToolStripMenuItem
        ' 
        OpenPreseToolStripMenuItem.Name = "OpenPreseToolStripMenuItem"
        OpenPreseToolStripMenuItem.Size = New Size(224, 26)
        OpenPreseToolStripMenuItem.Text = "Open Preset"
        ' 
        ' ToolStripMenuItem2
        ' 
        ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        ToolStripMenuItem2.Size = New Size(224, 26)
        ToolStripMenuItem2.Text = "Save Preset"
        ' 
        ' ToolStripMenuItem3
        ' 
        ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        ToolStripMenuItem3.Size = New Size(224, 26)
        ToolStripMenuItem3.Text = "Close Preset"
        ' 
        ' DeletePresetToolStripMenuItem
        ' 
        DeletePresetToolStripMenuItem.Name = "DeletePresetToolStripMenuItem"
        DeletePresetToolStripMenuItem.Size = New Size(224, 26)
        DeletePresetToolStripMenuItem.Text = "Delete Preset"
        ' 
        ' pnCover
        ' 
        pnCover.BackColor = Color.Silver
        pnCover.Location = New Point(0, 32)
        pnCover.Margin = New Padding(3, 4, 3, 4)
        pnCover.Name = "pnCover"
        pnCover.Size = New Size(1004, 622)
        pnCover.TabIndex = 48
        ' 
        ' EvidencePresetForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1004, 650)
        Controls.Add(btnGenerate)
        Controls.Add(tcMain)
        Controls.Add(lblOutput)
        Controls.Add(txtOutput)
        Controls.Add(MenuMain)
        Controls.Add(pnCover)
        MainMenuStrip = MenuMain
        MaximumSize = New Size(1022, 697)
        MinimumSize = New Size(1022, 697)
        Name = "EvidencePresetForm"
        Text = "Evidence Preset"
        tcMain.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        CType(nudRelCamPhotoEulerY, ComponentModel.ISupportInitialize).EndInit()
        CType(nudRelCamPhotoEulerX, ComponentModel.ISupportInitialize).EndInit()
        CType(nudRelCamPhotoEulerZ, ComponentModel.ISupportInitialize).EndInit()
        CType(nudRelCamPhotoPosY, ComponentModel.ISupportInitialize).EndInit()
        CType(nudRelCamPhotoPosX, ComponentModel.ISupportInitialize).EndInit()
        CType(nudRelCamPhotoPosZ, ComponentModel.ISupportInitialize).EndInit()
        TabPage3.ResumeLayout(False)
        TabPage3.PerformLayout()
        CType(nudCaptureTimeOfDay, ComponentModel.ISupportInitialize).EndInit()
        TabPage4.ResumeLayout(False)
        TabPage4.PerformLayout()
        TabPage5.ResumeLayout(False)
        TabPage5.PerformLayout()
        CType(nudPinnedBGColourA, ComponentModel.ISupportInitialize).EndInit()
        CType(nudPinnedBGColourB, ComponentModel.ISupportInitialize).EndInit()
        CType(nudPinnedBGColourG, ComponentModel.ISupportInitialize).EndInit()
        CType(nudPinnedBGColourR, ComponentModel.ISupportInitialize).EndInit()
        MenuMain.ResumeLayout(False)
        MenuMain.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblCaseName As Label
    Friend WithEvents txtPresetName As TextBox
    Friend WithEvents cmbCopyFromMain As ComboBox
    Friend WithEvents lblCopyFromMain As Label
    Friend WithEvents lblOutput As Label
    Friend WithEvents txtOutput As RichTextBox
    Friend WithEvents tcMain As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents btnGenerate As Button
    Friend WithEvents MenuMain As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents txtSubClass As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbWindowStyle As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lblDisabled As Label
    Friend WithEvents cmbUseDataKeys As ComboBox
    Friend WithEvents btnRemoveValidKeys As Button
    Friend WithEvents btnAddValidKeys As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents lbValidKeys As ListBox
    Friend WithEvents txtValidKeys As TextBox
    Friend WithEvents txtPassiveTies As TextBox
    Friend WithEvents btnRemovePassiveTies As Button
    Friend WithEvents btnAddPassiveTies As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents lbPassiveTies As ListBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbNotifyOfTies As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbBelongsToInName As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cmbIsSingleton As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents cmbDisableHistory As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents cmbAllowCustomNames As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents cmbMarkDiscovered As ComboBox
    Friend WithEvents cmbForceWorldInteraction As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents cmbUseWindowFocusMode As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtIconSpriteLarge As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtDefaultNullImage As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents cmbUseInGamePhoto As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents cmbUseWriter As ComboBox
    Friend WithEvents Label17 As Label
    Friend WithEvents nudRelCamPhotoPosY As NumericUpDown
    Friend WithEvents nudRelCamPhotoPosX As NumericUpDown
    Friend WithEvents nudRelCamPhotoPosZ As NumericUpDown
    Friend WithEvents nudRelCamPhotoEulerY As NumericUpDown
    Friend WithEvents nudRelCamPhotoEulerX As NumericUpDown
    Friend WithEvents nudRelCamPhotoEulerZ As NumericUpDown
    Friend WithEvents Label18 As Label
    Friend WithEvents cmbCaptureRules As ComboBox
    Friend WithEvents Label19 As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents cmbChangeTimeOfDay As ComboBox
    Friend WithEvents Label20 As Label
    Friend WithEvents nudCaptureTimeOfDay As NumericUpDown
    Friend WithEvents Label21 As Label
    Friend WithEvents cmbUseCaptureLight As ComboBox
    Friend WithEvents Label22 As Label
    Friend WithEvents cmbUseSurveillanceCapture As ComboBox
    Friend WithEvents Label23 As Label
    Friend WithEvents cmbItemOwner As ComboBox
    Friend WithEvents Label24 As Label
    Friend WithEvents cmbItemReceiver As ComboBox
    Friend WithEvents Label26 As Label
    Friend WithEvents cmbItemWriter As ComboBox
    Friend WithEvents Label25 As Label
    Friend WithEvents txtFactSetup As TextBox
    Friend WithEvents btnRemoveFactSetup As Button
    Friend WithEvents btnAddFactSetup As Button
    Friend WithEvents Label27 As Label
    Friend WithEvents lbFactSetup As ListBox
    Friend WithEvents txtAddFactLinks As TextBox
    Friend WithEvents btnRemoveFactLink As Button
    Friend WithEvents btnAddFactLink As Button
    Friend WithEvents Label29 As Label
    Friend WithEvents lbAddFactLinks As ListBox
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents cmbDiscoverOnCreate As ComboBox
    Friend WithEvents Label28 As Label
    Friend WithEvents txtKeyMergeOnDiscovery As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label30 As Label
    Friend WithEvents lbKeyMergeOnDiscovery As ListBox
    Friend WithEvents txtDiscoveryTriggers As TextBox
    Friend WithEvents btnRemoveDiscoveryTrigger As Button
    Friend WithEvents btnAddDiscoveryTrigger As Button
    Friend WithEvents Label31 As Label
    Friend WithEvents lbDiscoveryTriggers As ListBox
    Friend WithEvents txtApplicationOnDiscover As TextBox
    Friend WithEvents btnRemoveApplicationOnDiscover As Button
    Friend WithEvents btnAddApplicationOnDiscover As Button
    Friend WithEvents Label32 As Label
    Friend WithEvents lbApplicationOnDiscover As ListBox
    Friend WithEvents txtDDSDocumentID As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents txtMatchTypes As TextBox
    Friend WithEvents btnRemoveMatchTypes As Button
    Friend WithEvents btnAddMatchTypes As Button
    Friend WithEvents Label35 As Label
    Friend WithEvents lbMatchTypes As ListBox
    Friend WithEvents cmbIsMatchParent As ComboBox
    Friend WithEvents Label34 As Label
    Friend WithEvents cmbEnableFacts As ComboBox
    Friend WithEvents Label37 As Label
    Friend WithEvents cmbEnableSummary As ComboBox
    Friend WithEvents Label36 As Label
    Friend WithEvents cmbPinnedStyle As ComboBox
    Friend WithEvents Label38 As Label
    Friend WithEvents nudPinnedBGColourA As NumericUpDown
    Friend WithEvents nudPinnedBGColourB As NumericUpDown
    Friend WithEvents nudPinnedBGColourG As NumericUpDown
    Friend WithEvents nudPinnedBGColourR As NumericUpDown
    Friend WithEvents Label39 As Label
    Friend WithEvents Label43 As Label
    Friend WithEvents Label42 As Label
    Friend WithEvents Label41 As Label
    Friend WithEvents Label40 As Label
    Friend WithEvents NewFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenPreseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents DeletePresetToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents pnCover As Panel
End Class
