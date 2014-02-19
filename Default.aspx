<%@ Page Title="Software Estimator" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <div id="divInput">
        <h2>Project Data</h2>

        <table cellpadding="5px" width="500px"> 
            <tr>
                <td><asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Project Name"></asp:Label></td>
                <td><asp:TextBox ID="txtProjectName" Width="150px" runat="server" ></asp:TextBox></td>
                <asp:RequiredFieldValidator ID="ProjectNameReq" ControlToValidate="txtProjectName" Display="none" ErrorMessage="This is a required field." runat="server" ValidationGroup="On" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="txtProjectNameReqE" runat="server" TargetControlID="ProjectNameReq"></ajax:ValidatorCalloutExtender>
                <asp:Label ID="lblIDNo" runat="server" Visible="false"></asp:Label>

            </tr>
            <tr>
                <td><asp:Label runat="server" Font-Bold="true" Text="Function Points"></asp:Label></td>
                <td><asp:TextBox ID="txtFunctionPoints" Width="70px" runat="server" style="text-align:right;" ></asp:TextBox></td>
                <ajax:FilteredTextBoxExtender runat="server" ID="txtFunctionPointsE" TargetControlID="txtFunctionPoints" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="FunctionPointsReq" ControlToValidate="txtFunctionPoints" Display="none" ErrorMessage="This is a required field." runat="server" ValidationGroup="On" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="FunctionPointsReqE" runat="server" TargetControlID="FunctionPointsReq"></ajax:ValidatorCalloutExtender>
            </tr>
            <tr>
                <td><asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Language"></asp:Label></td>
                <td>
                    <asp:DropDownList id="ddlLanguage" runat="server" AutoPostBack="true">
                        <asp:ListItem>Select Language</asp:ListItem>
                        <asp:ListItem>Java</asp:ListItem>
                        <asp:ListItem>C#</asp:ListItem>
                        <asp:ListItem>Javascript</asp:ListItem>
                        <asp:ListItem>ASP</asp:ListItem>
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="LanguageReq" ControlToValidate="ddlLanguage" Display="none" ErrorMessage="This is a required field." runat="server" SetFocusOnError="true" ValidationGroup="On" InitialValue="Select Language"></asp:RequiredFieldValidator>
                    <ajax:ValidatorCalloutExtender ID="LanguageReqE" runat="server" TargetControlID="LanguageReq"></ajax:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Project Class"></asp:Label></td>
                <td colspan="2">
                    <asp:RadioButtonList ID="rbtnProjectClass" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                        <asp:ListItem Title="'Small' teams with 'good' experience working with 'less than rigid' requirements">Organic</asp:ListItem>
                        <asp:ListItem Title="'Medium' teams with mixed experience working with a mix of rigid and less than rigid requirements">Semi-Detached</asp:ListItem>
                        <asp:ListItem Title="Developed within a set of 'tight' constraints. It is also combination of organic and semi-detached projects.(hardware, software, operational, ...)">Embedded</asp:ListItem>
                    </asp:RadioButtonList>

                    <asp:RequiredFieldValidator ID="ProjectClassReq" ControlToValidate="rbtnProjectClass" Display="none" ErrorMessage="This is a required field." runat="server" SetFocusOnError="true" ValidationGroup="On"></asp:RequiredFieldValidator>
                    <ajax:ValidatorCalloutExtender ID="ProjectClassReqE" runat="server" TargetControlID="ProjectClassReq"></ajax:ValidatorCalloutExtender>
                </td>
            </tr>
        </table>

        <br />
        <h2>Cost Drivers</h2>
        <table cellpadding="5px"> 
            <tr>
                <td><asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Programer Capability" ToolTip="Measure of the developers overall capability level. Low will increase cost and effort, high will decrease it."></asp:Label></td>
                <td>
                    <asp:Textbox id="txtProgCap" runat="server" Text="2" AutoPostBack="true"></asp:Textbox>
                    <ajax:SliderExtender ID="txtProgCapSlider" runat="server" TargetControlID="txtProgCap" Minimum="1" Maximum="3" Steps="3"/>                    
                </td>
                <td style="width:100px;"><asp:Label ID="lblProgCap" runat="server" Text="Nominal" ></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Language/Toolset Experience" ToolTip="Measure of developers experience with the language and/or toolset used. Low will increase cost and effort, high will decrease it."></asp:Label></td>
                <td>
                    <asp:Textbox id="txtLangToolExp" runat="server" Text="2" AutoPostBack="true"></asp:Textbox>
                    <ajax:SliderExtender ID="txtLangToolExpSlider" runat="server" TargetControlID="txtLangToolExp" Minimum="1" Maximum="3" Steps="3"/>                    
                </td>
                <td><asp:Label ID="lblLangToolExp" runat="server" Text="Nominal" ></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Software Complexity" ToolTip="Measure of the projects complexity. Low will decrease cost and effort, high will increase it."></asp:Label></td>
                <td>
                    <asp:Textbox id="txtSoftComplex" runat="server" Text="3" AutoPostBack="true"></asp:Textbox>
                    <ajax:SliderExtender ID="txtSoftComplexSlider" runat="server" TargetControlID="txtSoftComplex" Minimum="1" Maximum="5" Steps="5"/>                    
                </td>
                <td><asp:Label ID="lblSoftComplex" runat="server" Text="Nominal" ></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label8" runat="server" Font-Bold="true" Text="Documentation Required" ToolTip="Measure of how much documentation will be required. Low will decrease cost and effort, high will increase."></asp:Label></td>
                <td>
                    <asp:Textbox id="txtDocumentation" runat="server" Text="2" AutoPostBack="true"></asp:Textbox>
                    <ajax:SliderExtender ID="txtDocumentationSlider" runat="server" TargetControlID="txtDocumentation" Minimum="1" Maximum="3" Steps="3"/>                    
                </td>
                <td><asp:Label ID="lblDocumentation" runat="server" Text="Nominal" ></asp:Label></td>
            </tr>

            <tr>
                <td><asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Cost per Person-Month"></asp:Label></td>
                <td><asp:TextBox ID="txtCostPersonMonth" Width="70px" runat="server" style="text-align:right;" ></asp:TextBox></td>
                <ajax:FilteredTextBoxExtender runat="server" ID="txtCostPersonMonthE" TargetControlID="txtCostPersonMonth" FilterType="Numbers,Custom" ValidChars="."></ajax:FilteredTextBoxExtender>
            
                <asp:RequiredFieldValidator ID="CostPersonMonthReq" ControlToValidate="txtCostPersonMonth" Display="none" ErrorMessage="This is a required field." runat="server" SetFocusOnError="true" ValidationGroup="On"></asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="CostPersonMonthReqE" runat="server" TargetControlID="CostPersonMonthReq"></ajax:ValidatorCalloutExtender>
            </tr>

        </table>
    </div>

    <br />
    <div id="divResults">
        <h2>Results</h2>

        <table cellpadding="5px">
            <tr>
                <td><asp:Label ID="Label11" runat="server" Font-Bold="true" Text="Equivalent Size"></asp:Label></td>
                <td>
                    <asp:Label ID="lblLoC" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Label ID="Label14" runat="server" Font-Bold="true" Text="Lines of Code"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Effort"></asp:Label></td>
                <td>
                    <asp:Label ID="lblEffort" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Label ID="Label15" runat="server" Font-Bold="true" Text="Person-Months"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Schedule"></asp:Label></td>
                <td>
                    <asp:Label ID="lblSchedule" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Label ID="Label16" runat="server" Font-Bold="true" Text="Months"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="Label12" runat="server" Font-Bold="true" Text="People Required"></asp:Label></td>
                <td>
                <asp:Label ID="lblPeople" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Label ID="Label17" runat="server" Font-Bold="true" Text="People"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="Label10" runat="server" Font-Bold="true" Text="Final Cost"></asp:Label></td>
                <td><asp:Label ID="lblFinalCost" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
        </table>

        <br />
        <center>
            <asp:LinkButton ID="btnReCalc" runat="server" >Re-Calculate</asp:LinkButton>
            <asp:LinkButton ID="btnSave" runat="server" ValidationGroup="On" >Save</asp:LinkButton>
            <asp:LinkButton ID="btnReset" runat="server" >Reset</asp:LinkButton>
        </center>

    </div>
    <div class="clearfix"></div>
    <br /><br />
    <div id="divPrevProjects" class="grid">
        <h2>Saved Projects           
            <div id="divFilter">
                <asp:Label runat="server" Font-Bold="false" Font-Size="11pt" Text="Search:"></asp:Label>
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

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/_images/icon-edit.gif" CommandName="Sel"/>
                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/_images/bin_closed.png" CommandName="Del" ClientIDMode="Predictable"/>
                        <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/_images/report.png" CommandName="Print"/>
                        
                        <ajax:ConfirmButtonExtender ID="confirmDelete" TargetControlID="btnDelete" ClientIDMode="Predictable" ConfirmText="Are you sure you want to permanently delete this project?" runat="server" ></ajax:ConfirmButtonExtender>
                    </ItemTemplate>                   
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    
    </div>

</asp:Content>

