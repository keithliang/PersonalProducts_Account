<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="Disperse_Account" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Account_numbers" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Account_numbers" HeaderText="Account_numbers" ReadOnly="True" SortExpression="Account_numbers" />
                <asp:BoundField DataField="Account_name" HeaderText="Account_name" SortExpression="Account_name" />
                <asp:TemplateField HeaderText="Account_state" SortExpression="Account_state">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Account_state") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Account_state") %>'></asp:Label>
                        <br />
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Account_state") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [Account_Numbers_D] WHERE [Account_numbers] = @Account_numbers" InsertCommand="INSERT INTO [Account_Numbers_D] ([Account_numbers], [Account_name], [Account_state]) VALUES (@Account_numbers, @Account_name, @Account_state)" SelectCommand="SELECT * FROM [Account_Numbers_D]" UpdateCommand="UPDATE [Account_Numbers_D] SET [Account_name] = @Account_name, [Account_state] = @Account_state WHERE [Account_numbers] = @Account_numbers">
            <DeleteParameters>
                <asp:Parameter Name="Account_numbers" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Account_numbers" Type="Int32" />
                <asp:Parameter Name="Account_name" Type="String" />
                <asp:Parameter Name="Account_state" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Account_name" Type="String" />
                <asp:Parameter Name="Account_state" Type="Int32" />
                <asp:Parameter Name="Account_numbers" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
