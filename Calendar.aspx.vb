Imports Helper
Imports Utilities
Imports System.Data

Partial Class Calendar
    Inherits System.Web.UI.Page

    Protected theDates As IList(Of DateTime) = New List(Of DateTime)
    Protected coll As Collection = New Collection()

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete

        ' Higlight the navigation tab for this page
        CType(Master.FindControl("topmenu"), Menu).Items(1).Selected = True

        Dim theDates As List(Of DateTime) = New List(Of DateTime)

        If Not IsPostBack Then
            LoadPreviousProjects()
        End If

        If coll.Count > 0 Then
            PlaceHolder1.Visible = True
            LoadTable()
        End If



    End Sub

    Protected Sub LoadPreviousProjects()

        Dim ds As DataSet = GetPreviousProjects()
        gvProjects.DataSource = ds
        gvProjects.DataBind()

    End Sub

    Protected Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs)

        ' clear all radiobuttons
        For Each gvr As GridViewRow In gvProjects.Rows
            CType(gvr.FindControl("RadioButton1"), RadioButton).Checked = False
        Next

        ' get value of selected radio button
        Dim rdoButtonValue As String = DirectCast(sender, System.Web.UI.WebControls.RadioButton).Attributes("value").ToString()
        coll = GetSavedProjectData(rdoButtonValue)

        ' populate list of dates
        Dim csCode As Common = New Common()
        Dim durDays As Integer = Math.Truncate(coll("Schedule") * 20)


        csCode.CalenderStuff(coll("StartDate"), durDays, theDates)

        ' check current row radiobutton
        Dim senderRB As RadioButton = sender
        senderRB.Checked = True

        Dim row As GridViewRow = senderRB.NamingContainer

        If senderRB.Checked Then
            row.CssClass = "row"
        Else
            row.CssClass = "row grey"
        End If

    End Sub

    Protected Sub LoadTable()

        Dim pcntInception1 As Double = 0.06
        Dim pcntInception2 As Double = 0.125
        Dim pcntElaboration1 As Double = 0.24
        Dim pcntElaboration2 As Double = 0.375
        Dim pcntConstruction1 As Double = 0.76
        Dim pcntConstruction2 As Double = 0.625
        Dim pcntTransition1 As Double = 0.12
        Dim pcntTransition2 As Double = 0.125

        Dim inceptionMonth As Double = 0.0
        Dim elaborationMonth As Double = 0.0
        Dim constructionMonth As Double = 0.0
        Dim transitionMonth As Double = 0.0

        Dim inceptionStaff As Double = 0.0
        Dim elaborationStaff As Double = 0.0
        Dim constructionStaff As Double = 0.0
        Dim transitionStaff As Double = 0.0

        lblProject.Text = coll("ProjectName")
        lblEffort.Text = coll("Effort")
        lblSchedule.Text = coll("Schedule")
        lblCost.Text = String.Format("{0:c0}", coll("Cost"))
        lblSloc.Text = String.Format("{0:n0}", coll("LoC"))

        lblInception1.Text = String.Format("{0:f1}", coll("Effort") * pcntInception1)
        inceptionMonth = coll("Schedule") * pcntInception2
        inceptionStaff = (coll("Effort") * pcntInception1) / inceptionMonth
        lblInception2.Text = String.Format("{0:f1}", inceptionMonth)
        lblInception3.Text = String.Format("{0:f1}", inceptionStaff)
        lblInception4.Text = String.Format("{0:c0}", coll("Cost") * pcntInception1)

        lblElaboration1.Text = String.Format("{0:f1}", coll("Effort") * pcntElaboration1)
        elaborationMonth = coll("Schedule") * pcntElaboration2
        elaborationStaff = (coll("Effort") * pcntElaboration1) / elaborationMonth
        lblElaboration2.Text = String.Format("{0:f1}", elaborationMonth)
        lblElaboration3.Text = String.Format("{0:f1}", elaborationStaff)
        lblElaboration4.Text = String.Format("{0:c0}", coll("Cost") * pcntElaboration1)

        lblConstruction1.Text = String.Format("{0:f1}", coll("Effort") * pcntConstruction1)
        constructionMonth = coll("Schedule") * pcntConstruction2
        constructionStaff = (coll("Effort") * pcntConstruction1) / constructionMonth
        lblConstruction2.Text = String.Format("{0:f1}", constructionMonth)
        lblConstruction3.Text = String.Format("{0:f1}", constructionStaff)
        lblConstruction4.Text = String.Format("{0:c0}", coll("Cost") * pcntConstruction1)

        lblTransition1.Text = String.Format("{0:f1}", coll("Effort") * pcntTransition1)
        transitionMonth = coll("Schedule") * pcntTransition2
        transitionStaff = (coll("Effort") * pcntTransition1) / transitionMonth
        lblTransition2.Text = String.Format("{0:f1}", transitionMonth)
        lblTransition3.Text = String.Format("{0:f1}", transitionStaff)
        lblTransition4.Text = String.Format("{0:c0}", coll("Cost") * pcntTransition1)

        Dim numMonths As Integer = Math.Floor(inceptionMonth) + Math.Floor(elaborationMonth) + Math.Floor(constructionMonth) + Math.Floor(transitionMonth)

        LoadChart(Math.Floor(inceptionMonth), Math.Floor(elaborationMonth), Math.Floor(constructionMonth), Math.Floor(transitionMonth),
                  inceptionStaff, elaborationStaff, constructionStaff, transitionStaff)

    End Sub

    Protected Sub LoadChart(ByVal inceptionMonths As Integer, ByVal elaborationMonths As Integer, ByVal constructionMonths As Integer, ByVal transitionMonths As Integer,
                            ByVal inceptionStaff As Double, ByVal elaborationStaff As Double, ByVal constructionStaff As Double, ByVal transitionStaff As Double)

        Dim totalColums As Integer = inceptionMonths + elaborationMonths + constructionMonths + transitionMonths
        Dim colInception As Integer = inceptionMonths
        Dim colElaboration As Integer = elaborationMonths
        Dim colConstruction As Integer = constructionMonths
        Dim colTransition As Integer = transitionMonths
        Dim pcntInceptionStaff As Double = inceptionStaff / constructionStaff
        Dim pcntElaborationStaff As Double = elaborationStaff / constructionStaff
        Dim pcntConstructionStaff As Double = constructionStaff / constructionStaff
        Dim pcntTransitionStaff As Double = transitionStaff / constructionStaff


        phImage.Controls.Add(New LiteralControl("TEST"))

    End Sub

    Protected Sub Calendar1_DayRender(sender As Object, e As DayRenderEventArgs) Handles Calendar1.DayRender

        'create label for each day
        Dim dayLabel As New Label()
        dayLabel.Width = Unit.Percentage(100)
        dayLabel.Height = Unit.Pixel(30)
        dayLabel.Text = "<br />"


        If theDates.Count <> 0 Then

            For Each item As DateTime In theDates

                If e.Day.Date = item Then

                    'e.Cell.BackColor = Drawing.Color.GhostWhite

                    dayLabel.BorderColor = Drawing.Color.BlueViolet
                    dayLabel.BorderStyle = BorderStyle.Solid
                    dayLabel.BorderWidth = 1
                    dayLabel.Text = coll("ProjectName")
                    'e.Cell.ToolTip = "Project #1"

                End If

            Next

            If e.Day.Date = theDates.First Then
                dayLabel.BorderColor = Drawing.Color.LightGreen
            End If

            If e.Day.Date = theDates.Last Then
                dayLabel.BorderColor = Drawing.Color.Red
            End If

        End If

        e.Cell.Controls.Add(dayLabel)
        'End If

    End Sub

End Class
