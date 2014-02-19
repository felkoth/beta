Imports Utilities.DataAccess
Imports System.Globalization
Imports System.Data
Imports Utilities.Utilities

Partial Class Log
    Inherits System.Web.UI.Page

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ' Higlight the navigation tab for this page
        CType(Master.FindControl("topmenu"), Menu).Items(0).Selected = True
        If Not IsPostBack Then
            txtDate.Text = Today.ToShortDateString
            lblDate.Text = Today.ToShortDateString
            lblDay.Text = "Today's "
        End If

        SeperateMeals()
        txtFood.Focus()

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Dim dtDate As DateTime = txtDate.Text
        Dim strFood As String = txtFood.Text
        Dim strMeal As String = ddlMeal.SelectedValue
        Dim decServings As Decimal = txtServings.Text
        Dim intDatabaseID As Decimal = 0 ' To be added later

        lblInfo.Text = DataOperation("InsertFoodLog", Master.ConStr, dtDate, strFood, strMeal, decServings, intDatabaseID)
        gvLog.DataBind()
        ClearFields()
    End Sub

    Protected Sub ClearFields()
        txtDate.Text = Today.ToShortDateString
        txtFood.Text = ""
        txtServings.Text = ""
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As System.EventArgs) Handles btnNext.Click
        Dim dtDate As DateTime = lblDate.Text
        dtDate = dtDate.AddDays(1)
        lblDate.Text = dtDate.ToShortDateString

        If dtDate = Today Then
            lblDay.Text = "Today's "
        ElseIf dtDate = Today.AddDays(-1) Then
            lblDay.Text = "Yesterday's "
        Else
            lblDay.Text = dtDate.DayOfWeek.ToString & "'s "
        End If
    End Sub

    Protected Sub btnPrev_Click(sender As Object, e As System.EventArgs) Handles btnPrev.Click
        Dim dtDate As DateTime = lblDate.Text
        dtDate = dtDate.AddDays(-1)
        lblDate.Text = dtDate.ToShortDateString

        If dtDate = Today Then
            lblDay.Text = "Today's "
        ElseIf dtDate = Today.AddDays(-1) Then
            lblDay.Text = "Yesterday's "
        Else
            lblDay.Text = dtDate.DayOfWeek.ToString & "'s "
        End If
    End Sub

    Protected Sub SeperateMeals()
        Dim dt As DataTable = GetDataSet("GetFoodLog", Master.ConStr, lblDate.Text).Tables(0)

        Dim strMeal As String = ""

        ' Keep track of totals     
        Dim totCalories As Integer = 0
        Dim totCarbs As Integer = 0
        Dim totProtein As Integer = 0
        Dim totFat As Integer = 0
        Dim totSugar As Integer = 0

        Dim rowCount As Integer = 0

        Dim firstGo As Boolean = True ' True when no subheading has been reached

        Do While rowCount < dt.Rows.Count
            Dim row As DataRow = dt.Rows(rowCount)
            Dim rowMeal As String = row("Meal")
            Dim rowIndex As Integer = dt.Rows.IndexOf(row)

            If rowMeal <> strMeal Then
                strMeal = rowMeal

                If Not firstGo Then
                    ' Add subtotals to new row
                    Dim newRow As DataRow = dt.NewRow
                    newRow("Food") = "Totals"
                    newRow("Calories") = totCalories
                    newRow("Carbs") = totCarbs
                    newRow("Fat") = totFat
                    newRow("Protein") = totProtein
                    newRow("Sugar") = totSugar

                    ' Insert before current row
                    rowIndex = dt.Rows.IndexOf(row)
                    dt.Rows.InsertAt(newRow, rowIndex)
                    rowIndex += 1
                    rowCount += 1
                End If

                'Add Heading
                Dim hdrRow As DataRow = dt.NewRow
                hdrRow("Food") = rowMeal

                ' Insert before current row
                dt.Rows.InsertAt(hdrRow, rowIndex)
                rowCount += 1

                ' Start count over
                totCalories = 0
                totCarbs = 0
                totProtein = 0
                totFat = 0
                totSugar = 0
                firstGo = False

            End If

            totCalories += row("Calories")
            totCarbs += row("Carbs")
            totFat += row("Fat")
            totProtein += row("Protein")
            totSugar += row("Sugar")


            rowCount += 1
        Loop

        ' Need to add subtotals for last grouping
        ' Add subtotals to new row
        Dim RowNew As DataRow = dt.NewRow
        RowNew("Food") = "Totals"
        RowNew("Calories") = totCalories
        RowNew("Carbs") = totCarbs
        RowNew("Fat") = totFat
        RowNew("Protein") = totProtein
        RowNew("Sugar") = totSugar

        ' Insert at end
        dt.Rows.InsertAt(RowNew, rowCount)

        gvLog.DataSource = dt
        gvLog.DataBind()

    End Sub

    Protected Sub gvLog_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLog.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If GetRowControlValue(e.Row, "lblMeal") = "" Then
                e.Row.CssClass = "subhead"
                ' e.Row.FindControl("btnDelete").Visible = False
            End If
            If GetRowControlValue(e.Row, "lblFood") = "Totals" Then
                e.Row.CssClass = "subTotal"
                ' e.Row.FindControl("btnDelete").Visible = False
            End If
        End If
    End Sub
End Class
