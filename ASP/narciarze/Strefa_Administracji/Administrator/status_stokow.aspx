<%@ Page Title="Strona Główna" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeBehind="status_stokow.aspx.cs" Inherits="narciarze._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

                   <asp:Table ID="Tabela_Stoki_Zielone" runat="server" Font-Size="Larger" HorizontalAlign="Center"  CellPadding="10" GridLines="Both" BorderStyle="Solid" BorderColor="Black">
                         <asp:TableHeaderRow BorderStyle="Solid" BorderColor="Black" BackColor="LightGray" >
                            <asp:TableHeaderCell>Nazwa Wyciągu</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Otwarty</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Zamknięty</asp:TableHeaderCell>
                        </asp:TableHeaderRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>A1</asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_A1" value="open"><br> </asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_A1" value="close"><br> </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>A2</asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_A2" value="open"><br> </asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_A2" value="close"><br> </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>A3</asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_A3" value="open"><br> </asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_A3" value="close"><br> </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>B1</asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_B1" value="open"><br> </asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_B2" value="close"><br> </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>B2</asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_B2" value="open"><br> </asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_B2" value="close"><br> </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>B3</asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_B3" value="open"><br> </asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_B3" value="close"><br> </asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>B4</asp:TableCell>
                               <asp:TableCell> <input type="radio" name="status_B4" value="open"><br> </asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_B4" value="close"><br> </asp:TableCell>
                            </asp:TableRow>               

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>C1</asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_C1" value="open"><br> </asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_C1" value="close"><br> </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>C2</asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_C2" value="open"><br> </asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_C2" value="close"><br> </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>C3</asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_C3" value="open"><br> </asp:TableCell>
                                <asp:TableCell> <input type="radio" name="status_C3" value="close"><br> </asp:TableCell>
                            </asp:TableRow>

                    </asp:Table>

    <input type="submit" value="Zatwierdź">


</asp:Content>
