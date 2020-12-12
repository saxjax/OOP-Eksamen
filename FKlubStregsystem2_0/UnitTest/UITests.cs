using NUnit.Framework;

namespace UITests
{
    public class UITest

    {
        [SetUp]
        public void Setup()
        {
        }

        //        Brugergrænsefladen skal implementeres, som en konsolapplikation.Du har helt frie hænder, men du kan lade dig inspirere af den nuværende brugergrænseflade på figuren ovenfor.
        //Quickbuy er markeret med 1 og syntaxen for køb er beskrevet i punkt 2.
        //I denne opgave skal der i UI fokuseres på:
        //• Præsentation af produkter og priser
        //• Quick-buy(punkt 2 på figuren), som er den primære købsmetode i stregsystemet. • Feedback til brugeren i form af tekst.
        //• Separation af brugergrænseflade
        [Test]// manuelt testet
        public void Test1()
        {
            Assert.AreEqual(true, true);
        }

        //        Der skal laves en klasse kaldet StregsystemCLI, der repræsenterer et command-line interface til stregsystem-klassen.
        //StregsystemCLI er karakteriseret ved:
        //OK• Start En metode der starter brugergrænsefladen.
        //OK Når brugergrænsefladen er startet, vil menuen blive vist, og være klar til at modtage quickbuy
        //kommandoer.Mere om kommandoer i 3. del.
        //OK Brugergrænsefladen skal kun vise aktive produkter.
        //◦ Denne klasse er den eneste i systemet der må skrive noget ud til brugeren!
        //StregsystemCLI har en reference til stregsystem (gerne et interface som beskrevet tidligere), der bruges til at hente data fra, som skal vises til brugeren.
        //StregsystemCLI bør implementere følgende interface: (det er op til dig hvilke parametre de forskellige metoder tager – der er givet et par bud)
        public void Test2()
        {
            Assert.AreEqual(true, false);
        }

        //        Der må ikke være output til brugeren eller modtages fra brugeren andre steder i programmet end i
        //StregsystemCLI!
        //Du kan med fordel lade dig inspirere af Start-implementationen beskrevet i Ugeopgave 1.
        public void Test3()
        {
            Assert.AreEqual(true, false);
        }


    }
}