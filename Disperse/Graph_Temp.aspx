<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Graph_Temp.aspx.cs" Inherits="Graph_Temp" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Expr1" 
            DataValueField="Expr1">
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click1" 
            Text="Button (選取月份)" />
        <br />

        <asp:Chart ID="Chart1" runat="server" Width="500px" Height="500px">
            <series>
                <asp:Series Name="Series1" XValueMember="Account_name" YValueMembers="COUNT" 
                    Legend="Legend1" IsValueShownAsLabel="True" ChartType="Pyramid">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
                        <Legends>
                <asp:Legend Font="新細明體, 9.75pt" IsTextAutoFit="False" Name="Legend1" 
                    Title="科目說明" TitleBackColor="White" TitleFont="新細明體, 12pt, style=Bold">
                </asp:Legend>
            </Legends>
        </asp:Chart>
        <br />
        <br />
<%--        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            SelectCommand="SELECT Account_name, COUNT(Account_name) AS count FROM [Account_Order_M_View] WHERE (Order_date LIKE '%' + @order_date + '%') GROUP BY Account_name">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="order_date"  PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>--%>

    
        <br />
</asp:Content>

