<%@ Page Title="Raporty" Language="C#" MasterPageFile="Klient.Master" AutoEventWireup="true" CodeBehind="raporty.aspx.cs" Inherits="narciarze.kontakt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <article>

            <section>
                <h2>Raporty</h2>
                   Wprowadź unikalny identyfikator klienta:<input type="text" name="id_klienta" value ="ID"/><br />
                   Wprowadź interesujący cię okres raportu: <br />
                   Od: <input type="date" name="data_start"/><br />
                   Do: <input type="date" name="data_zak"/><br />
                   <input type="Submit" name="generuj_rap" value="Generuj"/><br />
            </section>
        </article>

</asp:Content>
