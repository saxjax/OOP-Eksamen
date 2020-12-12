using NUnit.Framework;

namespace EndToEndTests
{
    public class EndToEndTest
    {
        [SetUp]
        public void Setup()
        {
        }


        /// <summary>
        /// Programmet kan compile
        /// </summary>
        public void TestProgramCanCompile()
        {
            Assert.AreEqual(true, true);
        }


        /// <summary>
        /// Vil man købe en vare, f.eks. en ”sodavand eller nestea (excl pant)”
        /// til 11,50 kr, skriver man sit brugernavn, efterfulgt af 11, og så
        /// bliver der trukket 11,50 kr på ens konto, og man må tage en sodavand.
        /// </summary>
        [Test]
        public void TestQuickBuy()
        {
            Assert.AreEqual(true, true);
        }





        /// <summary>
        /// command: Brugernavn
        /// vis: fulde navn og saldo
        /// Liste over max 10 seneste køb, nyeste øverst.
        /// Hvis saldo er under 50kr skal brugeren informeres med tekst
        /// hvis brugernavnet ikke eksisterer skal brugeren informeres
        /// </summary>
        [Test]
        public void Test1()
        {
            Assert.AreEqual(true, true);
        }



        /// <summary>
        /// command: brugernavn prodNr
        /// Køb af vare
        /// Hvis brugernavnet ikke eksisterer skal brugeren informeres
        /// hvis produktet ikke eksisterer skal brugeren informeres
        /// hvis produktnummeret ikke er et tal, skal brugeren informeres
        /// hvis brugeren ikke har penge, skal et køb ikke gennemføres
        /// </summary>
        [Test]
        public void Test2()
        {
            Assert.AreEqual(true, true);
        }

        /// <summary>
        /// command: brugernavn #antal prodNr
        /// genererer et antal separate transaktioner opdaterer brugerfladen
        /// </summary>
        [Test]
        public void Test3()
        {
            Assert.AreEqual(true, true);
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

        /// <summary>
        /// Følgende administratorkommandoer skal implementeres:
        /// :quit og :q skal afslutte programmet
        /// :activate og :deactivate(efterfulgt af produkt-id)
        /// skal henholdsvis aktivere og deaktivere et produkt(med mindre det er et seasonal produkt)
        /// :crediton og :creditoff(efterfulgt af produkt-id)
        /// skal henholdsvis aktivere og deaktivere om et produkt kan købes på kredit
        /// :addcredits(efterfulgt af brugernavn og tal)--ændret til :insert eller :insertmoney
        /// skal tilføje et antal credits til et brugernavns saldo
        /// </summary>
        [Test]
        public void Test4()
        {
            Assert.AreEqual(true, true);
        }


        //SuperTest
        //        Lav et passende klassedesign der opfylder ovenstående krav, inkl. et passende klassehierarki der
        //reducerer koderedundans.Brug ligeledes passende synlighedsmodifikatorer og datatyper.
        //Navngivning af klasser, egenskaber og metoder står jer frit for. Ligeledes står det jer frit for om –
        //og i givet fald hvordan - I vil foretage datavalidering udover de beskrevne krav. Endelig står det
        //jer frit for at tilføje yderligere funktionalitet (klasser, metoder og properties) udover de eksplicit
        //angivne krav.I forventes således ikke at understøtte data-persistens.
        //Opgaven rummer en del variationsmuligheder mht.opfyldelse af de funktionelle krav, hvorfor
        //der ikke kun findes én rigtig måde at løse den på. Der vil dog være fokus på at I laver et
        //hensigtsmæssigt klassedesign, samt at I sørger for at jeres kode er læsbar.
        //Eventuelle uklarheder i opgaveformuleringen forventes afklaret som en del af
        //opgaveløsningen.

        [Test]
        public void TestSuperTest()
        {
            Assert.AreEqual(true, true);
        }

    }
}