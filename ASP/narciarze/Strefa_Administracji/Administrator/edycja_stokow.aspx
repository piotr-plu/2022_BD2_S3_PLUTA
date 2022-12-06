<%@ Page Title="Strona Główna" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeBehind="edycja_stokow.aspx.cs" Inherits="narciarze._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <article>

                <div id="panel">
                    edycja wyciagow<br />
                    <input type="text" name="nazwa_e_wyciagu" value ="Wpisz nazwę wyciagu"/><br />
                    <input type ="submit" name="wyszukaj_wyciag" value="Szukaj"/><br /><br />
                    <input type ="text" name="zwrot_e_wyciag" value="NAZWA"/>
                    <input type ="text" name="zwrot_e_dlugosc" value="DLUGOSC"/>
                    <input type ="submit" name="zatwierdz_stok" value="Zatwierdz"/>
                </div>
            </article>

</asp:Content>
