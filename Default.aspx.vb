Imports Helper
Imports System.Data
Imports Utilities.Utilities

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ' Higlight the navigation tab for this page
        CType(Master.FindControl("topmenu"), Menu).Items(0).Selected = True

    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        DisplaySliderLabels()
        CalculateResults()

        If Not IsPostBack Then
            LoadPreviousProjects()
        End If

    End Sub

    Protected Sub CalculateResults()
        ' Calculate Lines Of Code
        Dim strLanguage As String = ddlLanguage.SelectedValue
        Dim intAvgLocPerFp As Integer = GetAvgLocPerFunctionPoint(strLanguage)

        Dim intFunctionPoints As Integer
        Dim strFunctionPoints As String = If(txtFunctionPoints.Text = "", "0", txtFunctionPoints.Text)
        Dim blFPPass As Boolean = Integer.TryParse(strFunctionPoints, intFunctionPoints)
        If Not blFPPass Then
            Master.Notify("Error: Invalid Function Point count.")
            txtFunctionPoints.Text = 0
            Exit Sub
        End If

        Dim decLoc As Decimal = intAvgLocPerFp * intFunctionPoints
        Dim decKLoc As Decimal = decLoc / 1000
        lblLoC.Text = decLoc


        ' Get Cocomo Factors
        Dim strProjectClass As String = rbtnProjectClass.SelectedValue
        Dim coll As Collection = GetProjectClassFactors(strProjectClass)
        Dim factA As Decimal = 0
        Dim factB As Decimal = 0
        Dim factC As Decimal = 0
        Dim factD As Decimal = 0

        If coll.Count <> 0 Then
            factA = coll("FactorA")
            factB = coll("FactorB")
            factC = coll("FactorC")
            factD = coll("FactorD")
        End If


        'Calculate Effort Adjustment Factor (EAF)
        Dim decEAF As Decimal = 1
        Dim decProgCapFactor As Decimal = GetEffortFactor("ProgramerCap", lblProgCap.Text)
        Dim decLangExp As Decimal = GetEffortFactor("LangToolExp", lblLangToolExp.Text)
        Dim decSoftComp As Decimal = GetEffortFactor("SoftComplex", lblSoftComplex.Text)
        Dim decDocumentation As Decimal = GetEffortFactor("Documentation", lblDocumentation.Text)
        decEAF = decProgCapFactor * decLangExp * decSoftComp * decDocumentation

        ' Calculate Effort
        Dim decEffort As Decimal = factA * (Math.Pow(decKLoc, factB)) * decEAF
        lblEffort.Text = Math.Round(decEffort, 1)


        ' Calculate Development Time (Schedule)
        Dim decSchedule As Decimal = factC * (Math.Pow(decEffort, factD))
        lblSchedule.Text = Math.Round(decSchedule, 2)


        ' JAG
        ' convert dev time to days assume 8h/d, 5d/w, 20d/m
        Dim durDays As Integer = Math.Truncate(decSchedule * 20)
        If txtStartDate.Text = "" Then
            txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
        End If

        Dim csCode As Common = New Common()
        lblEndDate.Text = csCode.GetLastDate(txtStartDate.Text, durDays).ToString("MMM dd, yyyy")


        ' Calculate People Required
        Dim intPeople As Integer = If(decSchedule = 0, 0, decEffort / decSchedule)
        lblPeople.Text = If(intPeople = 0 And decEffort <> 0, 1, intPeople)
        If intPeople = 1 Then
            lblPeopleLabel.Text = "Person"
        Else
            lblPeopleLabel.Text = "People"
        End If

        ' Calculate Cost
        Dim decCostPersonMonth As Decimal
        Dim strCostPersonMonth As String = If(txtCostPersonMonth.Text = "", "0", txtCostPersonMonth.Text)
        Dim blCostPass As Boolean = Decimal.TryParse(strCostPersonMonth, decCostPersonMonth)
        If Not blCostPass Then
            Master.Notify("Error: Invalid Cost amount.")
            txtCostPersonMonth.Text = 0
            Exit Sub
        End If

        Dim decCost As Decimal = decEffort * decCostPersonMonth
        If decCost < 1 And decCostPersonMonth <> 0 Then decCost = 1 ' Final Cost should only be zero if Cost Per Month is zero

        lblFinalCost.Text = String.Format("{0:C0}", decCost)
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As System.EventArgs) Handles btnReset.Click
        'Reset Form
        lblIDNo.Text = ""

        txtProjectName.Text = ""
        txtFunctionPoints.Text = ""
        ddlLanguage.SelectedValue = "Select Language"
        txtStartDate.Text = ""

        txtProgCap.Text = 2
        lblProgCap.Text = "Nominal"
        txtLangToolExp.Text = 2
        lblLangToolExp.Text = "Nominal"
        txtSoftComplex.Text = 3
        lblSoftComplex.Text = "Nominal"
        txtDocumentation.Text = 2
        lblDocumentation.Text = "Nominal"

        txtCostPersonMonth.Text = ""
    End Sub

    Protected Sub LoadPreviousProjects()
        Dim ds As DataSet = GetPreviousProjects()
        gvProjects.DataSource = ds
        gvProjects.DataBind()

    End Sub

    Protected Sub gvProjects_PageIndexChanged(sender As Object, e As System.EventArgs) Handles gvProjects.PageIndexChanged

    End Sub

    Protected Sub gvProjects_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvProjects.PageIndexChanging

    End Sub

    Protected Sub gvProjects_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProjects.RowCommand
        Select Case e.CommandName
            Case "Del"
                Dim row As GridViewRow = e.CommandSource.NamingContainer
                Dim intIDNo As Integer = GetRowControlValue(row, "lblIDNo")
                Master.Notify(DeleteProject(intIDNo))

                LoadPreviousProjects()

            Case "Sel"
                Dim row As GridViewRow = e.CommandSource.NamingContainer
                Dim intIDNo As Integer = GetRowControlValue(row, "lblIDNo")
                lblIDNo.Text = intIDNo

                Dim coll As Collection = GetSavedProjectData(intIDNo)
                LoadSavedData(coll)
            Case "Print"

        End Select
    End Sub

    Protected Sub LoadSavedData(ByVal coll As Collection)
        txtProjectName.Text = coll("ProjectName")
        ' JAG
        txtStartDate.Text = DateTime.Parse(coll("StartDate")).ToString("MM/dd/yyyy")
        lblEndDate.Text = coll("EndDate")
        txtFunctionPoints.Text = coll("FunctionPoints")
        ddlLanguage.SelectedValue = coll("Language")
        rbtnProjectClass.SelectedValue = coll("ProjectClass")
        txtProgCap.Text = coll("ProgCap")
        txtLangToolExp.Text = coll("LangExp")
        txtSoftComplex.Text = coll("SoftComplex")
        txtDocumentation.Text = coll("Documentation")
        txtCostPersonMonth.Text = coll("CostPerPM")
        lblLoC.Text = coll("LoC")
        lblEffort.Text = coll("Effort")
        lblSchedule.Text = coll("Schedule")
        lblPeople.Text = coll("People")
        lblFinalCost.Text = String.Format("{0:C0}", coll("Cost"))

        DisplaySliderLabels()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click

        'Recalculate results just in case
        CalculateResults()

        'If there is an IDNo then delete that entry and re-enter it
        If lblIDNo.Text <> "" Then
            DeleteProject(lblIDNo.Text)
            lblIDNo.Text = ""
        End If

        Dim strProjectName As String = txtProjectName.Text
        Dim intFunctionPoints As Integer = txtFunctionPoints.Text
        Dim strLanguage As String = ddlLanguage.SelectedValue
        Dim strProjectClass As String = rbtnProjectClass.SelectedValue
        Dim intProgCap As Integer = txtProgCap.Text
        Dim intLangExp As Integer = txtLangToolExp.Text
        Dim intSoftComplex As Integer = txtSoftComplex.Text
        Dim intDocumentation As Integer = txtDocumentation.Text
        Dim intCostPerPM As Integer = txtCostPersonMonth.Text
        Dim intLOC As Integer = lblLoC.Text
        Dim decEffort As Decimal = lblEffort.Text
        Dim decSchedule As Decimal = lblSchedule.Text
        Dim intPeople As Integer = lblPeople.Text
        Dim intCost As Integer = lblFinalCost.Text
        Dim strStartDate As String = DateTime.Parse(txtStartDate.Text).ToString("MM/dd/yyyy")
        Dim strEndDate As String = DateTime.Parse(lblEndDate.Text).ToString("MM/dd/yyyy")


        Master.Notify(InsertProject(strProjectName, intFunctionPoints, strLanguage, strProjectClass, intProgCap, intLangExp, intSoftComplex,
                                    intDocumentation, intCostPerPM, intLOC, decEffort, decSchedule, intPeople, intCost, strStartDate, strEndDate))

        LoadPreviousProjects()
    End Sub

    Protected Sub DisplaySliderLabels()
        Select Case txtSoftComplex.Text
            Case "1" : lblSoftComplex.Text = "Very Low"
            Case "2" : lblSoftComplex.Text = "Low"
            Case "3" : lblSoftComplex.Text = "Nominal"
            Case "4" : lblSoftComplex.Text = "High"
            Case "5" : lblSoftComplex.Text = "Very High"
        End Select
        Select Case txtLangToolExp.Text
            Case "1" : lblLangToolExp.Text = "Low"
            Case "2" : lblLangToolExp.Text = "Nominal"
            Case "3" : lblLangToolExp.Text = "High"
        End Select
        Select Case txtDocumentation.Text
            Case "1" : lblDocumentation.Text = "Low"
            Case "2" : lblDocumentation.Text = "Nominal"
            Case "3" : lblDocumentation.Text = "High"
        End Select
        Select Case txtProgCap.Text
            Case "1" : lblProgCap.Text = "Low"
            Case "2" : lblProgCap.Text = "Nominal"
            Case "3" : lblProgCap.Text = "High"
        End Select
    End Sub

End Class
