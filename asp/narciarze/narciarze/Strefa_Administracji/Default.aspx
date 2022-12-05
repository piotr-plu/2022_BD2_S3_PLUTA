<%@ Page Title="Strona Główna" Language="C#" MasterPageFile="~/Strefa_Administracji/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="narciarze._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <article>

                <div id="panel">
                    <form>
                        <label for="username">Nazwa użytkownika:</label>
                        <input type="text" id="username" name="username">
                        <label for="password">Hasło:</label>
                        <input type="password" id="password" name="password">
                        <div id="lower">
                            <input type="submit" value="Login">
                            <a href="Administrator/administrator.html">Administrator  </a>
                            <a href="Kasa/kasa.html">Kasjer  </a>
                            <a href="Zarzad/zarzad.html">Zarzad    </a>
                        </div>
                    </form>
                </div>
            </article>

</asp:Content>
