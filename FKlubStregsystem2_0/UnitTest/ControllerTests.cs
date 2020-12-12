using NUnit.Framework;

namespace ControllerTests
{
    public class ControllerTest

    {
        [SetUp]
        public void Setup()
        {
        }

        //        StregsystemCommandParser er en klasse der kan oversætte kommandoer i tekststrenge, der
        //skrives af brugeren, til funktionalitet i form af metodekald.StregsystemCommandParser
        //opdaterer brugergrænsefladen og modificerer stregsystemet.
        //Den er karakteriseret ved:
        //Den holder referencer til både Stregsystem og til en IStregsystemUI.
        //På samme måde som IStregsystemUI kunne man definere IStregsystem som
        //interface til Stregsystem klassen.Derved kan man udskifte Stregsystem, og evt. lettere
        //teste StregsystemCommandParser.
        //Dens primære funktionalitet ligger i metoden ParseCommand(string command).
        //En kommando kan have parametre, de er separeret af mellemrum.
        //I tilfælde af at man skriver kommandoen:
        //        fedtmule 11
        //- hvor fedtmule er brugernavn og 11 er produktID, vil der ske følgende:
        //ParseCommand vil parse og bestemme at der er tale om et køb.
        //Der vil blive tjekket om brugernavn findes, samt om strengen ”11” indeholder et tal, og
        //om det er en gyldig reference til et produkt.
        //Er dette overholdt, vil der blive kaldt passende metoder på instansen af Stregsystem, og
        //ud fra resultatet, vil der blive kaldt en passende metode på IStregsystemUI referencen.
        //I tilfælde af at købet går godt, vil metoden DisplayUserBuysProduct blive kaldt,
        //som vil opdatere brugergrænsefladen med passende tekst.
        //Brugergrænsefladen vil nu være klar til at modtage nye kommandoer.
                [Test]
        public void Test1()
        {
            Assert.AreEqual(true,true);
        }

        //Administration

                //        StregsystemCommandParser skal understøtte administrationskommandoer.
                //Administrationskommandoer er kommandoer der starter med et kolon ( ':' ). Dette er hardcodede
                //kommandoer, der kan bruges til at administrere produkter og lukke ned for systemet.Et
                //eksempel vil være kommandoen :q(eller :quit) der skal kunne afslutte programmet(ved
                //kald til Close() på IStregsystemUI instansen)
                //StregsystemCommandParser skal definere et dictionary der mapper
                //administratorkommandoer til funktioner.Et andet eksempel på en
                //administratorkommando kunne være ”activate”:
                //_adminCommands.Add(":activate", (lambda)...Active = true);
                //        activate-kommandoen aktiverer et produkt, altså sætter Active til true

                //        Følgende administratorkommandoer skal implementeres:
                //• :quit og :q
                //◦ skal afslutte programmet
                //• :activate og :deactivate(efterfulgt af produkt-id)
                //◦ skal henholdsvis aktivere og deaktivere et produkt(med mindre det er et seasonal produkt)
                //• :crediton og :creditoff(efterfulgt af produkt-id)
                //◦ skal henholdsvis aktivere og deaktivere om et produkt kan købes på kredit
                //• :addcredits(efterfulgt af brugernavn og tal)
                //◦ skal tilføje et antal credits til et brugernavns saldo
                [Test]
        public void Test2()
        {
            Assert.AreEqual(true, true);
        }


    }
}