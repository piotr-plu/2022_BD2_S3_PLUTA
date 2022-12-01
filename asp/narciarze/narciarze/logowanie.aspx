<%@ Page Title="Logowanie" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="logowanie.aspx.cs" Inherits="narciarze.logowanie" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <article>

            <section>
                <div id="panel">
                    <form>
                        <label for="username">Nazwa użytkownika:</label>
                        <input type="text" id="username" name="username">
                        <label for="password">Hasło:</label>
                        <input type="password" id="password" name="password">
                        <div id="lower">
                            <input type="submit" value="Login">
                            <a href="Klient/klient.html">Klient</a>
                        </div>
                    </form>
                </div>
              
            </section>

        </article>

</asp:Content>
