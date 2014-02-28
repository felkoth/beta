<%@ Page Title="Estimator - Compare" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="Compare.aspx.vb" Inherits="Analysis" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="divAnalysis" class="grid">
        <h2>Compare Results</h2>
        <br />
        <asp:GridView ID="gvProjects" runat="server" >
            <Columns>
                <asp:TemplateField HeaderText="Project Name">
                    <ItemTemplate>
                        <asp:Label ID="lblProjectName" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>                        
                    </ItemTemplate>
                    <ItemStyle CssClass="first" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="FP">
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

                <asp:TemplateField HeaderText="Class" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblProjClass" runat="server" Text='<%# Bind("ProjectClass") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Prog. Capability">
                    <ItemTemplate>
                        <asp:Label ID="lblProgCap" runat="server" Text='<%# Bind("ProgCap") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Language/T.S. Experience">
                    <ItemTemplate>
                        <asp:Label ID="lblLangExp" runat="server" Text='<%# Bind("LangExp") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Software Complexity">
                    <ItemTemplate>
                        <asp:Label ID="lblSoftComplex" runat="server" Text='<%# Bind("SoftComplex") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Doc. Required">
                    <ItemTemplate>
                        <asp:Label ID="lblDocumentation" runat="server" Text='<%# Bind("Documentation") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Cost per PM">
                    <ItemTemplate>
                        <asp:Label ID="lblCostPerPM" runat="server" Text='<%# Bind("CostPerPM","{0:C0}") %>'></asp:Label>
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

                <asp:TemplateField HeaderText="Compare" HeaderStyle-CssClass="noPrint">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkCompare" runat="server" AutoPostBack="true" OnCheckedChanged="chkCompare_Checked"/>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" CssClass="noPrint" />
                </asp:TemplateField>

            
            </Columns>
        </asp:GridView>

    </div>

</asp:Content>

