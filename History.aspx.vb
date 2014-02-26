Imports Helper
Imports Utilities

Partial Class History
    Inherits System.Web.UI.Page

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ' Higlight the navigation tab for this page
        CType(Master.FindControl("topmenu"), Menu).Items(1).Selected = True
    End Sub
End Class
