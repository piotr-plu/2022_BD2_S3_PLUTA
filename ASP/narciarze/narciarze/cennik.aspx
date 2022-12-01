<%@ Page Title="Cennik" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cennik.aspx.cs" Inherits="narciarze.cennik" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <article>

            <section>
                <h2>&compfn; Cennik karnety </h2>
                <div>
                    <asp:Table ID="Tabela_Cennik_Karnety" Caption="Cennik Karnety" CaptionAlign="Top" runat="server"  HorizontalAlign="Left"  CellPadding="10" GridLines="Both">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell>Stok</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Czas [h]</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Cenna karnetu</asp:TableHeaderCell>
                        </asp:TableHeaderRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell >Zielony</asp:TableCell>
                                <asp:TableCell>6</asp:TableCell>
                                <asp:TableCell>20</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>Zielony</asp:TableCell>
                                <asp:TableCell>12</asp:TableCell>
                                <asp:TableCell>40</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>Zielony</asp:TableCell>
                                <asp:TableCell>24</asp:TableCell>
                                <asp:TableCell>80</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>Czerwony</asp:TableCell>
                                <asp:TableCell>6</asp:TableCell>
                                <asp:TableCell>20</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>Czerwony</asp:TableCell>
                                <asp:TableCell>12</asp:TableCell>
                                <asp:TableCell>40</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>Czerwony</asp:TableCell>
                                <asp:TableCell>24</asp:TableCell>
                                <asp:TableCell>80</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>Niebieski</asp:TableCell>
                                <asp:TableCell>6</asp:TableCell>
                                <asp:TableCell>20</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>Niebieski</asp:TableCell>
                                <asp:TableCell>12</asp:TableCell>
                                <asp:TableCell>40</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>Niebieski</asp:TableCell>
                                <asp:TableCell>24</asp:TableCell>
                                <asp:TableCell>80</asp:TableCell>
                            </asp:TableRow>

                    </asp:Table>

                </div>

                <div>
                    <asp:Table ID="Tabela_Cennik_Bilety" Caption="Cennik Bilety" runat="server" HorizontalAlign="Center" CellPadding="10" GridLines="Both">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell>Wyciąg</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Cena za zjazd</asp:TableHeaderCell>
                        </asp:TableHeaderRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>A1</asp:TableCell>
                                <asp:TableCell>6</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>A2</asp:TableCell>
                                <asp:TableCell>12</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>A3</asp:TableCell>
                                <asp:TableCell>24</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>B1</asp:TableCell>
                                <asp:TableCell>6</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>B2</asp:TableCell>
                                <asp:TableCell>12</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>B3</asp:TableCell>
                                <asp:TableCell>24</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>B4</asp:TableCell>
                                <asp:TableCell>6</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>C1</asp:TableCell>
                                <asp:TableCell>12</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow BorderStyle="Solid" BorderColor="Black">
                                <asp:TableCell>C2</asp:TableCell>
                                <asp:TableCell>24</asp:TableCell>
                            </asp:TableRow>

                    </asp:Table>

                </div>

            </section>

        </article>

</asp:Content>
