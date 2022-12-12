<%@ Page Title="Strona Główna" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeBehind="harmonogram.aspx.cs" Inherits="narciarze._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <article>

                <div id="panel">
                    harmonogram<br />
                    <input type="text" name="nazwa_wyciagu" value ="Wpisz nazwę wyciągu"/><br />
                    <input type ="submit" name="wyszukaj_wyciag" value="Szukaj"/><br /><br />
                    <input type="text" name="zwrot_id" value ="ID" readonly/>
                    <input type="text" name="zwrot_nazwa" value ="NAZWA" readonly/>
                    <input type="text" name="zwrot_stan" value ="STAN" readonly/><br />
                   Godzina otwarcia: <input type="time" name="zwrot_otwarcie"/><br />
                    Godzina zamknięcia: <input type="time" name="zwrot_zamkniecie"/><br />
                    <input type ="submit" name="zatwiedz_harm" value="Zatwierdź"/>
                </div>
            </article>

</asp:Content>
