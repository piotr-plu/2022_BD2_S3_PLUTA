<%@ Page Title="Cennik" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="cennik.aspx.cs" Inherits="narciarze.cennik" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 
                <div>
                    <h2 >Cennik Karnety</h2>
                    <asp:Table ID="Tabela_Cennik_Karnety" Font-Size="Larger" runat="server"  HorizontalAlign="Center"  CellPadding="10" GridLines="Both" BorderStyle="Solid" BorderColor="Black">
                        <asp:TableHeaderRow BorderStyle="Solid" BorderColor="Black" BackColor="LightGray" >
                            <asp:TableHeaderCell>Stok</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Czas [h]</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Cenna karnetu</asp:TableHeaderCell>
                        </asp:TableHeaderRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="LightGreen">
                                <asp:TableCell >Zielony</asp:TableCell>
                                <asp:TableCell>6</asp:TableCell>
                                <asp:TableCell>20</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="LightGreen">
                                <asp:TableCell>Zielony</asp:TableCell>
                                <asp:TableCell>12</asp:TableCell>
                                <asp:TableCell>40</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="LightGreen">
                                <asp:TableCell>Zielony</asp:TableCell>
                                <asp:TableCell>24</asp:TableCell>
                                <asp:TableCell>80</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="DeepSkyBlue">
                                <asp:TableCell>Niebieski</asp:TableCell>
                                <asp:TableCell>6</asp:TableCell>
                                <asp:TableCell>20</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="DeepSkyBlue">
                                <asp:TableCell>Niebieski</asp:TableCell>
                                <asp:TableCell>12</asp:TableCell>
                                <asp:TableCell>40</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="DeepSkyBlue">
                                <asp:TableCell>Niebieski</asp:TableCell>
                                <asp:TableCell>24</asp:TableCell>
                                <asp:TableCell>80</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="IndianRed"> 
                                <asp:TableCell>Czerwony</asp:TableCell>
                                <asp:TableCell>6</asp:TableCell>
                                <asp:TableCell>20</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="IndianRed">  
                                <asp:TableCell>Czerwony</asp:TableCell>
                                <asp:TableCell>12</asp:TableCell>
                                <asp:TableCell>40</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="IndianRed">  
                                <asp:TableCell>Czerwony</asp:TableCell>
                                <asp:TableCell>24</asp:TableCell>
                                <asp:TableCell>80</asp:TableCell>
                            </asp:TableRow>

                    </asp:Table>

                </div>

                <div>
                    <h2>Cennik Bilety</h2>
                    <asp:Table ID="Tabela_Cennik_Bilety" Font-Size="Larger" runat="server" HorizontalAlign="Center" CellPadding="10" GridLines="Both">
                        <asp:TableHeaderRow BorderStyle="Solid" BorderColor="Black" BackColor="LightGray">
                            <asp:TableHeaderCell>Wyciąg</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Cena za zjazd</asp:TableHeaderCell>
                        </asp:TableHeaderRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>A1</asp:TableCell>
                                <asp:TableCell>6</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>A2</asp:TableCell>
                                <asp:TableCell>12</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>A3</asp:TableCell>
                                <asp:TableCell>24</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>B1</asp:TableCell>
                                <asp:TableCell>6</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>B2</asp:TableCell>
                                <asp:TableCell>12</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>B3</asp:TableCell>
                                <asp:TableCell>24</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>B4</asp:TableCell>
                                <asp:TableCell>6</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>C1</asp:TableCell>
                                <asp:TableCell>12</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black" HorizontalAlign="Center" BackColor="WhiteSmoke">
                                <asp:TableCell>C2</asp:TableCell>
                                <asp:TableCell>24</asp:TableCell>
                            </asp:TableRow>

                    </asp:Table>

                </div>

</asp:Content>
