Imports System.Data
Imports Helper
Imports Utilities.Utilities

Partial Class Analysis
    Inherits System.Web.UI.Page

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ' Higlight the navigation tab for this page ff
        CType(Master.FindControl("topmenu"), Menu).Items(1).Selected = True

        If Not IsPostBack Then
            LoadProjects()
        End If
    End Sub

    Protected Sub LoadProjects()
        ' Load Saved Projects from database
        Dim ds As DataSet = GetPreviousProjects()
        gvProjects.DataSource = ds
        gvProjects.DataBind()

    End Sub

    Protected Sub gvProjects_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProjects.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            'Change the cost drivers from integers to strings
            Dim lbl As Label = e.Row.FindControl("lblProgCap")
            Dim intVal As Integer = lbl.Text
            lbl.Text = GetCostDriverLabel(3, intVal) '3 options

            lbl = e.Row.FindControl("lblLangExp")
            intVal = lbl.Text
            lbl.Text = GetCostDriverLabel(3, intVal) '3 options

            lbl = e.Row.FindControl("lblSoftComplex")
            intVal = lbl.Text
            lbl.Text = GetCostDriverLabel(5, intVal) '5 options

            lbl = e.Row.FindControl("lblDocumentation")
            intVal = lbl.Text
            lbl.Text = GetCostDriverLabel(3, intVal) '3 options

            e.Row.CssClass = "row grey"

        End If
    End Sub

    Protected Function GetCostDriverLabel(ByVal intOptions As Integer, ByVal intValue As Integer) As String

        GetCostDriverLabel = ""

        If intOptions = 5 Then
            Select Case intValue
                Case "1" : GetCostDriverLabel = "Very Low"
                Case "2" : GetCostDriverLabel = "Low"
                Case "3" : GetCostDriverLabel = "Nominal"
                Case "4" : GetCostDriverLabel = "High"
                Case "5" : GetCostDriverLabel = "Very High"
            End Select
        Else
            Select Case intValue
                Case "1" : GetCostDriverLabel = "Low"
                Case "2" : GetCostDriverLabel = "Nominal"
                Case "3" : GetCostDriverLabel = "High"
            End Select
        End If

        Return GetCostDriverLabel
    End Function

    Protected Sub chkCompare_Checked(ByVal sender As Object, ByVal e As EventArgs)

        Dim chk As CheckBox = sender
        Dim row As GridViewRow = chk.NamingContainer
        If chk.Checked Then
            row.CssClass = "row"
        Else
            row.CssClass = "row grey"
        End If
        CompareGrid()
    End Sub

    Protected Sub SetLabelColor(ByVal intRowIndex As Integer, ByVal strLabelName As String, ByVal strValue As String)
        ' Sets the label color for strLabelname of the row at intRowIndex. strValue is either high,low, or normal

        Dim row As GridViewRow = gvProjects.Rows(intRowIndex)
        Dim lbl As Label = row.FindControl(strLabelName)

        Select Case strValue
            Case "High"
                lbl.Style("color") = "Red"
                lbl.Font.Bold = True
            Case "Low"
                lbl.Style("color") = "Green"
                lbl.Font.Bold = True
            Case "Normal"
                Dim chk As CheckBox = row.FindControl("chkCompare")
                If chk.Checked Then
                    lbl.Style("color") = "#666"
                Else
                    lbl.Style("color") = "#c8c4c4"
                End If
                lbl.Font.Bold = False
        End Select
    End Sub

    Protected Sub CompareGrid()
        ' Loop through grid to highlight the lowest and highest Cost and Schedule values

        Dim intHighestCost As Integer = 0
        Dim intLowestCost As Integer = 999999999
        Dim intHighestCostRow As Integer = 0
        Dim intLowestCostRow As Integer = 0

        Dim intHighestSch As Integer = 0
        Dim intLowestSch As Integer = 99999999
        Dim intHighestSchRow As Integer = 0
        Dim intLowestSchRow As Integer = 0

        For Each row As GridViewRow In gvProjects.Rows
            Dim chk As CheckBox = row.FindControl("chkCompare")

            If chk.Checked Then
                Dim intCost As Integer = GetRowControlValue(row, "lblCost")
                If intCost > intHighestCost Then
                    intHighestCost = intCost
                    intHighestCostRow = row.RowIndex
                End If
                If intCost < intLowestCost Then
                    intLowestCost = intCost
                    intLowestCostRow = row.RowIndex
                End If

                Dim intSch As Integer = GetRowControlValue(row, "lblSchedule")
                If intSch > intHighestSch Then
                    intHighestSch = intSch
                    intHighestSchRow = row.RowIndex
                End If
                If intSch < intLowestSch Then
                    intLowestSch = intSch
                    intLowestSchRow = row.RowIndex
                End If

            End If
        Next

        ResetLabelColors()

        SetLabelColor(intHighestCostRow, "lblCost", "High")
        SetLabelColor(intLowestCostRow, "lblCost", "Low")

        SetLabelColor(intHighestSchRow, "lblSchedule", "High")
        SetLabelColor(intLowestSchRow, "lblSchedule", "Low")
    End Sub

    Protected Sub ResetLabelColors()
        For Each row As GridViewRow In gvProjects.Rows
            SetLabelColor(row.RowIndex, "lblCost", "Normal")
            SetLabelColor(row.RowIndex, "lblSchedule", "Normal")
        Next
    End Sub
End Class
