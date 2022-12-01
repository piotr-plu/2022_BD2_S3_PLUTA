<%@ Page Title="Trasy" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="trasy.aspx.cs" Inherits="narciarze.trasy" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <article>
            <section>
                    <h2>&compfn; Trasy</h2>
                      
                                <div>
                   <asp:Table ID="Tabela_Stoki_Zielone" runat="server" BorderStyle="Solid" Caption="Grupa Zielona">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell>Nazwa Wyciągu</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                        </asp:TableHeaderRow>

                            <asp:TableRow>
                                <asp:TableCell>A1</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell>A2</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell>A3</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                    </asp:Table>

                </div>

                <div>
                   <asp:Table ID="Tabela_Stoki_Niebieskie" runat="server" BorderStyle="Solid" Caption="Grupa Niebieskie">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell>Nazwa Wyciągu</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                        </asp:TableHeaderRow>

                            <asp:TableRow>
                                <asp:TableCell>B1</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell>B2</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell>B3</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                           <asp:TableRow>
                                <asp:TableCell>B4</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                    </asp:Table>
                </div>

                <div>

                    <asp:Table ID="Tabla_Stoki_Czerwone" runat="server" BorderStyle="Solid" Caption="Grupa Czerwona">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell>Nazwa Wyciągu</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                        </asp:TableHeaderRow>

                            <asp:TableRow >
                                <asp:TableCell>C1</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell>C2</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell>C3</asp:TableCell>
                                <asp:TableCell>Otwarty</asp:TableCell>
                            </asp:TableRow>

                    </asp:Table>

                </div>

            </section>
        </article>

</asp:Content>
