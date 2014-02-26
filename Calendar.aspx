<%@ Page Title="Estimator - Calendar" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Calendar.aspx.vb" Inherits="Calendar" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 { width: 100%; }
        .center     { text-align: center; }
        .left       { text-align: left; }
        .red        { color: Red; }
        .brown      { color: Maroon; }
        .blue       { color: Blue; }
        .green      { color: Green; }
        #results    { width: 100%; padding: 1px; }
        #dist       { width: 350px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div id="divPrevProjects" class="grid">
        <h2>Select Project           
            <div id="divFilter">
                <asp:Label ID="Label1" runat="server" Font-Bold="false" Font-Size="11pt" Text="Search:"></asp:Label>
                <asp:TextBox ID="txtFilter" runat="server" onKeyUp="filter(this,'gvProjects',0);" ClientIDMode="Static"></asp:TextBox>
            </div>
        </h2>

    <asp:GridView ID="gvProjects" runat="server" ClientIDMode="Static">
            <Columns>
                <asp:TemplateField HeaderText="Project Name">
                    <ItemTemplate>
                        <asp:Label ID="lblProjectName" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>                        
                    </ItemTemplate>
                    <ItemStyle CssClass="first" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Function Points">
                    <ItemTemplate>
                        <asp:Label ID="lblFunctionPoints" runat="server" Text='<%# Bind("FunctionPoints") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Language">
                    <ItemTemplate>
                        <asp:Label ID="lblLanguage" runat="server" Text='<%# Bind("Language") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Project Class">
                    <ItemTemplate>
                        <asp:Label ID="lblProjClass" runat="server" Text='<%# Bind("ProjectClass") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Lines of Code">
                    <ItemTemplate>
                        <asp:Label ID="lblLoC" runat="server" Text='<%# Bind("LoC") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Effort">
                    <ItemTemplate>
                        <asp:Label ID="lblEffort" runat="server" Text='<%# Bind("Effort") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Schedule">
                    <ItemTemplate>
                        <asp:Label ID="lblSchedule" runat="server" Text='<%# Bind("Schedule") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Cost">
                    <ItemTemplate>
                        <asp:Label ID="lblCost" runat="server" Text='<%# Bind("Cost","{0:C0}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                 <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIDNo" runat="server" Text='<%# Bind("IDNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

               <asp:TemplateField HeaderText="Start Date">
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("StartDate", "{0:MMM-dd-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:TemplateField HeaderText="End Date">
                    <ItemTemplate>
                        <asp:Label ID="lblebdDate" runat="server" Text='<%# Bind("EndDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="true" value='<%# Eval("IDNo") %>'
                            OnCheckedChanged="RadioButton1_CheckedChanged" />
                        <%-- <input id="Radio1" type="radio" value='<%# Eval("IDNo") %>'>  />--%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" CssClass="noPrint" />
                </asp:TemplateField>
            </Columns>
    </asp:GridView>

    </div>


    <br /><br />
    <table class="auto-style1">
        <tr>
            <td colspan="2">
                <asp:Calendar ID="Calendar1" runat="server" Width="100%">
                    <TodayDayStyle BorderColor="#CCCCCC" BorderStyle="Solid" />
                </asp:Calendar>
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="false">
                <table id="results" class="left">
                    <tr>
                        <td style="vertical-align: top; text-align: left;">
                            <br />
                            <strong>Software Development (Elaboration and Construction)</strong><br /><br />
                            Project = <asp:Label ID="lblProject" runat="server" Text=""></asp:Label><br />
                            Effort = <asp:Label ID="lblEffort" runat="server" Text="0"></asp:Label>&nbsp;Person-Months<br />
                            Schedule = <asp:Label ID="lblSchedule" runat="server" Text="0"></asp:Label>&nbsp;Months<br />
                            Cost = <asp:Label ID="lblCost" runat="server" Text="0"></asp:Label><br /><br />
                            Total Equivalent Size = <asp:Label ID="lblSloc" runat="server" Text="0"></asp:Label>&nbsp;SLOC<br /><br />
                            <strong>Acquisition Phase Distribution</strong><br />
                            <table id="dist" border="1">
                                <tr>
                                    <td>Phase</td>
                                    <td>Effort</td>
                                    <td>Schedule</td>
                                    <td>Average Staff</td>
                                    <td>Cost</td>
                                </tr>
                                <tr>
                                    <td><span class="red">Inception</span></td>
                                    <td class="center"><asp:Label ID="lblInception1" runat="server" Text=""></asp:Label></td>
                                    <td class="center"><asp:Label ID="lblInception2" runat="server" Text=""></asp:Label></td>
                                    <td class="center"><asp:Label ID="lblInception3" runat="server" Text=""></asp:Label></td>
                                    <td><asp:Label ID="lblInception4" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><span class="brown">Elaboration</span></td>
                                    <td class="center"><asp:Label ID="lblElaboration1" runat="server" Text=""></asp:Label></td>
                                    <td class="center"><asp:Label ID="lblElaboration2" runat="server" Text=""></asp:Label></td>
                                    <td class="center"><asp:Label ID="lblElaboration3" runat="server" Text=""></asp:Label></td>
                                    <td><asp:Label ID="lblElaboration4" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><span class="blue">Construction</span></td>
                                    <td class="center"><asp:Label ID="lblConstruction1" runat="server" Text=""></asp:Label></td>
                                    <td class="center"><asp:Label ID="lblConstruction2" runat="server" Text=""></asp:Label></td>
                                    <td class="center"><asp:Label ID="lblConstruction3" runat="server" Text=""></asp:Label></td>
                                    <td><asp:Label ID="lblConstruction4" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><span class="green">Transition</span></td>
                                    <td class="center"><asp:Label ID="lblTransition1" runat="server" Text=""></asp:Label></td>
                                    <td class="center"><asp:Label ID="lblTransition2" runat="server" Text=""></asp:Label></td>
                                    <td class="center"><asp:Label ID="lblTransition3" runat="server" Text=""></asp:Label></td>
                                    <td><asp:Label ID="lblTransition4" runat="server" Text=""></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td><strong>Staffing Profile</strong></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:PlaceHolder ID="phImage" runat="server"></asp:PlaceHolder>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:PlaceHolder>
    </table>
    
</asp:Content>

