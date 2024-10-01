Imports AutoUpdaterDotNET
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Security.Policy
Imports System.Text

Public Class Form1
    Private currentVersion As String = "1.0.0" ' Your current application version
    Private updateFilePath As String = "update.txt" ' Path to your update text file

    Private defaultValues As New Dictionary(Of String, Object)
    Private listBoxItemCounts As New Dictionary(Of ListBox, Integer)
    Private WithEvents listBoxMonitorTimer As New Timer()
    Private controlEventHandlers As New Dictionary(Of Control, [Delegate])
    Private tabControlEventHandlers As New Dictionary(Of TabControl, ControlEventHandler)

    Private weaponTabOpen, hexacoTabOpen, ddsConfessionalTabOpen, playerTauntTabOpen, isCaseMadeOrLoaded, areControlsEnabled As Boolean

    Private previousOutput As String = String.Empty
    Private isOutputChanged As Boolean = False

    Public toolTip As New ToolTip()
    Dim tooltips As New ToolTips()

    Public setTabValueMO, setTabValueCC As Integer

    Public openForm As Form

    Private moleadEntryCount, callingcardEntryCount, murdererTraitEntryCount, victimTraitEntryCount, murdererJobModifierEntryCount, victimJobModifierEntryCount, murdererCompanyModifierEntryCount, victimCompanyModifierEntryCount, graffitiEntryCount As Integer

    Private loadedJsonDataPub As JObject

    Public tempFilePath, tempFolderName, folderNameNew As String
    Public lastCreated, lastOpened As Boolean
    Private Function GetDefaultJson() As String
        Return "{
  ""presetName"": """",
  ""notes"": """",
  ""disabled"": false,
  ""compatibleWith"": [
    ""REF:MurderPreset|SerialKiller""
  ],
  ""baseDifficulty"": 0,
  ""maximumPotentialScore"": 0,
  ""updateThis"": false,
  ""pickRandomScoreRange"": {
    ""x"": 0,
    ""y"": 1
  },
  ""murdererTraitModifiers"": [],
  ""murdererJobModifiers"": [],
  ""murdererCompanyModifiers"": [],
  ""useMurdererSocialClassRange"": false,
  ""murdererClassRange"": {
    ""x"": 0,
    ""y"": 1
  },
  ""murdererClassRangeBoost"": 0,
  ""useHexaco"": false,
  ""hexaco"": {
    ""outputMin"": 1,
    ""outputMax"": 10,
    ""enableFeminineMasculine"": false,
    ""feminineMasculine"": 0,
    ""enableHumility"": false,
    ""humility"": 0,
    ""enableEmotionality"": false,
    ""emotionality"": 0,
    ""enableExtraversion"": false,
    ""extraversion"": 0,
    ""enableAgreeableness"": false,
    ""agreeableness"": 0,
    ""enableConscientiousness"": false,
    ""conscientiousness"": 0,
    ""enableCreativity"": false,
    ""creativity"": 0
  },
  ""requiresSniperVantageAtHome"": false,
  ""weaponsPool"": [],
  ""blockDroppingWeapons"": false,
  ""allowAnywhere"": false,
  ""allowHome"": true,
  ""allowWork"": false,
  ""allowPublic"": false,
  ""allowStreets"": false,
  ""allowDen"": false,
  ""acquaintedSuitabilityBoost"": 0,
  ""attractedToSuitabilityBoost"": 0,
  ""likeSuitabilityBoost"": 0,
  ""sameWorkplaceBoost"": 0,
  ""murdererIsTenantBoost"": 0,
  ""victimRandomScoreRange"": {
    ""x"": 0,
    ""y"": 1
  },
  ""victimTraitModifiers"": [],
  ""victimJobModifiers"": [],
  ""victimCompanyModifiers"": [],
  ""useVictimSocialClassRange"": false,
  ""victimClassRange"": {
    ""x"": 0,
    ""y"": 1
  },
  ""victimClassRangeBoost"": 0,
  ""monkierDDSMessageList"": """",
  ""confessionalDDSResponses"": [],
  ""MOleads"": [],
  ""graffiti"": [],
  ""callingCardPool"": [],
  ""playerTaunts"": [],
  ""fileType"": ""MurderMO"",
  ""name"": """",
  ""copyFrom"": null
}"
    End Function
    Private Sub btnGenerateCase_Click(sender As Object, e As EventArgs) Handles btnGenerateCase.Click
        Dim defaultCase As JObject = JObject.Parse(GetDefaultJson())

        If loadedJsonDataPub IsNot Nothing Then
            ' Merge loadedJsonData into defaultCase
            For Each jsonProperty In loadedJsonDataPub.Properties()
                defaultCase(jsonProperty.Name) = jsonProperty.Value
            Next
        End If

        Dim presetName As String = If(String.IsNullOrEmpty(txtPresetName.Text), defaultCase("name")?.ToString(), txtPresetName.Text)
        ' Update default values with form data
        defaultCase("presetName") = presetName
        defaultCase("notes") = txtNotes.Text

        If cmbCopyFromMain.SelectedIndex = 0 Then
            defaultCase("copyFrom") = Nothing
        Else
            defaultCase("copyFrom") = "REF:MurderMO|" & cmbCopyFromMain.Text
        End If

        defaultCase("disabled") = Boolean.Parse(cmbDisabled.SelectedItem.ToString())

        Dim compatibleWithArray As New JArray()
        For Each item As String In lstCompatibleWith.Items
            compatibleWithArray.Add("REF:MurderPreset|" & item)
        Next

        defaultCase("compatibleWith") = compatibleWithArray
        defaultCase("baseDifficulty") = Integer.Parse(nudBaseDifficulty.Value)
        defaultCase("maximumPotentialScore") = Integer.Parse(nudMaxPotScore.Value)
        defaultCase("updateThis") = Boolean.Parse(cmbUpdateThis.SelectedItem.ToString())
        defaultCase("pickRandomScoreRange")("x") = Integer.Parse(nudRndScoreX.Value)
        defaultCase("pickRandomScoreRange")("y") = Integer.Parse(nudRndScoreY.Value)
        defaultCase("requiresSniperVantageAtHome") = Boolean.Parse(cmbSniperVantage.SelectedItem.ToString())
        defaultCase("blockDroppingWeapons") = Boolean.Parse(cmbBlockWeaponDrops.SelectedItem.ToString())
        defaultCase("name") = presetName
        defaultCase("monkierDDSMessageList") = txtMonkier.Text

        defaultCase("useMurdererSocialClassRange") = Boolean.Parse(cmbMurdererClassRange.SelectedItem.ToString())
        defaultCase("murdererClassRange")("x") = Integer.Parse(nudMurdererCrX.Value)
        defaultCase("murdererClassRange")("y") = Integer.Parse(nudMurdererCrY.Value)
        defaultCase("murdererClassRangeBoost") = Integer.Parse(nudMurdererCRBoost.Value)
        defaultCase("acquaintedSuitabilityBoost") = Integer.Parse(nudAcquaintSuitBoost.Value)
        defaultCase("likeSuitabilityBoost") = Integer.Parse(nudLikeSuitBoost.Value)
        defaultCase("sameWorkplaceBoost") = Integer.Parse(nudSameWork.Value)
        defaultCase("murdererIsTenantBoost") = Integer.Parse(nudMurdererIsTenantBoost.Value)
        defaultCase("attractedToSuitabilityBoost") = Integer.Parse(nudAttractSuitBoost.Value)

        defaultCase("victimRandomScoreRange")("x") = Integer.Parse(nudVictimRndCrX.Value)
        defaultCase("victimRandomScoreRange")("y") = Integer.Parse(nudVictimRndCrY.Value)
        defaultCase("useVictimSocialClassRange") = Boolean.Parse(cmbVictimSocialClassRange.SelectedItem.ToString())
        defaultCase("victimClassRange")("x") = Integer.Parse(nudVictimCrX.Value)
        defaultCase("victimClassRange")("y") = Integer.Parse(nudVictimCrY.Value)
        defaultCase("victimClassRangeBoost") = Integer.Parse(nudVictimCRBoost.Value)

        ' Update Hexaco Traits
        If cmbUseHexaco.SelectedItem.ToString().ToLower() = "true" Then
            defaultCase("useHexaco") = True

            ' Retrieve the hexaco values from the controls on the tab
            Dim hexaco As New JObject()
            For Each tpHexaco As TabPage In tabControlCase.TabPages
                If tpHexaco.Text.StartsWith("Hexaco Modifiers") Then

                    ' For each trait, retrieve the numeric value and the corresponding enable flag from ComboBox
                    hexaco("outputMin") = CInt(CType(tpHexaco.Controls("nudOutputMin"), NumericUpDown).Value)
                    hexaco("outputMax") = CInt(CType(tpHexaco.Controls("nudOutputMax"), NumericUpDown).Value)

                    ' Feminine Masculine trait
                    hexaco("enableFeminineMasculine") = Boolean.Parse(CType(tpHexaco.Controls("cbEnableFeminineMasculine"), ComboBox).SelectedItem.ToString())
                    hexaco("feminineMasculine") = CInt(CType(tpHexaco.Controls("nudFeminineMasculine"), NumericUpDown).Value)

                    ' Humility trait
                    hexaco("enableHumility") = Boolean.Parse(CType(tpHexaco.Controls("cbEnableHumility"), ComboBox).SelectedItem.ToString())
                    hexaco("humility") = CInt(CType(tpHexaco.Controls("nudHumility"), NumericUpDown).Value)

                    ' Emotionality trait
                    hexaco("enableEmotionality") = Boolean.Parse(CType(tpHexaco.Controls("cbEnableEmotionality"), ComboBox).SelectedItem.ToString())
                    hexaco("emotionality") = CInt(CType(tpHexaco.Controls("nudEmotionality"), NumericUpDown).Value)

                    ' Extraversion trait
                    hexaco("enableExtraversion") = Boolean.Parse(CType(tpHexaco.Controls("cbEnableExtraversion"), ComboBox).SelectedItem.ToString())
                    hexaco("extraversion") = CInt(CType(tpHexaco.Controls("nudExtraversion"), NumericUpDown).Value)

                    ' Agreeableness trait
                    hexaco("enableAgreeableness") = Boolean.Parse(CType(tpHexaco.Controls("cbEnableAgreeableness"), ComboBox).SelectedItem.ToString())
                    hexaco("agreeableness") = CInt(CType(tpHexaco.Controls("nudAgreeableness"), NumericUpDown).Value)

                    ' Conscientiousness trait
                    hexaco("enableConscientiousness") = Boolean.Parse(CType(tpHexaco.Controls("cbEnableConscientiousness"), ComboBox).SelectedItem.ToString())
                    hexaco("conscientiousness") = CInt(CType(tpHexaco.Controls("nudConscientiousness"), NumericUpDown).Value)

                    ' Creativity trait
                    hexaco("enableCreativity") = Boolean.Parse(CType(tpHexaco.Controls("cbEnableCreativity"), ComboBox).SelectedItem.ToString())
                    hexaco("creativity") = CInt(CType(tpHexaco.Controls("nudCreativity"), NumericUpDown).Value)

                    ' Assign the hexaco data back to the default JSON
                    defaultCase("hexaco") = hexaco
                End If
            Next
        Else
            defaultCase("useHexaco") = False
        End If


        ' Update MOleads
        Dim moleads As New JArray()

        For Each tpMOlead As TabPage In tabControlCase.TabPages
            If tpMOlead.Text.StartsWith("MOlead Entry") Then ' Check for MOlead tabs
                Dim panelLeads As Panel = tpMOlead.Controls.OfType(Of Panel)().FirstOrDefault(Function(p) p.Name.Contains("pnLeads"))

                ' Create the JSON object for each entry
                Dim entry As New JObject From {
            {"name", CType(panelLeads.Controls.OfType(Of TextBox).First(Function(x) x.Name = "txtName"), TextBox).Text},
            {"compatibleWithAllMotives", If(CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbAllMotives"), ComboBox).SelectedIndex = 0, 1, 0)},
            {"compatibleWithMotives", New JArray()} ' Placeholder, we'll update this next
        }

                ' Handle the "compatibleWithMotives" logic
                Dim motivesList As New JArray()
                Dim lbMotives As ListBox = CType(panelLeads.Controls.OfType(Of ListBox).First(Function(x) x.Name = "lbMotives"), ListBox)

                For Each item As String In lbMotives.Items.Cast(Of String)()
                    Dim parsedInt As Integer
                    If Integer.TryParse(item, parsedInt) Then
                        ' Item is an integer, add it as a dictionary with "fileID" key
                        motivesList.Add(New JObject(New JProperty("fileID", parsedInt)))
                    Else
                        ' Item is a string, prepend "REF:MurderMO|"
                        motivesList.Add("REF:MurderMO|" & item)
                    End If
                Next

                ' Update the entry's "compatibleWithMotives" with the built motivesList
                entry("compatibleWithMotives") = motivesList


                ' Add other fields to the entry
                entry.Add("spawnOnPhase", CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbSpawnPhase"), ComboBox).SelectedIndex)
                entry.Add("tryToSpawnWithEachNewMurder", If(CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbSpawnEachMurder"), ComboBox).SelectedIndex = 0, 1, 0))
                entry.Add("belongsTo", CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbBelongsTo"), ComboBox).SelectedIndex)
                entry.Add("useTraits", If(CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbUseTraits"), ComboBox).SelectedIndex = 0, 1, 0))
                entry.Add("traitModifiers", New JArray())
                entry.Add("useIf", If(CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbUseIf"), ComboBox).SelectedIndex = 0, 1, 0))
                entry.Add("ifTag", CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbIfTag"), ComboBox).SelectedIndex)
                entry.Add("useOrGroup", If(CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbUseOrGroup"), ComboBox).SelectedIndex = 0, 1, 0))
                entry.Add("orGroup", CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbOrGroup"), ComboBox).SelectedIndex)
                entry.Add("chanceRatio", Convert.ToSingle(CType(panelLeads.Controls.OfType(Of TextBox).First(Function(x) x.Name = "txtChanceRatio"), TextBox).Text))
                entry.Add("itemTag", CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbItemTag"), ComboBox).SelectedIndex)
                Dim vmailProgress As New JObject From {
                    {"x", Convert.ToInt32(CType(panelLeads.Controls.OfType(Of TextBox).First(Function(x) x.Name = "txtVmailProgressX"), TextBox).Text)},
                    {"y", Convert.ToInt32(CType(panelLeads.Controls.OfType(Of TextBox).First(Function(x) x.Name = "txtVmailProgressY"), TextBox).Text)}
                 }
                entry.Add("vmailProgressThreshold", vmailProgress)
                entry.Add("chance", Convert.ToSingle(CType(panelLeads.Controls.OfType(Of TextBox).First(Function(x) x.Name = "txtChance"), TextBox).Text))
                entry.Add("spawnItem", If(CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbSpawnItem"), ComboBox).SelectedIndex = 0, CType(Nothing, String), "REF:InteractablePreset|" & CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbSpawnItem"), ComboBox).SelectedItem.ToString()))
                entry.Add("vmailThread", CType(panelLeads.Controls.OfType(Of TextBox).First(Function(x) x.Name = "txtVmail"), TextBox).Text)
                entry.Add("writer", CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbWriter"), ComboBox).SelectedIndex)
                entry.Add("receiver", CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbReceiver"), ComboBox).SelectedIndex)
                entry.Add("where", CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbWhere"), ComboBox).SelectedIndex)
                entry.Add("security", Convert.ToInt32(CType(panelLeads.Controls.OfType(Of TextBox).First(Function(x) x.Name = "txtSecurity"), TextBox).Text))
                entry.Add("priority", Convert.ToInt32(CType(panelLeads.Controls.OfType(Of TextBox).First(Function(x) x.Name = "txtPriority"), TextBox).Text))
                entry.Add("ownershipRule", CType(panelLeads.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbOwnershipRule"), ComboBox).SelectedIndex)


                Dim othersList As New JArray()
                Dim lbOthers As ListBox = CType(panelLeads.Controls.OfType(Of ListBox).First(Function(x) x.Name = "lbVmailOthers"), ListBox)
                For Each item As String In lbOthers.Items.Cast(Of String)()
                    othersList.Add("REF:MurderMO|" & item)
                Next

                entry("vmailOtherParticipants") = othersList

                'moleads.Add(entry)

                Dim innerTabControlMO As TabControl = panelLeads.Controls.OfType(Of TabControl)().FirstOrDefault(Function(p) p.Name.Contains("innerTabControlMO"))

                If innerTabControlMO IsNot Nothing Then
                    For Each tpTraitModifier As TabPage In innerTabControlMO.TabPages
                        Dim traitModifier As New JObject From {
                    {"who", CType(tpTraitModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbWho"), ComboBox).SelectedIndex},
                    {"rule", CType(tpTraitModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbRule"), ComboBox).SelectedIndex},
                    {"traitList", New JArray(CType(tpTraitModifier.Controls.OfType(Of ListBox).First(Function(x) x.Name = "lbTraitList"), ListBox).Items.Cast(Of String)().Select(Function(item) "REF:CharacterTrait|" & item).ToArray())},
                    {"mustPassForApplication", Convert.ToBoolean(CType(tpTraitModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbMustPassForApplication"), ComboBox).SelectedItem.ToString())},
                    {"chanceModifier", Convert.ToInt32(CType(tpTraitModifier.Controls.OfType(Of NumericUpDown).First(Function(x) x.Name = "nudScoreModifier"), NumericUpDown).Value)},
                    {"copyFrom", If(CType(tpTraitModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = 0, CType(Nothing, String), "REF:MurderMO|" & If(CType(tpTraitModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = -1, CType(tpTraitModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).Text, CType(tpTraitModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedItem.ToString()))}
                }
                        CType(entry("traitModifiers"), JArray).Add(traitModifier)
                    Next
                End If
                moleads.Add(entry)
            End If
        Next

        ' Set the updated moleads array in defaultCase
        defaultCase("MOleads") = moleads


        ' Update callingCardPool
        Dim callingCardPool As New JArray()

        For Each tpCallingCard As TabPage In tabControlCase.TabPages
            If tpCallingCard.Text.StartsWith("Calling Card Entry") Then
                Dim cardEntry As New JObject From {
            {"item", If(CType(tpCallingCard.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbItem"), ComboBox).SelectedIndex = 0, CType(Nothing, String), "REF:InteractablePreset|" & CType(tpCallingCard.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbItem"), ComboBox).SelectedItem.ToString())},
            {"origin", CType(tpCallingCard.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbOrigin"), ComboBox).SelectedIndex},
            {"randomScoreRange", New JObject From {
                {"x", Convert.ToInt32(CType(tpCallingCard.Controls.OfType(Of TextBox).First(Function(x) x.Name = "txtRandomScoreX"), TextBox).Text)},
                {"y", Convert.ToInt32(CType(tpCallingCard.Controls.OfType(Of TextBox).First(Function(x) x.Name = "txtRandomScoreY"), TextBox).Text)}
            }},
            {"traitModifiers", New JArray()},
            {"copyFrom", If(CType(tpCallingCard.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = 0, CType(Nothing, String), "REF:MurderMO|" & If(CType(tpCallingCard.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = -1, CType(tpCallingCard.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).Text, CType(tpCallingCard.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedItem.ToString()))}
        }

                ' Find the inner TabControl that holds the trait modifier tabs
                Dim innerTabControl As TabControl = tpCallingCard.Controls.OfType(Of TabControl)().FirstOrDefault(Function(p) p.Name.Contains("innerTabControl"))


                If innerTabControl IsNot Nothing Then
                    For Each tpTraitModifier As TabPage In innerTabControl.TabPages
                        Dim traitModifier As New JObject From {
                    {"rule", CType(tpTraitModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbRule"), ComboBox).SelectedIndex},
                    {"traitList", New JArray(CType(tpTraitModifier.Controls.OfType(Of ListBox).First(Function(x) x.Name = "lbTraitList"), ListBox).Items.Cast(Of String)().Select(Function(item) "REF:CharacterTrait|" & item).ToArray())},
                    {"mustPassForApplication", Convert.ToBoolean(CType(tpTraitModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbMustPassForApplication"), ComboBox).SelectedItem.ToString())},
                    {"scoreModifier", Convert.ToInt32(CType(tpTraitModifier.Controls.OfType(Of NumericUpDown).First(Function(x) x.Name = "nudScoreModifier"), NumericUpDown).Value)},
                    {"copyFrom", If(CType(tpTraitModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = 0, CType(Nothing, String), "REF:MurderMO|" & If(CType(tpTraitModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = -1, CType(tpTraitModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).Text, CType(tpTraitModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedItem.ToString()))}
                }
                        CType(cardEntry("traitModifiers"), JArray).Add(traitModifier)
                    Next
                End If
                callingCardPool.Add(cardEntry)
            End If
        Next

        defaultCase("callingCardPool") = callingCardPool

        ' Update Murderer traitModifiers
        Dim traitModifiers As New JArray()
        For Each tpMurdererTraits As TabPage In tabControlCase.TabPages
            If tpMurdererTraits.Text.StartsWith("Murderer Trait Modifiers") Then ' Check for Trait Modifiers tabs
                Dim traitModifierEntry As New JObject From {
            {"rule", CType(tpMurdererTraits.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbRule"), ComboBox).SelectedIndex},
            {"traitList", New JArray(CType(tpMurdererTraits.Controls.OfType(Of ListBox).First(Function(x) x.Name = "lstTraits"), ListBox).Items.Cast(Of String)().Select(Function(item) "REF:CharacterTrait|" & item).ToArray())},
            {"mustPassForApplication", If(CType(tpMurdererTraits.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbMustPass"), ComboBox).SelectedIndex = 0, 1, 0)},
            {"scoreModifier", CInt(CType(tpMurdererTraits.Controls.OfType(Of NumericUpDown).First(Function(x) x.Name = "numScoreModifier"), NumericUpDown).Value)},
            {"copyFrom", If(CType(tpMurdererTraits.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = 0, CType(Nothing, String), "REF:MurderMO|" & If(CType(tpMurdererTraits.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = -1, CType(tpMurdererTraits.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).Text, CType(tpMurdererTraits.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedItem.ToString()))}
        }
                traitModifiers.Add(traitModifierEntry)
            End If
        Next
        defaultCase("murdererTraitModifiers") = traitModifiers

        ' Update Victim traitModifiers
        Dim victimTraitModifiers As New JArray()
        For Each tpVictimTraits As TabPage In tabControlCase.TabPages
            If tpVictimTraits.Text.StartsWith("Victim Trait Modifiers") Then ' Check for Trait Modifiers tabs
                Dim victimTraitModifierEntry As New JObject From {
            {"rule", CType(tpVictimTraits.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbRule"), ComboBox).SelectedIndex},
            {"traitList", New JArray(CType(tpVictimTraits.Controls.OfType(Of ListBox).First(Function(x) x.Name = "lstTraits"), ListBox).Items.Cast(Of String)().Select(Function(item) "REF:CharacterTrait|" & item).ToArray())},
            {"mustPassForApplication", If(CType(tpVictimTraits.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbMustPass"), ComboBox).SelectedIndex = 0, 1, 0)},
            {"scoreModifier", CInt(CType(tpVictimTraits.Controls.OfType(Of NumericUpDown).First(Function(x) x.Name = "numScoreModifier"), NumericUpDown).Value)},
            {"copyFrom", If(CType(tpVictimTraits.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = 0, CType(Nothing, String), "REF:MurderMO|" & If(CType(tpVictimTraits.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = -1, CType(tpVictimTraits.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).Text, CType(tpVictimTraits.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedItem.ToString()))}
        }
                victimTraitModifiers.Add(victimTraitModifierEntry)
            End If
        Next
        defaultCase("victimTraitModifiers") = victimTraitModifiers

        ' Update weaponsPool
        Dim weaponsPool As New JArray()
        For Each tpWeapon As TabPage In tabControlCase.TabPages
            If tpWeapon.Text.StartsWith("Weapon Entry") Then ' Check for Weapon tabs
                ' Get the ListBox containing the weapons
                Dim lstWeapons As ListBox = CType(tpWeapon.Controls.OfType(Of ListBox).First(Function(x) x.Name = "lstWeapons"), ListBox)

                ' Loop through the items in the ListBox and add them to the JArray
                For Each weapon As String In lstWeapons.Items
                    weaponsPool.Add("REF:MurderWeaponsPool|" & weapon)
                Next
            End If
        Next
        defaultCase("weaponsPool") = weaponsPool

        ' Update murdererJobModifiers
        Dim jobModifiers As New JArray()
        For Each tpJobModifier As TabPage In tabControlCase.TabPages
            If tpJobModifier.Text.StartsWith("Murderer Job Modifier") Then ' Check for Job Modifiers tabs
                Dim jobModifierEntry As New JObject From {
            {"jobs", New JArray(CType(tpJobModifier.Controls.OfType(Of ListBox).First(Function(x) x.Name = "lstJobs"), ListBox).Items.Cast(Of String)().Select(Function(item) "REF:OccupationPreset|" & item).ToArray())},
            {"jobBoost", CInt(CType(tpJobModifier.Controls.OfType(Of NumericUpDown).First(Function(x) x.Name = "numJobBoost"), NumericUpDown).Value)},
            {"copyFrom", If(CType(tpJobModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = 0, CType(Nothing, String), "REF:MurderMO|" & If(CType(tpJobModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = -1, CType(tpJobModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).Text, CType(tpJobModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedItem.ToString()))}
        }

                jobModifiers.Add(jobModifierEntry)
            End If
        Next
        defaultCase("murdererJobModifiers") = jobModifiers

        ' Update victimJobModifiers
        Dim victimJobModifiers As New JArray()
        For Each tpVictimJobModifier As TabPage In tabControlCase.TabPages
            If tpVictimJobModifier.Text.StartsWith("Victim Job Modifier") Then ' Check for Job Modifiers tabs
                Dim jobModifierEntry As New JObject From {
            {"jobs", New JArray(CType(tpVictimJobModifier.Controls.OfType(Of ListBox).First(Function(x) x.Name = "lstJobs"), ListBox).Items.Cast(Of String)().Select(Function(item) "REF:OccupationPreset|" & item).ToArray())},
            {"jobBoost", CInt(CType(tpVictimJobModifier.Controls.OfType(Of NumericUpDown).First(Function(x) x.Name = "numJobBoost"), NumericUpDown).Value)},
            {"copyFrom", If(CType(tpVictimJobModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = 0, CType(Nothing, String), "REF:MurderMO|" & If(CType(tpVictimJobModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = -1, CType(tpVictimJobModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).Text, CType(tpVictimJobModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedItem.ToString()))}
        }

                victimJobModifiers.Add(jobModifierEntry)
            End If
        Next
        defaultCase("victimJobModifiers") = victimJobModifiers

        ' Update murdererCompanyModifiers
        Dim companyModifiers As New JArray()
        For Each tpCompanyModifier As TabPage In tabControlCase.TabPages
            If tpCompanyModifier.Text.StartsWith("Murderer Company Modifiers") Then ' Check for Company Modifiers tabs
                Dim companyModifierEntry As New JObject From {
            {"companies", New JArray(CType(tpCompanyModifier.Controls.OfType(Of ListBox).First(Function(x) x.Name = "lstCompanies"), ListBox).Items.Cast(Of String)().Select(Function(item) "REF:CompanyPreset|" & item).ToArray())},
            {"minimumEmployees", CInt(CType(tpCompanyModifier.Controls.OfType(Of NumericUpDown).First(Function(x) x.Name = "nudMinimumEmployees"), NumericUpDown).Value)},
            {"companyBoost", CInt(CType(tpCompanyModifier.Controls.OfType(Of NumericUpDown).First(Function(x) x.Name = "nudCompanyBoost"), NumericUpDown).Value)},
            {"boostPerEmployeeOverMinimum", CInt(CType(tpCompanyModifier.Controls.OfType(Of NumericUpDown).First(Function(x) x.Name = "nudBoostPerEmployeeOverMinimum"), NumericUpDown).Value)},
            {"copyFrom", If(CType(tpCompanyModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = 0, CType(Nothing, String), "REF:MurderMO|" & If(CType(tpCompanyModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = -1, CType(tpCompanyModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).Text, CType(tpCompanyModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedItem.ToString()))}
        }

                companyModifiers.Add(companyModifierEntry)
            End If
        Next
        defaultCase("murdererCompanyModifiers") = companyModifiers

        ' Update victimCompanyModifiers
        Dim victimCompanyModifiers As New JArray()
        For Each tpVictimCompanyModifier As TabPage In tabControlCase.TabPages
            If tpVictimCompanyModifier.Text.StartsWith("Victim Company Modifiers") Then ' Check for Company Modifiers tabs
                Dim victimCompanyModifierEntry As New JObject From {
            {"companies", New JArray(CType(tpVictimCompanyModifier.Controls.OfType(Of ListBox).First(Function(x) x.Name = "lstCompanies"), ListBox).Items.Cast(Of String)().Select(Function(item) "REF:CompanyPreset|" & item).ToArray())},
            {"minimumEmployees", CInt(CType(tpVictimCompanyModifier.Controls.OfType(Of NumericUpDown).First(Function(x) x.Name = "nudMinimumEmployees"), NumericUpDown).Value)},
            {"companyBoost", CInt(CType(tpVictimCompanyModifier.Controls.OfType(Of NumericUpDown).First(Function(x) x.Name = "nudCompanyBoost"), NumericUpDown).Value)},
            {"boostPerEmployeeOverMinimum", CInt(CType(tpVictimCompanyModifier.Controls.OfType(Of NumericUpDown).First(Function(x) x.Name = "nudBoostPerEmployeeOverMinimum"), NumericUpDown).Value)},
            {"copyFrom", If(CType(tpVictimCompanyModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = 0, CType(Nothing, String), "REF:MurderMO|" & If(CType(tpVictimCompanyModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = -1, CType(tpVictimCompanyModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).Text, CType(tpVictimCompanyModifier.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedItem.ToString()))}
        }

                victimCompanyModifiers.Add(victimCompanyModifierEntry)
            End If
        Next
        defaultCase("victimCompanyModifiers") = victimCompanyModifiers

        ' Update dds confessions
        Dim ddsConfessionals As New JArray()
        For Each tpDDSConfessionals As TabPage In tabControlCase.TabPages
            If tpDDSConfessionals.Text.StartsWith("Confessional DDS") Then
                Dim lstDDSResponses As ListBox = CType(tpDDSConfessionals.Controls.OfType(Of ListBox).First(Function(x) x.Name = "lstDDSResponses"), ListBox)

                For Each item In lstDDSResponses.Items
                    ddsConfessionals.Add(item.ToString())
                Next
            End If
        Next

        defaultCase("confessionalDDSResponses") = ddsConfessionals

        ' Update Graffiti
        Dim killerGraffiti As New JArray()
        For Each tpKillerGraffiti As TabPage In tabControlCase.TabPages
            If tpKillerGraffiti.Text.StartsWith("Graffiti Entry") Then
                Dim killerGraffitiEntry As New JObject From {
            {"preset", If(CType(tpKillerGraffiti.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbPreset"), ComboBox).SelectedIndex = -1, CType(Nothing, String), "REF:InteractablePreset|" & CType(tpKillerGraffiti.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbPreset"), ComboBox).SelectedItem.ToString())},
            {"pos", CType(tpKillerGraffiti.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbPos"), ComboBox).SelectedIndex},
            {"artImage", If(CType(tpKillerGraffiti.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbArtImage"), ComboBox).SelectedIndex = 0, CType(Nothing, String), "REF:ArtPreset|" & CType(tpKillerGraffiti.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbArtImage"), ComboBox).SelectedItem.ToString())},
            {"ddsMessageTextList", CType(tpKillerGraffiti.Controls.OfType(Of TextBox).First(Function(x) x.Name = "txtDDSMessageText"), TextBox).Text},
            {"color", CType(tpKillerGraffiti.Controls.OfType(Of TextBox).First(Function(x) x.Name = "txtColor"), TextBox).Text},
            {"size", CInt(CType(tpKillerGraffiti.Controls.OfType(Of NumericUpDown).First(Function(x) x.Name = "numSize"), NumericUpDown).Value)},
            {"copyFrom", If(CType(tpKillerGraffiti.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = 0, CType(Nothing, String), "REF:MurderMO|" & If(CType(tpKillerGraffiti.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedIndex = -1, CType(tpKillerGraffiti.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).Text, CType(tpKillerGraffiti.Controls.OfType(Of ComboBox).First(Function(x) x.Name = "cbCopyFrom"), ComboBox).SelectedItem.ToString()))}
        }

                killerGraffiti.Add(killerGraffitiEntry)
            End If
        Next
        defaultCase("graffiti") = killerGraffiti

        ' Update playerTaunts
        Dim playerTaunts As New JArray()
        For Each tpPlayerTaunts As TabPage In tabControlCase.TabPages
            If tpPlayerTaunts.Text.StartsWith("Player Taunts") Then
                Dim lstPlayerTaunts As ListBox = CType(tpPlayerTaunts.Controls.OfType(Of ListBox).First(Function(x) x.Name = "lstPlayerTaunts"), ListBox)

                For Each item In lstPlayerTaunts.Items
                    playerTaunts.Add("REF:InteractablePreset|" & item.ToString())
                Next
            End If
        Next

        defaultCase("playerTaunts") = playerTaunts

        ' Update locations
        Dim locationsTab As TabPage = tabControlCase.TabPages.Cast(Of TabPage)().FirstOrDefault(Function(tab) tab.Text = "Locations")

        If locationsTab IsNot Nothing Then
            defaultCase("allowAnywhere") = GetComboBoxValue(locationsTab, "cmbAnywhere")
            defaultCase("allowHome") = GetComboBoxValue(locationsTab, "cmbHome")
            defaultCase("allowWork") = GetComboBoxValue(locationsTab, "cmbWork")
            defaultCase("allowPublic") = GetComboBoxValue(locationsTab, "cmbPublic")
            defaultCase("allowStreets") = GetComboBoxValue(locationsTab, "cmbStreets")
            defaultCase("allowDen") = GetComboBoxValue(locationsTab, "cmbDen")
        Else
            ' Handle the case where the Locations tab is not found
            MessageBox.Show("Locations tab not found!")
        End If

        ' Serialize the final merged object to JSON
        Dim jsonOutput As String = defaultCase.ToString(Formatting.Indented)

        ' Display the JSON output in the text box
        txtOutput.Text = jsonOutput

        If My.Settings.isRemoveKeysEnabled = True Then
            RemoveUnusedKeys()
        End If
        FormatJson()

        Dim currentOutput = txtOutput.Text
        If currentOutput <> previousOutput Then
            isOutputChanged = True
            If Not Me.Text.EndsWith("*") Then
                Me.Text += "*"
            End If
        End If
        previousOutput = currentOutput

        ' Create and display the additional JSON string in txtManiOutput
        Dim maniOutput As New With {
            .enabled = True,
            .fileOrder = New List(Of String) From {"REF:" + txtPresetName.Text.ToLower()},
            .version = 1
        }

        Dim maniJsonOutput As String = JsonConvert.SerializeObject(maniOutput)
        txtManiOutput.Text = maniJsonOutput
        FormatManifest()
    End Sub
    Private Sub btnAddCompat_Click(sender As Object, e As EventArgs) Handles btnAddCompat.Click
        If cmbCompatibleWith.SelectedItem IsNot Nothing Then
            lstCompatibleWith.Items.Add(cmbCompatibleWith.SelectedItem.ToString())
            cmbCompatibleWith.SelectedIndex = 1
        ElseIf cmbCompatibleWith.SelectedIndex = -1 Then
            lstCompatibleWith.Items.Add(cmbCompatibleWith.Text)
        End If
    End Sub
    Private Sub btnRemoveCompatibleWith_Click(sender As Object, e As EventArgs) Handles btnRemoveCompatibleWith.Click
        If lstCompatibleWith.SelectedItem IsNot Nothing Then
            lstCompatibleWith.Items.Remove(lstCompatibleWith.SelectedItem)
        End If
    End Sub
    Private Function GetComboBoxValue(tab As TabPage, controlName As String) As Boolean
        Dim comboBox As ComboBox = CType(tab.Controls(controlName), ComboBox)
        If comboBox IsNot Nothing AndAlso comboBox.SelectedItem IsNot Nothing Then
            Return comboBox.SelectedItem.ToString() = "true"
        End If
        Return False ' Default value if the ComboBox is not found or no item is selected
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnAllowedWeapons.Click
        If weaponTabOpen = False Then
            Dim tpWeapon As TabPage = CreateWeaponsTab()
            tabControlCase.TabPages.Add(tpWeapon)
            tabControlCase.SelectedTab = tpWeapon
        Else
            MessageBox.Show("You already have a weapon entry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub SetupLocationControls()
        ' Clear existing tabs to ensure only one tab is open
        tabControlCase.TabPages.Clear()

        ' Create a new tab for locations
        Dim locationsTab As New TabPage()
        locationsTab.Text = "Locations"

        Dim labels() As String = {"Anywhere", "Home", "Work", "Public", "Streets", "Den"}
        Dim locationOptions As String() = {"true", "false"}

        For i As Integer = 0 To labels.Length - 1
            ' Create a new label
            Dim lbl As New Label()
            lbl.Text = labels(i)
            lbl.AutoSize = True

            ' Create a new ComboBox
            Dim cmb As New ComboBox()
            cmb.Name = "cmb" & labels(i)
            cmb.DropDownStyle = ComboBoxStyle.DropDownList
            cmb.Items.AddRange(locationOptions)
            cmb.SelectedIndex = 1 ' Set default to false

            If cmb.Name = "cmbHome" Then
                cmb.SelectedIndex = 0  ' Set home to true by default
            End If

            ' Set position for first three in the left column
            If i < 3 Then
                lbl.Location = New Point(0, 23 + (i * 30))
                cmb.Location = New Point(80, 20 + (i * 30))
            Else ' Set position for last three in the right column
                lbl.Location = New Point(225, 23 + ((i - 3) * 30))
                cmb.Location = New Point(305, 20 + ((i - 3) * 30))
            End If

            ' Add the controls to the new tab page
            locationsTab.Controls.Add(lbl)
            locationsTab.Controls.Add(cmb)
        Next

        ' Add the new tab page to the tab control
        tabControlCase.TabPages.Add(locationsTab)

        ' Optionally, set the new tab as the selected tab
        tabControlCase.SelectedTab = locationsTab
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Icon
        SetupLocationControls()
        UpdateComboBoxValues()
        txtOutput.Text = GetDefaultJson()
        FormatJson()
        FormatManifest()
        Dim bgColor As Color = Color.FromArgb(199, 199, 199)
        Me.BackColor = bgColor
        pnCover.BackColor = bgColor
        CaptureDefaultValues()
        pnCover.BringToFront()
        lblOpen.Parent = Me
        lblOpen.BackColor = Color.Transparent
        lblOpen.BringToFront()
        UpdateToolMenus()
        If My.Settings.isAutoGenerationEnabled = True Then
            AddValueChangedHandlers(Me)
            ListBoxChanged(Me)
        End If
        If My.Settings.isToolTipsEnabled = False Then
            DisableAllToolTips(Me)
        Else
            tooltips.SetAllTooltips()
        End If
    End Sub
    Private Sub UpdateComboBoxValues()
        cmbDisabled.SelectedIndex = 0
        cmbCompatibleWith.SelectedIndex = 1
        cmbUpdateThis.SelectedIndex = 0
        cmbVictimSocialClassRange.SelectedIndex = 0
        cmbSniperVantage.SelectedIndex = 0
        cmbBlockWeaponDrops.SelectedIndex = 0
        cmbMurdererClassRange.SelectedIndex = 0
        cmbUseHexaco.SelectedIndex = 0
    End Sub
    Private Sub MergeJson(ByRef defaultJson As JObject, ByVal loadedJsonData As JObject)
        For Each defaultProperty As JProperty In defaultJson.Properties()
            Dim loadedProperty As JToken = loadedJsonData(defaultProperty.Name)

            ' If the property exists in loadedJson, overwrite defaultJson with loadedJson value
            If loadedProperty IsNot Nothing Then
                defaultProperty.Value = loadedProperty
            End If
        Next

        ' Now add any properties from defaultJson that do not exist in loadedJson
        For Each defaultProperty As JProperty In defaultJson.Properties()
            If loadedJsonData(defaultProperty.Name) Is Nothing Then
                loadedJsonData.Add(defaultProperty.Name, defaultProperty.Value)
            End If
        Next
    End Sub
    Private Sub LoadJsonData(ByVal filePath As String)
        Try
            ClearSpecificTabs()
            Dim jsonContent As String = File.ReadAllText(filePath)
            Dim jsonData As JObject = JObject.Parse(jsonContent)
            ' =============================
            ' Load Main And Singular Values
            ' =============================
            txtPresetName.Text = If(jsonData("presetName") IsNot Nothing AndAlso Not String.IsNullOrEmpty(jsonData("presetName").ToString()),
            jsonData("presetName").ToString(),
            If(jsonData("name") IsNot Nothing AndAlso Not String.IsNullOrEmpty(jsonData("name").ToString()),
            jsonData("name").ToString(),
            String.Empty))

            txtNotes.Text = If(jsonData("notes") IsNot Nothing, jsonData("notes").ToString(), String.Empty)

            cmbDisabled.SelectedItem = If(jsonData("disabled") IsNot Nothing, If(ConvertToBool(jsonData("disabled")), "true", "false"), "false")

            lstCompatibleWith.Items.Clear()
            If jsonData("compatibleWith") IsNot Nothing AndAlso jsonData("compatibleWith").Type = JTokenType.Array AndAlso jsonData("compatibleWith").HasValues Then
                For Each compatibleWith In jsonData("compatibleWith")
                    If TypeOf compatibleWith Is JValue Then
                        Dim compatibleWithString As String = compatibleWith.ToString()
                        If compatibleWithString.StartsWith("REF:MurderPreset|") Then
                            compatibleWithString = compatibleWithString.Replace("REF:MurderPreset|", "")
                            lstCompatibleWith.Items.Add(compatibleWithString.ToString())
                            If Not cmbCompatibleWith.Items.Contains(compatibleWithString) Then
                                cmbCompatibleWith.Items.Add(compatibleWithString)
                            End If
                            cmbCompatibleWith.SelectedIndex = 0
                        End If
                    End If
                Next
            End If

            If jsonData("copyFrom") IsNot Nothing AndAlso jsonData("copyFrom").Type <> JTokenType.Null Then
                Dim copyFromString = jsonData("copyFrom").ToString()
                If copyFromString.StartsWith("REF:MurderMO|") Then
                    copyFromString = copyFromString.Replace("REF:MurderMO|", "")
                    cmbCopyFromMain.SelectedItem = copyFromString
                End If
                ' If the item is not in the ComboBox, add it and set the Text
                If cmbCopyFromMain.SelectedItem Is Nothing Then
                    cmbCopyFromMain.Text = jsonData("copyFrom").ToString()
                    cmbCopyFromMain.Items.Add(jsonData("copyFrom").ToString())
                End If
            Else
                ' If jsonData("copyFrom") is null or JTokenType.Null, set the selected index to 0 ("null" option)
                cmbCopyFromMain.SelectedIndex = 0
            End If

            nudBaseDifficulty.Value = If(jsonData("baseDifficulty") IsNot Nothing, CInt(jsonData("baseDifficulty").ToObject(Of Decimal)()), 0)
            nudMaxPotScore.Value = If(jsonData("maximumPotentialScore") IsNot Nothing, CInt(jsonData("maximumPotentialScore").ToObject(Of Decimal)()), 0)
            cmbUpdateThis.SelectedItem = If(jsonData("updateThis") IsNot Nothing, If(ConvertToBool(jsonData("updateThis")), "true", "false"), "false")
            nudRndScoreX.Value = If(jsonData("pickRandomScoreRange")?("x") IsNot Nothing, CInt(jsonData("pickRandomScoreRange")("x").ToObject(Of Decimal)()), 0)
            nudRndScoreY.Value = If(jsonData("pickRandomScoreRange")?("y") IsNot Nothing, CInt(jsonData("pickRandomScoreRange")("y").ToObject(Of Decimal)()), 0)
            cmbMurdererClassRange.SelectedItem = If(jsonData("useMurdererSocialClassRange") IsNot Nothing, If(ConvertToBool(jsonData("useMurdererSocialClassRange")), "true", "false"), "false")
            cmbSniperVantage.SelectedItem = If(jsonData("requiresSniperVantageAtHome") IsNot Nothing, If(ConvertToBool(jsonData("requiresSniperVantageAtHome")), "true", "false"), "false")
            cmbBlockWeaponDrops.SelectedItem = If(jsonData("blockDroppingWeapons") IsNot Nothing, If(ConvertToBool(jsonData("blockDroppingWeapons")), "true", "false"), "false")
            cmbUseHexaco.SelectedItem = If(jsonData("useHexaco") IsNot Nothing, If(ConvertToBool(jsonData("useHexaco")), "true", "false"), "false")

            nudMurdererCrX.Value = If(jsonData("murdererClassRange")?("x") IsNot Nothing, CInt(jsonData("murdererClassRange")("x").ToObject(Of Decimal)()), 0)
            nudMurdererCrY.Value = If(jsonData("murdererClassRange")?("y") IsNot Nothing, CInt(jsonData("murdererClassRange")("y").ToObject(Of Decimal)()), 0)
            nudMurdererCRBoost.Value = If(jsonData("murdererClassRangeBoost") IsNot Nothing, CInt(jsonData("murdererClassRangeBoost").ToObject(Of Decimal)()), 0)
            nudAcquaintSuitBoost.Value = If(jsonData("acquaintedSuitabilityBoost") IsNot Nothing, CInt(jsonData("acquaintedSuitabilityBoost").ToObject(Of Decimal)()), 0)
            nudAttractSuitBoost.Value = If(jsonData("attractedToSuitabilityBoost") IsNot Nothing, CInt(jsonData("attractedToSuitabilityBoost").ToObject(Of Decimal)()), 0)
            nudLikeSuitBoost.Value = If(jsonData("likeSuitabilityBoost") IsNot Nothing, CInt(jsonData("likeSuitabilityBoost").ToObject(Of Decimal)()), 0)
            nudSameWork.Value = If(jsonData("sameWorkplaceBoost") IsNot Nothing, CInt(jsonData("sameWorkplaceBoost").ToObject(Of Decimal)()), 0)
            nudMurdererIsTenantBoost.Value = If(jsonData("murdererIsTenantBoost") IsNot Nothing, CInt(jsonData("murdererIsTenantBoost").ToObject(Of Decimal)()), 0)

            nudVictimRndCrX.Value = If(jsonData("victimRandomScoreRange")?("x") IsNot Nothing, CInt(jsonData("victimRandomScoreRange")("x").ToObject(Of Decimal)()), 0)
            nudVictimRndCrY.Value = If(jsonData("victimRandomScoreRange")?("y") IsNot Nothing, CInt(jsonData("victimRandomScoreRange")("y").ToObject(Of Decimal)()), 0)
            cmbVictimSocialClassRange.SelectedItem = If(jsonData("useVictimSocialClassRange") IsNot Nothing, If(ConvertToBool(jsonData("useVictimSocialClassRange")), "true", "false"), "false")
            nudVictimCrX.Value = If(jsonData("victimClassRange")?("x") IsNot Nothing, CInt(jsonData("victimClassRange")("x").ToObject(Of Decimal)()), 0)
            nudVictimCrY.Value = If(jsonData("victimClassRange")?("y") IsNot Nothing, CInt(jsonData("victimClassRange")("y").ToObject(Of Decimal)()), 0)
            nudVictimCRBoost.Value = If(jsonData("victimClassRangeBoost") IsNot Nothing, CInt(jsonData("victimClassRangeBoost").ToObject(Of Decimal)()), 0)

            ' ===================
            ' Load Hexaco tab
            ' ===================
            hexacoTabOpen = False
            Dim tpHexaco As TabPage = tabControlCase.TabPages.OfType(Of TabPage)().FirstOrDefault(Function(tp) tp.Text.StartsWith("Hexaco Modifiers"))
            If tpHexaco Is Nothing AndAlso jsonData.ContainsKey("hexaco") AndAlso cmbUseHexaco.SelectedItem = "true" Then
                tpHexaco = CreateHexacoTab()
                tabControlCase.TabPages.Add(tpHexaco)
                Dim hexacoData As JObject = jsonData("hexaco")
                LoadHexacoData(tpHexaco, hexacoData)
            End If
            ' ===================
            ' Load Locations
            ' ===================
            CType(tabControlCase.TabPages(0).Controls("cmbAnywhere"), ComboBox).SelectedItem = If(ConvertToBool(jsonData("allowAnywhere")), "true", "false")
            CType(tabControlCase.TabPages(0).Controls("cmbHome"), ComboBox).SelectedItem = If(ConvertToBool(jsonData("allowHome")), "true", "false")
            CType(tabControlCase.TabPages(0).Controls("cmbWork"), ComboBox).SelectedItem = If(ConvertToBool(jsonData("allowWork")), "true", "false")
            CType(tabControlCase.TabPages(0).Controls("cmbPublic"), ComboBox).SelectedItem = If(ConvertToBool(jsonData("allowPublic")), "true", "false")
            CType(tabControlCase.TabPages(0).Controls("cmbStreets"), ComboBox).SelectedItem = If(ConvertToBool(jsonData("allowStreets")), "true", "false")
            CType(tabControlCase.TabPages(0).Controls("cmbDen"), ComboBox).SelectedItem = If(ConvertToBool(jsonData("allowDen")), "true", "false")
            ' ===================
            ' Load Weapon tabs
            ' ===================
            weaponTabOpen = False
            If jsonData("weaponsPool") IsNot Nothing Then
                Dim weaponsData As JArray = jsonData("weaponsPool")
                If weaponsData.Count >= 1 Then
                    Dim tpWeapon As TabPage = tabControlCase.TabPages.OfType(Of TabPage)().FirstOrDefault(Function(tp) tp.Text.StartsWith("Weapon Entry"))
                    If tpWeapon Is Nothing Then
                        tpWeapon = CreateWeaponsTab()
                        tabControlCase.TabPages.Add(tpWeapon)
                    End If

                    Dim lstWeapons As ListBox = CType(tpWeapon.Controls.OfType(Of ListBox).FirstOrDefault(Function(x) x.Name = "lstWeapons"), ListBox)

                    lstWeapons.Items.Clear()

                    For Each weaponEntry In jsonData("weaponsPool")
                        If TypeOf weaponEntry Is JValue Then
                            Dim weaponName As String = weaponEntry.ToString()
                            If weaponName.StartsWith("REF:MurderWeaponsPool|") Then
                                weaponName = weaponName.Replace("REF:MurderWeaponsPool|", "")
                                lstWeapons.Items.Add(weaponName)
                            End If
                        End If
                    Next
                End If
            End If
            ' ===========================
            ' Load Murderer Trait Modifiers
            ' ===========================
            murdererTraitEntryCount = 0
            For Each traitModifierEntry In jsonData("murdererTraitModifiers")
                Dim tpTraitModifiers As TabPage = CreateMurdererTraitModifiersTab()
                LoadMurdererTraitModifiersTab(traitModifierEntry, tpTraitModifiers)

                tabControlCase.TabPages.Add(tpTraitModifiers)
            Next
            ' ===========================
            ' Load Murderer Job Modifiers
            ' ===========================
            murdererJobModifierEntryCount = 0
            For Each jobModifierEntry In jsonData("murdererJobModifiers")
                Dim tpJobModifier As TabPage = CreateMurdererJobModifierTab() ' Create a new Job Modifier tab
                LoadMurdererJobModifierTab(jobModifierEntry, tpJobModifier) ' Load the data into the tab

                tabControlCase.TabPages.Add(tpJobModifier) ' Add the tab to the control
            Next
            ' ===========================
            ' Load Murderer Company Modifiers
            ' ===========================
            murdererCompanyModifierEntryCount = 0
            For Each companyModifierEntry In jsonData("murdererCompanyModifiers")
                Dim tpCompanyModifier As TabPage = CreateMurdererCompanyModifierTab() ' Create a new Job Modifier tab
                LoadMurdererCompanyModifierTab(companyModifierEntry, tpCompanyModifier) ' Load the data into the tab

                tabControlCase.TabPages.Add(tpCompanyModifier) ' Add the tab to the control
            Next
            ' ===========================
            ' Load Victim Trait Modifiers
            ' ===========================
            victimTraitEntryCount = 0
            For Each victimTraitModifierEntry In jsonData("victimTraitModifiers")
                Dim tpVictimTraitModifiers As TabPage = CreateVictimTraitModifiersTab()
                LoadVictimTraitModifiersTab(victimTraitModifierEntry, tpVictimTraitModifiers)

                tabControlCase.TabPages.Add(tpVictimTraitModifiers)
            Next
            ' ===========================
            ' Load Victim Job Modifiers
            ' ===========================
            victimJobModifierEntryCount = 0
            For Each victimJobModifierEntry In jsonData("victimJobModifiers")
                Dim tpVictimJobModifier As TabPage = CreateVictimJobModifierTab() ' Create a new Job Modifier tab
                LoadVictimJobModifierTab(victimJobModifierEntry, tpVictimJobModifier) ' Load the data into the tab

                tabControlCase.TabPages.Add(tpVictimJobModifier) ' Add the tab to the control
            Next
            ' ===========================
            ' Load Victim Company Modifiers
            ' ===========================
            victimCompanyModifierEntryCount = 0
            For Each VictimCompanyModifierEntry In jsonData("victimCompanyModifiers")
                Dim tpVictimCompanyModifier As TabPage = CreateVictimCompanyModifierTab() ' Create a new Job Modifier tab
                LoadVictimCompanyModifierTab(VictimCompanyModifierEntry, tpVictimCompanyModifier) ' Load the data into the tab

                tabControlCase.TabPages.Add(tpVictimCompanyModifier) ' Add the tab to the control
            Next
            ' ===========================
            ' Load DDS Confessionals
            ' ===========================
            ddsConfessionalTabOpen = False
            Dim ddsConfData As JArray = jsonData("confessionalDDSResponses")
            If ddsConfData.Count >= 1 Then
                Dim tpDDSConfessional As TabPage = CreateConfessionalDDSTab()
                LoadDDSConfessionalTab(jsonData, tpDDSConfessional)
                tabControlCase.TabPages.Add(tpDDSConfessional)
            End If
            ' ===================
            ' Load MOlead tabs
            ' ===================
            moleadEntryCount = 0
            setTabValueMO = 0
            Dim traitMOLeadModifierTabCount As Integer = 0
            For Each moLeadEntry In jsonData("MOleads")
                setTabValueMO += 1
                Dim tpMOlead As TabPage = CreateMOleadTab(traitMOLeadModifierTabCount)
                LoadMOleadTab(moLeadEntry, tpMOlead)

                tabControlCase.TabPages.Add(tpMOlead)

                Dim pnLeads As Panel = CType(tpMOlead.Controls("pnLeads" & moleadEntryCount), Panel)
                If pnLeads IsNot Nothing Then
                    Dim innerTabControlMO As TabControl = CType(pnLeads.Controls("innerTabControlMO" & moleadEntryCount), TabControl)
                    If innerTabControlMO IsNot Nothing Then
                        Dim traitModifiers As JArray = CType(moLeadEntry("traitModifiers"), JArray)
                        If traitModifiers IsNot Nothing Then
                            For Each traitModifier In traitModifiers
                                Dim newTab As TabPage = CreateMOLeadTraitModifierTab(traitMOLeadModifierTabCount, setTabValueMO)
                                innerTabControlMO.TabPages.Add(newTab)
                                LoadMOLeadTraitModifiersTab(traitModifier, newTab)
                            Next
                        End If
                    End If
                End If
            Next
            ' ==========================
            ' Load Graffiti tabs
            ' ==========================
            graffitiEntryCount = 0
            For Each graffitiEntry In jsonData("graffiti")
                Dim tpGraffiti As TabPage = CreateGraffitiTab()
                LoadGraffitiTab(graffitiEntry, tpGraffiti)

                tabControlCase.TabPages.Add(tpGraffiti)
            Next
            ' ==========================
            ' Load Calling Cards tabs
            ' ==========================
            callingcardEntryCount = 0
            setTabValueCC = 0
            Dim traitModifierTabCount As Integer = 0
            For Each cardEntry In jsonData("callingCardPool")
                setTabValueCC += 1
                Dim tpCallingCard As TabPage = CreateCallingCardTab(traitModifierTabCount)
                LoadCallingCardTab(cardEntry, tpCallingCard)

                tabControlCase.TabPages.Add(tpCallingCard)


                Dim innerTabControl As TabControl = CType(tpCallingCard.Controls("innerTabControl" & callingcardEntryCount), TabControl)

                If innerTabControl IsNot Nothing Then
                    ' Load traitModifiers or other relevant nested values
                    Dim traitModifiers As JArray = CType(cardEntry("traitModifiers"), JArray)

                    For Each traitModifier In traitModifiers
                        Dim newTab As TabPage = CreateTraitModifierTab(traitModifierTabCount, setTabValueCC)
                        innerTabControl.TabPages.Add(newTab)
                        LoadTraitModifierTab(traitModifier, newTab)
                    Next
                End If
            Next
            ' ===========================
            ' Load Player Taunts
            ' ===========================
            playerTauntTabOpen = False
            Dim tauntData As JArray = jsonData("playerTaunts")
            If tauntData.Count >= 1 Then
                Dim tpPlayerTaunts As TabPage = CreatePlayerTauntTab()
                LoadPlayerTauntTab(jsonData, tpPlayerTaunts)
                tabControlCase.TabPages.Add(tpPlayerTaunts)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading JSON: " & ex.Message)
        End Try
    End Sub
    Private Sub ClearSpecificTabs()
        Dim tabsToRemove As New List(Of TabPage)

        ' Loop through the TabControl and find the tabs to remove (all except "Locations")
        For Each tab As TabPage In tabControlCase.TabPages
            If tab.Text <> "Locations" Then
                tabsToRemove.Add(tab)
            End If
        Next

        ' Remove the selected tabs
        For Each tab As TabPage In tabsToRemove
            tabControlCase.TabPages.Remove(tab)
        Next
    End Sub
    Private Sub SetComboBoxText(cb As ComboBox, jsonValue As JToken)
        If cb Is Nothing Then
            MessageBox.Show("Can't find ComboBox for " & jsonValue.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit the method if cb is Nothing
        End If

        If jsonValue IsNot Nothing AndAlso Not jsonValue.Type = JTokenType.Null Then
            Dim value As String = jsonValue.ToString()
            If cb.Items.Contains(value) Then
                cb.SelectedItem = value
            Else
                cb.Items.Add(value)
                cb.SelectedItem = value
            End If
        Else
            If cb.Items.Count > 0 Then
                cb.SelectedIndex = 0
            End If
        End If
    End Sub
    Private Sub LoadCallingCardTab(jsonData As JObject, tabPage As TabPage)
        ' Ensure the tab has the correct name
        Dim entryIndex As Integer = Integer.Parse(tabPage.Text.Split(" "c).Last()) ' Extract the index from the tab name
        tabPage.Text = "Calling Card Entry " & entryIndex


        Dim cbItem As ComboBox = CType(tabPage.Controls("cbItem"), ComboBox)

        If jsonData("item") IsNot Nothing Then
            Dim itemValue As String = jsonData("item").ToString()
            If itemValue.StartsWith("REF:InteractablePreset|") Then
                itemValue = itemValue.Replace("REF:InteractablePreset|", "")
                If Not cbItem.Items.Contains(itemValue) Then
                    cbItem.Items.Add(itemValue)
                    cbItem.SelectedItem = itemValue
                Else
                    SetComboBoxText(cbItem, itemValue)
                End If
            ElseIf itemValue = "" Or JTokenType.Null Or Nothing Then
                cbItem.SelectedIndex = 0
            End If
        Else
            cbItem.SelectedIndex = 0
        End If

        ' Origin ComboBox
        Dim cbOrigin As ComboBox = CType(tabPage.Controls("cbOrigin"), ComboBox)
        SetComboBoxValue(cbOrigin, jsonData("origin"))

        ' Random Score Range X
        Dim txtRandomScoreX As TextBox = CType(tabPage.Controls("txtRandomScoreX"), TextBox)
        txtRandomScoreX.Text = If(jsonData("randomScoreRange")?("x")?.ToString(), String.Empty)

        ' Random Score Range Y
        Dim txtRandomScoreY As TextBox = CType(tabPage.Controls("txtRandomScoreY"), TextBox)
        txtRandomScoreY.Text = If(jsonData("randomScoreRange")?("y")?.ToString(), String.Empty)

        ' Copy From ComboBox (set text directly)
        Dim cbCopyFrom As ComboBox = CType(tabPage.Controls("cbCopyFrom"), ComboBox)
        ' Retrieve the original value and remove the prefix if it exists
        If jsonData("copyFrom") IsNot Nothing Then
            Dim copyFromValue As String = jsonData("copyFrom").ToString()
            If copyFromValue.StartsWith("REF:MurderMO|") Then
                copyFromValue = copyFromValue.Replace("REF:MurderMO|", "")
                If Not cbCopyFrom.Items.Contains(copyFromValue) Then
                    cbCopyFrom.Items.Add(copyFromValue)
                    cbCopyFrom.SelectedItem = copyFromValue
                Else
                    SetComboBoxText(cbCopyFrom, copyFromValue)
                End If
            ElseIf copyFromValue = "" Or JTokenType.Null Or Nothing Then
                cbCopyFrom.SelectedIndex = 0
            End If
        Else
            cbCopyFrom.SelectedIndex = 0
        End If
    End Sub
    Private Function CreateCallingCardTab(traitModifierTabCount) As TabPage
        callingcardEntryCount += 1
        Dim thisCCValue = setTabValueCC
        Dim tpCallingCard As New TabPage
        tpCallingCard.Text = "Calling Card Entry " & callingcardEntryCount

        ' Add controls for each input (similar to your original code)
        ' Item Label and ComboBox
        Dim itemLabel As New Label
        itemLabel.Text = "Item"
        itemLabel.Top = 23
        itemLabel.Left = 0
        itemLabel.Width = 100
        tpCallingCard.Controls.Add(itemLabel)

        Dim cbItem As New ComboBox
        cbItem.Name = "cbItem"
        cbItem.Top = 20
        cbItem.Left = 150
        cbItem.Width = 200
        cbItem.Items.Add("null")
        Dim itemLines As String() = My.Resources.interactablepreset.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLines
            cbItem.Items.Add(item.Trim())
        Next
        cbItem.DropDownStyle = ComboBoxStyle.DropDown
        cbItem.SelectedIndex = 0
        tpCallingCard.Controls.Add(cbItem)

        ' Origin Label and ComboBox
        Dim originLabel As New Label
        originLabel.Text = "Origin"
        originLabel.Top = 53
        originLabel.Left = 0
        originLabel.Width = 100
        tpCallingCard.Controls.Add(originLabel)

        Dim cbOrigin As New ComboBox
        cbOrigin.Name = "cbOrigin"
        cbOrigin.Top = 50
        cbOrigin.Left = 150
        cbOrigin.Width = 200
        cbOrigin.Items.AddRange(New String() {
        "createAtScene",
        "createOnGoToLocation"
    })
        cbOrigin.DropDownStyle = ComboBoxStyle.DropDownList
        cbOrigin.SelectedIndex = 0
        tpCallingCard.Controls.Add(cbOrigin)

        ' Random Score Range X and Y
        Dim randomScoreXLabel As New Label
        randomScoreXLabel.Text = "Random Score X/Y"
        randomScoreXLabel.Top = 83
        randomScoreXLabel.Left = 0
        randomScoreXLabel.Width = 150
        tpCallingCard.Controls.Add(randomScoreXLabel)

        Dim txtRandomScoreX As New TextBox
        txtRandomScoreX.Name = "txtRandomScoreX"
        txtRandomScoreX.Text = "0"
        txtRandomScoreX.Top = 80
        txtRandomScoreX.Left = 150
        txtRandomScoreX.Width = 20
        tpCallingCard.Controls.Add(txtRandomScoreX)

        Dim txtRandomScoreY As New TextBox
        txtRandomScoreY.Name = "txtRandomScoreY"
        txtRandomScoreY.Text = "1"
        txtRandomScoreY.Top = 80
        txtRandomScoreY.Left = 180
        txtRandomScoreY.Width = 20
        tpCallingCard.Controls.Add(txtRandomScoreY)

        ' Copy From Label and ComboBox
        Dim copyFromLabel As New Label
        copyFromLabel.Text = "Copy From"
        copyFromLabel.Top = 113
        copyFromLabel.Left = 0
        copyFromLabel.Width = 100
        tpCallingCard.Controls.Add(copyFromLabel)

        Dim cbCopyFrom As New ComboBox
        cbCopyFrom.Name = "cbCopyFrom"
        cbCopyFrom.Top = 110
        cbCopyFrom.Left = 150
        cbCopyFrom.Width = 200
        Dim itemLinesCopy As String() = My.Resources.murdermo.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLinesCopy
            cbCopyFrom.Items.Add(item.Trim())
        Next
        cbCopyFrom.DropDownStyle = ComboBoxStyle.DropDown
        cbCopyFrom.SelectedIndex = 0
        tpCallingCard.Controls.Add(cbCopyFrom)

        ' Add Remove Button
        Dim btnRemove As New Button
        btnRemove.Text = "Remove"
        btnRemove.Top = 20
        btnRemove.Left = 355
        AddHandler btnRemove.Click, Sub(s, ev)
                                        callingcardEntryCount -= 1
                                        Dim currentIndex As Integer = tabControlCase.TabPages.IndexOf(tpCallingCard)
                                        tabControlCase.TabPages.Remove(tpCallingCard)
                                        If currentIndex < tabControlCase.TabPages.Count Then
                                            tabControlCase.SelectedIndex = currentIndex
                                        ElseIf currentIndex > 0 Then
                                            tabControlCase.SelectedIndex = currentIndex - 1
                                        End If
                                    End Sub
        tpCallingCard.Controls.Add(btnRemove)


        Dim innerTabControl As New TabControl()
        innerTabControl.Name = "innerTabControl" & callingcardEntryCount
        innerTabControl.Top = 180
        innerTabControl.Left = 20
        innerTabControl.Width = 400
        innerTabControl.Height = 400
        tpCallingCard.Controls.Add(innerTabControl)

        Dim traitTabCount As Integer = traitModifierTabCount

        Dim btnAddTraitModifier As New Button
        btnAddTraitModifier.Text = "Add Trait Modifier"
        btnAddTraitModifier.Top = 140
        btnAddTraitModifier.Left = 150
        btnAddTraitModifier.Width = 200
        AddHandler btnAddTraitModifier.Click, Sub(s, ev)
                                                  traitTabCount += 1
                                                  Dim tpTraitModifier As TabPage = CreateTraitModifierTab(traitTabCount, thisCCValue)
                                                  innerTabControl.TabPages.Add(tpTraitModifier)
                                              End Sub

        tpCallingCard.Controls.Add(btnAddTraitModifier) ' Add the button to the tab page

        Return tpCallingCard
    End Function
    Private Function CreateTraitModifierTab(traitModifierTabCount, thisCCValue) As TabPage
        ' Define the dropdown options for "rule" and "mustPassForApplication"
        Dim ruleOptions As String() = {"ifAnyOfThese", "ifAllOfThese", "ifNoneOfThese", "ifPartnerAnyOfThese"}
        Dim booleanOptions As String() = {"false", "true"}

        ' Create a new TabPage for Trait Modifier
        Dim newTab As New TabPage
        newTab.Text = "Trait Modifier Tab " & traitModifierTabCount
        newTab.Name = "traitModifierTab" & traitModifierTabCount

        Dim innerTabControl As TabControl = FindControlRecursive(Of TabControl)(tabControlCase, Function(tc) tc.Name.Equals("innerTabControl" & thisCCValue))

        ' Rule ComboBox (Dropdown)
        Dim cbRule As New ComboBox
        cbRule.Name = "cbRule"
        cbRule.Top = 20
        cbRule.Left = 20
        cbRule.Width = 200
        cbRule.Items.AddRange(ruleOptions)
        cbRule.DropDownStyle = ComboBoxStyle.DropDownList
        cbRule.SelectedItem = "ifAnyOfThese"
        newTab.Controls.Add(New Label With {.Text = "Rule", .Top = 23, .Left = 230, .Width = 30})
        newTab.Controls.Add(cbRule)

        ' Trait List ComboBox and ListBox
        Dim cbTraitList As New ComboBox
        cbTraitList.Top = 60
        cbTraitList.Left = 20
        cbTraitList.Width = 200
        Dim itemLines As String() = My.Resources.charactertrait.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLines
            cbTraitList.Items.Add(item.Trim())
        Next
        cbTraitList.DropDownStyle = ComboBoxStyle.DropDown
        cbTraitList.SelectedIndex = 0
        newTab.Controls.Add(New Label With {.Text = "Trait List", .Top = 63, .Left = 230})
        newTab.Controls.Add(cbTraitList)

        ' ListBox to hold selected traits
        Dim lbTraitList As New ListBox
        lbTraitList.Name = "lbTraitList"
        lbTraitList.Top = 100
        lbTraitList.Left = 20
        lbTraitList.Width = 200
        newTab.Controls.Add(lbTraitList)

        ' Add Button for Trait List
        Dim btnAddTrait As New Button
        btnAddTrait.Text = "Add Trait"
        btnAddTrait.Top = 140
        btnAddTrait.Left = 230
        AddHandler btnAddTrait.Click, Sub()
                                          If cbTraitList.SelectedItem IsNot Nothing Then
                                              If Not lbTraitList.Items.Contains(cbTraitList.SelectedItem) Then
                                                  lbTraitList.Items.Add(cbTraitList.SelectedItem)
                                              End If
                                          End If
                                      End Sub
        newTab.Controls.Add(btnAddTrait)

        ' Remove Button for Trait List
        Dim btnRemoveTrait As New Button
        btnRemoveTrait.Text = "Remove Trait"
        btnRemoveTrait.Top = 165
        btnRemoveTrait.Left = 230
        AddHandler btnRemoveTrait.Click, Sub()
                                             If lbTraitList.SelectedItem IsNot Nothing Then
                                                 lbTraitList.Items.Remove(lbTraitList.SelectedItem)
                                             End If
                                         End Sub
        newTab.Controls.Add(btnRemoveTrait)

        ' mustPassForApplication ComboBox (True/False)
        Dim cbMustPassForApplication As New ComboBox
        cbMustPassForApplication.Name = "cbMustPassForApplication"
        cbMustPassForApplication.Top = 215
        cbMustPassForApplication.Left = 20
        cbMustPassForApplication.Width = 200
        cbMustPassForApplication.Items.AddRange(booleanOptions)
        cbMustPassForApplication.DropDownStyle = ComboBoxStyle.DropDownList
        cbMustPassForApplication.SelectedItem = "false" ' Default value
        newTab.Controls.Add(New Label With {.Text = "Must Pass For Application", .Top = 218, .Left = 230, .Width = 250})
        newTab.Controls.Add(cbMustPassForApplication)

        ' Score Modifier NumericUpDown
        Dim nudScoreModifier As New NumericUpDown
        nudScoreModifier.Name = "nudScoreModifier"
        nudScoreModifier.Top = 245
        nudScoreModifier.Left = 20
        nudScoreModifier.Width = 200
        nudScoreModifier.Minimum = -1000
        nudScoreModifier.Maximum = 1000
        newTab.Controls.Add(New Label With {.Text = "Score Modifier", .Top = 248, .Left = 230})
        newTab.Controls.Add(nudScoreModifier)

        ' Copy From ComboBox (with null as the default value)
        Dim cbCopyFromTraitModifier As New ComboBox
        cbCopyFromTraitModifier.Name = "cbCopyFrom"
        cbCopyFromTraitModifier.Top = 275
        cbCopyFromTraitModifier.Left = 20
        cbCopyFromTraitModifier.Width = 200
        cbCopyFromTraitModifier.Items.Add("null")
        Dim itemLinesCopy As String() = My.Resources.murdermo.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLinesCopy
            cbCopyFromTraitModifier.Items.Add(item.Trim())
        Next
        cbCopyFromTraitModifier.DropDownStyle = ComboBoxStyle.DropDown
        cbCopyFromTraitModifier.SelectedItem = "null" ' Default value
        newTab.Controls.Add(New Label With {.Text = "Copy From", .Top = 278, .Left = 230})
        newTab.Controls.Add(cbCopyFromTraitModifier)

        ' Remove Modifier Button
        Dim btnRemoveTraitModifier As New Button
        btnRemoveTraitModifier.Text = "Remove Modifier"
        btnRemoveTraitModifier.Top = 20
        btnRemoveTraitModifier.Left = 310
        AddHandler btnRemoveTraitModifier.Click, Sub(f, rb)
                                                     traitModifierTabCount -= 1
                                                     Dim currentIndex As Integer = innerTabControl.TabPages.IndexOf(newTab)
                                                     innerTabControl.TabPages.Remove(newTab)
                                                     If currentIndex < innerTabControl.TabPages.Count Then
                                                         innerTabControl.SelectedIndex = currentIndex
                                                     ElseIf currentIndex > 0 Then
                                                         innerTabControl.SelectedIndex = currentIndex - 1
                                                     End If
                                                 End Sub
        newTab.Controls.Add(btnRemoveTraitModifier)

        If innerTabControl IsNot Nothing Then
            innerTabControl.SelectedTab = newTab
        End If
        Return newTab
    End Function
    Private Sub LoadTraitModifierTab(jsonData As JObject, tabPage As TabPage)
        ' Ensure the tab has the correct name
        Dim entryIndex As Integer = Integer.Parse(tabPage.Text.Split(" "c).Last()) ' Extract the index from the tab name
        tabPage.Text = "Trait Modifier Tab " & entryIndex

        Dim cbRule As ComboBox = CType(tabPage.Controls("cbRule"), ComboBox)
        Dim ruleValue = jsonData("rule")

        If TypeOf ruleValue Is JValue AndAlso ruleValue.Type = JTokenType.Integer Then
            Dim index As Integer = Convert.ToInt32(ruleValue)
            If index >= 0 AndAlso index < cbRule.Items.Count Then
                cbRule.SelectedIndex = index
            End If
        Else
            cbRule.SelectedItem = ruleValue?.ToString()
            cbRule.Items.Add(ruleValue)
        End If

        'traits
        Dim lstTraits As ListBox = CType(tabPage.Controls("lbTraitList"), ListBox)
        lstTraits.Items.Clear()

        For Each trait In jsonData("traitList")
            If TypeOf trait Is JValue Then
                Dim traitName As String = trait.ToString()

                ' Remove the prefix if it exists
                If traitName.StartsWith("REF:CharacterTrait|") Then
                    traitName = traitName.Replace("REF:CharacterTrait|", "") ' Remove "REF:" prefix
                End If

                lstTraits.Items.Add(traitName)
            End If
        Next

        ' Must Pass For Application ComboBox
        Dim cbMustPassForApplication As ComboBox = CType(tabPage.Controls("cbMustPassForApplication"), ComboBox)
        SetComboBoxValue(cbMustPassForApplication, jsonData("mustPassForApplication"))

        ' Score Modifier NumericUpDown
        Dim nudScoreModifier As NumericUpDown = CType(tabPage.Controls("nudScoreModifier"), NumericUpDown)
        nudScoreModifier.Value = If(jsonData("scoreModifier")?.ToObject(Of Decimal?), Nothing) ' Set value directly

        ' Copy From ComboBox (set text directly)
        Dim cbCopyFrom As ComboBox = CType(tabPage.Controls("cbCopyFrom"), ComboBox)
        ' Retrieve the original value and remove the prefix if it exists
        If jsonData("copyFrom") IsNot Nothing Then
            Dim copyFromValue As String = jsonData("copyFrom").ToString()
            If copyFromValue.StartsWith("REF:MurderMO|") Then
                copyFromValue = copyFromValue.Replace("REF:MurderMO|", "")
                If Not cbCopyFrom.Items.Contains(copyFromValue) Then
                    cbCopyFrom.Items.Add(copyFromValue)
                    cbCopyFrom.SelectedItem = copyFromValue
                Else
                    SetComboBoxText(cbCopyFrom, copyFromValue)
                End If
            ElseIf copyFromValue = "" Or JTokenType.Null Or Nothing Then
                cbCopyFrom.SelectedIndex = 0
            End If
        Else
            cbCopyFrom.SelectedIndex = 0
        End If
    End Sub
    Private Sub LoadMOleadTab(jsonData As JObject, tabPage As TabPage)
        Dim tpPanel As Panel = tabPage.Controls("pnLeads" & moleadEntryCount)
        ' Ensure the tab has the correct name
        Dim entryIndex As Integer = Integer.Parse(tabPage.Text.Split(" "c).Last()) ' Extract the index from the tab name
        tabPage.Text = "MOlead Entry " & entryIndex

        ' Name TextBox
        Dim txtName As TextBox = CType(tpPanel.Controls("txtName"), TextBox)
        txtName.Text = If(jsonData("name")?.ToString(), String.Empty)

        ' All Motives ComboBox
        Dim cbAllMotives As ComboBox = CType(tpPanel.Controls("cbAllMotives"), ComboBox)
        cbAllMotives.SelectedItem = If(ConvertToBool(jsonData("compatibleWithAllMotives")), "true", "false")

        ' UseTraits
        Dim cbUseTraits As ComboBox = CType(tpPanel.Controls("cbUseTraits"), ComboBox)
        cbUseTraits.SelectedItem = If(ConvertToBool(jsonData("useTraits")), "true", "false")

        ' UseIf
        Dim cbUseIf As ComboBox = CType(tpPanel.Controls("cbUseIf"), ComboBox)
        cbUseIf.SelectedItem = If(ConvertToBool(jsonData("useIf")), "true", "false")

        ' IfTag
        Dim cbIfTag As ComboBox = CType(tpPanel.Controls("cbIfTag"), ComboBox)
        SetComboBoxValue(cbIfTag, jsonData("ifTag"))

        ' UseOrGroup
        Dim cbUseOrGroup As ComboBox = CType(tpPanel.Controls("cbUseOrGroup"), ComboBox)
        cbUseOrGroup.SelectedItem = If(ConvertToBool(jsonData("useOrGroup")), "true", "false")

        ' OrGroup
        Dim cbOrGroup As ComboBox = CType(tpPanel.Controls("cbOrGroup"), ComboBox)
        SetComboBoxValue(cbOrGroup, jsonData("orGroup"))

        ' ChanceRatio TextBox
        Dim txtChanceRatio As TextBox = CType(tpPanel.Controls("txtChanceRatio"), TextBox)
        txtChanceRatio.Text = If(jsonData("chanceRatio")?.ToString(), String.Empty)

        ' Vmail Progress Threshold X
        Dim txtVmailProgressX As TextBox = CType(tpPanel.Controls("txtVmailProgressX"), TextBox)
        txtVmailProgressX.Text = If(jsonData("vmailProgressThreshold")?("x")?.ToString(), String.Empty)

        ' Vmail Progress Threshold X
        Dim txtVmailProgressY As TextBox = CType(tpPanel.Controls("txtVmailProgressY"), TextBox)
        txtVmailProgressY.Text = If(jsonData("vmailProgressThreshold")?("y")?.ToString(), String.Empty)

        ' ItemTag
        Dim cbItemTag As ComboBox = CType(tpPanel.Controls("cbItemTag"), ComboBox)
        SetComboBoxValue(cbItemTag, jsonData("itemTag"))

        ' Spawn Phase ComboBox
        Dim cbSpawnPhase As ComboBox = CType(tpPanel.Controls("cbSpawnPhase"), ComboBox)
        SetComboBoxValue(cbSpawnPhase, jsonData("spawnOnPhase"))

        ' Spawn Each Murder ComboBox
        Dim cbSpawnEachMurder As ComboBox = CType(tpPanel.Controls("cbSpawnEachMurder"), ComboBox)
        cbSpawnEachMurder.SelectedItem = If(ConvertToBool(jsonData("tryToSpawnWithEachNewMurder")), "true", "false")

        ' Belongs To ComboBox
        Dim cbBelongsTo As ComboBox = CType(tpPanel.Controls("cbBelongsTo"), ComboBox)
        SetComboBoxValue(cbBelongsTo, jsonData("belongsTo"))

        ' Chance TextBox
        Dim txtChance As TextBox = CType(tpPanel.Controls("txtChance"), TextBox)
        txtChance.Text = If(jsonData("chance")?.ToString(), String.Empty)

        ' Item ComboBox (set text directly)
        Dim cbSpawnItem As ComboBox = CType(tpPanel.Controls("cbSpawnItem"), ComboBox)
        ' Retrieve the original value and remove the prefix if it exists
        Dim cbSpawnItemValue As String = jsonData("spawnItem").ToString()
        If cbSpawnItemValue.StartsWith("REF:InteractablePreset|") Then
            cbSpawnItemValue = cbSpawnItemValue.Replace("REF:InteractablePreset|", "")
            If Not cbSpawnItem.Items.Contains(cbSpawnItemValue) Then
                cbSpawnItem.Items.Add(cbSpawnItemValue)
                cbSpawnItem.SelectedItem = cbSpawnItem
            Else
                SetComboBoxText(cbSpawnItem, cbSpawnItemValue)
            End If
        ElseIf cbSpawnItemValue = "" Or JTokenType.Null Or Nothing Then
            cbSpawnItem.SelectedIndex = 0
        End If

        ' VMail Thread TextBox
        Dim txtVmail As TextBox = CType(tpPanel.Controls("txtVmail"), TextBox)
        txtVmail.Text = If(jsonData("vmailThread")?.ToString(), String.Empty)

        ' Vmail Other List
        Dim lstVmailOther As ListBox = CType(tpPanel.Controls("lbVmailOthers"), ListBox)
        lstVmailOther.Items.Clear() ' Clear existing items

        For Each other As JToken In jsonData("vmailOtherParticipants")
            Dim otherMO As String = other.ToString()
            If otherMO.StartsWith("REF:MurderMO|") Then
                otherMO = otherMO.Replace("REF:MurderMO|", "")
            End If
            lstVmailOther.Items.Add(otherMO)
        Next

        ' Writer ComboBox
        Dim cbWriter As ComboBox = CType(tpPanel.Controls("cbWriter"), ComboBox)
        SetComboBoxValue(cbWriter, jsonData("writer"))

        ' Receiver ComboBox
        Dim cbReceiver As ComboBox = CType(tpPanel.Controls("cbReceiver"), ComboBox)
        SetComboBoxValue(cbReceiver, jsonData("receiver"))

        ' Where ComboBox
        Dim cbWhere As ComboBox = CType(tpPanel.Controls("cbWhere"), ComboBox)
        SetComboBoxValue(cbWhere, jsonData("where"))

        ' Security TextBox
        Dim txtSecurity As TextBox = CType(tpPanel.Controls("txtSecurity"), TextBox)
        txtSecurity.Text = If(jsonData("security")?.ToString(), String.Empty)

        ' Priority TextBox
        Dim txtPriority As TextBox = CType(tpPanel.Controls("txtPriority"), TextBox)
        txtPriority.Text = If(jsonData("priority")?.ToString(), String.Empty)

        ' Ownership Rule ComboBox
        Dim cbOwnershipRule As ComboBox = CType(tpPanel.Controls("cbOwnershipRule"), ComboBox)
        SetComboBoxValue(cbOwnershipRule, jsonData("ownershipRule"))

        ' Compatible With ListBox
        Dim lstMotives As ListBox = CType(tpPanel.Controls("lbMotives"), ListBox)
        lstMotives.Items.Clear() ' Clear existing items

        For Each motive As JToken In jsonData("compatibleWithMotives")
            Dim motiveString As String = motive.ToString()
            If motiveString.StartsWith("REF:MurderMO|") Then
                motiveString = motiveString.Replace("REF:MurderMO|", "")
            ElseIf motiveString.Contains("{") AndAlso motiveString.Contains("fileID") AndAlso motiveString.Contains("}") Then
                motiveString = motiveString.Replace("{", "").Replace("""fileID"":", "").Replace("}", "").Replace(" ", "")
                motiveString = CInt(motiveString)
            End If
            lstMotives.Items.Add(motiveString)
        Next
    End Sub
    Private Sub LoadMOLeadTraitModifiersTab(jsonData As JObject, tabPage As TabPage)
        Dim entryIndex As Integer = Integer.Parse(tabPage.Text.Split(" "c).Last())
        tabPage.Text = "MOlead Trait Modifier Tab " & entryIndex

        Dim cbWho As ComboBox = CType(tabPage.Controls("cbWho"), ComboBox)
        Dim whoVaue = jsonData("who")

        Dim cbRule As ComboBox = CType(tabPage.Controls("cbRule"), ComboBox)
        Dim ruleValue = jsonData("rule")

        If TypeOf ruleValue Is JValue AndAlso ruleValue.Type = JTokenType.Integer Then
            Dim index As Integer = Convert.ToInt32(ruleValue)
            If index >= 0 AndAlso index < cbRule.Items.Count Then
                cbRule.SelectedIndex = index
            End If
        Else
            cbRule.SelectedItem = ruleValue?.ToString()
            cbRule.Items.Add(ruleValue)
        End If

        If TypeOf whoVaue Is JValue AndAlso whoVaue.Type = JTokenType.Integer Then
            Dim index As Integer = Convert.ToInt32(whoVaue)
            If index >= 0 AndAlso index < cbWho.Items.Count Then
                cbWho.SelectedIndex = index
            End If
        Else
            cbWho.SelectedItem = cbWho?.ToString()
            cbWho.Items.Add(cbWho)
        End If

        Dim lstTraits As ListBox = CType(tabPage.Controls("lbTraitList"), ListBox)
        lstTraits.Items.Clear()

        For Each trait In jsonData("traitList")
            If TypeOf trait Is JValue Then
                Dim traitName As String = trait.ToString()

                ' Remove the prefix if it exists
                If traitName.StartsWith("REF:CharacterTrait|") Then
                    traitName = traitName.Replace("REF:CharacterTrait|", "") ' Remove "REF:" prefix
                End If

                lstTraits.Items.Add(traitName)
            End If
        Next

        Dim cbMustPass As ComboBox = CType(tabPage.Controls("cbMustPassForApplication"), ComboBox)
        Dim mustPassValue = jsonData("mustPassForApplication")
        Dim mustPassBool As Boolean = ConvertToBool(mustPassValue)
        cbMustPass.SelectedItem = If(mustPassBool, "true", "false")

        Dim numScoreModifier As NumericUpDown = CType(tabPage.Controls("nudScoreModifier"), NumericUpDown)
        numScoreModifier.Value = If(jsonData("chanceModifier") IsNot Nothing, CInt(jsonData("chanceModifier").ToObject(Of Decimal)()), 0)

        ' Copy From ComboBox (set text directly)
        Dim cbCopyFrom As ComboBox = CType(tabPage.Controls("cbCopyFrom"), ComboBox)
        ' Retrieve the original value and remove the prefix if it exists
        If jsonData("copyFrom") IsNot Nothing Then
            Dim copyFromValue As String = jsonData("copyFrom").ToString()
            If copyFromValue.StartsWith("REF:MurderMO|") Then
                copyFromValue = copyFromValue.Replace("REF:MurderMO|", "")
                If Not cbCopyFrom.Items.Contains(copyFromValue) Then
                    cbCopyFrom.Items.Add(copyFromValue)
                    cbCopyFrom.SelectedItem = copyFromValue
                Else
                    SetComboBoxText(cbCopyFrom, copyFromValue)
                End If
            ElseIf copyFromValue = "" Or JTokenType.Null Or Nothing Then
                cbCopyFrom.SelectedIndex = 0
            End If
        Else
            cbCopyFrom.SelectedIndex = 0
        End If
    End Sub
    Private Function CreateMOleadTab(traitMOLeadModifierTabCount) As TabPage
        moleadEntryCount += 1
        Dim thisTabValue As Integer = setTabValueMO
        Dim tpMOlead As New TabPage
        tpMOlead.Text = "MOlead Entry " & moleadEntryCount
        tpMOlead.Name = "tpMOlead"
        tpMOlead.Tag = "MOLEADNO:" & thisTabValue
        Dim alphabetList As Array = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

        ' Create a Panel
        Dim scrollablePanel As New Panel()
        scrollablePanel.Name = "pnLeads" & moleadEntryCount
        scrollablePanel.AutoScroll = True ' Enable scroll
        scrollablePanel.Size = New Size(460, 587) ' Set size
        scrollablePanel.Location = New Point(0, 0) ' Set location
        scrollablePanel.BackColor = SystemColors.HighlightText
        scrollablePanel.AutoScrollMargin = New Size(20, 20)
        tpMOlead.Controls.Add(scrollablePanel)


        ' Add controls for each input
        Dim nameLabel As New Label
        nameLabel.Text = "Name"
        nameLabel.Name = "lblName"
        nameLabel.Top = 23
        nameLabel.Left = 0
        nameLabel.Width = 100
        scrollablePanel.Controls.Add(nameLabel)

        Dim txtName As New TextBox
        txtName.PlaceholderText = "Lead Name"
        txtName.Top = 20
        txtName.Left = 150
        txtName.Width = 150
        txtName.Name = "txtName"
        scrollablePanel.Controls.Add(txtName)

        Dim allMotivesLabel As New Label
        allMotivesLabel.Text = "All Motives"
        allMotivesLabel.Top = 53
        allMotivesLabel.Left = 0
        allMotivesLabel.Width = 100
        scrollablePanel.Controls.Add(allMotivesLabel)

        Dim cbAllMotives As New ComboBox
        cbAllMotives.Top = 50
        cbAllMotives.Left = 150
        cbAllMotives.Width = 150
        cbAllMotives.Name = "cbAllMotives"
        cbAllMotives.Items.AddRange(New String() {"true", "false"})
        cbAllMotives.DropDownStyle = ComboBoxStyle.DropDownList
        cbAllMotives.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbAllMotives)

        Dim spawnPhaseLabel As New Label
        spawnPhaseLabel.Text = "Spawn Phase"
        spawnPhaseLabel.Top = 83
        spawnPhaseLabel.Left = 0
        spawnPhaseLabel.Width = 100
        scrollablePanel.Controls.Add(spawnPhaseLabel)

        Dim cbSpawnPhase As New ComboBox
        cbSpawnPhase.Top = 80
        cbSpawnPhase.Left = 150
        cbSpawnPhase.Width = 150
        cbSpawnPhase.Name = "cbSpawnPhase"
        cbSpawnPhase.Items.AddRange(New String() {"none", "aquireEquipment", "research", "waitForLocation", "travellingTo", "executing", "post", "escaping", "unsolved", "solved"})
        cbSpawnPhase.DropDownStyle = ComboBoxStyle.DropDownList
        cbSpawnPhase.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbSpawnPhase)

        Dim spawnEachMurderLabel As New Label
        spawnEachMurderLabel.Text = "Spawn Each Murder"
        spawnEachMurderLabel.Top = 113
        spawnEachMurderLabel.Left = 0
        spawnEachMurderLabel.Width = 150
        scrollablePanel.Controls.Add(spawnEachMurderLabel)

        Dim cbSpawnEachMurder As New ComboBox
        cbSpawnEachMurder.Top = 110
        cbSpawnEachMurder.Left = 150
        cbSpawnEachMurder.Width = 150
        cbSpawnEachMurder.Name = "cbSpawnEachMurder"
        cbSpawnEachMurder.Items.AddRange(New String() {"true", "false"})
        cbSpawnEachMurder.DropDownStyle = ComboBoxStyle.DropDownList
        cbSpawnEachMurder.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbSpawnEachMurder)

        Dim belongsToLabel As New Label
        belongsToLabel.Text = "Belongs To"
        belongsToLabel.Top = 143
        belongsToLabel.Left = 0
        belongsToLabel.Width = 150
        scrollablePanel.Controls.Add(belongsToLabel)

        Dim cbBelongsTo As New ComboBox
        cbBelongsTo.Top = 140
        cbBelongsTo.Left = 150
        cbBelongsTo.Width = 150
        cbBelongsTo.Name = "cbBelongsTo"
        cbBelongsTo.Items.AddRange(New String() {"nobody", "victim", "killer", "victimsClosest", "killersClosest", "victimsDoctor", "killersDoctor", "ransom", "victimsLandlord", "killersLandlord"})
        cbBelongsTo.DropDownStyle = ComboBoxStyle.DropDownList
        cbBelongsTo.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbBelongsTo)

        Dim chanceLabel As New Label
        chanceLabel.Text = "Chance (0.0 To 1.0)"
        chanceLabel.Top = 173
        chanceLabel.Left = 0
        chanceLabel.Width = 150
        scrollablePanel.Controls.Add(chanceLabel)

        Dim txtChance As New TextBox
        txtChance.Text = "1"
        txtChance.Top = 170
        txtChance.Left = 150
        txtChance.Width = 150
        txtChance.Name = "txtChance"
        scrollablePanel.Controls.Add(txtChance)

        Dim useTraitsLabel As New Label
        useTraitsLabel.Text = "Use Traits"
        useTraitsLabel.Top = 203
        useTraitsLabel.Left = 0
        useTraitsLabel.Width = 150
        scrollablePanel.Controls.Add(useTraitsLabel)

        Dim cbUseTraits As New ComboBox
        cbUseTraits.Top = 200
        cbUseTraits.Left = 150
        cbUseTraits.Width = 150
        cbUseTraits.Name = "cbUseTraits"
        cbUseTraits.Items.AddRange(New String() {"true", "false"})
        cbUseTraits.DropDownStyle = ComboBoxStyle.DropDownList
        cbUseTraits.SelectedIndex = 1
        scrollablePanel.Controls.Add(cbUseTraits)

        ' Create a Tab
        Dim innerTabControlMO As New TabControl()
        innerTabControlMO.Name = "innerTabControlMO" & moleadEntryCount
        innerTabControlMO.Top = 880
        innerTabControlMO.Left = 20
        innerTabControlMO.Width = 400
        innerTabControlMO.Height = 400
        scrollablePanel.Controls.Add(innerTabControlMO)

        Dim traitTabCount As Integer = traitMOLeadModifierTabCount

        Dim btnAddTraitModifier As New Button
        btnAddTraitModifier.Text = "Add Trait Modifier"
        btnAddTraitModifier.Top = 230
        btnAddTraitModifier.Left = 150
        btnAddTraitModifier.Width = 150
        btnAddTraitModifier.Name = "btnAddMotive"
        AddHandler btnAddTraitModifier.Click, Sub(sender, e)
                                                  traitTabCount += 1
                                                  Dim tpMOLeadTraitModifier As TabPage = CreateMOLeadTraitModifierTab(traitTabCount, thisTabValue)
                                                  innerTabControlMO.TabPages.Add(tpMOLeadTraitModifier)
                                              End Sub
        scrollablePanel.Controls.Add(btnAddTraitModifier)

        Dim useIfLabel As New Label
        useIfLabel.Text = "Use If"
        useIfLabel.Top = 263
        useIfLabel.Left = 0
        useIfLabel.Width = 150
        scrollablePanel.Controls.Add(useIfLabel)

        Dim cbUseIf As New ComboBox
        cbUseIf.Top = 260
        cbUseIf.Left = 150
        cbUseIf.Width = 150
        cbUseIf.Name = "cbUseIf"
        cbUseIf.Items.AddRange(New String() {"true", "false"})
        cbUseIf.DropDownStyle = ComboBoxStyle.DropDownList
        cbUseIf.SelectedIndex = 1
        scrollablePanel.Controls.Add(cbUseIf)

        Dim ifTagLabel As New Label
        ifTagLabel.Text = "If Tag"
        ifTagLabel.Top = 293
        ifTagLabel.Left = 0
        ifTagLabel.Width = 150
        scrollablePanel.Controls.Add(ifTagLabel)

        Dim cbIfTag As New ComboBox
        cbIfTag.Top = 293
        cbIfTag.Left = 150
        cbIfTag.Width = 150
        cbIfTag.Name = "cbIfTag"
        cbIfTag.Items.AddRange(alphabetList)
        cbIfTag.DropDownStyle = ComboBoxStyle.DropDownList
        cbIfTag.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbIfTag)

        Dim useOrGroupLabel As New Label
        useOrGroupLabel.Text = "Use Or Group"
        useOrGroupLabel.Top = 323
        useOrGroupLabel.Left = 0
        useOrGroupLabel.Width = 150
        scrollablePanel.Controls.Add(useOrGroupLabel)

        Dim cbUseOrGroup As New ComboBox
        cbUseOrGroup.Top = 320
        cbUseOrGroup.Left = 150
        cbUseOrGroup.Width = 150
        cbUseOrGroup.Name = "cbUseOrGroup"
        cbUseOrGroup.Items.AddRange(New String() {"true", "false"})
        cbUseOrGroup.DropDownStyle = ComboBoxStyle.DropDownList
        cbUseOrGroup.SelectedIndex = 1
        scrollablePanel.Controls.Add(cbUseOrGroup)

        Dim orGroupLabel As New Label
        orGroupLabel.Text = "Or Group"
        orGroupLabel.Top = 353
        orGroupLabel.Left = 0
        orGroupLabel.Width = 150
        scrollablePanel.Controls.Add(orGroupLabel)

        Dim cbOrGroup As New ComboBox
        cbOrGroup.Top = 350
        cbOrGroup.Left = 150
        cbOrGroup.Width = 150
        cbOrGroup.Name = "cbOrGroup"
        cbOrGroup.Items.AddRange(alphabetList)
        cbOrGroup.DropDownStyle = ComboBoxStyle.DropDownList
        cbOrGroup.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbOrGroup)

        Dim chanceRatioLabel As New Label
        chanceRatioLabel.Text = "Chance Ratio"
        chanceRatioLabel.Top = 383
        chanceRatioLabel.Left = 0
        chanceRatioLabel.Width = 150
        scrollablePanel.Controls.Add(chanceRatioLabel)

        Dim txtChanceRatio As New TextBox
        txtChanceRatio.Text = "0"
        txtChanceRatio.Top = 380
        txtChanceRatio.Left = 150
        txtChanceRatio.Width = 150
        txtChanceRatio.Name = "txtChanceRatio"
        scrollablePanel.Controls.Add(txtChanceRatio)

        Dim itemTagLabel As New Label
        itemTagLabel.Text = "Item Tag"
        itemTagLabel.Top = 413
        itemTagLabel.Left = 0
        itemTagLabel.Width = 150
        scrollablePanel.Controls.Add(itemTagLabel)

        Dim cbItemTag As New ComboBox
        cbItemTag.Top = 410
        cbItemTag.Left = 150
        cbItemTag.Width = 150
        cbItemTag.Name = "cbItemTag"
        cbItemTag.Items.AddRange(alphabetList)
        cbItemTag.DropDownStyle = ComboBoxStyle.DropDownList
        cbItemTag.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbItemTag)

        Dim spawnItemLabel As New Label
        spawnItemLabel.Text = "Spawn Item"
        spawnItemLabel.Top = 443
        spawnItemLabel.Left = 0
        spawnItemLabel.Width = 150
        scrollablePanel.Controls.Add(spawnItemLabel)

        Dim cbSpawnItem As New ComboBox
        cbSpawnItem.Top = 440
        cbSpawnItem.Left = 150
        cbSpawnItem.Width = 250
        cbSpawnItem.Name = "cbSpawnItem"
        cbSpawnItem.Items.Add("null")
        Dim itemLines As String() = My.Resources.interactablepreset.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLines
            cbSpawnItem.Items.Add(item.Trim())
        Next
        cbSpawnItem.DropDownStyle = ComboBoxStyle.DropDown
        cbSpawnItem.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbSpawnItem)

        Dim vmailThreadLabel As New Label
        vmailThreadLabel.Text = "VMail Thread (DDS String)"
        vmailThreadLabel.Top = 473
        vmailThreadLabel.Left = 0
        vmailThreadLabel.Width = 150
        scrollablePanel.Controls.Add(vmailThreadLabel)

        Dim txtVmail As New TextBox
        txtVmail.PlaceholderText = ""
        txtVmail.Top = 470
        txtVmail.Left = 150
        txtVmail.Width = 150
        txtVmail.Name = "txtVmail"
        scrollablePanel.Controls.Add(txtVmail)

        Dim vmailProgressLabel As New Label
        vmailProgressLabel.Text = "VMail Progress Threshold"
        vmailProgressLabel.Top = 503
        vmailProgressLabel.Left = 0
        vmailProgressLabel.Width = 150
        scrollablePanel.Controls.Add(vmailProgressLabel)

        Dim txtVmailProgressX As New TextBox
        txtVmailProgressX.Text = "0"
        txtVmailProgressX.Top = 500
        txtVmailProgressX.Left = 150
        txtVmailProgressX.Width = 30
        txtVmailProgressX.Name = "txtVmailProgressX"
        scrollablePanel.Controls.Add(txtVmailProgressX)

        Dim txtVmailProgressY As New TextBox
        txtVmailProgressY.Text = "1"
        txtVmailProgressY.Top = 500
        txtVmailProgressY.Left = 185
        txtVmailProgressY.Width = 30
        txtVmailProgressY.Name = "txtVmailProgressY"
        scrollablePanel.Controls.Add(txtVmailProgressY)

        Dim vmailWriterLabel As New Label
        vmailWriterLabel.Text = "Writer"
        vmailWriterLabel.Top = 533
        vmailWriterLabel.Left = 0
        vmailWriterLabel.Width = 150
        scrollablePanel.Controls.Add(vmailWriterLabel)

        Dim cbWriter As New ComboBox
        cbWriter.Top = 530
        cbWriter.Left = 150
        cbWriter.Width = 150
        cbWriter.Name = "cbWriter"
        cbWriter.Items.AddRange(New String() {"nobody", "victim", "killer", "victimsClosest", "killersClosest", "victimsDoctor", "killersDoctor", "ransom", "victimsLandlord", "killersLandlord"})
        cbWriter.DropDownStyle = ComboBoxStyle.DropDownList
        cbWriter.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbWriter)

        Dim vmailReceiverLabel As New Label
        vmailReceiverLabel.Text = "Receiver"
        vmailReceiverLabel.Top = 563
        vmailReceiverLabel.Left = 0
        vmailReceiverLabel.Width = 150
        scrollablePanel.Controls.Add(vmailReceiverLabel)

        Dim cbReceiver As New ComboBox
        cbReceiver.Top = 560
        cbReceiver.Left = 150
        cbReceiver.Width = 150
        cbReceiver.Name = "cbReceiver"
        cbReceiver.Items.AddRange(New String() {"nobody", "victim", "killer", "victimsClosest", "killersClosest", "victimsDoctor", "killersDoctor", "ransom", "victimsLandlord", "killersLandlord"})
        cbReceiver.DropDownStyle = ComboBoxStyle.DropDownList
        cbReceiver.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbReceiver)

        Dim vmailOthersLabel As New Label
        vmailOthersLabel.Text = "VMail Other Participants"
        vmailOthersLabel.Top = 593
        vmailOthersLabel.Left = 0
        vmailOthersLabel.Width = 150
        scrollablePanel.Controls.Add(vmailOthersLabel)

        Dim cbVmailOthers As New ComboBox
        cbVmailOthers.Top = 590
        cbVmailOthers.Left = 150
        cbVmailOthers.Width = 150
        cbVmailOthers.Name = "cbVmailOthers"
        cbVmailOthers.DropDownStyle = ComboBoxStyle.DropDown
        Dim itemLinesOthers As String() = My.Resources.murdermo.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLinesOthers
            cbVmailOthers.Items.Add(item.Trim())
        Next
        cbVmailOthers.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbVmailOthers)

        Dim lbVmailOthers As New ListBox
        lbVmailOthers.Top = 620
        lbVmailOthers.Left = 0
        lbVmailOthers.Width = 300
        lbVmailOthers.Height = 50
        lbVmailOthers.Name = "lbVmailOthers"
        scrollablePanel.Controls.Add(lbVmailOthers)

        Dim btnAddOther As New Button
        btnAddOther.Text = "Add Other"
        btnAddOther.Top = 620
        btnAddOther.Left = 315
        btnAddOther.Width = 70
        btnAddOther.Name = "btnAddOther"
        AddHandler btnAddOther.Click, Sub(sender, e)
                                          If Not String.IsNullOrWhiteSpace(cbVmailOthers.Text) Then
                                              lbVmailOthers.Items.Add(cbVmailOthers.Text)
                                          End If
                                      End Sub
        scrollablePanel.Controls.Add(btnAddOther)

        Dim btnRemoveOther As New Button
        btnRemoveOther.Text = "Remove Other"
        btnRemoveOther.Top = 645
        btnRemoveOther.Left = 315
        btnRemoveOther.Width = 70
        btnRemoveOther.Name = "btnRemoveOther"
        AddHandler btnRemoveOther.Click, Sub(sender, e)
                                             If lbVmailOthers.SelectedIndex <> -1 Then
                                                 lbVmailOthers.Items.RemoveAt(lbVmailOthers.SelectedIndex)
                                             End If
                                         End Sub
        scrollablePanel.Controls.Add(btnRemoveOther)

        Dim whereLabel As New Label
        whereLabel.Text = "Where"
        whereLabel.Top = 673
        whereLabel.Left = 0
        whereLabel.Width = 150
        scrollablePanel.Controls.Add(whereLabel)

        Dim cbWhere As New ComboBox
        cbWhere.Top = 670
        cbWhere.Left = 150
        cbWhere.Width = 150
        cbWhere.Name = "cbWhere"
        cbWhere.Items.AddRange(New String() {"victimHome", "victimWork", "killerHome", "killerWork", "ransom", "killerDen"})
        cbWhere.DropDownStyle = ComboBoxStyle.DropDownList
        cbWhere.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbWhere)

        Dim securityLabel As New Label
        securityLabel.Text = "Security"
        securityLabel.Top = 703
        securityLabel.Left = 0
        securityLabel.Width = 150
        scrollablePanel.Controls.Add(securityLabel)

        Dim txtSecurity As New TextBox
        txtSecurity.Text = "0"
        txtSecurity.Top = 700
        txtSecurity.Left = 150
        txtSecurity.Width = 150
        txtSecurity.Name = "txtSecurity"
        scrollablePanel.Controls.Add(txtSecurity)

        Dim priorityLabel As New Label
        priorityLabel.Text = "Priority"
        priorityLabel.Top = 733
        priorityLabel.Left = 0
        priorityLabel.Width = 150
        scrollablePanel.Controls.Add(priorityLabel)

        Dim txtPriority As New TextBox
        txtPriority.Text = "1"
        txtPriority.Top = 730
        txtPriority.Left = 150
        txtPriority.Width = 150
        txtPriority.Name = "txtPriority"
        scrollablePanel.Controls.Add(txtPriority)

        Dim ownershipRuleLabel As New Label
        ownershipRuleLabel.Text = "Ownership Rule"
        ownershipRuleLabel.Top = 763
        ownershipRuleLabel.Left = 0
        ownershipRuleLabel.Width = 150
        scrollablePanel.Controls.Add(ownershipRuleLabel)

        Dim cbOwnershipRule As New ComboBox
        cbOwnershipRule.Top = 760
        cbOwnershipRule.Left = 150
        cbOwnershipRule.Width = 150
        cbOwnershipRule.Name = "cbOwnershipRule"
        cbOwnershipRule.Items.AddRange(New String() {"nonOwnedOnly", "ownedOnly", "prioritiseNonOwend", "prioritiseOwend", "both"})
        cbOwnershipRule.DropDownStyle = ComboBoxStyle.DropDownList
        cbOwnershipRule.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbOwnershipRule)

        ' Add the Remove button
        Dim btnRemove As New Button
        btnRemove.Text = "Remove"
        btnRemove.Top = 20
        btnRemove.Left = 355
        AddHandler btnRemove.Click, Sub(s, ev)
                                        moleadEntryCount -= 1
                                        setTabValueMO -= 1
                                        If moleadEntryCount = 0 Then
                                            setTabValueMO = 0
                                        End If
                                        Dim currentIndex As Integer = tabControlCase.TabPages.IndexOf(tpMOlead)
                                        tabControlCase.TabPages.Remove(tpMOlead)
                                        If currentIndex < tabControlCase.TabPages.Count Then
                                            tabControlCase.SelectedIndex = currentIndex
                                        ElseIf currentIndex > 0 Then
                                            tabControlCase.SelectedIndex = currentIndex - 1
                                        End If
                                    End Sub
        scrollablePanel.Controls.Add(btnRemove)

        ' New ComboBox for "Compatible With Motives"
        Dim compatibleWithLabel As New Label
        compatibleWithLabel.Text = "Compatible With Motives"
        compatibleWithLabel.Top = 793
        compatibleWithLabel.Left = 0
        compatibleWithLabel.Width = 150
        scrollablePanel.Controls.Add(compatibleWithLabel)

        Dim cbCompatibleWith As New ComboBox
        cbCompatibleWith.Top = 790
        cbCompatibleWith.Left = 150
        cbCompatibleWith.Width = 150
        cbCompatibleWith.Name = "cbCompatibleWith"
        cbCompatibleWith.DropDownStyle = ComboBoxStyle.DropDown
        Dim itemLinesMO As String() = My.Resources.murdermo.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLinesMO
            cbCompatibleWith.Items.Add(item.Trim())
        Next
        cbCompatibleWith.SelectedIndex = 0
        scrollablePanel.Controls.Add(cbCompatibleWith)

        ' New ListBox for adding and removing items
        Dim lbMotives As New ListBox
        lbMotives.Top = 820
        lbMotives.Left = 0
        lbMotives.Width = 300
        lbMotives.Height = 50
        lbMotives.Name = "lbMotives"
        scrollablePanel.Controls.Add(lbMotives)

        Dim btnAddMotive As New Button
        btnAddMotive.Text = "Add Motive"
        btnAddMotive.Top = 820
        btnAddMotive.Left = 315
        btnAddMotive.Width = 70
        btnAddMotive.Name = "btnAddMotive"
        AddHandler btnAddMotive.Click, Sub(sender, e)
                                           If Not String.IsNullOrWhiteSpace(cbCompatibleWith.Text) Then
                                               lbMotives.Items.Add(cbCompatibleWith.Text)
                                           End If
                                       End Sub
        scrollablePanel.Controls.Add(btnAddMotive)

        Dim btnRemoveMotive As New Button
        btnRemoveMotive.Text = "Remove Motive"
        btnRemoveMotive.Top = 845
        btnRemoveMotive.Left = 315
        btnRemoveMotive.Width = 70
        btnRemoveMotive.Name = "btnRemoveMotive"
        AddHandler btnRemoveMotive.Click, Sub(sender, e)
                                              If lbMotives.SelectedIndex <> -1 Then
                                                  lbMotives.Items.RemoveAt(lbMotives.SelectedIndex)
                                              End If
                                          End Sub
        scrollablePanel.Controls.Add(btnRemoveMotive)

        Return tpMOlead
    End Function
    Private Function CreateMOLeadTraitModifierTab(traitMOLeadModifierTabCount, thisTabValue) As TabPage
        Dim whoOptions As String() = {"nobody", "victim", "killer", "victimsClosest", "killersClosest", "victimsDoctor", "killersDoctor", "ransom", "victimsLandlord", "killersLandlord"}
        Dim ruleOptions As String() = {"ifAnyOfThese", "ifAllOfThese", "ifNoneOfThese", "ifPartnerAnyOfThese"}
        Dim booleanOptions As String() = {"false", "true"}

        ' Create a new TabPage for Trait Modifier
        Dim newTab As New TabPage
        newTab.Text = "MOlead Trait Modifier " & traitMOLeadModifierTabCount
        newTab.Name = "MOleadTraitModifier" & traitMOLeadModifierTabCount

        Dim innerTabControlMO As TabControl = FindControlRecursive(Of TabControl)(Me, Function(tc) tc.Name.Equals("innerTabControlMO" & thisTabValue))
        Dim pnLeads As Panel = FindControlRecursive(Of Panel)(Me, Function(p) p.Name.Equals("pnLeads" & thisTabValue))
        If pnLeads IsNot Nothing Then
            pnLeads.ScrollControlIntoView(innerTabControlMO)
            innerTabControlMO.SelectedTab = newTab

        End If


        Dim cbWho As New ComboBox
        cbWho.Top = 20
        cbWho.Left = 20
        cbWho.Width = 200
        cbWho.Name = "cbWho"
        cbWho.Items.AddRange(whoOptions)
        cbWho.DropDownStyle = ComboBoxStyle.DropDownList
        cbWho.SelectedIndex = 0
        newTab.Controls.Add(New Label With {.Text = "Who", .Top = 23, .Left = 230, .Width = 35, .Name = "lblWho"})
        newTab.Controls.Add(cbWho)

        ' Rule ComboBox (Dropdown)
        Dim cbRule As New ComboBox
        cbRule.Name = "cbRule"
        cbRule.Top = 50
        cbRule.Left = 20
        cbRule.Width = 200
        cbRule.Items.AddRange(ruleOptions)
        cbRule.DropDownStyle = ComboBoxStyle.DropDownList
        cbRule.SelectedItem = "ifAnyOfThese"
        newTab.Controls.Add(New Label With {.Text = "Rule", .Top = 53, .Left = 230, .Width = 30})
        newTab.Controls.Add(cbRule)

        ' Trait List ComboBox and ListBox
        Dim cbTraitList As New ComboBox
        cbTraitList.Top = 80
        cbTraitList.Left = 20
        cbTraitList.Width = 200
        Dim itemLines As String() = My.Resources.charactertrait.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLines
            cbTraitList.Items.Add(item.Trim())
        Next
        cbTraitList.DropDownStyle = ComboBoxStyle.DropDown
        cbTraitList.SelectedIndex = 0
        newTab.Controls.Add(New Label With {.Text = "Trait List", .Top = 83, .Left = 230})
        newTab.Controls.Add(cbTraitList)

        ' ListBox to hold selected traits
        Dim lbTraitList As New ListBox
        lbTraitList.Name = "lbTraitList"
        lbTraitList.Top = 120
        lbTraitList.Left = 20
        lbTraitList.Width = 200
        newTab.Controls.Add(lbTraitList)

        ' Add Button for Trait List
        Dim btnAddTrait As New Button
        btnAddTrait.Text = "Add Trait"
        btnAddTrait.Top = 160
        btnAddTrait.Left = 230
        AddHandler btnAddTrait.Click, Sub()
                                          If cbTraitList.SelectedItem IsNot Nothing Then
                                              If Not lbTraitList.Items.Contains(cbTraitList.SelectedItem) Then
                                                  lbTraitList.Items.Add(cbTraitList.SelectedItem)
                                              End If
                                          End If
                                      End Sub
        newTab.Controls.Add(btnAddTrait)

        ' Remove Button for Trait List
        Dim btnRemoveTrait As New Button
        btnRemoveTrait.Text = "Remove Trait"
        btnRemoveTrait.Top = 185
        btnRemoveTrait.Left = 230
        AddHandler btnRemoveTrait.Click, Sub()
                                             If lbTraitList.SelectedItem IsNot Nothing Then
                                                 lbTraitList.Items.Remove(lbTraitList.SelectedItem)
                                             End If
                                         End Sub
        newTab.Controls.Add(btnRemoveTrait)

        ' mustPassForApplication ComboBox (True/False)
        Dim cbMustPassForApplication As New ComboBox
        cbMustPassForApplication.Name = "cbMustPassForApplication"
        cbMustPassForApplication.Top = 225
        cbMustPassForApplication.Left = 20
        cbMustPassForApplication.Width = 200
        cbMustPassForApplication.Items.AddRange(booleanOptions)
        cbMustPassForApplication.DropDownStyle = ComboBoxStyle.DropDownList
        cbMustPassForApplication.SelectedItem = "false" ' Default value
        newTab.Controls.Add(New Label With {.Text = "Must Pass For Application", .Top = 228, .Left = 230, .Width = 250})
        newTab.Controls.Add(cbMustPassForApplication)

        ' Score Modifier NumericUpDown
        Dim nudScoreModifier As New NumericUpDown
        nudScoreModifier.Name = "nudScoreModifier"
        nudScoreModifier.Top = 255
        nudScoreModifier.Left = 20
        nudScoreModifier.Width = 200
        nudScoreModifier.Minimum = -1000
        nudScoreModifier.Maximum = 1000
        newTab.Controls.Add(New Label With {.Text = "Chance Modifier", .Top = 258, .Left = 230})
        newTab.Controls.Add(nudScoreModifier)

        ' Copy From ComboBox (with null as the default value)
        Dim cbCopyFromTraitModifier As New ComboBox
        cbCopyFromTraitModifier.Name = "cbCopyFrom"
        cbCopyFromTraitModifier.Top = 285
        cbCopyFromTraitModifier.Left = 20
        cbCopyFromTraitModifier.Width = 200
        cbCopyFromTraitModifier.Items.Add("null")
        Dim itemLinesCopy As String() = My.Resources.murdermo.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLinesCopy
            cbCopyFromTraitModifier.Items.Add(item.Trim())
        Next
        cbCopyFromTraitModifier.DropDownStyle = ComboBoxStyle.DropDown
        cbCopyFromTraitModifier.SelectedItem = "null" ' Default value
        newTab.Controls.Add(New Label With {.Text = "Copy From", .Top = 288, .Left = 230})
        newTab.Controls.Add(cbCopyFromTraitModifier)

        ' Remove Modifier Button
        Dim btnRemoveTraitModifier As New Button
        btnRemoveTraitModifier.Text = "Remove Modifier"
        btnRemoveTraitModifier.Top = 20
        btnRemoveTraitModifier.Left = 310
        AddHandler btnRemoveTraitModifier.Click, Sub(f, rb)
                                                     traitMOLeadModifierTabCount -= 1
                                                     Dim currentIndex As Integer = innerTabControlMO.TabPages.IndexOf(newTab)
                                                     innerTabControlMO.TabPages.Remove(newTab)
                                                     If currentIndex < innerTabControlMO.TabPages.Count Then
                                                         innerTabControlMO.SelectedIndex = currentIndex
                                                     ElseIf currentIndex > 0 Then
                                                         innerTabControlMO.SelectedIndex = currentIndex - 1
                                                     End If
                                                 End Sub
        newTab.Controls.Add(btnRemoveTraitModifier)

        Return newTab
    End Function
    Private Function CreateWeaponsTab() As TabPage
        weaponTabOpen = True
        Dim tpWeapon As New TabPage
        tpWeapon.Text = "Weapon Entry "

        ' Weapon Label and ComboBox
        Dim weaponLabel As New Label
        weaponLabel.Text = "Weapon"
        weaponLabel.Top = 23
        weaponLabel.Left = 0
        weaponLabel.Width = 70
        tpWeapon.Controls.Add(weaponLabel)

        Dim cbWeapon As New ComboBox
        cbWeapon.Name = "cbWeapon"
        cbWeapon.Top = 20
        cbWeapon.Left = 85
        cbWeapon.Width = 200
        cbWeapon.Items.AddRange(New String() {
        "Blades",
        "Guns",
        "Poison",
        "BluntObjects"
    })
        cbWeapon.DropDownStyle = ComboBoxStyle.DropDown ' Prevent user from entering their own values
        cbWeapon.SelectedIndex = 0 ' Set the default value
        tpWeapon.Controls.Add(cbWeapon)

        ' ListBox to show added weapons
        Dim lstWeapons As New ListBox
        lstWeapons.Name = "lstWeapons"
        lstWeapons.Top = 100
        lstWeapons.Left = 10
        lstWeapons.Width = 300
        lstWeapons.Height = 100
        tpWeapon.Controls.Add(lstWeapons)

        ' Button to add selected ComboBox item to the ListBox
        Dim btnAddWeapon As New Button
        btnAddWeapon.Text = "Add Weapon"
        btnAddWeapon.Top = 60
        btnAddWeapon.Left = 100
        AddHandler btnAddWeapon.Click, Sub(s, ev)
                                           If cbWeapon.SelectedItem IsNot Nothing Then
                                               lstWeapons.Items.Add(cbWeapon.SelectedItem.ToString())
                                               cbWeapon.SelectedIndex = -1 ' Reset selection after adding
                                           End If
                                       End Sub
        tpWeapon.Controls.Add(btnAddWeapon)

        ' Button to remove selected item from the ListBox
        Dim btnRemoveWeapon As New Button
        btnRemoveWeapon.Text = "Remove Weapon"
        btnRemoveWeapon.Top = 60
        btnRemoveWeapon.Left = 200
        AddHandler btnRemoveWeapon.Click, Sub(s, ev)
                                              If lstWeapons.SelectedItem IsNot Nothing Then
                                                  lstWeapons.Items.Remove(lstWeapons.SelectedItem)
                                              End If
                                          End Sub
        tpWeapon.Controls.Add(btnRemoveWeapon)

        ' Add a Remove button inside the TabPage
        Dim btnRemove As New Button
        btnRemove.Text = "Remove"
        btnRemove.Top = 210
        btnRemove.Top = 20
        btnRemove.Left = 355
        AddHandler btnRemove.Click, Sub(s, ev)
                                        weaponTabOpen = False
                                        ' Get the index of the current tab
                                        Dim currentIndex As Integer = tabControlCase.TabPages.IndexOf(tpWeapon)

                                        ' Remove the TabPage from the TabControl
                                        tabControlCase.TabPages.Remove(tpWeapon)

                                        ' Select the tab below the current one if it exists
                                        If currentIndex < tabControlCase.TabPages.Count Then
                                            tabControlCase.SelectedIndex = currentIndex
                                        ElseIf currentIndex > 0 Then
                                            tabControlCase.SelectedIndex = currentIndex - 1
                                        End If
                                    End Sub
        tpWeapon.Controls.Add(btnRemove)

        Return tpWeapon
    End Function
    Private Function CreateMurdererTraitModifiersTab() As TabPage
        murdererTraitEntryCount += 1
        Dim tpTraitModifiers As New TabPage
        tpTraitModifiers.Text = "Murderer Trait Modifiers " & murdererTraitEntryCount

        ' Rule
        Dim ruleLabel As New Label
        ruleLabel.Text = "Rule"
        ruleLabel.Top = 23
        ruleLabel.Left = 10
        ruleLabel.Width = 50
        tpTraitModifiers.Controls.Add(ruleLabel)

        Dim cbRule As New ComboBox
        cbRule.Name = "cbRule"
        cbRule.Top = 20
        cbRule.Left = 90
        cbRule.Width = 210
        cbRule.Items.AddRange(New String() {
            "ifAnyOfThese",
            "ifAllOfThese",
            "ifNoneOfThese",
            "ifPartnerAnyOfThese"
        })
        cbRule.DropDownStyle = ComboBoxStyle.DropDownList
        cbRule.SelectedIndex = 0
        tpTraitModifiers.Controls.Add(cbRule)

        ' Trait List
        Dim traitListLabel As New Label
        traitListLabel.Text = "Trait List"
        traitListLabel.Top = 60
        traitListLabel.Left = 10
        tpTraitModifiers.Controls.Add(traitListLabel)

        Dim lstTraits As New ListBox
        lstTraits.Name = "lstTraits"
        lstTraits.Top = 80
        lstTraits.Left = 10
        lstTraits.Width = 300
        lstTraits.Height = 100
        tpTraitModifiers.Controls.Add(lstTraits)
        lstTraits.BringToFront()

        Dim cbTrait As New ComboBox
        cbTrait.Name = "cbTrait"
        cbTrait.Top = 190
        cbTrait.Left = 10
        cbTrait.Width = 200
        Dim itemLines As String() = My.Resources.charactertrait.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLines
            cbTrait.Items.Add(item.Trim())
        Next
        cbTrait.DropDownStyle = ComboBoxStyle.DropDown
        tpTraitModifiers.Controls.Add(cbTrait)

        Dim btnAddTrait As New Button
        btnAddTrait.Text = "Add Trait"
        btnAddTrait.Top = 190
        btnAddTrait.Left = 220
        AddHandler btnAddTrait.Click, Sub(s, ev)
                                          If cbTrait.SelectedItem IsNot Nothing Then
                                              lstTraits.Items.Add(cbTrait.SelectedItem.ToString())
                                              cbTrait.SelectedIndex = -1 ' Reset selection
                                          End If
                                      End Sub
        tpTraitModifiers.Controls.Add(btnAddTrait)

        ' Remove Job Button
        Dim btnRemoveJob As New Button
        btnRemoveJob.Text = "Remove Trait"
        btnRemoveJob.Top = 190
        btnRemoveJob.Left = 300 ' Position next to the Add Job button
        AddHandler btnRemoveJob.Click, Sub(s, ev)
                                           If lstTraits.SelectedItem IsNot Nothing Then
                                               lstTraits.Items.Remove(lstTraits.SelectedItem)
                                           End If
                                       End Sub
        tpTraitModifiers.Controls.Add(btnRemoveJob)

        ' Must Pass For Application
        Dim mustPassLabel As New Label
        mustPassLabel.Text = "Must Pass For Application"
        mustPassLabel.Top = 223
        mustPassLabel.Left = 10
        mustPassLabel.Width = 150
        tpTraitModifiers.Controls.Add(mustPassLabel)


        Dim cbMustPass As New ComboBox
        cbMustPass.Name = "cbMustPass"
        cbMustPass.Top = 220
        cbMustPass.Left = 200
        cbMustPass.Width = 100
        cbMustPass.Items.AddRange(New String() {
            "true",
            "false"
        })
        cbMustPass.DropDownStyle = ComboBoxStyle.DropDownList
        cbMustPass.SelectedIndex = 1 ' Set default to "False"
        tpTraitModifiers.Controls.Add(cbMustPass)

        ' Score Modifier
        Dim scoreModifierLabel As New Label
        scoreModifierLabel.Text = "Score Modifier"
        scoreModifierLabel.Top = 263
        scoreModifierLabel.Left = 10
        tpTraitModifiers.Controls.Add(scoreModifierLabel)

        Dim numScoreModifier As New NumericUpDown
        numScoreModifier.Name = "numScoreModifier"
        numScoreModifier.Top = 260
        numScoreModifier.Left = 200
        numScoreModifier.Width = 100
        numScoreModifier.Minimum = -100 ' Adjust as needed
        numScoreModifier.Maximum = 100 ' Adjust as needed
        tpTraitModifiers.Controls.Add(numScoreModifier)

        Dim lblCopyFrom As New Label
        lblCopyFrom.Text = "Copy From"
        lblCopyFrom.Top = 293
        lblCopyFrom.Left = 10
        lblCopyFrom.Width = 80
        tpTraitModifiers.Controls.Add(lblCopyFrom)

        Dim cbCopyFrom As New ComboBox
        cbCopyFrom.Name = "cbCopyFrom"
        cbCopyFrom.Top = 290
        cbCopyFrom.Left = 90
        cbCopyFrom.Width = 210
        cbCopyFrom.Items.Add("null")
        Dim itemLinesCopy As String() = My.Resources.murdermo.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLinesCopy
            cbCopyFrom.Items.Add(item.Trim())
        Next
        cbCopyFrom.DropDownStyle = ComboBoxStyle.DropDown
        cbCopyFrom.SelectedIndex = 0 ' Default to null
        tpTraitModifiers.Controls.Add(cbCopyFrom)

        ' Add a Remove button inside the TabPage
        Dim btnRemove As New Button
        btnRemove.Text = "Remove"
        btnRemove.Top = 20
        btnRemove.Left = 355
        AddHandler btnRemove.Click, Sub(s, ev)
                                        murdererTraitEntryCount -= 1
                                        ' Get the index of the current tab
                                        Dim currentIndex As Integer = tabControlCase.TabPages.IndexOf(tpTraitModifiers)

                                        ' Remove the TabPage from the TabControl
                                        tabControlCase.TabPages.Remove(tpTraitModifiers)

                                        ' Select the tab below the current one if it exists
                                        If currentIndex < tabControlCase.TabPages.Count Then
                                            tabControlCase.SelectedIndex = currentIndex
                                        ElseIf currentIndex > 0 Then
                                            tabControlCase.SelectedIndex = currentIndex - 1
                                        End If
                                    End Sub
        tpTraitModifiers.Controls.Add(btnRemove)

        Return tpTraitModifiers
    End Function
    Private Sub LoadMurdererTraitModifiersTab(jsonData As JObject, tabPage As TabPage)
        Dim entryIndex As Integer = Integer.Parse(tabPage.Text.Split(" "c).Last())
        tabPage.Text = "Murderer Trait Modifiers " & entryIndex

        Dim cbRule As ComboBox = CType(tabPage.Controls("cbRule"), ComboBox)
        Dim ruleValue = jsonData("rule")

        If TypeOf ruleValue Is JValue AndAlso ruleValue.Type = JTokenType.Integer Then
            Dim index As Integer = Convert.ToInt32(ruleValue)
            If index >= 0 AndAlso index < cbRule.Items.Count Then
                cbRule.SelectedIndex = index
            End If
        Else
            cbRule.SelectedItem = ruleValue?.ToString()
            cbRule.Items.Add(ruleValue)
        End If

        Dim lstTraits As ListBox = CType(tabPage.Controls("lstTraits"), ListBox)
        lstTraits.Items.Clear()

        For Each trait In jsonData("traitList")
            If TypeOf trait Is JValue Then
                Dim traitName As String = trait.ToString()

                ' Remove the prefix if it exists
                If traitName.StartsWith("REF:CharacterTrait|") Then
                    traitName = traitName.Replace("REF:CharacterTrait|", "") ' Remove "REF:" prefix
                End If

                lstTraits.Items.Add(traitName)
            End If
        Next

        Dim cbMustPass As ComboBox = CType(tabPage.Controls("cbMustPass"), ComboBox)
        Dim mustPassValue = jsonData("mustPassForApplication")
        Dim mustPassBool As Boolean = ConvertToBool(mustPassValue)
        cbMustPass.SelectedItem = If(mustPassBool, "true", "false")

        Dim numScoreModifier As NumericUpDown = CType(tabPage.Controls("numScoreModifier"), NumericUpDown)
        numScoreModifier.Value = If(jsonData("scoreModifier") IsNot Nothing, CInt(jsonData("scoreModifier").ToObject(Of Decimal)()), 0)

        ' Copy From ComboBox (set text directly)
        Dim cbCopyFrom As ComboBox = CType(tabPage.Controls("cbCopyFrom"), ComboBox)
        ' Retrieve the original value and remove the prefix if it exists
        If jsonData("copyFrom") IsNot Nothing Then
            Dim copyFromValue As String = jsonData("copyFrom").ToString()
            If copyFromValue.StartsWith("REF:MurderMO|") Then
                copyFromValue = copyFromValue.Replace("REF:MurderMO|", "")
                If Not cbCopyFrom.Items.Contains(copyFromValue) Then
                    cbCopyFrom.Items.Add(copyFromValue)
                    cbCopyFrom.SelectedItem = copyFromValue
                Else
                    SetComboBoxText(cbCopyFrom, copyFromValue)
                End If
            ElseIf copyFromValue = "" Or JTokenType.Null Or Nothing Then
                cbCopyFrom.SelectedIndex = 0
            End If
        Else
            cbCopyFrom.SelectedIndex = 0
        End If
    End Sub
    Private Function ConvertToBool(value As JToken) As Boolean
        Try
            If value Is Nothing Then
                Throw New ArgumentNullException(NameOf(value), "The JToken value is null.")
            End If

            ' Check if it's a Boolean
            If value.Type = JTokenType.Boolean Then
                Return value.ToObject(Of Boolean)()
                ' Check if it's an integer
            ElseIf value.Type = JTokenType.Integer Then
                Return value.ToObject(Of Integer)() = 1
            End If

            Return False
        Catch ex As Exception
            ' Log the exception message and stack trace
            Console.WriteLine($"Error: {ex.Message}")
            Console.WriteLine($"Stack Trace: {ex.StackTrace}")
            Return False
        End Try
    End Function
    Private Sub SetComboBoxValue(comboBox As ComboBox, value As JToken)
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
    Private Sub FormatJson()
        Dim originalText As String = txtOutput.Text
        Dim formattedText As New StringBuilder()

        ' Process each line of the original text
        For Each line As String In originalText.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
            ' Keep the original indentation
            Dim trimmedLine As String = line.Trim()

            ' Add color formatting logic
            If Not String.IsNullOrWhiteSpace(trimmedLine) Then
                If trimmedLine.StartsWith("{") OrElse trimmedLine.StartsWith("}") Then
                    formattedText.AppendLine(line) ' No color for braces, keep original
                ElseIf trimmedLine.Contains(":") Then
                    ' Split the line at the first colon
                    Dim parts As String() = trimmedLine.Split(New Char() {":"c}, 2)

                    ' Apply color formatting (example: keys in blue, values in black)
                    formattedText.Append(parts(0).Trim() & ": ") ' Key without color
                    formattedText.Append(parts(1).Trim() & Environment.NewLine) ' Value without color
                Else
                    formattedText.AppendLine(line) ' For other lines, keep original
                End If
            Else
                formattedText.AppendLine(line) ' Blank lines
            End If
        Next

        ' Clear the rich text box and set the formatted text
        txtOutput.Clear()
        txtOutput.AppendText(formattedText.ToString())

        ' Apply color formatting after setting the text
        ApplyColors()
    End Sub
    Private Sub FormatManifest()
        Dim originalText As String = txtManiOutput.Text
        Dim formattedText As New StringBuilder()

        ' Process each line of the original text
        For Each line As String In originalText.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
            ' Keep the original indentation
            Dim trimmedLine As String = line.Trim()

            ' Add color formatting logic
            If Not String.IsNullOrWhiteSpace(trimmedLine) Then
                If trimmedLine.StartsWith("{") OrElse trimmedLine.StartsWith("}") Then
                    formattedText.AppendLine(line) ' No color for braces, keep original
                ElseIf trimmedLine.Contains(":") Then
                    ' Split the line at the first colon
                    Dim parts As String() = trimmedLine.Split(New Char() {":"c}, 2)

                    ' Apply color formatting (example: keys in blue, values in black)
                    formattedText.Append(parts(0).Trim() & ": ") ' Key without color
                    formattedText.Append(parts(1).Trim() & Environment.NewLine) ' Value without color
                Else
                    formattedText.AppendLine(line) ' For other lines, keep original
                End If
            Else
                formattedText.AppendLine(line) ' Blank lines
            End If
        Next

        ' Clear the rich text box and set the formatted text
        txtManiOutput.Clear()
        txtManiOutput.AppendText(formattedText.ToString())

        ' Apply color formatting after setting the text
        ApplyColorsManifest()
    End Sub
    Private Sub ApplyColors()
        ' Clear previous formatting
        txtOutput.SelectAll()
        txtOutput.SelectionColor = Color.Black ' Reset to default color

        Dim lines As String() = txtOutput.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.None)

        For Each line As String In lines
            Dim currentIndex As Integer = 0

            While currentIndex < line.Length
                Dim currentChar As Char = line(currentIndex)

                ' Color for braces
                If currentChar = "{"c OrElse currentChar = "}"c Then
                    txtOutput.SelectionStart = txtOutput.GetFirstCharIndexFromLine(Array.IndexOf(lines, line)) + currentIndex
                    txtOutput.SelectionLength = 1
                    txtOutput.SelectionColor = Color.Blue
                ElseIf currentChar = """"c Then
                    ' Handle the start of a string
                    Dim endQuoteIndex As Integer = currentIndex + 1
                    While endQuoteIndex < line.Length AndAlso line(endQuoteIndex) <> """"c
                        endQuoteIndex += 1
                    End While

                    If endQuoteIndex < line.Length Then
                        ' Check if the string is a key (immediately followed by a colon)
                        Dim isKey As Boolean = (endQuoteIndex + 1 < line.Length AndAlso line(endQuoteIndex + 1) = ":"c)
                        txtOutput.SelectionStart = txtOutput.GetFirstCharIndexFromLine(Array.IndexOf(lines, line)) + currentIndex
                        txtOutput.SelectionLength = endQuoteIndex - currentIndex + 1
                        txtOutput.SelectionColor = If(isKey, Color.Purple, Color.Red) ' Keys are purple, values are red
                        currentIndex = endQuoteIndex ' Move index to the end of the string
                    End If
                ElseIf Char.IsDigit(currentChar) Then
                    ' Color numerical values
                    Dim numberStart As Integer = currentIndex
                    While currentIndex < line.Length AndAlso Char.IsDigit(line(currentIndex))
                        currentIndex += 1
                    End While
                    txtOutput.SelectionStart = txtOutput.GetFirstCharIndexFromLine(Array.IndexOf(lines, line)) + numberStart
                    txtOutput.SelectionLength = currentIndex - numberStart
                    txtOutput.SelectionColor = Color.DarkOrange
                    currentIndex -= 1 ' Adjust index back
                ElseIf String.Compare(line.Substring(currentIndex, Math.Min(4, line.Length - currentIndex)), "true", StringComparison.OrdinalIgnoreCase) = 0 OrElse
                       String.Compare(line.Substring(currentIndex, Math.Min(5, line.Length - currentIndex)), "false", StringComparison.OrdinalIgnoreCase) = 0 OrElse
                       String.Compare(line.Substring(currentIndex, Math.Min(4, line.Length - currentIndex)), "null", StringComparison.OrdinalIgnoreCase) = 0 Then
                    ' Color boolean values
                    Dim boolStart As Integer = currentIndex
                    While currentIndex < line.Length AndAlso Not Char.IsWhiteSpace(line(currentIndex)) AndAlso line(currentIndex) <> ","c AndAlso line(currentIndex) <> "}"c
                        currentIndex += 1
                    End While
                    txtOutput.SelectionStart = txtOutput.GetFirstCharIndexFromLine(Array.IndexOf(lines, line)) + boolStart
                    txtOutput.SelectionLength = currentIndex - boolStart
                    txtOutput.SelectionColor = Color.Green
                    currentIndex -= 1 ' Adjust index back
                End If

                currentIndex += 1 ' Move to the next character
            End While
        Next

        ' Reset selection
        txtOutput.SelectionStart = 0
        txtOutput.SelectionLength = 0
        txtOutput.SelectionColor = Color.Black ' Reset to default color
    End Sub
    Private Sub ApplyColorsManifest()
        ' Clear previous formatting
        txtManiOutput.SelectAll()
        txtManiOutput.SelectionColor = Color.Black ' Reset to default color

        Dim lines As String() = txtManiOutput.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.None)

        For Each line As String In lines
            Dim currentIndex As Integer = 0

            While currentIndex < line.Length
                Dim currentChar As Char = line(currentIndex)

                ' Color for braces
                If currentChar = "{"c OrElse currentChar = "}"c Then
                    txtManiOutput.SelectionStart = txtManiOutput.GetFirstCharIndexFromLine(Array.IndexOf(lines, line)) + currentIndex
                    txtManiOutput.SelectionLength = 1
                    txtManiOutput.SelectionColor = Color.Blue
                ElseIf currentChar = """"c Then
                    ' Handle the start of a string
                    Dim endQuoteIndex As Integer = currentIndex + 1
                    While endQuoteIndex < line.Length AndAlso line(endQuoteIndex) <> """"c
                        endQuoteIndex += 1
                    End While

                    If endQuoteIndex < line.Length Then
                        ' Check if the string is a key (immediately followed by a colon)
                        Dim isKey As Boolean = (endQuoteIndex + 1 < line.Length AndAlso line(endQuoteIndex + 1) = ":"c)
                        txtManiOutput.SelectionStart = txtManiOutput.GetFirstCharIndexFromLine(Array.IndexOf(lines, line)) + currentIndex
                        txtManiOutput.SelectionLength = endQuoteIndex - currentIndex + 1
                        txtManiOutput.SelectionColor = If(isKey, Color.Purple, Color.Red) ' Keys are purple, values are red
                        currentIndex = endQuoteIndex ' Move index to the end of the string
                    End If
                ElseIf Char.IsDigit(currentChar) Then
                    ' Color numerical values
                    Dim numberStart As Integer = currentIndex
                    While currentIndex < line.Length AndAlso Char.IsDigit(line(currentIndex))
                        currentIndex += 1
                    End While
                    txtManiOutput.SelectionStart = txtManiOutput.GetFirstCharIndexFromLine(Array.IndexOf(lines, line)) + numberStart
                    txtManiOutput.SelectionLength = currentIndex - numberStart
                    txtManiOutput.SelectionColor = Color.DarkOrange
                    currentIndex -= 1 ' Adjust index back
                ElseIf String.Compare(line.Substring(currentIndex, Math.Min(4, line.Length - currentIndex)), "true", StringComparison.OrdinalIgnoreCase) = 0 OrElse
                       String.Compare(line.Substring(currentIndex, Math.Min(5, line.Length - currentIndex)), "false", StringComparison.OrdinalIgnoreCase) = 0 OrElse
                       String.Compare(line.Substring(currentIndex, Math.Min(4, line.Length - currentIndex)), "null", StringComparison.OrdinalIgnoreCase) = 0 Then
                    ' Color boolean values
                    Dim boolStart As Integer = currentIndex
                    While currentIndex < line.Length AndAlso Not Char.IsWhiteSpace(line(currentIndex)) AndAlso line(currentIndex) <> ","c AndAlso line(currentIndex) <> "}"c
                        currentIndex += 1
                    End While
                    txtManiOutput.SelectionStart = txtManiOutput.GetFirstCharIndexFromLine(Array.IndexOf(lines, line)) + boolStart
                    txtManiOutput.SelectionLength = currentIndex - boolStart
                    txtManiOutput.SelectionColor = Color.Green
                    currentIndex -= 1 ' Adjust index back
                End If

                currentIndex += 1 ' Move to the next character
            End While
        Next

        ' Reset selection
        txtManiOutput.SelectionStart = 0
        txtManiOutput.SelectionLength = 0
        txtManiOutput.SelectionColor = Color.Black ' Reset to default color
    End Sub
    Private Sub btnAddMurdererTraits_Click(sender As Object, e As EventArgs) Handles btnAddMurdererTraits.Click
        Dim tpMurdererTraits As TabPage = CreateMurdererTraitModifiersTab()
        tabControlCase.TabPages.Add(tpMurdererTraits)
        tabControlCase.SelectedTab = tpMurdererTraits
    End Sub
    Private Sub btnAddMOlead_Click_1(sender As Object, e As EventArgs) Handles btnAddMOlead.Click
        setTabValueMO += 1
        Dim traitMOLeadModifierTabCount As Integer = 0
        Dim tpMOlead As TabPage = CreateMOleadTab(traitMOLeadModifierTabCount)
        tabControlCase.TabPages.Add(tpMOlead)
        tabControlCase.SelectedTab = tpMOlead
    End Sub
    Private Sub btnCallingCard_Click_1(sender As Object, e As EventArgs) Handles btnCallingCard.Click
        setTabValueCC += 1
        Dim traitModifierTabCount As Integer = 0
        Dim tpCallingCard As TabPage = CreateCallingCardTab(traitModifierTabCount)
        tabControlCase.TabPages.Add(tpCallingCard)
        tabControlCase.SelectedTab = tpCallingCard
    End Sub
    Private Sub btnAddMurdererJobModifier_Click(sender As Object, e As EventArgs) Handles btnAddMurdererJobModifier.Click
        Dim tpMurdererJobModifier As TabPage = CreateMurdererJobModifierTab()
        tabControlCase.TabPages.Add(tpMurdererJobModifier)
        tabControlCase.SelectedTab = tpMurdererJobModifier
    End Sub
    Private Function CreateMurdererJobModifierTab() As TabPage
        murdererJobModifierEntryCount += 1
        Dim tpJobModifier As New TabPage
        tpJobModifier.Text = "Murderer Job Modifiers " & murdererJobModifierEntryCount

        ' Job Label and ComboBox
        Dim jobLabel As New Label
        jobLabel.Text = "Job"
        jobLabel.Top = 23
        jobLabel.Left = 10
        jobLabel.Width = 30
        tpJobModifier.Controls.Add(jobLabel)

        Dim cbJob As New ComboBox
        cbJob.Name = "cbJob"
        cbJob.Top = 20
        cbJob.Left = 50
        cbJob.Width = 200
        Dim itemLines As String() = My.Resources.jobpreset.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLines
            cbJob.Items.Add(item.Trim())
        Next
        cbJob.DropDownStyle = ComboBoxStyle.DropDown
        tpJobModifier.Controls.Add(cbJob)

        ' ListBox for selected jobs
        Dim lstJobs As New ListBox
        lstJobs.Name = "lstJobs"
        lstJobs.Top = 60
        lstJobs.Left = 10
        lstJobs.Width = 300
        lstJobs.Height = 100
        tpJobModifier.Controls.Add(lstJobs)

        ' Add Job Button
        Dim btnAddJob As New Button
        btnAddJob.Text = "Add"
        btnAddJob.Top = 170
        btnAddJob.Left = 10
        AddHandler btnAddJob.Click, Sub(s, ev)
                                        If cbJob.SelectedItem IsNot Nothing Then
                                            lstJobs.Items.Add(cbJob.SelectedItem.ToString())
                                            cbJob.SelectedIndex = -1 ' Reset selection
                                        End If
                                    End Sub
        tpJobModifier.Controls.Add(btnAddJob)

        ' Remove Job Button
        Dim btnRemoveJob As New Button
        btnRemoveJob.Text = "Remove Job"
        btnRemoveJob.Top = 170
        btnRemoveJob.Left = 90 ' Position next to the Add Job button
        AddHandler btnRemoveJob.Click, Sub(s, ev)
                                           If lstJobs.SelectedItem IsNot Nothing Then
                                               lstJobs.Items.Remove(lstJobs.SelectedItem)
                                           End If
                                       End Sub
        tpJobModifier.Controls.Add(btnRemoveJob)

        ' Job Boost Label and NumericUpDown
        Dim lblJobBoost As New Label
        lblJobBoost.Text = "Job Boost"
        lblJobBoost.Top = 213
        lblJobBoost.Left = 10
        lblJobBoost.Width = 80
        tpJobModifier.Controls.Add(lblJobBoost)

        Dim numJobBoost As New NumericUpDown
        numJobBoost.Name = "numJobBoost"
        numJobBoost.Top = 210
        numJobBoost.Left = 90
        numJobBoost.Width = 100
        numJobBoost.Minimum = 0
        numJobBoost.Maximum = 100
        tpJobModifier.Controls.Add(numJobBoost)


        Dim lblCopyFrom As New Label
        lblCopyFrom.Text = "Copy From"
        lblCopyFrom.Top = 243
        lblCopyFrom.Left = 10
        lblCopyFrom.Width = 80
        tpJobModifier.Controls.Add(lblCopyFrom)

        Dim cbCopyFrom As New ComboBox
        cbCopyFrom.Name = "cbCopyFrom"
        cbCopyFrom.Top = 240
        cbCopyFrom.Left = 90
        cbCopyFrom.Width = 200
        Dim itemLinesCopy As String() = My.Resources.murdermo.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLinesCopy
            cbCopyFrom.Items.Add(item.Trim())
        Next
        cbCopyFrom.DropDownStyle = ComboBoxStyle.DropDown
        cbCopyFrom.SelectedIndex = 0 ' Default to null
        tpJobModifier.Controls.Add(cbCopyFrom)
        ' Add a Remove button inside the TabPage
        Dim btnRemove As New Button
        btnRemove.Text = "Remove"
        btnRemove.Top = 20
        btnRemove.Left = 355
        AddHandler btnRemove.Click, Sub(s, ev)
                                        murdererJobModifierEntryCount -= 1

                                        Dim currentIndex As Integer = tabControlCase.TabPages.IndexOf(tpJobModifier)
                                        tabControlCase.TabPages.Remove(tpJobModifier)

                                        If currentIndex < tabControlCase.TabPages.Count Then
                                            tabControlCase.SelectedIndex = currentIndex
                                        ElseIf currentIndex > 0 Then
                                            tabControlCase.SelectedIndex = currentIndex - 1
                                        End If
                                    End Sub
        tpJobModifier.Controls.Add(btnRemove)

        Return tpJobModifier
    End Function
    Private Sub LoadMurdererJobModifierTab(jsonData As JObject, tabPage As TabPage)
        ' Ensure the tab has the correct name
        Dim entryIndex As Integer = Integer.Parse(tabPage.Text.Split(" "c).Last()) ' Extract the index from the tab name
        tabPage.Text = "Murderer Job Modifiers " & entryIndex

        ' Jobs ListBox
        Dim lstJobs As ListBox = CType(tabPage.Controls("lstJobs"), ListBox)
        lstJobs.Items.Clear()
        For Each job In jsonData("jobs")
            If TypeOf job Is JValue Then
                Dim jobName As String = job.ToString()

                ' Remove the prefix if it exists
                If jobName.StartsWith("REF:OccupationPreset|") Then
                    jobName = jobName.Replace("REF:OccupationPreset|", "")
                End If

                lstJobs.Items.Add(jobName)
            End If
        Next

        ' Job Boost NumericUpDown
        Dim numJobBoost As NumericUpDown = CType(tabPage.Controls("numJobBoost"), NumericUpDown)
        numJobBoost.Value = If(jsonData("jobBoost") IsNot Nothing, CInt(jsonData("jobBoost").ToObject(Of Decimal)()), 0)

        ' Copy From ComboBox (set text directly)
        Dim cbCopyFrom As ComboBox = CType(tabPage.Controls("cbCopyFrom"), ComboBox)
        ' Retrieve the original value and remove the prefix if it exists
        Dim copyFromValue As String = jsonData("copyFrom").ToString()
        If copyFromValue.StartsWith("REF:MurderMO|") Then
            copyFromValue = copyFromValue.Replace("REF:MurderMO|", "")
            If Not cbCopyFrom.Items.Contains(copyFromValue) Then
                cbCopyFrom.Items.Add(copyFromValue)
                cbCopyFrom.SelectedItem = copyFromValue
            Else
                SetComboBoxText(cbCopyFrom, copyFromValue)
            End If
        ElseIf copyFromValue = "" Or JTokenType.Null Or Nothing Then
            cbCopyFrom.SelectedIndex = 0
        End If
    End Sub
    Private Sub btnAddNewMurdererCompModifier_Click(sender As Object, e As EventArgs) Handles btnAddNewMurdererCompModifier.Click
        Dim tpMurdererCompanyModifier As TabPage = CreateMurdererCompanyModifierTab()
        tabControlCase.TabPages.Add(tpMurdererCompanyModifier)
        tabControlCase.SelectedTab = tpMurdererCompanyModifier
    End Sub
    Private Function CreateMurdererCompanyModifierTab() As TabPage
        murdererCompanyModifierEntryCount += 1
        Dim tpCompanyModifier As New TabPage
        tpCompanyModifier.Text = "Murderer Company Modifiers " & murdererCompanyModifierEntryCount

        ' Companies Label and ComboBox
        Dim companiesLabel As New Label
        companiesLabel.Text = "Company"
        companiesLabel.Top = 23
        companiesLabel.Left = 10
        companiesLabel.Width = 60
        tpCompanyModifier.Controls.Add(companiesLabel)

        Dim cbCompanies As New ComboBox
        cbCompanies.Name = "cbCompanies"
        cbCompanies.Top = 20
        cbCompanies.Left = 80
        cbCompanies.Width = 200
        Dim itemLines As String() = My.Resources.companypreset.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLines
            cbCompanies.Items.Add(item.Trim())
        Next
        cbCompanies.DropDownStyle = ComboBoxStyle.DropDown
        tpCompanyModifier.Controls.Add(cbCompanies)

        ' ListBox for selected companies
        Dim lstCompanies As New ListBox
        lstCompanies.Name = "lstCompanies"
        lstCompanies.Top = 60
        lstCompanies.Left = 10
        lstCompanies.Width = 300
        lstCompanies.Height = 100
        tpCompanyModifier.Controls.Add(lstCompanies)

        ' Add Company Button
        Dim btnAddCompany As New Button
        btnAddCompany.Text = "Add Company"
        btnAddCompany.Top = 170
        btnAddCompany.Left = 10
        AddHandler btnAddCompany.Click, Sub(s, ev)
                                            If cbCompanies.SelectedItem IsNot Nothing Then
                                                lstCompanies.Items.Add(cbCompanies.SelectedItem.ToString())
                                                cbCompanies.SelectedIndex = -1 ' Reset selection
                                            End If
                                        End Sub
        tpCompanyModifier.Controls.Add(btnAddCompany)

        ' Remove Company Button
        Dim btnRemoveCompany As New Button
        btnRemoveCompany.Text = "Remove Company"
        btnRemoveCompany.Top = 170
        btnRemoveCompany.Left = 120 ' Position next to the Add Company button
        AddHandler btnRemoveCompany.Click, Sub(s, ev)
                                               If lstCompanies.SelectedItem IsNot Nothing Then
                                                   lstCompanies.Items.Remove(lstCompanies.SelectedItem)
                                               End If
                                           End Sub
        tpCompanyModifier.Controls.Add(btnRemoveCompany)

        ' Minimum Employees Label and NumericUpDown
        Dim lblMinimumEmployees As New Label
        lblMinimumEmployees.Text = "Minimum Employees"
        lblMinimumEmployees.Top = 213
        lblMinimumEmployees.Left = 10
        lblMinimumEmployees.Width = 120
        tpCompanyModifier.Controls.Add(lblMinimumEmployees)

        Dim nudMinimumEmployees As New NumericUpDown
        nudMinimumEmployees.Name = "nudMinimumEmployees"
        nudMinimumEmployees.Top = 210
        nudMinimumEmployees.Left = 140
        nudMinimumEmployees.Width = 100
        nudMinimumEmployees.Minimum = 0
        nudMinimumEmployees.Maximum = 1000
        tpCompanyModifier.Controls.Add(nudMinimumEmployees)

        ' Company Boost Label and NumericUpDown
        Dim lblCompanyBoost As New Label
        lblCompanyBoost.Text = "Company Boost"
        lblCompanyBoost.Top = 243
        lblCompanyBoost.Left = 10
        lblCompanyBoost.Width = 100
        tpCompanyModifier.Controls.Add(lblCompanyBoost)

        Dim nudCompanyBoost As New NumericUpDown
        nudCompanyBoost.Name = "nudCompanyBoost"
        nudCompanyBoost.Top = 240
        nudCompanyBoost.Left = 140
        nudCompanyBoost.Width = 100
        nudCompanyBoost.Minimum = -100
        nudCompanyBoost.Maximum = 1000
        tpCompanyModifier.Controls.Add(nudCompanyBoost)

        ' Boost Per Employee Label and NumericUpDown
        Dim lblBoostPerEmployee As New Label
        lblBoostPerEmployee.Text = "Boost Per Employee"
        lblBoostPerEmployee.Top = 273
        lblBoostPerEmployee.Left = 10
        lblBoostPerEmployee.Width = 130
        tpCompanyModifier.Controls.Add(lblBoostPerEmployee)

        Dim nudBoostPerEmployee As New NumericUpDown
        nudBoostPerEmployee.Name = "nudBoostPerEmployeeOverMinimum"
        nudBoostPerEmployee.Top = 270
        nudBoostPerEmployee.Left = 140
        nudBoostPerEmployee.Width = 100
        nudBoostPerEmployee.Minimum = -100
        nudBoostPerEmployee.Maximum = 1000
        tpCompanyModifier.Controls.Add(nudBoostPerEmployee)

        ' Copy From Label and ComboBox
        Dim lblCopyFrom As New Label
        lblCopyFrom.Text = "Copy From"
        lblCopyFrom.Top = 303
        lblCopyFrom.Left = 10
        lblCopyFrom.Width = 80
        tpCompanyModifier.Controls.Add(lblCopyFrom)

        Dim cbCopyFrom As New ComboBox
        cbCopyFrom.Name = "cbCopyFrom"
        cbCopyFrom.Top = 300
        cbCopyFrom.Left = 90
        cbCopyFrom.Width = 200
        Dim itemLinesCopy As String() = My.Resources.murdermo.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLinesCopy
            cbCopyFrom.Items.Add(item.Trim())
        Next
        cbCopyFrom.DropDownStyle = ComboBoxStyle.DropDown
        cbCopyFrom.SelectedIndex = 0 ' Default to null
        tpCompanyModifier.Controls.Add(cbCopyFrom)

        ' Add a Remove button inside the TabPage
        Dim btnRemove As New Button
        btnRemove.Text = "Remove"
        btnRemove.Top = 20
        btnRemove.Left = 355
        AddHandler btnRemove.Click, Sub(s, ev)
                                        murdererCompanyModifierEntryCount -= 1

                                        Dim currentIndex As Integer = tabControlCase.TabPages.IndexOf(tpCompanyModifier)
                                        tabControlCase.TabPages.Remove(tpCompanyModifier)

                                        If currentIndex < tabControlCase.TabPages.Count Then
                                            tabControlCase.SelectedIndex = currentIndex
                                        ElseIf currentIndex > 0 Then
                                            tabControlCase.SelectedIndex = currentIndex - 1
                                        End If
                                    End Sub
        tpCompanyModifier.Controls.Add(btnRemove)

        Return tpCompanyModifier
    End Function
    Private Sub LoadMurdererCompanyModifierTab(jsonData As JObject, tabPage As TabPage)
        ' Ensure the tab has the correct name
        Dim entryIndex As Integer = Integer.Parse(tabPage.Text.Split(" "c).Last()) ' Extract the index from the tab name
        tabPage.Text = "Murderer Company Modifiers " & entryIndex

        ' Companies ListBox
        Dim lstCompanies As ListBox = CType(tabPage.Controls("lstCompanies"), ListBox)
        lstCompanies.Items.Clear()
        For Each company In jsonData("companies")
            If TypeOf company Is JValue Then
                Dim companyName As String = company.ToString()

                ' Remove the prefix if it exists
                If companyName.StartsWith("REF:CompanyPreset|") Then
                    companyName = companyName.Replace("REF:CompanyPreset|", "")
                End If

                lstCompanies.Items.Add(companyName)
            End If
        Next

        ' Minimum Employees NumericUpDown
        Dim nudMinimumEmployees As NumericUpDown = CType(tabPage.Controls("nudMinimumEmployees"), NumericUpDown)
        nudMinimumEmployees.Value = If(jsonData("minimumEmployees") IsNot Nothing, CInt(jsonData("minimumEmployees").ToObject(Of Decimal)()), 0)

        ' Company Boost NumericUpDown
        Dim nudCompanyBoost As NumericUpDown = CType(tabPage.Controls("nudCompanyBoost"), NumericUpDown)
        nudCompanyBoost.Value = If(jsonData("companyBoost") IsNot Nothing, CInt(jsonData("companyBoost").ToObject(Of Decimal)()), 0)

        ' Boost Per Employee Over Minimum NumericUpDown
        Dim nudBoostPerEmployee As NumericUpDown = CType(tabPage.Controls("nudBoostPerEmployeeOverMinimum"), NumericUpDown)
        nudBoostPerEmployee.Value = If(jsonData("boostPerEmployeeOverMinimum") IsNot Nothing, CInt(jsonData("boostPerEmployeeOverMinimum").ToObject(Of Decimal)()), 0)

        ' Copy From ComboBox (set text directly)
        Dim cbCopyFrom As ComboBox = CType(tabPage.Controls("cbCopyFrom"), ComboBox)
        ' Retrieve the original value and remove the prefix if it exists
        If jsonData("copyFrom") IsNot Nothing Then
            Dim copyFromValue As String = jsonData("copyFrom").ToString()
            If copyFromValue.StartsWith("REF:MurderMO|") Then
                copyFromValue = copyFromValue.Replace("REF:MurderMO|", "")
                If Not cbCopyFrom.Items.Contains(copyFromValue) Then
                    cbCopyFrom.Items.Add(copyFromValue)
                    cbCopyFrom.SelectedItem = copyFromValue
                Else
                    SetComboBoxText(cbCopyFrom, copyFromValue)
                End If
            ElseIf copyFromValue = "" Or JTokenType.Null Or Nothing Then
                cbCopyFrom.SelectedIndex = 0
            End If
        Else
            cbCopyFrom.SelectedIndex = 0
        End If
    End Sub
    Private Sub btnOpenHexaco_Click(sender As Object, e As EventArgs) Handles btnOpenHexaco.Click
        If hexacoTabOpen = False Then
            Dim tpHexacoModifier As TabPage = CreateHexacoTab()
            tabControlCase.TabPages.Add(tpHexacoModifier)
            tabControlCase.SelectedTab = tpHexacoModifier
        Else
            MessageBox.Show("You already have a hexaco entry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Function CreateHexacoTab() As TabPage
        hexacoTabOpen = True
        Dim tpHexaco As New TabPage
        tpHexaco.Text = "Hexaco Modifiers"

        ' Create controls for outputMin
        Dim lblOutputMin As New Label
        lblOutputMin.Text = "Output Min"
        lblOutputMin.Top = 10
        lblOutputMin.Left = 10
        tpHexaco.Controls.Add(lblOutputMin)

        Dim nudOutputMin As New NumericUpDown
        nudOutputMin.Name = "nudOutputMin"
        nudOutputMin.Top = 10
        nudOutputMin.Left = 120
        nudOutputMin.Minimum = -100
        nudOutputMin.Maximum = 100
        nudOutputMin.Value = 1
        tpHexaco.Controls.Add(nudOutputMin)

        ' Create controls for outputMax
        Dim lblOutputMax As New Label
        lblOutputMax.Text = "Output Max"
        lblOutputMax.Top = 40
        lblOutputMax.Left = 10
        tpHexaco.Controls.Add(lblOutputMax)

        Dim nudOutputMax As New NumericUpDown
        nudOutputMax.Name = "nudOutputMax"
        nudOutputMax.Top = 40
        nudOutputMax.Left = 120
        nudOutputMax.Minimum = -100
        nudOutputMax.Maximum = 100
        nudOutputMax.Value = 10
        tpHexaco.Controls.Add(nudOutputMax)

        ' Function to create checkbox and numeric controls for HEXACO traits
        Dim currentTop As Integer = 70
        Dim traits As String() = {"FeminineMasculine", "Humility", "Emotionality", "Extraversion", "Agreeableness", "Conscientiousness", "Creativity"}

        For Each trait As String In traits
            ' Enable trait dropdown (true/false)
            Dim lblEnableTrait As New Label
            If trait = "FeminineMasculine" Then
                lblEnableTrait.Text = "Enable Feminine Masculine"
            Else
                lblEnableTrait.Text = "Enable " & trait
            End If
            lblEnableTrait.Top = currentTop + 3
            lblEnableTrait.Left = 10
            lblEnableTrait.Width = 160
            tpHexaco.Controls.Add(lblEnableTrait)


            Dim cbEnableTrait As New ComboBox
            cbEnableTrait.Name = "cbEnable" & trait
            cbEnableTrait.Top = currentTop
            cbEnableTrait.Left = 170
            cbEnableTrait.Items.AddRange(New String() {"false", "true"})
            cbEnableTrait.DropDownStyle = ComboBoxStyle.DropDownList
            cbEnableTrait.SelectedIndex = 0 ' Default to false
            tpHexaco.Controls.Add(cbEnableTrait)

            ' Trait numeric input
            Dim lblTrait As New Label
            If trait = "FeminineMasculine" Then
                lblTrait.Text = "Feminine Masculine"
            Else
                lblTrait.Text = trait
            End If
            lblTrait.Top = currentTop + 33
            lblTrait.Left = 10
            lblTrait.Width = 160
            tpHexaco.Controls.Add(lblTrait)

            Dim nudTrait As New NumericUpDown
            nudTrait.Name = "nud" & trait
            nudTrait.Top = currentTop + 30
            nudTrait.Left = 170
            nudTrait.Minimum = -100
            nudTrait.Maximum = 100
            tpHexaco.Controls.Add(nudTrait)

            currentTop += 60

            ' Add a Remove button inside the TabPage
            Dim btnRemove As New Button
            btnRemove.Text = "Remove"
            btnRemove.Top = 20
            btnRemove.Left = 355
            AddHandler btnRemove.Click, Sub(s, ev)
                                            ' Remove the TabPage from the TabControl
                                            hexacoTabOpen = False
                                            tabControlCase.TabPages.Remove(tpHexaco)
                                        End Sub
            tpHexaco.Controls.Add(btnRemove)

            lblEnableTrait.BringToFront()
            lblTrait.BringToFront()
        Next

        Return tpHexaco
    End Function
    Private Sub LoadHexacoData(ByVal tpHexaco As TabPage, ByVal hexacoData As JObject)
        ' Set NumericUpDown values
        CType(tpHexaco.Controls("nudOutputMin"), NumericUpDown).Value = CDec(hexacoData("outputMin"))
        CType(tpHexaco.Controls("nudOutputMax"), NumericUpDown).Value = CDec(hexacoData("outputMax"))

        ' Set ComboBox values (enable flags) and corresponding NumericUpDowns for each trait
        CType(tpHexaco.Controls("cbEnableFeminineMasculine"), ComboBox).SelectedItem = hexacoData("enableFeminineMasculine").ToString().ToLower()
        CType(tpHexaco.Controls("nudFeminineMasculine"), NumericUpDown).Value = CDec(hexacoData("feminineMasculine"))

        CType(tpHexaco.Controls("cbEnableHumility"), ComboBox).SelectedItem = hexacoData("enableHumility").ToString().ToLower()
        CType(tpHexaco.Controls("nudHumility"), NumericUpDown).Value = CDec(hexacoData("humility"))

        CType(tpHexaco.Controls("cbEnableEmotionality"), ComboBox).SelectedItem = hexacoData("enableEmotionality").ToString().ToLower()
        CType(tpHexaco.Controls("nudEmotionality"), NumericUpDown).Value = CDec(hexacoData("emotionality"))

        CType(tpHexaco.Controls("cbEnableExtraversion"), ComboBox).SelectedItem = hexacoData("enableExtraversion").ToString().ToLower()
        CType(tpHexaco.Controls("nudExtraversion"), NumericUpDown).Value = CDec(hexacoData("extraversion"))

        CType(tpHexaco.Controls("cbEnableAgreeableness"), ComboBox).SelectedItem = hexacoData("enableAgreeableness").ToString().ToLower()
        CType(tpHexaco.Controls("nudAgreeableness"), NumericUpDown).Value = CDec(hexacoData("agreeableness"))

        CType(tpHexaco.Controls("cbEnableConscientiousness"), ComboBox).SelectedItem = hexacoData("enableConscientiousness").ToString().ToLower()
        CType(tpHexaco.Controls("nudConscientiousness"), NumericUpDown).Value = CDec(hexacoData("conscientiousness"))

        CType(tpHexaco.Controls("cbEnableCreativity"), ComboBox).SelectedItem = hexacoData("enableCreativity").ToString().ToLower()
        CType(tpHexaco.Controls("nudCreativity"), NumericUpDown).Value = CDec(hexacoData("creativity"))
    End Sub
    Private Sub btnAddVictimTraits_Click(sender As Object, e As EventArgs) Handles btnAddVictimTraits.Click
        Dim tpVictimTraits As TabPage = CreateVictimTraitModifiersTab()
        tabControlCase.TabPages.Add(tpVictimTraits)
        tabControlCase.SelectedTab = tpVictimTraits
    End Sub
    Private Function CreateVictimTraitModifiersTab() As TabPage
        victimTraitEntryCount += 1
        Dim tpVictimTraitModifiers As New TabPage
        tpVictimTraitModifiers.Text = "Victim Trait Modifiers " & victimTraitEntryCount

        ' Rule
        Dim ruleLabel As New Label
        ruleLabel.Text = "Rule"
        ruleLabel.Top = 23
        ruleLabel.Left = 10
        ruleLabel.Width = 50
        tpVictimTraitModifiers.Controls.Add(ruleLabel)

        Dim cbRule As New ComboBox
        cbRule.Name = "cbRule"
        cbRule.Top = 20
        cbRule.Left = 90
        cbRule.Width = 210
        cbRule.Items.AddRange(New String() {
            "ifAnyOfThese",
            "ifAllOfThese",
            "ifNoneOfThese",
            "ifPartnerAnyOfThese"
        })
        cbRule.DropDownStyle = ComboBoxStyle.DropDownList
        cbRule.SelectedIndex = 0
        tpVictimTraitModifiers.Controls.Add(cbRule)

        ' Trait List
        Dim traitListLabel As New Label
        traitListLabel.Text = "Trait List"
        traitListLabel.Top = 60
        traitListLabel.Left = 10
        tpVictimTraitModifiers.Controls.Add(traitListLabel)

        Dim lstTraits As New ListBox
        lstTraits.Name = "lstTraits"
        lstTraits.Top = 80
        lstTraits.Left = 10
        lstTraits.Width = 300
        lstTraits.Height = 100
        tpVictimTraitModifiers.Controls.Add(lstTraits)
        lstTraits.BringToFront()

        Dim cbTrait As New ComboBox
        cbTrait.Name = "cbTrait"
        cbTrait.Top = 190
        cbTrait.Left = 10
        cbTrait.Width = 200
        Dim itemLines As String() = My.Resources.charactertrait.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLines
            cbTrait.Items.Add(item.Trim())
        Next
        cbTrait.DropDownStyle = ComboBoxStyle.DropDown
        tpVictimTraitModifiers.Controls.Add(cbTrait)

        Dim btnAddTrait As New Button
        btnAddTrait.Text = "Add Trait"
        btnAddTrait.Top = 190
        btnAddTrait.Left = 220
        AddHandler btnAddTrait.Click, Sub(s, ev)
                                          If cbTrait.SelectedItem IsNot Nothing Then
                                              lstTraits.Items.Add(cbTrait.SelectedItem.ToString())
                                              cbTrait.SelectedIndex = -1 ' Reset selection
                                          End If
                                      End Sub
        tpVictimTraitModifiers.Controls.Add(btnAddTrait)

        ' Remove Job Button
        Dim btnRemoveJob As New Button
        btnRemoveJob.Text = "Remove Trait"
        btnRemoveJob.Top = 190
        btnRemoveJob.Left = 300 ' Position next to the Add Job button
        AddHandler btnRemoveJob.Click, Sub(s, ev)
                                           If lstTraits.SelectedItem IsNot Nothing Then
                                               lstTraits.Items.Remove(lstTraits.SelectedItem)
                                           End If
                                       End Sub
        tpVictimTraitModifiers.Controls.Add(btnRemoveJob)

        ' Must Pass For Application
        Dim mustPassLabel As New Label
        mustPassLabel.Text = "Must Pass For Application"
        mustPassLabel.Top = 223
        mustPassLabel.Left = 10
        mustPassLabel.Width = 150
        tpVictimTraitModifiers.Controls.Add(mustPassLabel)


        Dim cbMustPass As New ComboBox
        cbMustPass.Name = "cbMustPass"
        cbMustPass.Top = 220
        cbMustPass.Left = 200
        cbMustPass.Width = 100
        cbMustPass.Items.AddRange(New String() {
            "true",
            "false"
        })
        cbMustPass.DropDownStyle = ComboBoxStyle.DropDownList
        cbMustPass.SelectedIndex = 1 ' Set default to "False"
        tpVictimTraitModifiers.Controls.Add(cbMustPass)

        ' Score Modifier
        Dim scoreModifierLabel As New Label
        scoreModifierLabel.Text = "Score Modifier"
        scoreModifierLabel.Top = 263
        scoreModifierLabel.Left = 10
        tpVictimTraitModifiers.Controls.Add(scoreModifierLabel)

        Dim numScoreModifier As New NumericUpDown
        numScoreModifier.Name = "numScoreModifier"
        numScoreModifier.Top = 260
        numScoreModifier.Left = 200
        numScoreModifier.Width = 100
        numScoreModifier.Minimum = -100 ' Adjust as needed
        numScoreModifier.Maximum = 100 ' Adjust as needed
        tpVictimTraitModifiers.Controls.Add(numScoreModifier)

        Dim lblCopyFrom As New Label
        lblCopyFrom.Text = "Copy From"
        lblCopyFrom.Top = 293
        lblCopyFrom.Left = 10
        lblCopyFrom.Width = 80
        tpVictimTraitModifiers.Controls.Add(lblCopyFrom)

        Dim cbCopyFrom As New ComboBox
        cbCopyFrom.Name = "cbCopyFrom"
        cbCopyFrom.Top = 290
        cbCopyFrom.Left = 90
        cbCopyFrom.Width = 210
        cbCopyFrom.Items.Add("null")
        Dim itemLinesCopy As String() = My.Resources.murdermo.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLinesCopy
            cbCopyFrom.Items.Add(item.Trim())
        Next
        cbCopyFrom.DropDownStyle = ComboBoxStyle.DropDown
        cbCopyFrom.SelectedIndex = 0 ' Default to null
        tpVictimTraitModifiers.Controls.Add(cbCopyFrom)

        ' Add a Remove button inside the TabPage
        Dim btnRemove As New Button
        btnRemove.Text = "Remove"
        btnRemove.Top = 20
        btnRemove.Left = 355
        AddHandler btnRemove.Click, Sub(s, ev)
                                        victimTraitEntryCount -= 1
                                        ' Get the index of the current tab
                                        Dim currentIndex As Integer = tabControlCase.TabPages.IndexOf(tpVictimTraitModifiers)

                                        ' Remove the TabPage from the TabControl
                                        tabControlCase.TabPages.Remove(tpVictimTraitModifiers)

                                        ' Select the tab below the current one if it exists
                                        If currentIndex < tabControlCase.TabPages.Count Then
                                            tabControlCase.SelectedIndex = currentIndex
                                        ElseIf currentIndex > 0 Then
                                            tabControlCase.SelectedIndex = currentIndex - 1
                                        End If
                                    End Sub
        tpVictimTraitModifiers.Controls.Add(btnRemove)

        Return tpVictimTraitModifiers
    End Function
    Private Sub LoadVictimTraitModifiersTab(jsonData As JObject, tabPage As TabPage)
        Dim entryIndex As Integer = Integer.Parse(tabPage.Text.Split(" "c).Last())
        tabPage.Text = "Victim Trait Modifiers " & entryIndex

        Dim cbRule As ComboBox = CType(tabPage.Controls("cbRule"), ComboBox)
        Dim ruleValue = jsonData("rule")

        If TypeOf ruleValue Is JValue AndAlso ruleValue.Type = JTokenType.Integer Then
            Dim index As Integer = Convert.ToInt32(ruleValue)
            If index >= 0 AndAlso index < cbRule.Items.Count Then
                cbRule.SelectedIndex = index
            End If
        Else
            cbRule.SelectedItem = ruleValue?.ToString()
        End If

        Dim lstTraits As ListBox = CType(tabPage.Controls("lstTraits"), ListBox)
        lstTraits.Items.Clear()

        For Each trait In jsonData("traitList")
            If TypeOf trait Is JValue Then
                Dim traitName As String = trait.ToString()

                ' Remove the prefix if it exists
                If traitName.StartsWith("REF:CharacterTrait|") Then
                    traitName = traitName.Replace("REF:CharacterTrait|", "") ' Remove "REF:" prefix
                End If

                lstTraits.Items.Add(traitName)
            End If
        Next

        Dim cbMustPass As ComboBox = CType(tabPage.Controls("cbMustPass"), ComboBox)
        Dim mustPassValue = jsonData("mustPassForApplication")
        Dim mustPassBool As Boolean = ConvertToBool(mustPassValue)
        cbMustPass.SelectedItem = If(mustPassBool, "true", "false")

        Dim numScoreModifier As NumericUpDown = CType(tabPage.Controls("numScoreModifier"), NumericUpDown)
        numScoreModifier.Value = If(jsonData("scoreModifier") IsNot Nothing, CInt(jsonData("scoreModifier").ToObject(Of Decimal)()), 0)

        ' Copy From ComboBox (set text directly)
        Dim cbCopyFrom As ComboBox = CType(tabPage.Controls("cbCopyFrom"), ComboBox)
        ' Retrieve the original value and remove the prefix if it exists
        If jsonData("copyFrom") IsNot Nothing Then
            Dim copyFromValue As String = jsonData("copyFrom").ToString()
            If copyFromValue.StartsWith("REF:MurderMO|") Then
                copyFromValue = copyFromValue.Replace("REF:MurderMO|", "")
                If Not cbCopyFrom.Items.Contains(copyFromValue) Then
                    cbCopyFrom.Items.Add(copyFromValue)
                    cbCopyFrom.SelectedItem = copyFromValue
                Else
                    SetComboBoxText(cbCopyFrom, copyFromValue)
                End If
            ElseIf copyFromValue = "" Or JTokenType.Null Or Nothing Then
                cbCopyFrom.SelectedIndex = 0
            End If
        Else
            cbCopyFrom.SelectedIndex = 0
        End If
    End Sub
    Private Sub btnAddVictimJobModifier_Click(sender As Object, e As EventArgs) Handles btnAddVictimJobModifier.Click
        Dim tpVictimJobModifier As TabPage = CreateVictimJobModifierTab()
        tabControlCase.TabPages.Add(tpVictimJobModifier)
        tabControlCase.SelectedTab = tpVictimJobModifier
    End Sub
    Private Function CreateVictimJobModifierTab() As TabPage
        victimJobModifierEntryCount += 1
        Dim tpJobModifier As New TabPage
        tpJobModifier.Text = "Victim Job Modifiers " & victimJobModifierEntryCount

        ' Job Label and ComboBox
        Dim jobLabel As New Label
        jobLabel.Name = "lblJob"
        jobLabel.Text = "Job"
        jobLabel.Top = 23
        jobLabel.Left = 10
        jobLabel.Width = 30
        tpJobModifier.Controls.Add(jobLabel)

        Dim cbJob As New ComboBox
        cbJob.Name = "cbJob"
        cbJob.Top = 20
        cbJob.Left = 50
        cbJob.Width = 200
        Dim itemLines As String() = My.Resources.jobpreset.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLines
            cbJob.Items.Add(item.Trim())
        Next
        cbJob.DropDownStyle = ComboBoxStyle.DropDown
        tpJobModifier.Controls.Add(cbJob)

        ' ListBox for selected jobs
        Dim lstJobs As New ListBox
        lstJobs.Name = "lstJobs"
        lstJobs.Top = 60
        lstJobs.Left = 10
        lstJobs.Width = 300
        lstJobs.Height = 100
        tpJobModifier.Controls.Add(lstJobs)

        ' Add Job Button
        Dim btnAddJob As New Button
        btnAddJob.Text = "Add"
        btnAddJob.Top = 170
        btnAddJob.Left = 10
        AddHandler btnAddJob.Click, Sub(s, ev)
                                        If cbJob.SelectedItem IsNot Nothing Then
                                            lstJobs.Items.Add(cbJob.SelectedItem.ToString())
                                            cbJob.SelectedIndex = -1 ' Reset selection
                                        End If
                                    End Sub
        tpJobModifier.Controls.Add(btnAddJob)

        ' Remove Job Button
        Dim btnRemoveJob As New Button
        btnRemoveJob.Text = "Remove Job"
        btnRemoveJob.Top = 170
        btnRemoveJob.Left = 90 ' Position next to the Add Job button
        AddHandler btnRemoveJob.Click, Sub(s, ev)
                                           If lstJobs.SelectedItem IsNot Nothing Then
                                               lstJobs.Items.Remove(lstJobs.SelectedItem)
                                           End If
                                       End Sub
        tpJobModifier.Controls.Add(btnRemoveJob)

        ' Job Boost Label and NumericUpDown
        Dim lblJobBoost As New Label
        lblJobBoost.Text = "Job Boost"
        lblJobBoost.Top = 213
        lblJobBoost.Left = 10
        lblJobBoost.Width = 80
        tpJobModifier.Controls.Add(lblJobBoost)

        Dim numJobBoost As New NumericUpDown
        numJobBoost.Name = "numJobBoost"
        numJobBoost.Top = 210
        numJobBoost.Left = 90
        numJobBoost.Width = 100
        numJobBoost.Minimum = 0
        numJobBoost.Maximum = 100
        tpJobModifier.Controls.Add(numJobBoost)


        Dim lblCopyFrom As New Label
        lblCopyFrom.Text = "Copy From"
        lblCopyFrom.Top = 243
        lblCopyFrom.Left = 10
        lblCopyFrom.Width = 80
        tpJobModifier.Controls.Add(lblCopyFrom)

        Dim cbCopyFrom As New ComboBox
        cbCopyFrom.Name = "cbCopyFrom"
        cbCopyFrom.Top = 240
        cbCopyFrom.Left = 90
        cbCopyFrom.Width = 200
        Dim itemLinesCopy As String() = My.Resources.murdermo.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLinesCopy
            cbCopyFrom.Items.Add(item.Trim())
        Next
        cbCopyFrom.DropDownStyle = ComboBoxStyle.DropDown
        cbCopyFrom.SelectedIndex = 0 ' Default to null
        tpJobModifier.Controls.Add(cbCopyFrom)
        ' Add a Remove button inside the TabPage
        Dim btnRemove As New Button
        btnRemove.Text = "Remove"
        btnRemove.Top = 20
        btnRemove.Left = 355
        AddHandler btnRemove.Click, Sub(s, ev)
                                        murdererJobModifierEntryCount -= 1

                                        Dim currentIndex As Integer = tabControlCase.TabPages.IndexOf(tpJobModifier)
                                        tabControlCase.TabPages.Remove(tpJobModifier)

                                        If currentIndex < tabControlCase.TabPages.Count Then
                                            tabControlCase.SelectedIndex = currentIndex
                                        ElseIf currentIndex > 0 Then
                                            tabControlCase.SelectedIndex = currentIndex - 1
                                        End If
                                    End Sub
        tpJobModifier.Controls.Add(btnRemove)

        Return tpJobModifier
    End Function
    Private Sub LoadVictimJobModifierTab(jsonData As JObject, tabPage As TabPage)
        ' Ensure the tab has the correct name
        Dim entryIndex As Integer = Integer.Parse(tabPage.Text.Split(" "c).Last()) ' Extract the index from the tab name
        tabPage.Text = "Victim Job Modifiers " & entryIndex

        ' Jobs ListBox
        Dim lstJobs As ListBox = CType(tabPage.Controls("lstJobs"), ListBox)
        lstJobs.Items.Clear()
        For Each job In jsonData("jobs")
            If TypeOf job Is JValue Then
                Dim jobName As String = job.ToString()

                ' Remove the prefix if it exists
                If jobName.StartsWith("REF:OccupationPreset|") Then
                    jobName = jobName.Replace("REF:OccupationPreset|", "")
                End If

                lstJobs.Items.Add(jobName)
            End If
        Next

        ' Job Boost NumericUpDown
        Dim numJobBoost As NumericUpDown = CType(tabPage.Controls("numJobBoost"), NumericUpDown)
        numJobBoost.Value = If(jsonData("jobBoost") IsNot Nothing, CInt(jsonData("jobBoost").ToObject(Of Decimal)()), 0)

        ' Copy From ComboBox (set text directly)
        Dim cbCopyFrom As ComboBox = CType(tabPage.Controls("cbCopyFrom"), ComboBox)
        ' Retrieve the original value and remove the prefix if it exists
        If jsonData("copyFrom") IsNot Nothing Then
            Dim copyFromValue As String = jsonData("copyFrom").ToString()
            If copyFromValue.StartsWith("REF:MurderMO|") Then
                copyFromValue = copyFromValue.Replace("REF:MurderMO|", "")
                If Not cbCopyFrom.Items.Contains(copyFromValue) Then
                    cbCopyFrom.Items.Add(copyFromValue)
                    cbCopyFrom.SelectedItem = copyFromValue
                Else
                    SetComboBoxText(cbCopyFrom, copyFromValue)
                End If
            ElseIf copyFromValue = "" Or JTokenType.Null Or Nothing Then
                cbCopyFrom.SelectedIndex = 0
            End If
        Else
            cbCopyFrom.SelectedIndex = 0
        End If
    End Sub
    Private Sub btnAddVictimCompanyModifier_Click(sender As Object, e As EventArgs) Handles btnAddVictimCompanyModifier.Click
        Dim tpVictimCompanyModifier As TabPage = CreateVictimCompanyModifierTab()
        tabControlCase.TabPages.Add(tpVictimCompanyModifier)
        tabControlCase.SelectedTab = tpVictimCompanyModifier
    End Sub
    Private Function CreateVictimCompanyModifierTab() As TabPage
        victimCompanyModifierEntryCount += 1
        Dim tpCompanyModifier As New TabPage
        tpCompanyModifier.Text = "Victim Company Modifiers " & victimCompanyModifierEntryCount

        ' Companies Label and ComboBox
        Dim companiesLabel As New Label
        companiesLabel.Text = "Company"
        companiesLabel.Top = 23
        companiesLabel.Left = 10
        companiesLabel.Width = 60
        tpCompanyModifier.Controls.Add(companiesLabel)

        Dim cbCompanies As New ComboBox
        cbCompanies.Name = "cbCompanies"
        cbCompanies.Top = 20
        cbCompanies.Left = 80
        cbCompanies.Width = 200
        Dim itemLines As String() = My.Resources.companypreset.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLines
            cbCompanies.Items.Add(item.Trim())
        Next
        cbCompanies.DropDownStyle = ComboBoxStyle.DropDown
        tpCompanyModifier.Controls.Add(cbCompanies)

        ' ListBox for selected companies
        Dim lstCompanies As New ListBox
        lstCompanies.Name = "lstCompanies"
        lstCompanies.Top = 60
        lstCompanies.Left = 10
        lstCompanies.Width = 300
        lstCompanies.Height = 100
        tpCompanyModifier.Controls.Add(lstCompanies)

        ' Add Company Button
        Dim btnAddCompany As New Button
        btnAddCompany.Text = "Add Company"
        btnAddCompany.Top = 170
        btnAddCompany.Left = 10
        AddHandler btnAddCompany.Click, Sub(s, ev)
                                            If cbCompanies.SelectedItem IsNot Nothing Then
                                                lstCompanies.Items.Add(cbCompanies.SelectedItem.ToString())
                                                cbCompanies.SelectedIndex = -1 ' Reset selection
                                            End If
                                        End Sub
        tpCompanyModifier.Controls.Add(btnAddCompany)

        ' Remove Company Button
        Dim btnRemoveCompany As New Button
        btnRemoveCompany.Text = "Remove Company"
        btnRemoveCompany.Top = 170
        btnRemoveCompany.Left = 120 ' Position next to the Add Company button
        AddHandler btnRemoveCompany.Click, Sub(s, ev)
                                               If lstCompanies.SelectedItem IsNot Nothing Then
                                                   lstCompanies.Items.Remove(lstCompanies.SelectedItem)
                                               End If
                                           End Sub
        tpCompanyModifier.Controls.Add(btnRemoveCompany)

        ' Minimum Employees Label and NumericUpDown
        Dim lblMinimumEmployees As New Label
        lblMinimumEmployees.Text = "Minimum Employees"
        lblMinimumEmployees.Top = 213
        lblMinimumEmployees.Left = 10
        lblMinimumEmployees.Width = 120
        tpCompanyModifier.Controls.Add(lblMinimumEmployees)

        Dim nudMinimumEmployees As New NumericUpDown
        nudMinimumEmployees.Name = "nudMinimumEmployees"
        nudMinimumEmployees.Top = 210
        nudMinimumEmployees.Left = 140
        nudMinimumEmployees.Width = 100
        nudMinimumEmployees.Minimum = 0
        nudMinimumEmployees.Maximum = 1000
        tpCompanyModifier.Controls.Add(nudMinimumEmployees)

        ' Company Boost Label and NumericUpDown
        Dim lblCompanyBoost As New Label
        lblCompanyBoost.Text = "Company Boost"
        lblCompanyBoost.Top = 243
        lblCompanyBoost.Left = 10
        lblCompanyBoost.Width = 100
        tpCompanyModifier.Controls.Add(lblCompanyBoost)

        Dim nudCompanyBoost As New NumericUpDown
        nudCompanyBoost.Name = "nudCompanyBoost"
        nudCompanyBoost.Top = 240
        nudCompanyBoost.Left = 140
        nudCompanyBoost.Width = 100
        nudCompanyBoost.Minimum = -100
        nudCompanyBoost.Maximum = 1000
        tpCompanyModifier.Controls.Add(nudCompanyBoost)

        ' Boost Per Employee Label and NumericUpDown
        Dim lblBoostPerEmployee As New Label
        lblBoostPerEmployee.Text = "Boost Per Employee"
        lblBoostPerEmployee.Top = 273
        lblBoostPerEmployee.Left = 10
        lblBoostPerEmployee.Width = 130
        tpCompanyModifier.Controls.Add(lblBoostPerEmployee)

        Dim nudBoostPerEmployee As New NumericUpDown
        nudBoostPerEmployee.Name = "nudBoostPerEmployeeOverMinimum"
        nudBoostPerEmployee.Top = 270
        nudBoostPerEmployee.Left = 140
        nudBoostPerEmployee.Width = 100
        nudBoostPerEmployee.Minimum = -100
        nudBoostPerEmployee.Maximum = 1000
        tpCompanyModifier.Controls.Add(nudBoostPerEmployee)

        ' Copy From Label and ComboBox
        Dim lblCopyFrom As New Label
        lblCopyFrom.Text = "Copy From"
        lblCopyFrom.Top = 303
        lblCopyFrom.Left = 10
        lblCopyFrom.Width = 80
        tpCompanyModifier.Controls.Add(lblCopyFrom)

        Dim cbCopyFrom As New ComboBox
        cbCopyFrom.Name = "cbCopyFrom"
        cbCopyFrom.Top = 300
        cbCopyFrom.Left = 90
        cbCopyFrom.Width = 200
        Dim itemLinesCopy As String() = My.Resources.murdermo.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLinesCopy
            cbCopyFrom.Items.Add(item.Trim())
        Next
        cbCopyFrom.DropDownStyle = ComboBoxStyle.DropDown
        cbCopyFrom.SelectedIndex = 0 ' Default to null
        tpCompanyModifier.Controls.Add(cbCopyFrom)

        ' Add a Remove button inside the TabPage
        Dim btnRemove As New Button
        btnRemove.Text = "Remove"
        btnRemove.Top = 20
        btnRemove.Left = 355
        AddHandler btnRemove.Click, Sub(s, ev)
                                        victimCompanyModifierEntryCount -= 1

                                        Dim currentIndex As Integer = tabControlCase.TabPages.IndexOf(tpCompanyModifier)
                                        tabControlCase.TabPages.Remove(tpCompanyModifier)

                                        If currentIndex < tabControlCase.TabPages.Count Then
                                            tabControlCase.SelectedIndex = currentIndex
                                        ElseIf currentIndex > 0 Then
                                            tabControlCase.SelectedIndex = currentIndex - 1
                                        End If
                                    End Sub
        tpCompanyModifier.Controls.Add(btnRemove)

        Return tpCompanyModifier
    End Function
    Private Sub LoadVictimCompanyModifierTab(jsonData As JObject, tabPage As TabPage)
        ' Ensure the tab has the correct name
        Dim entryIndex As Integer = Integer.Parse(tabPage.Text.Split(" "c).Last()) ' Extract the index from the tab name
        tabPage.Text = "Victim Company Modifiers " & entryIndex

        ' Companies ListBox
        Dim lstCompanies As ListBox = CType(tabPage.Controls("lstCompanies"), ListBox)
        lstCompanies.Items.Clear()
        For Each company In jsonData("companies")
            If TypeOf company Is JValue Then
                Dim companyName As String = company.ToString()

                ' Remove the prefix if it exists
                If companyName.StartsWith("REF:CompanyPreset|") Then
                    companyName = companyName.Replace("REF:CompanyPreset|", "")
                End If

                lstCompanies.Items.Add(companyName)
            End If
        Next

        ' Minimum Employees NumericUpDown
        Dim nudMinimumEmployees As NumericUpDown = CType(tabPage.Controls("nudMinimumEmployees"), NumericUpDown)
        nudMinimumEmployees.Value = If(jsonData("minimumEmployees") IsNot Nothing, CInt(jsonData("minimumEmployees").ToObject(Of Decimal)()), 0)

        ' Company Boost NumericUpDown
        Dim nudCompanyBoost As NumericUpDown = CType(tabPage.Controls("nudCompanyBoost"), NumericUpDown)
        nudCompanyBoost.Value = If(jsonData("companyBoost") IsNot Nothing, CInt(jsonData("companyBoost").ToObject(Of Decimal)()), 0)

        ' Boost Per Employee Over Minimum NumericUpDown
        Dim nudBoostPerEmployee As NumericUpDown = CType(tabPage.Controls("nudBoostPerEmployeeOverMinimum"), NumericUpDown)
        nudBoostPerEmployee.Value = If(jsonData("boostPerEmployeeOverMinimum") IsNot Nothing, CInt(jsonData("boostPerEmployeeOverMinimum").ToObject(Of Decimal)()), 0)

        ' Copy From ComboBox (set text directly)
        Dim cbCopyFrom As ComboBox = CType(tabPage.Controls("cbCopyFrom"), ComboBox)
        ' Retrieve the original value and remove the prefix if it exists
        If jsonData("copyFrom") IsNot Nothing Then
            Dim copyFromValue As String = jsonData("copyFrom").ToString()
            If copyFromValue.StartsWith("REF:MurderMO|") Then
                copyFromValue = copyFromValue.Replace("REF:MurderMO|", "")
                If Not cbCopyFrom.Items.Contains(copyFromValue) Then
                    cbCopyFrom.Items.Add(copyFromValue)
                    cbCopyFrom.SelectedItem = copyFromValue
                Else
                    SetComboBoxText(cbCopyFrom, copyFromValue)
                End If
            ElseIf copyFromValue = "" Or JTokenType.Null Or Nothing Then
                cbCopyFrom.SelectedIndex = 0
            End If
        Else
            cbCopyFrom.SelectedIndex = 0
        End If
    End Sub
    Private Sub btnAddConfessionalTab_Click(sender As Object, e As EventArgs) Handles btnAddConfessionalTab.Click
        If ddsConfessionalTabOpen = False Then
            Dim tpConfessionalDDS As TabPage = CreateConfessionalDDSTab()
            tabControlCase.TabPages.Add(tpConfessionalDDS)
            tabControlCase.SelectedTab = tpConfessionalDDS
        Else
            MessageBox.Show("You already have a DDS Confessional entry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Function CreateConfessionalDDSTab() As TabPage
        ddsConfessionalTabOpen = True
        ' Create a new TabPage for Confessional DDS
        Dim tpConfessionalDDS As New TabPage("Confessional DDS")

        ' Create a label for the DDS string input
        Dim lblDDSString As New Label()
        lblDDSString.Text = "DDS String:"
        lblDDSString.Top = 10
        lblDDSString.Left = 10
        tpConfessionalDDS.Controls.Add(lblDDSString)

        ' Create a TextBox for entering the DDS string
        Dim txtDDSString As New TextBox()
        txtDDSString.Name = "txtDDSString"
        txtDDSString.Top = 30
        txtDDSString.Left = 10
        txtDDSString.Width = 250
        tpConfessionalDDS.Controls.Add(txtDDSString)

        ' Create a ListBox for displaying the stored DDS responses
        Dim lstDDSResponses As New ListBox()
        lstDDSResponses.Name = "lstDDSResponses"
        lstDDSResponses.Top = 60
        lstDDSResponses.Left = 10
        lstDDSResponses.Width = 250
        lstDDSResponses.Height = 150
        tpConfessionalDDS.Controls.Add(lstDDSResponses)

        ' Create a Button to add the DDS string to the ListBox
        Dim btnAddDDS As New Button()
        btnAddDDS.Text = "Add DDS"
        btnAddDDS.Top = 220
        btnAddDDS.Left = 10
        AddHandler btnAddDDS.Click, Sub(sender, e)
                                        If Not String.IsNullOrWhiteSpace(txtDDSString.Text) Then
                                            lstDDSResponses.Items.Add(txtDDSString.Text)
                                            txtDDSString.Clear() ' Clear the TextBox after adding
                                        End If
                                    End Sub
        tpConfessionalDDS.Controls.Add(btnAddDDS)

        ' Create a Button to remove the selected DDS string from the ListBox
        Dim btnRemoveDDS As New Button()
        btnRemoveDDS.Text = "Remove Selected"
        btnRemoveDDS.Top = 220
        btnRemoveDDS.Left = 100
        AddHandler btnRemoveDDS.Click, Sub(sender, e)
                                           If lstDDSResponses.SelectedIndex >= 0 Then
                                               lstDDSResponses.Items.RemoveAt(lstDDSResponses.SelectedIndex)
                                           End If
                                       End Sub
        tpConfessionalDDS.Controls.Add(btnRemoveDDS)

        Dim btnRemove As New Button
        btnRemove.Text = "Remove"
        btnRemove.Top = 20
        btnRemove.Left = 355
        AddHandler btnRemove.Click, Sub(s, ev)
                                        ddsConfessionalTabOpen = False

                                        Dim currentIndex As Integer = tabControlCase.TabPages.IndexOf(tpConfessionalDDS)
                                        tabControlCase.TabPages.Remove(tpConfessionalDDS)

                                        If currentIndex < tabControlCase.TabPages.Count Then
                                            tabControlCase.SelectedIndex = currentIndex
                                        ElseIf currentIndex > 0 Then
                                            tabControlCase.SelectedIndex = currentIndex - 1
                                        End If
                                    End Sub
        tpConfessionalDDS.Controls.Add(btnRemove)

        Return tpConfessionalDDS
    End Function
    Private Sub LoadDDSConfessionalTab(jsonData As JObject, tabPage As TabPage)
        tabPage.Text = "Confessional DDS"

        Dim lstDDS As ListBox = CType(tabPage.Controls("lstDDSResponses"), ListBox)

        lstDDS.Items.Clear()

        If jsonData.ContainsKey("confessionalDDSResponses") Then
            For Each dds In jsonData("confessionalDDSResponses")
                lstDDS.Items.Add(dds.ToString())
            Next
        End If
    End Sub
    Private Sub btnAddGraffiti_Click(sender As Object, e As EventArgs) Handles btnAddGraffiti.Click
        Dim tpGraffitiTab As TabPage = CreateGraffitiTab()
        tabControlCase.TabPages.Add(tpGraffitiTab)
        tabControlCase.SelectedTab = tpGraffitiTab
    End Sub
    Private Function CreateGraffitiTab() As TabPage
        graffitiEntryCount += 1
        Dim tpGraffiti As New TabPage
        tpGraffiti.Text = "Graffiti Entry " & graffitiEntryCount

        ' Preset Label
        Dim presetLabel As New Label
        presetLabel.Text = "Preset"
        presetLabel.Top = 23
        presetLabel.Left = 0
        presetLabel.Width = 100
        tpGraffiti.Controls.Add(presetLabel)

        ' Preset ComboBox
        Dim cbPreset As New ComboBox
        cbPreset.Top = 20
        cbPreset.Left = 150
        cbPreset.Width = 200
        cbPreset.Name = "cbPreset"
        Dim itemLines As String() = My.Resources.interactablepreset.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLines
            cbPreset.Items.Add(item.Trim())
        Next
        cbPreset.DropDownStyle = ComboBoxStyle.DropDown
        cbPreset.SelectedIndex = 0
        tpGraffiti.Controls.Add(cbPreset)

        ' Pos Label
        Dim posLabel As New Label
        posLabel.Text = "Pos"
        posLabel.Top = 53
        posLabel.Left = 0
        posLabel.Width = 100
        tpGraffiti.Controls.Add(posLabel)

        ' Pos ComboBox
        Dim cbPos As New ComboBox
        cbPos.Top = 50
        cbPos.Left = 150
        cbPos.Width = 200
        cbPos.Name = "cbPos"
        cbPos.Items.AddRange(New String() {"victim", "nearbyWall"})
        cbPos.DropDownStyle = ComboBoxStyle.DropDownList
        cbPos.SelectedIndex = 0
        tpGraffiti.Controls.Add(cbPos)

        ' Art Image Label
        Dim artImageLabel As New Label
        artImageLabel.Text = "Art Image"
        artImageLabel.Top = 83
        artImageLabel.Left = 0
        artImageLabel.Width = 100
        tpGraffiti.Controls.Add(artImageLabel)

        ' Art Image ComboBox
        Dim cbArtImage As New ComboBox
        cbArtImage.Top = 80
        cbArtImage.Left = 150
        cbArtImage.Width = 200
        cbArtImage.Name = "cbArtImage"
        Dim itemLinesArt As String() = My.Resources.artpreset.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLinesArt
            cbArtImage.Items.Add(item.Trim())
        Next
        cbArtImage.DropDownStyle = ComboBoxStyle.DropDown
        cbArtImage.SelectedIndex = 0
        tpGraffiti.Controls.Add(cbArtImage)

        ' DDS Message Text List Label
        Dim ddsMessageTextLabel As New Label
        ddsMessageTextLabel.Text = "DDS Message Text List"
        ddsMessageTextLabel.Top = 113
        ddsMessageTextLabel.Left = 0
        ddsMessageTextLabel.Width = 150
        tpGraffiti.Controls.Add(ddsMessageTextLabel)

        ' DDS Message Text List TextBox
        Dim txtDDSMessageText As New TextBox
        txtDDSMessageText.Top = 110
        txtDDSMessageText.Left = 150
        txtDDSMessageText.Width = 200
        txtDDSMessageText.Name = "txtDDSMessageText"
        tpGraffiti.Controls.Add(txtDDSMessageText)

        ' Color Label
        Dim colorLabel As New Label
        colorLabel.Text = "Color"
        colorLabel.Top = 143
        colorLabel.Left = 0
        colorLabel.Width = 100
        tpGraffiti.Controls.Add(colorLabel)

        ' Color TextBox
        Dim txtColor As New TextBox
        txtColor.Top = 140
        txtColor.Left = 150
        txtColor.Width = 200
        txtColor.Name = "txtColor"
        tpGraffiti.Controls.Add(txtColor)

        ' Size Label
        Dim sizeLabel As New Label
        sizeLabel.Text = "Size"
        sizeLabel.Top = 173
        sizeLabel.Left = 0
        sizeLabel.Width = 100
        tpGraffiti.Controls.Add(sizeLabel)

        ' Size NumericUpDown
        Dim numSize As New NumericUpDown
        numSize.Top = 170
        numSize.Left = 150
        numSize.Width = 200
        numSize.Name = "numSize"
        numSize.Minimum = 0
        numSize.Maximum = 1000
        tpGraffiti.Controls.Add(numSize)

        ' Copy From Label
        Dim copyFromLabel As New Label
        copyFromLabel.Text = "Copy From"
        copyFromLabel.Top = 203
        copyFromLabel.Left = 0
        copyFromLabel.Width = 100
        tpGraffiti.Controls.Add(copyFromLabel)

        ' Copy From ComboBox
        Dim cbCopyFrom As New ComboBox
        cbCopyFrom.Top = 200
        cbCopyFrom.Left = 150
        cbCopyFrom.Width = 200
        cbCopyFrom.Name = "cbCopyFrom"
        cbCopyFrom.Items.Add("null")
        Dim itemLinesCopy As String() = My.Resources.murdermo.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLinesCopy
            cbCopyFrom.Items.Add(item.Trim())
        Next
        cbCopyFrom.DropDownStyle = ComboBoxStyle.DropDown
        cbCopyFrom.SelectedIndex = 0
        tpGraffiti.Controls.Add(cbCopyFrom)

        ' Add the Remove button
        Dim btnRemove As New Button
        btnRemove.Text = "Remove"
        btnRemove.Top = 20
        btnRemove.Left = 355
        AddHandler btnRemove.Click, Sub(s, ev)
                                        graffitiEntryCount -= 1
                                        Dim currentIndex As Integer = tabControlCase.TabPages.IndexOf(tpGraffiti)
                                        tabControlCase.TabPages.Remove(tpGraffiti)
                                        If currentIndex < tabControlCase.TabPages.Count Then
                                            tabControlCase.SelectedIndex = currentIndex
                                        ElseIf currentIndex > 0 Then
                                            tabControlCase.SelectedIndex = currentIndex - 1
                                        End If
                                    End Sub
        tpGraffiti.Controls.Add(btnRemove)

        Return tpGraffiti
    End Function
    Private Sub LoadGraffitiTab(jsonData As JObject, tabPage As TabPage)
        ' Ensure the tab has the correct name
        Dim entryIndex As Integer = Integer.Parse(tabPage.Text.Split(" "c).Last()) ' Extract the index from the tab name
        tabPage.Text = "Graffiti Entry " & entryIndex

        ' Preset ComboBox
        Dim cbPreset As ComboBox = CType(tabPage.Controls("cbPreset"), ComboBox)
        ' Retrieve the original value and remove the prefix if it exists
        Dim cbPresetValue As String = jsonData("preset").ToString()
        If cbPresetValue.StartsWith("REF:InteractablePreset|") Then
            cbPresetValue = cbPresetValue.Replace("REF:InteractablePreset|", "")
            If Not cbPreset.Items.Contains(cbPresetValue) Then
                cbPreset.Items.Add(cbPresetValue)
                cbPreset.SelectedItem = cbPresetValue
            Else
                SetComboBoxText(cbPreset, cbPresetValue)
            End If
        ElseIf cbPresetValue = "" Or JTokenType.Null Or Nothing Then
            cbPreset.SelectedIndex = 0
        End If

        ' Pos ComboBox
        Dim cbPos As ComboBox = CType(tabPage.Controls("cbPos"), ComboBox)
        SetComboBoxValue(cbPos, jsonData("pos"))

        ' Art Image ComboBox
        Dim cbArtImage As ComboBox = CType(tabPage.Controls("cbArtImage"), ComboBox)
        ' Retrieve the original value and remove the prefix if it exists
        Dim cbArtImageValue As String = jsonData("artImage").ToString()
        If cbArtImageValue.StartsWith("REF:ArtPreset|") Then
            cbArtImageValue = cbArtImageValue.Replace("REF:ArtPreset|", "")
            If Not cbArtImage.Items.Contains(cbArtImageValue) Then
                cbArtImage.Items.Add(cbArtImageValue)
                cbArtImage.SelectedItem = cbArtImageValue
            Else
                SetComboBoxText(cbArtImage, cbArtImageValue)
            End If
        ElseIf cbArtImageValue = "" Or JTokenType.Null Or Nothing Then
            cbArtImage.SelectedIndex = 0
        End If

        ' DDS Message Text List TextBox
        Dim txtDDSMessageText As TextBox = CType(tabPage.Controls("txtDDSMessageText"), TextBox)
        txtDDSMessageText.Text = If(jsonData("ddsMessageTextList")?.ToString(), String.Empty)

        ' Color TextBox
        Dim txtColor As TextBox = CType(tabPage.Controls("txtColor"), TextBox)
        txtColor.Text = If(jsonData("color")?.ToString(), String.Empty)

        ' Size NumericUpDown
        Dim numSize As NumericUpDown = CType(tabPage.Controls("numSize"), NumericUpDown)
        numSize.Value = If(jsonData("size") IsNot Nothing, CDec(jsonData("size")), 0)

        ' Copy From ComboBox (set text directly)
        Dim cbCopyFrom As ComboBox = CType(tabPage.Controls("cbCopyFrom"), ComboBox)
        ' Retrieve the original value and remove the prefix if it exists
        If jsonData("copyFrom") IsNot Nothing Then
            Dim copyFromValue As String = jsonData("copyFrom").ToString()
            If copyFromValue.StartsWith("REF:MurderMO|") Then
                copyFromValue = copyFromValue.Replace("REF:MurderMO|", "")
                If Not cbCopyFrom.Items.Contains(copyFromValue) Then
                    cbCopyFrom.Items.Add(copyFromValue)
                    cbCopyFrom.SelectedItem = copyFromValue
                Else
                    SetComboBoxText(cbCopyFrom, copyFromValue)
                End If
            ElseIf copyFromValue = "" Or JTokenType.Null Or Nothing Then
                cbCopyFrom.SelectedIndex = 0
            End If
        Else
            cbCopyFrom.SelectedIndex = 0
        End If
    End Sub
    Private Sub btnAddPlayerTauntTab_Click(sender As Object, e As EventArgs) Handles btnAddPlayerTauntTab.Click
        If playerTauntTabOpen = False Then
            Dim tpPlayerTaunt As TabPage = CreatePlayerTauntTab()
            tabControlCase.TabPages.Add(tpPlayerTaunt)
            tabControlCase.SelectedTab = tpPlayerTaunt
        Else
            MessageBox.Show("You already have a player taunt entry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Function CreatePlayerTauntTab() As TabPage
        playerTauntTabOpen = True
        Dim tpPlayerTaunt As New TabPage("Player Taunts")

        ' Create a label for the player taunt ComboBox
        Dim lblPlayerTaunt As New Label()
        lblPlayerTaunt.Text = "Select Taunt:"
        lblPlayerTaunt.Top = 10
        lblPlayerTaunt.Left = 10
        tpPlayerTaunt.Controls.Add(lblPlayerTaunt)

        ' Create a ComboBox for selecting player taunts
        Dim cbPlayerTaunt As New ComboBox()
        cbPlayerTaunt.Name = "cbPlayerTaunt"
        cbPlayerTaunt.Top = 30
        cbPlayerTaunt.Left = 10
        cbPlayerTaunt.Width = 250
        cbPlayerTaunt.DropDownStyle = ComboBoxStyle.DropDown
        Dim itemLines As String() = My.Resources.interactablepreset.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        For Each item As String In itemLines
            cbPlayerTaunt.Items.Add(item.Trim())
        Next
        cbPlayerTaunt.SelectedIndex = 0 ' Set default selection
        tpPlayerTaunt.Controls.Add(cbPlayerTaunt)
        cbPlayerTaunt.BringToFront()
        ' Create a ListBox for displaying the selected player taunts
        Dim lstPlayerTaunts As New ListBox()
        lstPlayerTaunts.Name = "lstPlayerTaunts"
        lstPlayerTaunts.Top = 60
        lstPlayerTaunts.Left = 10
        lstPlayerTaunts.Width = 250
        lstPlayerTaunts.Height = 150
        tpPlayerTaunt.Controls.Add(lstPlayerTaunts)

        ' Create a Button to add the selected taunt to the ListBox
        Dim btnAddTaunt As New Button()
        btnAddTaunt.Text = "Add Taunt"
        btnAddTaunt.Top = 220
        btnAddTaunt.Left = 10
        AddHandler btnAddTaunt.Click, Sub(sender, e)
                                          If cbPlayerTaunt.SelectedItem IsNot Nothing Then
                                              lstPlayerTaunts.Items.Add(cbPlayerTaunt.SelectedItem.ToString())
                                          End If
                                      End Sub
        tpPlayerTaunt.Controls.Add(btnAddTaunt)

        ' Create a Button to remove the selected taunt from the ListBox
        Dim btnRemoveTaunt As New Button()
        btnRemoveTaunt.Text = "Remove Selected"
        btnRemoveTaunt.Top = 220
        btnRemoveTaunt.Left = 100
        AddHandler btnRemoveTaunt.Click, Sub(sender, e)
                                             If lstPlayerTaunts.SelectedIndex >= 0 Then
                                                 lstPlayerTaunts.Items.RemoveAt(lstPlayerTaunts.SelectedIndex)
                                             End If
                                         End Sub
        tpPlayerTaunt.Controls.Add(btnRemoveTaunt)

        ' Create a Button to remove the Player Taunt tab
        Dim btnRemove As New Button()
        btnRemove.Text = "Remove Tab"
        btnRemove.Top = 20
        btnRemove.Left = 355
        AddHandler btnRemove.Click, Sub(s, ev)
                                        playerTauntTabOpen = False
                                        Dim currentIndex As Integer = tabControlCase.TabPages.IndexOf(tpPlayerTaunt)
                                        tabControlCase.TabPages.Remove(tpPlayerTaunt)

                                        If currentIndex < tabControlCase.TabPages.Count Then
                                            tabControlCase.SelectedIndex = currentIndex
                                        ElseIf currentIndex > 0 Then
                                            tabControlCase.SelectedIndex = currentIndex - 1
                                        End If
                                    End Sub
        tpPlayerTaunt.Controls.Add(btnRemove)

        Return tpPlayerTaunt
    End Function
    Private Sub LoadPlayerTauntTab(jsonData As JObject, tabPage As TabPage)
        tabPage.Text = "Player Taunts"

        Dim lstPlayerTaunts As ListBox = CType(tabPage.Controls("lstPlayerTaunts"), ListBox)
        lstPlayerTaunts.Items.Clear()

        For Each taunt In jsonData("playerTaunts")
            If TypeOf taunt Is JValue Then
                Dim tauntName As String = taunt.ToString()

                ' Remove the prefix if it exists
                If tauntName.StartsWith("REF:InteractablePreset|") Then
                    tauntName = tauntName.Replace("REF:InteractablePreset|", "") ' Remove "REF:" prefix
                End If

                lstPlayerTaunts.Items.Add(tauntName)
            End If
        Next
    End Sub
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        If isCaseMadeOrLoaded = True AndAlso isOutputChanged = True Then
            Dim result As DialogResult = MessageBox.Show("Save changes? Any unsaved changes will be lost.", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                SaveCase()
                OpenCase()
            ElseIf result = DialogResult.No Then
                OpenCase()
            ElseIf result = DialogResult.Cancel Then

            End If
        Else
            OpenCase()
        End If

    End Sub
    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        If isCaseMadeOrLoaded = True AndAlso isOutputChanged = True Then
            Dim result As DialogResult = MessageBox.Show("Save changes? Any unsaved changes will be lost.", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                SaveCase()
                CreateNewCase()
            ElseIf result = DialogResult.No Then
                CreateNewCase()
            ElseIf result = DialogResult.Cancel Then

            End If
        Else
            CreateNewCase()
        End If
    End Sub
    Private Sub OpenCase()
        ' Create and configure the OpenFileDialog
        Dim openFileDialog As New OpenFileDialog()

        ' Set filter options and title
        openFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"
        openFileDialog.Title = "Select a case"

        ' Show the dialog and check if the user clicked OK
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Get the file path from the dialog
            Dim filePath As String = openFileDialog.FileName
            Dim folderName As String = IO.Path.GetFileName(IO.Path.GetDirectoryName(filePath))

            tempFilePath = filePath
            tempFolderName = folderName
            ' Load the default JSON
            Dim defaultJson As JObject = JObject.Parse(GetDefaultJson())

            ' Load the JSON data from the file
            Dim loadedJsonData As JObject
            Try
                Dim jsonContent As String = File.ReadAllText(filePath)
                loadedJsonData = JObject.Parse(jsonContent)
            Catch ex As Exception
                MessageBox.Show("Error reading the JSON file: " & ex.Message)
                Return
            End Try

            ResetTabCount()
            ' Merge loaded JSON into default JSON
            MergeJson(defaultJson, loadedJsonData)

            ' Save the merged JSON back to the file (overwriting)
            File.WriteAllText(filePath, defaultJson.ToString())

            ' Load the JSON data into the form using the existing method
            LoadJsonData(filePath)
            ' Display the merged JSON content in txtOutput
            Dim jsonContentNew As String = File.ReadAllText(filePath)
            loadedJsonDataPub = JObject.Parse(jsonContentNew)
            txtOutput.Text = jsonContentNew.ToString()


            Dim manifestFilePath As String = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filePath), "murdermanifest.sodso.json")
            If System.IO.File.Exists(manifestFilePath) Then
                Dim manifestJson As String = System.IO.File.ReadAllText(manifestFilePath)
                txtManiOutput.Text = manifestJson
            Else
                txtManiOutput.Text = "murdermanifest.sodso.json not found."
            End If


            If isCaseMadeOrLoaded = False Then
                isCaseMadeOrLoaded = True
            End If

            lastOpened = True
            lastCreated = False

            If areControlsEnabled = False Then
                EnableControls()
            End If

            Me.Text = "Case Generator - " & folderName
            isCaseMadeOrLoaded = True
            isOutputChanged = False
        End If
        If My.Settings.isRemoveKeysEnabled = True Then
            RemoveUnusedKeys()
            FormatJson()
        Else
            FormatJson()
        End If
        FormatManifest()
        If My.Settings.isToolTipsEnabled = False Then
            DisableAllToolTips(Me)
        Else
            tooltips.SetAllTooltips()
        End If
    End Sub
    Public Sub CreateNewCase()
        ' Initialize FolderBrowserDialog
        Dim folderBrowserDialog As New FolderBrowserDialog()
        folderBrowserDialog.Description = "Select a location to create a new case folder"

        ' Show the dialog and get the user's selection
        If folderBrowserDialog.ShowDialog() = DialogResult.OK Then
            ' Prompt the user to enter a folder name
            Dim folderName As String = InputBox("Enter the name for the new case:", "Case/Folder Name")
            tempFolderName = folderName

            ' Check if the user provided a name
            If Not String.IsNullOrWhiteSpace(folderName) Then
                isCaseMadeOrLoaded = True
                ' Combine the selected path with the new folder name
                Dim newFolderPath As String = IO.Path.Combine(folderBrowserDialog.SelectedPath, folderName)
                tempFilePath = newFolderPath

                folderNameNew = IO.Path.GetFileName(IO.Path.GetDirectoryName(newFolderPath))

                Dim maniOutput As New With {
                     .enabled = True,
                     .fileOrder = New List(Of String) From {"REF:" + folderName.ToLower()},
                     .version = 1
                    }
                ' Create the new folder
                Try

                    IO.Directory.CreateDirectory(newFolderPath)
                    File.SetAttributes(newFolderPath, FileAttributes.ReadOnly = False)
                    Dim ddsContentPath As String = IO.Path.Combine(newFolderPath, "DDSContent")
                    Dim ddsPath As String = IO.Path.Combine(ddsContentPath, "DDS")
                    Dim stringsPath As String = IO.Path.Combine(ddsContentPath, "Strings")
                    Dim blocksPath As String = IO.Path.Combine(ddsPath, "Blocks")
                    Dim messagesPath As String = IO.Path.Combine(ddsPath, "Messages")
                    Dim treesPath As String = IO.Path.Combine(ddsPath, "Trees")
                    Dim englishPath As String = IO.Path.Combine(stringsPath, "English")
                    Dim ddsEnglishPath As String = IO.Path.Combine(englishPath, "DDS")

                    Dim presetFilePath As String = IO.Path.Combine(newFolderPath, folderName.ToLower() & ".sodso.json")
                    Dim manifestFilePath As String = IO.Path.Combine(newFolderPath, "murdermanifest.sodso.json")
                    IO.File.WriteAllText(presetFilePath, GetDefaultJson)
                    IO.File.WriteAllText(manifestFilePath, maniOutput.ToString)

                    ' Create the folders if they do not exist
                    IO.Directory.CreateDirectory(ddsContentPath)
                    IO.Directory.CreateDirectory(ddsPath)
                    IO.Directory.CreateDirectory(stringsPath)
                    IO.Directory.CreateDirectory(blocksPath)
                    IO.Directory.CreateDirectory(messagesPath)
                    IO.Directory.CreateDirectory(treesPath)
                    IO.Directory.CreateDirectory(englishPath)
                    IO.Directory.CreateDirectory(ddsEnglishPath)

                    MessageBox.Show("New case folder created successfully at: " & newFolderPath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ResetToDefaultValues()

                    FormatJson()
                    FormatManifest()

                    lastOpened = False
                    lastCreated = True
                    If areControlsEnabled = False Then
                        EnableControls()
                    End If


                    Me.Text = "Case Generator - " & folderName

                    txtPresetName.Text = folderName

                    ResetTabCount()
                Catch ex As Exception
                    MessageBox.Show("Error creating folder: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                MessageBox.Show("Folder name cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
        isCaseMadeOrLoaded = True
    End Sub
    Private Sub EnableControls()
        areControlsEnabled = True
        tabControlCase.Enabled = True
        TabControl1.Enabled = True
        btnGenerateCase.Enabled = True
        txtManiOutput.Enabled = True
        txtOutput.Enabled = True
        lblManifest.Enabled = True
        lblOutput.Enabled = True
        lblOpen.Enabled = False
        lblOpen.Visible = False
        pnCover.Visible = False
        pnCover.Enabled = False
        SearchToolStripMenuItem.Visible = True
        ToolsToolStripMenuItem.Visible = True
    End Sub
    Private Sub SaveCaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveCaseToolStripMenuItem.Click
        If isCaseMadeOrLoaded = True Then
            btnGenerateCase.PerformClick()
            SaveCase()
        Else
            MessageBox.Show("Please create or load a case first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub SearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchToolStripMenuItem.Click
        If isCaseMadeOrLoaded = True Then
            Dim searchTerm As String = InputBox("Enter text to search:", "Search Text")

            If searchTerm Is Nothing OrElse searchTerm = "" Then
                Return
            End If

            Dim searchForm As New SearchForm(searchTerm, txtOutput)

            searchForm.Show()
        Else
            MessageBox.Show("Please create or load a case first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub NewDDSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewDDSToolStripMenuItem.Click
        If isCaseMadeOrLoaded = True Then
            VMail_Form.Show()
        Else
            MessageBox.Show("Please create or load a case first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub CaptureControlDefaultValue(ctrl As Control)
        If TypeOf ctrl Is TextBox Then
            defaultValues(ctrl.Name) = CType(ctrl, TextBox).Text
        ElseIf TypeOf ctrl Is ComboBox Then
            defaultValues(ctrl.Name) = CType(ctrl, ComboBox).SelectedIndex
        ElseIf TypeOf ctrl Is CheckBox Then
            defaultValues(ctrl.Name) = CType(ctrl, CheckBox).Checked
        ElseIf TypeOf ctrl Is RadioButton Then
            defaultValues(ctrl.Name) = CType(ctrl, RadioButton).Checked
        ElseIf TypeOf ctrl Is ListBox Then
            defaultValues(ctrl.Name) = CType(ctrl, ListBox).SelectedIndex
        ElseIf TypeOf ctrl Is NumericUpDown Then
            defaultValues(ctrl.Name) = CType(ctrl, NumericUpDown).Value
        ElseIf TypeOf ctrl Is RichTextBox Then
            defaultValues(ctrl.Name) = CType(ctrl, RichTextBox).Text
        End If

        ' Recursively handle container controls (TabControl, TabPage, GroupBox, Panel, etc.)
        If TypeOf ctrl Is TabControl Then
            For Each tabPage As TabPage In CType(ctrl, TabControl).TabPages
                For Each childCtrl As Control In tabPage.Controls
                    CaptureControlDefaultValue(childCtrl)
                Next
            Next
        ElseIf TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is Panel Then
            For Each childCtrl As Control In ctrl.Controls
                CaptureControlDefaultValue(childCtrl)
            Next
        End If
    End Sub
    Private Sub CaptureDefaultValues()
        For Each ctrl As Control In Me.Controls
            CaptureControlDefaultValue(ctrl)
        Next
    End Sub
    Private Sub ResetToDefaultValues()
        For Each ctrl As Control In Me.Controls
            ResetControlToDefault(ctrl)
        Next
    End Sub
    Private Sub ResetControlToDefault(ctrl As Control)
        If defaultValues.ContainsKey(ctrl.Name) Then
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = defaultValues(ctrl.Name).ToString()
            ElseIf TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).SelectedIndex = CInt(defaultValues(ctrl.Name))
            ElseIf TypeOf ctrl Is CheckBox Then
                CType(ctrl, CheckBox).Checked = CBool(defaultValues(ctrl.Name))
            ElseIf TypeOf ctrl Is RadioButton Then
                CType(ctrl, RadioButton).Checked = CBool(defaultValues(ctrl.Name))
            ElseIf TypeOf ctrl Is ListBox Then
                CType(ctrl, ListBox).SelectedIndex = CInt(defaultValues(ctrl.Name))
            ElseIf TypeOf ctrl Is NumericUpDown Then
                CType(ctrl, NumericUpDown).Value = CDec(defaultValues(ctrl.Name))
            ElseIf TypeOf ctrl Is RichTextBox Then
                CType(ctrl, RichTextBox).Text = defaultValues(ctrl.Name).ToString()
            End If
        End If

        Dim targetTabControl As TabControl = CType(Me.Controls.Find("tabControlCase", True).FirstOrDefault(), TabControl)
        If targetTabControl IsNot Nothing AndAlso targetTabControl.TabPages.Count > 1 Then
            ' Remove all TabPages except the first one
            For i As Integer = targetTabControl.TabPages.Count - 1 To 1 Step -1
                targetTabControl.TabPages.RemoveAt(i)
            Next
        End If

        ' Recursively handle container controls (TabControl, TabPage, GroupBox, Panel, etc.)
        If TypeOf ctrl Is TabControl Then
            For Each tabPage As TabPage In CType(ctrl, TabControl).TabPages
                For Each childCtrl As Control In tabPage.Controls
                    ResetControlToDefault(childCtrl)
                Next
            Next
        ElseIf TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is Panel Then
            For Each childCtrl As Control In ctrl.Controls
                ResetControlToDefault(childCtrl)
            Next
        End If
    End Sub
    Private Sub LoadStringListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadStringListToolStripMenuItem.Click
        If isCaseMadeOrLoaded = True Then
            StringListForm.Show()
        Else
            MessageBox.Show("Please create or load a case first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub RemoveUnusedKeys()
        Dim jsonContent As String = txtOutput.Text

        ' Parse the JSON content
        Dim jsonData As JObject = JObject.Parse(jsonContent)

        ' Create a list of keys to remove
        Dim keysToRemove As New List(Of String)
        Dim disableHexaco As Boolean = False
        ' Loop through each property in the JObject
        For Each propertyPair In jsonData.Properties()
            Dim key As String = propertyPair.Name
            Dim value As JToken = propertyPair.Value

            ' Check if the value is null, empty, an empty array, or an empty object
            If value.Type = JTokenType.Null OrElse
               (value.Type = JTokenType.String AndAlso String.IsNullOrEmpty(value.ToString())) OrElse
               (value.Type = JTokenType.Array AndAlso Not value.Any()) OrElse
               (value.Type = JTokenType.Object AndAlso Not value.HasValues) OrElse
               (value.Type = JTokenType.Boolean AndAlso Boolean.Parse(value) = False AndAlso key <> "disabled" AndAlso key <> "allowAnywhere" AndAlso key <> "allowHome" AndAlso key <> "allowWork" AndAlso key <> "allowPublic" AndAlso key <> "allowStreets" AndAlso key <> "allowDen") OrElse
               (value.Type = JTokenType.Integer AndAlso Integer.Parse(value) = 0) Then
                ' Add the key to the list of keys to remove
                keysToRemove.Add(key)
            End If
            ' Manual Removal
            If key = "useHexaco" Then
                If value.Type = JTokenType.Boolean AndAlso Boolean.Parse(value) = False Then
                    keysToRemove.Add(key)
                    disableHexaco = True
                End If
            End If

            If key = "hexaco" AndAlso disableHexaco = True Then
                keysToRemove.Add(key)
            End If
        Next

        ' Remove the unused keys from jsonData
        For Each key In keysToRemove
            jsonData.Remove(key)
        Next

        RemoveRangeIfDefault(jsonData, "murdererClassRange")
        RemoveRangeIfDefault(jsonData, "victimClassRange")
        RemoveRangeIfDefault(jsonData, "pickRandomScoreRange")
        RemoveRangeIfDefault(jsonData, "victimRandomScoreRange")

        ' Update the txtOutput with the cleaned JSON
        txtOutput.Text = jsonData.ToString()
        FormatJson()
    End Sub
    Private Sub RemoveRangeIfDefault(jsonData As JObject, rangeKey As String)
        Dim range As JObject = jsonData(rangeKey)
        If range IsNot Nothing AndAlso
            range("x").ToObject(Of Integer) = 0 AndAlso
            range("y").ToObject(Of Integer) = 1 Then
            jsonData.Remove(rangeKey)
        End If
    End Sub
    Private Sub ResetTabCount()
        victimCompanyModifierEntryCount = 0
        murdererCompanyModifierEntryCount = 0
        victimJobModifierEntryCount = 0
        murdererJobModifierEntryCount = 0
        victimTraitEntryCount = 0
        murdererTraitEntryCount = 0
        callingcardEntryCount = 0
        moleadEntryCount = 0
        graffitiEntryCount = 0
        setTabValueCC = 0
        setTabValueMO = 0
        weaponTabOpen = False
        hexacoTabOpen = False
        ddsConfessionalTabOpen = False
        playerTauntTabOpen = False
        isCaseMadeOrLoaded = False
    End Sub
    Private Sub SaveCase()
        ' Initialize SaveFileDialog
        Dim saveFileDialog As New SaveFileDialog
        saveFileDialog.Filter = "Shadows of Doubt SOJSON Files (*.sodso.json)|*.sodso.json"
        saveFileDialog.Title = "Save Case and Manifest Files"

        ' Set default file name for the preset case JSON (in lowercase)
        saveFileDialog.FileName = txtPresetName.Text.ToLower() & ".sodso.json"

        ' Show the dialog and get the file path
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            ' Get the directory where the files will be saved
            Dim saveDirectory As String = IO.Path.GetDirectoryName(saveFileDialog.FileName)

            ' Define the file paths (preset case file in lowercase)
            Dim presetFilePath As String = IO.Path.Combine(saveDirectory, txtPresetName.Text.ToLower() & ".sodso.json")
            Dim manifestFilePath As String = IO.Path.Combine(saveDirectory, "murdermanifest.sodso.json")

            ' Save the preset JSON file
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

            ' Save the manifest JSON file
            Try
                IO.File.WriteAllText(manifestFilePath, txtManiOutput.Text)
                'MessageBox.Show("Manifest file saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error saving manifest file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Sub RemoveUnusedKeysToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveUnusedKeysToolStripMenuItem.Click
        If isCaseMadeOrLoaded = True Then
            RemoveUnusedKeys()
        Else
            MessageBox.Show("Please create or load a case first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        HelpForm.Show()
        openForm = HelpForm
        DisableOtherForms()
    End Sub
    Private Sub CloseCaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseCaseToolStripMenuItem.Click
        If isCaseMadeOrLoaded = True AndAlso isOutputChanged = True Then
            Dim result As DialogResult = MessageBox.Show("Save changes? Any unsaved changes will be lost.", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                btnGenerateCase.PerformClick()
                SaveCase()
                FullReset()
            ElseIf result = DialogResult.No Then
                FullReset()
            ElseIf result = DialogResult.Cancel Then

            End If

        ElseIf isCaseMadeOrLoaded = True AndAlso isOutputChanged = False Then
            FullReset()
        Else
            MessageBox.Show("Please create or load a case first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub FullReset()
        ResetToDefaultValues()
        Me.Text = "Case Generator"
        isCaseMadeOrLoaded = False
        pnCover.Enabled = True
        pnCover.Visible = True
        lblOpen.Enabled = True
        lblOpen.Visible = True
        areControlsEnabled = False
        isOutputChanged = False
        ToolsToolStripMenuItem.Visible = False
        SearchToolStripMenuItem.Visible = False
    End Sub
    Private Sub DisableAllToolTips(ByVal parentControl As Control)
        For Each control As Control In parentControl.Controls
            toolTip.SetToolTip(control, String.Empty)

            If control.HasChildren Then
                DisableAllToolTips(control)
            End If
        Next
    End Sub
    Public Sub DisableOtherForms()
        For Each frm As Form In My.Application.OpenForms
            If Not frm Is openForm Then
                frm.Enabled = False
            End If
        Next
    End Sub
    Public Sub EnableOtherForms()
        For Each frm As Form In My.Application.OpenForms
            frm.Enabled = True
        Next
    End Sub
    Private Sub AddValueChangedHandlers(ByVal parent As Control)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is MenuStrip Then
                Continue For
            End If

            If TypeOf ctrl Is TextBox Then
                Dim handler As EventHandler = AddressOf ControlValueChanged
                AddHandler DirectCast(ctrl, TextBox).TextChanged, handler
                controlEventHandlers(ctrl) = handler
            ElseIf TypeOf ctrl Is CheckBox Then
                Dim handler As EventHandler = AddressOf ControlValueChanged
                AddHandler DirectCast(ctrl, CheckBox).CheckedChanged, handler
                controlEventHandlers(ctrl) = handler
            ElseIf TypeOf ctrl Is RadioButton Then
                Dim handler As EventHandler = AddressOf ControlValueChanged
                AddHandler DirectCast(ctrl, RadioButton).CheckedChanged, handler
                controlEventHandlers(ctrl) = handler
            ElseIf TypeOf ctrl Is ComboBox Then
                Dim handler As EventHandler = AddressOf ControlValueChanged
                AddHandler DirectCast(ctrl, ComboBox).SelectedIndexChanged, handler
                controlEventHandlers(ctrl) = handler
            ElseIf TypeOf ctrl Is NumericUpDown Then
                Dim handler As EventHandler = AddressOf ControlValueChanged
                AddHandler DirectCast(ctrl, NumericUpDown).ValueChanged, handler
                controlEventHandlers(ctrl) = handler
            ElseIf TypeOf ctrl Is TabControl Then
                Dim tabControl As TabControl = DirectCast(ctrl, TabControl)
                Dim tabControlHandler As ControlEventHandler = AddressOf TabPageAddedHandler
                AddHandler tabControl.ControlAdded, tabControlHandler
                tabControlEventHandlers(tabControl) = tabControlHandler

                Dim tabIndexChangedHandler As EventHandler = AddressOf ControlValueChanged
                AddHandler tabControl.SelectedIndexChanged, tabIndexChangedHandler
                controlEventHandlers(tabControl) = tabIndexChangedHandler


                For Each tabPage As TabPage In tabControl.TabPages
                    AddValueChangedHandlers(tabPage)
                Next

            ElseIf TypeOf ctrl Is DateTimePicker Then
                Dim handler As EventHandler = AddressOf ControlValueChanged
                AddHandler DirectCast(ctrl, DateTimePicker).ValueChanged, handler
                controlEventHandlers(ctrl) = handler
            End If

            If ctrl.HasChildren Then
                AddValueChangedHandlers(ctrl)
            End If
        Next
    End Sub

    Private Sub RemoveValueChangedHandlers(ByVal parent As Control)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is MenuStrip Then
                Continue For
            End If

            If controlEventHandlers.ContainsKey(ctrl) Then
                If TypeOf ctrl Is TextBox Then
                    RemoveHandler DirectCast(ctrl, TextBox).TextChanged, DirectCast(controlEventHandlers(ctrl), EventHandler)
                ElseIf TypeOf ctrl Is CheckBox Then
                    RemoveHandler DirectCast(ctrl, CheckBox).CheckedChanged, DirectCast(controlEventHandlers(ctrl), EventHandler)
                ElseIf TypeOf ctrl Is RadioButton Then
                    RemoveHandler DirectCast(ctrl, RadioButton).CheckedChanged, DirectCast(controlEventHandlers(ctrl), EventHandler)
                ElseIf TypeOf ctrl Is ComboBox Then
                    RemoveHandler DirectCast(ctrl, ComboBox).SelectedIndexChanged, DirectCast(controlEventHandlers(ctrl), EventHandler)
                ElseIf TypeOf ctrl Is NumericUpDown Then
                    RemoveHandler DirectCast(ctrl, NumericUpDown).ValueChanged, DirectCast(controlEventHandlers(ctrl), EventHandler)
                ElseIf TypeOf ctrl Is DateTimePicker Then
                    RemoveHandler DirectCast(ctrl, DateTimePicker).ValueChanged, DirectCast(controlEventHandlers(ctrl), EventHandler)
                ElseIf TypeOf ctrl Is TabControl Then
                    Dim tabControl As TabControl = DirectCast(ctrl, TabControl)
                    If tabControlEventHandlers.ContainsKey(tabControl) Then
                        RemoveHandler tabControl.ControlAdded, DirectCast(tabControlEventHandlers(tabControl), ControlEventHandler)
                        tabControlEventHandlers.Remove(tabControl)
                    End If
                    If controlEventHandlers.ContainsKey(tabControl) Then
                        RemoveHandler tabControl.TabIndexChanged, DirectCast(controlEventHandlers(tabControl), EventHandler)
                        tabControlEventHandlers.Remove(tabControl)
                    End If

                    For Each tabPage As TabPage In tabControl.TabPages
                        RemoveValueChangedHandlers(tabPage)
                    Next
                End If

                controlEventHandlers.Remove(ctrl)
            End If

            If ctrl.HasChildren Then
                RemoveValueChangedHandlers(ctrl)
            End If
        Next
    End Sub
    Private Sub TabPageAddedHandler(ByVal sender As Object, ByVal e As ControlEventArgs)
        If TypeOf e.Control Is TabPage Then
            Dim newTabPage As TabPage = DirectCast(e.Control, TabPage)

            AddValueChangedHandlers(newTabPage)
        End If
    End Sub
    Private Sub ListBoxChanged(ByVal parent As Control)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is ListBox Then
                Dim listBox As ListBox = DirectCast(ctrl, ListBox)

                If Not listBoxItemCounts.ContainsKey(listBox) Then
                    listBoxItemCounts.Add(listBox, listBox.Items.Count)
                End If
            ElseIf TypeOf ctrl Is TabControl Then
                Dim tabControl As TabControl = DirectCast(ctrl, TabControl)


                For Each tabPage As TabPage In tabControl.TabPages
                    ListBoxChanged(tabPage)
                Next
            End If

            If ctrl.HasChildren Then
                ListBoxChanged(ctrl)
            End If
        Next

        listBoxMonitorTimer.Interval = 1000
        listBoxMonitorTimer.Start()
    End Sub
    Private Sub listBoxMonitorTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles listBoxMonitorTimer.Tick
        ListBoxChanged(Me)
        For Each listBox In listBoxItemCounts.Keys
            CheckListBoxItemCount(listBox)
        Next
    End Sub
    Private Sub CheckListBoxItemCount(ByVal listBox As ListBox)
        Dim previousCount As Integer = listBoxItemCounts(listBox)
        Dim currentCount As Integer = listBox.Items.Count

        If currentCount <> previousCount Then
            btnGenerateCase.PerformClick()
            listBoxItemCounts(listBox) = currentCount
        End If
    End Sub
    Private Sub ControlValueChanged(sender As Object, e As EventArgs)
        btnGenerateCase.PerformClick()
    End Sub
    Private Sub ClearListBoxCounts()
        listBoxItemCounts.Clear()
    End Sub
    Private Sub UpdateToolMenus()
        If My.Settings.isAutoGenerationEnabled = True Then
            itemAutoGen.Text = "Auto Generate Enabled"
            itemAutoGen.ForeColor = Color.Green
        Else
            itemAutoGen.Text = "Auto Generate Disabled"
            itemAutoGen.ForeColor = Color.Red
        End If

        If My.Settings.isRemoveKeysEnabled = True Then
            itemRemoveKeys.Text = "Auto Remove Keys Enabled"
            itemRemoveKeys.ForeColor = Color.Green
        Else
            itemRemoveKeys.Text = "Auto Remove Keys Disabled"
            itemRemoveKeys.ForeColor = Color.Red
        End If

        If My.Settings.isToolTipsEnabled = True Then
            itemToolTips.Text = "Tool Tips Enabled"
            itemToolTips.ForeColor = Color.Green
        Else
            itemToolTips.Text = "Tool Tips Disabled"
            itemToolTips.ForeColor = Color.Red
        End If
    End Sub

    Private Sub itemAutoGen_Click(sender As Object, e As EventArgs) Handles itemAutoGen.Click
        If My.Settings.isAutoGenerationEnabled = False Then
            My.Settings.isAutoGenerationEnabled = True
            RemoveValueChangedHandlers(Me)
            AddValueChangedHandlers(Me)
            ListBoxChanged(Me)

            If Not listBoxMonitorTimer.Enabled Then
                listBoxMonitorTimer.Start()
            End If
        Else
            Dim result As DialogResult = MessageBox.Show("This requires a restart, do you wish continue? Any unsaved changes will be lost.", "Restart Required", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                My.Settings.isAutoGenerationEnabled = False
                listBoxMonitorTimer.Stop()
                ClearListBoxCounts()
                RemoveValueChangedHandlers(Me)
                Application.Restart()
            Else

            End If
        End If
        UpdateToolMenus()
    End Sub
    Private Sub RemoveUnusedKeysToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles itemRemoveKeys.Click
        If My.Settings.isRemoveKeysEnabled = False Then
            My.Settings.isRemoveKeysEnabled = True
        Else
            My.Settings.isRemoveKeysEnabled = False
        End If
        UpdateToolMenus()
    End Sub

    Private Sub itemToolTips_Click(sender As Object, e As EventArgs) Handles itemToolTips.Click
        If My.Settings.isToolTipsEnabled = False Then
            My.Settings.isToolTipsEnabled = True
            tooltips.SetAllTooltips()
        Else
            My.Settings.isToolTipsEnabled = False
            DisableAllToolTips(Me)
        End If
        UpdateToolMenus()
    End Sub
    Private Sub UpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateToolStripMenuItem.Click
        CheckForUpdates()
    End Sub
    Private Sub CheckForUpdates()
        Try
            Dim updateFileUrl As String = "https://download-files.wixmp.com/raw/ecb0f0_12f5c38b5d3947f283222554cd4de9c7.txt?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJ1cm46YXBwOmU2NjYzMGU3MTRmMDQ5MGFhZWExZjE0OWIzYjY5ZTMyIiwic3ViIjoidXJuOmFwcDplNjY2MzBlNzE0ZjA0OTBhYWVhMWYxNDliM2I2OWUzMiIsImF1ZCI6WyJ1cm46c2VydmljZTpmaWxlLmRvd25sb2FkIl0sImlhdCI6MTcyNzc0ODQwMywiZXhwIjoxNzI3NzQ5MzEzLCJqdGkiOiI0MDlmZWRlMy1kZjU4LTRkNjMtYTMzNi01MDQ0Y2E3MzFiZTEiLCJvYmoiOltbeyJwYXRoIjoiL3Jhdy9lY2IwZjBfMTJmNWMzOGI1ZDM5NDdmMjgzMjIyNTU0Y2Q0ZGU5YzcudHh0In1dXSwiZGlzIjp7ImZpbGVuYW1lIjoidXBkYXRlLnR4dCIsInR5cGUiOiJpbmxpbmUifX0.roKpeHglClzHsVQFjifQZbl_51EonFYgIYQyL8vld0I"

            Using client As New System.Net.WebClient()
                Dim updateInfo As String = client.DownloadString(updateFileUrl)
                Dim parts As String() = updateInfo.Split("|"c)

                If parts.Length = 2 Then
                    Dim latestVersion As String = parts(0)
                    Dim downloadLink As String = parts(1)

                    ' Compare versions
                    If IsNewVersion(currentVersion, latestVersion) Then
                        Dim result As DialogResult = MessageBox.Show(
                        $"A new version {latestVersion} is available. Do you want to update?",
                        "Update Available",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information)

                        If result = DialogResult.Yes Then
                            ' Open the download link
                            Process.Start(New ProcessStartInfo With {
                            .FileName = downloadLink,
                            .UseShellExecute = True
                })
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking for updates: " & ex.Message)
        End Try
    End Sub
    Private Function IsNewVersion(current As String, latest As String) As Boolean
        Return String.Compare(current, latest) < 0
    End Function
    Public Function FindControlRecursive(Of T As Control)(ByVal parent As Control, ByVal matchCondition As Func(Of T, Boolean)) As T
        ' Loop through all controls in the parent
        For Each ctrl As Control In parent.Controls

            If TypeOf ctrl Is T AndAlso matchCondition(DirectCast(ctrl, T)) Then
                Return DirectCast(ctrl, T)
            End If

            ' Recursively search in child controls
            Dim foundControl As T = FindControlRecursive(Of T)(ctrl, matchCondition)
            If foundControl IsNot Nothing Then
                Return foundControl
            End If
        Next

        Return Nothing
    End Function

End Class

'TODO

'==============================================================
'Finish Tool Tips
'==============================================================




