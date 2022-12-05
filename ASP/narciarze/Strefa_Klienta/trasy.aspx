<%@ Page Title="Trasy" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="trasy.aspx.cs" Inherits="narciarze.trasy" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
                      
                                <div>
                                    <h2>Grupa Zielona</h2>
                   <asp:Table ID="Tabela_Stoki_Zielone" runat="server" Font-Size="Larger" HorizontalAlign="Center"  CellPadding="10" GridLines="Both" BorderStyle="Solid" BorderColor="Black">
                         <asp:TableHeaderRow BorderStyle="Solid" BorderColor="Black" BackColor="LightGray" >
                            <asp:TableHeaderCell>Nazwa Wyciągu</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                        </asp:TableHeaderRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>A1</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>A2</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>A3</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                    </asp:Table>

                </div>

                <div>
                    <h2>Grupa Niebieska</h2>
                   <asp:Table ID="Tabela_Stoki_Niebieskie" runat="server" Font-Size="Larger" HorizontalAlign="Center"  CellPadding="10" GridLines="Both" BorderStyle="Solid" BorderColor="Black">
                         <asp:TableHeaderRow BorderStyle="Solid" BorderColor="Black" BackColor="LightGray" >
                            <asp:TableHeaderCell>Nazwa Wyciągu</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                        </asp:TableHeaderRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>B1</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>B2</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>B3</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>B4</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                    </asp:Table>
                </div>

                <div>
                    <h2>Grupa Czerwona</h2>
                    <asp:Table ID="Tabla_Stoki_Czerwone" runat="server" Font-Size="Larger" HorizontalAlign="Center"  CellPadding="10" GridLines="Both" BorderStyle="Solid" BorderColor="Black">
                         <asp:TableHeaderRow BorderStyle="Solid" BorderColor="Black" BackColor="LightGray" >
                            <asp:TableHeaderCell>Nazwa Wyciągu</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                        </asp:TableHeaderRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>C1</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>C2</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>C3</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                    </asp:Table>

                </div>

</asp:Content>
