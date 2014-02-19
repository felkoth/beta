<%@ Page Title="Food Log" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="Log.aspx.vb" Inherits="Log" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="divLog">
        <h2>Enter Food</h2>
        
        <table id="tblFoodEntry" cellpadding="5px">
            <tr>
                <td><span>Date:</span></td>
                <td><asp:TextBox ID="txtDate" runat="server" Width="80px"></asp:TextBox>
                    <ajax:CalendarExtender ID="txtDateE" runat="server" TargetControlID="txtDate"></ajax:CalendarExtender>
                </td>
          
                <td><span>Food:</span></td>
                <td><asp:TextBox ID="txtFood" runat="server" Width="250px"></asp:TextBox></td>
          
                <td><span>Servings:</span></td>
                <td><asp:TextBox ID="txtServings" runat="server" Width="80px"></asp:TextBox></td>

                <td><span>Meal:</span></td>
                <td>
                    <asp:DropDownList ID="ddlMeal" runat="server">
                        <asp:ListItem>Breakfast</asp:ListItem>
                        <asp:ListItem>Lunch</asp:ListItem>
                        <asp:ListItem>Dinner</asp:ListItem>
                        <asp:ListItem>Snack</asp:ListItem>
                    </asp:DropDownList>
                </td>
                
                <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="100px" /></td>
            </tr>
            <tr>
                <td colspan="9"><asp:Label ID="lblInfo" runat="server" ForeColor="Blue"></asp:Label></td>
            </tr>
        </table>
        <div class="divider"></div>
        <h2 style="float:left;">
            <asp:Label ID="lblDay" runat="server"></asp:Label>Log            
        </h2> 
        
        <div id="divGridControls">
            <asp:Label ID="lblDate" runat="server" Visible="true"></asp:Label>
            <asp:LinkButton ID="btnPrev" runat="server" Text=" &lt;&lt; Prev"></asp:LinkButton>
            <asp:LinkButton ID="btnNext" runat="server" Text="Next &gt;&gt;"></asp:LinkButton>
        </div>

        <div id="divLogGrid" class="grid">            
                         
            <asp:GridView ID="gvLog" runat="server">
                <Columns>
                    
                    <asp:TemplateField HeaderText="Food">
                        <ItemStyle CssClass="first" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblFood" runat="server" Text='<%# Bind("Food") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                                    

                    <asp:TemplateField HeaderText="Servings">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblServings" runat="server" Text='<%# Bind("Servings") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Calories">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCalories" runat="server" Text='<%# Bind("Calories") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Carbs">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCarbs" runat="server" Text='<%# Bind("Carbs") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Protein">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblProtein" runat="server" Text='<%# Bind("Protein") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fat">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblFat" runat="server" Text='<%# Bind("Fat") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sugar">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSugar" runat="server" Text='<%# Bind("Sugar") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Meal" Visible="false">                      
                        <ItemTemplate>
                            <asp:Label ID="lblMeal" runat="server" Text='<%# Bind("Meal") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>            
        </div>
    </div>


    

   
</asp:Content>

